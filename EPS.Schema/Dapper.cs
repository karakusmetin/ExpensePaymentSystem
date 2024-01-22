using ESP.Base.Schema;

namespace EPS.Schema
{
	public class DapperRequest : BaseRequest
	{
		public DateTime startDate { get; set; }
		public DateTime endDate { get; set; }
		
	}
}
