using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Application.Validators;
using RedarborEmployees.Domain.Entities;
using RedarborEmployees.Infrastructure.Data;

namespace RedarborEmployees.Application.EmployeesAdministration.Commands
{
    public class UpdateEmployeeCommand
    {
        public record Command(int employeeId, EmployeeDto Employee) : IRequest<Result<Employee>>;
        public class Handler : IRequestHandler<Command, Result<Employee>>
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

            public async Task<Result<Employee>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request.Employee);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return Result<Employee>.Failure(errorMessages);
                }

                var employeeDb = await _dbcontext.Employees
                                    .FirstOrDefaultAsync(e => e.EmployeeId == request.employeeId, cancellationToken);

                if (employeeDb == null) return Result<Employee>.Failure("Employee not found");

                _mapper.Map(request.Employee, employeeDb);
                employeeDb.UpdatedOn = DateTime.Now;
                _dbcontext.Update(employeeDb);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                return Result<Employee>.Success(_mapper.Map<Employee>(employeeDb));
            }
        }
        public record Response(Employee employee);
    }
}

