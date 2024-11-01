﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Application.Validators;
using RedarborEmployees.Domain.Entities;
using RedarborEmployees.Infrastructure.Data;
using RedarborEmployees.Infrastructure.Models;
using System.Threading;

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
            private readonly EmployeeDtoValidator _validator;

            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _mapper = mapper;
                _dbcontext = dbcontext;
            }
            public async Task<Employee> Handle(Command request, CancellationToken cancellationToken)
            {
                ValidateRequest(request);
                var employee = _mapper.Map<Employee>(request.Employee);
                employee.Validate();

                var employeeDb = _mapper.Map<EmployeeModel>(employee)
                                 ?? throw new Exception("Employee could not be created");

                _dbcontext.Employees.Add(employeeDb);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Employee>(employeeDb);
            }

            private async void ValidateRequest(Command request)
            {
                var validationResult = _validator.Validate(request.Employee);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new Exception(errorMessages);
                }

                var emailRegister= await _dbcontext.Employees
                                     .AnyAsync(e => e.Email == request.Employee.Email);

                if (emailRegister) throw new Exception("The email address is already in use.");
            }
        }
    }

    public record Response (Employee Employee);
}
