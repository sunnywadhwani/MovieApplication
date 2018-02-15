using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeltaxApplication.Interfaces;
using DeltaxApplication.Models;
using System.Data.Entity;

namespace DeltaxApplication.Repository
{
    public class MovieRepository :IMovieRepository,IDisposable
    {
        private MovieDbContext context;

        public MovieRepository(MovieDbContext context)
        {
            this.context = context;
        }
        public Movie GetMovie(int? id)
        {
            return context.Movies.Include(m=>m.Actors).SingleOrDefault(m=>m.Id==id);
        }

        public void AddMovie(Movie movie)
        {
            context.Movies.Attach(movie);
            context.Movies.Add(movie);
        }
        public List<Movie> GetMovies()
        {
            return context.Movies.Include(m => m.Producer).Include(m => m.Actors).ToList();
        }

        public void DeleteMovie(Movie movie)
        {
            if (movie == null)
                throw new Exception();

            else
            {
                context.Movies.Remove(movie);
            }

            
        }

        public void EditMovie(Movie movie)
        {
            context.Entry(movie).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



    }
}