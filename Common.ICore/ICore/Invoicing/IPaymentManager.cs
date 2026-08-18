using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Invoicing;

namespace TechnoPro.Common.ICore.Invoicing
{
	// Token: 0x0200007D RID: 125
	public interface IPaymentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000369 RID: 873
		int CreatePayment(Payment Payment);

		// Token: 0x0600036A RID: 874
		void DeletePayment(int PaymentId);

		// Token: 0x0600036B RID: 875
		void UpdatePayment(Payment Payment);
	}
}
