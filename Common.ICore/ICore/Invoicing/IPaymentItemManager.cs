using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Invoicing;

namespace TechnoPro.Common.ICore.Invoicing
{
	// Token: 0x0200007C RID: 124
	public interface IPaymentItemManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000364 RID: 868
		int CreatePaymentItem(int PaymentId, PaymentItem PaymentItem);

		// Token: 0x06000365 RID: 869
		void DeletePaymentItem(int PaymentItemId);

		// Token: 0x06000366 RID: 870
		void UpdatePaymentItem(PaymentItem PaymentItem);

		// Token: 0x06000367 RID: 871
		PaymentItem LoadPaymentItemById(int PaymentItemId);

		// Token: 0x06000368 RID: 872
		IList<PaymentItem> LoadPaymentItemsByPaymentId(int PaymentId);
	}
}
