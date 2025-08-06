using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.Models
{
    public class Patient : Person
    {
        public int PatientId { get; set; }
        public List<string> MedicalHistory { get; set; } = new List<string>();

        public Patient(int patientId, string name, string phone, string email)
            : base(name, phone, email)
        {
            PatientId = patientId;
          
        }
    }
}
