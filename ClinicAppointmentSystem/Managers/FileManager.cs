using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace ClinicAppointmentSystem.Managers
{
    public class FileManager : ILoggable
    {
        // our old way of deleclation folders and files
        //private readonly string _dataDirectory = "Data";
        //private readonly string _patientsFile = Path.Combine("Data", "Patients.json");
        //private readonly string _doctorsFile = Path.Combine("Data", "Doctors.json");
        //private readonly string _appointmentsFile = Path.Combine("Data", "Appointments.json");
        //private readonly string _paymentsFile = Path.Combine("Data", "Payments.json");
        //private readonly string _logFile = Path.Combine("Data", "Logs.txt");

        // our new way
        private readonly string _baseDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\.."));
        private readonly string _dataDirectory = Path.Combine(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..")), "Data");
        private readonly string _patientsFile = Path.Combine(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..")), "Data", "Patients.json");
        private readonly string _doctorsFile = Path.Combine(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..")), "Data", "Doctors.json");
        private readonly string _appointmentsFile = Path.Combine(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..")), "Data", "Appointments.json");
        private readonly string _paymentsFile = Path.Combine(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..")), "Data", "Payments.json");
        private readonly string _logFile = Path.Combine(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..")), "Data", "Logs.txt");

        public FileManager()
        {
            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
                Log("Created Data directory");
            }

            InitializeFile(_patientsFile);
            InitializeFile(_doctorsFile);
            InitializeFile(_appointmentsFile);
            InitializeFile(_paymentsFile);
            InitializeFile(_logFile);
        }

        private void InitializeFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, filePath.EndsWith(".json") ? "[]" : "");
                Log($"Created empty file: {filePath}");
            }
        }

        public void SaveToFile<T>(List<T> data, string filePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
                Log($"Saved data to {filePath}");
            }
            catch (Exception ex)
            {
                Log($"Error saving to {filePath}: {ex.Message}");
                throw;
            }
        }

        public List<T> LoadFromFile<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    InitializeFile(filePath);
                }
                string json = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
                Log($"Loaded data from {filePath}");
                return data;
            }
            catch (Exception ex)
            {
                Log($"Error loading from {filePath}: {ex.Message}");
                throw;
            }
        }

        public void Log(string message)
        {
            try
            {
                File.AppendAllText(_logFile, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging failed: {ex.Message}");
            }
        }

        public List<Models.Patient> LoadPatients() => LoadFromFile<Models.Patient>(_patientsFile);
        public void SavePatients(List<Models.Patient> patients) => SaveToFile(patients, _patientsFile);
        public List<Models.Doctor> LoadDoctors() => LoadFromFile<Models.Doctor>(_doctorsFile);
        public void SaveDoctors(List<Models.Doctor> doctors) => SaveToFile(doctors, _doctorsFile);
        public List<Models.Appointment> LoadAppointments() => LoadFromFile<Models.Appointment>(_appointmentsFile);
        public void SaveAppointments(List<Models.Appointment> appointments) => SaveToFile(appointments, _appointmentsFile);
        public List<Models.Payment> LoadPayments() => LoadFromFile<Models.Payment>(_paymentsFile);
        public void SavePayments(List<Models.Payment> payments) => SaveToFile(payments, _paymentsFile);
    }
}