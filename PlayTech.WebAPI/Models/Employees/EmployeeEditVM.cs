using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace PlayTech.WebAPI.Models.Employees
{
    public class EmployeeEditVM
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
    }

    public class EmployeeEditValidator : AbstractValidator<EmployeeEditVM>
    {
        public EmployeeEditValidator()
        {
            RuleFor(o => o.DepartmentId)
                .NotNull();
            RuleFor(o => o.ManagerId)
                .NotNull();
            RuleFor(o => o.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.Salary)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
