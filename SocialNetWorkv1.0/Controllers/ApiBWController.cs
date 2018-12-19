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
    [Authorize(Roles = "Admin")]
    public class ApiBWController : ApiController
    {
        private Soc_NetWorkCF db = new Soc_NetWorkCF();

        // GET: api/ApiBW
        public IQueryable<BadWord> GetBadWord()
        {
            return db.BadWord;
        }


        // GET: api/ApiBW/5
        [ResponseType(typeof(BadWord))]
        public IHttpActionResult GetBadWord(int id)
        {
            BadWord badWord = db.BadWord.Find(id);
            if (badWord == null)
            {
                return NotFound();
            }

            return Ok(badWord);
        }

        // GET: api/ApiBW/5
        [ResponseType(typeof(BadWord))]
        public IHttpActionResult GetBadWord(string word)
        {
            BadWord badWord = db.BadWord.Find(word);
            if (badWord == null)
            {
                return NotFound();
            }

            return Ok(badWord);
        }



        // PUT: api/ApiBW/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBadWord(int id, BadWord badWord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != badWord.ID)
            {
                return BadRequest();
            }

            db.Entry(badWord).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BadWordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ApiBW
        [ResponseType(typeof(BadWord))]
        public IHttpActionResult PostBadWord(BadWord badWord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BadWord.Add(badWord);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = badWord.ID }, badWord);
        }

        // DELETE: api/ApiBW/5
        [ResponseType(typeof(BadWord))]
        public IHttpActionResult DeleteBadWord(int id)
        {
            BadWord badWord = db.BadWord.Find(id);
            if (badWord == null)
            {
                return NotFound();
            }

            db.BadWord.Remove(badWord);
            db.SaveChanges();

            return Ok(badWord);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BadWordExists(int id)
        {
            return db.BadWord.Count(e => e.ID == id) > 0;
        }
    }
}