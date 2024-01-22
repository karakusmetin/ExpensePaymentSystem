using System;

namespace EPS.Business.Services
{
	public interface IPaymentService
	{
		bool SimulatePayment(decimal amount, string currency, string paymentMethod);
	}

	public class PaymentSimulator : IPaymentService
	{
		public bool SimulatePayment(decimal amount, string currency, string paymentMethod)
		{



			// Ödeme başarılıysa
			if (paymentMethod == "success")
			{

				return true;
			}
			else
			{
				// Ödeme başarısızsa

				return false;
			}
		}
	}
}

