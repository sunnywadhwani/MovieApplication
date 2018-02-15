using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeltaxApplication.Models;
namespace DeltaxApplication.Interfaces
{
    public interface IMovieRepository:IDisposable
    {


        Movie GetMovie(int? id);
        void AddMovie(Movie movie);
        List<Movie> GetMovies();

        void EditMovie(Movie movie);
        void DeleteMovie(Movie movie);


    }
}