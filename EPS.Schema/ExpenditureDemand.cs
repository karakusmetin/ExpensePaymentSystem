using EPS.Data.Enums;
using ESP.Base.Schema;

namespace EPS.Schema
{
	public class ExpenditureDemandRequest : BaseRequest
	{
		public string Title { get; set; }
		public int ExpenseCategoryId { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public string? DocumentUrl { get; set; } // Fatura veya fişin depolandığı URL
	}
	public class ExpenditureDemandAdminRequest : BaseRequest
	{
		public int Id { get; set; }
		public string IsApproved { get; set; } // Yönetici tarafından onaylanıp onaylanmadığı
		public string? RejectionReason { get; set; } // Reddedilme durumunda nedeni
		public string IsActive { get; set; }
	}

	public class ExpenditureDemandResponse : BaseResponse
	{
		public int Id { get; set; }
		public int EmployeeId { get; set; } // Personel ID'si
		public string EmployeeFirstName { get; set; } // Personel İsmi
		public string EmployeeLastName { get; set; } // Personel Soyismi
		public string Title { get; set; }
		public string ExpenseCategory { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public string? DocumentUrl { get; set; } // Fatura veya fişin depolandığı URL
		public DateTime SubmissionDate { get; set; }
		public ExpenditureDemandStatus IsApproved { get; set; } // Yönetici tarafından onaylanıp onaylanmadığı
		public DateTime? UpdateDate { get; set; }
		public bool IsActive { get; set; }
	}
}
