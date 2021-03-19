using System;
using System.Collections.Generic;

#nullable disable

namespace efTutorialCore.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Exams = new HashSet<Exam>();
        }

        public string Pcode { get; set; }
        public string Pname { get; set; }
        public string Paddress { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
