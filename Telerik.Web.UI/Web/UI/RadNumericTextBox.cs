using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020012C3 RID: 4803
	[Description("Telerik RadInput")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadNumericTextBox", "Telerik.Web.UI.Input.NumericTextBox.RadNumericInputScript.js")]
	[RequiredScript(typeof(RadTextBox))]
	[Designer("Telerik.Web.Design.NumericTextBoxControlDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadNumericTextBox Runat=server></{0}:RadNumericTextBox>")]
	[ValidationProperty("ValidationText")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadNumericTextBox))]
	[ControlBuilder(typeof(TextBoxControlBuilder))]
	[ControlValueProperty("Text")]
	[DefaultEvent("TextChanged")]
	[SupportsEventValidation]
	[ToolboxBitmap(typeof(RadNumericTextBox), "Telerik.Web.UI.NumericTextbox.png")]
	[TelerikToolboxCategory("Data Editing")]
	[LightweightRendering]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadNumericTextBox : RadInputControl, IRadNumericTextBox
	{
		// Token: 0x17004118 RID: 16664
		// (get) Token: 0x0600C949 RID: 51529 RVA: 0x002CDFC2 File Offset: 0x002CC1C2
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The style applied to the control when the value is negative.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public InputStyle NegativeStyle
		{
			get
			{
				if (this.negativeStyle == null)
				{
					this.negativeStyle = new InputStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.negativeStyle).TrackViewState();
					}
				}
				return this.negativeStyle;
			}
		}

		// Token: 0x17004119 RID: 16665
		// (get) Token: 0x0600C94A RID: 51530 RVA: 0x002CDFF0 File Offset: 0x002CC1F0
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public InputIncrementSettings IncrementSettings
		{
			get
			{
				if (this.incrementSettings == null)
				{
					this.incrementSettings = new InputIncrementSettings(this.ViewState);
				}
				return this.incrementSettings;
			}
		}

		// Token: 0x1700411A RID: 16666
		// (get) Token: 0x0600C94B RID: 51531 RVA: 0x002CE014 File Offset: 0x002CC214
		// (set) Token: 0x0600C94C RID: 51532 RVA: 0x002CE050 File Offset: 0x002CC250
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(double?), "")]
		[Bindable(true, BindingDirection.TwoWay)]
		[Description("The value.")]
		[Browsable(true)]
		public virtual double? Value
		{
			get
			{
				if (string.IsNullOrEmpty(this.Text))
				{
					return null;
				}
				return new double?(double.Parse(this.Text, NumberFormatInfo.InvariantInfo));
			}
			set
			{
				if (value != null)
				{
					double? num = value;
					double maxValue = this.MaxValue;
					if (num.GetValueOrDefault() <= maxValue || num == null)
					{
						double? num2 = value;
						double minValue = this.MinValue;
						if (num2.GetValueOrDefault() >= minValue || num2 == null)
						{
							this.Text = value.Value.ToString(NumberFormatInfo.InvariantInfo);
							return;
						}
					}
					throw new ArgumentOutOfRangeException("Value", string.Format("Value of '{0}' is not valid for 'Value'. 'Value' should be between 'MinValue' and 'MaxValue'.", value));
				}
				this.Text = null;
			}
		}

		// Token: 0x1700411B RID: 16667
		// (get) Token: 0x0600C94D RID: 51533 RVA: 0x002CE0E4 File Offset: 0x002CC2E4
		// (set) Token: 0x0600C94E RID: 51534 RVA: 0x002CE0EC File Offset: 0x002CC2EC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = this.RangeTextProperty(value);
			}
		}

		// Token: 0x1700411C RID: 16668
		// (get) Token: 0x0600C94F RID: 51535 RVA: 0x002CE0FB File Offset: 0x002CC2FB
		public override string ValidationText
		{
			get
			{
				return this.Text.Replace(".", this.NumberFormat.DecimalSeparator);
			}
		}

		// Token: 0x1700411D RID: 16669
		// (get) Token: 0x0600C950 RID: 51536 RVA: 0x002CE118 File Offset: 0x002CC318
		// (set) Token: 0x0600C951 RID: 51537 RVA: 0x002CE1F0 File Offset: 0x002CC3F0
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(true, BindingDirection.TwoWay)]
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public virtual object DbValue
		{
			get
			{
				if (string.IsNullOrEmpty(this.Text))
				{
					return null;
				}
				if (this.DataType == typeof(TimeSpan))
				{
					long ticks = long.Parse(this.Text, NumberFormatInfo.InvariantInfo);
					return new TimeSpan(ticks);
				}
				if (this.DataType == typeof(char))
				{
					short value = short.Parse(this.Text, NumberFormatInfo.InvariantInfo);
					return Convert.ToChar(value);
				}
				double num = double.Parse(this.Text, NumberFormatInfo.InvariantInfo);
				if (this.DbValueFactor != 1.0)
				{
					num /= Convert.ToDouble(Convert.ChangeType(this.DbValueFactor, this.DataType));
				}
				return Convert.ChangeType(num, this.DataType);
			}
			set
			{
				if (value is double)
				{
					this.Text = ((double)value * this.DbValueFactor).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is float)
				{
					this.Text = ((double)((float)value) * this.DbValueFactor).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is long)
				{
					this.Text = ((double)((long)value) * ((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is int)
				{
					this.Text = ((double)((int)value) * ((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is short)
				{
					this.Text = ((double)((short)value) * ((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is byte)
				{
					this.Text = ((double)((byte)value) * ((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is decimal)
				{
					this.Text = ((decimal)value * Convert.ToDecimal((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is DBNull)
				{
					this.Text = null;
					return;
				}
				if (value == null)
				{
					this.Text = null;
					return;
				}
				if (value is string)
				{
					this.Text = this.ParseDbValueString((string)value);
					return;
				}
				if (value is bool)
				{
					this.Text = (((bool)value) ? "1" : "0");
					return;
				}
				if (value is TimeSpan)
				{
					this.Text = ((TimeSpan)value).Ticks.ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is char)
				{
					this.Text = ((double)((char)value) * ((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is sbyte)
				{
					this.Text = ((double)((sbyte)value) * ((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is ushort)
				{
					this.Text = ((double)((ushort)value) * ((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is uint)
				{
					this.Text = ((uint)value * ((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				if (value is ulong)
				{
					this.Text = ((ulong)value * ((this.DbValueFactor == 1.0) ? 1.0 : this.DbValueFactor)).ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				this.Text = value.ToString();
			}
		}

		// Token: 0x1700411E RID: 16670
		// (get) Token: 0x0600C952 RID: 51538 RVA: 0x002CE5AF File Offset: 0x002CC7AF
		// (set) Token: 0x0600C953 RID: 51539 RVA: 0x002CE5E2 File Offset: 0x002CC7E2
		[Description("Factor by which the value of DbValue property will be multiplied")]
		[DefaultValue(1f)]
		[Category("Behavior")]
		public virtual double DbValueFactor
		{
			get
			{
				if (this.ViewState["DbValueFactor"] == null)
				{
					return 1.0;
				}
				return (double)this.ViewState["DbValueFactor"];
			}
			set
			{
				if (value == 0.0)
				{
					throw new ArgumentOutOfRangeException("DbValueFactor should be different than 0");
				}
				this.ViewState["DbValueFactor"] = value;
			}
		}

		// Token: 0x1700411F RID: 16671
		// (get) Token: 0x0600C954 RID: 51540 RVA: 0x002CE611 File Offset: 0x002CC811
		// (set) Token: 0x0600C955 RID: 51541 RVA: 0x002CE63C File Offset: 0x002CC83C
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("Whether the button is displayed")]
		[Category("Appearance")]
		public virtual bool ShowSpinButtons
		{
			get
			{
				return this.ViewState["ShowSpinButtons"] != null && (bool)this.ViewState["ShowSpinButtons"];
			}
			set
			{
				this.ViewState["ShowSpinButtons"] = value;
			}
		}

		// Token: 0x17004120 RID: 16672
		// (get) Token: 0x0600C956 RID: 51542 RVA: 0x002CE654 File Offset: 0x002CC854
		// (set) Token: 0x0600C957 RID: 51543 RVA: 0x002CE686 File Offset: 0x002CC886
		[TypeConverter("Telerik.Web.UI.NumericDataTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(double))]
		public Type DataType
		{
			get
			{
				object obj = this.ViewState["DataType"];
				if (obj == null)
				{
					obj = typeof(double);
				}
				return (Type)obj;
			}
			set
			{
				this.ViewState["DataType"] = value;
			}
		}

		// Token: 0x17004121 RID: 16673
		// (get) Token: 0x0600C958 RID: 51544 RVA: 0x002CE699 File Offset: 0x002CC899
		// (set) Token: 0x0600C959 RID: 51545 RVA: 0x002CE6C8 File Offset: 0x002CC8C8
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		public virtual string SpinUpCssClass
		{
			get
			{
				if (this.ViewState["SpinUpCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["SpinUpCssClass"];
			}
			set
			{
				this.ViewState["SpinUpCssClass"] = value;
			}
		}

		// Token: 0x17004122 RID: 16674
		// (get) Token: 0x0600C95A RID: 51546 RVA: 0x002CE6DB File Offset: 0x002CC8DB
		// (set) Token: 0x0600C95B RID: 51547 RVA: 0x002CE70A File Offset: 0x002CC90A
		[Category("Appearance")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string SpinDownCssClass
		{
			get
			{
				if (this.ViewState["SpinDownCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["SpinDownCssClass"];
			}
			set
			{
				this.ViewState["SpinDownCssClass"] = value;
			}
		}

		// Token: 0x17004123 RID: 16675
		// (get) Token: 0x0600C95C RID: 51548 RVA: 0x002CE71D File Offset: 0x002CC91D
		// (set) Token: 0x0600C95D RID: 51549 RVA: 0x002CE751 File Offset: 0x002CC951
		[Description("Culture used by RadNumericTextBox to format the numburs or currency.")]
		[Category("Behavior")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public CultureInfo Culture
		{
			get
			{
				if (this.ViewState["Culture"] == null)
				{
					return Thread.CurrentThread.CurrentCulture;
				}
				return (CultureInfo)this.ViewState["Culture"];
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17004124 RID: 16676
		// (get) Token: 0x0600C95E RID: 51550 RVA: 0x002CE764 File Offset: 0x002CC964
		// (set) Token: 0x0600C95F RID: 51551 RVA: 0x002CE797 File Offset: 0x002CC997
		[DefaultValue(70368744177664.0)]
		[Category("Behavior")]
		[Description("Gets or sets the largest possible value of a RadNumericTextBox.")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		public virtual double MaxValue
		{
			get
			{
				if (this.ViewState["MaxValue"] == null)
				{
					return 70368744177664.0;
				}
				return (double)this.ViewState["MaxValue"];
			}
			set
			{
				this.ViewState["MaxValue"] = value;
				this.Text = this.RangeTextProperty(this.Text);
			}
		}

		// Token: 0x17004125 RID: 16677
		// (get) Token: 0x0600C960 RID: 51552 RVA: 0x002CE7C1 File Offset: 0x002CC9C1
		// (set) Token: 0x0600C961 RID: 51553 RVA: 0x002CE7F4 File Offset: 0x002CC9F4
		[ClientControlProperty]
		[DefaultValue(-70368744177664.0)]
		[Category("Behavior")]
		[Description("Gets or sets the smallest possible value of a RadNumericTextBox.")]
		[NotifyParentProperty(true)]
		public virtual double MinValue
		{
			get
			{
				if (this.ViewState["MinValue"] == null)
				{
					return -70368744177664.0;
				}
				return (double)this.ViewState["MinValue"];
			}
			set
			{
				this.ViewState["MinValue"] = value;
				this.Text = this.RangeTextProperty(this.Text);
			}
		}

		// Token: 0x17004126 RID: 16678
		// (get) Token: 0x0600C962 RID: 51554 RVA: 0x002CE81E File Offset: 0x002CCA1E
		// (set) Token: 0x0600C963 RID: 51555 RVA: 0x002CE849 File Offset: 0x002CCA49
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Gets or sets whether the RadNumericTextBox should autocorrect out of range values to valid values or leave them and apply its InvalidStyle. If the InvalidStyle is applied, the control will have no value.")]
		[NotifyParentProperty(true)]
		public bool AllowOutOfRangeAutoCorrect
		{
			get
			{
				return this.ViewState["AllowOutOfRangeAutoCorrect"] == null || (bool)this.ViewState["AllowOutOfRangeAutoCorrect"];
			}
			set
			{
				this.ViewState["AllowOutOfRangeAutoCorrect"] = value;
			}
		}

		// Token: 0x17004127 RID: 16679
		// (get) Token: 0x0600C964 RID: 51556 RVA: 0x002CE861 File Offset: 0x002CCA61
		// (set) Token: 0x0600C965 RID: 51557 RVA: 0x002CE88C File Offset: 0x002CCA8C
		[DefaultValue(NumericType.Number)]
		[Category("Behavior")]
		[Description("The type of the RadNumericTextBox")]
		[NotifyParentProperty(true)]
		public virtual NumericType Type
		{
			get
			{
				if (this.ViewState["Type"] == null)
				{
					return NumericType.Number;
				}
				return (NumericType)this.ViewState["Type"];
			}
			set
			{
				this.ViewState["Type"] = value;
			}
		}

		// Token: 0x0600C966 RID: 51558 RVA: 0x002CE8A4 File Offset: 0x002CCAA4
		protected override bool isOnlyInputRendered()
		{
			return base.isOnlyInputRendered() && !this.ShowSpinButtons;
		}

		// Token: 0x17004128 RID: 16680
		// (get) Token: 0x0600C967 RID: 51559 RVA: 0x002CE8BC File Offset: 0x002CCABC
		// (set) Token: 0x0600C968 RID: 51560 RVA: 0x002CE930 File Offset: 0x002CCB30
		[DefaultValue("")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public override string DisplayText
		{
			get
			{
				if (!string.IsNullOrEmpty(this._displayText))
				{
					return this._displayText;
				}
				if (!base.DesignMode)
				{
					string text = "";
					if (this.Value != null)
					{
						text = Telerik.Web.UI.NumberFormat.Format(this.Value, this.NumberFormat);
					}
					if (!string.IsNullOrEmpty(text))
					{
						return text;
					}
					if (!string.IsNullOrEmpty(this.EmptyMessage))
					{
						return this.EmptyMessage;
					}
				}
				return "";
			}
			set
			{
				this._displayText = value;
			}
		}

		// Token: 0x17004129 RID: 16681
		// (get) Token: 0x0600C969 RID: 51561 RVA: 0x002CE939 File Offset: 0x002CCB39
		// (set) Token: 0x0600C96A RID: 51562 RVA: 0x002CE964 File Offset: 0x002CCB64
		[DefaultValue(typeof(SelectionOnFocus), "SelectAll")]
		[ClientControlProperty]
		[Themeable(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public override SelectionOnFocus SelectionOnFocus
		{
			get
			{
				if (this.ViewState["SelectionOnFocus"] == null)
				{
					return SelectionOnFocus.SelectAll;
				}
				return (SelectionOnFocus)this.ViewState["SelectionOnFocus"];
			}
			set
			{
				this.ViewState["SelectionOnFocus"] = value;
			}
		}

		// Token: 0x1700412A RID: 16682
		// (get) Token: 0x0600C96B RID: 51563 RVA: 0x002CE97C File Offset: 0x002CCB7C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Setting the numeric format of the RadNumericTextBox control")]
		[NotifyParentProperty(true)]
		public NumberFormatSettings NumberFormat
		{
			get
			{
				if (this.numberFormat == null)
				{
					this.numberFormat = new NumberFormatSettings(this, this.ViewState);
				}
				return this.numberFormat;
			}
		}

		// Token: 0x1700412B RID: 16683
		// (get) Token: 0x0600C96C RID: 51564 RVA: 0x002CE99E File Offset: 0x002CCB9E
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x0600C96D RID: 51565 RVA: 0x002CE9A4 File Offset: 0x002CCBA4
		public RadNumericTextBox DeepClone()
		{
			RadNumericTextBox radNumericTextBox = new RadNumericTextBox();
			foreach (object obj in this.ViewState.Keys)
			{
				string text = (string)obj;
				PropertyInfo property = typeof(WebControl).GetProperty(text);
				if (property != null)
				{
					property.SetValue(radNumericTextBox, property.GetValue(this, new object[0]), new object[0]);
				}
				else
				{
					radNumericTextBox.ViewState[text] = this.ViewState[text];
				}
			}
			radNumericTextBox.Events.AddHandlers(base.Events);
			radNumericTextBox.ControlStyle.CopyFrom(base.ControlStyle);
			radNumericTextBox.EnabledStyle.CopyFrom(base.EnabledStyle);
			radNumericTextBox.HoveredStyle.CopyFrom(base.HoveredStyle);
			radNumericTextBox.FocusedStyle.CopyFrom(base.FocusedStyle);
			radNumericTextBox.EmptyMessageStyle.CopyFrom(base.EmptyMessageStyle);
			radNumericTextBox.ReadOnlyStyle.CopyFrom(base.ReadOnlyStyle);
			radNumericTextBox.DisabledStyle.CopyFrom(base.DisabledStyle);
			radNumericTextBox.InvalidStyle.CopyFrom(base.InvalidStyle);
			return radNumericTextBox;
		}

		// Token: 0x0600C96E RID: 51566 RVA: 0x002CEAEC File Offset: 0x002CCCEC
		protected override void SetDesignTimeAttributes(HtmlTextWriter writer)
		{
			if (this.Enabled && !string.IsNullOrEmpty(this.Text))
			{
				double? value = this.Value;
				if (value.GetValueOrDefault() < 0.0 && value != null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, base.FormatCssClass("riTextBox riNegative", this.CssClass));
					writer.AddAttribute(HtmlTextWriterAttribute.Style, InputUtil.GetStyle(this.NegativeStyle, base.Style));
				}
			}
			base.SetDesignTimeAttributes(writer);
		}

		// Token: 0x0600C96F RID: 51567 RVA: 0x002CEB6D File Offset: 0x002CCD6D
		protected override void OnPreRender(EventArgs e)
		{
			if (this.MaxValue < this.MinValue)
			{
				throw new ArgumentOutOfRangeException("MinValue", "MinValue need to be less than MaxValue");
			}
			base.OnPreRender(e);
		}

		// Token: 0x0600C970 RID: 51568 RVA: 0x002CEB94 File Offset: 0x002CCD94
		protected override void SetStyleClasses()
		{
			base.SetStyleClasses();
			if (!base.EmptySkin)
			{
				this.NegativeStyle.CssClass = base.FormatCssClass("riTextBox riNegative", this.NegativeStyle.CssClass);
			}
		}

		// Token: 0x0600C971 RID: 51569 RVA: 0x002CEBC8 File Offset: 0x002CCDC8
		protected override string StylesToClient()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(InputUtil.GetStyle("HoveredStyle", base.HoveredStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("InvalidStyle", base.InvalidStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("DisabledStyle", base.DisabledStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("FocusedStyle", base.FocusedStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("EmptyMessageStyle", base.EmptyMessageStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("ReadOnlyStyle", base.ReadOnlyStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("EnabledStyle", base.EnabledStyle, base.Style) + ",");
			stringBuilder.Append(InputUtil.GetStyle("NegativeStyle", this.NegativeStyle, base.Style));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600C972 RID: 51570 RVA: 0x002CED27 File Offset: 0x002CCF27
		protected override void MergeAuxiliaryStyles()
		{
			base.MergeAuxiliaryStyles();
			this.NegativeStyle.MergeWith(base.EnabledStyle);
		}

		// Token: 0x0600C973 RID: 51571 RVA: 0x002CED40 File Offset: 0x002CCF40
		protected virtual string RangeTextProperty(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				double num;
				try
				{
					num = double.Parse(value, NumberFormatInfo.InvariantInfo);
				}
				catch (Exception ex)
				{
					throw new InvalidCastException("Text property cannot be set. " + ex.Message);
				}
				num = ((this.MaxValue < num) ? this.MaxValue : num);
				num = ((this.MinValue > num) ? this.MinValue : num);
				return num.ToString(NumberFormatInfo.InvariantInfo);
			}
			return null;
		}

		// Token: 0x0600C974 RID: 51572 RVA: 0x002CEDC0 File Offset: 0x002CCFC0
		private void RenderChildrenSingleInput(HtmlTextWriter writer)
		{
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				if (this.ShowButton || this.ShowSpinButtons)
				{
					this.ButtonsLightContainer.RenderControl(writer);
					return;
				}
			}
			else
			{
				if (this.ShowSpinButtons)
				{
					this.ButtonUpContainer.RenderControl(writer);
					this.ButtonDownContainer.RenderControl(writer);
				}
				if (this.ShowButton)
				{
					this.ButtonContainer.RenderControl(writer);
				}
			}
		}

		// Token: 0x0600C975 RID: 51573 RVA: 0x002CEE28 File Offset: 0x002CD028
		protected override string GetOffsetAdditionalClasses()
		{
			string text = "";
			if (this.ShowButton)
			{
				text += " riContButton ";
			}
			if (this.ShowSpinButtons)
			{
				text += " riContSpinButtons ";
			}
			if (text != "" && this.ButtonsPosition == InputButtonsPosition.Left)
			{
				text += "riButtonSwap ";
			}
			return text;
		}

		// Token: 0x0600C976 RID: 51574 RVA: 0x002CEE88 File Offset: 0x002CD088
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			if (this.EnableSingleInputRendering && !base.DesignMode)
			{
				this.RenderChildrenSingleInput(writer);
				return;
			}
			if (this.ShowSpinButtons)
			{
				writer.AddAttribute("class", "riSpin");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.ButtonUpContainer.RenderControl(writer);
				this.ButtonDownContainer.RenderControl(writer);
				writer.RenderEndTag();
			}
			if (this.ShowButton)
			{
				writer.AddAttribute("class", "riBtn");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.ButtonContainer.RenderControl(writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600C977 RID: 51575 RVA: 0x002CEF24 File Offset: 0x002CD124
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.DescribeNumberFormat(descriptor);
			descriptor.AddScriptProperty("incrementSettings", InputUtil.IncrementSettingsToClient(this.IncrementSettings));
			if (!this.AllowOutOfRangeAutoCorrect)
			{
				descriptor.AddProperty("_allowOutOfRangeAutoCorrect", this.AllowOutOfRangeAutoCorrect);
			}
		}

		// Token: 0x0600C978 RID: 51576 RVA: 0x002CEF74 File Offset: 0x002CD174
		private void DescribeNumberFormat(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new NumberFormatSettingsConverter()
			});
			descriptor.AddScriptProperty("numberFormat", javaScriptSerializer.Serialize(this.NumberFormat));
		}

		// Token: 0x0600C979 RID: 51577 RVA: 0x002CEFB4 File Offset: 0x002CD1B4
		private string ParseDbValueString(string sValue)
		{
			if (string.IsNullOrEmpty(sValue))
			{
				return null;
			}
			double num;
			bool flag = double.TryParse(sValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, this.Culture, out num);
			if (!flag)
			{
				flag = double.TryParse(sValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num);
			}
			if (!flag)
			{
				throw new FormatException("The string was not recognized as a valid format.");
			}
			num *= this.DbValueFactor;
			return num.ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600C97A RID: 51578 RVA: 0x002CF01C File Offset: 0x002CD21C
		protected override void LoadViewState(object savedState)
		{
			if (!this.EnableViewState)
			{
				return;
			}
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.NegativeStyle).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600C97B RID: 51579 RVA: 0x002CF060 File Offset: 0x002CD260
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool result = true;
			try
			{
				result = base.LoadPostData(postDataKey, postCollection);
			}
			catch (InvalidCastException)
			{
				this.Text = "";
			}
			return result;
		}

		// Token: 0x0600C97C RID: 51580 RVA: 0x002CF09C File Offset: 0x002CD29C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				(this.negativeStyle != null) ? ((IStateManager)this.negativeStyle).SaveViewState() : null
			};
		}

		// Token: 0x0600C97D RID: 51581 RVA: 0x002CF0D3 File Offset: 0x002CD2D3
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.negativeStyle != null)
			{
				((IStateManager)this.negativeStyle).TrackViewState();
			}
		}

		// Token: 0x0600C97E RID: 51582 RVA: 0x002CF0F0 File Offset: 0x002CD2F0
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			if (clientState.ContainsKey("minValue"))
			{
				this.MinValue = Convert.ToDouble(clientState["minValue"]);
			}
			if (clientState.ContainsKey("maxValue"))
			{
				this.MaxValue = Convert.ToDouble(clientState["maxValue"]);
			}
		}

		// Token: 0x1700412C RID: 16684
		// (get) Token: 0x0600C97F RID: 51583 RVA: 0x002CF14A File Offset: 0x002CD34A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual HtmlGenericControl ButtonDownContainer
		{
			get
			{
				if (this.buttonDownContainer == null)
				{
					this.buttonDownContainer = new HtmlGenericControl("a");
				}
				return this.buttonDownContainer;
			}
		}

		// Token: 0x1700412D RID: 16685
		// (get) Token: 0x0600C980 RID: 51584 RVA: 0x002CF16A File Offset: 0x002CD36A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual HtmlGenericControl ButtonUpContainer
		{
			get
			{
				if (this.buttonUpContainer == null)
				{
					this.buttonUpContainer = new HtmlGenericControl("a");
				}
				return this.buttonUpContainer;
			}
		}

		// Token: 0x0600C981 RID: 51585 RVA: 0x002CF18C File Offset: 0x002CD38C
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.ShowSpinButtons)
			{
				if (!string.IsNullOrEmpty(this.SpinUpCssClass))
				{
					this.ButtonUpContainer.Attributes["class"] = "riUp " + HttpUtility.HtmlEncode(this.SpinUpCssClass);
				}
				else
				{
					this.ButtonUpContainer.Attributes["class"] = "riUp";
				}
				this.ButtonUpContainer.Attributes["href"] = "javascript:void(0)";
				this.ButtonUpContainer.Attributes["id"] = this.ClientID + "_SpinUpButton";
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
				htmlGenericControl.InnerHtml = "Spin Up";
				this.ButtonUpContainer.Controls.Add(htmlGenericControl);
				if (!string.IsNullOrEmpty(this.SpinDownCssClass))
				{
					this.ButtonDownContainer.Attributes["class"] = "riDown " + HttpUtility.HtmlEncode(this.SpinDownCssClass);
				}
				else
				{
					this.ButtonDownContainer.Attributes["class"] = "riDown";
				}
				this.ButtonDownContainer.Attributes["href"] = "javascript:void(0)";
				this.ButtonDownContainer.Attributes["id"] = this.ClientID + "_SpinDownButton";
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
				htmlGenericControl2.InnerHtml = "Spin Down";
				this.ButtonDownContainer.Controls.Add(htmlGenericControl2);
				if (this.ResolvedRenderMode == RenderMode.Lightweight)
				{
					this.ButtonsLightContainer.Controls.Add(this.ButtonUpContainer);
					this.ButtonsLightContainer.Controls.Add(this.ButtonDownContainer);
					this.Controls.Add(this.ButtonsLightContainer);
				}
				else
				{
					this.Controls.Add(this.ButtonUpContainer);
					this.Controls.Add(this.ButtonDownContainer);
				}
			}
			if (this.ShowButton)
			{
				if (!string.IsNullOrEmpty(this.ButtonCssClass))
				{
					this.ButtonContainer.Attributes["class"] = "riButton " + HttpUtility.HtmlEncode(this.ButtonCssClass);
				}
				else
				{
					this.ButtonContainer.Attributes["class"] = "riButton";
				}
				this.ButtonContainer.Attributes["href"] = "javascript:void(0)";
				this.ButtonContainer.Attributes["id"] = this.ClientID + "_GoButton";
				HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("span");
				htmlGenericControl3.InnerHtml = "Button";
				this.ButtonContainer.Controls.Add(htmlGenericControl3);
				if (this.ResolvedRenderMode == RenderMode.Lightweight)
				{
					this.ButtonsLightContainer.Controls.Add(this.ButtonContainer);
					this.Controls.Add(this.ButtonsLightContainer);
				}
				else
				{
					this.Controls.Add(this.ButtonContainer);
				}
			}
			this.OnChildrenCreated();
		}

		// Token: 0x0600C982 RID: 51586 RVA: 0x002CF498 File Offset: 0x002CD698
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<double>(descriptor, "maxValue", this.MaxValue, 70368744177664.0);
			base.DescribeProperty<double>(descriptor, "minValue", this.MinValue, -70368744177664.0);
			base.DescribeProperty<object>(descriptor, "selectionOnFocus", this.SelectionOnFocus, Enum.Parse(typeof(SelectionOnFocus), "SelectAll"));
			base.DescribeProperty<bool>(descriptor, "showSpinButtons", this.ShowSpinButtons, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600C983 RID: 51587 RVA: 0x002CF520 File Offset: 0x002CD720
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040034E3 RID: 13539
		private NumberFormatSettings numberFormat;

		// Token: 0x040034E4 RID: 13540
		private InputIncrementSettings incrementSettings;

		// Token: 0x040034E5 RID: 13541
		private InputStyle negativeStyle;

		// Token: 0x040034E6 RID: 13542
		private string _displayText = string.Empty;

		// Token: 0x040034E7 RID: 13543
		private HtmlGenericControl buttonUpContainer;

		// Token: 0x040034E8 RID: 13544
		private HtmlGenericControl buttonDownContainer;
	}
}
