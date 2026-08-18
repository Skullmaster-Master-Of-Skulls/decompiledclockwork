using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.ReportFilter
{
	// Token: 0x020006EF RID: 1775
	internal interface IHierarchyFilterDescription
	{
		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x06003F22 RID: 16162
		IEnumerable<FilterDescription> Levels { get; }

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x06003F23 RID: 16163
		int LevelsCount { get; }

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x06003F24 RID: 16164
		bool IgnoreChildren { get; }
	}
}
