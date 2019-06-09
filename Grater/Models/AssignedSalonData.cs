using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public class AssignedSalonData
    {
        public int Id { get; set; }
        public string SalonName { get; set; }
        public bool Assigned { get; set; }
    }
}