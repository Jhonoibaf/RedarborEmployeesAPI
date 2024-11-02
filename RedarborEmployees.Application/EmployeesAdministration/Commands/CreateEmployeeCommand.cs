using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Application.Validators;
using RedarborEmployees.Domain.Entities;
using RedarborEmployees.Infrastructure.Data;
using RedarborEmployees.Infrastructure.Models;

namespace RedarborEmployees.Application.EmployeesAdministration.Commands
{
    public class CreateEmployeeCommand
    {
        public class Command : IRequest<Result<Employee>>
        {
            public EmployeeDto Employee { get; }
            public Command(EmployeeDto employee)
            {
                Employee = employee;
            }
        }

        public class Handler : IRequestHandler<Command, Result<Employee>>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;
            private readonly EmployeeDtoValidator _validator;

            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _mapper = mapper;
                _dbcontext = dbcontext;
                _validator = new EmployeeDtoValidator();
            }

            public async Task<Result<Employee>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request.Employee);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return Result<Employee>.Failure(errorMessages);
                }

                var emailRegister = await _dbcontext.Employees
                                      .AnyAsync(e => e.Email == request.Employee.Email);

                if (emailRegister)
                    return Result<Employee>.Failure("The email address is already in use.");

                var employee = _mapper.Map<Employee>(request.Employee);
                employee.Validate();

                var employeeDb = _mapper.Map<EmployeeModel>(employee)
                                  ?? throw new Exception("Employee could not be created");

                _dbcontext.Employees.Add(employeeDb);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                return Result<Employee>.Success(_mapper.Map<Employee>(employeeDb));
            }
        }
    }
    public record Response(Employee Employee);
}
