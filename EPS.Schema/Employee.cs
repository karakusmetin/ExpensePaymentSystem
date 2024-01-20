using EPS.Data.Entity;
using ESP.Base.Schema;

namespace EPS.Schema
{
	public class EmployeeRequest : BaseRequest
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
	}

	public class EmployeeResponse : BaseResponse
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
		public int ExpenRequestCount { get; set; }
		public DateTime? LastActivityDate { get; set; }
	}
}
