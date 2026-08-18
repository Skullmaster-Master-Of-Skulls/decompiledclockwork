using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001947 RID: 6471
	public class RadListViewUpdatedEventArgs : RadListViewDataChangeEventArgs
	{
		// Token: 0x0600FA7D RID: 64125 RVA: 0x0038685C File Offset: 0x00384A5C
		public RadListViewUpdatedEventArgs(int affectedRows, Exception e, RadListViewDataItem item) : base(affectedRows, e, item)
		{
			this.KeepInEditMode = false;
		}

		// Token: 0x17004BAF RID: 19375
		// (get) Token: 0x0600FA7E RID: 64126 RVA: 0x0038686E File Offset: 0x00384A6E
		// (set) Token: 0x0600FA7F RID: 64127 RVA: 0x00386876 File Offset: 0x00384A76
		public bool KeepInEditMode { get; set; }
	}
}
