using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayTech.Shared.DTOs;

namespace PlayTech.Business.Models.Employees
{
    public class EmployeeInfoDTO : BaseDTO
    {
        public string Name { get; set; }
        public string FirstName => !string.IsNullOrEmpty(Name) ? Name.Split(' ').FirstOrDefault() : string.Empty;
        public string LastName => !string.IsNullOrEmpty(Name) ? Name.Split(' ').LastOrDefault() : string.Empty;
    }
}
