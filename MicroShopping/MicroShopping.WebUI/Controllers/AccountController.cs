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
using AutoMapper;
using MicroShopping.Domain;
using MicroShopping.WebUI.Helpers;

namespace MicroShopping.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IUserRepository _userRepository;

        public AccountController(IGenderRepository genderRepository, IUserRepository userRepository)
        {
            _genderRepository = genderRepository;
            _userRepository = userRepository;
        }

        public ActionResult EditProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userRepository.FindUserByEmail(User.Identity.Name);
                if (user == null)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Index", "Home");
                }

                var model = Mapper.Map<User, ProfileModel>(user);
                model.Password = "";
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

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult EditProfile(ProfileModel model)
        {
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

            if (ModelState.IsValid)
            {
                var user = _userRepository.FindUserById(model.UserId);
                var existingNickname = _userRepository.FindUserByNickname(model.Nickname);
                if (user.Nickname != model.Nickname && existingNickname != null)
                {
                    ModelState.AddModelError("", "Ese nombre de usuario ya esta siendo utilizado. Eliga otro.");
                    return View(model);
                }

                user.Nickname = model.Nickname;
                user.Name = model.Name;
                user.Lastname = model.Lastname;
                user.DateOfBirth = model.DateOfBirth;
                user.GenderId = model.GenderId;
                user.Address = model.Address;
                user.Telephone = model.Telephone;
                user.MobilePhone = model.MobilePhone;

                if (!String.IsNullOrWhiteSpace(model.Password))
                {
                    if (model.Password == model.ConfirmPassword)
                    {
                        user.Password = SecurityHelpers.HashPassword(model.Password);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Las dos contraseñas no coinciden.");
                        return View(model);
                    }
                }

                _userRepository.SaveChanges();

                return RedirectToAction("Profile", "Account", new { nickname = user.Nickname });
            }

            return View(model);
        }

        public ActionResult Profile(string nickname)
        {
            var user = _userRepository.FindUserByNickname(nickname);
            if (user == null) return RedirectToAction("NotFound", "Error");

            var model = Mapper.Map<User, ProfileModel>(user);
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.ValidateCredentials(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Su correo o contraseña no estan correctas.");
                    return View(model);
                }
            }

            return View(model);
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
            // Reload the items inside the Gender drop down list in case the form doesn't clear.
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

            if (ModelState.IsValid)
            {
                var newUser = Mapper.Map<UserModel, User>(model);

                /* Initial configuration for new users. */
                newUser.LanceCreditBalance = 10;
                newUser.LastDateLogin = DateTime.Now;
                newUser.DateOfRegistry = DateTime.Now;
                newUser.EmailVerificationCode = SecurityHelpers.GenerateRandomToken();
                newUser.Password = SecurityHelpers.HashPassword(newUser.Password);
                newUser.UserRoleId = 1; // Role for a 'regular user'.

                var result = _userRepository.CreateUser(newUser);
                if (result == UserCreationResults.Ok)
	            {
		            //EmailHelpers.SendConfirmationEmail(newUser);
                    FormsAuthentication.SetAuthCookie(newUser.Email, true);
                    return RedirectToAction("Index", "Home");
	            }
                else 
                {
                    switch (result)
	                {
                        case UserCreationResults.EmailAlreadyExists:
                            ModelState.AddModelError("", "Ese correo ya esta en uso.");
                            break;
                        case UserCreationResults.CarnetAlreadyExists:
                            ModelState.AddModelError("", "Su carnet ya esta siendo utilizado. Solo puede tener una cuenta.");
                            break;
                        case UserCreationResults.UnknownError:
                            ModelState.AddModelError("", "Algo sucedio durante el registro. Por favor intente de nuevo.");
                            break;
                        default:
                            break;
	                }
                }
            }

            return View(model);
        }

        public ActionResult RegistrationComplete()
        {
            return View();
        }
    }
}
