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
    public class CategoryController : Controller
    {
        private IProductCategoryRepository productCategoryRepository;

        public CategoryController(IProductCategoryRepository _productCategoryRepository)
        {
            productCategoryRepository = _productCategoryRepository;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                ProductCategory p = new ProductCategory();
                p.Name = model.Name;
                productCategoryRepository.AddCategory(p);
                productCategoryRepository.SaveChanges();

                return RedirectToAction("Categories", "Admin");
            }

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var category = productCategoryRepository.FindCategoryById(id);
            if (category != null)
            {
                var model = new ProductCategoryModel();
                model.ProductCategoryId = category.ProductCategoryId;
                model.Name = category.Name;
                return View(model);    
            }

            return RedirectToAction("Categories", "Admin");
        }

        [HttpPost]
        public ActionResult Edit(ProductCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = productCategoryRepository.FindCategoryById(model.ProductCategoryId);
                category.Name = model.Name;
                productCategoryRepository.SaveChanges();

                return RedirectToAction("Categories", "Admin");
            }

            return View(model);
        }

    }
}
