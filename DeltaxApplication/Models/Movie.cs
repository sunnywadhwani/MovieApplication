using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace DeltaxApplication.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Actors = new HashSet<Actor>();
            this.SelectedActorIds = new List<int>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Movie Name Cannot be empty")]
        public string Name { get; set; }
        public string Plot { get; set; }
        [Required(ErrorMessage = "Year Of Release Cannot be null")]
        public int Year_Of_Release { get; set; }

        public Nullable<int> ProducerId { get; set; }
        public byte[] Poster { get; set; }
        public virtual Producer Producer { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
        [EnsureOneElement(ErrorMessage = "Atleast One Actor Needed To Be Selected")]
        [NotMapped] public List<int> SelectedActorIds { get; set; }
    }

   
}