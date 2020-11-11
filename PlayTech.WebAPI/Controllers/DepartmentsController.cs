using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayTech.Business.Services.Interfaces;
using PlayTech.Shared.Utils;
using PlayTech.WebAPI.Controllers.Base;

namespace PlayTech.WebAPI.Controllers
{
    public class DepartmentsController : BaseApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [ProducesResponseType(typeof(IEnumerable<KeyValue<int, string>>), StatusCodes.Status200OK)]
        [HttpGet(nameof(Autocomplete) + "/{input}")]
        public async Task<IActionResult> Autocomplete(string input) =>
            Result(HttpStatusCode.OK, await _departmentService.AutocompleteAsync(input));
    }
}
