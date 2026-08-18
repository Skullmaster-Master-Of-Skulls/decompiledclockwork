using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Web.UI.Timeline;

namespace Telerik.Web.UI
{
	// Token: 0x0200092B RID: 2347
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class TimelinePostBackArguments
	{
		// Token: 0x17001D5D RID: 7517
		// (get) Token: 0x06005919 RID: 22809 RVA: 0x0010FE7D File Offset: 0x0010E07D
		// (set) Token: 0x0600591A RID: 22810 RVA: 0x0010FE85 File Offset: 0x0010E085
		public string Command
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x0600591B RID: 22811 RVA: 0x0010FE8E File Offset: 0x0010E08E
		// (set) Token: 0x0600591C RID: 22812 RVA: 0x0010FE96 File Offset: 0x0010E096
		public RadTimelineClientState ClientState
		{
			get
			{
				return this._clientState;
			}
			set
			{
				this._clientState = value;
			}
		}

		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x0010FE9F File Offset: 0x0010E09F
		// (set) Token: 0x0600591E RID: 22814 RVA: 0x0010FEA7 File Offset: 0x0010E0A7
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x17001D60 RID: 7520
		// (get) Token: 0x0600591F RID: 22815 RVA: 0x0010FEB0 File Offset: 0x0010E0B0
		// (set) Token: 0x06005920 RID: 22816 RVA: 0x0010FEB8 File Offset: 0x0010E0B8
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x06005921 RID: 22817 RVA: 0x0010FEC1 File Offset: 0x0010E0C1
		// (set) Token: 0x06005922 RID: 22818 RVA: 0x0010FEC9 File Offset: 0x0010E0C9
		public Dictionary<string, object> DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x040015A1 RID: 5537
		private string _command;

		// Token: 0x040015A2 RID: 5538
		private RadTimelineClientState _clientState;

		// Token: 0x040015A3 RID: 5539
		private string _text;

		// Token: 0x040015A4 RID: 5540
		private string _value;

		// Token: 0x040015A5 RID: 5541
		private Dictionary<string, object> _dataItem;
	}
}
