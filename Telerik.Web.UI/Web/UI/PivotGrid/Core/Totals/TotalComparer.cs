using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C62 RID: 3170
	internal abstract class TotalComparer : SettingsNode, IComparer<TotalValue>
	{
		// Token: 0x0600778D RID: 30605
		public abstract int Compare(TotalValue x, TotalValue y);
	}
}
