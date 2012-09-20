using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroShopping.WebUI.Filters;

namespace MicroShopping.WebUI.Controllers
{
    [Role(Roles=RoleDefinitions.Staff)]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
