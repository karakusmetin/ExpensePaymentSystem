using ESP.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Schema
{
	public class DapperRequest : BaseRequest
	{
		public DateTime startDate { get; set; }
		public DateTime endDate { get; set; }
		
	}
}
