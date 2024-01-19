﻿using ESP.Base.Schema;
namespace EPS.Schema
{
	public class AdminRequest : BaseRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}

	public class AdminResponse : BaseResponse
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
	}
}