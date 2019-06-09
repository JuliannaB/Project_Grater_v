using Grater.Models;
using Grater.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace Grater.Controllers
{
    // [Authorize]
    public class TherapistController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        private void PopulateAssignedSkillData(Therapist therapist)
        {
            var allSkills = _context.Skills;

            var therapistSkills = new HashSet<int>(therapist.Skills.Select(c => c.SkillId));

            var viewModel = new List<AssignedSkillData>();
            foreach (var skill in allSkills)
            {
                viewModel.Add(new AssignedSkillData
                {
                    SkillId = skill.SkillId,
                    SkillName = skill.SkillName,
                    Assigned = therapistSkills.Contains(skill.SkillId)
                });
            }
            ViewBag.Skills = viewModel;
        }

        private void PopulateAssignedSalonData(Therapist therapist)
        {
            var allSalons = _context.Salons;

            var therapistSalons = new HashSet<int>(therapist.Salons.Select(c => c.Id));

            var viewModel = new List<AssignedSalonData>();
            foreach (var salon in allSalons)
            {
                viewModel.Add(new AssignedSalonData
                {
                    Id = salon.Id,
                    SalonName = salon.SalonName,
                    Assigned = therapistSalons.Contains(salon.Id)
                });
            }
            ViewBag.Salons = viewModel;
        }


        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CitySortParm = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "";
            ViewBag.AvgSortParm = sortOrder == "Avg" ? "avg_desc" : "Avg";
            var therapists = from t in _context.Therapists
                             select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                therapists = therapists.Where(s => s.TherapistName.Contains(searchString)
                                       || s.TherapistSurname.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    therapists = therapists.OrderByDescending(s => s.TherapistName);
                    break;
                case "city_desc":
                    therapists = therapists.OrderByDescending(s => s.City);
                    break;
                case "Avg":
                    therapists = therapists.OrderBy(s => s.RatingAvg);
                    break;
                case "avg_desc":
                    therapists = therapists.OrderByDescending(s => s.RatingAvg);
                    break;
                default:
                    therapists = therapists.OrderBy(s => s.TherapistName);
                    break;
            }

            return View(therapists.ToList());
        }




        [HttpGet]
        public ActionResult Create()
        {
            var therapist = new Therapist();
            therapist.Skills = new List<Skill>();
            therapist.Salons = new List<Salon>();
            PopulateAssignedSkillData(therapist);
            PopulateAssignedSalonData(therapist);
            return View();
        }

        [Authorize(Roles = "Therapist")]
        // POST: /Create
        [HttpPost]

        public ActionResult Create(Therapist therapist, string[] SelectedSkills, string[] SelectedSalons) // dziala jak zloto, razem ze skillsami6 june
        {
            if (SelectedSkills != null)
            {
                therapist.Skills = new List<Skill>();
                foreach (var skill in SelectedSkills)
                {
                    var skillToAdd = _context.Skills.Find(int.Parse(skill));
                    therapist.Skills.Add(skillToAdd);
                }
            }


            if (SelectedSalons != null)
            {
                therapist.Salons = new List<Salon>();
                foreach (var salon in SelectedSalons)
                {
                    var salonToAdd = _context.Salons.Find(int.Parse(salon));
                    therapist.Salons.Add(salonToAdd) ;
                }
            }
            string fileName = "default-user";
            string extension = ".png";

            if (therapist.ImageFile != null)
                fileName = Path.GetFileNameWithoutExtension(therapist.ImageFile.FileName);
            if (therapist.ImageFile != null)
                extension = Path.GetExtension(therapist.ImageFile.FileName);
            if (therapist.ImageFile == null)
            { fileName = "default-user.png"; }
            else
            { fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension; }
            therapist.TherapistImagePath = "~/Image/" + fileName;

            if (therapist.ImageFile != null)
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            if (therapist.ImageFile != null)
                therapist.ImageFile.SaveAs(fileName);

            var store = new UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(_context);
            var manager = new ApplicationUserManager(store);

            int actUserId = User.Identity.GetUserId<int>();
            ApplicationUser actUser = manager.FindById(actUserId);
            actUser.Therapist = therapist;
            manager.Update(actUser);


            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            try
            {
                PopulateAssignedSalonData(therapist);
                PopulateAssignedSkillData(therapist);
            }
            catch (DataException /* dex */)
            {

            }
            return RedirectToAction("Details/" + therapist.TherapistId);
        }


        [HttpGet]
        //GET: Therapist/Details
        public ActionResult Details(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Therapist therapist = _context.Therapists.Find(id);
            if (therapist == null)
            {
                return HttpNotFound();
            }

            ViewBag.TherapistId = id.Value;

            var commentContent = _context.Comments.Where(d => d.TherapistId.Equals(id.Value)).ToList();
            ViewBag.CommentContent = commentContent;

            var ratings = _context.Comments.Where(d => d.TherapistId.Equals(id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
                therapist.RatingCount = ratingCount;
                therapist.RatingAvg = (double)ratingSum / (double)ratingCount;
                _context.SaveChanges();
            }
            else
            {
                ViewBag.RatingSym = 0;
                ViewBag.RatingCount = 0;
            }

            return View(therapist);
        }


        [HttpGet]

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            ApplicationUser user = _context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // POST: Therapist/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                ApplicationUser user = _context.Users.Find(id);
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Therapist therapist = _context.Therapists
            .Include(i => i.Skills)
            .Include(i => i.Salons)
               .Where(i => i.TherapistId == id)
               .Include(i => i.Salons)
               .Single();
            PopulateAssignedSalonData(therapist);
            PopulateAssignedSkillData(therapist);
            if (therapist == null)
            {
                return HttpNotFound();
            }
            return View(therapist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int? id, string[] selectedSkills, string [] selectedSalons)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var therapToUpdate = _context.Therapists
                .Include(i => i.Skills)
                .Include(i =>i.Salons)
               .Where(i => i.TherapistId == id)
               .Single();

            if (TryUpdateModel(therapToUpdate, "",
               new string[] { "TherapistName", "City", "Skills" }))
            {
                try
                {
                    UpdateTherap(selectedSkills, selectedSalons, therapToUpdate);

                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedSalonData(therapToUpdate);
            PopulateAssignedSkillData(therapToUpdate);
            return View(therapToUpdate);
        }

        private void UpdateTherap(string[] selectedSkills, string[] selectedSalons, Therapist therapistUpdate)
        {
            if (selectedSkills == null)
            {
                therapistUpdate.Skills = new List<Skill>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedSkills);
            var therapistSkills = new HashSet<int>
                (therapistUpdate.Skills.Select(c => c.SkillId));
            foreach (var skill in _context.Skills)
            {
                if (selectedSkills.Contains(skill.SkillId.ToString()))
                {
                    if (!therapistSkills.Contains(skill.SkillId))
                    {
                        therapistUpdate.Skills.Add(skill);
                    }
                }
                else
                {
                    if (therapistSkills.Contains(skill.SkillId))
                    {
                        therapistUpdate.Skills.Remove(skill);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

