using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TrabBimestral.VIEW.Models
{
    public class LoginView
    {
        [Required]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="A {0} precisar ter {2} caracteres.", MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}