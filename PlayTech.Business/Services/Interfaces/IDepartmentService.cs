using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlayTech.Business.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<Dictionary<int, string>> AutocompleteAsync(string input);
    }
}
