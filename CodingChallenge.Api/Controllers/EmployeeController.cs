using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CodingChallenge.Api.Models;
using CodingChallenge.Business.Components;
using CodingChallenge.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodingChallenge.Api.Controllers
{
    [ApiController]
    [Route("_api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeComponent _employeeComponent;
        private readonly IMapper _mapper;

        public EmployeeController(
            IEmployeeComponent employeeComponent,
            IMapper mapper)
        {
            _employeeComponent = employeeComponent;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetAllEmployees()
        {
            try
            {
                var results = await _employeeComponent.GetAllEmployeesAsync();

                return Ok(_mapper.Map<ICollection<EmployeeViewModel>>(results));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeComponent.GetEmployeeByIdAsync(id);

                return Ok(_mapper.Map<EmployeeViewModel>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeViewModel>> AddEmployee(EmployeeInputModel employee)
        {
            try
            {
                var result = await _employeeComponent.AddEmployeeAsync(_mapper.Map<Employee>(employee));

                return Ok(_mapper.Map<EmployeeViewModel>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateEmployee(EmployeeInputModel employee)
        {
            try
            {
                var result = await _employeeComponent.UpdateEmployeeAsync(_mapper.Map<Employee>(employee));

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeComponent.DeleteEmployeeAsync(id);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
