using SocialNetWorkv1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SocialNetWorkv1._0.Controllers
{
    /// <summary>
    /// контролеер для регистрации полльзовтелей и авторизации
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Акшен для авторизации
        /// </summary>
        /// <returns>Вью для авторизации</returns>
        /// GET
        public ActionResult Logon()
        {
            return View();
        }

        /// <summary>
        /// Акшен для входа
        /// </summary>
        /// <param name="model">Модоль регитрации ЛОГОН</param>
        /// <returns></returns>
        /// POS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logon(Logon model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Logins user = null; //пустая модель для записи 
                using (Soc_NetWorkCF db = new Soc_NetWorkCF()) // открываем контекст
                {
                    // если есть совпадения по паролю и логину
                    user = db.Logins.FirstOrDefault(u => u.LoginUser == model.LoginUser 
                    && u.PasswordUser == model.PasswordUser);
                }

                if (user != null) //  если было 
                {
                    FormsAuthentication.SetAuthCookie(model.LoginUser, true); /// авторизация через куки
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error =  "Пользователя с таким логином и паролем нет"; // передает ошибку во вью
                }
            }

            return View(model); // возращает модель
        }


        /// <summary>
        /// Для регистрации
        /// </summary>
        /// <returns>Вью на страницу регистрации</returns>        
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Пост метод для регистрации
        /// </summary>
        /// <returns>Вью на главную страницу</returns>
        [HttpPost]
        public ActionResult Registration(Registrete reg)
        {
            // констект хранимых процедур
            using (App_Data.Soc_NetWorkEntitiesProc pro = new App_Data.Soc_NetWorkEntitiesProc())
            {
                //подключаем конткст бд
                using (Soc_NetWorkCF db = new Soc_NetWorkCF())
                {
                    if (db.Logins.Any(x => x.LoginUser == reg.Login)) // если уже есть в базхе такой логин
                    {
                        ViewBag.Error = "Такой логин уже есть"; // для вывода сообщения обшиьке на странице
                        return View(reg); // возращает модель на исправление
                    }

                    int maxID = db.Logins.Max(x => x.ID); // записываем максимальное значени 
                    maxID++; // увеличиваем ID
                    pro.AddUser(reg.Login, reg.Password, maxID);
                }
            }
            return RedirectToAction("Logon"); //перехордит на страицу входа 
        }


        /// <summary>
        /// для выходы учетной записи
        /// </summary>
        /// <returns></returns>
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut(); //выход из учетки
            return RedirectToAction("Index", "Home"); //переходд на гл страницу
        }
    }
}