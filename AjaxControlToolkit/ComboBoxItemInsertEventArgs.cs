using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000073 RID: 115
	public class ComboBoxItemInsertEventArgs : CancelEventArgs
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000C2F3 File Offset: 0x0000A4F3
		internal ComboBoxItemInsertEventArgs(string text, ComboBoxItemInsertLocation location)
		{
			this._listItem = new ListItem(text);
			this._insertLocation = location;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000C317 File Offset: 0x0000A517
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0000C30E File Offset: 0x0000A50E
		public ListItem Item
		{
			get
			{
				return this._listItem;
			}
			set
			{
				this._listItem = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000C328 File Offset: 0x0000A528
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0000C31F File Offset: 0x0000A51F
		public ComboBoxItemInsertLocation InsertLocation
		{
			get
			{
				return this._insertLocation;
			}
			set
			{
				this._insertLocation = value;
			}
		}

		// Token: 0x04000139 RID: 313
		private ListItem _listItem;

		// Token: 0x0400013A RID: 314
		private ComboBoxItemInsertLocation _insertLocation;
	}
}
