using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Data.Entity.Validation;
using DeltaxApplication.Interfaces;
using DeltaxApplication.Models;
using DeltaxApplication.Repository;
using Microsoft.Ajax.Utilities;

namespace DeltaxApplication.Controllers
{
    public class ProducerController : Controller
    {
        // GET: Producer
        private ProducerRepository _producerRepository;

        public ProducerController()
        {
            this._producerRepository = new ProducerRepository(new MovieDbContext());

        }
        protected override void Dispose(bool disposing)
        {
            _producerRepository.Dispose();
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

            Producer x = _producerRepository.GetProducer(id);
            return View(x);

        }

        [HttpPost]
        public JsonResult Save(Producer producer)
        {
           
              _producerRepository.AddProducer(producer);
            _producerRepository.SaveChanges();




            return Json(new
            {
                producer
            });
        }


    }
}