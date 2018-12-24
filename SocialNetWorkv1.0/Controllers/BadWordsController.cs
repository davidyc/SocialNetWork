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
    public class BadWordsController : Controller
    {

        /// <summary>
        ////Выводит список всех плохих слов
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                return View(db.BadWord.ToList());
            }
        }        

        /// <summary>
        /// Выводит страницу добавления плохого слова
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

       
        /// <summary>
        /// созадет плохое слово
        /// </summary>
        /// <param name="badWord">Объек класс плохие слова для обработки</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BadWord badWord)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())//создаем подключение 
            {
                if (ModelState.IsValid) // провекра на валидность
                {
                    db.BadWord.Add(badWord); // добавить слово 
                    db.SaveChanges(); // сохраним изменинеия
                    return RedirectToAction("Index");
                }
                return View(badWord); 
            }
        }

        /// <summary>
        /// Редактирования плохого слова 
        /// </summary>
        /// <param name="id"> ID плохого слова</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            { 
                if (id == null)
                {
                    ViewBag.Error = "Такого пользователя нет";
                    return Redirect("~/home/Error");//то плохо
                }

                BadWord badWord = db.BadWord.Find(id);

                if (badWord == null)// если нет то плоха
                {
                    ViewBag.Error = "Такого пользователя нет";
                    return Redirect("~/home/Error");//то плохо
                }
                return View(badWord);
            }
        }

        // POST: BadWords/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,word")] BadWord badWord)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(badWord).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(badWord);
            }
        }
        
        /// <summary>
        /// удаляет плохое слово
        /// </summary>
        /// <param name="id">id слова</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                if (id == null) // если нет пенреданного id 
                {
                    ViewBag.Error = "Такого пользователя нет";
                    return Redirect("~/home/Error");//то плохом
                }

                BadWord badWord = db.BadWord.Find(id); // ищем по id плохое слова в базе

                if (badWord == null) // если не находим
                {
                    return Redirect("~/home/Error");//то плохом
                }
                return View(badWord);
            }
        }

        /// <summary>
        /// Пост версия удаления плохого слова
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null) // если нет пенреданного id 
            {
                ViewBag.Error = "Такого пользователя нет";
                return Redirect("~/home/Error");//то плохом
            }

            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) // подключеемся
            {
                BadWord badWord = db.BadWord.Find(id); // нвходим его в базе

                if (badWord == null) // если не находим
                {
                    return Redirect("~/home/Error");//то плохом
                }

                db.BadWord.Remove(badWord); // удаляем его из базы
                db.SaveChanges();//сорхраняем изменния
                return RedirectToAction("Index");
            }
        }
    }
}
