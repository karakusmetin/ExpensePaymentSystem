using EPS.Data.Enums;
using ESP.Base.Schema;

namespace EPS.Schema
{
	public class ExpenseRequestRequest : BaseRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ExpenseCategory { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public string? DocumentUrl { get; set; } // Fatura veya fişin depolandığı URL
		public DateTime SubmissionDate { get; set; }
	}

	public class ExpenseRequestResponse : BaseResponse
	{
		public decimal Amount { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public string? DocumentUrl { get; set; } // Fatura veya fişin depolandığı URL
		public DateTime SubmissionDate { get; set; }
		public ExpenseRequestStatus Status { get; set; }
	}
}
