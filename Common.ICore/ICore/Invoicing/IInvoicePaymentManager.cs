using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Invoicing;

namespace TechnoPro.Common.ICore.Invoicing
{
	// Token: 0x0200007B RID: 123
	public interface IInvoicePaymentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000362 RID: 866
		int CreateInvoicePayment(int InvoiceId, InvoicePayment InvoicePayment);

		// Token: 0x06000363 RID: 867
		void DeleteInvoicePayment(int InvoicePaymentId);
	}
}
