using System;
using System.Collections.Generic;

namespace PlayTech.UnitOfWork.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? ManagerId { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
    }
}
