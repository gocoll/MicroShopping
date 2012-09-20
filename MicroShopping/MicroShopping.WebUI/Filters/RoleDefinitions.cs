using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroShopping.WebUI.Filters
{
    public static class RoleDefinitions
    {
        public const string Regular = "Regular";
        public const string UserAdministrator = "Regular, User Administrator";
        public const string AuctionAdministrator = "Regular, User Administrator, Auction Administrator";
        public const string FinanceAdministrator = "Regular, User Administrator, Auction Administrator, Finance Administrator";
        public const string Admin = "Regular, User Administrator, Auction Administrator, Finance Administrator";
        public const string God = "Regular, User Administrator, Auction Administrator, Finance Administrator, God";
    }
}