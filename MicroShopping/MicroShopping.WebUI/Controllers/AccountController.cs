using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MicroShopping.Domain.Abstract;
using MicroShopping.Domain.Concrete;
using MicroShopping.WebUI.Models;

namespace MicroShopping.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderRepository _genderRepository;

        public AccountController(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public ActionResult Register()
        {
            var model = new UserModel();

            model.Genders = new List<SelectListItem>();

            foreach (var gender in _genderRepository.FindAllGenders())
            {
                model.Genders.Add(new SelectListItem()
                                      {
                                          Selected = true,
                                          Text = gender.Name,
                                          Value = gender.GenderId.ToString()
                                      });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            return View();
        }

        public ActionResult RegistrationComplete()
        {
            return View();
        }
    }
}
