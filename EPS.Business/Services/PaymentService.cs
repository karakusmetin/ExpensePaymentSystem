using Stripe;

namespace EPS.Business.Services
{
	public interface IPaymentService
	{
		Task<bool> ProcessPaymentAsync(decimal amount, string currency, string cardToken);
	}
}

// PaymentService.cs

namespace EPS.Business.Services
{
	public class PaymentService : IPaymentService
	{
		public async Task<bool> ProcessPaymentAsync(decimal amount, string currency, string cardToken)
		{
			
				var options = new PaymentIntentCreateOptions
				{
					Amount = (long)(amount * 100), // Convert to cents
					Currency = currency,
					PaymentMethod = cardToken,
					ConfirmationMethod = "manual",
					Confirm = true,
				};

				var service = new PaymentIntentService();
				var paymentIntent = await service.CreateAsync(options);

				return paymentIntent.Status == "succeeded";
			
		}
	}
}
