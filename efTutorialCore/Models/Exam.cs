using System;
using System.Collections.Generic;

#nullable disable

namespace efTutorialCore.Models
{
    public partial class Exam
    {
        public decimal Examid { get; set; }
        public string Patientcode { get; set; }
        public string Doctorcode { get; set; }
        public DateTime Examdate { get; set; }

        public virtual Doctor DoctorcodeNavigation { get; set; }
        public virtual Patient PatientcodeNavigation { get; set; }
    }
}
