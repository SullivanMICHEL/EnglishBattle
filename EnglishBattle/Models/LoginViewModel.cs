using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnglishBattle.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Mot de passe")]
        [Required]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }
    }
}