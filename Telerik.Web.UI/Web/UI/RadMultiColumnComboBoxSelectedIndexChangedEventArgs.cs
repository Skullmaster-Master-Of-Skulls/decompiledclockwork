using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200005D RID: 93
	public class RadMultiColumnComboBoxSelectedIndexChangedEventArgs : EventArgs
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00007745 File Offset: 0x00005945
		public RadMultiColumnComboBoxSelectedIndexChangedEventArgs(string text, string oldtext, string value, string oldValue)
		{
			this.Text = text;
			this.OldText = oldtext;
			this.Value = value;
			this.OldValue = oldValue;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000776A File Offset: 0x0000596A
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00007772 File Offset: 0x00005972
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000777B File Offset: 0x0000597B
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00007783 File Offset: 0x00005983
		public string OldText
		{
			get
			{
				return this._oldText;
			}
			set
			{
				this._oldText = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000778C File Offset: 0x0000598C
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00007794 File Offset: 0x00005994
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

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000779D File Offset: 0x0000599D
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x000077A5 File Offset: 0x000059A5
		public string OldValue
		{
			get
			{
				return this._oldValue;
			}
			set
			{
				this._oldValue = value;
			}
		}

		// Token: 0x04000057 RID: 87
		private string _text;

		// Token: 0x04000058 RID: 88
		private string _oldText;

		// Token: 0x04000059 RID: 89
		private string _value;

		// Token: 0x0400005A RID: 90
		private string _oldValue;
	}
}
