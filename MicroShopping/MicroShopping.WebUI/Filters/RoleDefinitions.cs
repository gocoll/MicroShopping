using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroShopping.WebUI.Filters
{
    public static class RoleDefinitions
    {
        public const string Regular = "Regular";
        public const string UserAdministratorAndBelow = "Regular,User Administrator";
        public const string AuctionAdministratorAndBelow = "Regular,User Administrator,Auction Administrator";
        public const string FinanceAdministratorAndBelow = "Regular,User Administrator,Auction Administrator,Finance Administrator";
        public const string AdminAndBelow = "Regular,User Administrator,Auction Administrator,Finance Administrator,Admin";
        public const string GodAndBelow = "Regular,User Administrator,Auction Administrator,Finance Administrator,Admin,God";

        public const string Staff = "User Administrator,Auction Administrator,Finance Administrator,God";
        public const string FinanceAdministratorAndAbove = "Finance Administrator,Admin,God";
    }
}