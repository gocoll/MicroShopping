using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroShopping.Domain.Concrete;

namespace MicroShopping.WebUI.Filters
{
    public class RoleAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.Request.IsAuthenticated)
                return false;
            
            using (EfUserRepository userRepository = new EfUserRepository())
            {
                var user = userRepository.FindUserByEmail(httpContext.User.Identity.Name);
                string role = userRepository.FindRoleForUserById(user.UserId);

                foreach (string definedRole in this.Roles.Split(','))
                {
                    if (role == definedRole)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/");
            base.HandleUnauthorizedRequest(filterContext);
        }

    }
}