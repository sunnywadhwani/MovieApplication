using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeltaxApplication.Models;

namespace DeltaxApplication.Controllers
{
    public class ActorController : Controller
    {
        // GET: Actor
        private MovieEntitie _context;

        public ActorController()
        {
            _context = new MovieEntitie();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Actor x = _context.Actors.Find(id);
            return View(x);
        }


        [HttpPost]
        public JsonResult Save(Actor actor)
        {

            _context.Actors.Add(actor);
            _context.SaveChanges();
            return Json(new
            {
                actor
            });
        }



    }
}