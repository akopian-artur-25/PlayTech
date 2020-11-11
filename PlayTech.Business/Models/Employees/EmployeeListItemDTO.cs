using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using PlayTech.Business.Models.Departments;
using PlayTech.UnitOfWork.Models;

namespace PlayTech.Business.Models.Employees
{
    public class EmployeeListItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
        public DepartmentInfoDTO Department { get; set; }
        public EmployeeInfoDTO Manager { get; set; }

        public static Expression<Func<Employee, EmployeeListItemDTO>> ExpressionSelector()
        {
            return item => new EmployeeListItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Salary = item.Salary,
                Department = item.Department != null ? new DepartmentInfoDTO
                {
                    Id = item.Department.Id,
                    Name = item.Department.Name
                } : null,
                Manager = item.Manager != null ? new EmployeeInfoDTO
                {
                    Id = item.Manager.Id,
                    Name = item.Manager.Name
                } : null
            };
        }
    }
}
