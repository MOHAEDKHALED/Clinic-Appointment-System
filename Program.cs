using System;
using ClinicAppointmentSystem.Managers;

namespace ClinicAppointmentSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileManager = new FileManager();
            var clinicManager = new ClinicManager(fileManager);

            while (true)
            {
                Console.WriteLine("\nWelcome to the Clinic Appointment System");
                Console.WriteLine("1. Add Appointment");
                Console.WriteLine("2. Update Appointment");
                Console.WriteLine("3. Delete Appointment");
                Console.WriteLine("4. Search Appointments");
                Console.WriteLine("5. Record Payment");
                Console.WriteLine("6. Add Medical History");
                Console.WriteLine("7. Add Patient");
                Console.WriteLine("8. Add Doctor");
                Console.WriteLine("9. List All Doctors");
                Console.WriteLine("10. List All Patients");
                Console.WriteLine("11. List All Appointments");
                Console.WriteLine("12. List Paid Appointments");
                Console.WriteLine("13. List Unpaid Appointments");
                Console.WriteLine("14. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter patient name: ");
                            string patientName = Console.ReadLine();
                            Console.Write("Enter doctor name: ");
                            string doctorName = Console.ReadLine();
                            Console.Write("Enter appointment date and time (yyyy-MM-dd HH:mm): ");
                            DateTime dateTime = DateTime.Parse(Console.ReadLine());
                            int newAppointmentId = clinicManager.AddAppointment(patientName, doctorName, dateTime);
                            Console.WriteLine($"Appointment added successfully! Appointment ID: {newAppointmentId}");
                            break;

                        case "2":
                            Console.Write("Enter appointment ID: ");
                            int appointmentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter new date and time (yyyy-MM-dd HH:mm): ");
                            DateTime newDateTime = DateTime.Parse(Console.ReadLine());
                            clinicManager.UpdateAppointment(appointmentId, newDateTime);
                            Console.WriteLine("Appointment updated successfully!");
                            break;

                        case "3":
                            Console.Write("Enter appointment ID: ");
                            appointmentId = int.Parse(Console.ReadLine());
                            clinicManager.DeleteAppointment(appointmentId);
                            Console.WriteLine("Appointment deleted successfully!");
                            break;

                        case "4":
                            Console.Write("Enter doctor or patient name: ");
                            string searchTerm = Console.ReadLine();
                            var doctorAppointments = clinicManager.SearchAppointmentsByDoctor(searchTerm);
                            var patientAppointments = clinicManager.SearchAppointmentsByPatient(searchTerm);
                            var appointments = doctorAppointments.Concat(patientAppointments).Distinct().ToList();
                            if (appointments.Count == 0)
                                Console.WriteLine("No appointments found.");
                            else
                                foreach (var appt in appointments)
                                    Console.WriteLine($"Appointment ID: {appt.AppointmentId}, Patient: {appt.Patient.Name}, Doctor: {appt.Doctor.Name}, Time: {appt.DateTime}");
                            break;

                        case "5":
                            Console.Write("Enter appointment ID: ");
                            appointmentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            clinicManager.RecordPayment(appointmentId, amount);
                            Console.WriteLine("Payment recorded successfully!");
                            break;

                        case "6":
                            Console.Write("Enter patient ID: ");
                            int patientId = int.Parse(Console.ReadLine());
                            Console.Write("Enter medical note: ");
                            string note = Console.ReadLine();
                            clinicManager.AddMedicalHistory(patientId, note);
                            Console.WriteLine("Medical history added successfully!");
                            break;

                        case "7":
                            Console.Write("Enter patient name: ");
                            string pName = Console.ReadLine();
                            Console.Write("Enter phone number: ");
                            string pPhone = Console.ReadLine();
                            Console.Write("Enter email: ");
                            string pEmail = Console.ReadLine();
                            int newPatientId = clinicManager.AddPatient(pName, pPhone, pEmail);
                            Console.WriteLine($"Patient added successfully! Patient ID: {newPatientId}");
                            break;

                        case "8":
                            Console.Write("Enter doctor name: ");
                            string dName = Console.ReadLine();
                            Console.Write("Enter phone number: ");
                            string dPhone = Console.ReadLine();
                            Console.Write("Enter email: ");
                            string dEmail = Console.ReadLine();
                            Console.Write("Enter specialty: ");
                            string specialty = Console.ReadLine();
                            int newDoctorId = clinicManager.AddDoctor(dName, dPhone, dEmail, specialty);
                            Console.WriteLine($"Doctor added successfully! Doctor ID: {newDoctorId}");
                            break;

                        case "9":
                            var doctors = clinicManager.GetAllDoctors();
                            if (doctors.Count == 0)
                                Console.WriteLine("No doctors found.");
                            else
                                foreach (var doc in doctors)
                                    Console.WriteLine($"Doctor ID: {doc.DoctorId}, Name: {doc.Name}, Specialty: {doc.Specialty}");
                            break;

                        case "10":
                            var patients = clinicManager.GetAllPatients();
                            if (patients.Count == 0)
                                Console.WriteLine("No patients found.");
                            else
                                foreach (var patient in patients)
                                    Console.WriteLine($"Patient ID: {patient.PatientId}, Name: {patient.Name}");
                            break;

                        case "11":
                            var allAppointments = clinicManager.GetAllAppointments();
                            if (allAppointments.Count == 0)
                                Console.WriteLine("No appointments found.");
                            else
                                foreach (var appt in allAppointments)
                                    Console.WriteLine($"Appointment ID: {appt.AppointmentId}, Patient: {appt.Patient.Name}, Doctor: {appt.Doctor.Name}, Time: {appt.DateTime}");
                            break;

                        case "12":
                            var paidAppointments = clinicManager.GetPaidAppointments();
                            if (paidAppointments.Count == 0)
                                Console.WriteLine("No paid appointments found.");
                            else
                                foreach (var appt in paidAppointments)
                                    Console.WriteLine($"Appointment ID: {appt.AppointmentId}, Patient: {appt.Patient.Name}, Doctor: {appt.Doctor.Name}, Time: {appt.DateTime}");
                            break;

                        case "13":
                            var unpaidAppointments = clinicManager.GetUnpaidAppointments();
                            if (unpaidAppointments.Count == 0)
                                Console.WriteLine("No unpaid appointments found.");
                            else
                                foreach (var appt in unpaidAppointments)
                                    Console.WriteLine($"Appointment ID: {appt.AppointmentId}, Patient: {appt.Patient.Name}, Doctor: {appt.Doctor.Name}, Time: {appt.DateTime}");
                            break;

                        case "14":
                            return;

                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}