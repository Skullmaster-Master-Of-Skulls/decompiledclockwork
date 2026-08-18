using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD5 RID: 3541
	public class PivotGridItemDataBoundEventArgs : EventArgs
	{
		// Token: 0x0600839C RID: 33692 RVA: 0x001DFE62 File Offset: 0x001DE062
		public PivotGridItemDataBoundEventArgs(PivotGridItem item)
		{
			this.Item = item;
		}

		// Token: 0x1700298F RID: 10639
		// (get) Token: 0x0600839D RID: 33693 RVA: 0x001DFE71 File Offset: 0x001DE071
		// (set) Token: 0x0600839E RID: 33694 RVA: 0x001DFE79 File Offset: 0x001DE079
		public PivotGridItem Item { get; internal set; }
	}
}
