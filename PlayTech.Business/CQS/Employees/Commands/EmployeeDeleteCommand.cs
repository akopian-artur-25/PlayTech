using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PlayTech.Business.Models.Employees.Exceptions;
using PlayTech.Shared.CQS.Commands;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.UnitOfWork.Models;

namespace PlayTech.Business.CQS.Employees.Commands
{
    public class EmployeeDeleteCommand : BaseDeleteCommandAsync<Employee>
    {
        public EmployeeDeleteCommand(IRepository<Employee> repository) : base(repository)
        {
        }

        public override Task BeforeDeleteAsync(Employee entity)
        {
            if (entity == null)
            {
                throw new EmployeeException("Employee not found");
            }

            if (_repository.Any(o => o.ManagerId == entity.Id))
            {
                throw new EmployeeException("Employee is manager");
            }

            return base.BeforeDeleteAsync(entity);
        }
    }
}
