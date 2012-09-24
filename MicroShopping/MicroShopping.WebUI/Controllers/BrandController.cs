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
    public class BrandController : Controller
    {
        private IProductBrandRepository productBrandRepository;

        public BrandController(IProductBrandRepository _productBrandRepository)
        {
            productBrandRepository = _productBrandRepository;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductBrandModel model)
        {
            if (ModelState.IsValid)
            {
                ProductBrand p = new ProductBrand();
                p.Name = model.Name;
                productBrandRepository.AddBrand(p);
                productBrandRepository.SaveChanges();

                return RedirectToAction("Brands", "Admin");
            }

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var brand = productBrandRepository.FindBrandById(id);
            if (brand != null)
            {
                var model = new ProductBrandModel();
                model.ProductBrandId = brand.ProductBrandId;
                model.Name = brand.Name;
                return View(model);
            }

            return RedirectToAction("Brands", "Admin");
        }

        [HttpPost]
        public ActionResult Edit(ProductBrandModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = productBrandRepository.FindBrandById(model.ProductBrandId);
                brand.Name = model.Name;
                productBrandRepository.SaveChanges();

                return RedirectToAction("Brands", "Admin");
            }

            return View(model);
        }
    }
}
