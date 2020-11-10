using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PlayTech.Business.Models.Employees;
using PlayTech.Shared.Data.Interfaces;

namespace PlayTech.Business.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IPagedList<EmployeeListItemDTO>> GetListAsync(EmployeeListFilterDTO filter);
        Task<int> SaveAsync(EmployeeEditDTO model);
        Task<int> DeleteAsync(int id);
        Task<Dictionary<int, string>> AutocompleteAsync(string input);
    }
}
