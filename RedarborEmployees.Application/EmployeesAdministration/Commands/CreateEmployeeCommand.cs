using AutoMapper;
using MediatR;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Domain.Entities;
using RedarborEmployees.Infrastructure.Data;
using RedarborEmployees.Infrastructure.Models;

namespace RedarborEmployees.Application.EmployeesAdministration.Commands
{
    public class CreateEmployeeCommand
    {
        public class Command : IRequest<Employee>
        {
            public EmployeeDto Employee { get; }
            public Command(EmployeeDto employee)
            {
                Employee = employee;
            }
        }
        public class Handler : IRequestHandler<Command, Employee>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _mapper = mapper;
                _dbcontext = dbcontext;
            }
            public async Task<Employee> Handle(Command request, CancellationToken cancellationToken)
            {
                var employee = _mapper.Map<Employee>(request.Employee);
                var employeeDb = _mapper.Map<EmployeeModel>(employee);
                if (employeeDb == null) throw new Exception("Employee not be created");
                _dbcontext.Employees.Add(employeeDb);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Employee>(employeeDb);

            }
        }
    }

    public record Response (Employee Employee);
}
