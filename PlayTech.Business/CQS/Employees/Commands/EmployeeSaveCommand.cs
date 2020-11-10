using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PlayTech.Business.Models.Employees;
using PlayTech.Business.Models.Employees.Exceptions;
using PlayTech.Shared.CQS.Commands;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.UnitOfWork.Models;

namespace PlayTech.Business.CQS.Employees.Commands
{
    public class EmployeeSaveCommand : BaseSaveCommandAsync<Employee, EmployeeEditDTO>
    {
        public EmployeeSaveCommand(IRepository<Employee> repository) : base(repository)
        {
        }

        protected override Task<Employee> PrepareEntityToInsertAsync(Employee entity, EmployeeEditDTO model)
        {
            if (entity.Id == model.ManagerId)
            {
                throw new EmployeeException("Employee cannot be a manager to himself");
            }

            return base.PrepareEntityToInsertAsync(entity, model);
        }

        protected override Employee PrepareEntity(Employee entity, EmployeeEditDTO model)
        {
            return new Employee
            {
                Id = model.Id,
                Name = model.Name,
                Salary = model.Salary,
                DepartmentId = model.DepartmentId,
                ManagerId = model.ManagerId
            };
        }
    }
}
