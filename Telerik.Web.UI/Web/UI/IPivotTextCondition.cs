using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000761 RID: 1889
	internal interface IPivotTextCondition
	{
		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06004299 RID: 17049
		// (set) Token: 0x0600429A RID: 17050
		string Pattern { get; set; }

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x0600429B RID: 17051
		// (set) Token: 0x0600429C RID: 17052
		TextComparison Comparison { get; set; }

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x0600429D RID: 17053
		// (set) Token: 0x0600429E RID: 17054
		bool IgnoreCase { get; set; }
	}
}
