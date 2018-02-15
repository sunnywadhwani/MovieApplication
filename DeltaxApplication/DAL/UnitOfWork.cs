using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeltaxApplication.Models;
using DeltaxApplication.Repository;

namespace DeltaxApplication.DAL
{
    public class UnitOfWork:IDisposable
    {
        private MovieDbContext context=new MovieDbContext();

        private MovieRepository _movieRepository;
        private ActorRepository _actorRepository;
        private ProducerRepository _producerRepository;


        public MovieRepository MovieRepository
        {
            get
            {
                return this._movieRepository ?? new MovieRepository(context);

            }

        }

        public ActorRepository ActorRepository
        {
            get
            {
                return this._actorRepository ?? new ActorRepository(context);

            }

        }
        public ProducerRepository ProducerRepository
        {
            get
            {
                return this._producerRepository ?? new ProducerRepository(context);

            }

        }

        public void Save()
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