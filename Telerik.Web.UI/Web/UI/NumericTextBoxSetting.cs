using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001918 RID: 6424
	public class NumericTextBoxSetting : InputSetting, IRadNumericTextBox
	{
		// Token: 0x17004B46 RID: 19270
		// (get) Token: 0x0600F940 RID: 63808 RVA: 0x003848F8 File Offset: 0x00382AF8
		protected internal virtual NumberFormatSettings NumberFormat
		{
			get
			{
				if (this.numberFormat == null)
				{
					this.numberFormat = new NumberFormatSettings(this, base.ViewState);
				}
				return this.numberFormat;
			}
		}

		// Token: 0x17004B47 RID: 19271
		// (get) Token: 0x0600F941 RID: 63809 RVA: 0x0038491A File Offset: 0x00382B1A
		// (set) Token: 0x0600F942 RID: 63810 RVA: 0x00384927 File Offset: 0x00382B27
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Description("Gets or sets the string to use as the decimal separator in values.")]
		public string DecimalSeparator
		{
			get
			{
				return this.NumberFormat.DecimalSeparator;
			}
			set
			{
				this.NumberFormat.DecimalSeparator = value;
			}
		}

		// Token: 0x17004B48 RID: 19272
		// (get) Token: 0x0600F943 RID: 63811 RVA: 0x00384935 File Offset: 0x00382B35
		// (set) Token: 0x0600F944 RID: 63812 RVA: 0x00384942 File Offset: 0x00382B42
		[Description("Gets or sets the number of decimal places to use in values")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		public int DecimalDigits
		{
			get
			{
				return this.NumberFormat.DecimalDigits;
			}
			set
			{
				this.NumberFormat.DecimalDigits = value;
			}
		}

		// Token: 0x17004B49 RID: 19273
		// (get) Token: 0x0600F945 RID: 63813 RVA: 0x00384950 File Offset: 0x00382B50
		// (set) Token: 0x0600F946 RID: 63814 RVA: 0x0038495D File Offset: 0x00382B5D
		[Description("Gets or sets the number of digits in each group to the left of the decimal in values.")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		public int GroupSizes
		{
			get
			{
				return this.NumberFormat.GroupSizes;
			}
			set
			{
				this.NumberFormat.GroupSizes = value;
			}
		}

		// Token: 0x17004B4A RID: 19274
		// (get) Token: 0x0600F947 RID: 63815 RVA: 0x0038496B File Offset: 0x00382B6B
		// (set) Token: 0x0600F948 RID: 63816 RVA: 0x00384978 File Offset: 0x00382B78
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the string that separates groups of digits to the left of the decimal in values.")]
		public string GroupSeparator
		{
			get
			{
				return this.NumberFormat.GroupSeparator;
			}
			set
			{
				this.NumberFormat.GroupSeparator = value;
			}
		}

		// Token: 0x17004B4B RID: 19275
		// (get) Token: 0x0600F949 RID: 63817 RVA: 0x00384986 File Offset: 0x00382B86
		// (set) Token: 0x0600F94A RID: 63818 RVA: 0x00384993 File Offset: 0x00382B93
		[ClientControlProperty]
		[Description("Gets or sets the format pattern for negative numeric values.")]
		[NotifyParentProperty(true)]
		public string NegativePattern
		{
			get
			{
				return this.NumberFormat.NegativePattern;
			}
			set
			{
				this.NumberFormat.NegativePattern = value;
			}
		}

		// Token: 0x17004B4C RID: 19276
		// (get) Token: 0x0600F94B RID: 63819 RVA: 0x003849A1 File Offset: 0x00382BA1
		// (set) Token: 0x0600F94C RID: 63820 RVA: 0x003849AE File Offset: 0x00382BAE
		[ClientControlProperty]
		[Description("Gets or sets the format pattern for positive values.")]
		[NotifyParentProperty(true)]
		public string PositivePattern
		{
			get
			{
				return this.NumberFormat.PositivePattern;
			}
			set
			{
				this.NumberFormat.PositivePattern = value;
			}
		}

		// Token: 0x17004B4D RID: 19277
		// (get) Token: 0x0600F94D RID: 63821 RVA: 0x003849BC File Offset: 0x00382BBC
		// (set) Token: 0x0600F94E RID: 63822 RVA: 0x003849C9 File Offset: 0x00382BC9
		[Description("Gets or sets the format pattern for zero values.")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		public string ZeroPattern
		{
			get
			{
				return this.NumberFormat.ZeroPattern;
			}
			set
			{
				this.NumberFormat.ZeroPattern = value;
			}
		}

		// Token: 0x17004B4E RID: 19278
		// (get) Token: 0x0600F94F RID: 63823 RVA: 0x003849D7 File Offset: 0x00382BD7
		// (set) Token: 0x0600F950 RID: 63824 RVA: 0x003849E4 File Offset: 0x00382BE4
		[ClientControlProperty]
		[Description("Gets or sets the value that indicates whether the value will be rounded.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool AllowRounding
		{
			get
			{
				return this.NumberFormat.AllowRounding;
			}
			set
			{
				this.NumberFormat.AllowRounding = value;
			}
		}

		// Token: 0x17004B4F RID: 19279
		// (get) Token: 0x0600F951 RID: 63825 RVA: 0x003849F2 File Offset: 0x00382BF2
		// (set) Token: 0x0600F952 RID: 63826 RVA: 0x003849FF File Offset: 0x00382BFF
		[Description("Gets or sets whether the control will keep its trailing zeros (according to the DecimalDigits setting) when focused")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool KeepTrailingZerosOnFocus
		{
			get
			{
				return this.NumberFormat.KeepTrailingZerosOnFocus;
			}
			set
			{
				this.NumberFormat.KeepTrailingZerosOnFocus = value;
			}
		}

		// Token: 0x17004B50 RID: 19280
		// (get) Token: 0x0600F953 RID: 63827 RVA: 0x00384A0D File Offset: 0x00382C0D
		// (set) Token: 0x0600F954 RID: 63828 RVA: 0x00384A41 File Offset: 0x00382C41
		[NotifyParentProperty(true)]
		[Description("Culture used by RadNumericTextBox to format the numburs or currency.")]
		[Browsable(true)]
		[Category("Behavior")]
		public CultureInfo Culture
		{
			get
			{
				if (base.ViewState["Culture"] == null)
				{
					return Thread.CurrentThread.CurrentCulture;
				}
				return (CultureInfo)base.ViewState["Culture"];
			}
			set
			{
				base.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17004B51 RID: 19281
		// (get) Token: 0x0600F955 RID: 63829 RVA: 0x00384A54 File Offset: 0x00382C54
		// (set) Token: 0x0600F956 RID: 63830 RVA: 0x00384A87 File Offset: 0x00382C87
		[NotifyParentProperty(true)]
		[DefaultValue(70368744177664.0)]
		[Category("Behavior")]
		[Description("Gets or sets the largest possible value of a RadNumericTextBox.")]
		public virtual double MaxValue
		{
			get
			{
				if (base.ViewState["MaxValue"] == null)
				{
					return 70368744177664.0;
				}
				return (double)base.ViewState["MaxValue"];
			}
			set
			{
				base.ViewState["MaxValue"] = value;
			}
		}

		// Token: 0x17004B52 RID: 19282
		// (get) Token: 0x0600F957 RID: 63831 RVA: 0x00384A9F File Offset: 0x00382C9F
		// (set) Token: 0x0600F958 RID: 63832 RVA: 0x00384AD2 File Offset: 0x00382CD2
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(-70368744177664.0)]
		[Description("Gets or sets the smallest possible value of a RadNumericTextBox.")]
		public virtual double MinValue
		{
			get
			{
				if (base.ViewState["MinValue"] == null)
				{
					return -70368744177664.0;
				}
				return (double)base.ViewState["MinValue"];
			}
			set
			{
				base.ViewState["MinValue"] = value;
			}
		}

		// Token: 0x17004B53 RID: 19283
		// (get) Token: 0x0600F959 RID: 63833 RVA: 0x00384AEA File Offset: 0x00382CEA
		// (set) Token: 0x0600F95A RID: 63834 RVA: 0x00384B16 File Offset: 0x00382D16
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Time, in milliseconds, the InvalidStyle should be displayd. Must be a positive integer.")]
		[DefaultValue(100)]
		public virtual int InvalidStyleDuration
		{
			get
			{
				if (base.ViewState["InvalidStyleDuration"] == null)
				{
					return 100;
				}
				return (int)base.ViewState["InvalidStyleDuration"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("InvalidStyleDuration", "Must be a positive integer.");
				}
				base.ViewState["InvalidStyleDuration"] = value;
			}
		}

		// Token: 0x17004B54 RID: 19284
		// (get) Token: 0x0600F95B RID: 63835 RVA: 0x00384B42 File Offset: 0x00382D42
		// (set) Token: 0x0600F95C RID: 63836 RVA: 0x00384B6D File Offset: 0x00382D6D
		[DefaultValue(NumericType.Number)]
		[Description("The type of the RadNumericTextBox")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual NumericType Type
		{
			get
			{
				if (base.ViewState["Type"] == null)
				{
					return NumericType.Number;
				}
				return (NumericType)base.ViewState["Type"];
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x17004B55 RID: 19285
		// (get) Token: 0x0600F95D RID: 63837 RVA: 0x00384B85 File Offset: 0x00382D85
		// (set) Token: 0x0600F95E RID: 63838 RVA: 0x00384BB4 File Offset: 0x00382DB4
		[DefaultValue("")]
		[Description("The CSS style applied to control when is negative.")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public virtual string NegativeCssClass
		{
			get
			{
				if (base.ViewState["NegativeCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["NegativeCssClass"];
			}
			set
			{
				base.ViewState["NegativeCssClass"] = value;
			}
		}

		// Token: 0x0600F95F RID: 63839 RVA: 0x00384BC8 File Offset: 0x00382DC8
		internal override void Describe(IScriptDescriptor descriptor)
		{
			base.Describe(descriptor);
			if (this.MaxValue != 70368744177664.0)
			{
				descriptor.AddProperty("maxValue", this.MaxValue);
			}
			if (this.MinValue != -70368744177664.0)
			{
				descriptor.AddProperty("minValue", this.MinValue);
			}
			if (this.NegativeCssClass != "RadInputMgr RadInputMgr_Default RadInput_Negative_Default")
			{
				descriptor.AddProperty("negativeCss", this.NegativeCssClass);
			}
			if (this.InvalidStyleDuration != 100)
			{
				descriptor.AddProperty("invalidStyleDuration", this.InvalidStyleDuration);
			}
		}

		// Token: 0x0600F960 RID: 63840 RVA: 0x00384C70 File Offset: 0x00382E70
		public override void Validate(TextBox input, object context)
		{
			base.Validate(input, context);
			if (this.IsValid)
			{
				if (!string.IsNullOrEmpty(input.Text) && input.Text != this.EmptyMessage)
				{
					string text = input.Text;
					if (this.Type == NumericType.Percent)
					{
						text = text.Replace("%", string.Empty);
					}
					double num;
					bool flag = double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out num);
					if (!flag)
					{
						flag = double.TryParse(text, NumberStyles.Any, this.Culture, out num);
					}
					if (!flag)
					{
						this._isValid = false;
						this.invalidIds.Add(input.ID);
					}
					else if (num > this.MaxValue || num < this.MinValue)
					{
						this._isValid = false;
						this.invalidIds.Add(input.ID);
					}
				}
				this.UpdateValue(input, false);
			}
		}

		// Token: 0x0600F961 RID: 63841 RVA: 0x00384D50 File Offset: 0x00382F50
		public override void Validate(TextBox input)
		{
			this.Validate(input, null);
		}

		// Token: 0x0600F962 RID: 63842 RVA: 0x00384D5C File Offset: 0x00382F5C
		internal override void UpdateValue(TextBox input, bool shouldFormat)
		{
			if (!string.IsNullOrEmpty(input.Text) && input.Text != this.EmptyMessage)
			{
				double num = InputUtil.ParseDouble(input, this.Culture, this.Type);
				if (num > this.MaxValue || num < this.MinValue)
				{
					input.Text = "";
				}
				else if (shouldFormat)
				{
					input.Text = InputUtil.FormatDouble(num, this.NumberFormat);
				}
			}
			base.UpdateValue(input, shouldFormat);
		}

		// Token: 0x0600F963 RID: 63843 RVA: 0x00384DD8 File Offset: 0x00382FD8
		internal override bool IsNegative(TextBox input)
		{
			if (!string.IsNullOrEmpty(input.Text))
			{
				string text = input.Text;
				double num = InputUtil.ParseDouble(input, this.Culture, this.Type);
				return num < 0.0;
			}
			return false;
		}

		// Token: 0x040046E4 RID: 18148
		private NumberFormatSettings numberFormat;
	}
}
