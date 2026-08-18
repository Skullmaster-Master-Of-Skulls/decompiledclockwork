using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020012BF RID: 4799
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class NumberFormatSettings : ICustomTypeDescriptor
	{
		// Token: 0x17004106 RID: 16646
		// (get) Token: 0x0600C913 RID: 51475 RVA: 0x002CD708 File Offset: 0x002CB908
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public InputStateBag ViewState
		{
			get
			{
				return this._viewStateOwner;
			}
		}

		// Token: 0x17004107 RID: 16647
		// (get) Token: 0x0600C914 RID: 51476 RVA: 0x002CD710 File Offset: 0x002CB910
		internal StateBag ViewStateOwner
		{
			get
			{
				return this._ownerStateBag;
			}
		}

		// Token: 0x0600C915 RID: 51477 RVA: 0x002CD718 File Offset: 0x002CB918
		public override string ToString()
		{
			return "";
		}

		// Token: 0x0600C916 RID: 51478 RVA: 0x002CD71F File Offset: 0x002CB91F
		public NumberFormatSettings(IRadNumericTextBox numericTextBox, StateBag viewStateOwner)
		{
			this._viewStateOwner = new InputStateBag("input_format_", viewStateOwner);
			this._ownerStateBag = viewStateOwner;
			this.numericTextBox = numericTextBox;
		}

		// Token: 0x17004108 RID: 16648
		// (get) Token: 0x0600C917 RID: 51479 RVA: 0x002CD748 File Offset: 0x002CB948
		// (set) Token: 0x0600C918 RID: 51480 RVA: 0x002CD7DC File Offset: 0x002CB9DC
		[Description("Gets or sets the string to use as the decimal separator in values.")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		public string DecimalSeparator
		{
			get
			{
				if (this.ViewState["DecimalSeparator"] != null)
				{
					return (string)this.ViewState["DecimalSeparator"];
				}
				switch (this.numericTextBox.Type)
				{
				case NumericType.Currency:
					return this.numericTextBox.Culture.NumberFormat.CurrencyDecimalSeparator;
				case NumericType.Percent:
					return this.numericTextBox.Culture.NumberFormat.PercentDecimalSeparator;
				default:
					return this.numericTextBox.Culture.NumberFormat.NumberDecimalSeparator;
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DecimalSeparator", "String reference not set to an instance of a String");
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("DecimalSeparator", "");
				}
				this.ViewState["DecimalSeparator"] = value;
			}
		}

		// Token: 0x17004109 RID: 16649
		// (get) Token: 0x0600C919 RID: 51481 RVA: 0x002CD81A File Offset: 0x002CBA1A
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Description("Gets the native decimal separator of the control's culture.")]
		public string CultureNativeDecimalSeparator
		{
			get
			{
				return this.numericTextBox.Culture.NumberFormat.NumberDecimalSeparator;
			}
		}

		// Token: 0x1700410A RID: 16650
		// (get) Token: 0x0600C91A RID: 51482 RVA: 0x002CD834 File Offset: 0x002CBA34
		// (set) Token: 0x0600C91B RID: 51483 RVA: 0x002CD8C8 File Offset: 0x002CBAC8
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the number of decimal places to use in values")]
		public int DecimalDigits
		{
			get
			{
				if (this.ViewState["DecimalDigits"] != null)
				{
					return (int)this.ViewState["DecimalDigits"];
				}
				switch (this.numericTextBox.Type)
				{
				case NumericType.Currency:
					return this.numericTextBox.Culture.NumberFormat.CurrencyDecimalDigits;
				case NumericType.Percent:
					return this.numericTextBox.Culture.NumberFormat.PercentDecimalDigits;
				default:
					return this.numericTextBox.Culture.NumberFormat.NumberDecimalDigits;
				}
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("DecimalDigits", "Valid values are between 0 and 99, inclusive.");
				}
				this.ViewState["DecimalDigits"] = value;
			}
		}

		// Token: 0x1700410B RID: 16651
		// (get) Token: 0x0600C91C RID: 51484 RVA: 0x002CD8FC File Offset: 0x002CBAFC
		// (set) Token: 0x0600C91D RID: 51485 RVA: 0x002CD996 File Offset: 0x002CBB96
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the number of digits in each group to the left of the decimal in values.")]
		public int GroupSizes
		{
			get
			{
				if (this.ViewState["GroupSizes"] != null)
				{
					return (int)this.ViewState["GroupSizes"];
				}
				switch (this.numericTextBox.Type)
				{
				case NumericType.Currency:
					return this.numericTextBox.Culture.NumberFormat.CurrencyGroupSizes[0];
				case NumericType.Percent:
					return this.numericTextBox.Culture.NumberFormat.PercentGroupSizes[0];
				default:
					return this.numericTextBox.Culture.NumberFormat.NumberGroupSizes[0];
				}
			}
			set
			{
				if (value < 1 || value > 9)
				{
					throw new ArgumentOutOfRangeException("GroupSizes", "The value should be between one and nine");
				}
				this.ViewState["GroupSizes"] = value;
			}
		}

		// Token: 0x1700410C RID: 16652
		// (get) Token: 0x0600C91E RID: 51486 RVA: 0x002CD9C8 File Offset: 0x002CBBC8
		// (set) Token: 0x0600C91F RID: 51487 RVA: 0x002CDA5C File Offset: 0x002CBC5C
		[ClientControlProperty]
		[Description("Gets or sets the string that separates groups of digits to the left of the decimal in values.")]
		[NotifyParentProperty(true)]
		public string GroupSeparator
		{
			get
			{
				if (this.ViewState["GroupSeparator"] != null)
				{
					return (string)this.ViewState["GroupSeparator"];
				}
				switch (this.numericTextBox.Type)
				{
				case NumericType.Currency:
					return this.numericTextBox.Culture.NumberFormat.CurrencyGroupSeparator;
				case NumericType.Percent:
					return this.numericTextBox.Culture.NumberFormat.PercentGroupSeparator;
				default:
					return this.numericTextBox.Culture.NumberFormat.NumberGroupSeparator;
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("GroupSeparator", "String reference not set to an instance of a String");
				}
				this.ViewState["GroupSeparator"] = value;
			}
		}

		// Token: 0x1700410D RID: 16653
		// (get) Token: 0x0600C920 RID: 51488 RVA: 0x002CDA84 File Offset: 0x002CBC84
		// (set) Token: 0x0600C921 RID: 51489 RVA: 0x002CDB1B File Offset: 0x002CBD1B
		[Description("Gets or sets the format pattern for negative numeric values.")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		public string NegativePattern
		{
			get
			{
				if (this.ViewState["NegativePattern"] != null)
				{
					return (string)this.ViewState["NegativePattern"];
				}
				switch (this.numericTextBox.Type)
				{
				case NumericType.Currency:
					return InputUtil.ToStringCurrencyNegativePattern(this.numericTextBox.Culture, this.NumericPlaceHolder);
				case NumericType.Percent:
					return InputUtil.ToStringPercentNegativePattern(this.numericTextBox.Culture, this.NumericPlaceHolder);
				default:
					return InputUtil.ToStringNumberNegativePattern(this.numericTextBox.Culture, this.NumericPlaceHolder);
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativePattern", "String reference not set to an instance of a String");
				}
				this.ViewState["NegativePattern"] = value;
			}
		}

		// Token: 0x1700410E RID: 16654
		// (get) Token: 0x0600C922 RID: 51490 RVA: 0x002CDB44 File Offset: 0x002CBD44
		// (set) Token: 0x0600C923 RID: 51491 RVA: 0x002CDBD0 File Offset: 0x002CBDD0
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the format pattern for positive values.")]
		public string PositivePattern
		{
			get
			{
				if (this.ViewState["PositivePattern"] != null)
				{
					return (string)this.ViewState["PositivePattern"];
				}
				switch (this.numericTextBox.Type)
				{
				case NumericType.Currency:
					return InputUtil.ToStringCurrencyPositivePattern(this.numericTextBox.Culture, this.NumericPlaceHolder);
				case NumericType.Percent:
					return InputUtil.ToStringPercentPositivePattern(this.numericTextBox.Culture, this.NumericPlaceHolder);
				default:
					return InputUtil.ToStringNumberPositivePattern(this.NumericPlaceHolder);
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositivePattern", "String reference not set to an instance of a String");
				}
				this.ViewState["PositivePattern"] = value;
			}
		}

		// Token: 0x1700410F RID: 16655
		// (get) Token: 0x0600C924 RID: 51492 RVA: 0x002CDBF6 File Offset: 0x002CBDF6
		// (set) Token: 0x0600C925 RID: 51493 RVA: 0x002CDC26 File Offset: 0x002CBE26
		[NotifyParentProperty(true)]
		[Description("Gets or sets the format pattern for zero values.")]
		[ClientControlProperty]
		public string ZeroPattern
		{
			get
			{
				if (this.ViewState["ZeroPattern"] != null)
				{
					return (string)this.ViewState["ZeroPattern"];
				}
				return this.PositivePattern;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ZeroPattern", "String reference not set to an instance of a String");
				}
				this.ViewState["ZeroPattern"] = value;
			}
		}

		// Token: 0x17004110 RID: 16656
		// (get) Token: 0x0600C926 RID: 51494 RVA: 0x002CDC4C File Offset: 0x002CBE4C
		// (set) Token: 0x0600C927 RID: 51495 RVA: 0x002CDC77 File Offset: 0x002CBE77
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the value that indicates whether the value will be rounded.")]
		[DefaultValue(true)]
		public bool AllowRounding
		{
			get
			{
				return this.ViewState["AllowRounding"] == null || (bool)this.ViewState["AllowRounding"];
			}
			set
			{
				this.ViewState["AllowRounding"] = value;
			}
		}

		// Token: 0x17004111 RID: 16657
		// (get) Token: 0x0600C928 RID: 51496 RVA: 0x002CDC8F File Offset: 0x002CBE8F
		// (set) Token: 0x0600C929 RID: 51497 RVA: 0x002CDCBA File Offset: 0x002CBEBA
		[NotifyParentProperty(true)]
		[Description("Gets or sets the value that indicates whether the control will keep his not rounded value on edit mode")]
		[ClientControlProperty]
		[DefaultValue(false)]
		public bool KeepNotRoundedValue
		{
			get
			{
				return this.ViewState["KeepNotRoundedValue"] != null && (bool)this.ViewState["KeepNotRoundedValue"];
			}
			set
			{
				this.ViewState["KeepNotRoundedValue"] = value;
			}
		}

		// Token: 0x17004112 RID: 16658
		// (get) Token: 0x0600C92A RID: 51498 RVA: 0x002CDCD2 File Offset: 0x002CBED2
		// (set) Token: 0x0600C92B RID: 51499 RVA: 0x002CDCFD File Offset: 0x002CBEFD
		[ClientControlProperty]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets whether the control will keep its trailing zeros (according to the DecimalDigits setting) when focused")]
		public bool KeepTrailingZerosOnFocus
		{
			get
			{
				return this.ViewState["KeepTrailingZerosOnFocus"] != null && (bool)this.ViewState["KeepTrailingZerosOnFocus"];
			}
			set
			{
				this.ViewState["KeepTrailingZerosOnFocus"] = value;
			}
		}

		// Token: 0x17004113 RID: 16659
		// (get) Token: 0x0600C92C RID: 51500 RVA: 0x002CDD15 File Offset: 0x002CBF15
		// (set) Token: 0x0600C92D RID: 51501 RVA: 0x002CDD35 File Offset: 0x002CBF35
		[NotifyParentProperty(true)]
		[DefaultValue("n")]
		[Description("Gets or sets numeric value placeholder inside PositivePattern/NegativePattern.")]
		public string NumericPlaceHolder
		{
			get
			{
				return ((string)this.ViewState["NumericPlaceHolder"]) ?? "n";
			}
			set
			{
				this.ViewState["NumericPlaceHolder"] = value;
			}
		}

		// Token: 0x17004114 RID: 16660
		// (get) Token: 0x0600C92E RID: 51502 RVA: 0x002CDD48 File Offset: 0x002CBF48
		[ClientControlProperty]
		internal string NegativeSign
		{
			get
			{
				return this.numericTextBox.Culture.NumberFormat.NegativeSign;
			}
		}

		// Token: 0x0600C92F RID: 51503 RVA: 0x002CDD5F File Offset: 0x002CBF5F
		protected virtual bool ShouldSerializeDecimalDigits()
		{
			return this.ViewState["DecimalDigits"] != null;
		}

		// Token: 0x0600C930 RID: 51504 RVA: 0x002CDD77 File Offset: 0x002CBF77
		protected virtual bool ShouldSerializeDecimalSeparator()
		{
			return this.ViewState["DecimalSeparator"] != null;
		}

		// Token: 0x0600C931 RID: 51505 RVA: 0x002CDD8F File Offset: 0x002CBF8F
		protected virtual bool ShouldSerializePositivePattern()
		{
			return this.ViewState["PositivePattern"] != null;
		}

		// Token: 0x0600C932 RID: 51506 RVA: 0x002CDDA7 File Offset: 0x002CBFA7
		protected virtual bool ShouldSerializeNegativePattern()
		{
			return this.ViewState["NegativePattern"] != null;
		}

		// Token: 0x0600C933 RID: 51507 RVA: 0x002CDDBF File Offset: 0x002CBFBF
		protected virtual bool ShouldSerializeGroupSeparator()
		{
			return this.ViewState["GroupSeparator"] != null;
		}

		// Token: 0x0600C934 RID: 51508 RVA: 0x002CDDD7 File Offset: 0x002CBFD7
		protected virtual bool ShouldSerializeGroupSizes()
		{
			return this.ViewState["GroupSizes"] != null;
		}

		// Token: 0x0600C935 RID: 51509 RVA: 0x002CDDEF File Offset: 0x002CBFEF
		public System.ComponentModel.AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x0600C936 RID: 51510 RVA: 0x002CDDF8 File Offset: 0x002CBFF8
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x0600C937 RID: 51511 RVA: 0x002CDE01 File Offset: 0x002CC001
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x0600C938 RID: 51512 RVA: 0x002CDE0A File Offset: 0x002CC00A
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x0600C939 RID: 51513 RVA: 0x002CDE13 File Offset: 0x002CC013
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x0600C93A RID: 51514 RVA: 0x002CDE1C File Offset: 0x002CC01C
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600C93B RID: 51515 RVA: 0x002CDE25 File Offset: 0x002CC025
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600C93C RID: 51516 RVA: 0x002CDE2F File Offset: 0x002CC02F
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600C93D RID: 51517 RVA: 0x002CDE38 File Offset: 0x002CC038
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x0600C93E RID: 51518 RVA: 0x002CDE42 File Offset: 0x002CC042
		public virtual PropertyDescriptorCollection GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}

		// Token: 0x0600C93F RID: 51519 RVA: 0x002CDE4B File Offset: 0x002CC04B
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(this, attributes, true);
		}

		// Token: 0x0600C940 RID: 51520 RVA: 0x002CDE55 File Offset: 0x002CC055
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x040034DC RID: 13532
		private InputStateBag _viewStateOwner;

		// Token: 0x040034DD RID: 13533
		private StateBag _ownerStateBag;

		// Token: 0x040034DE RID: 13534
		private IRadNumericTextBox numericTextBox;
	}
}
