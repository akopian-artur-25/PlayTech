using System;
using System.Collections.Generic;
using System.Text;

namespace PlayTech.Business.Models.Employees.Exceptions
{
    public class EmployeeException : Exception
    {
        public EmployeeException() : base()
        {
        }

        public EmployeeException(string message) : base(message)
        {
        }
    }
}
