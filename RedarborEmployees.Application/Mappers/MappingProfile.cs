using AutoMapper;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Application.EmployeesAdministration.Commands;
using RedarborEmployees.Domain.Entities;
using RedarborEmployees.Infrastructure.Models;

namespace RedarborEmployees.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<EmployeeModel, Employee>();
            CreateMap<Employee, EmployeeModel>();
            CreateMap<CreateEmployeeCommand.Command, Employee>()
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore());
        }
    }
}
