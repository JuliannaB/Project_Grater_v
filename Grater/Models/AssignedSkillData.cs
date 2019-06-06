using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public class AssignedSkillData
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public bool Assigned { get; set; }
    }
}