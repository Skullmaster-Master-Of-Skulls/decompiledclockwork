using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001AF1 RID: 6897
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadComboBoxClientState
	{
		// Token: 0x1700513B RID: 20795
		// (get) Token: 0x06010B1C RID: 68380 RVA: 0x003B7A5F File Offset: 0x003B5C5F
		// (set) Token: 0x06010B1D RID: 68381 RVA: 0x003B7A67 File Offset: 0x003B5C67
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

		// Token: 0x1700513C RID: 20796
		// (get) Token: 0x06010B1E RID: 68382 RVA: 0x003B7A70 File Offset: 0x003B5C70
		// (set) Token: 0x06010B1F RID: 68383 RVA: 0x003B7A78 File Offset: 0x003B5C78
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

		// Token: 0x1700513D RID: 20797
		// (get) Token: 0x06010B20 RID: 68384 RVA: 0x003B7A81 File Offset: 0x003B5C81
		// (set) Token: 0x06010B21 RID: 68385 RVA: 0x003B7A89 File Offset: 0x003B5C89
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

		// Token: 0x1700513E RID: 20798
		// (get) Token: 0x06010B22 RID: 68386 RVA: 0x003B7A92 File Offset: 0x003B5C92
		// (set) Token: 0x06010B23 RID: 68387 RVA: 0x003B7A9A File Offset: 0x003B5C9A
		public string EmptyMessage
		{
			get
			{
				return this._emptyMessage;
			}
			set
			{
				this._emptyMessage = value;
			}
		}

		// Token: 0x1700513F RID: 20799
		// (get) Token: 0x06010B24 RID: 68388 RVA: 0x003B7AA3 File Offset: 0x003B5CA3
		// (set) Token: 0x06010B25 RID: 68389 RVA: 0x003B7AAB File Offset: 0x003B5CAB
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}

		// Token: 0x17005140 RID: 20800
		// (get) Token: 0x06010B26 RID: 68390 RVA: 0x003B7AB4 File Offset: 0x003B5CB4
		// (set) Token: 0x06010B27 RID: 68391 RVA: 0x003B7ABC File Offset: 0x003B5CBC
		public int[] CheckedIndices { get; set; }

		// Token: 0x17005141 RID: 20801
		// (get) Token: 0x06010B28 RID: 68392 RVA: 0x003B7AC5 File Offset: 0x003B5CC5
		// (set) Token: 0x06010B29 RID: 68393 RVA: 0x003B7ACD File Offset: 0x003B5CCD
		public bool CheckedItemsTextOverflows
		{
			get
			{
				return this._checkedItemsTextOverflows;
			}
			set
			{
				this._checkedItemsTextOverflows = value;
			}
		}

		// Token: 0x04004A83 RID: 19075
		private ClientStateLogEntry[] _logEntries;

		// Token: 0x04004A84 RID: 19076
		private string _text;

		// Token: 0x04004A85 RID: 19077
		private string _value;

		// Token: 0x04004A86 RID: 19078
		private string _emptyMessage;

		// Token: 0x04004A87 RID: 19079
		private bool _enabled;

		// Token: 0x04004A88 RID: 19080
		private bool _checkedItemsTextOverflows;
	}
}
