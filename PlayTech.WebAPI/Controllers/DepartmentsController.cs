using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlayTech.Business.Services.Interfaces;
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

        [HttpGet(nameof(Autocomplete) + "/{input}")]
        public async Task<IActionResult> Autocomplete(string input) =>
            Result(HttpStatusCode.OK, await _departmentService.AutocompleteAsync(input));
    }
}
