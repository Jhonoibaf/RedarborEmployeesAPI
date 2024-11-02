using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Application.EmployeesAdministration.Commands;
using RedarborEmployees.Application.EmployeesAdministration.Queries;

namespace RedarborEmployees.API.Controllers
{
    [Route("api/redarbor")]
    [ApiController]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<EmployeeDto>> GetAllEmployees()
        {
            var employees = await _mediator.Send(new GetAllEployeesQuery.Query());
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery.Query(id));
            if (employee.EmployeeId == 0 ) return NotFound();
            return Ok(employee);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(new CreateEmployeeCommand.Command(employeeDto));

            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);

            return StatusCode(201, result.Data.EmployeeId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id , [FromBody] EmployeeDto employeeDto)
        {
            var result = await _mediator.Send(new UpdateEmployeeCommand.Command(id, employeeDto));

            if (result.IsSuccess) return Ok($"Employee with ID: {result.Data.EmployeeId} has been updated");

            return result.ErrorMessage == "Employee not found"
                ? NotFound(result.ErrorMessage)
                : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employeeDeleted = await _mediator.Send(new DeleteEmployeeCommand.Command(id));
            return employeeDeleted != null ? 
                StatusCode(200, $"Employee whit ID:{employeeDeleted.EmployeeId} has be deleted"):
                StatusCode(500, "Employee not be deleted");
        }
    }
}
