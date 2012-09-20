using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroShopping.Domain.Abstract;
using MicroShopping.WebUI.Models;
using AutoMapper;
using MicroShopping.Domain;

namespace MicroShopping.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IPackageRepository packageRepository;

        public ShopController(IPackageRepository _packageRepository)
        {
            packageRepository = _packageRepository;
        }

        public ActionResult BuyLances()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckoutPackage()
        {
            var packageId = Convert.ToInt32(Request.Form["packageId"]);
            var package = packageRepository.FindPackageById(packageId);
            var model = Mapper.Map<LancePackage, LancePackageModel>(package);
            return View(model);
        }
    }
}
