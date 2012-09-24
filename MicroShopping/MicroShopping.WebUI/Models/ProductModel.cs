using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroShopping.WebUI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Debe escribir el nombre del producto.")]
        [Display(Name = "Nombre:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe escribir la descripcion del producto.")]
        [Display(Name = "Descripcion:")]
        [AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessage = "El peso (en miligramos) del producto.")]
        [Display(Name = "Peso en Miligramos:")]
        public decimal WeightInMilligrams { get; set; }

        [Required(ErrorMessage = "Debe escribir el precio regular del producto.")]
        [Display(Name = "Precio Regular en Tiendas:")]
        public decimal SuggestedPrice { get; set; }

        public string CategoryName { get; set; }
        public string BrandName { get; set; }

        public int ProductBrandId { get; set; }

        [Display(Name = "Marca:")]
        public List<SelectListItem> ProductBrands { get; set; }

        public int ProductCategoryId { get; set; }

        [Display(Name = "Categorias:")]
        public List<SelectListItem> ProductCategories{ get; set; }
    }
}