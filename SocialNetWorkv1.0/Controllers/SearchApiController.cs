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
    public class SearchApiController : ApiController
    {
        private Soc_NetWorkCF db = new Soc_NetWorkCF();

        // GET: api/SearchApi
        public IQueryable<UserInfo> GetUserInfo()
        {
            return db.UserInfo;
        }

        // GET: api/SearchApi/5
        [ResponseType(typeof(UserInfo))]
        public IHttpActionResult GetUserInfo(int id)
        {
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                //return NotFound();
                return Redirect("~/home/Error");
            }

            return Ok(userInfo);
        }
    }
      
}