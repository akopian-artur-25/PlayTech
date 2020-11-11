using System;
using System.Linq.Expressions;
using PlayTech.Shared.DTOs;
using PlayTech.UnitOfWork.Models;

namespace PlayTech.Business.Models.Employees
{
    public class EmployeeEditDTO : BaseDTO
    {
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }

        public static Expression<Func<Employee, EmployeeEditDTO>> ExpressionSelector()
        {
            return item => new EmployeeEditDTO
            {
                Id = item.Id,
                Name = item.Name,
                ManagerId = item.ManagerId,
                ManagerName = item.Manager != null ? item.Manager.Name : null,
                DepartmentId = item.DepartmentId,
                DepartmentName = item.Department != null ? item.Department.Name : null,
                Salary = item.Salary
            };
        }
    }
}
