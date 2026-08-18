using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000466 RID: 1126
	public class DropDownTreeEntryEventArgs : EventArgs
	{
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x000831D4 File Offset: 0x000813D4
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x000831DC File Offset: 0x000813DC
		public DropDownTreeEntry Entry
		{
			get
			{
				return this._entry;
			}
			set
			{
				this._entry = value;
			}
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000831E5 File Offset: 0x000813E5
		public DropDownTreeEntryEventArgs(DropDownTreeEntry entry)
		{
			this._entry = entry;
		}

		// Token: 0x04000A4D RID: 2637
		private DropDownTreeEntry _entry;
	}
}
