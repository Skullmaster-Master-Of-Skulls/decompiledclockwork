using System;

namespace Telerik.Web.UI
{
	// Token: 0x020009BA RID: 2490
	public class AutoCompleteEntryEventArgs : EventArgs
	{
		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x06005F3B RID: 24379 RVA: 0x00122688 File Offset: 0x00120888
		// (set) Token: 0x06005F3C RID: 24380 RVA: 0x00122690 File Offset: 0x00120890
		public AutoCompleteBoxEntry Entry
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

		// Token: 0x06005F3D RID: 24381 RVA: 0x00122699 File Offset: 0x00120899
		public AutoCompleteEntryEventArgs(AutoCompleteBoxEntry entry)
		{
			this._entry = entry;
		}

		// Token: 0x040016F1 RID: 5873
		private AutoCompleteBoxEntry _entry;
	}
}
