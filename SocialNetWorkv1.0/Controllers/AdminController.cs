using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialNetWorkv1._0.Models;

namespace SocialNetWorkv1._0.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {  
        /// <summary>
        ////Выводит всех пользователей
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())//подключение
            {
                var userInfo = db.UserInfo.Include(u => u.Logins);// получаем по всех пользователей
                return View(userInfo.ToList());
            }
        }

        /// <summary>
        /// Получаем информавцию о пользовтале
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())// создаем подключение
            {
                if (id == null) //  если такого пользователя нет
                {                    
                    return Redirect("~/home/Error");//то плохо
                }

                UserInfo userInfo = db.UserInfo.Find(id);// находим по ID

                if (userInfo == null)
                {                   
                    return Redirect("~/home/Error");//то плохо
                }

                Logins logTmp = db.Logins.Find(id);

                if (logTmp.LoginUser == User.Identity.Name) // при попытки перейти на самого себя
                {                   
                    return Redirect("~/home/Error");//то плохо
                }

                ViewBag.ID = ReturnRoleID(id);
                return View(userInfo);// возращаем пользователя
            }
        }

        /// <summary>
        /// Повывает роль пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
       [HttpPost]
        public ActionResult UpRoles(int? id)
        {
            using(Soc_NetWorkCF db = new Soc_NetWorkCF())// создаем подключение
            {
                if (id == null) //  если такого пользователя нет
                {
                    ViewBag.Error = "Такого пользователя нет";
                    return Redirect("~/home/Error");//то плохо
                }

                var userrole = db.Logins.FirstOrDefault(x=>x.ID == id);// плучаем логитн с роли

                if(userrole == null)
                {
                    ViewBag.Error = "Такого пользователя нет";
                    return Redirect("~/home/Error");//то плохо
                }

                int? idRoles = userrole.RoleUser; // получаем ID роли пользователя
                 
                if(idRoles == 1 || idRoles == null)
                {
                    ViewBag.Error = "У пользователя нет роли или он уже админ";
                    return Redirect("~/home/Error");//то плохо
                }

                db.Logins.FirstOrDefault(x => x.ID == id).RoleUser--;// увеличиваем роль
                db.SaveChanges();//сохраним изменния
            }

            return RedirectToAction("Details", new { id = id });
        }

        /// <summary>
        /// Снижает роль пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DownRoles(int? id)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())// создаем подключение
            {
                if (id == null) //  если такого пользователя нет
                {
                    ViewBag.Error = "Такого пользователя нет";
                    return Redirect("~/home/Error");//то плохо
                }

                var userrole = db.Logins.FirstOrDefault(x => x.ID == id);// плучаем логитн с роли

                if (userrole == null)
                {
                    ViewBag.Error = "Такого пользователя нет";
                    return Redirect("~/home/Error");//то плохо
                }

                int? idRoles = userrole.RoleUser; // получаем ID роли пользователя

                if (idRoles == 3 || idRoles == null || id == 1) // что бы нельзя было заброль админку у 01
                {
                    ViewBag.Error = "У пользователя нет роли или он уже забанен";
                    return Redirect("~/home/Error");//то плохо
                }

                db.Logins.FirstOrDefault(x => x.ID == id).RoleUser++;// увеличиваем роль
                db.SaveChanges();//сохраним изменния
            }

            return RedirectToAction("Details", new { id = id });
        }

        /// <summary>
        /// Перенаправляет на страницу с плохими словами
        /// </summary>
        /// <returns></returns>
        public ActionResult MessModeration()
        {
            return RedirectToAction("Index", "BadWords");
        }


        /// <summary>
        /// возращает ID роли ползователя
        /// </summary>
        /// <param name="iduser">ID пользователя</param>
        /// <returns></returns>
        public static int? ReturnRoleID(int? iduser)
        {
            using(Soc_NetWorkCF db = new Soc_NetWorkCF())// подключение
            {
                Logins tmp = db.Logins.FirstOrDefault(x => x.ID == iduser);// получаем логин
                int? idroles = tmp.Roles.ID; // возращаем ID 
                
                return idroles;// возращает ID роли
            }   
        }
             
    }
}
