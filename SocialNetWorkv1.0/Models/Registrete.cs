using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetWorkv1._0.Models
{
    /// <summary>
    /// модель для регистрации
    /// </summary>
    public class Registrete
    {
        /// <summary>
        /// Свойство название логина
        /// </summary>
        [Required]
        [StringLength(15, MinimumLength =3)]
        [Display(Name ="Логин")]
        public string Login { get; set; }

        /// <summary>
        /// пароль для логина
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength =3)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Повторение пароля
        /// </summary>
        [Compare("Password", ErrorMessage="Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name ="Повторите пароль")]
        public string PasswordCopy { get; set; }



    }
}