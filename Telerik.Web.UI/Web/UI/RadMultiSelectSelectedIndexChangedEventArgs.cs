using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000603 RID: 1539
	public class RadMultiSelectSelectedIndexChangedEventArgs : EventArgs
	{
		// Token: 0x0600377F RID: 14207 RVA: 0x000B77DE File Offset: 0x000B59DE
		public RadMultiSelectSelectedIndexChangedEventArgs(string text, string oldtext, string value, string oldValue)
		{
			this.Text = text;
			this.OldText = oldtext;
			this.Value = value;
			this.OldValue = oldValue;
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06003780 RID: 14208 RVA: 0x000B7803 File Offset: 0x000B5A03
		// (set) Token: 0x06003781 RID: 14209 RVA: 0x000B780B File Offset: 0x000B5A0B
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

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06003782 RID: 14210 RVA: 0x000B7814 File Offset: 0x000B5A14
		// (set) Token: 0x06003783 RID: 14211 RVA: 0x000B781C File Offset: 0x000B5A1C
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

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06003784 RID: 14212 RVA: 0x000B7825 File Offset: 0x000B5A25
		// (set) Token: 0x06003785 RID: 14213 RVA: 0x000B782D File Offset: 0x000B5A2D
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

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06003786 RID: 14214 RVA: 0x000B7836 File Offset: 0x000B5A36
		// (set) Token: 0x06003787 RID: 14215 RVA: 0x000B783E File Offset: 0x000B5A3E
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

		// Token: 0x04000EE1 RID: 3809
		private string _text;

		// Token: 0x04000EE2 RID: 3810
		private string _oldText;

		// Token: 0x04000EE3 RID: 3811
		private string _value;

		// Token: 0x04000EE4 RID: 3812
		private string _oldValue;
	}
}
