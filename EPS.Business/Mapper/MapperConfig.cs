using AutoMapper;
using EPS.Data.Entity;
using EPS.Data.Enums;
using EPS.Schema;
using System;

namespace ESP.Business.Mapper;

public class MapperConfig : Profile
{
	public MapperConfig()
	{
		CreateMap<AdminRequest, Admin>();
		CreateMap<Admin, AdminResponse>();

		CreateMap<EmployeeRequest, Employee>();
		CreateMap<Employee, EmployeeResponse>();

		CreateMap<ExpenditureDemand, Expense>();
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
		
		CreateMap<ExpenditureDemand, Expense>();

		CreateMap<ExpenditureDemandAdminRequest, ExpenditureDemand>()
			.ForMember(dest => dest.IsApproved,
		opt => opt.MapFrom(src => GetMyEnumValue(src.IsApproved)));
		
		CreateMap<ExpenditureDemandRequest, ExpenditureDemand>()
		.ForMember(dest => dest.ExpenseCategoryId,
				src => src.MapFrom(x => x.ExpenseCategoryId));
		
		CreateMap<ExpenditureDemand, ExpenditureDemandResponse>()
			.ForMember(dest => dest.EmployeeId,
				src => src.MapFrom(x => x.EmployeeId))
			.ForMember(dest => dest.EmployeeFirstName,
				src => src.MapFrom(x => x.Employee.FirstName))
			.ForMember(dest => dest.EmployeeLastName,
				src => src.MapFrom(x => x.Employee.LastName))
			.ForMember(dest => dest.ExpenseCategory,
				src => src.MapFrom(x => x.ExpenseCategory.CategoryName));

	}

	private static ExpenditureDemandStatus GetMyEnumValue(string enumString)
	{
		if (Enum.TryParse(enumString, out ExpenditureDemandStatus result))
		{
			return result;
		}

		return default(ExpenditureDemandStatus);
	}
}