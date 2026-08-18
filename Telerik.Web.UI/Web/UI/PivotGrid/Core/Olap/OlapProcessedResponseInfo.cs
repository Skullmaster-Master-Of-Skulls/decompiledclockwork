using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D11 RID: 3345
	internal class OlapProcessedResponseInfo
	{
		// Token: 0x170027C3 RID: 10179
		// (get) Token: 0x06007CA7 RID: 31911 RVA: 0x001C9F8A File Offset: 0x001C818A
		// (set) Token: 0x06007CA8 RID: 31912 RVA: 0x001C9F92 File Offset: 0x001C8192
		public Dictionary<Coordinate, AggregateValue[]> Aggregates { get; set; }

		// Token: 0x170027C4 RID: 10180
		// (get) Token: 0x06007CA9 RID: 31913 RVA: 0x001C9F9B File Offset: 0x001C819B
		// (set) Token: 0x06007CAA RID: 31914 RVA: 0x001C9FA3 File Offset: 0x001C81A3
		public Coordinate RootCoordinate { get; set; }

		// Token: 0x170027C5 RID: 10181
		// (get) Token: 0x06007CAB RID: 31915 RVA: 0x001C9FAC File Offset: 0x001C81AC
		// (set) Token: 0x06007CAC RID: 31916 RVA: 0x001C9FB4 File Offset: 0x001C81B4
		public IOlapPivotConfiguration PivotConfiguration { get; set; }
	}
}
