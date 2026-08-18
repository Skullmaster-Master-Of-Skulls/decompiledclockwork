using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000E04 RID: 3588
	[Serializable]
	internal class PivotGridModelDataRow : PivotGridModelRowBase
	{
		// Token: 0x17002A11 RID: 10769
		// (get) Token: 0x0600850B RID: 34059 RVA: 0x001E6331 File Offset: 0x001E4531
		// (set) Token: 0x0600850C RID: 34060 RVA: 0x001E6339 File Offset: 0x001E4539
		public int DisplayIndex { get; set; }

		// Token: 0x0600850D RID: 34061 RVA: 0x001E6342 File Offset: 0x001E4542
		public PivotGridModelDataRow()
		{
			base.Cells = new List<PivotGridModelCellBase>();
		}
	}
}
