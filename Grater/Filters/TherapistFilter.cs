using Grater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grater.Filters
{
    public class TherapistFilter
    {
        public string TherapistName { get; set; }

        public string TherapistSurname { get; set; }

        public string City { get; set; }

    }


    public class TherapisFilterLogic
    {
        private ApplicationDbContext _context;
        public TherapisFilterLogic()
        {
            _context = new ApplicationDbContext();
        }

        public IQueryable<Therapist> GetTherapists(TherapistFilter searchModel)
        {
            var result = _context.Therapists.AsQueryable();
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.TherapistName))
                    result = result.Where(x => x.TherapistName.Contains(searchModel.TherapistName));
                if (!string.IsNullOrEmpty(searchModel.TherapistSurname))
                    result = result.Where(x => x.TherapistSurname.Contains(searchModel.TherapistSurname));
                if (!string.IsNullOrEmpty(searchModel.City))
                    result = result.Where(x => x.City.Contains(searchModel.City));
            }
            return result;
        }
    }
}
