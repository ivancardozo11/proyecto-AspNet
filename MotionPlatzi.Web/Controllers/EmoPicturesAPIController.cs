using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MotionPlatzi.Web.Models;

namespace MotionPlatzi.Web.Controllers
{
    public class EmoPicturesAPIController : ApiController
    {
        private MotionPlatziWebContext db = new MotionPlatziWebContext();

        // GET: api/EmoPicturesAPI
        public IQueryable<EmoPicture> GetEmoPictures()
        {
            return db.EmoPictures;
        }

        // GET: api/EmoPicturesAPI/5
        [ResponseType(typeof(EmoPicture))]
        public async Task<IHttpActionResult> GetEmoPicture(int id)
        {
            EmoPicture emoPicture = await db.EmoPictures.FindAsync(id);
            if (emoPicture == null)
            {
                return NotFound();
            }

            return Ok(emoPicture);
        }

        // PUT: api/EmoPicturesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmoPicture(int id, EmoPicture emoPicture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emoPicture.Id)
            {
                return BadRequest();
            }

            db.Entry(emoPicture).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmoPictureExists(id))
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

        // POST: api/EmoPicturesAPI
        [ResponseType(typeof(EmoPicture))]
        public async Task<IHttpActionResult> PostEmoPicture(EmoPicture emoPicture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmoPictures.Add(emoPicture);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = emoPicture.Id }, emoPicture);
        }

        // DELETE: api/EmoPicturesAPI/5
        [ResponseType(typeof(EmoPicture))]
        public async Task<IHttpActionResult> DeleteEmoPicture(int id)
        {
            EmoPicture emoPicture = await db.EmoPictures.FindAsync(id);
            if (emoPicture == null)
            {
                return NotFound();
            }

            db.EmoPictures.Remove(emoPicture);
            await db.SaveChangesAsync();

            return Ok(emoPicture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmoPictureExists(int id)
        {
            return db.EmoPictures.Count(e => e.Id == id) > 0;
        }
    }
}