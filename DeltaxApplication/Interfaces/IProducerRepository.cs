using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeltaxApplication.Models;

namespace DeltaxApplication.Interfaces
{
    public interface IProducerRepository:IDisposable
    {

        Producer GetProducer(int? id);
        List<Producer> GetProducers();
        void AddProducer(Producer producer);
        void EditProducer(Producer producer);
        void DeleteProducer(Producer producer);


    }
}