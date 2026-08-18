using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001E0 RID: 480
	public class RadDataFormNeedDataSourceEventArgs : EventArgs
	{
		// Token: 0x0600110C RID: 4364 RVA: 0x0003E96E File Offset: 0x0003CB6E
		public RadDataFormNeedDataSourceEventArgs(RadDataFormRebindReason rebindReason)
		{
			this.RebindReason = rebindReason;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0003E97D File Offset: 0x0003CB7D
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x0003E985 File Offset: 0x0003CB85
		public RadDataFormRebindReason RebindReason { get; protected set; }
	}
}
