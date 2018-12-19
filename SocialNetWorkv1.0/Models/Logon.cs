using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetWorkv1._0.Models
{
    /// <summary>
    /// Модель для входа на сайт
    /// </summary>
    public class Logon
    {
        /// <summary>
        /// Свойтсво логин узера
        /// </summary>
        [Display(Name = "Логин")]
        public string LoginUser { get; set; }

        /// <summary>
        /// свойсвто пароль узера
        /// </summary>
        [Display(Name ="Пароль")]
        [DataType(DataType.Password)]
        public string PasswordUser { get; set; }
    }
}