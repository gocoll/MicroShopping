using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroShopping.WebUI.Controllers
{
    public class AuctionController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Finished()
        {
            return PartialView();
        }
    }
}
