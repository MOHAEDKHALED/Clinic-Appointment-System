using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime DateTime { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public string Status { get; set; }

        public Appointment(int appointmentId, DateTime dateTime, Doctor doctor, Patient patient)
        {
            AppointmentId = appointmentId;
            DateTime = dateTime;
            Doctor = doctor;
            Patient = patient;
            Status = "Scheduled";
        }
    }
}
