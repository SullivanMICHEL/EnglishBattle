using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnglishBattle.Models
{
    public class InscriptionViewModel
    {
        [Display(Name = "Nom")]
        [Required]
        public String Nom { get; set; }
        [Display(Name = "Prenom")]
        [Required]
        public String Prenom { get; set; }
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Display(Name = "Mot de passe")]
        [Required]
        [StringLength(100, ErrorMessage = "Le mot de passe doit comporter au moins {2} caractères:", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public String MotDePasse { get; set; }
        [Display(Name = "Niveau")]
        [Required]
        public int Niveau { get; set; }
        [Display(Name = "Ville")]
        [Required]
        public int Ville { get; set; }

    }
}