using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PlayTech.Shared.Utils;

namespace PlayTech.Business.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<KeyValue<int, string>>> AutocompleteAsync(string input);
    }
}
