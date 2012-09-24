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

        public AdminController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
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
    }
}
