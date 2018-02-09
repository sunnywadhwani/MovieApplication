using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Validation;
using DeltaxApplication.ViewModel;
using System.Web;
using System.Web.Mvc;
using DeltaxApplication.Models;

using Microsoft.Ajax.Utilities;

namespace deltaxapp.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        private MovieEntitie _context;

        public MovieController()
        {
            _context = new MovieEntitie();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()


        {
            var films = _context.Movies.Include(c => c.Producer).Include(f => f.Actors).ToList();
            return View(films);
        }
        /* 
       public ActionResult AddImage()
       {
           Movie m = new Movie();
           return View(m);
       }

       [HttpPost]
         public ActionResult AddImage(Movie mov, HttpPostedFileBase image1)
         {
             if (image1 != null)
             {
                 mov.Poster=new byte[image1.ContentLength];
                 image1.InputStream.Read(mov.Poster, 0, image1.ContentLength);
             }

             _context.Movies.Add(mov);
             _context.SaveChanges();
             return View(mov);

         }
 */
        public ActionResult New()
        {
            var producers = _context.Producers.ToList();
            var actors = _context.Actors.ToList();
            var viewModel = new NewMovieViewModel
            {
                Producers = producers,
                Actors= actors
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Movie movies, HttpPostedFileBase image1)
        {
            HashSet<Actor> selectedActors = new HashSet<Actor>();
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    movies.Poster = new byte[image1.ContentLength];
                    image1.InputStream.Read(movies.Poster, 0, image1.ContentLength);
                }







                if (movies.Id == 0)
                {

                    foreach (var x in movies.SelectedActorIds)
                    {
                        selectedActors.Add(_context.Actors.SingleOrDefault(i=>i.Id==x));
                    }

                    movies.Actors = selectedActors;
                    _context.Movies.Add(movies);
                    _context.SaveChanges();

                    TempData["notice"] = movies.Name + " Added Successfully ";

                }
                else
                {
                    var movindb = _context.Movies.Include(c => c.Actors).SingleOrDefault(c => c.Id == movies.Id);

                   var existingactorids= movindb.Actors.Select(actor=>actor.Id).ToList();
                    var newactorids = movies.SelectedActorIds.Except(existingactorids);
                    var deletedactorids = existingactorids.Except(movies.SelectedActorIds);
                    
                 //   foreach (var x in movies.SelectedActorIds)
                   // {
                      //  selectedActors.Add(_context.Actors.SingleOrDefault(i=>i.Id==x));
                    //}

                   // movies.Actors = selectedActors;

                    foreach (var x in newactorids)
                    {
                        var actor = _context.Actors.SingleOrDefault(a => a.Id == x);
                        movindb.Actors.Add(actor);


                    }
                    foreach (var x in deletedactorids)
                    {
                        var actor = _context.Actors.SingleOrDefault(a => a.Id == x);
                        movindb.Actors.Remove(actor);


                    }




                    movindb.Name = movies.Name;
                    movindb.Plot = movies.Plot;
                    movindb.Id = movies.Id;
                    movindb.Year_Of_Release = movies.Year_Of_Release;
                   
                    movindb.Poster = movies.Poster;


                    _context.SaveChanges();
                }
                TempData["notice"] = movies.Name + " Edited Successfully ";

                return RedirectToAction("Index", "Movie");
            }
            else
            {
                var producers = _context.Producers.ToList();
                var actors = _context.Actors.ToList();
                var viewModel = new NewMovieViewModel
                {
                    Producers = producers,
                    Actors= actors,
                    Movies = movies

                };

                return View("New", viewModel);
            }
        }


        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return HttpNotFound();

            }

            movie.SelectedActorIds = movie.Actors.Select(actor => actor.Id).ToList();
            var viewModel = new NewMovieViewModel()
            {

                Producers = _context.Producers.ToList(),
                Actors = _context.Actors.ToList(),
                Movies = movie

            };






            return View("New", viewModel);

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie mov = _context.Movies.Find(id);
            if (mov == null)
            {
                return HttpNotFound();
            }
            _context.Movies.Remove(mov);
            _context.SaveChanges();

            TempData["notice"] =mov.Name+ " Deleted Successfully ";


            return RedirectToAction("Index", "Movie");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Movie film = _context.Movies.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }

            if (film.Poster != null)
            {
                ViewBag.Image = Convert.ToBase64String(film.Poster);

            }
            else if (film.Poster == null)
            {
                ViewBag.Image = null;
            }

            return View(film);


        }





    }
}
