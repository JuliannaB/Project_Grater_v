using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Grater.Models;

namespace Grater.Controllers.API
{
    public class TherapistsController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

     /*   public TherapistsController()
        {
            _context = new ApplicationDbContext();
        }   */

        //Get / api/therapist
        public IEnumerable<Therapist> GetTherapists()
        {
            return _context.Therapists.ToList();
        }

        //Get/api/therapist/1
        public Therapist GetTherapists(int id)
        {
            var therapist = _context.Therapists.SingleOrDefault(c => c.TherapistId == id);

            if (therapist == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return therapist;
        }

        // POST /api/therapist
        [HttpPost]
        public Therapist CreateTherapist(Therapist therapist)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Therapists.Add(therapist);
            _context.SaveChanges();

            
            return therapist;
        }

        //PUT /api/therapist/1
        public void UpdateTherapist(int id, Therapist therapist)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var therapistInDb = _context.Therapists.SingleOrDefault(c => c.TherapistId == id);
            if (therapistInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            therapistInDb.TherapistName = therapist.TherapistName;
            therapistInDb.City = therapist.City;
          //  therapistInDb.Description = therapist.Description;

            _context.SaveChanges();

        }

        //DELETE /api/
        [HttpDelete]
        public void DeleteTherapist(int id)
        {
            var therapistInDb = _context.Therapists.SingleOrDefault(c => c.TherapistId == id);
            if (therapistInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Therapists.Remove(therapistInDb);
            _context.SaveChanges();

        }
    }
}
