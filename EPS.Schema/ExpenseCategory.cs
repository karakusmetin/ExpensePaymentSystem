using ESP.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
