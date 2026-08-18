using System;
using Telerik.Web.UI.PivotGrid.Core.Totals;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200067F RID: 1663
	public interface IAggregateDescription : IDescriptionBase, INamed
	{
		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06003CAE RID: 15534
		TotalFormat TotalFormat { get; }

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06003CAF RID: 15535
		bool DisplayValueAsKpi { get; }
	}
}
