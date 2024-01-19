using ESP.Base.Schema;

namespace EPS.Schema
{
	public class ExpenseCategoryRequest : BaseRequest
	{
		public string CategoryName { get; set; }
	}

	public class ExpenseCategoryResponse : BaseResponse
	{
		public string CategoryName { get; set; }
	}
}
