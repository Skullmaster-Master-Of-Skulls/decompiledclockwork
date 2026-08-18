using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000273 RID: 627
	public class DropDownTreeEntriesEventArgs : EventArgs
	{
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0004D10D File Offset: 0x0004B30D
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x0004D115 File Offset: 0x0004B315
		public List<DropDownTreeEntry> Entries
		{
			get
			{
				return this._entries;
			}
			set
			{
				this._entries = value;
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0004D11E File Offset: 0x0004B31E
		public DropDownTreeEntriesEventArgs(List<DropDownTreeEntry> entries)
		{
			this._entries = entries;
		}

		// Token: 0x040005F7 RID: 1527
		private List<DropDownTreeEntry> _entries;
	}
}
