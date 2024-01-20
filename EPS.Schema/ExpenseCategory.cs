using ESP.Base.Schema;

namespace EPS.Schema
{
	public class ExpenseCategoryRequest : BaseRequest
	{
		public string CategoryName { get; set; }
	}

	public class ExpenseCategoryResponse : BaseResponse
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public DateTime InsertDate { get; set; }
	}
}
