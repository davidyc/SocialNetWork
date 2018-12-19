using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialNetWorkv1._0.Models;
using SocialNetWorkv1._0.App_Data;
using System.Web.Security;
using System.IO;

namespace SocialNetWorkv1._0.Controllers
{
    [Authorize]
    public class MyPageController : Controller
    {     
        /// <summary>
        /// Актион выводит на страницу всю информацию о пользователе
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            string LoginName = User.Identity.Name; // достаем имя 
            int? id;// для определение по ID
            UserInfo infoAboutUser; // пересенная для хранения информации  пользователе

            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) // поключаемся бд
            {
                Logins tmp = db.Logins.FirstOrDefault(x => x.LoginUser == LoginName); //находим по id польховтаеля
                id = tmp.ID; // передеем ID

                if (id == null) //  если такого пользователя нет
                {
                    return Redirect("~/home/Error");//то плохо
                }

                infoAboutUser = db.UserInfo.Find(id); // в базе ищем инфу по ID

                if (infoAboutUser == null) // если пустой
                {                  
                    return Redirect("~/home/Error");//то плохо
                }
            }
            return View(infoAboutUser); // возращаем вью с инфой по пользователю
        }

        /// <summary>
        /// Редактировать информацию о пользователе
        /// </summary>
        /// <param name="id"> ID Пользователя</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)// если пусто
            {
                return Redirect("~/home/Error");//то плохо
            }

            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                UserInfo userInfo = db.UserInfo.Find(id);// им=щим по ID

                if (userInfo == null)
                {
                    return Redirect("~/home/Error");//то плохо
                }

                ViewBag.ID = new SelectList(db.Logins, "ID", "LoginUser", userInfo.ID); // передаем id пользовтаеля
                return View(userInfo); // передеа во вью модель
            }
          
        }

        /// <summary>
        /// Сохранения изменний о пользователе
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserInfo userInfo)
        {
            if (ModelState.IsValid)// если модель коректна
            {
                using(Soc_NetWorkEntitiesProc db = new Soc_NetWorkEntitiesProc()) // создаем подключение к бд
                {
                    //сохранения  через хранимаую процедуру
                    db.EditUserInfo(userInfo.ID, userInfo.Firstname, userInfo.Lastname, userInfo.Age, userInfo.Email, userInfo.Adress);
                    //без хранимаой процедуры
                    //db.Entry(userInfo).State = EntityState.Modified;
                    //db.SaveChanges();
                    return RedirectToAction("Details");
                }              
            }

            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                // иначе 
                ViewBag.ID = new SelectList(db.Logins, "ID", "LoginUser", userInfo.ID); //передаем ID
                return View(userInfo); // и снова на заполнения 
            }
        }


        /// <summary>
        /// Удаление пользовтаеля
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)// если пусто
            {
                return Redirect("~/home/Error");//то плохо
            }         

            FormsAuthentication.SignOut(); //выход из учетки 

            using(Soc_NetWorkEntitiesProc db = new Soc_NetWorkEntitiesProc()) //подключение 
            {
                db.DeleteUser(id); // удаляем пользователя с базы
            }

            return RedirectToAction("Details"); // переходит на закрытй метод для пользовтелей не вошедших
        }

        /// <summary>
        /// Для редактирвания фото пользователя
        /// </summary>      
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPhoto()
        {
           string Name =  User.Identity.Name;   // записываем имя пользоватлея      
            UserInfo userinfo; // переменная для модели 
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                userinfo = db.UserInfo.FirstOrDefault(x => x.Logins.LoginUser == Name); // ищеи модель по имени пользователя

                if (userinfo == null)
                {
                    return Redirect("~/home/Error");//то плохо
                }

                return View(userinfo);
            }
        }

        /// <summary>
        /// для загрузки фото в приложения 
        /// </summary>
        /// <param name="uploaded"></param>
        /// <param name="id">ID пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpLoad(HttpPostedFileBase uploaded, int? id)
        {
            if (id == null)// если пусто
            {
                return Redirect("~/home/Error");//то плохо
            }

            var uploadd = Request.Files["uploaded"];
            var contentLenght = uploadd.ContentLength; // установим длину файла
            var contentContent = uploadd.ContentType; // типа файла
            var fileName = uploadd.FileName; // имя для файла     
            var inputStream = uploadd.InputStream; // поток записи на сервер

            string ext; // переменная для рассширения

            try // на слчай еслии не был выбюран файл для загрузки
            {
                FileInfo fi = new FileInfo(fileName); // создаем объект фаил инфо 
                ext  = fi.Extension; // получаем расшитерние файла
            }
            catch//(ArgumentException ex)
            {              
                return Redirect("~/home/Error");//то плохо
            }   

            string Name = User.Identity.Name + ext;// назвнеи файла фотмар можно переделать

            string path = @"~/image/" + Name; // путь
            try
            {
                uploaded.SaveAs(path); // записываем файл на сервер   
            }
            catch
            {
              path = @"~/image/no_photo.jpg"; // путь с фото без авы
            }           

            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) // создаем подклюение к базе
            {
                db.UserInfo.Find(id).ImageUser = Name;   // записываем адрес
                db.SaveChanges();  // сохраним изменения
            }
            
            return RedirectToAction("Details"); // переходит на закрытй метод для пользовтелей не вошедших
        }

        /// <summary>
        /// Удаляет фото у пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeletePhoto(int? id)
        {
            string path; // путь до файла
            if (id == null)// если пусто
            {
                return Redirect("~/home/Error");//то плохо
            }

            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) // создаем подклюение к базе
            {
                UserInfo tmp = db.UserInfo.FirstOrDefault(x => x.ID == id); // находим по ID
                string Name = tmp.ImageUser; // записываем имя файла
                db.UserInfo.Find(id).ImageUser = null;   // записываем адрес
                db.SaveChanges();  // сохраним изменения

                path = @"~/image/" + Name; // путь дляудаления
            }
            
            FileInfo fileInf = new FileInfo(path); // передем путь лежит фото
            if (fileInf.Exists)
            {
                fileInf.Delete();//удаляем фото
            }

            return RedirectToAction("Details"); // переходит на закрытй метод для пользовтелей не вошедших
        }
    }
}
