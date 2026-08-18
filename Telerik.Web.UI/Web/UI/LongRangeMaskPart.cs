using System;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI
{
	// Token: 0x0200055F RID: 1375
	[ClientScriptResource("Telerik.Web.UI.Input.MaskedTextBox.MaskParts.LongRangeMaskPart", "Telerik.Web.UI.Input.MaskedTextBox.MaskParts.RadLongRangeMaskPart.js")]
	public class LongRangeMaskPart : MaskPart
	{
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x000A2C6C File Offset: 0x000A0E6C
		// (set) Token: 0x06003194 RID: 12692 RVA: 0x000A2CD4 File Offset: 0x000A0ED4
		public override string Value
		{
			get
			{
				if (base.ZeroPadNumericRanges)
				{
					this._intValue = Math.Max(this._intValue, this.LowerLimit);
					this._intValue = Math.Min(this._intValue, this.UpperLimit);
					return this._intValue.ToString().PadLeft(this.PromptLength, '0');
				}
				return this._intValue.ToString();
			}
			set
			{
				long num = long.Parse(value);
				num = Math.Min(this._upperLimit, num);
				num = Math.Max(this._lowerLimit, num);
				this._intValue = num;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000A2D09 File Offset: 0x000A0F09
		// (set) Token: 0x06003196 RID: 12694 RVA: 0x000A2D11 File Offset: 0x000A0F11
		public long LowerLimit
		{
			get
			{
				return this._lowerLimit;
			}
			set
			{
				this._lowerLimit = value;
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000A2D1A File Offset: 0x000A0F1A
		// (set) Token: 0x06003198 RID: 12696 RVA: 0x000A2D22 File Offset: 0x000A0F22
		public long UpperLimit
		{
			get
			{
				return this._upperLimit;
			}
			set
			{
				this._upperLimit = value;
			}
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000A2D2B File Offset: 0x000A0F2B
		public override string ToString()
		{
			return "Long Range";
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000A2D32 File Offset: 0x000A0F32
		internal override int PromptLength
		{
			get
			{
				return Math.Max(this._lowerLimit.ToString().Length, this._upperLimit.ToString().Length);
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000A2D5C File Offset: 0x000A0F5C
		internal override string Part
		{
			get
			{
				return string.Concat(new string[]
				{
					"<",
					this._lowerLimit.ToString(),
					"...",
					this._upperLimit.ToString(),
					">"
				});
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000A2DAC File Offset: 0x000A0FAC
		internal override string Prompt
		{
			get
			{
				if (base.NumericRangeAlign == NumericRangeAlign.Left)
				{
					return this.Value.PadRight(this.PromptLength, base.PromptChar[0]);
				}
				if (base.ZeroPadNumericRanges)
				{
					return this.Value.PadLeft(this.PromptLength, base.PromptChar[0]);
				}
				if (this._intValue >= 0L)
				{
					return this.Value.PadLeft(this.PromptLength, '0');
				}
				return "-" + Math.Abs(this._intValue).ToString().PadLeft(this.PromptLength, base.PromptChar[0]);
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000A2E58 File Offset: 0x000A1058
		internal override string InitScript
		{
			get
			{
				return string.Format("new Telerik.Web.UI.RadLongRangeMaskPart(\"{0}\", \"{1}\", {2}, {3})", new object[]
				{
					this._lowerLimit,
					this._upperLimit,
					(base.NumericRangeAlign == NumericRangeAlign.Left).ToString().ToLower(),
					base.ZeroPadNumericRanges.ToString().ToLower()
				});
			}
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000A2EC4 File Offset: 0x000A10C4
		internal override int SetValue(string value)
		{
			value = value.Replace(base.PromptChar, "");
			string text = "";
			int num = this.PromptLength;
			if (this._lowerLimit < 0L)
			{
				if (this._upperLimit < 0L)
				{
					text = "-{1}";
					num--;
				}
				else
				{
					text = "-?";
				}
			}
			string pattern = string.Concat(new object[]
			{
				"^",
				text,
				"\\d{1,",
				num,
				"}$"
			});
			Regex regex = new Regex(pattern);
			while (!regex.Match(value).Success)
			{
				if (string.IsNullOrEmpty(value))
				{
					return 0;
				}
				value = value.Substring(0, value.Length - 1);
			}
			this._intValue = long.Parse(value);
			return value.Length;
		}

		// Token: 0x04000D66 RID: 3430
		private long _lowerLimit;

		// Token: 0x04000D67 RID: 3431
		private long _upperLimit;

		// Token: 0x04000D68 RID: 3432
		private long _intValue;
	}
}
