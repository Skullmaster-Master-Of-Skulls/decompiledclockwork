using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019C2 RID: 6594
	public class RadListViewNeedDataSourceEventArgs : EventArgs
	{
		// Token: 0x0600FEAA RID: 65194 RVA: 0x00392AA5 File Offset: 0x00390CA5
		public RadListViewNeedDataSourceEventArgs(RadListViewRebindReason rebindReason)
		{
			this.RebindReason = rebindReason;
		}

		// Token: 0x17004CDE RID: 19678
		// (get) Token: 0x0600FEAB RID: 65195 RVA: 0x00392AB4 File Offset: 0x00390CB4
		// (set) Token: 0x0600FEAC RID: 65196 RVA: 0x00392ABC File Offset: 0x00390CBC
		public RadListViewRebindReason RebindReason { get; protected set; }
	}
}
