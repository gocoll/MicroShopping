using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroShopping.WebUI.Models
{
    public class ProductPhotosModel
    {
        public ProductPhotosModel()
        {
            ImageUrls = new List<string>();
        }

        public int ProductId { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}