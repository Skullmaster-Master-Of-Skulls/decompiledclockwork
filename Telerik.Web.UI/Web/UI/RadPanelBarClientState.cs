using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001B49 RID: 6985
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadPanelBarClientState
	{
		// Token: 0x17005259 RID: 21081
		// (get) Token: 0x06010E24 RID: 69156 RVA: 0x003BE419 File Offset: 0x003BC619
		// (set) Token: 0x06010E25 RID: 69157 RVA: 0x003BE421 File Offset: 0x003BC621
		public ClientStateLogEntry[] LogEntries
		{
			get
			{
				return this._logEntries;
			}
			set
			{
				this._logEntries = value;
			}
		}

		// Token: 0x1700525A RID: 21082
		// (get) Token: 0x06010E26 RID: 69158 RVA: 0x003BE42A File Offset: 0x003BC62A
		// (set) Token: 0x06010E27 RID: 69159 RVA: 0x003BE432 File Offset: 0x003BC632
		public string[] ExpandedItems
		{
			get
			{
				return this._expandedItems;
			}
			set
			{
				this._expandedItems = value;
			}
		}

		// Token: 0x1700525B RID: 21083
		// (get) Token: 0x06010E28 RID: 69160 RVA: 0x003BE43B File Offset: 0x003BC63B
		// (set) Token: 0x06010E29 RID: 69161 RVA: 0x003BE443 File Offset: 0x003BC643
		public string[] SelectedItems
		{
			get
			{
				return this._selectedItems;
			}
			set
			{
				this._selectedItems = value;
			}
		}

		// Token: 0x04004B94 RID: 19348
		private ClientStateLogEntry[] _logEntries;

		// Token: 0x04004B95 RID: 19349
		private string[] _expandedItems;

		// Token: 0x04004B96 RID: 19350
		private string[] _selectedItems;
	}
}
