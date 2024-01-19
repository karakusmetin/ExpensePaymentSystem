using AutoMapper;
using EPS.Data.Entity;
using EPS.Schema;
using System.Data;
using System.Net;

namespace Vb.Business.Mapper;

public class MapperConfig : Profile
{
	public MapperConfig()
	{
		CreateMap<AdminRequest, Admin>();
		CreateMap<Admin, AdminResponse>();

		CreateMap<EmployeeRequest, Employee>();
		CreateMap<Employee, EmployeeResponse>();

		CreateMap<EPS.Schema.ExpenseRequest, Expense>();
		CreateMap<Expense, ExpenseResponse>()
			.ForMember(dest => dest.EmployeeId,
				src => src.MapFrom(x => x.EmployeeId))
			.ForMember(dest => dest.EmployeeFirstName,
				src => src.MapFrom(x => x.Employee.FirstName))
			.ForMember(dest => dest.EmployeeLastName,
				src => src.MapFrom(x => x.Employee.LastName))
			.ForMember(dest => dest.ExpenseCategory,
				src => src.MapFrom(x => x.ExpenseCategory.CategoryName));

		CreateMap<ExpenseCategoryRequest, ExpenseCategory>();
		CreateMap<ExpenseCategory, ExpenseCategoryResponse>();

		CreateMap<ExpenseRequestRequest, EPS.Data.Entity.ExpenseRequest>();
		CreateMap<EPS.Data.Entity.ExpenseRequest, ExpenseRequestResponse>();
	}
}