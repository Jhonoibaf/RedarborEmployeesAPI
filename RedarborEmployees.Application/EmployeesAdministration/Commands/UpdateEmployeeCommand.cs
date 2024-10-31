using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Application.Validators;
using RedarborEmployees.Domain.Entities;
using RedarborEmployees.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedarborEmployees.Application.EmployeesAdministration.Commands
{
    public class UpdateEmployeeCommand
    {
        public record Command(int employeeId, EmployeeDto Employee) : IRequest<Employee>;
        public class Handler : IRequestHandler<Command, Employee>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;
            private readonly EmployeeDtoValidator _validator;

            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _dbcontext = dbcontext;
                _mapper = mapper;
                _validator = new EmployeeDtoValidator();
            }

            public async Task<Employee> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request.Employee);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new Exception(errorMessages);
                }

                var employeeDb = await _dbcontext.Employees
                                        .FirstOrDefaultAsync(e => e.EmployeeId == request.employeeId, cancellationToken);

                if (employeeDb == null) throw new Exception("Candidate not found");

                _mapper.Map(request.Employee, employeeDb);
                _dbcontext.Update(employeeDb);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Employee>(employeeDb);
            }
        }
        public record Response(Employee employee);
    }
}
}
