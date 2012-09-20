using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MicroShopping.WebUI.Models
{
    public class LancePackageModel
    {
        public int LancePackageId { get; set; }

        [Required(ErrorMessage = "El paquete debe tener un nombre.")]
        [Display(Name = "Nombre de Paquete:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe tener una cantidad de creditos para abonar.")]
        [Display(Name = "Creditos Dentro de Paquete:")]
        public int CreditAmount { get; set; }

        [Required(ErrorMessage = "Debe escribir el precio del paquete.")]
        [Display(Name = "Costo (en dolares):")]
        public decimal Costo { get; set; }
    }
}