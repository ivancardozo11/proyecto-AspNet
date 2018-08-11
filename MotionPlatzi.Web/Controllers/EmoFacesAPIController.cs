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
    public class EmoFacesAPIController : ApiController
    {
        private MotionPlatziWebContext db = new MotionPlatziWebContext();

        // GET: api/EmoFacesAPI
        public IQueryable<EmoFace> GetEmoFace()
        {
            return db.EmoFace;
        }

        // GET: api/EmoFacesAPI/5
        [ResponseType(typeof(EmoFace))]
        public async Task<IHttpActionResult> GetEmoFace(int id)
        {
            EmoFace emoFace = await db.EmoFace.FindAsync(id);
            if (emoFace == null)
            {
                return NotFound();
            }

            return Ok(emoFace);
        }

        // PUT: api/EmoFacesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmoFace(int id, EmoFace emoFace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emoFace.Id)
            {
                return BadRequest();
            }

            db.Entry(emoFace).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmoFaceExists(id))
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

        // POST: api/EmoFacesAPI
        [ResponseType(typeof(EmoFace))]
        public async Task<IHttpActionResult> PostEmoFace(EmoFace emoFace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmoFace.Add(emoFace);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = emoFace.Id }, emoFace);
        }

        // DELETE: api/EmoFacesAPI/5
        [ResponseType(typeof(EmoFace))]
        public async Task<IHttpActionResult> DeleteEmoFace(int id)
        {
            EmoFace emoFace = await db.EmoFace.FindAsync(id);
            if (emoFace == null)
            {
                return NotFound();
            }

            db.EmoFace.Remove(emoFace);
            await db.SaveChangesAsync();

            return Ok(emoFace);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmoFaceExists(int id)
        {
            return db.EmoFace.Count(e => e.Id == id) > 0;
        }
    }
}