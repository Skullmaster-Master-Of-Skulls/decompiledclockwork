using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001B6C RID: 7020
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class TreeViewClientState
	{
		// Token: 0x17005308 RID: 21256
		// (get) Token: 0x06011007 RID: 69639 RVA: 0x003C20E3 File Offset: 0x003C02E3
		// (set) Token: 0x06011008 RID: 69640 RVA: 0x003C20EB File Offset: 0x003C02EB
		public string[] ExpandedNodes
		{
			get
			{
				return this._expandedNodes;
			}
			set
			{
				this._expandedNodes = value;
			}
		}

		// Token: 0x17005309 RID: 21257
		// (get) Token: 0x06011009 RID: 69641 RVA: 0x003C20F4 File Offset: 0x003C02F4
		// (set) Token: 0x0601100A RID: 69642 RVA: 0x003C20FC File Offset: 0x003C02FC
		public string[] CollapsedNodes
		{
			get
			{
				return this._collapsedNodes;
			}
			set
			{
				this._collapsedNodes = value;
			}
		}

		// Token: 0x1700530A RID: 21258
		// (get) Token: 0x0601100B RID: 69643 RVA: 0x003C2105 File Offset: 0x003C0305
		// (set) Token: 0x0601100C RID: 69644 RVA: 0x003C210D File Offset: 0x003C030D
		public string[] CheckedNodes
		{
			get
			{
				return this._checkedNodes;
			}
			set
			{
				this._checkedNodes = value;
			}
		}

		// Token: 0x1700530B RID: 21259
		// (get) Token: 0x0601100D RID: 69645 RVA: 0x003C2116 File Offset: 0x003C0316
		// (set) Token: 0x0601100E RID: 69646 RVA: 0x003C211E File Offset: 0x003C031E
		public string[] SelectedNodes
		{
			get
			{
				return this._selectedNodes;
			}
			set
			{
				this._selectedNodes = value;
			}
		}

		// Token: 0x1700530C RID: 21260
		// (get) Token: 0x0601100F RID: 69647 RVA: 0x003C2127 File Offset: 0x003C0327
		// (set) Token: 0x06011010 RID: 69648 RVA: 0x003C212F File Offset: 0x003C032F
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

		// Token: 0x1700530D RID: 21261
		// (get) Token: 0x06011011 RID: 69649 RVA: 0x003C2138 File Offset: 0x003C0338
		// (set) Token: 0x06011012 RID: 69650 RVA: 0x003C2140 File Offset: 0x003C0340
		public int ScrollPosition
		{
			get
			{
				return this._scrollPosition;
			}
			set
			{
				this._scrollPosition = value;
			}
		}

		// Token: 0x04004C0A RID: 19466
		private string[] _expandedNodes;

		// Token: 0x04004C0B RID: 19467
		private string[] _collapsedNodes;

		// Token: 0x04004C0C RID: 19468
		private string[] _checkedNodes;

		// Token: 0x04004C0D RID: 19469
		private string[] _selectedNodes;

		// Token: 0x04004C0E RID: 19470
		private ClientStateLogEntry[] _logEntries;

		// Token: 0x04004C0F RID: 19471
		private int _scrollPosition;
	}
}
