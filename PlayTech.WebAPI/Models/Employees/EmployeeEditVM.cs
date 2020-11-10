using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayTech.WebAPI.Models.Employees
{
    public class EmployeeEditVM
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? ManagerId { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
    }
}
