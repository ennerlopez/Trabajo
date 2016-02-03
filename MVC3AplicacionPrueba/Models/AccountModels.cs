using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Actual Contraseña")]
        public string OldPassword { get; set; }

        [Required]
      
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contraseña")]
        [Compare("NewPassword", ErrorMessage = "Contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class Login  {
        
        public  Login()
        {

        }

        public static bool ValidateUser(string userName, string password)
        {
            
            var valor = false;

            try
            {
                var contexto = new DataContextoDataContext();

                var query = (from u in contexto.Login_vws
                             where u.nombreUsuario == userName && u.passwordUsuario == password
                             select u);



            bool vf=    query.Any(vw => vw.accionContralador == "" && vw.accionFuncion == "");

                if(query.Any())
                {
                    valor = true;
                }
            }
            catch (Exception)
            {

                
            }

            return valor;
        }

        public static ValidationResult getResultValidation(string userName, string password)
        {
            var validation =new ValidationResult(false,null);
           
            try
            {
                var contexto = new DataContextoDataContext();

                var query = (from u in contexto.Login_vws
                             where u.nombreUsuario == userName && u.passwordUsuario == password
                             select u);

                validation = new ValidationResult(query.Any(),query.ToList());
             
            }
            catch (Exception)
            {


            }

            return validation;
        }

        public class ValidationResult
        {
            public ValidationResult(bool v,List<Login_vw> funciones )
            {
                this.isTrue = v;
                listaFunciones = funciones;
            }
            public bool isTrue { get; private set; }
            public List<Login_vw> listaFunciones { get; private set; } 
        }


        public static int getIdUsuario(string nombre)
        {
             var contexto = new DataContextoDataContext();

            int idUsuaio = (from u in contexto.Usuarios
                            where u.nombreUsuario == nombre
                            select u.codigoUsuario).FirstOrDefault();

            return idUsuaio;
        }

    }
}
