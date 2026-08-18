using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Invoicing;

namespace TechnoPro.Common.ICore.Invoicing
{
	// Token: 0x02000079 RID: 121
	public interface IInvoicableItemManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600035B RID: 859
		int CreateInvoicableItem(InvoicableItem Item);

		// Token: 0x0600035C RID: 860
		void DeleteInvoicableItem(int InvoicableItemId);

		// Token: 0x0600035D RID: 861
		void UpdateInvoicableItem(InvoicableItem Item);
	}
}
