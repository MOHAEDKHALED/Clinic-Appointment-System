using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.Models
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        protected Person(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}

