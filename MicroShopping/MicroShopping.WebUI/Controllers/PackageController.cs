using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroShopping.Domain.Abstract;
using AutoMapper;
using MicroShopping.Domain;
using MicroShopping.WebUI.Models;
using MicroShopping.WebUI.Filters;

namespace MicroShopping.WebUI.Controllers
{
    [Role(Roles=RoleDefinitions.FinanceAdministratorAndAbove)]
    public class PackageController : Controller
    {
        private readonly IPackageRepository packageRepository;

        public PackageController(IPackageRepository _packageRepository)
        {
            packageRepository = _packageRepository;
        }

        public ActionResult Edit(int id)
        {
            var package = packageRepository.FindPackageById(id);
            var model = Mapper.Map<LancePackage, LancePackageModel>(package);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(LancePackageModel model)
        {
            if (ModelState.IsValid)
            {
                var package = packageRepository.FindPackageById(model.LancePackageId);
                package.Name = model.Name;
                package.Costo = model.Costo;
                package.CreditAmount = model.CreditAmount;
                packageRepository.SaveChanges();

                return RedirectToAction("Packages", "Admin");
            }

            return View(model);
        }
    }
}
