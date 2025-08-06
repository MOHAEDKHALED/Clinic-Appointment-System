using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Payment(int paymentId, int appointmentId, decimal amount, DateTime date)
        {
            PaymentId = paymentId;
            AppointmentId = appointmentId;
            Amount = amount;
            Date = date;
        }
    }
}
