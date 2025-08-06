using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.Managers
{
    public interface ILoggable
    {
        void Log(string message);
    }
}