using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MicroShopping.WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Debe escribir su correo electronico.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Debe escribir un correo electronico valido.")]
        [Display(Name = "Correo:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe escribir su contraseña.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña:")]
        public string Password { get; set; }
    }
}