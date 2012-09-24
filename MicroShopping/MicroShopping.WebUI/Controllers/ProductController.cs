using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroShopping.Domain;
using MicroShopping.Domain.Abstract;
using MicroShopping.WebUI.Models;

namespace MicroShopping.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductBrandRepository productBrandRepository;
        private IProductCategoryRepository productCategoryRepository;
        private IProductRepository productRepository;

        public ProductController(IProductBrandRepository _productBrandRepository, IProductCategoryRepository _productCategoryRepository, IProductRepository _productRepository)
        {
            productBrandRepository = _productBrandRepository;
            productCategoryRepository = _productCategoryRepository;
            productRepository = _productRepository;
        }

        public ActionResult Create()
        {
            var model = new ProductModel();

            model.ProductCategories = new List<SelectListItem>();
            foreach (var category in productCategoryRepository.FindAllCategories())
            {
                model.ProductCategories.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = category.Name,
                    Value = category.ProductCategoryId.ToString()
                });
            }

            model.ProductBrands = new List<SelectListItem>();
            foreach (var brand in productBrandRepository.FindAllBrands())
            {
                model.ProductBrands.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = brand.Name,
                    Value = brand.ProductBrandId.ToString()
                });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            model.ProductCategories = new List<SelectListItem>();
            foreach (var category in productCategoryRepository.FindAllCategories())
            {
                model.ProductCategories.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = category.Name,
                    Value = category.ProductCategoryId.ToString()
                });
            }

            model.ProductBrands = new List<SelectListItem>();
            foreach (var brand in productBrandRepository.FindAllBrands())
            {
                model.ProductBrands.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = brand.Name,
                    Value = brand.ProductBrandId.ToString()
                });
            }

            if (ModelState.IsValid)
            {
                Product p = new Product();
                p.Name = model.Name;
                p.Description = model.Description;
                p.WeightInMilligrams = model.WeightInMilligrams;
                p.SuggestedPrice = model.SuggestedPrice;
                p.ProductBrandId = model.ProductBrandId;
                p.ProductCategoryId = model.ProductCategoryId;

                productRepository.AddProduct(p);
                productRepository.SaveChanges();

                return RedirectToAction("Products", "Admin");
            }

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var product = productRepository.FindProductById(id);
            if (product != null)
            {
                var model = new ProductModel();
                model.ProductId = product.ProductId;
                model.Name = product.Name;
                model.Description = product.Description;
                model.WeightInMilligrams = (decimal)product.WeightInMilligrams;
                model.SuggestedPrice = (decimal)product.SuggestedPrice;

                model.ProductCategories = new List<SelectListItem>();
                foreach (var category in productCategoryRepository.FindAllCategories())
                {
                    model.ProductCategories.Add(new SelectListItem()
                    {
                        Selected = true,
                        Text = category.Name,
                        Value = category.ProductCategoryId.ToString()
                    });
                }

                model.ProductBrands = new List<SelectListItem>();
                foreach (var brand in productBrandRepository.FindAllBrands())
                {
                    model.ProductBrands.Add(new SelectListItem()
                    {
                        Selected = true,
                        Text = brand.Name,
                        Value = brand.ProductBrandId.ToString()
                    });
                }

                return View(model);
            }

            return RedirectToAction("Products", "Admin");
        }

        [HttpPost]
        public ActionResult Edit(ProductModel model)
        {
            model.ProductCategories = new List<SelectListItem>();
            foreach (var category in productCategoryRepository.FindAllCategories())
            {
                model.ProductCategories.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = category.Name,
                    Value = category.ProductCategoryId.ToString()
                });
            }

            model.ProductBrands = new List<SelectListItem>();
            foreach (var brand in productBrandRepository.FindAllBrands())
            {
                model.ProductBrands.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = brand.Name,
                    Value = brand.ProductBrandId.ToString()
                });
            }

            if (ModelState.IsValid)
            {
                var product = productRepository.FindProductById(model.ProductId);
                product.Name = model.Name;
                product.Description = model.Description;
                product.WeightInMilligrams = model.WeightInMilligrams;
                product.SuggestedPrice = model.SuggestedPrice;
                product.ProductBrandId = model.ProductBrandId;
                product.ProductCategoryId = model.ProductCategoryId;
                productRepository.SaveChanges();
                return RedirectToAction("Products", "Admin");
            }

            return View(model);
        }
            
        public ActionResult Photos(int id)
        {
            var model = new ProductPhotosModel();
            var photosForProduct = productRepository.FindAllPhotosForProduct(id);
            
            model.ProductId = id;
            foreach (var pic in photosForProduct)
            {
                model.ImageUrls.Add(pic.ImageUrl);
            }

            return View(model);
        }

        public ActionResult AddPhoto(int id)
        {
            ViewBag.ProductId = id;
            var product = productRepository.FindProductById(id);
            ViewBag.ProductName = product.Name;
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(int id, HttpPostedFileBase picture)
        {
            if (picture != null && picture.ContentLength > 0)
            {
                var product = productRepository.FindProductById(id);
                var pic = Image.FromStream(picture.InputStream, true, true);

                var filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + picture.FileName;
                var path = Server.MapPath("~/Public/assets/user-content/product-pictures/") + filename;
                pic.Save(path);

                ProductPicture newPicture = new ProductPicture();
                newPicture.ProductId = product.ProductId;
                newPicture.ImageUrl = Url.Content("~/Public/assets/user-content/product-pictures/") + filename;

                productRepository.AddProductPicture(newPicture);
                productRepository.SaveChanges();

                return RedirectToAction("Photos", "Product", new { id = id });
            }

            return RedirectToAction("AddPhoto", id);
        }
    }
}
