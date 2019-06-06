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

        //GET: Therapist/Index    Jest ok, przywrocic po usunieciu Radom
        /*   public ViewResult Index(string sortOrder, string searchString)  // wylistowuje terapeutki, dodaje mozliwosc szukania
           {
               ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

               var therapists = from b in _context.Therapists
                                select b;

               switch (sortOrder)
               {
                   default:
                       therapists = therapists.OrderBy(s => s.City);
                       break;
               }

               if (!String.IsNullOrEmpty(searchString))
               {
                   therapists = therapists.Where(b => (b.TherapistName.Contains(searchString) || (b.TherapistName == null)) || (b.City.Contains(searchString) || (b.City == null)));
               }

               return View(therapists.ToList());
           }

           */


        public ActionResult Index(int? id, int? skillId)
        {
            var viewModel = new TherapistIndexData();

            viewModel.Therapists = _context.Therapists
                .Include(i => i.Skills)
                .OrderBy(i => i.TherapistName);


            if (id != null)
            {
                ViewBag.TherapistId = id.Value;
                viewModel.Skills = viewModel.Therapists.Where(
                    i => i.TherapistId == id.Value).Single().Skills;
            }

            if (skillId != null)
            {
                ViewBag.SkillId = skillId.Value;
                // Lazy loading
                //viewModel.Enrollments = viewModel.Courses.Where(
                //    x => x.CourseID == courseID).Single().Enrollments;
                // Explicit loading
                var selectedSkills = viewModel.Skills.Where(x => x.SkillId == skillId).Single();

            }

            return View(viewModel);
        }
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
        /*
        //

    public ActionResult Edit(int? id)
          {
              if (id == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }
              Therapist therapist = _context.Therapists
                  .Include(i => i.TherapistSkills)
                  .Include(i => i.)
                  .Where(i => i.ID == id)
                  .Single();
              PopulateAssignedSkillData(therapist);
              if (therapist == null)
              {
                  return HttpNotFound();
              }
              return View(therapist);
          }

          private void PopulateAssignedSkillData(Therapist therapist)
          {
              var allSkills = _context.Skills;
              var therapistSkill = new HashSet<int>(therapist.Skills.Select(c => c.SkillId));
              var viewModel = new List<TherapistSkill>();
              foreach (var skill in allSkills)
              {
                  viewModel.Add(new AssignedSkillData
                  {
                      SkillId = skill.SkillId,
                      SkillName = skill.SkillName
                      Assigned = therapistSkill.Contains(skill.SkillId.CourseID)
                  });
              }
              ViewBag.Courses = viewModel;
          }  */
        /*      [HttpGet]
         public ActionResult Create([Bind(Include = "TherapistName, TherapistSurname, City, Description, PhoneNumber, Email, Mobile,")]   Therapist therapist, Skill skill)            // umozliwie nam wejscie do veiw create
         {

             try
             {
                 if (ModelState.IsValid)
                 {
                     _context.Therapists.Add(therapist);
                     _context.Skills.Add(skill);
                     _context.SaveChanges();
                     return RedirectToAction("Index");
                 }
             }
             catch (DataException)
             {
                 //Log the error (uncomment dex variable name and add a line here to write a log.
                 ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
             }
             return View(therapist);
         }  */


        /*   [HttpPost]  
           [Authorize(Roles = "Therapist")]
           [HttpGet]
           [Authorize(Roles = "Therapist")]  
           public ActionResult Create()
           {

               return View();
           }   */


        [HttpGet]
        public ActionResult Create()
        {
            var therapist = new Therapist();
            therapist.Skills = new List<Skill>();
            PopulateAssignedSkillData(therapist);
            return View();
        }

        [Authorize(Roles = "Therapist")]
        // POST: /Create
        [HttpPost]


     /*   public ActionResult Create(Therapist therapist)
          {
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


              ModelState.Clear();
              return RedirectToAction("Details/" + therapist.TherapistId);
          } */

   
               public ActionResult Create(Therapist therapist, string[] SelectedSkills) // dziala jak zloto, razem ze skillsami6 june
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

            PopulateAssignedSkillData(therapist);
            return RedirectToAction("Details/" + therapist.TherapistId);
        } 
        /*   private void PopulateAssignedCourseData(Therapist therapist )
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
               ViewBag.Courses = viewModel;
           }  */

        /*    public ActionResult Create(Therapist therapist, string[] SelectedSkills)
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
                 if (ModelState.IsValid)
                 {
                     _context.Therapists.Add(therapist);
                     _context.SaveChanges();
                     return RedirectToAction("Index");
                 }
                 PopulateAssignedCourseData(therapist);
                 return View(therapist);
             }  */
        [HttpGet]
        //GET: Therapist/Details
        public ActionResult Details(int? id)    //Wchodzimy do view details, po klikieciu na imie. Dziala ok
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

        /*      public ActionResult Edit(int? id)
        {
            int userLog = User.Identity.GetUserId<int>();
            return RedirectToAction("Edit/" + userLog);
        } */
             public ActionResult Edit(int? id)
             {
                 if (id == null)
                 {
                     return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                 }
                 Therapist therapist = _context.Therapists
                 .Include(i => i.Skills)
                    .Where(i => i.TherapistId == id)
                    .Single();
                       PopulateAssignedSkillData(therapist);
                 if (therapist == null)
                 {
                     return HttpNotFound();
                 }
                 return View(therapist);
             }

    /*
             public ActionResult Edit(int? id)
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
                 return View(therapist);
             }


             /*     [HttpPost]
                  [ValidateAntiForgeryToken]
                  public ActionResult Edit([Bind(Include = "TherapistName, PhoneNumber, City")] ApplicationUser user)
                  {
                      if (ModelState.IsValid)
                      {
                          _context.Entry(user).State = EntityState.Modified;


                       /*   var store = new UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(_context);
                          var manager = new ApplicationUserManager(store);

                          int actUserId = User.Identity.GetUserId<int>();
                          ApplicationUser actUser = manager.FindById(actUserId);
                          actUser.Therapist = therapist;
                          manager.Update(actUser);

                          _context.SaveChanges();
                          return RedirectToAction("Index");
                      }
                      return View(user);
                  }
                  */
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public ActionResult Edit(int? id, string[] selectedSkills)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var therapToUpdate = _context.Therapists
                .Include(i => i.Skills)
               .Where(i => i.TherapistId == id)
               .Single();

            if (TryUpdateModel(therapToUpdate, "",
               new string[] { "TherapistName", "City", "Skills"}))
            {
                try
                {


                    UpdateTherap(selectedSkills, therapToUpdate);

                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedSkillData(therapToUpdate);
            return View(therapToUpdate);
        }





        private void UpdateTherap(string[] selectedSkills, Therapist therapistUpdate)
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

