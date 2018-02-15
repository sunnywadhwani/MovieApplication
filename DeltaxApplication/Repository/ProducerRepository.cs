using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeltaxApplication.Interfaces;
using DeltaxApplication.Models;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace DeltaxApplication.Repository
{
    public class ProducerRepository : IProducerRepository, IDisposable
    {
        private MovieDbContext context;

       

        public ProducerRepository(MovieDbContext context)
        {
            this.context= context;
        }

        public Producer GetProducer(int? id)
        {
            return context.Producers.Find(id);
        }



        public List<Producer> GetProducers()
        {

            return context.Producers.ToList();

        }

        public void AddProducer(Producer producer)
        {
            context.Producers.Add(producer);
        }

        public void EditProducer(Producer producer)
        {
            context.Entry(producer).State = EntityState.Modified;

        }

        public void DeleteProducer(Producer producer)
        {
            context.Producers.Remove(producer);
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