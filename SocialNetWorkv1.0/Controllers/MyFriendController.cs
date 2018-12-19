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

namespace SocialNetWorkv1._0.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class MyFriendController : Controller
    {
        
        /// <summary>
        /// Выводит список друзхей пользователя
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) //создает подключение пользователя
            {   
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                int? userID = tmpLogin.ID; // передеем ID

                // получаем список всех пользовтелей из списка друзей пользователя по ID
                var userListFriend = db.UserListFriend.Include(u => u.Logins).Include(u => u.UserFriends)
                     .Where(x => x.UserFrinds == userID);

                List<UserInfo> ListInfoFriend = new List<UserInfo>(); // новый список для передачи во вью

                var userInfo = db.UserInfo; // получаем всех пользователей из базы

                foreach (var item in userListFriend)
                {
                    ListInfoFriend.Add(userInfo.First(x=>x.ID==item.IdFriend)); //добавляем в список                      
                }
                
                ViewBag.ReqFrnd = RequestFriend(userID); // передаем во вью спиоск запросов на дружбу
                ViewBag.MyReqFrnd = MyRequestFriend(userID); // передаем во вью запросов пользоваптелдя на дружбу
                return View(ListInfoFriend);// возращаем список
            }    
        }

        /// <summary>
        /// Выводит список запросов в друзья у пользовтаеля
        /// </summary>
        /// <param name="ID">I пользовтеля</param>
        /// <returns></returns>
        public List<UserInfo> RequestFriend(int? ID)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                // получаем список всех пользовтелей из списка друзей пользователя по ID
                var userListFriend = db.FriendRequest.Where(x => x.FriendID == ID);

                var userInfo = db.UserInfo; // получаем всех пользователей из базы

                List<UserInfo> ListInfoFriend = new List<UserInfo>(); // новый список для передачи во вью

                foreach (var item in userListFriend)
                {
                    ListInfoFriend.Add(userInfo.First(x=>x.ID==item.UserID)); //добавляем список 
                }      

                return ListInfoFriend;
            }
        }


        /// <summary>
        /// Выводит список запросов у пользывотеля на дружбу
        /// </summary>
        /// <param name="ID">IВ пользовтеля</param>
        /// <returns></returns>
        public List<UserInfo> MyRequestFriend(int? ID)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {

                // получаем список всех пользовтелей из списка друзей пользователя по ID
                var userListFriend = db.FriendRequest.Where(x => x.UserID == ID);

                var userInfo = db.UserInfo; // получаем всех пользователей из базы

                List<UserInfo> ListInfoFriend = new List<UserInfo>(); // новый список для передачи во вью

                foreach (var item in userListFriend) // передераем всех пользовталей
                {
                    ListInfoFriend.Add(userInfo.First(x => x.ID == item.FriendID)); //добавляем список                                             
                }
             

                return ListInfoFriend;
            }
        }

        /// <summary>
        /// Gthtyfghfdkztb на страницу пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {  
          return RedirectToAction("Details","Search", new { @id = id });
        }


        /// <summary>
        /// Запрос в друзья
        /// </summary>
        /// <param name="id"> ID друга </param>
        /// <returns></returns>
        public ActionResult AddReqFriend(int? id)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                int? userID;
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                userID = tmpLogin.ID; // передеем ID

               //var tmp = db.FriendRequest.Where(x => x.FriendID == id && x.UserID == userID);

                using (Soc_NetWorkEntitiesProc proc = new Soc_NetWorkEntitiesProc())
                {
                  proc.AddFriendRequest(userID, id);// вызываем процекдуры из бд добавление нового друга   
                }

                return RedirectToAction("Details", new { @id = id });// возращаем список
            }
        }

        /// <summary>
        /// Отменяет запрос на дружбу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CanselReqFriend(int? id)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                int? userID;
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                userID = tmpLogin.ID; // передеем ID     

                using (Soc_NetWorkEntitiesProc proc = new Soc_NetWorkEntitiesProc())
                {
                    proc.DeleteFriendRequest(id, userID);// вызываем процекдуры из бд добавление нового друга
                }

                return RedirectToAction("Details", new { @id = id });// возращаем список
            }
        }
       
        /// <summary>
        /// Добавление в друзья 
        /// </summary>
        /// <param name="id">ID будущего друга</param>
        /// <returns></returns>
        public ActionResult AddFriend(int? id)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                int? userID;
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                userID = tmpLogin.ID; // передеем ID     

                using (Soc_NetWorkEntitiesProc proc = new Soc_NetWorkEntitiesProc())
                {
                    proc.AddNewFriend(userID, id);// вызываем процекдуры из бд добавление нового друга
                    proc.DeleteFriendRequest(userID, id);// вызываем процекдуры из бд добавление нового друга
                }

                return RedirectToAction("Details", new { @id = id });// возращаем список
            }
        }
        
        /// <summary>
        /// Удаляет друга 
        /// </summary>
        /// <param name="id">ID друга</param>
        /// <returns></returns>
        public ActionResult DeleteFriend(int? id)
        {
            int? userID;
            using (Soc_NetWorkCF db = new Soc_NetWorkCF()) //создает подключение пользователя
            {
                Logins tmpLogin = db.Logins.FirstOrDefault(x => x.LoginUser == User.Identity.Name); //находим по id польховтаеля
                userID = tmpLogin.ID; // передеем ID       
            }

            using(Soc_NetWorkEntitiesProc db = new Soc_NetWorkEntitiesProc())
            {
                db.DeleteListFriens(userID, id);
            }

            return RedirectToAction("Details", new { @id = id });// возращаем список            
        }  


    }
}
