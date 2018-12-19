using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetWork._0.Controllers
{
     // для всех пользовтателей
    public class HomeController : Controller
    {
        /// <summary>
        /// стартоврый мактион
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Метод для проверки выдапчи правап админу
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Test()
        {
            return View();
        }

        /// <summary>
        ///  Выводит ошибку
        /// </summary>
        /// <returns>Представление с ошибкой</returns>
        public ActionResult Error()
        {
            return View();
        }


    }


}