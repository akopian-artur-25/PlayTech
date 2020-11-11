using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayTech.Business.Models.Departments;
using PlayTech.Business.Models.Employees;
using PlayTech.Shared.Data.Interfaces;
using PlayTech.WebAPI.Models.Departments;
using PlayTech.WebAPI.Models.Employees;
using PlayTech.WebAPI.Models.Shared.Data;

namespace PlayTech.WebAPI
{
    public class VMToDTOMapping : Profile
    {
        public VMToDTOMapping()
        {
            CreateMap<EmployeeListFilterVM, EmployeeListFilterDTO>();
            CreateMap<EmployeeEditVM, EmployeeEditDTO>();
        }
    }

    public class DTOToVMMapping : Profile
    {
        public DTOToVMMapping()
        {
            CreateMap(typeof(IPagedList<>), typeof(PagedListVM<>));

            CreateMap<EmployeeEditDTO, EmployeeEditVM>();
            CreateMap<EmployeeListItemDTO, EmployeeListItemVM>();
            CreateMap<EmployeeInfoDTO, EmployeeInfoVM>();

            CreateMap<DepartmentInfoDTO, DepartmentInfoVM>();
        }
    }
}
