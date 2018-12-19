using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SocialNetWorkv1._0.Models
{
    /// <summary>
    ////Класс описывает лоди и выдачу ролей
    /// </summary>
    public class MyRoleProvider : RoleProvider
    {
        /// <summary>
        /// Создание ролей
        /// </summary>
        /// <returns> Массиив ролей</returns>
        public override string[] GetAllRoles()
        {
            string[] arrayRoles = new string[2]; // массив для хранения всех роей
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                //  arrayRoles = db.Roles.Count();
                for (int i = 1; i <= db.Roles.Count(); i++) // перебираем таблицу с базы со спиком ролей
                {
                    Roles tmp = db.Roles.FirstOrDefault(x => x.ID == i); // записываем роль во временую по ID
                    if (tmp != null) 
                    {
                        arrayRoles[i -  1] = tmp.NameRole;//ЗАПИСЫВАЕМ название роли в массив строк
                    }
                }     
            }
            return arrayRoles;
        }

        /// <summary>
        /// Метод раздачи ролей
        /// </summary>
        /// <param name="username">Логин пользователя</param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { }; // создаем масств для записи ролей

            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                // Получаем пользователя
                Logins user = db.Logins.FirstOrDefault(x => x.LoginUser == username); // получаем пользователя из бд
                if (user != null)
                {
                    // получаем роль
                    roles = new string[] { user.Roles.NameRole };// записываем еиу роль
                }
                return roles; // возращаем роли
            }

        }
        

        #region Not implemenled
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}