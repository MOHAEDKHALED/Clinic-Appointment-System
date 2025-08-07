# Clinic-Appointment-System
The Clinic Appointment System is a C# console application designed to streamline clinic operations. It provides a user-friendly interface to manage appointments, add patients and doctors, record payments, and store medical histories. Data is persisted in JSON files for simplicity and portability.
Features


![Screenshot](<img width="1918" height="1022" alt="image" src="https://github.com/user-attachments/assets/a4b650ce-299a-4df9-98ee-1634013f082e" />
)



- Add, update, and delete appointments



- Manage patient and doctor records



- Record and track payments



- Add medical history notes for patients



- Search appointments by doctor or patient name



- List all doctors, patients, paid/unpaid appointments



- Persistent storage using JSON files



- Logging of all operations to Logs.txt

## Built With





C# - Programming language used for the application



.NET Framework/Core - Framework for running the application




## Getting Started

Follow these steps to set up and run the Clinic Appointment System on your local machine.

Prerequisites





.NET SDK (version 6.0 or later recommended)



A text editor or IDE like Visual Studio or Visual Studio Code

## Project Structure
```
ClinicAppointmentSystem/
├── Data/
│   ├── Patients.json
│   ├── Doctors.json
│   ├── Appointments.json
│   ├── Payments.json
│   ├── MedicalHistories.json
│   ├── Logs.txt
├── Managers/
│   ├── ClinicManager.cs
│   ├── FileManager.cs
│   ├── PaymentManager.cs
│   ├── ScheduleManager.cs
│   ├── ILoggable.cs
├── Models/
│   ├── Appointment.cs
│   ├── Doctor.cs
│   ├── Patient.cs
│   ├── Payment.cs
│   ├── MedicalHistory.cs
├── Program.cs
├── ClinicAppointmentSystem.csproj
├── README.md
```

Data/: Stores JSON files for persistent data and logs.



Managers/: Contains classes for managing operations (e.g., ClinicManager, FileManager).



Models/: Defines data models (e.g., Patient, Doctor).



Program.cs: Entry point with the console interface.



ILoggable.cs: Interface for logging operations.

Contributing

Contributions are welcome! To contribute:





## Fork the repository.



- Create a new branch (git checkout -b feature/your-feature).



- Make your changes and commit (git commit -m "Add your feature").



- Push to the branch (git push origin feature/your-feature).



## Open a pull request.

Please ensure your code follows the existing style and includes appropriate comments. For major changes, open an issue first to discuss your ideas.

## License

Distributed under the MIT License. See LICENSE for more information.



Basic knowledge of C# and console applications
