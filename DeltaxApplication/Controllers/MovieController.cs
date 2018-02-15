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
using DeltaxApplication.Repository;
using Microsoft.Ajax.Utilities;

namespace deltaxapp.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        private MovieRepository _movieRepository;
        private ActorRepository _actorRepository;
        private ProducerRepository _producerRepository;

        public MovieController()
        {
            var context = new MovieDbContext();
            this._movieRepository = new MovieRepository(context);
            this._producerRepository=new ProducerRepository(context);
            this._actorRepository=new ActorRepository(context);

        }
        protected override void Dispose(bool disposing)
        {
            _movieRepository.Dispose();
        }

        public ActionResult Index()

        {
            var movies = _movieRepository.GetMovies();
            return View(movies);
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
            var producers = _producerRepository.GetProducers();
            var actors = _actorRepository.GetActors();
            var viewModel = new NewMovieViewModel
            {
                Producers = producers,
                Actors= actors
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Movie movie, HttpPostedFileBase image1)
        {
            HashSet<Actor> selectedActors = new HashSet<Actor>();
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    movie.Poster = new byte[image1.ContentLength];
                    image1.InputStream.Read(movie.Poster, 0, image1.ContentLength);
                }







                if (movie.Id == 0)
                {

                    foreach (var x in movie.SelectedActorIds)
                    {
                      //  selectedActors.Add(_context.Actors.SingleOrDefault(i=>i.Id==x));
                        selectedActors.Add(_actorRepository.GetActor(x));
                    }

                    movie.Actors = selectedActors;
                    //   _context.Movies.Add(movies);
                    _movieRepository.AddMovie(movie);
                   
                   // _context.SaveChanges();
                    _movieRepository.SaveChanges();

                    TempData["notice"] = movie.Name + " Added Successfully ";

                }
                else
                {
                   
                    //var movindb = _context.Movies.Include(c => c.Actors).SingleOrDefault(c => c.Id == movies.Id);
                    var movindb = _movieRepository.GetMovie(movie.Id);
                   var existingactorids= movindb.Actors.Select(actor=>actor.Id).ToList();
                    var newactorids = movie.SelectedActorIds.Except(existingactorids);
                    var deletedactorids = existingactorids.Except(movie.SelectedActorIds);
                    
                 //   foreach (var x in movies.SelectedActorIds)
                   // {
                      //  selectedActors.Add(_context.Actors.SingleOrDefault(i=>i.Id==x));
                    //}

                   // movies.Actors = selectedActors;

                    foreach (var x in newactorids)
                    {
                       // var actor = _context.Actors.SingleOrDefault(a => a.Id == x);
                        var actor = _actorRepository.GetActor(x);
                        movindb.Actors.Add(actor);


                    }
                    foreach (var x in deletedactorids)
                    {
                      //  var actor = _context.Actors.SingleOrDefault(a => a.Id == x);
                        var actor = _actorRepository.GetActor(x);
                        movindb.Actors.Remove(actor);


                    }




                    movindb.Name = movie.Name;
                    movindb.Plot = movie.Plot;
                    movindb.Id = movie.Id;
                    movindb.Year_Of_Release = movie.Year_Of_Release;
                   
                    movindb.Poster = movie.Poster;
                    movindb.SelectedActorIds = movie.SelectedActorIds;

                    _movieRepository.SaveChanges();
                    TempData["notice"] = movie.Name + " Edited Successfully ";
                }

                return RedirectToAction("Index", "Movie");
            }
            else
            {
                var producers = _producerRepository.GetProducers();
                var actors = _actorRepository.GetActors();
                var viewModel = new NewMovieViewModel
                {
                    Producers = producers,
                    Actors= actors,
                    Movie = movie

                };

                return View("New", viewModel);
            }
        }


        public ActionResult Edit(int id)
        {
            //var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            var movie = _movieRepository.GetMovie(id);
            if (movie == null)
            {
                return HttpNotFound();

            }

            movie.SelectedActorIds = movie.Actors.Select(actor => actor.Id).ToList();
            var viewModel = new NewMovieViewModel()
            {

                Producers = _producerRepository.GetProducers(),
                Actors =_actorRepository.GetActors(),
                Movie = movie

            };


            if (movie.Poster != null)
            {
                ViewBag.Image = Convert.ToBase64String(movie.Poster);

            }



            return View("New", viewModel);

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie mov = _movieRepository.GetMovie(id);
            if (mov == null)
            {
                return HttpNotFound();
            }
           _movieRepository.DeleteMovie(mov);
            _movieRepository.SaveChanges();

            TempData["notice"] =mov.Name+ " Deleted Successfully ";


            return RedirectToAction("Index", "Movie");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Movie movie = _movieRepository.GetMovie(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            if (movie.Poster != null)
            {
                ViewBag.Image = Convert.ToBase64String(movie.Poster);

            }
            else if (movie.Poster == null)
            {
                ViewBag.Image = null;
            }

            return View(movie);


        }





    }
}
