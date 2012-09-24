using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroShopping.WebUI.Models
{
    public class ProductBrandModel
    {
        public int ProductBrandId { get; set; }

        [Required(ErrorMessage = "Debe escribir el nombre de la marca.")]
        [Display(Name = "Nombre de Marca:")]
        public string Name { get; set; }
    }
}