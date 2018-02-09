using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeltaxApplication.Models;

namespace DeltaxApplication.ViewModel
{
    public class NewMovieViewModel
    {
        public IEnumerable<Producer> Producers { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public Movie Movies { get; set; }
    }
}