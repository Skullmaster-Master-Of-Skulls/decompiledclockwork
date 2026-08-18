using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD4 RID: 3540
	public class PivotGridItemCreatedEventArgs : EventArgs
	{
		// Token: 0x06008399 RID: 33689 RVA: 0x001DFE42 File Offset: 0x001DE042
		public PivotGridItemCreatedEventArgs(PivotGridItem item)
		{
			this.Item = item;
		}

		// Token: 0x1700298E RID: 10638
		// (get) Token: 0x0600839A RID: 33690 RVA: 0x001DFE51 File Offset: 0x001DE051
		// (set) Token: 0x0600839B RID: 33691 RVA: 0x001DFE59 File Offset: 0x001DE059
		public PivotGridItem Item { get; internal set; }
	}
}
