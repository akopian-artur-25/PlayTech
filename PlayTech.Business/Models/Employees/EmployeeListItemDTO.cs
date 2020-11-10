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
        public DepartmentInfoDTO Department { get; set; }
        public EmployeeInfoDTO Manager { get; set; }
        public EmployeeInfoDTO Employee { get; set; }
        public decimal? Salary { get; set; }

        public static Expression<Func<Employee, EmployeeListItemDTO>> ExpressionSelector()
        {
            return item => new EmployeeListItemDTO
            {
                Employee = new EmployeeInfoDTO
                {
                    Id = item.Id,
                    Name = item.Name
                },
                Salary = item.Salary
            };
        }
    }
}
