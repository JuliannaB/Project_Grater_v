using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grater.Models
{
    public class Contract
    {
        public int ContractId { get; set; }
        public int TherapistId { get; set; }
        public int SalonId { get; set; }
        public Therapist Therapist { get; set; }
        public Salon Salon { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FinishDate { get; set; }

    }
}
