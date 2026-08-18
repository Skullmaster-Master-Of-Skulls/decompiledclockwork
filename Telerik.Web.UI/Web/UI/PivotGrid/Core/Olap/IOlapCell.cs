using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CF3 RID: 3315
	internal interface IOlapCell
	{
		// Token: 0x1700278D RID: 10125
		// (get) Token: 0x06007BD8 RID: 31704
		string FormattedValue { get; }

		// Token: 0x1700278E RID: 10126
		// (get) Token: 0x06007BD9 RID: 31705
		object Value { get; }

		// Token: 0x1700278F RID: 10127
		// (get) Token: 0x06007BDA RID: 31706
		int Ordinal { get; }
	}
}
