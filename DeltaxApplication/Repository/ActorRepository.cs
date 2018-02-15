using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DeltaxApplication.Interfaces;
using DeltaxApplication.Models;

namespace DeltaxApplication.Repository
{
    public class ActorRepository :IActorRepository,IDisposable
    {


        private MovieDbContext context;

        public ActorRepository(MovieDbContext context)
        {
            this.context = context;

        }
        public Actor GetActor(int? id)
        {
            return context.Actors.Find(id);
        }
        public List<Actor> GetActors()
        {
            return context.Actors.ToList();
        }

        public List<Actor> GetActors(List<int> ids)
        {
            return context.Actors.Where(a => ids.Contains(a.Id)).ToList();
        }

        public void AddActor(Actor actor)
        {
            context.Actors.Add(actor);
        }

        public void EditActor(Actor actor)
        {
            context.Entry(actor).State = EntityState.Modified;
        }


        public void DeleteActor(Actor actor)
        {
            context.Actors.Remove(actor);
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