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
    [Authorize(Roles = "Admin, User")]
    public class SearchController : Controller
    {
        /// <summary>
        ///  вводит страницу поискв
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(); // в виде списка передаем во вью            
        }

        /// <summary>
        /// Ищет по логину пользовтеля
        /// </summary>
        /// <param name="login">логин пользователя</param>
        /// <returns></returns>
        public ActionResult ShowResultSearchForLogin(string login)
        {
            if(login == "" || login == null) // если не путстое
            {
                return PartialView("ShowResultSearchForLogin", null);
            }

            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) // подключение а бд
            {
                var userInfo = db.UserInfo.Include(u => u.Logins)
                    .Where(x=>x.Logins.LoginUser==login); // записываем в переменную если совпадает по логину
                return PartialView("ShowResultSearchForLogin", userInfo.ToList()); // в виде списка передаем во вью
            }
        }

        /// <summary>
        /// Ищет по разным параметрам
        /// </summary>
        /// <param name="name">Имя </param>
        /// <param name="lastname">Фамилия</param>
        /// <param name="mixage">мн возраст</param>
        /// <param name="maxage">макс возраст</param>
        /// <param name="email">почта </param>
        /// <param name="adress">адресс</param>
        /// <returns></returns>
        public ActionResult ShowResultSearch(string name, string lastname , int? minage, int? maxage, string email, string adress)
        {           
            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) // подключение а бд
            {
                if (minage < 0) minage = 0; //  если возраст пришел меньше нуля
                if (maxage < 0) maxage = 0;
                if (maxage < minage) // если возраст мах < min
                {                    
                    int? tmp = maxage; // меняем местанм
                    maxage = minage;
                    minage = tmp;
                }

                var userInfo = db.UserInfo // выбрать всех где
                    .Where(x => (x.Firstname == name) || // совпадает имя
                    (x.Lastname == lastname) || // или фамилия 
                    (x.Age >= minage && x.Age <= maxage) || // или возоатс
                    (x.Email ==  email) || // почта
                    (x.Adress == adress)); // адресс

                return PartialView("ShowResultSearchForLogin", userInfo.ToList()); // в виде списка передаем во вью
            }
        }
        

        /// <summary>
        /// Показывает информацию о пользовтале
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null) //  если такого пользователя нет
            {              
                return Redirect("~/home/Error");//то плохо
            }

            UserInfo userInfo; // переменная для информации о пользователя

            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) // подключение
            {
                userInfo = db.UserInfo.Find(id); // находим по ID

                if (userInfo == null) //  если пустой
                {
                    return Redirect("~/home/Error");//то плохо
                }                

                // временнеая переменная для хранения информации по пользовтаеля кто ищет
                UserInfo tmp = db.UserInfo.FirstOrDefault(x=>x.Logins.LoginUser == User.Identity.Name);


                if (tmp.ID == id) // если открываем страницу нв себя
                {
                    return RedirectToAction("Details", "MyPage"); // перенаправляем 
                }

                ViewBag.Friend = Friend(id);// является ли пользователь другом переити на методы
                ViewBag.MyReqFrnd = MyReqFriend(id); // есть ли запрос от потзователя
                ViewBag.ReqFrnd = ReqFriend(id); // есть ли запрос от другого потзователя            

            }                      
            return View(userInfo); 
        }


        /// <summary>
        /// метод проверку являкется ли пользователь другом
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Friend(int? id)
        {
            int? userID; // пользовталея 
            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) //создает подключение пользователя
            {
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                userID = tmpLogin.ID; // передеем ID    

                var tmplistFriend = db.UserListFriend; // записывем список друзей

                return tmplistFriend.Any(x => x.IdFriend == id && x.UserFrinds == userID); // если друг есть в друзьях у пошльзователя

                //    foreach (var item in tmplistFriend) // пробегаем по нему 
                //    {
                //        if(item.UserFriends.IdUser == userID && item.IdFriend == id){ // если друг есть в друзьях у пошльзователя
                //            return true; // вернем тру
                //        }
                //    }
                //}         
                //return false; // вернем ефолс если не будет совпадений
            }
        }


        /// <summary>
        /// Проверяет есть ли запрос на дружбу
        /// </summary>
        /// <param name="id"> ID пользователя</param>
        /// <returns></returns>
        public bool MyReqFriend(int? id)
        {
            int? userID;
            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) //создает подключение пользователя
            {
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                userID = tmpLogin.ID; // передеем ID    

                var tmpReqFriend = db.FriendRequest; // записывем список запросов в друзья

                return tmpReqFriend.Any(x => x.UserID == userID && x.FriendID == id);// если друг есть в запросах у пошльзователя

                //    foreach (var item in tmpReqFriend) // пробегаем по нему 
                //    {
                //        if (item.UserID == userID && item.FriendID == id)
                //        { // если друг есть в запросах у пошльзователя
                //            return true; // вернем тру
                //        }
                //    }
                //}
                //return false; // вернем ефолс если не будет совпадений
            }
        }


        /// <summary>
        /// Проверяет есть ли запрос на дружбу
        /// </summary>
        /// <param name="id"> ID пользователя</param>
        /// <returns></returns>
        public bool ReqFriend(int? id)
        {
            int? userID;
            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) //создает подключение пользователя
            {
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                userID = tmpLogin.ID; // передеем ID    

                var tmpReqFriend = db.FriendRequest; // записывем список 

                return tmpReqFriend.Any(x => x.UserID == id && x.FriendID == userID);

                //    foreach (var item in tmpReqFriend) // пробегаем по нему 
                //    {
                //        if (item.UserID == id && item.FriendID == userID) //разница меджу ними только здесь
                //        { // если друг есть в запросах у пошльзователя
                //            return true; // вернем тру
                //        }
                //    }
                //}
                //return false; // вернем фолс если не будет совпадений
            }
        }


    }
}
