﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public class Salon
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string SalonName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Therapist> Therapists { get; set; }

    }
}