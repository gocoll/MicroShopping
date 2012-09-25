using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroShopping.WebUI.Filters;
using MicroShopping.Domain.Abstract;
using MicroShopping.Domain;
using MicroShopping.WebUI.Models;
using AutoMapper;

namespace MicroShopping.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IProductBrandRepository productBrandRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IAuctionRepository auctionRepository;

        public AdminController(IPackageRepository packageRepository, IProductBrandRepository _productBrandRepository, IProductCategoryRepository _productCategoryRepository, IProductRepository _productRepository, IAuctionRepository _auctionRepository)
        {
            _packageRepository = packageRepository;
            productBrandRepository = _productBrandRepository;
            productCategoryRepository = _productCategoryRepository;
            productRepository = _productRepository;
            auctionRepository = _auctionRepository;
        }

        [Role(Roles = RoleDefinitions.Staff)]
        public ActionResult Index()
        {
            return View();
        }

        [Role(Roles = RoleDefinitions.FinanceAdministratorAndAbove)]
        public ActionResult Packages()
        {
            var packages = _packageRepository.FindAllPackages().ToList();
            var model = Mapper.Map<List<LancePackage>, List<LancePackageModel>>(packages);
            return View(model);
        }

        [Role(Roles = RoleDefinitions.FinanceAdministratorAndAbove)]
        public ActionResult SoldPackages()
        {
            var soldPackages = _packageRepository.FindAllBoughtPackages();
            ViewBag.TotalEarnings = soldPackages.Select(x => x.Total).Sum();
            var model = new List<BoughtPackageModel>();

            foreach (var p in soldPackages)
            {
                model.Add(new BoughtPackageModel()
                              {
                                  BoughtPackageId = p.BoughtPackageId,
                                  DateOfPurchase = p.DateOfPurchase,
                                  LancePackageId = (int)p.LancePackageId,
                                  Name = p.LancePackage.Name,
                                  Total = p.Total,
                                  UserId = (int)p.UserId,
                                  UserWhoBoughtEmail = p.User.Email,
                                  UserWhoBoughtName = p.User.Name + " " + p.User.Lastname
                              });
            }

            return View(model);
        }

        [Role(Roles = RoleDefinitions.Staff)]
        public ActionResult Brands()
        {
            var brands = productBrandRepository.FindAllBrands().ToList();
            var model = Mapper.Map<List<ProductBrand>, List<ProductBrandModel>>(brands);
            return View(model);
        }

        [Role(Roles = RoleDefinitions.Staff)]
        public ActionResult Categories()
        {
            var categories = productCategoryRepository.FindAllCategories().ToList();
            var model = Mapper.Map<List<ProductCategory>, List<ProductCategoryModel>>(categories);
            return View(model);
        }

        [Role(Roles = RoleDefinitions.Staff)]
        public ActionResult Products()
        {
            var products = productRepository.FindAllProducts();
            var model = new List<ProductModel>();

            foreach (var product in products)
            {
                model.Add(new ProductModel()
                {
                    BrandName = product.ProductBrand.Name,
                    CategoryName = product.ProductCategory.Name,
                    Description = product.Description,
                    Name = product.Name,
                    ProductId = product.ProductId,
                    SuggestedPrice = (decimal)product.SuggestedPrice,
                    WeightInMilligrams = (decimal)product.WeightInMilligrams
                });
            }

            return View(model);
        }

        [Role(Roles = RoleDefinitions.FinanceAdministratorAndAbove)]
        public ActionResult Auctions()
        {
            var auctions = auctionRepository.FindAllAuctions();
            var model = new List<AuctionModel>();

            foreach (var a in auctions)
            {
                model.Add(new AuctionModel()
                {
                    AuctionId = a.AuctionId,
                    ProductName = a.Product.Name,
                    LanceCost = (decimal)a.LanceCost,
                    StartTime = (DateTime)a.StartTime,
                    IsFinished = a.EndTime != null
                });
            }

            return View(model);
        }
    }
}
