using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Calendar;

namespace Telerik.Web.UI
{
	// Token: 0x0200100C RID: 4108
	[TelerikToolboxCategory("Date/Color Picker")]
	[EmbeddedSkin("TimePicker", "WebBlue", typeof(RadTimePicker))]
	[EmbeddedSkin("TimePicker", "Windows7", typeof(RadTimePicker))]
	[ToolboxBitmap(typeof(RadTimePicker), "Telerik.Web.UI.TimePicker.png")]
	[ToolboxData("<{0}:RadTimePicker Runat=\"server\"></{0}:RadTimePicker>")]
	[EmbeddedSkin("TimePicker", typeof(RadTimePicker))]
	[EmbeddedSkin("TimePicker", "Web20", typeof(RadTimePicker))]
	[ValidationProperty("ValidationDate")]
	[DefaultEvent("SelectedDateChanged")]
	[DefaultProperty("SelectedDate")]
	[Description("Telerik RadCalendar")]
	[ControlValueProperty("SelectedDate")]
	[LightweightRendering]
	[EmbeddedSkin("TimePicker", "Vista", typeof(RadTimePicker))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadDateTimePicker", "Telerik.Web.UI.Calendar.RadCalendarCommonScript.js")]
	[ClientScriptResource("Telerik.Web.UI.RadDateTimePicker", "Telerik.Web.UI.Calendar.RadDateTimePickerScript.js")]
	[Designer("Telerik.Web.Design.RadTimePickerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[EmbeddedSkin("TimePicker", "Black", typeof(RadTimePicker))]
	[EmbeddedSkin("TimePicker", "Default", typeof(RadTimePicker))]
	[EmbeddedSkin("TimePicker", "Simple", typeof(RadTimePicker))]
	[EmbeddedSkin("TimePicker", "Office2007", typeof(RadTimePicker))]
	[EmbeddedSkin("TimePicker", "Outlook", typeof(RadTimePicker))]
	[EmbeddedSkin("TimePicker", "Telerik", typeof(RadTimePicker))]
	[EmbeddedSkin("TimePicker", "Sunset", typeof(RadTimePicker))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadTimePicker : RadDateTimePicker
	{
		// Token: 0x170032EE RID: 13038
		// (get) Token: 0x0600A10F RID: 41231 RVA: 0x0023D452 File Offset: 0x0023B652
		[Browsable(false)]
		[Obsolete("Please don't touch Calendar property")]
		[Description("Please don't touch Calendar property")]
		public override RadCalendar Calendar
		{
			get
			{
				return base.Calendar;
			}
		}

		// Token: 0x170032EF RID: 13039
		// (get) Token: 0x0600A110 RID: 41232 RVA: 0x0023D45A File Offset: 0x0023B65A
		[Description("Please don't touch PopupButton property")]
		[Obsolete("Please don't touch PopupButton property")]
		[Browsable(false)]
		public override CalendarPopupButton DatePopupButton
		{
			get
			{
				return base.DatePopupButton;
			}
		}

		// Token: 0x170032F0 RID: 13040
		// (get) Token: 0x0600A111 RID: 41233 RVA: 0x0023D462 File Offset: 0x0023B662
		// (set) Token: 0x0600A112 RID: 41234 RVA: 0x0023D46A File Offset: 0x0023B66A
		[Description("Please don't touch SharedCalendarID property")]
		[Obsolete("Please don't touch SharedCalendarID property")]
		[Browsable(false)]
		public override string SharedCalendarID
		{
			get
			{
				return base.SharedCalendarID;
			}
			set
			{
				base.SharedCalendarID = value;
			}
		}

		// Token: 0x170032F1 RID: 13041
		// (get) Token: 0x0600A113 RID: 41235 RVA: 0x0023D473 File Offset: 0x0023B673
		// (set) Token: 0x0600A114 RID: 41236 RVA: 0x0023D47E File Offset: 0x0023B67E
		[Browsable(true)]
		public override bool AutoPostBack
		{
			get
			{
				return this.AutoPostBackControl == AutoPostBackControl.TimeView;
			}
			set
			{
				if (value)
				{
					this.AutoPostBackControl = AutoPostBackControl.TimeView;
					return;
				}
				this.AutoPostBackControl = AutoPostBackControl.None;
			}
		}

		// Token: 0x170032F2 RID: 13042
		// (get) Token: 0x0600A115 RID: 41237 RVA: 0x0023D492 File Offset: 0x0023B692
		// (set) Token: 0x0600A116 RID: 41238 RVA: 0x0023D49A File Offset: 0x0023B69A
		[Browsable(false)]
		public override AutoPostBackControl AutoPostBackControl
		{
			get
			{
				return base.AutoPostBackControl;
			}
			set
			{
				base.AutoPostBackControl = value;
			}
		}

		// Token: 0x170032F3 RID: 13043
		// (get) Token: 0x0600A117 RID: 41239 RVA: 0x0023D4A3 File Offset: 0x0023B6A3
		// (set) Token: 0x0600A118 RID: 41240 RVA: 0x0023D4CE File Offset: 0x0023B6CE
		[Description("Change the returning type of DbSelectedDate to TimeSpan instead of DateTime")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public bool UseTimeSpanForBinding
		{
			get
			{
				return this.ViewState["UseTimeSpanForBinding"] != null && (bool)this.ViewState["UseTimeSpanForBinding"];
			}
			set
			{
				this.ViewState["UseTimeSpanForBinding"] = value;
			}
		}

		// Token: 0x170032F4 RID: 13044
		// (get) Token: 0x0600A119 RID: 41241 RVA: 0x0023D4E6 File Offset: 0x0023B6E6
		// (set) Token: 0x0600A11A RID: 41242 RVA: 0x0023D507 File Offset: 0x0023B707
		[Bindable(true, BindingDirection.TwoWay)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[Description("The currently selected date, boxed in object")]
		[TypeConverter(typeof(DateTimeConverter))]
		public override object DbSelectedDate
		{
			get
			{
				if (this.UseTimeSpanForBinding)
				{
					return this.SelectedTime;
				}
				return this.SelectedDate;
			}
			set
			{
				if (value is TimeSpan)
				{
					this.SelectedTime = new TimeSpan?((TimeSpan)value);
					return;
				}
				base.DbSelectedDate = value;
			}
		}

		// Token: 0x170032F5 RID: 13045
		// (get) Token: 0x0600A11B RID: 41243 RVA: 0x0023D52C File Offset: 0x0023B72C
		// (set) Token: 0x0600A11C RID: 41244 RVA: 0x0023D570 File Offset: 0x0023B770
		[Browsable(true)]
		public virtual TimeSpan? SelectedTime
		{
			get
			{
				if (this.SelectedDate != null)
				{
					return new TimeSpan?(this.SelectedDate.Value.TimeOfDay);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.SelectedDate = null;
					return;
				}
				if (this.SelectedDate != null)
				{
					this.SelectedDate = new DateTime?(this.SelectedDate.Value.Date.Add(value.Value));
					return;
				}
				this.SelectedDate = new DateTime?(DateTime.Today.Date.Add(value.Value));
			}
		}

		// Token: 0x170032F6 RID: 13046
		// (get) Token: 0x0600A11D RID: 41245 RVA: 0x0023D604 File Offset: 0x0023B804
		// (set) Token: 0x0600A11E RID: 41246 RVA: 0x0023D62D File Offset: 0x0023B82D
		[DefaultValue(false)]
		[Description("Enable client side navigation with keyboard")]
		public override bool EnableKeyboardNavigation
		{
			get
			{
				object obj = this.ViewState["EnableKeyboardNavigation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableKeyboardNavigation"] = value;
				this.TimeView.EnableKeyboardNavigation = value;
			}
		}

		// Token: 0x0600A11F RID: 41247 RVA: 0x0023D654 File Offset: 0x0023B854
		protected override void CreateTimeControls()
		{
			base.CreateTimeControls();
			if (this.DateInput.DisplayDateFormat == this.DateInput.Culture.DateTimeFormat.ShortDatePattern + " " + this.DateInput.Culture.DateTimeFormat.ShortTimePattern)
			{
				this.DateInput.DisplayDateFormat = "t";
			}
			if (this.DateInput.DateFormat == this.DateInput.Culture.DateTimeFormat.ShortDatePattern + " " + this.DateInput.Culture.DateTimeFormat.ShortTimePattern)
			{
				this.DateInput.DateFormat = "t";
			}
			this.DatePopupButton.Visible = false;
			this.Calendar.Visible = false;
		}

		// Token: 0x0600A120 RID: 41248 RVA: 0x0023D72B File Offset: 0x0023B92B
		protected override void SetDefaultSize()
		{
			this.defaultWidth = Unit.Pixel(160);
		}

		// Token: 0x170032F7 RID: 13047
		// (get) Token: 0x0600A121 RID: 41249 RVA: 0x0023D73D File Offset: 0x0023B93D
		protected override string CssClassFormatString
		{
			get
			{
				if (!string.IsNullOrEmpty(base.RuntimeSkin))
				{
					return "RadPicker RadTimePicker RadPicker_{0}";
				}
				return "RadPicker RadTimePicker";
			}
		}

		// Token: 0x0600A122 RID: 41250 RVA: 0x0023D758 File Offset: 0x0023B958
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			bool visible = this.TimeView.Visible;
			if (base.DesignMode)
			{
				this.TimeView.Visible = false;
			}
			base.RenderChildren(writer);
			this.TimeView.Visible = visible;
		}

		// Token: 0x0600A123 RID: 41251 RVA: 0x0023D798 File Offset: 0x0023B998
		protected override void AddControlComponents(HtmlTextWriter writer)
		{
			if (base.IsLightweightRendering)
			{
				if (!string.IsNullOrEmpty(this.DateInput.Label))
				{
					if (!string.IsNullOrEmpty(this.DateInput.LabelCssClass))
					{
						writer.AddAttribute("class", this.DateInput.LabelCssClass);
					}
					writer.AddAttribute("for", this.DateInput.ClientID);
					writer.AddAttribute("id", this.DateInput.ClientID + "_Label");
					writer.RenderBeginTag("label");
					writer.Write(this.DateInput.Label);
					writer.RenderEndTag();
				}
				if (this.DateInput.Visible)
				{
					this.DateInput.RenderControl(writer);
					return;
				}
			}
			else
			{
				if (this.DateInput.Visible)
				{
					writer.AddStyleAttribute("width", "100%");
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcInputCell");
					if (this.ShouldRenderAdditionalControls && base.Browser.IsBrowser("IE"))
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "4px");
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					this.DateInput.RenderControl(writer);
					writer.RenderEndTag();
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				if (!this.isOnlyInputRendered())
				{
					this.TimePopupButton.RenderControl(writer);
				}
				this.TimeView.RenderControl(writer);
				writer.RenderEndTag();
				this.AddAdditionalControlComponents(writer);
			}
		}

		// Token: 0x0600A124 RID: 41252 RVA: 0x0023D902 File Offset: 0x0023BB02
		protected internal override bool isOnlyInputRendered()
		{
			return !this.TimePopupButton.Visible && this.Controls.Count == 5;
		}

		// Token: 0x0600A125 RID: 41253 RVA: 0x0023D924 File Offset: 0x0023BB24
		protected override void DescribeProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperties(descriptor);
			if (this != null)
			{
				string script = string.Format("{{ShowAnimationDuration:{0},ShowAnimationType:{1},HideAnimationDuration:{2},HideAnimationType:{3}}}", new object[]
				{
					base.ShowAnimation.Duration,
					(int)base.ShowAnimation.Type,
					(this.AutoPostBack || base.SharedTimeView != null) ? 0 : base.HideAnimation.Duration,
					(int)base.HideAnimation.Type
				});
				descriptor.AddScriptProperty("_animationSettings", script);
				descriptor.AddProperty("_enableKeyboardNavigation", this.EnableKeyboardNavigation);
			}
		}
	}
}
