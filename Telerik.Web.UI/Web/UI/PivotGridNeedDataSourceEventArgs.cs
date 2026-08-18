using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C3A RID: 3130
	public class PivotGridNeedDataSourceEventArgs : EventArgs
	{
		// Token: 0x0600767E RID: 30334 RVA: 0x001B8406 File Offset: 0x001B6606
		public PivotGridNeedDataSourceEventArgs(PivotGridRebindReason rebindReason)
		{
			this.RebindReason = rebindReason;
		}

		// Token: 0x17002686 RID: 9862
		// (get) Token: 0x0600767F RID: 30335 RVA: 0x001B8415 File Offset: 0x001B6615
		// (set) Token: 0x06007680 RID: 30336 RVA: 0x001B841D File Offset: 0x001B661D
		public PivotGridRebindReason RebindReason { get; protected set; }
	}
}
