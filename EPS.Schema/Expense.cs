using EPS.Data.Enums;
using ESP.Base.Schema;

namespace EPS.Schema
{
	public class ExpenseRequest : BaseRequest
	{
		public int EmployeeId { get; set; } // Personel ID'si
		public string EmployeeFirstName { get; set; }
		public string EmployeeLastName { get; set; }
		public string ExpenseCategory { get; set; }
		public string Title { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public string? DocumentUrl { get; set; } // Fatura veya fişin depolandığı URL
		public DateTime SubmissionDate { get; set; }
		public ExpenditureDemandStatus IsApproved { get; set; } // Yönetici tarafından onaylanıp onaylanmadığı
		public string? RejectionReason { get; set; } // Reddedilme durumunda nedeni
		public DateTime? ApprovalDate { get; set; } // Onay tarihi, eğer onaylandıysa
	}

	public class ExpenseResponse : BaseResponse
	{
		public int EmployeeId { get; set; } // Personel ID'si
		public int EmployeeFirstName { get; set; } // Personel İsmi
		public int EmployeeLastName { get; set; } // Personel Soyismi
		public string Title { get; set; }
		public string ExpenseCategory { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public string? DocumentUrl { get; set; } // Fatura veya fişin depolandığı URL
		public DateTime SubmissionDate { get; set; }
		public ExpenditureDemandStatus IsApproved { get; set; } // Yönetici tarafından onaylanıp onaylanmadığı
		public string? RejectionReason { get; set; } // Reddedilme durumunda nedeni
		public DateTime? ApprovalDate { get; set; } // Onay tarihi, eğer onaylandıysa
	}
}
