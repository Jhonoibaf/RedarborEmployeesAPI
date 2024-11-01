﻿using AutoMapper;
using Dapper;
using MediatR;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Domain.Enums;
using System.Data;

namespace RedarborEmployees.Application.EmployeesAdministration.Queries
{
    public class GetAllEployeesQuery
    {
        public record Query() : IRequest<List<EmployeeDto>>;

        public class Handler : IRequestHandler<Query, List<EmployeeDto>>
        {
            private readonly IDbConnection _dbConnection;
            private readonly IMapper _mapper;
            public Handler(IDbConnection dbConnection, IMapper mapper)
            {
                _dbConnection = dbConnection;
                _mapper = mapper;
            }

            public async Task<List<EmployeeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                const string sql = @"SELECT * 
                                     FROM Employees 
                                     WHERE status_id != @DeletedStatusId";

                var employees = await _dbConnection
                    .QueryAsync<EmployeeDto>(sql, new { DeletedStatusId = (int)StatusId.Deleted });

                return employees.AsList();
            }
        }

        public record Response(List<EmployeeDto> Employees);
    }
}
