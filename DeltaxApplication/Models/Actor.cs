using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaxApplication.Models
{



    
        public class Actor
        {

            public int Id { get; set; }
            public string Name { get; set; }
            public string Sex { get; set; }
            public DateTime? DOB { get; set; }
            public string Bio { get; set; }
            public ICollection<Movie> Movies { get; set; }

            public Actor()
            {
                this.Movies = new HashSet<Movie>();
            }
        }
    






}