using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeltaxApplication.Models;
using DeltaxApplication.Repository;

namespace DeltaxApplication.Controllers
{
    public class ActorController : Controller
    {
        // GET: Actor
        private ActorRepository _actorRepository;

        public ActorController()
        {
            this._actorRepository = new ActorRepository(new MovieDbContext());

        }
        protected override void Dispose(bool disposing)
        {
            _actorRepository.Dispose();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Actor x = _actorRepository.GetActor(id);
            return View(x);
        }


        [HttpPost]
        public JsonResult Save(Actor actor)
        {

            _actorRepository.AddActor(actor);
           _actorRepository.SaveChanges();
            return Json(new
            {
                actor
            });
        }



    }
}