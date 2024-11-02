using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Application.EmployeesAdministration.Commands;
using RedarborEmployees.Application.EmployeesAdministration.Queries;

namespace RedarborEmployees.Web.Controllers
{
    public class EmployeeModelsController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new GetAllEployeesQuery.Query()));
        }

        public async Task<IActionResult> Details(int id)
        {
            var employeeModel = await _mediator.Send(new GetEmployeeByIdQuery.Query(id));
            if (employeeModel == null) return NotFound();

            return View(employeeModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid) return View(employeeDto);
            var employeeCreated = await _mediator.Send(new CreateEmployeeCommand.Command(employeeDto));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employeeModel = await _mediator.Send(new GetEmployeeByIdQuery.Query(id));
            if (employeeModel == null) return NotFound();
            return View(employeeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee( [FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid) return View(employeeDto);
            var employeeUpdated = await _mediator.Send(new UpdateEmployeeCommand.Command(employeeDto.EmployeeId, employeeDto));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employeeModel = await _mediator.Send(new GetEmployeeByIdQuery.Query(id));
            if (employeeModel == null) return NotFound();
            return View(employeeModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeDeleted = await _mediator.Send(new DeleteEmployeeCommand.Command(id));
            return RedirectToAction(nameof(Index));
        }

    }
}
