using System;
using System.Collections.Generic;

#nullable disable

namespace efTutorialCore.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            Doctors = new HashSet<Doctor>();
        }

        public string Scode2 { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
