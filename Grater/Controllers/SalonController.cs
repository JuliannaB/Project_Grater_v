using Grater.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grater.Controllers
{
    public class SalonController : Controller
    {

        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Salon
        public ActionResult Index()
        {
            return View();
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