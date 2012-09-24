using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroShopping.WebUI.Models
{
    public class BoughtPackageModel
    {
        public int BoughtPackageId { get; set; }
        public int UserId { get; set; }
        public int LancePackageId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public decimal Total { get; set; }
        public string Name { get; set; }
        public string UserWhoBoughtName { get; set; }
        public string UserWhoBoughtEmail { get; set; }
    }
}