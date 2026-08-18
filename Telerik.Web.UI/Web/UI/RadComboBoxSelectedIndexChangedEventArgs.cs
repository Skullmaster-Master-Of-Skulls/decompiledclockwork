using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AEE RID: 6894
	public class RadComboBoxSelectedIndexChangedEventArgs : EventArgs
	{
		// Token: 0x06010AFF RID: 68351 RVA: 0x003B791F File Offset: 0x003B5B1F
		public RadComboBoxSelectedIndexChangedEventArgs(string text, string oldtext, string value, string oldValue)
		{
			this.Text = text;
			this.OldText = oldtext;
			this.Value = value;
			this.OldValue = oldValue;
		}

		// Token: 0x1700512E RID: 20782
		// (get) Token: 0x06010B00 RID: 68352 RVA: 0x003B7944 File Offset: 0x003B5B44
		// (set) Token: 0x06010B01 RID: 68353 RVA: 0x003B794C File Offset: 0x003B5B4C
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

		// Token: 0x1700512F RID: 20783
		// (get) Token: 0x06010B02 RID: 68354 RVA: 0x003B7955 File Offset: 0x003B5B55
		// (set) Token: 0x06010B03 RID: 68355 RVA: 0x003B795D File Offset: 0x003B5B5D
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

		// Token: 0x17005130 RID: 20784
		// (get) Token: 0x06010B04 RID: 68356 RVA: 0x003B7966 File Offset: 0x003B5B66
		// (set) Token: 0x06010B05 RID: 68357 RVA: 0x003B796E File Offset: 0x003B5B6E
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

		// Token: 0x17005131 RID: 20785
		// (get) Token: 0x06010B06 RID: 68358 RVA: 0x003B7977 File Offset: 0x003B5B77
		// (set) Token: 0x06010B07 RID: 68359 RVA: 0x003B797F File Offset: 0x003B5B7F
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

		// Token: 0x04004A78 RID: 19064
		private string _text;

		// Token: 0x04004A79 RID: 19065
		private string _oldText;

		// Token: 0x04004A7A RID: 19066
		private string _value;

		// Token: 0x04004A7B RID: 19067
		private string _oldValue;
	}
}
