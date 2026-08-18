using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CFB RID: 3323
	internal interface IOlapPivotConfiguration
	{
		// Token: 0x170027A3 RID: 10147
		// (get) Token: 0x06007C00 RID: 31744
		IList<OlapAggregateDescription> PivotAggregateDescriptions { get; }

		// Token: 0x170027A4 RID: 10148
		// (get) Token: 0x06007C01 RID: 31745
		IList<OlapGroupDescription> PivotRowGroupDescriptions { get; }

		// Token: 0x170027A5 RID: 10149
		// (get) Token: 0x06007C02 RID: 31746
		IList<OlapGroupDescription> PivotColumnGroupDescriptions { get; }

		// Token: 0x170027A6 RID: 10150
		// (get) Token: 0x06007C03 RID: 31747
		IList<OlapFilterDescription> PivotFilterDescriptions { get; }
	}
}
