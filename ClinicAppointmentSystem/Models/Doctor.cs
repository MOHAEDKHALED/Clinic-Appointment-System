using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.Models
{
    public class Doctor : Person
    {
        public int DoctorId { get; set; }
        public string Specialty { get; set; }

        public Doctor(int doctorId, string name, string phone, string email, string specialty)
            : base(name, phone, email)
        {
            DoctorId = doctorId;
            Specialty = specialty;
        }
    }
}
