using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeltaxApplication.Models;

namespace DeltaxApplication.Interfaces
{
    public interface IActorRepository:IDisposable
    {
        Actor GetActor(int? id);
        List<Actor> GetActors();
        List<Actor> GetActors(List<int> ids);
        void AddActor(Actor actor);

        void EditActor(Actor actor);

        void DeleteActor(Actor actor);


    }
}