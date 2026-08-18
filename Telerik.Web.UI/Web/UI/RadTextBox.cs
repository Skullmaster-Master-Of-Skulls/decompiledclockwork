using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020012C5 RID: 4805
	[Designer("Telerik.Web.Design.TextBoxControlDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadTextBox Runat=server></{0}:RadTextBox>")]
	[ControlBuilder(typeof(TextBoxControlBuilder))]
	[LightweightRendering]
	[Description("Telerik RadInput")]
	[ClientScriptResource("Telerik.Web.UI.RadTextBox", "Telerik.Web.UI.Input.TextBox.RadInputScript.js")]
	[ValidationProperty("ValidationText")]
	[SupportsEventValidation]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ControlValueProperty("Text")]
	[DefaultEvent("TextChanged")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[ToolboxBitmap(typeof(RadTextBox), "Telerik.Web.UI.Input.png")]
	[TelerikToolboxCategory("Data Editing")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadTextBox : RadInputControl
	{
		// Token: 0x1700412E RID: 16686
		// (get) Token: 0x0600C985 RID: 51589 RVA: 0x002CF53C File Offset: 0x002CD73C
		// (set) Token: 0x0600C986 RID: 51590 RVA: 0x002CF567 File Offset: 0x002CD767
		[NotifyParentProperty(true)]
		[DefaultValue(InputMode.SingleLine)]
		[Category("Behavior")]
		[Description("Single-line or multiline mode.")]
		public InputMode TextMode
		{
			get
			{
				if (this.ViewState["TextMode"] == null)
				{
					return InputMode.SingleLine;
				}
				return (InputMode)this.ViewState["TextMode"];
			}
			set
			{
				this.ViewState["TextMode"] = value;
			}
		}

		// Token: 0x1700412F RID: 16687
		// (get) Token: 0x0600C987 RID: 51591 RVA: 0x002CF580 File Offset: 0x002CD780
		// (set) Token: 0x0600C988 RID: 51592 RVA: 0x002CF5A9 File Offset: 0x002CD7A9
		[Themeable(false)]
		[DefaultValue(2)]
		[Category("Behavior")]
		[Description("Gets or sets the number of rows displayed in a multiline RadTextBox.")]
		public virtual int Rows
		{
			get
			{
				object obj = this.ViewState["Rows"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 2;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Rows", "");
				}
				this.ViewState["Rows"] = value;
			}
		}

		// Token: 0x17004130 RID: 16688
		// (get) Token: 0x0600C989 RID: 51593 RVA: 0x002CF5D8 File Offset: 0x002CD7D8
		// (set) Token: 0x0600C98A RID: 51594 RVA: 0x002CF602 File Offset: 0x002CD802
		[Description("")]
		[Category("Appearance")]
		[DefaultValue(20)]
		public virtual int Columns
		{
			get
			{
				object obj = this.ViewState["Columns"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 20;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Columns", "");
				}
				this.ViewState["Columns"] = value;
			}
		}

		// Token: 0x17004131 RID: 16689
		// (get) Token: 0x0600C98B RID: 51595 RVA: 0x002CF630 File Offset: 0x002CD830
		// (set) Token: 0x0600C98C RID: 51596 RVA: 0x002CF659 File Offset: 0x002CD859
		[Description("TextBox_Wrap")]
		[Category("Layout")]
		[DefaultValue(true)]
		public virtual bool Wrap
		{
			get
			{
				object obj = this.ViewState["Wrap"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Wrap"] = value;
			}
		}

		// Token: 0x17004132 RID: 16690
		// (get) Token: 0x0600C98D RID: 51597 RVA: 0x002CF674 File Offset: 0x002CD874
		// (set) Token: 0x0600C98E RID: 51598 RVA: 0x002CF69D File Offset: 0x002CD89D
		[Description("Get or sets the specific HTML input type that will be rendered in the control")]
		[DefaultValue(Html5InputType.Text)]
		[Category("Layout")]
		public virtual Html5InputType InputType
		{
			get
			{
				object obj = this.ViewState["InputType"];
				if (obj != null)
				{
					return (Html5InputType)obj;
				}
				return Html5InputType.Text;
			}
			set
			{
				this.ViewState["InputType"] = value;
			}
		}

		// Token: 0x17004133 RID: 16691
		// (get) Token: 0x0600C98F RID: 51599 RVA: 0x002CF6B5 File Offset: 0x002CD8B5
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.TextMode == InputMode.MultiLine)
				{
					return HtmlTextWriterTag.Textarea;
				}
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x0600C990 RID: 51600 RVA: 0x002CF6C5 File Offset: 0x002CD8C5
		protected override bool IsMultiLine()
		{
			return this.TextMode == InputMode.MultiLine;
		}

		// Token: 0x0600C991 RID: 51601 RVA: 0x002CF6D0 File Offset: 0x002CD8D0
		private Unit GetPasswordStrengthIndicatorWidth()
		{
			if (this.PasswordStrengthSettings.ShowIndicator)
			{
				return this.PasswordStrengthSettings.IndicatorWidth;
			}
			return Unit.Empty;
		}

		// Token: 0x0600C992 RID: 51602 RVA: 0x002CF6F0 File Offset: 0x002CD8F0
		protected override void SetDefaultSize()
		{
			if (!this.EnableSingleInputRendering || base.DesignMode)
			{
				if (this.isOnlyInputRendered() && !base.DesignMode)
				{
					this.defaultWidth = ((this.Columns != 20) ? Unit.Empty : Unit.Pixel(160));
				}
				else if (this.Columns == 20)
				{
					this.defaultWidth = Unit.Pixel(160);
				}
				else
				{
					this.defaultWidth = Unit.Parse(((int)((double)this.Columns * 8.25) + 22).ToString());
				}
				base.SetDefaultSize();
				return;
			}
			if (this.ViewState["Columns"] != null)
			{
				this.defaultWidth = Unit.Empty;
				return;
			}
			base.SetDefaultSize();
		}

		// Token: 0x0600C993 RID: 51603 RVA: 0x002CF7B0 File Offset: 0x002CD9B0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this.TextMode == InputMode.MultiLine && this.MaxLength > 0)
			{
				descriptor.AddProperty("maxLength", this.MaxLength);
			}
			if (this.TextMode == InputMode.SingleLine && this.InputType != Html5InputType.Text)
			{
				descriptor.AddProperty("inputType", this.InputType);
			}
			if (this.Resize != ResizeMode.None && this.TextMode == InputMode.MultiLine)
			{
				descriptor.AddProperty("_resizeMode", this.Resize.ToString().ToLowerInvariant());
			}
			if (this.PasswordStrengthSettings.ShowIndicator)
			{
				descriptor.AddScriptProperty("passwordSettings", InputUtil.PasswordStrengthSettingsToClient(this.PasswordStrengthSettings));
				if (this.PasswordStrengthSettings.OnClientPasswordStrengthCalculating != "")
				{
					descriptor.AddEvent("passwordStrengthCalculating", this.PasswordStrengthSettings.OnClientPasswordStrengthCalculating);
				}
			}
		}

		// Token: 0x0600C994 RID: 51604 RVA: 0x002CF894 File Offset: 0x002CDA94
		protected override Unit CalculateInputWidth()
		{
			Unit unit = base.CalculateInputWidth();
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return Unit.Empty;
			}
			if (unit.Type == UnitType.Pixel)
			{
				if (this.GetPasswordStrengthIndicatorWidth().Type == UnitType.Percentage)
				{
					this.PasswordStrengthSettings.IndicatorWidth = Unit.Pixel((int)this.PasswordStrengthSettings.IndicatorWidth.Value * (int)base.CalculateWrapperWidth().Value / 100);
				}
				else if (this.GetPasswordStrengthIndicatorWidth().Type != UnitType.Pixel)
				{
					this.PasswordStrengthSettings.IndicatorWidth = Unit.Pixel(100);
				}
				int num = (int)unit.Value - (int)this.GetPasswordStrengthIndicatorWidth().Value;
				if (num < 0)
				{
					num = 0;
				}
				return Unit.Pixel(num);
			}
			if (unit.Type == UnitType.Em)
			{
				if (this.GetPasswordStrengthIndicatorWidth().Type == UnitType.Percentage)
				{
					this.PasswordStrengthSettings.IndicatorWidth = new Unit((double)((int)this.PasswordStrengthSettings.IndicatorWidth.Value * (int)base.CalculateWrapperWidth().Value / 100), UnitType.Em);
				}
				else if (this.GetPasswordStrengthIndicatorWidth().Type != UnitType.Pixel)
				{
					this.PasswordStrengthSettings.IndicatorWidth = Unit.Pixel(100);
				}
				float num2 = (float)unit.Value - (float)((int)this.GetPasswordStrengthIndicatorWidth().Value);
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				return new Unit((double)num2, UnitType.Em);
			}
			if (this.GetPasswordStrengthIndicatorWidth().Type != UnitType.Percentage)
			{
				this.PasswordStrengthSettings.IndicatorWidth = Unit.Percentage(30.0);
			}
			int num3 = (int)unit.Value - (int)this.GetPasswordStrengthIndicatorWidth().Value;
			if (num3 < 0)
			{
				num3 = 0;
			}
			return Unit.Percentage((double)num3);
		}

		// Token: 0x0600C995 RID: 51605 RVA: 0x002CFA60 File Offset: 0x002CDC60
		protected override void RenderContentsSingleInput(HtmlTextWriter writer)
		{
			this.RenderLabel(writer, this.ClientID);
			if (!string.IsNullOrEmpty(this.Label) || this.PasswordStrengthSettings.ShowIndicator)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "riContentWrapper");
				if (this.ResolvedRenderMode != RenderMode.Lightweight)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.CalculateInputWidth().ToString(CultureInfo.InvariantCulture));
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			this.RenderInnerWrapperContent(writer);
			if (!string.IsNullOrEmpty(this.Label) || this.PasswordStrengthSettings.ShowIndicator)
			{
				writer.RenderEndTag();
			}
			this.RenderPasswordStrengthIndicator(writer, false);
		}

		// Token: 0x17004134 RID: 16692
		// (get) Token: 0x0600C996 RID: 51606 RVA: 0x002CFAFC File Offset: 0x002CDCFC
		protected override bool Resizable
		{
			get
			{
				return this.Resize != ResizeMode.None;
			}
		}

		// Token: 0x0600C997 RID: 51607 RVA: 0x002CFB0C File Offset: 0x002CDD0C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (!this.IsMultiLine() && this.TextMode != InputMode.Password && !base.DesignMode)
			{
				base.Attributes[HtmlTextWriterAttribute.Value.ToString().ToLower()] = this.DisplayText;
			}
			switch (this.TextMode)
			{
			case InputMode.SingleLine:
				if (!base.DesignMode)
				{
					base.Attributes[HtmlTextWriterAttribute.Type.ToString().ToLower()] = this.GetInputTypeAttribute();
				}
				if (this.Columns > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Size, this.Columns.ToString(NumberFormatInfo.InvariantInfo));
				}
				break;
			case InputMode.MultiLine:
				writer.AddAttribute(HtmlTextWriterAttribute.Rows, this.Rows.ToString(NumberFormatInfo.InvariantInfo));
				writer.AddAttribute(HtmlTextWriterAttribute.Cols, this.Columns.ToString(NumberFormatInfo.InvariantInfo));
				if (!this.Wrap)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Wrap, "off");
				}
				break;
			case InputMode.Password:
				base.Attributes[HtmlTextWriterAttribute.Type.ToString().ToLower()] = "password";
				base.Attributes.Remove(HtmlTextWriterAttribute.Value.ToString().ToLower());
				if (this.Columns > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Size, this.Columns.ToString(NumberFormatInfo.InvariantInfo));
				}
				break;
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600C998 RID: 51608 RVA: 0x002CFC84 File Offset: 0x002CDE84
		private string GetInputTypeAttribute()
		{
			switch (this.InputType)
			{
			case Html5InputType.Date:
				return "date";
			case Html5InputType.DateTime:
				return "datetime";
			case Html5InputType.Number:
				return "number";
			case Html5InputType.Time:
				return "time";
			case Html5InputType.DateTimeLocal:
				return "datetime-local";
			case Html5InputType.Month:
				return "month";
			case Html5InputType.Week:
				return "week";
			case Html5InputType.Range:
				return "range";
			case Html5InputType.Email:
				return "email";
			case Html5InputType.Url:
				return "url";
			case Html5InputType.Search:
				return "search";
			case Html5InputType.Tel:
				return "tel";
			case Html5InputType.Color:
				return "color";
			}
			return "text";
		}

		// Token: 0x0600C999 RID: 51609 RVA: 0x002CFD2B File Offset: 0x002CDF2B
		protected override void DescribeValueAndTextProperties(IScriptDescriptor descriptor)
		{
			if (this.TextMode != InputMode.Password)
			{
				base.DescribeValueAndTextProperties(descriptor);
			}
		}

		// Token: 0x0600C99A RID: 51610 RVA: 0x002CFD40 File Offset: 0x002CDF40
		protected override void RenderBeginTagSingleInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_wrapper");
			if (!base.Display)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			string absolutePositionValue = InputUtil.GetAbsolutePositionValue(base.Style);
			if (!string.IsNullOrEmpty(absolutePositionValue))
			{
				writer.AddAttribute("style", absolutePositionValue);
			}
			string str = "riSingle ";
			string str2 = string.Empty;
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				str = string.Empty;
				if (this.PasswordStrengthSettings.ShowIndicator && this.PasswordStrengthSettings.IndicatorElementID == "")
				{
					str2 = "riPassIndicator ";
				}
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, str + str2 + this.GetOffsetAdditionalClasses() + base.FormatCssClass("RadInput", this.CssClass));
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "static");
				this.SetDefaultSize();
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.CalculateWrapperWidth().ToString(CultureInfo.InvariantCulture));
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString(CultureInfo.InvariantCulture));
				writer.AddStyleAttribute(HtmlTextWriterStyle.MarginRight, "15px");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				return;
			}
			if (this.ResolvedRenderMode != RenderMode.Lightweight)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.CalculateWrapperWidth().ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				if (!base.EnabledStyle.Width.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.EnabledStyle.Width.ToString(CultureInfo.InvariantCulture));
				}
				if (!base.EnabledStyle.Height.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, base.EnabledStyle.Height.ToString(CultureInfo.InvariantCulture));
				}
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
		}

		// Token: 0x0600C99B RID: 51611 RVA: 0x002CFF18 File Offset: 0x002CE118
		protected override void RenderPasswordStrengthIndicator(HtmlTextWriter writer, bool inTable)
		{
			if (this.PasswordStrengthSettings.ShowIndicator && this.PasswordStrengthSettings.IndicatorElementID == "")
			{
				if (inTable)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.AddAttribute("id", this.ClientID + "_passwordStrengthIndicator");
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					writer.Write("&nbsp;");
					writer.RenderEndTag();
					writer.RenderEndTag();
					return;
				}
				writer.AddAttribute("id", this.ClientID + "_passwordStrengthIndicator");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.PasswordStrengthSettings.IndicatorWidth.ToString(CultureInfo.InvariantCulture));
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
			}
		}

		// Token: 0x17004135 RID: 16693
		// (get) Token: 0x0600C99C RID: 51612 RVA: 0x002CFFE9 File Offset: 0x002CE1E9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public InputPasswordStrengthSettings PasswordStrengthSettings
		{
			get
			{
				if (this.passwordStrengthSettings == null)
				{
					this.passwordStrengthSettings = new InputPasswordStrengthSettings(this.ViewState);
				}
				return this.passwordStrengthSettings;
			}
		}

		// Token: 0x17004136 RID: 16694
		// (get) Token: 0x0600C99D RID: 51613 RVA: 0x002D000A File Offset: 0x002CE20A
		// (set) Token: 0x0600C99E RID: 51614 RVA: 0x002D0035 File Offset: 0x002CE235
		[Category("Client")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("set or get the ResizeMode of RadTextBox")]
		public ResizeMode Resize
		{
			get
			{
				if (this.ViewState["Resize"] == null)
				{
					return ResizeMode.None;
				}
				return (ResizeMode)this.ViewState["Resize"];
			}
			set
			{
				this.ViewState["Resize"] = value;
			}
		}

		// Token: 0x0600C99F RID: 51615 RVA: 0x002D004D File Offset: 0x002CE24D
		protected override void ControlPreRender()
		{
			base.EnabledStyle.Resize = this.Resize;
			base.ControlPreRender();
		}

		// Token: 0x0600C9A0 RID: 51616 RVA: 0x002D0066 File Offset: 0x002CE266
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600C9A1 RID: 51617 RVA: 0x002D006F File Offset: 0x002CE26F
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040034EE RID: 13550
		private const int _defaultColumnsValue = 20;

		// Token: 0x040034EF RID: 13551
		private const int _defaultRowsValue = 2;

		// Token: 0x040034F0 RID: 13552
		private InputPasswordStrengthSettings passwordStrengthSettings;
	}
}
