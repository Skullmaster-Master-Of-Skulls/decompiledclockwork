using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020009B5 RID: 2485
	[DataContract]
	[Serializable]
	public class AutoCompleteBoxItemData
	{
		// Token: 0x06005F32 RID: 24370 RVA: 0x00122614 File Offset: 0x00120814
		public AutoCompleteBoxItemData()
		{
			this.Text = string.Empty;
			this.Value = string.Empty;
			this.Enabled = true;
			this._attributes = new Dictionary<string, object>();
		}

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x06005F33 RID: 24371 RVA: 0x00122644 File Offset: 0x00120844
		// (set) Token: 0x06005F34 RID: 24372 RVA: 0x0012264C File Offset: 0x0012084C
		[DataMember]
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

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x06005F35 RID: 24373 RVA: 0x00122655 File Offset: 0x00120855
		// (set) Token: 0x06005F36 RID: 24374 RVA: 0x0012265D File Offset: 0x0012085D
		[DataMember]
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

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x06005F37 RID: 24375 RVA: 0x00122666 File Offset: 0x00120866
		// (set) Token: 0x06005F38 RID: 24376 RVA: 0x0012266E File Offset: 0x0012086E
		[DataMember]
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

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x06005F39 RID: 24377 RVA: 0x00122677 File Offset: 0x00120877
		// (set) Token: 0x06005F3A RID: 24378 RVA: 0x0012267F File Offset: 0x0012087F
		[DataMember]
		public IDictionary<string, object> Attributes
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

		// Token: 0x040016E1 RID: 5857
		private string _text;

		// Token: 0x040016E2 RID: 5858
		private string _value;

		// Token: 0x040016E3 RID: 5859
		private bool _enabled;

		// Token: 0x040016E4 RID: 5860
		private IDictionary<string, object> _attributes;
	}
}
