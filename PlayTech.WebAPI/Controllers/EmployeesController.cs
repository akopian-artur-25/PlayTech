using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayTech.Business.Models.Employees;
using PlayTech.Business.Models.Employees.Exceptions;
using PlayTech.Business.Services.Interfaces;
using PlayTech.Shared.Data.Interfaces;
using PlayTech.WebAPI.Controllers.Base;
using PlayTech.WebAPI.Models.Employees;

namespace PlayTech.WebAPI.Controllers
{
    public class EmployeesController : BaseApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService
            , IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> List([FromBody] EmployeeListFilterVM filterVM)
        {
            var filter = _mapper.Map<EmployeeListFilterDTO>(filterVM);
            
            var list = await _employeeService.GetListAsync(filter);
            var listVM = _mapper.Map<IPagedList<EmployeeListItemVM>>(list);

            return Result(HttpStatusCode.OK, listVM);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeEditVM vm)
        {
            try
            {
                var model = _mapper.Map<EmployeeEditDTO>(vm);
                var employeeId = await _employeeService.SaveAsync(model);
                return Result(HttpStatusCode.OK, employeeId);
            }
            catch (EmployeeException ex)
            {
                ModelState.AddModelError(ErrorKey, ex.Message);
                return Result(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeEditVM vm)
        {
            try
            {
                var model = _mapper.Map<EmployeeEditDTO>(vm);
                var employeeId = await _employeeService.SaveAsync(model);
                return Result(HttpStatusCode.OK, employeeId);
            }
            catch (EmployeeException ex)
            {
                ModelState.AddModelError(ErrorKey, ex.Message);
                return Result(HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employeeId = await _employeeService.DeleteAsync(id);
                return Result(HttpStatusCode.OK, employeeId);
            }
            catch (EmployeeException ex)
            {
                ModelState.AddModelError(ErrorKey, ex.Message);
                return Result(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet(nameof(Autocomplete) + "/{input}")]
        public async Task<IActionResult> Autocomplete(string input) =>
            Result(HttpStatusCode.OK, await _employeeService.AutocompleteAsync(input));
    }
}
