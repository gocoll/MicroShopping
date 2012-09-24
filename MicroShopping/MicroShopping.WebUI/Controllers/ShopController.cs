using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroShopping.Domain.Abstract;
using MicroShopping.WebUI.Models;
using AutoMapper;
using MicroShopping.Domain;
using Stripe;

namespace MicroShopping.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IPackageRepository packageRepository;
        private readonly IUserRepository userRepository;

        public ShopController(IPackageRepository _packageRepository, IUserRepository _userRepository)
        {
            packageRepository = _packageRepository;
            userRepository = _userRepository;
        }

        public ActionResult BuyLances()
        {
            return View();
        }

        public ActionResult PurchaseComplete()
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

        [HttpPost]
        public ActionResult CompleteCheckout()
        {
            if (User.Identity.IsAuthenticated)
            {
                var packageId = Convert.ToInt32(Request.Form["packageId"]);
                var package = packageRepository.FindPackageById(packageId);
                var user = userRepository.FindUserByEmail(User.Identity.Name);

                var api = new StripeClient("2JeZdhBfTR4b1pMH4d9O0S58YtalPDbf");
                var token = Request.Form["stripeToken"];

                var creditCard = new CreditCardToken(token);
                dynamic response = api.CreateCharge(package.Costo, "usd", creditCard);

                if (response.Paid)
                {
                    // Give the user the amount of lances in his bought package.
                    user.LanceCreditBalance += package.CreditAmount;
                    userRepository.SaveChanges();

                    // Save a record of the purchase of the package.
                    BoughtPackage boughtPackage = new BoughtPackage();
                    boughtPackage.DateOfPurchase = DateTime.Now;
                    boughtPackage.UserId = user.UserId;
                    boughtPackage.LancePackageId = package.LancePackageId;
                    boughtPackage.Total = package.Costo;
                    packageRepository.CreateNewPackagePurchase(boughtPackage);
                    packageRepository.SaveChanges();

                    // Redirect to purchase complete page.
                    return RedirectToAction("PurchaseComplete", "Shop");
                }
                else
                {
                    return RedirectToAction("BuyLances", "Shop");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
