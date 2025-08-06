using System;
using System.Collections.Generic;
using System.Linq;
using ClinicAppointmentSystem.Models;

namespace ClinicAppointmentSystem.Managers
{
    public class ClinicManager : ILoggable
    {
        private readonly List<Patient> _patients;
        private readonly List<Doctor> _doctors;
        private readonly List<Appointment> _appointments;
        private readonly FileManager _fileManager;
        private readonly ScheduleManager _scheduleManager;
        private readonly PaymentManager _paymentManager;

        public ClinicManager(FileManager fileManager)
        {
            _fileManager = fileManager;
            _patients = fileManager.LoadPatients();
            _doctors = fileManager.LoadDoctors();
            _appointments = fileManager.LoadAppointments();
            _scheduleManager = new ScheduleManager(_appointments);
            _paymentManager = new PaymentManager(fileManager);
        }

        public int AddPatient(string name, string phone, string email)
        {
            int patientId = _patients.Count > 0 ? _patients.Max(p => p.PatientId) + 1 : 1;
            var patient = new Patient(patientId, name, phone, email);
            _patients.Add(patient);
            _fileManager.SavePatients(_patients);
            Log($"Added patient: {name} (ID: {patientId})");
            return patientId;
        }

        public int AddDoctor(string name, string phone, string email, string specialty)
        {
            int doctorId = _doctors.Count > 0 ? _doctors.Max(d => d.DoctorId) + 1 : 1;
            var doctor = new Doctor(doctorId, name, phone, email, specialty);
            _doctors.Add(doctor);
            _fileManager.SaveDoctors(_doctors);
            Log($"Added doctor: {name} (ID: {doctorId})");
            return doctorId;
        }

        public int AddAppointment(string patientName, string doctorName, DateTime dateTime)
        {
            var patient = FindPatientByName(patientName);
            var doctor = FindDoctorByName(doctorName);

            if (patient == null || doctor == null)
                throw new Exception("Patient or Doctor not found.");

            if (!_scheduleManager.IsAvailable(doctor, dateTime))
                throw new Exception("Doctor is not available at this time.");

            int appointmentId = _appointments.Count > 0 ? _appointments.Max(a => a.AppointmentId) + 1 : 1;
            var appointment = new Appointment(appointmentId, dateTime, doctor, patient);
            _appointments.Add(appointment);
            _fileManager.SaveAppointments(_appointments);
            Log($"Added appointment {appointmentId} for {patientName} with {doctorName}");
            return appointmentId; 
        }

        public void UpdateAppointment(int appointmentId, DateTime newDateTime)
        {
            var appointment = _appointments.Find(a => a.AppointmentId == appointmentId);
            if (appointment == null)
                throw new Exception("Appointment not found.");

            if (!_scheduleManager.IsAvailable(appointment.Doctor, newDateTime))
                throw new Exception("Doctor is not available at the new time.");

            appointment.DateTime = newDateTime;
            _fileManager.SaveAppointments(_appointments);
            Log($"Updated appointment {appointmentId} to {newDateTime}");
        }

        public void DeleteAppointment(int appointmentId)
        {
            var appointment = _appointments.Find(a => a.AppointmentId == appointmentId);
            if (appointment == null)
                throw new Exception("Appointment not found.");

            _appointments.Remove(appointment);
            _fileManager.SaveAppointments(_appointments);
            Log($"Deleted appointment {appointmentId}");
        }

        public List<Appointment> SearchAppointmentsByDoctor(string doctorName)
        {
            var doctor = FindDoctorByName(doctorName);
            if (doctor == null)
                return new List<Appointment>();

            return _appointments.FindAll(a => a.Doctor.DoctorId == doctor.DoctorId && a.Status == "Scheduled");
        }

        public List<Appointment> SearchAppointmentsByPatient(string patientName)
        {
            var patient = FindPatientByName(patientName);
            if (patient == null)
                return new List<Appointment>();

            return _appointments.FindAll(a => a.Patient.PatientId == patient.PatientId && a.Status == "Scheduled");
        }

        public void AddMedicalHistory(int patientId, string note)
        {
            var patient = _patients.Find(p => p.PatientId == patientId);
            if (patient == null)
                throw new Exception("Patient not found.");

            patient.MedicalHistory.Add(note);
            _fileManager.SavePatients(_patients);
            Log($"Added medical history for patient {patientId}: {note}");
        }

        public void RecordPayment(int appointmentId, decimal amount)
        {
            _paymentManager.RecordPayment(appointmentId, amount);
        }

        public List<Doctor> GetAllDoctors()
        {
            return _doctors;
        }

        public List<Patient> GetAllPatients()
        {
            return _patients;
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointments.FindAll(a => a.Status == "Scheduled");
        }

        public List<Appointment> GetPaidAppointments()
        {
            var paidAppointmentIds = _paymentManager.GetAllPayments().Select(p => p.AppointmentId).Distinct();
            return _appointments.FindAll(a => paidAppointmentIds.Contains(a.AppointmentId) && a.Status == "Scheduled");
        }

        public List<Appointment> GetUnpaidAppointments()
        {
            var paidAppointmentIds = _paymentManager.GetAllPayments().Select(p => p.AppointmentId).Distinct();
            return _appointments.FindAll(a => !paidAppointmentIds.Contains(a.AppointmentId) && a.Status == "Scheduled");
        }

        public Doctor FindDoctorByName(string name)
        {
            return _doctors.FirstOrDefault(d => d.Name.ToLower().Contains(name.ToLower()));
        }

        public Patient FindPatientByName(string name)
        {
            return _patients.FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()));
        }

        public void Log(string message)
        {
            _fileManager.Log(message);
        }
    }
}