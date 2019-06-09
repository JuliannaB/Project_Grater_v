using Grater.Models;
using Grater.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Grater.Controllers
{
    public class SalonController : Controller
    {

        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Salon
        public ActionResult Index(int? id, int? therapistId)
        {
            var viewModel = new TherapistIndexData();

            viewModel.Salons = _context.Salons
                    .Include(i => i.Therapists)
                .OrderBy(i => i.SalonName);


            if (id != null)
            {
                ViewBag.Id = id.Value;
                viewModel.Therapists = viewModel.Salons.Where(
                    i => i.Id == id.Value).Single().Therapists;
            }
                
            return View(viewModel);
        }

        public ActionResult Detail()
        {
            return View();
        }
    
                [HttpGet]
        public ActionResult Create(Salon salon)            // umozliwie nam wejscie do veiw create
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Salons.Add(salon);
         
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(salon);
        }
    
    }
}