using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001215 RID: 4629
	public class TreeListNeedDataSourceEventArgs : EventArgs
	{
		// Token: 0x0600BF31 RID: 48945 RVA: 0x002A575A File Offset: 0x002A395A
		public TreeListNeedDataSourceEventArgs(TreeListRebindReason rebindReason)
		{
			this.RebindReason = rebindReason;
		}

		// Token: 0x17003DB0 RID: 15792
		// (get) Token: 0x0600BF32 RID: 48946 RVA: 0x002A5769 File Offset: 0x002A3969
		// (set) Token: 0x0600BF33 RID: 48947 RVA: 0x002A5771 File Offset: 0x002A3971
		public TreeListRebindReason RebindReason { get; protected set; }
	}
}
