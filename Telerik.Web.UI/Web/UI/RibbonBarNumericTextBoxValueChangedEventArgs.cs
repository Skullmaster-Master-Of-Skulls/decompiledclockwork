using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E2D RID: 3629
	public class RibbonBarNumericTextBoxValueChangedEventArgs : EventArgs
	{
		// Token: 0x17002B7C RID: 11132
		// (get) Token: 0x06008972 RID: 35186 RVA: 0x001F5C50 File Offset: 0x001F3E50
		public RibbonBarNumericTextBox NumericTextBox
		{
			get
			{
				return this._numericTextBox;
			}
		}

		// Token: 0x17002B7D RID: 11133
		// (get) Token: 0x06008973 RID: 35187 RVA: 0x001F5C58 File Offset: 0x001F3E58
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002B7E RID: 11134
		// (get) Token: 0x06008974 RID: 35188 RVA: 0x001F5C60 File Offset: 0x001F3E60
		public double Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06008975 RID: 35189 RVA: 0x001F5C68 File Offset: 0x001F3E68
		public RibbonBarNumericTextBoxValueChangedEventArgs(double value, RibbonBarNumericTextBox numericTextBox, RibbonBarGroup group)
		{
			this._group = group;
			this._numericTextBox = numericTextBox;
			this._value = value;
		}

		// Token: 0x04002670 RID: 9840
		private RibbonBarGroup _group;

		// Token: 0x04002671 RID: 9841
		private RibbonBarNumericTextBox _numericTextBox;

		// Token: 0x04002672 RID: 9842
		private double _value;
	}
}
