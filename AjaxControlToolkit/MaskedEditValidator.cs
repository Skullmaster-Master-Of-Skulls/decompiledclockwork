using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000140 RID: 320
	[ToolboxBitmap(typeof(Accessor), "MaskedEdit.bmp")]
	public class MaskedEditValidator : BaseValidator
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000807 RID: 2055 RVA: 0x00015694 File Offset: 0x00013894
		// (remove) Token: 0x06000808 RID: 2056 RVA: 0x000156CC File Offset: 0x000138CC
		public event EventHandler<ServerValidateEventArgs> MaskedEditServerValidator;

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00015701 File Offset: 0x00013901
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x0001571C File Offset: 0x0001391C
		protected CultureInfo ControlCulture
		{
			get
			{
				if (this._culture == null)
				{
					this._culture = CultureInfo.CurrentCulture;
				}
				return this._culture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00015725 File Offset: 0x00013925
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x00015746 File Offset: 0x00013946
		public new string ErrorMessage
		{
			get
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = base.ID;
				}
				return base.ErrorMessage;
			}
			set
			{
				base.ErrorMessage = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001574F File Offset: 0x0001394F
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x00015757 File Offset: 0x00013957
		[DefaultValue(true)]
		[Category("MaskedEdit")]
		public bool IsValidEmpty
		{
			get
			{
				return this._isValidEmpty;
			}
			set
			{
				this._isValidEmpty = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00015760 File Offset: 0x00013960
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00015776 File Offset: 0x00013976
		[DefaultValue("")]
		[Category("MaskedEdit")]
		public string TooltipMessage
		{
			get
			{
				if (this._messageTip == null)
				{
					return string.Empty;
				}
				return this._messageTip;
			}
			set
			{
				this._messageTip = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001577F File Offset: 0x0001397F
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x00015795 File Offset: 0x00013995
		[DefaultValue("")]
		[Category("MaskedEdit")]
		public string EmptyValueMessage
		{
			get
			{
				if (this._messageEmpty == null)
				{
					return string.Empty;
				}
				return this._messageEmpty;
			}
			set
			{
				this._messageEmpty = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001579E File Offset: 0x0001399E
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x000157B4 File Offset: 0x000139B4
		[Category("MaskedEdit")]
		[DefaultValue("")]
		public string EmptyValueBlurredText
		{
			get
			{
				if (this._textEmpty == null)
				{
					return string.Empty;
				}
				return this._textEmpty;
			}
			set
			{
				this._textEmpty = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x000157BD File Offset: 0x000139BD
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x000157D3 File Offset: 0x000139D3
		[Category("MaskedEdit")]
		[DefaultValue("")]
		public string InvalidValueMessage
		{
			get
			{
				if (this._messageInvalid == null)
				{
					return string.Empty;
				}
				return this._messageInvalid;
			}
			set
			{
				this._messageInvalid = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x000157DC File Offset: 0x000139DC
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x000157F2 File Offset: 0x000139F2
		[DefaultValue("")]
		[Category("MaskedEdit")]
		public string InvalidValueBlurredMessage
		{
			get
			{
				if (this._textInvalid == null)
				{
					return string.Empty;
				}
				return this._textInvalid;
			}
			set
			{
				this._textInvalid = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x000157FB File Offset: 0x000139FB
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x00015811 File Offset: 0x00013A11
		[DefaultValue("")]
		[Category("MaskedEdit")]
		public string MaximumValue
		{
			get
			{
				if (this._maximumValue == null)
				{
					return string.Empty;
				}
				return this._maximumValue;
			}
			set
			{
				this._maximumValue = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0001581A File Offset: 0x00013A1A
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00015830 File Offset: 0x00013A30
		[Category("MaskedEdit")]
		[DefaultValue("")]
		public string MaximumValueMessage
		{
			get
			{
				if (this._messageMax == null)
				{
					return string.Empty;
				}
				return this._messageMax;
			}
			set
			{
				this._messageMax = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00015839 File Offset: 0x00013A39
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0001584F File Offset: 0x00013A4F
		[DefaultValue("")]
		[Category("MaskedEdit")]
		public string MaximumValueBlurredMessage
		{
			get
			{
				if (this._textMax == null)
				{
					return string.Empty;
				}
				return this._textMax;
			}
			set
			{
				this._textMax = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00015858 File Offset: 0x00013A58
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0001586E File Offset: 0x00013A6E
		[Category("MaskedEdit")]
		[DefaultValue("")]
		public string ClientValidationFunction
		{
			get
			{
				if (this._clientValidationFunction == null)
				{
					return string.Empty;
				}
				return this._clientValidationFunction;
			}
			set
			{
				this._clientValidationFunction = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00015877 File Offset: 0x00013A77
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0001588D File Offset: 0x00013A8D
		[DefaultValue("")]
		[Category("MaskedEdit")]
		public string InitialValue
		{
			get
			{
				if (this._initialValue == null)
				{
					return string.Empty;
				}
				return this._initialValue;
			}
			set
			{
				this._initialValue = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00015896 File Offset: 0x00013A96
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x000158AC File Offset: 0x00013AAC
		[DefaultValue("")]
		[Category("MaskedEdit")]
		public string ValidationExpression
		{
			get
			{
				if (this._validationExpression == null)
				{
					return string.Empty;
				}
				return this._validationExpression;
			}
			set
			{
				this._validationExpression = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x000158B5 File Offset: 0x00013AB5
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x000158CB File Offset: 0x00013ACB
		[Category("MaskedEdit")]
		[DefaultValue("")]
		public string MinimumValue
		{
			get
			{
				if (this._minimumValue == null)
				{
					return string.Empty;
				}
				return this._minimumValue;
			}
			set
			{
				this._minimumValue = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x000158D4 File Offset: 0x00013AD4
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x000158EA File Offset: 0x00013AEA
		[Category("MaskedEdit")]
		[DefaultValue("")]
		public string MinimumValueMessage
		{
			get
			{
				if (this._messageMin == null)
				{
					return string.Empty;
				}
				return this._messageMin;
			}
			set
			{
				this._messageMin = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x000158F3 File Offset: 0x00013AF3
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00015909 File Offset: 0x00013B09
		[DefaultValue("")]
		[Category("MaskedEdit")]
		public string MinimumValueBlurredText
		{
			get
			{
				if (this._textMin == null)
				{
					return string.Empty;
				}
				return this._textMin;
			}
			set
			{
				this._textMin = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00015912 File Offset: 0x00013B12
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00015928 File Offset: 0x00013B28
		[DefaultValue("")]
		[RequiredProperty]
		[TypeConverter(typeof(MaskedEditTypeConvert))]
		[Category("MaskedEdit")]
		public string ControlExtender
		{
			get
			{
				if (this._controlExtender == null)
				{
					return string.Empty;
				}
				return this._controlExtender;
			}
			set
			{
				this._controlExtender = value;
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00015934 File Offset: 0x00013B34
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.EnableClientScript)
			{
				MaskedEditExtender maskedEditExtender = (MaskedEditExtender)this.FindControl(this.ControlExtender);
				TextBox textBox = (TextBox)maskedEditExtender.FindControl(base.ControlToValidate);
				int num;
				int num2;
				if (maskedEditExtender.ClearMaskOnLostFocus)
				{
					num = 0;
					num2 = MaskedEditCommon.GetValidMask(maskedEditExtender.Mask).Length + 1;
				}
				else
				{
					num = MaskedEditCommon.GetFirstMaskPosition(maskedEditExtender.Mask);
					num2 = MaskedEditCommon.GetLastMaskPosition(maskedEditExtender.Mask) + 1;
				}
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "IsMaskedEdit", true.ToString().ToLower(CultureInfo.InvariantCulture), true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "ValidEmpty", this.IsValidEmpty.ToString().ToLower(CultureInfo.InvariantCulture), true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "MaximumValue", this.MaximumValue, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "MinimumValue", this.MinimumValue, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "InitialValue", this.InitialValue, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "ValidationExpression", this.ValidationExpression, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "ClientValidationFunction", this.ClientValidationFunction, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "TargetValidator", textBox.ClientID, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "EmptyValueMessage", this.EmptyValueMessage, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "EmptyValueText", this.EmptyValueBlurredText, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "MaximumValueMessage", this.MaximumValueMessage, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "MaximumValueText", this.MaximumValueBlurredMessage, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "MinimumValueMessage", this.MinimumValueMessage, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "MinimumValueText", this.MinimumValueBlurredText, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "InvalidValueMessage", this.InvalidValueMessage, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "InvalidValueText", this.InvalidValueBlurredMessage, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "InvalidValueCssClass", maskedEditExtender.OnInvalidCssClass, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "CssBlurNegative", maskedEditExtender.OnBlurCssNegative, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "CssFocus", maskedEditExtender.OnFocusCssClass, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "CssFocusNegative", maskedEditExtender.OnFocusCssNegative, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "TooltipMessage", this.TooltipMessage, true);
				ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "FirstMaskPosition", num.ToString(CultureInfo.InvariantCulture), true);
				if (!string.IsNullOrEmpty(maskedEditExtender.CultureName) && maskedEditExtender.OverridePageCulture)
				{
					this.ControlCulture = CultureInfo.GetCultureInfo(maskedEditExtender.CultureName);
				}
				else
				{
					this.ControlCulture = CultureInfo.CurrentCulture;
				}
				switch (maskedEditExtender.MaskType)
				{
				case MaskedEditType.None:
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "evaluationfunction", "MaskedEditValidatorNone", true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "LastMaskPosition", num2.ToString(CultureInfo.InvariantCulture), true);
					return;
				case MaskedEditType.Date:
				{
					string dateSeparator = this.ControlCulture.DateTimeFormat.DateSeparator;
					string[] array = this.ControlCulture.DateTimeFormat.ShortDatePattern.Split(new string[]
					{
						this.ControlCulture.DateTimeFormat.DateSeparator
					}, StringSplitOptions.None);
					string text = array[0].Substring(0, 1).ToUpper(this.ControlCulture);
					text += array[1].Substring(0, 1).ToUpper(this.ControlCulture);
					text += array[2].Substring(0, 1).ToUpper(this.ControlCulture);
					text = ((maskedEditExtender.UserDateFormat == MaskedEditUserDateFormat.None) ? text : maskedEditExtender.UserDateFormat.ToString());
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "DateSeparator", dateSeparator, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "DateFormat", text, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "Century", maskedEditExtender.Century.ToString(CultureInfo.InvariantCulture), true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "evaluationfunction", "MaskedEditValidatorDate", true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "LastMaskPosition", num2.ToString(CultureInfo.InvariantCulture), true);
					return;
				}
				case MaskedEditType.Number:
				{
					string currencySymbol = this.ControlCulture.NumberFormat.CurrencySymbol;
					string currencyDecimalSeparator = this.ControlCulture.NumberFormat.CurrencyDecimalSeparator;
					string currencyGroupSeparator = this.ControlCulture.NumberFormat.CurrencyGroupSeparator;
					if (maskedEditExtender.DisplayMoney != MaskedEditShowSymbol.None)
					{
						num2 += maskedEditExtender.CultureCurrencySymbolPlaceholder.Length + 1;
					}
					if (maskedEditExtender.AcceptNegative != MaskedEditShowSymbol.None)
					{
						if (maskedEditExtender.DisplayMoney != MaskedEditShowSymbol.None)
						{
							num2++;
						}
						else
						{
							num2 += 2;
						}
					}
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "Money", currencySymbol, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "Decimal", currencyDecimalSeparator, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "Thousands", currencyGroupSeparator, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "evaluationfunction", "MaskedEditValidatorNumber", true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "LastMaskPosition", num2.ToString(CultureInfo.InvariantCulture), true);
					return;
				}
				case MaskedEditType.Time:
				{
					string timeSeparator = this.ControlCulture.DateTimeFormat.TimeSeparator;
					string text2 = string.Empty;
					if (string.IsNullOrEmpty(this.ControlCulture.DateTimeFormat.AMDesignator + this.ControlCulture.DateTimeFormat.PMDesignator))
					{
						text2 = string.Empty;
					}
					else
					{
						text2 = this.ControlCulture.DateTimeFormat.AMDesignator + ";" + this.ControlCulture.DateTimeFormat.PMDesignator;
					}
					text2 = ((maskedEditExtender.UserTimeFormat == MaskedEditUserTimeFormat.None) ? text2 : string.Empty);
					if (maskedEditExtender.AcceptAMPM && !string.IsNullOrEmpty(text2))
					{
						char c = char.Parse(timeSeparator);
						string[] array2 = text2.Split(new char[]
						{
							c
						});
						num2 += array2[0].Length + 1;
					}
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "TimeSeparator", timeSeparator, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "AmPmSymbol", text2, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "evaluationfunction", "MaskedEditValidatorTime", true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "LastMaskPosition", num2.ToString(CultureInfo.InvariantCulture), true);
					break;
				}
				case MaskedEditType.DateTime:
				{
					string text3 = this.ControlCulture.DateTimeFormat.DateSeparator;
					string[] array3 = this.ControlCulture.DateTimeFormat.ShortDatePattern.Split(new string[]
					{
						this.ControlCulture.DateTimeFormat.DateSeparator
					}, StringSplitOptions.None);
					string text4 = array3[0].Substring(0, 1).ToUpper(this.ControlCulture);
					text4 += array3[1].Substring(0, 1).ToUpper(this.ControlCulture);
					text4 += array3[2].Substring(0, 1).ToUpper(this.ControlCulture);
					text4 = ((maskedEditExtender.UserDateFormat == MaskedEditUserDateFormat.None) ? text4 : maskedEditExtender.UserDateFormat.ToString());
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "DateSeparator", text3, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "DateFormat", text4, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "Century", maskedEditExtender.Century.ToString(CultureInfo.InvariantCulture), true);
					text3 = this.ControlCulture.DateTimeFormat.TimeSeparator;
					string text5 = string.Empty;
					if (string.IsNullOrEmpty(this.ControlCulture.DateTimeFormat.AMDesignator + this.ControlCulture.DateTimeFormat.PMDesignator))
					{
						text5 = string.Empty;
					}
					else
					{
						text5 = this.ControlCulture.DateTimeFormat.AMDesignator + ";" + this.ControlCulture.DateTimeFormat.PMDesignator;
					}
					text5 = ((maskedEditExtender.UserTimeFormat == MaskedEditUserTimeFormat.None) ? text5 : string.Empty);
					if (maskedEditExtender.AcceptAMPM && !string.IsNullOrEmpty(text5))
					{
						char c2 = char.Parse(text3);
						string[] array4 = text5.Split(new char[]
						{
							c2
						});
						num2 += array4[0].Length + 1;
					}
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "TimeSeparator", text3, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "AmPmSymbol", text5, true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "evaluationfunction", "MaskedEditValidatorDateTime", true);
					ScriptManager.RegisterExpandoAttribute(this, this.ClientID, "LastMaskPosition", num2.ToString(CultureInfo.InvariantCulture), true);
					return;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000161F3 File Offset: 0x000143F3
		protected override bool ControlPropertiesValid()
		{
			return this.FindControl(base.ControlToValidate) is TextBox;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001620C File Offset: 0x0001440C
		protected override bool EvaluateIsValid()
		{
			MaskedEditExtender maskedEditExtender = (MaskedEditExtender)this.FindControl(this.ControlExtender);
			TextBox textBox = (TextBox)maskedEditExtender.FindControl(base.ControlToValidate);
			base.ErrorMessage = string.Empty;
			base.Text = string.Empty;
			string text = string.Empty;
			bool flag = true;
			if (!this.IsValidEmpty && textBox.Text.Trim() == this.InitialValue)
			{
				base.ErrorMessage = this.EmptyValueMessage;
				if (string.IsNullOrEmpty(this.EmptyValueBlurredText))
				{
					base.Text = base.ErrorMessage;
				}
				else
				{
					base.Text = this.EmptyValueBlurredText;
				}
				text = maskedEditExtender.OnInvalidCssClass;
				flag = false;
			}
			if (flag && textBox.Text.Length != 0 && this.ValidationExpression.Length != 0)
			{
				try
				{
					Regex regex = new Regex(this.ValidationExpression);
					flag = regex.IsMatch(textBox.Text);
				}
				catch
				{
					flag = false;
				}
			}
			if (flag && textBox.Text.Length != 0)
			{
				string text2 = maskedEditExtender.CultureName;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = CultureInfo.CurrentCulture.Name;
				}
				this.ControlCulture = CultureInfo.GetCultureInfo(text2);
				string text3 = string.Empty;
				if (!string.IsNullOrEmpty(this.ControlCulture.DateTimeFormat.AMDesignator) && !string.IsNullOrEmpty(this.ControlCulture.DateTimeFormat.PMDesignator))
				{
					text3 = this.ControlCulture.DateTimeFormat.AMDesignator + ";" + this.ControlCulture.DateTimeFormat.PMDesignator;
				}
				switch (maskedEditExtender.MaskType)
				{
				case MaskedEditType.Date:
				case MaskedEditType.Time:
				case MaskedEditType.DateTime:
					break;
				case MaskedEditType.Number:
					try
					{
						decimal.Parse(textBox.Text, this.ControlCulture);
						goto IL_2A5;
					}
					catch
					{
						flag = false;
						goto IL_2A5;
					}
					break;
				default:
					goto IL_2A5;
				}
				int num = textBox.Text.Length;
				if (maskedEditExtender.AcceptAMPM && !string.IsNullOrEmpty(text3) && (maskedEditExtender.MaskType == MaskedEditType.Time || maskedEditExtender.MaskType == MaskedEditType.DateTime))
				{
					char[] separator = new char[]
					{
						';'
					};
					string[] array = text3.Split(separator);
					if (array[0].Length != 0)
					{
						num -= array[0].Length + 1;
					}
				}
				int num2 = MaskedEditCommon.GetValidMask(maskedEditExtender.Mask).Length;
				if (maskedEditExtender.MaskType != MaskedEditType.Time)
				{
					int length = (string.IsNullOrEmpty(maskedEditExtender.CultureName) ? CultureInfo.CurrentCulture : CultureInfo.GetCultureInfo(maskedEditExtender.CultureName)).DateTimeFormat.DateSeparator.Length;
					num2 += (length - 1) * 2;
				}
				if (num2 != num)
				{
					flag = false;
				}
				if (flag)
				{
					try
					{
						DateTime.Parse(textBox.Text, this.ControlCulture);
					}
					catch
					{
						flag = false;
					}
				}
				IL_2A5:
				if (!flag)
				{
					base.ErrorMessage = this.InvalidValueMessage;
					if (string.IsNullOrEmpty(this.InvalidValueBlurredMessage))
					{
						base.Text = base.ErrorMessage;
					}
					else
					{
						base.Text = this.InvalidValueBlurredMessage;
					}
					text = maskedEditExtender.OnInvalidCssClass;
				}
				if (flag && (!string.IsNullOrEmpty(this.MaximumValue) || !string.IsNullOrEmpty(this.MinimumValue)))
				{
					switch (maskedEditExtender.MaskType)
					{
					case MaskedEditType.None:
						if (!string.IsNullOrEmpty(this.MaximumValue))
						{
							try
							{
								int num3 = int.Parse(this.MaximumValue, this.ControlCulture);
								flag = (num3 >= textBox.Text.Length);
							}
							catch
							{
								base.ErrorMessage = this.InvalidValueMessage;
								if (string.IsNullOrEmpty(this.InvalidValueBlurredMessage))
								{
									base.Text = base.ErrorMessage;
								}
								else
								{
									base.Text = this.InvalidValueBlurredMessage;
								}
								flag = false;
							}
							if (!flag)
							{
								base.ErrorMessage = this.MaximumValueMessage;
								if (string.IsNullOrEmpty(this.MaximumValueBlurredMessage))
								{
									base.Text = base.ErrorMessage;
								}
								else
								{
									base.Text = this.MaximumValueBlurredMessage;
								}
								text = maskedEditExtender.OnInvalidCssClass;
							}
						}
						if (flag && !string.IsNullOrEmpty(this.MinimumValue))
						{
							try
							{
								int num3 = int.Parse(this.MinimumValue, this.ControlCulture);
								flag = (num3 <= textBox.Text.Length);
							}
							catch
							{
								base.ErrorMessage = this.InvalidValueMessage;
								if (string.IsNullOrEmpty(this.InvalidValueBlurredMessage))
								{
									base.Text = base.ErrorMessage;
								}
								else
								{
									base.Text = this.InvalidValueBlurredMessage;
								}
								flag = false;
							}
							if (!flag)
							{
								base.ErrorMessage = this.MinimumValueMessage;
								if (string.IsNullOrEmpty(this.MinimumValueBlurredText))
								{
									base.Text = base.ErrorMessage;
								}
								else
								{
									base.Text = this.MinimumValueBlurredText;
								}
								text = maskedEditExtender.OnInvalidCssClass;
							}
						}
						break;
					case MaskedEditType.Date:
					case MaskedEditType.Time:
					case MaskedEditType.DateTime:
					{
						DateTime t = DateTime.Parse(textBox.Text, this.ControlCulture);
						if (!string.IsNullOrEmpty(this.MaximumValue))
						{
							try
							{
								DateTime t2 = DateTime.Parse(this.MaximumValue, this.ControlCulture);
								flag = (t2 >= t);
							}
							catch
							{
								flag = false;
							}
							if (!flag)
							{
								base.ErrorMessage = this.MaximumValueMessage;
								if (string.IsNullOrEmpty(this.MaximumValueBlurredMessage))
								{
									base.Text = base.ErrorMessage;
								}
								else
								{
									base.Text = this.MaximumValueBlurredMessage;
								}
								text = maskedEditExtender.OnInvalidCssClass;
							}
						}
						if (flag && !string.IsNullOrEmpty(this.MinimumValue))
						{
							try
							{
								DateTime t2 = DateTime.Parse(this.MinimumValue, this.ControlCulture);
								flag = (t2 <= t);
							}
							catch
							{
								flag = false;
							}
							if (!flag)
							{
								base.ErrorMessage = this.MinimumValueMessage;
								if (string.IsNullOrEmpty(this.MinimumValueBlurredText))
								{
									base.Text = base.ErrorMessage;
								}
								else
								{
									base.Text = this.MinimumValueBlurredText;
								}
								text = maskedEditExtender.OnInvalidCssClass;
							}
						}
						break;
					}
					case MaskedEditType.Number:
					{
						decimal d = decimal.Parse(textBox.Text, this.ControlCulture);
						if (!string.IsNullOrEmpty(this.MaximumValue))
						{
							try
							{
								decimal d2 = decimal.Parse(this.MaximumValue, this.ControlCulture);
								flag = (d2 >= d);
							}
							catch
							{
								flag = false;
							}
							if (!flag)
							{
								base.ErrorMessage = this.MaximumValueMessage;
								if (string.IsNullOrEmpty(this.MaximumValueBlurredMessage))
								{
									base.Text = base.ErrorMessage;
								}
								else
								{
									base.Text = this.MaximumValueBlurredMessage;
								}
								text = maskedEditExtender.OnInvalidCssClass;
							}
						}
						if (flag && !string.IsNullOrEmpty(this.MinimumValue))
						{
							try
							{
								decimal d2 = decimal.Parse(this.MinimumValue, this.ControlCulture);
								flag = (d2 <= d);
							}
							catch
							{
								flag = false;
							}
							if (!flag)
							{
								base.ErrorMessage = this.MinimumValueMessage;
								if (string.IsNullOrEmpty(this.MinimumValueBlurredText))
								{
									base.Text = base.ErrorMessage;
								}
								else
								{
									base.Text = this.MinimumValueBlurredText;
								}
								text = maskedEditExtender.OnInvalidCssClass;
							}
						}
						break;
					}
					}
				}
			}
			if (flag && this.MaskedEditServerValidator != null)
			{
				ServerValidateEventArgs serverValidateEventArgs = new ServerValidateEventArgs(textBox.Text, flag);
				this.MaskedEditServerValidator(textBox, serverValidateEventArgs);
				flag = serverValidateEventArgs.IsValid;
				if (!flag)
				{
					text = maskedEditExtender.OnInvalidCssClass;
					base.ErrorMessage = this.InvalidValueMessage;
					if (string.IsNullOrEmpty(this.InvalidValueBlurredMessage))
					{
						base.Text = base.ErrorMessage;
					}
					else
					{
						base.Text = this.InvalidValueBlurredMessage;
					}
				}
			}
			if (!flag)
			{
				string script = string.Concat(new string[]
				{
					"MaskedEditSetCssClass(",
					this.ClientID,
					",'",
					text,
					"');"
				});
				ScriptManager.RegisterStartupScript(this, typeof(MaskedEditValidator), "MaskedEditServerValidator_" + this.ID, script, true);
			}
			return flag;
		}

		// Token: 0x04000349 RID: 841
		private bool _isValidEmpty = true;

		// Token: 0x0400034A RID: 842
		private string _messageTip = string.Empty;

		// Token: 0x0400034B RID: 843
		private string _messageInvalid = string.Empty;

		// Token: 0x0400034C RID: 844
		private string _messageEmpty = string.Empty;

		// Token: 0x0400034D RID: 845
		private string _messageMax = string.Empty;

		// Token: 0x0400034E RID: 846
		private string _messageMin = string.Empty;

		// Token: 0x0400034F RID: 847
		private string _textInvalid = string.Empty;

		// Token: 0x04000350 RID: 848
		private string _textEmpty = string.Empty;

		// Token: 0x04000351 RID: 849
		private string _textMax = string.Empty;

		// Token: 0x04000352 RID: 850
		private string _textMin = string.Empty;

		// Token: 0x04000353 RID: 851
		private string _initialValue = string.Empty;

		// Token: 0x04000354 RID: 852
		private string _validationExpression = string.Empty;

		// Token: 0x04000355 RID: 853
		private string _clientValidationFunction = string.Empty;

		// Token: 0x04000356 RID: 854
		private string _maximumValue = string.Empty;

		// Token: 0x04000357 RID: 855
		private string _minimumValue = string.Empty;

		// Token: 0x04000358 RID: 856
		private string _controlExtender = string.Empty;

		// Token: 0x04000359 RID: 857
		private CultureInfo _culture;
	}
}
