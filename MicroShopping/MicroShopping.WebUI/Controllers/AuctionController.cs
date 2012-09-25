using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroShopping.Domain;
using MicroShopping.Domain.Abstract;
using MicroShopping.WebUI.Models;

namespace MicroShopping.WebUI.Controllers
{
    public class AuctionController : Controller
    {
        private IAuctionRepository auctionRepository;
        private IProductRepository productRepository;

        public AuctionController(IAuctionRepository _auctionRepository, IProductRepository _productRepository)
        {
            auctionRepository = _auctionRepository;
            productRepository = _productRepository;
        }

        public ActionResult Index()
        {
            /* Open auctions that need opening. */
            var needToOpenAuctions = auctionRepository.FindAllAuctions()
                                                      .Where(x => x.EndTime == null)
                                                      .Where(x => x.StartTime <= DateTime.Now);
            foreach (var a in needToOpenAuctions)
            {
                a.IsActive = true;
            }
            auctionRepository.SaveChanges();
            /* Done opening auctions. */

            var upcomingAuctions = auctionRepository.FindAllAuctions()
                                                    .Where(x => x.EndTime == null)
                                                    .OrderBy(x => x.StartTime);

            var model = new List<AuctionModel>();
            
            foreach (var a in upcomingAuctions)
            {
                var auction = new AuctionModel();
                auction.ProductName = a.Product.Name;
                auction.AuctionId = a.AuctionId;
                auction.StartTime = (DateTime)a.StartTime;
                auction.IsActive = (bool)a.IsActive;
                auction.LanceCost = (decimal)a.LanceCost;
                auction.Thumbnail = a.Product.ProductPictures.First().ImageUrl;

                var latestBidder = a.UserAuctionLances.LastOrDefault();
                if (latestBidder != null)
                    auction.LatestBidder = latestBidder.UserName;
                else
                    auction.LatestBidder = "Ninguna";

                model.Add(auction);
            }
            return PartialView(model);
        }

        public ActionResult Finished()
        {
            return PartialView();
        }

        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new AuctionModel();

            model.Products = new List<SelectListItem>();
            foreach (var category in productRepository.FindAllProducts())
            {
                model.Products.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = category.Name,
                    Value = category.ProductCategoryId.ToString()
                });
            }

            model.StartTime = DateTime.Now;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AuctionModel model)
        {
            model.Products = new List<SelectListItem>();
            foreach (var category in productRepository.FindAllProducts())
            {
                model.Products.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = category.Name,
                    Value = category.ProductCategoryId.ToString()
                });
            }

            if (model.StartTime <= DateTime.Now)
            {
                ModelState.AddModelError("", "No puede crear un remate en el pasado.");
            }

            if (ModelState.IsValid)
            {
                Auction a = new Auction();
                a.ProductId = model.ProductId;
                a.SerialNumber = model.SerialNumber;
                a.IsActive = model.IsActive;
                a.StartTime = new DateTime(model.StartTime.Year, model.StartTime.Month, model.StartTime.Day, model.StartTime.Hour, model.StartTime.Minute, 0);
                a.LanceCost = model.LanceCost;
                a.RegularCost = model.RegularCost;

                auctionRepository.AddAuction(a);
                auctionRepository.SaveChanges();

                return RedirectToAction("Auctions", "Admin");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var auction = auctionRepository.FindAuctionById(id);
            if (auction != null)
            {
                var model = new AuctionModel();
                model.ProductId = (int)auction.ProductId;
                model.AuctionId = (int) auction.AuctionId;
                model.SerialNumber = auction.SerialNumber;
                model.IsActive = (bool)auction.IsActive;
                model.StartTime = (DateTime)auction.StartTime;
                model.LanceCost = (decimal)auction.LanceCost;
                model.RegularCost = (decimal)auction.RegularCost;

                model.Products = new List<SelectListItem>();
                foreach (var category in productRepository.FindAllProducts())
                {
                    model.Products.Add(new SelectListItem()
                    {
                        Selected = true,
                        Text = category.Name,
                        Value = category.ProductCategoryId.ToString()
                    });
                }

                return View(model);
            }

            return RedirectToAction("Auctions", "Admin");
        }

        [HttpPost]
        public ActionResult Edit(AuctionModel model)
        {
            model.Products = new List<SelectListItem>();
            foreach (var category in productRepository.FindAllProducts())
            {
                model.Products.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = category.Name,
                    Value = category.ProductCategoryId.ToString()
                });
            }

            if (ModelState.IsValid)
            {
                var auction = auctionRepository.FindAuctionById(model.AuctionId);
                auction.ProductId = model.ProductId;
                auction.SerialNumber = model.SerialNumber;
                auction.IsActive = model.IsActive;
                auction.StartTime = new DateTime(model.StartTime.Year, model.StartTime.Month, model.StartTime.Day, model.StartTime.Hour, model.StartTime.Minute, 0); ;
                auction.LanceCost = model.LanceCost;
                auction.RegularCost = model.RegularCost;

                auctionRepository.SaveChanges();
                return RedirectToAction("Auctions", "Admin");
            }

            return View(model);
        }
    }
}
