using Grater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grater.ViewModels
{
    public class TherapistIndexData
    {
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Therapist> Therapists { get; set; }

        public IEnumerable<Salon>Salons { get; set; }
       
    }
}