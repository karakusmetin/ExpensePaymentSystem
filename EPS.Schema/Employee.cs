using EPS.Data.Entity;
using ESP.Base.Schema;

namespace EPS.Schema
{
	public class EmployeeRequest : BaseRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
		public bool Username { get; set; }
		public bool Password { get; set; }
	}

	public class EmployeeResponse : BaseResponse
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
		public virtual List<Expense> Expenses { get; set; }
	}
}
