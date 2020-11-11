using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayTech.WebAPI.Models.Departments;

namespace PlayTech.WebAPI.Models.Employees
{
    public class EmployeeListItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
        public DepartmentInfoVM Department { get; set; }
        public EmployeeInfoVM Manager { get; set; }
    }
}
