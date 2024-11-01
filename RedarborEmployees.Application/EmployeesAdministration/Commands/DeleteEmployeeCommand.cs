using AutoMapper;
using MediatR;
using RedarborEmployees.Domain.Entities;
using RedarborEmployees.Infrastructure.Data;

namespace RedarborEmployees.Application.EmployeesAdministration.Commands
{
        public class DeleteEmployeeCommand
        {
            public record Command(int Id) : IRequest<Employee>;
            public class Handler : IRequestHandler<Command, Employee>
            {
                private readonly ApplicationDbContext _dbcontext;
                private readonly IMapper _mapper;
                public Handler(ApplicationDbContext dbcontext, IMapper mapper)
                {
                    _dbcontext = dbcontext;
                    _mapper = mapper;
                }
                public async Task<Employee> Handle(Command request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var employeeModel = await _dbcontext.Employees.FindAsync(request.Id);
                        if (employeeModel == null)
                        {
                            throw new Exception("Employee not found");
                        }

                        employeeModel.StatusId = 3;
                        employeeModel.DeletedOn = DateTime.Now;
                        employeeModel.UpdatedOn = DateTime.Now;
                        _dbcontext.Employees.Update(employeeModel);
                        await _dbcontext.SaveChangesAsync(cancellationToken);
                        var candidate = _mapper.Map<Employee>(employeeModel);
                        return candidate;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error deleting employee: {ex.Message}", ex);
                    }
                }
            }
            public record Response(Employee employee);
        }
}
