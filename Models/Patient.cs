using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public enum StatusP
    {
        Arrived,
        Sick,
        Healthy
    }

    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 1 - Arrived
        /// 2 - Sick
        /// 3 - Healthy
        /// 
        /// TODO: change it to enum
        /// </summary>
        public StatusP Status { get; set; }

        public DateTime DayOfBirth { get; set; }

        /// <summary>
        /// Идентификационный код.
        /// </summary>
        public string TaxCode { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}