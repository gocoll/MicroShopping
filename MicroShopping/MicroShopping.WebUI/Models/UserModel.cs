﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroShopping.WebUI.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Debe escribir su nombre.")]
        [Display(Name = "Nombre:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe escribir su apellido.")]
        [Display(Name = "Apellido:")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Debe escribir su correo electronico.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Debe escribir un correo electronico valido.")]
        [Display(Name = "Correo:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe escribir su telefono.")]
        [Display(Name = "Telefono:")]
        public string Telephone { get; set; }

        [Display(Name = "Celular:")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Debe escribir su carnet.")]
        [Display(Name = "Carnet:")]
        public string Carnet { get; set; }

        [Required(ErrorMessage = "Debe escribir su fecha de nacimiento.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfRegistry { get; set; }

        public DateTime LastDateLogin { get; set; }

        public bool IsActive { get; set; }

        public int LanceCreditBalance { get; set; }

        public int LancesSpent { get; set; }

        [Required(ErrorMessage = "Debe escribir un nombre usuario.")]
        [Display(Name = "Nombre de Usuario:")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Debe elegir una contraseña.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña:")]
        [Compare("Password", ErrorMessage = "Las dos contraseñas no coinciden. Por favor escriba la misma contraseña dos veces.")]
        public string ConfirmPassword { get; set; }

        public string EmailVerificationCode { get; set; }

        public int GenderId { get; set; }

        [Display(Name = "Sexo:")]
        public List<SelectListItem> Genders { get; set; }

        [Required(ErrorMessage = "Debe escribir su direccion.")]
        [Display(Name = "Direccion:")]
        public string Address { get; set; }
    }
}