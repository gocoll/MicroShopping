using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroShopping.WebUI.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult BuyLances()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckoutPackage()
        {
            var packageId = Request.Form["packageId"];

            return View();
        }
    }
}
