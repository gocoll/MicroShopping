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
    }
}
