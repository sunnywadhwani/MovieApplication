using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Data.Entity.Validation;
using DeltaxApplication.Models;
using Microsoft.Ajax.Utilities;

namespace DeltaxApplication.Controllers
{
    public class ProducerController : Controller
    {
        // GET: Producer
        private MovieEntitie _context;

        public ProducerController()
        {
            _context = new MovieEntitie();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Producer x = _context.Producers.Find(id);
            return View(x);

        }

        [HttpPost]
        public JsonResult Save(Producer producer)
        {
           
               
            _context.Producers.Add(producer);
            _context.SaveChanges();
          


            return Json(new
            {
                producer
            });
        }


    }
}