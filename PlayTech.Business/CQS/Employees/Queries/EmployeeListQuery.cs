using System;
using System.Linq.Expressions;
using PlayTech.Business.Models.Employees;
using PlayTech.Shared.CQS.Queries;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.UnitOfWork.Models;

namespace PlayTech.Business.CQS.Employees.Queries
{
    public class EmployeeListQuery : BaseListQueryAsync<Employee, EmployeeListItemDTO, EmployeeListFilterDTO>
    {
        public EmployeeListQuery(IRepository<Employee> repository) : base(repository)
        {
        }

        protected override Expression<Func<Employee, EmployeeListItemDTO>> ProjectToListItemDTO()
        {
            return EmployeeListItemDTO.ExpressionSelector();
        }
    }
}
