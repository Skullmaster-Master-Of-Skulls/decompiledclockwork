using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020009BC RID: 2492
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class AutoCompletePostBackArguments
	{
		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x06005F42 RID: 24386 RVA: 0x001226A8 File Offset: 0x001208A8
		// (set) Token: 0x06005F43 RID: 24387 RVA: 0x001226B0 File Offset: 0x001208B0
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

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x06005F44 RID: 24388 RVA: 0x001226B9 File Offset: 0x001208B9
		// (set) Token: 0x06005F45 RID: 24389 RVA: 0x001226C1 File Offset: 0x001208C1
		public string Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x06005F46 RID: 24390 RVA: 0x001226CA File Offset: 0x001208CA
		// (set) Token: 0x06005F47 RID: 24391 RVA: 0x001226D2 File Offset: 0x001208D2
		public AutoCompleteBoxClientState ClientState
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

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x06005F48 RID: 24392 RVA: 0x001226DB File Offset: 0x001208DB
		// (set) Token: 0x06005F49 RID: 24393 RVA: 0x001226E3 File Offset: 0x001208E3
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

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x06005F4A RID: 24394 RVA: 0x001226EC File Offset: 0x001208EC
		// (set) Token: 0x06005F4B RID: 24395 RVA: 0x001226F4 File Offset: 0x001208F4
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

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x06005F4C RID: 24396 RVA: 0x001226FD File Offset: 0x001208FD
		// (set) Token: 0x06005F4D RID: 24397 RVA: 0x00122705 File Offset: 0x00120905
		public Dictionary<string, object> Attributes
		{
			get
			{
				return this._attributes;
			}
			set
			{
				this._attributes = value;
			}
		}

		// Token: 0x040016F2 RID: 5874
		private string _command;

		// Token: 0x040016F3 RID: 5875
		private string _index;

		// Token: 0x040016F4 RID: 5876
		private AutoCompleteBoxClientState _clientState;

		// Token: 0x040016F5 RID: 5877
		private string _text;

		// Token: 0x040016F6 RID: 5878
		private string _value;

		// Token: 0x040016F7 RID: 5879
		private Dictionary<string, object> _attributes;
	}
}
