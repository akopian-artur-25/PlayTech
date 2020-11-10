using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using PlayTech.Business.Services.Interfaces;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.UnitOfWork.Models;

namespace PlayTech.Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;

        public DepartmentService(IRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task<Dictionary<int, string>> AutocompleteAsync(string input)
        {
            return await _repository.GetMany(o => o.Name.ToLower().Contains(input.ToLower()))
                .Take(10).ToDictionaryAsync(o => o.Id, o => o.Name);
        } 
    }
}
