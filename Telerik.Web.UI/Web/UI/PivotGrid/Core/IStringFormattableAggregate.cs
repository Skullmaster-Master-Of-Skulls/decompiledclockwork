using System;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000683 RID: 1667
	internal interface IStringFormattableAggregate
	{
		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06003CBC RID: 15548
		// (set) Token: 0x06003CBD RID: 15549
		string StringFormat { get; set; }

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06003CBE RID: 15550
		// (set) Token: 0x06003CBF RID: 15551
		StringFormatSelector StringFormatSelector { get; set; }
	}
}
