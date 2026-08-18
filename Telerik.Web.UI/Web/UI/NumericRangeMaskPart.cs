using System;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI
{
	// Token: 0x020012BB RID: 4795
	[ClientScriptResource("Telerik.Web.UI.Input.MaskedTextBox.MaskParts.NumericRangeMaskPart", "Telerik.Web.UI.Input.MaskedTextBox.MaskParts.RadNumericRangeMaskPart.js")]
	public class NumericRangeMaskPart : MaskPart
	{
		// Token: 0x170040DB RID: 16603
		// (get) Token: 0x0600C8B5 RID: 51381 RVA: 0x002CC220 File Offset: 0x002CA420
		// (set) Token: 0x0600C8B6 RID: 51382 RVA: 0x002CC288 File Offset: 0x002CA488
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
				int num = int.Parse(value);
				num = Math.Min(this._upperLimit, num);
				num = Math.Max(this._lowerLimit, num);
				this._intValue = num;
			}
		}

		// Token: 0x170040DC RID: 16604
		// (get) Token: 0x0600C8B7 RID: 51383 RVA: 0x002CC2BD File Offset: 0x002CA4BD
		// (set) Token: 0x0600C8B8 RID: 51384 RVA: 0x002CC2C5 File Offset: 0x002CA4C5
		public int LowerLimit
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

		// Token: 0x170040DD RID: 16605
		// (get) Token: 0x0600C8B9 RID: 51385 RVA: 0x002CC2CE File Offset: 0x002CA4CE
		// (set) Token: 0x0600C8BA RID: 51386 RVA: 0x002CC2D6 File Offset: 0x002CA4D6
		public int UpperLimit
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

		// Token: 0x0600C8BB RID: 51387 RVA: 0x002CC2DF File Offset: 0x002CA4DF
		public override string ToString()
		{
			return "Numeric Range";
		}

		// Token: 0x170040DE RID: 16606
		// (get) Token: 0x0600C8BC RID: 51388 RVA: 0x002CC2E6 File Offset: 0x002CA4E6
		internal override int PromptLength
		{
			get
			{
				return Math.Max(this._lowerLimit.ToString().Length, this._upperLimit.ToString().Length);
			}
		}

		// Token: 0x170040DF RID: 16607
		// (get) Token: 0x0600C8BD RID: 51389 RVA: 0x002CC310 File Offset: 0x002CA510
		internal override string Part
		{
			get
			{
				return string.Concat(new string[]
				{
					"<",
					this._lowerLimit.ToString(),
					"..",
					this._upperLimit.ToString(),
					">"
				});
			}
		}

		// Token: 0x170040E0 RID: 16608
		// (get) Token: 0x0600C8BE RID: 51390 RVA: 0x002CC360 File Offset: 0x002CA560
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
				if (this._intValue >= 0)
				{
					return this.Value.PadLeft(this.PromptLength, '0');
				}
				return "-" + Math.Abs(this._intValue).ToString().PadLeft(this.PromptLength, base.PromptChar[0]);
			}
		}

		// Token: 0x170040E1 RID: 16609
		// (get) Token: 0x0600C8BF RID: 51391 RVA: 0x002CC40C File Offset: 0x002CA60C
		internal override string InitScript
		{
			get
			{
				return string.Format("new Telerik.Web.UI.RadNumericRangeMaskPart({0}, {1}, {2}, {3})", new object[]
				{
					this._lowerLimit,
					this._upperLimit,
					(base.NumericRangeAlign == NumericRangeAlign.Left).ToString().ToLower(),
					base.ZeroPadNumericRanges.ToString().ToLower()
				});
			}
		}

		// Token: 0x0600C8C0 RID: 51392 RVA: 0x002CC478 File Offset: 0x002CA678
		internal override int SetValue(string value)
		{
			value = value.Replace(base.PromptChar, "");
			string text = "";
			int num = this.PromptLength;
			if (this._lowerLimit < 0)
			{
				if (this._upperLimit < 0)
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
			this._intValue = int.Parse(value);
			return value.Length;
		}

		// Token: 0x040034D1 RID: 13521
		private int _lowerLimit;

		// Token: 0x040034D2 RID: 13522
		private int _upperLimit;

		// Token: 0x040034D3 RID: 13523
		private int _intValue;
	}
}
