using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroShopping.WebUI.Models
{
    public class ProductCategoryModel
    {
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage = "Debe escribir el nombre de la categoria.")]
        [Display(Name = "Nombre de Categoria:")]
        public string Name { get; set; }
    }
}