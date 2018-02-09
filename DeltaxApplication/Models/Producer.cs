using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaxApplication.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Bio { get; set; }



    }



}