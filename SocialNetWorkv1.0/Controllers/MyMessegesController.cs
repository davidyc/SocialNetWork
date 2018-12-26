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
using System.Text.RegularExpressions;

namespace SocialNetWorkv1._0.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class MyMessegesController : Controller
    {

        /// <summary>
        /// Метод выводит список переписок пользоывателя
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) // создаем подклбючение
            {
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                int? userID = tmpLogin.ID; // передеем ID

                // получаем список всех пользовтелей из списка друзей пользователя по ID
                var UserMess = db.UserMessege // выбираем из таблии сообщений
                    .Include(u => u.Logins)  // таблицы с длогинами
                    .Include(u => u.Logins1)
                    .Where(x => x.IDrSender == userID || x.IDCatcher == userID); // где отправитель или получатель пользователь

                UserMess.OrderByDescending(x => x.SendTime); // сортируем по времени

                List<HerderChat> Listtmp = new List<HerderChat>(); // создаем список диалогов

                foreach (var item in UserMess) // проходимся по колеекции
                {
                    HerderChat tmp = new HerderChat();// создаем объект переписки(сообщения)

                    if (item.IDCatcher == userID)// если получатель пользователь
                    {
                        tmp.ID = item.IDrSender;  // записывваем ID и Логин
                        tmp.Login = item.Logins1.LoginUser;
                    }
                    else // если отправиьель пользователь
                    {
                        tmp.ID = item.IDCatcher;
                        tmp.Login = item.Logins.LoginUser;
                        tmp.SenderFlag = true; // установим флаг рассветки страницы по цвета отправитель получатель
                    }

                    tmp.LastMassege = item.MessageText; // записывает текс

                    Listtmp.Add(tmp); // добавляем в коллецию
                }

                //групируем и выбираем послдений  для заголовков
                var GroupListtmp = Listtmp.GroupBy(x => x.ID).Select(y => y.Last());

                return View(GroupListtmp.ToList());// предеем во вью
            }
        }

        /// <summary>
        /// показывает переписки с пользовтаелем по ID
        /// </summary>
        /// <param name="id">ID собеседника</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)// если нету 
            {
                return RedirectToAction("Error", "Home");// то плохо
            }

            List<Chat> tmpChat = ReturnChat(id); // получаем и переписку

            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                ViewBag.ID = id; // передаем id во ВЬЮ
                var userName = db.Logins.Find(id);
                ViewBag.Name = userName.LoginUser; // передаем логин во вью    

                return View(tmpChat);//передаем список моделйц во вью
            }
        }

        /// <summary>
        /// Перенаправляем на страницу пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns></returns>
        public ActionResult RedirectToInfoUser(int? id)
        {
            return RedirectToAction("Details", "Search", new { @id = id });
        }

        /// <summary>
        /// Отправляет сообщение 
        /// </summary>
        /// <param name="catcher">ID получателя</param>
        /// <param name="text">Текст сообщения</param>
        /// <returns></returns>   
        public ActionResult PostMessege(int? catcher, string text)
        {
            int? userID; // ID юзера
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())// подключение
            {
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                userID = tmpLogin.ID; // передеем ID 
            }

            if (catcher == null)// если нету 
            {
                return RedirectToAction("Error", "Home");// то плохо
            }

            string texttmp = Zamena(text); // проверка на плохие слова

            using (Soc_NetWorkEntitiesProc db = new Soc_NetWorkEntitiesProc()) // создаем подключение к бд
            {
               db.SendMessage(userID, catcher, texttmp, DateTime.Now); // вызываем хранимую процеуру 
            }

            List<Chat> tmpChat = ReturnChat(catcher); // получаем и переписку


            return PartialView("ChatMessege", tmpChat); // перенаправляем на переписку
        }

        /// <summary>
        /// Метод повеяет на наличие плохих слов
        /// </summary>
        /// <param name="str">строку для повеки</param>
        /// <returns>Возращает чистую строку</returns>
        public string Zamena(string str)
        {
            using(Soc_NetWorkCF db = new Soc_NetWorkCF()) // создаем подключение
            { 
                var BadWords = db.BadWord;// получаем с базы все плохие слова 

                foreach (var item in BadWords) // перебираем коллекцию 
                {
                    str = Regex.Replace(str, item.word, "***"); // ну собственно замена
                }

                return str; // вернм правление слова
            }
       
        }

        /// <summary>
        /// Метод  мвозращае переписку с пользовтелм
        /// </summary>
        /// <param name="id">ID собеседника</param>
        /// <returns></returns>
        public List<Chat> ReturnChat(int? id)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                int? userID = tmpLogin.ID; // передеем ID

                // получаем список всех пользовтелей из списка друзей пользователя по ID
                var UserMess = db.UserMessege // выбираем из таблии сообщений
                    .Include(u => u.Logins)  // таблицы с длогинами
                    .Include(u => u.Logins1)
                    // где отправитель пользотватель и получатель по ID или наоборот 
                    .Where(x => x.IDrSender == userID && x.IDCatcher == id || x.IDCatcher == userID && x.IDrSender == id);

                UserMess.OrderByDescending(x => x.SendTime); // сортируем по времени

                List<Chat> ListTmp = new List<Chat>(); // оздаем объект для хранения переписки всех               

                foreach (var item in UserMess) // перебираем список
                {
                    Chat tmp = new Chat(); // создаем временный отчет 

                    if (item.IDCatcher == userID) // если от получатель пользователь
                    {
                        tmp.ID = item.IDrSender; // записываем данные
                        tmp.Login = item.Logins1.LoginUser;
                    }
                    else // если пользовтель отрпаввитель
                    {
                        tmp.ID = item.IDCatcher;
                        tmp.Login = item.Logins.LoginUser;
                        tmp.SenderFlag = true; // установим флаг рассветки страницы по цвета отправитель получатель
                    }

                    tmp.Text = item.MessageText; // записываем текст 
                    tmp.SendTime = item.SendTime; // записываем время                  

                    ListTmp.Add(tmp);//добавляем в список
                }

                return ListTmp;
            }

        }
    }
}
