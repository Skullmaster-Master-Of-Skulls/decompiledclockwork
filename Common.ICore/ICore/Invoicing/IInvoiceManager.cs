using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Invoicing;

namespace TechnoPro.Common.ICore.Invoicing
{
	// Token: 0x0200007A RID: 122
	public interface IInvoiceManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600035E RID: 862
		int CreateInvoice(Invoice Invoice);

		// Token: 0x0600035F RID: 863
		void DeleteInvoice(int InvoiceId);

		// Token: 0x06000360 RID: 864
		int AddPayment(InvoicePayment InvoicePayment);

		// Token: 0x06000361 RID: 865
		void UpdateInvoiceClosed(int InvoiceId, DateTime? NewClosedDate);
	}
}
