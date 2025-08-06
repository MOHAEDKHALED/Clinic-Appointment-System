using ClinicAppointmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.Managers
{
    public class ScheduleManager
    {
        private readonly List<Appointment> _appointments;

        public ScheduleManager(List<Appointment> appointments)
        {
            _appointments = appointments;
        }

        public bool IsAvailable(Doctor doctor, DateTime dateTime)
        {
            return !_appointments.Any(a => a.Doctor.DoctorId == doctor.DoctorId &&
                                          a.DateTime == dateTime &&
                                          a.Status == "Scheduled");
        }
    }
}
