using EPS.Data.Enums;
using ESP.Base.Schema;

namespace EPS.Schema
{
	public class ExpenseRequestRequest : BaseRequest
	{
		public int ExpenseId { get; set; }
		public ExpenseRequestStatus Status { get; set; }
		public string EvaluationComment { get; set; }
	}

	public class ExpenseRequestResponse : BaseResponse
	{
		public int ExpenseId { get; set; }
		public ExpenseRequestStatus Status { get; set; }
		public string EvaluationComment { get; set; }
	}
}
