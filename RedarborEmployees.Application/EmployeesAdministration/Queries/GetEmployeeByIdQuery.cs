using AutoMapper;
using Dapper;
using MediatR;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Domain.Enums;
using System.Data;

namespace RedarborEmployees.Application.EmployeesAdministration.Queries
{
    public class GetEmployeeByIdQuery
    {
        public record Query(int employeeId) : IRequest<EmployeeDto>;

        public class Handler : IRequestHandler<Query, EmployeeDto>
        {
            private readonly IDbConnection _dbConnection;
            private readonly IMapper _mapper;
            public Handler(IDbConnection dbConnection, IMapper mapper)
            {
                _dbConnection = dbConnection;
                _mapper = mapper;
            }

            public async Task<EmployeeDto> Handle(Query request, CancellationToken cancellationToken)
            {
                const string sql = @"SELECT *
                                 FROM Employees
                                 WHERE employee_id = @EmployeeId AND status_id != @DeletedStatusId";

                using (var connection = _dbConnection)
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var employee = await connection.QueryFirstOrDefaultAsync<EmployeeDto>(sql, new { EmployeeId = request.employeeId, DeletedStatusId = (int)StatusId.Deleted });

                    return employee ?? new EmployeeDto();
                }
            }
        }

        public record Response(EmployeeDto Employee);
    }
}
