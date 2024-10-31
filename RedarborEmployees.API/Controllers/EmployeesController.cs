using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Application.EmployeesAdministration.Commands;

namespace RedarborEmployees.Web.Controllers
{
    [Route("api/redarbor")]
    [ApiController]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var employeeCreated = await _mediator.Send(new CreateEmployeeCommand.Command(employeeDto));
            return employeeCreated != null ? StatusCode(200, employeeCreated.EmployeeId): StatusCode(500, "Employee not be created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id , [FromBody] EmployeeDto employeeDto)
        {
            var employeeUpdated = await _mediator.Send(new UpdateEmployeeCommand.Command(id , employeeDto));
            return employeeUpdated != null ? StatusCode(200, $"Employee whit ID:{employeeUpdated.EmployeeId} has be updated") : StatusCode(500, "Employee not be Updated");
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
