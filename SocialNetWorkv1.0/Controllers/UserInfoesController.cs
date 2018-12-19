using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SocialNetWorkv1._0.Models;

namespace SocialNetWorkv1._0.Controllers
{
    public class UserInfoesController : ApiController
    {    

        // GET: api/UserInfoes
        public IQueryable<UserInfo> GetUserInfo()
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                return db.UserInfo;
            }
        }

        // GET: api/UserInfoes/5
        [ResponseType(typeof(UserInfo))]
        public IHttpActionResult GetUserInfo(int id)
        {
            using (Soc_NetWorkCF db = new Soc_NetWorkCF())
            {
                UserInfo userInfo = db.UserInfo.Find(id);
                if (userInfo == null)
                {
                    return NotFound();
                }

                return Ok(userInfo);
            }
        }  
    }
}