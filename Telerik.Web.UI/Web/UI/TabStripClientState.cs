using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001ADF RID: 6879
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class TabStripClientState
	{
		// Token: 0x17005122 RID: 20770
		// (get) Token: 0x06010AC7 RID: 68295 RVA: 0x003B75AD File Offset: 0x003B57AD
		// (set) Token: 0x06010AC8 RID: 68296 RVA: 0x003B75B5 File Offset: 0x003B57B5
		public string[] SelectedIndexes
		{
			get
			{
				return this._selectedIndexes;
			}
			set
			{
				this._selectedIndexes = value;
			}
		}

		// Token: 0x17005123 RID: 20771
		// (get) Token: 0x06010AC9 RID: 68297 RVA: 0x003B75BE File Offset: 0x003B57BE
		// (set) Token: 0x06010ACA RID: 68298 RVA: 0x003B75C6 File Offset: 0x003B57C6
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

		// Token: 0x17005124 RID: 20772
		// (get) Token: 0x06010ACB RID: 68299 RVA: 0x003B75CF File Offset: 0x003B57CF
		// (set) Token: 0x06010ACC RID: 68300 RVA: 0x003B75D7 File Offset: 0x003B57D7
		public IDictionary<string, int> ScrollState
		{
			get
			{
				return this._scrollState;
			}
			set
			{
				this._scrollState = value;
			}
		}

		// Token: 0x04004A62 RID: 19042
		private string[] _selectedIndexes;

		// Token: 0x04004A63 RID: 19043
		private ClientStateLogEntry[] _logEntries;

		// Token: 0x04004A64 RID: 19044
		private IDictionary<string, int> _scrollState;
	}
}
