
namespace ESP.Base.Entity
{
	public class UserBaseEntity : BaseEntityWithId
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Status { get; set; }
		public string Email { get; set; }
		public DateTime? LastActivityDate { get; set; }
		public int? PasswordRetryCount { get; set; }
		public string Role { get; set; }
	}
}
