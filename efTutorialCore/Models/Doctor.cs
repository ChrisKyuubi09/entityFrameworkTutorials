using System;
using System.Collections.Generic;

#nullable disable

namespace efTutorialCore.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Exams = new HashSet<Exam>();
        }

        public string Dcode { get; set; }
        public string Dname { get; set; }
        public string Specialtycode { get; set; }

        public virtual Specialty SpecialtycodeNavigation { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
