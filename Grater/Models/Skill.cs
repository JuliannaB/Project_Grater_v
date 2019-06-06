using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Grater.Models
{
 /*   public enum SkillName
    {
        Nails,
        FaceTreatment,
        Massage,
        BodyTreatment,
        Waxing,
        EyesTreatment,
        Makeup,
        Fillers,
        PermanentMakeup,
        FakeNails

    } */
    public class Skill
    {
    //    [ForeignKey("Therapist")]
        public int SkillId { get; set; }
        public string SkillName { get; set; }

        public virtual ICollection<Therapist> Therapists { get; set; }
     

    }
}