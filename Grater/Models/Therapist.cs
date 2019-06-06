using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Grater.Models
{
    public class Therapist
    {
        [Key, ForeignKey("User")]

        public int TherapistId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
        
        [Required]
        [Display(Name = "Name")]
        public string TherapistName { get; set; }

        [Display(Name = "Surname")]
        public string TherapistSurname { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Display(Name = "I'm Mobile.")]
        public bool Mobile { get; set; }
        [Display(Name = "Please find below where we can meet")]
        public string WhereMobile { get; set; }

        public string SkillDesc { get; set; }
        // public byte[] TherapistImage { get; set; }
           
        public string TherapistImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public bool Active { get; set; }

        public int CreatedBy { get; set; }
                
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
