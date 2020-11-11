using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayTech.Business.CQS.Employees.Commands;
using PlayTech.Business.CQS.Employees.Queries;
using PlayTech.Business.Models.Employees;
using PlayTech.Business.Services.Interfaces;
using PlayTech.Shared.Data.Interfaces;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.Shared.Utils;
using PlayTech.UnitOfWork.Models;

namespace PlayTech.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<IPagedList<EmployeeListItemDTO>> GetListAsync(EmployeeListFilterDTO filter)
        {
            return await new EmployeeListQuery(_repository).ExecuteAsync(filter);
        }

        public async Task<EmployeeEditDTO> GetByIdAsync(int id)
        {
            return await _repository.GetMany(o => o.Id == id)
                .Select(EmployeeEditDTO.ExpressionSelector())
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveAsync(EmployeeEditDTO model)
        {
            return await new EmployeeSaveCommand(_repository).ExecuteAsync(model);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await new EmployeeDeleteCommand(_repository).ExecuteAsync(id);
        }

        public async Task<IEnumerable<KeyValue<int, string>>> AutocompleteAsync(string input)
        {
            return await _repository.GetMany(o => o.Name.ToLower().Contains(input.ToLower()))
                .Take(10).Select(o => new KeyValue<int, string>(o.Id, o.Name)).ToListAsync();
        }
    }
}
