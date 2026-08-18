using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200055E RID: 1374
	[ClientScriptResource("Telerik.Web.UI.RadMaskedTextBox", "Telerik.Web.UI.Input.MaskedTextBox.MaskParts.RadBaseMaskPart.js")]
	public abstract class MaskPart
	{
		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000A2B49 File Offset: 0x000A0D49
		// (set) Token: 0x06003183 RID: 12675 RVA: 0x000A2B51 File Offset: 0x000A0D51
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public virtual string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value != this.Prompt)
				{
					this._value = value;
				}
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000A2B68 File Offset: 0x000A0D68
		internal virtual int PromptLength
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000A2B6B File Offset: 0x000A0D6B
		internal virtual int ValueLength
		{
			get
			{
				return this.PromptLength;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000A2B73 File Offset: 0x000A0D73
		internal string PromptChar
		{
			get
			{
				if (this.Input == null)
				{
					return "_";
				}
				if (string.IsNullOrEmpty(this.Input.PromptChar))
				{
					return " ";
				}
				return this.Input.PromptChar;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000A2BA6 File Offset: 0x000A0DA6
		internal bool AllowEmptyEnumerations
		{
			get
			{
				if (this.Input != null)
				{
					return this.Input.AllowEmptyEnumerations;
				}
				return this.MaskedTextBoxSetting != null && this.MaskedTextBoxSetting.AllowEmptyEnumerations;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000A2BD1 File Offset: 0x000A0DD1
		internal bool ZeroPadNumericRanges
		{
			get
			{
				if (this.Input != null)
				{
					return this.Input.ZeroPadNumericRanges;
				}
				return this.MaskedTextBoxSetting == null || this.MaskedTextBoxSetting.ZeroPadNumericRanges;
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000A2BFC File Offset: 0x000A0DFC
		internal NumericRangeAlign NumericRangeAlign
		{
			get
			{
				if (this.Input != null)
				{
					return this.Input.NumericRangeAlign;
				}
				if (this.MaskedTextBoxSetting != null)
				{
					return this.MaskedTextBoxSetting.NumericRangeAlign;
				}
				return NumericRangeAlign.Left;
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x0600318A RID: 12682
		internal abstract string InitScript { get; }

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x0600318B RID: 12683
		internal abstract string Part { get; }

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x0600318C RID: 12684
		internal abstract string Prompt { get; }

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000A2C27 File Offset: 0x000A0E27
		// (set) Token: 0x0600318E RID: 12686 RVA: 0x000A2C2F File Offset: 0x000A0E2F
		internal RadMaskedTextBox Input
		{
			get
			{
				return this._textBox;
			}
			set
			{
				this._textBox = value;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000A2C38 File Offset: 0x000A0E38
		// (set) Token: 0x06003190 RID: 12688 RVA: 0x000A2C40 File Offset: 0x000A0E40
		internal MaskedTextBoxSetting MaskedTextBoxSetting
		{
			get
			{
				return this._maskedTextBoxSetting;
			}
			set
			{
				this._maskedTextBoxSetting = value;
			}
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000A2C49 File Offset: 0x000A0E49
		internal virtual int SetValue(string value)
		{
			this.Value = value;
			return value.Length;
		}

		// Token: 0x04000D63 RID: 3427
		private string _value = string.Empty;

		// Token: 0x04000D64 RID: 3428
		private RadMaskedTextBox _textBox;

		// Token: 0x04000D65 RID: 3429
		private MaskedTextBoxSetting _maskedTextBoxSetting;
	}
}
