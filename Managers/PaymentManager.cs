using ClinicAppointmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinicAppointmentSystem.Managers
{
    public class PaymentManager : ILoggable
    {
        private readonly List<Payment> _payments;
        private readonly FileManager _fileManager;

        public PaymentManager(FileManager fileManager)
        {
            _fileManager = fileManager;
            _payments = fileManager.LoadPayments();
        }

        public void RecordPayment(int appointmentId, decimal amount)
        {
            int paymentId = _payments.Count > 0 ? _payments.Max(p => p.PaymentId) + 1 : 1;
            var payment = new Payment(paymentId, appointmentId, amount, DateTime.Now);
            _payments.Add(payment);
            _fileManager.SavePayments(_payments);
            Log($"Recorded payment {paymentId} for appointment {appointmentId}: {amount}");
        }

        public List<Payment> GetPaymentHistory(int appointmentId)
        {
            return _payments.FindAll(p => p.AppointmentId == appointmentId);
        }

        public List<Payment> GetAllPayments()
        {
            return _payments;
        }

        public void Log(string message)
        {
            _fileManager.Log(message);
        }
    }
}
