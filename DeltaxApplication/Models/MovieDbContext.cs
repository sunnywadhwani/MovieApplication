using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using DeltaxApplication.Models;

namespace DeltaxApplication.Models
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
            : base("name=MovieDbContext")
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
    }
}