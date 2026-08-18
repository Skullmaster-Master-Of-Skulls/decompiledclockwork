using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001948 RID: 6472
	public class RadListViewInsertedEventArgs : RadListViewDataChangeEventArgs
	{
		// Token: 0x0600FA80 RID: 64128 RVA: 0x0038687F File Offset: 0x00384A7F
		public RadListViewInsertedEventArgs(int affectedRows, Exception e, RadListViewDataItem item) : base(affectedRows, e, item)
		{
			this.KeepInInsertMode = false;
		}

		// Token: 0x17004BB0 RID: 19376
		// (get) Token: 0x0600FA81 RID: 64129 RVA: 0x00386891 File Offset: 0x00384A91
		// (set) Token: 0x0600FA82 RID: 64130 RVA: 0x00386899 File Offset: 0x00384A99
		public bool KeepInInsertMode { get; set; }
	}
}
