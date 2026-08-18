using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Calendar;

namespace Telerik.Web.UI
{
	// Token: 0x02001009 RID: 4105
	[DefaultEvent("SelectedDateChanged")]
	[ValidationProperty("ValidationDate")]
	[EmbeddedSkin("DatePicker", typeof(RadDatePicker))]
	[DefaultProperty("SelectedDate")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadDatePicker))]
	[Description("Telerik RadCalendar")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ControlValueProperty("SelectedDate")]
	[ClientScriptResource("Telerik.Web.UI.RadDatePicker", "Telerik.Web.UI.Calendar.RadDatePicker.js")]
	[ClientScriptResource("Telerik.Web.UI.RadDatePicker", "Telerik.Web.UI.Calendar.RadPickersPopupDirectionEnumeration.js")]
	[ClientScriptResource("Telerik.Web.UI.RadDatePicker", "Telerik.Web.UI.Common.Navigation.OverlayScript.js")]
	[RequiredScript(typeof(jQuery))]
	[RequiredScript(typeof(MaterialRipple))]
	[LightweightRendering]
	[EmbeddedSkin("DatePicker", "Default", typeof(RadDatePicker))]
	[PersistChildren(false)]
	[ToolboxData("<{0}:RadDatePicker Runat=\"server\"></{0}:RadDatePicker>")]
	[TelerikToolboxCategory("Date/Color Picker")]
	[ToolboxBitmap(typeof(RadDatePicker), "Telerik.Web.UI.DatePicker.png")]
	[Designer("Telerik.Web.Design.RadDatePickerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ParseChildren(true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadDatePicker : RadWebControl, INamingContainer, ILocalizableControl, IPostBackEventHandler, IPostBackDataHandler, ILabelableControl
	{
		// Token: 0x170032AA RID: 12970
		// (get) Token: 0x0600A055 RID: 41045 RVA: 0x0023A989 File Offset: 0x00238B89
		internal DatePickerStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new DatePickerStrings(new LocalizationProvider("RadDatePicker.Main", this, base.DesignMode ? "" : this.LocalizationPath));
				}
				return this._localization;
			}
		}

		// Token: 0x170032AB RID: 12971
		// (get) Token: 0x0600A056 RID: 41046 RVA: 0x0023A9C4 File Offset: 0x00238BC4
		// (set) Token: 0x0600A057 RID: 41047 RVA: 0x0023A9E4 File Offset: 0x00238BE4
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where RadDatePicker will look for its .resx localization files.")]
		[Category("Misc")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x170032AC RID: 12972
		// (get) Token: 0x0600A058 RID: 41048 RVA: 0x0023AA37 File Offset: 0x00238C37
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170032AD RID: 12973
		// (get) Token: 0x0600A059 RID: 41049 RVA: 0x0023AA3A File Offset: 0x00238C3A
		// (set) Token: 0x0600A05A RID: 41050 RVA: 0x0023AA42 File Offset: 0x00238C42
		[NotifyParentProperty(true)]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				base.Skin = value;
			}
		}

		// Token: 0x170032AE RID: 12974
		// (get) Token: 0x0600A05B RID: 41051 RVA: 0x0023AA4C File Offset: 0x00238C4C
		// (set) Token: 0x0600A05C RID: 41052 RVA: 0x0023AA75 File Offset: 0x00238C75
		[Description("Enable client side navigation with keyboard")]
		[DefaultValue(false)]
		public virtual bool EnableKeyboardNavigation
		{
			get
			{
				object obj = this.ViewState["EnableKeyboardNavigation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableKeyboardNavigation"] = value;
				this.Calendar.EnableKeyboardNavigation = value;
			}
		}

		// Token: 0x170032AF RID: 12975
		// (get) Token: 0x0600A05D RID: 41053 RVA: 0x0023AA99 File Offset: 0x00238C99
		// (set) Token: 0x0600A05E RID: 41054 RVA: 0x0023AAC8 File Offset: 0x00238CC8
		[Description("Specifies default path for the grid images when EnableEmbeddedSkins is set to false.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public virtual string ImagesPath
		{
			get
			{
				if (this.ViewState["ImagesPath"] == null)
				{
					return "";
				}
				return (string)this.ViewState["ImagesPath"];
			}
			set
			{
				this.Calendar.ImagesPath = value;
				this.ViewState["ImagesPath"] = value;
			}
		}

		// Token: 0x170032B0 RID: 12976
		// (get) Token: 0x0600A05F RID: 41055 RVA: 0x0023AAE7 File Offset: 0x00238CE7
		// (set) Token: 0x0600A060 RID: 41056 RVA: 0x0023AAEF File Offset: 0x00238CEF
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return base.EnableEmbeddedSkins;
			}
			set
			{
				this.Calendar.EnableEmbeddedSkins = value;
				this.DateInput.EnableEmbeddedSkins = value;
				base.EnableEmbeddedSkins = value;
			}
		}

		// Token: 0x170032B1 RID: 12977
		// (get) Token: 0x0600A061 RID: 41057 RVA: 0x0023AB10 File Offset: 0x00238D10
		// (set) Token: 0x0600A062 RID: 41058 RVA: 0x0023AB18 File Offset: 0x00238D18
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedScripts
		{
			get
			{
				return base.EnableEmbeddedScripts;
			}
			set
			{
				this.Calendar.EnableEmbeddedScripts = value;
				this.DateInput.EnableEmbeddedScripts = value;
				base.EnableEmbeddedScripts = value;
			}
		}

		// Token: 0x170032B2 RID: 12978
		// (get) Token: 0x0600A063 RID: 41059 RVA: 0x0023AB39 File Offset: 0x00238D39
		// (set) Token: 0x0600A064 RID: 41060 RVA: 0x0023AB41 File Offset: 0x00238D41
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return base.EnableEmbeddedBaseStylesheet;
			}
			set
			{
				this.Calendar.EnableEmbeddedBaseStylesheet = value;
				this.DateInput.EnableEmbeddedBaseStylesheet = value;
				base.EnableEmbeddedBaseStylesheet = value;
			}
		}

		// Token: 0x170032B3 RID: 12979
		// (get) Token: 0x0600A065 RID: 41061 RVA: 0x0023AB62 File Offset: 0x00238D62
		// (set) Token: 0x0600A066 RID: 41062 RVA: 0x0023AB6A File Offset: 0x00238D6A
		[NotifyParentProperty(true)]
		public override bool RegisterWithScriptManager
		{
			get
			{
				return base.RegisterWithScriptManager;
			}
			set
			{
				this.Calendar.RegisterWithScriptManager = value;
				this.DateInput.RegisterWithScriptManager = value;
				base.RegisterWithScriptManager = value;
			}
		}

		// Token: 0x1400017B RID: 379
		// (add) Token: 0x0600A067 RID: 41063 RVA: 0x0023AB8B File Offset: 0x00238D8B
		// (remove) Token: 0x0600A068 RID: 41064 RVA: 0x0023AB9E File Offset: 0x00238D9E
		[Description("Occurs after all child controls of the DatePicker control have been created.")]
		public event EventHandler ChildrenCreated
		{
			add
			{
				base.Events.AddHandler(RadDatePicker.EventChildrenCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDatePicker.EventChildrenCreated, value);
			}
		}

		// Token: 0x0600A069 RID: 41065 RVA: 0x0023ABB4 File Offset: 0x00238DB4
		protected virtual void OnChildrenCreated()
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadDatePicker.EventChildrenCreated];
			if (eventHandler != null)
			{
				eventHandler(this, new EventArgs());
			}
		}

		// Token: 0x1400017C RID: 380
		// (add) Token: 0x0600A06A RID: 41066 RVA: 0x0023ABE6 File Offset: 0x00238DE6
		// (remove) Token: 0x0600A06B RID: 41067 RVA: 0x0023ABF9 File Offset: 0x00238DF9
		[Description("Occurs when the selected date of the DatePicker changes between posts to the server.")]
		public event SelectedDateChangedEventHandler SelectedDateChanged
		{
			add
			{
				base.Events.AddHandler(RadDatePicker.EventSelectedDateChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDatePicker.EventSelectedDateChanged, value);
			}
		}

		// Token: 0x0600A06C RID: 41068 RVA: 0x0023AC0C File Offset: 0x00238E0C
		protected virtual void OnSelectedDateChanged(SelectedDateChangedEventArgs eventArgs)
		{
			SelectedDateChangedEventHandler selectedDateChangedEventHandler = (SelectedDateChangedEventHandler)base.Events[RadDatePicker.EventSelectedDateChanged];
			if (selectedDateChangedEventHandler != null)
			{
				selectedDateChangedEventHandler(this, eventArgs);
			}
		}

		// Token: 0x0600A06D RID: 41069 RVA: 0x0023AC3A File Offset: 0x00238E3A
		internal new void EnsureChildControls()
		{
			base.EnsureChildControls();
		}

		// Token: 0x0600A06E RID: 41070 RVA: 0x0023AC42 File Offset: 0x00238E42
		protected override void OnInit(EventArgs e)
		{
			this.EnsureChildControls();
			base.OnInit(e);
		}

		// Token: 0x0600A06F RID: 41071 RVA: 0x0023AC54 File Offset: 0x00238E54
		protected override void CreateChildControls()
		{
			this.DateInput.ID = "dateInput";
			this.DateInput.TextChanged += this.DateInput_TextChanged;
			this.Controls.Add(this.DateInput);
			this.DatePopupButton.ID = "popupButton";
			this.Controls.Add(this.DatePopupButton);
			this.Calendar.ID = "calendar";
			this.Calendar.RenderInvisible = true;
			this.Controls.Add(this.Calendar);
			this.CreateTimeControls();
			this.OnChildrenCreated();
			base.CreateChildControls();
		}

		// Token: 0x0600A070 RID: 41072 RVA: 0x0023ACF9 File Offset: 0x00238EF9
		protected virtual void SetRenderMode(RenderMode mode)
		{
			this.Calendar.RenderMode = mode;
			this.DateInput.RenderMode = mode;
		}

		// Token: 0x0600A071 RID: 41073 RVA: 0x0023AD13 File Offset: 0x00238F13
		protected virtual void CreateTimeControls()
		{
		}

		// Token: 0x170032B4 RID: 12980
		// (get) Token: 0x0600A072 RID: 41074 RVA: 0x0023AD15 File Offset: 0x00238F15
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170032B5 RID: 12981
		// (get) Token: 0x0600A073 RID: 41075 RVA: 0x0023AD19 File Offset: 0x00238F19
		protected HttpBrowserCapabilities Browser
		{
			get
			{
				return this.Context.Request.Browser;
			}
		}

		// Token: 0x0600A074 RID: 41076 RVA: 0x0023AD2B File Offset: 0x00238F2B
		protected internal virtual bool isOnlyInputRendered()
		{
			return !this.DatePopupButton.Visible && this.Controls.Count == 3;
		}

		// Token: 0x0600A075 RID: 41077 RVA: 0x0023AD4A File Offset: 0x00238F4A
		protected virtual void SetDefaultSize()
		{
			this.defaultWidth = Unit.Pixel(160);
		}

		// Token: 0x170032B6 RID: 12982
		// (get) Token: 0x0600A076 RID: 41078 RVA: 0x0023AD5C File Offset: 0x00238F5C
		protected override string CssClassFormatString
		{
			get
			{
				if (!string.IsNullOrEmpty(base.RuntimeSkin))
				{
					return "RadPicker RadPicker_{0}";
				}
				return "RadPicker";
			}
		}

		// Token: 0x0600A077 RID: 41079 RVA: 0x0023AD78 File Offset: 0x00238F78
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.SetDefaultSize();
			}
			bool flag = this.ID != null;
			string id = this.ID;
			string clientID = this.ClientID;
			this.ID = null;
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID + "_wrapper");
			}
			if (this.isOnlyInputRendered() && !this.IsLightweightRendering)
			{
				if (base.DesignMode)
				{
					writer.AddStyleAttribute("zoom", "1");
					if (!this.Width.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
					}
					else if (!this.defaultWidth.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.defaultWidth.ToString());
					}
				}
				else if (this.Browser.IsBrowser("IE") && this.Context.Request.Browser.MajorVersion < 8)
				{
					writer.AddStyleAttribute("display", "inline");
					writer.AddStyleAttribute("zoom", "1");
				}
				else
				{
					writer.AddStyleAttribute("display", "inline-block");
				}
			}
			else if (!this.IsLightweightRendering)
			{
				this.RenderBrowserSpecificStyles(writer);
				if (this.Width.IsEmpty && base.Style["width"] == null)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.defaultWidth.ToString());
				}
				if (!this.Height.IsEmpty && this.Height.Type == UnitType.Percentage)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
				}
			}
			bool enabled = this.Enabled;
			this.Enabled = true;
			base.AddAttributesToRender(writer);
			this.Enabled = enabled;
			this.ID = id;
		}

		// Token: 0x0600A078 RID: 41080 RVA: 0x0023AF70 File Offset: 0x00239170
		protected void RenderBrowserSpecificStyles(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.AddStyleAttribute("display", "inline-block");
				writer.AddStyleAttribute("_display", "inline");
				writer.AddStyleAttribute("_zoom", "1");
				return;
			}
			if (this.Browser.IsBrowser("IE") && this.Context.Request.Browser.MajorVersion < 8)
			{
				writer.AddStyleAttribute("display", "inline");
				writer.AddStyleAttribute("zoom", "1");
				return;
			}
			writer.AddStyleAttribute("display", "inline-block");
		}

		// Token: 0x0600A079 RID: 41081 RVA: 0x0023B014 File Offset: 0x00239214
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			bool visible = this.Calendar.Visible;
			if (base.DesignMode)
			{
				this.Calendar.Visible = false;
			}
			if (this.isOnlyInputRendered() && !this.IsLightweightRendering)
			{
				if (base.DesignMode)
				{
					if (!this.Width.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
					}
					else if (!this.defaultWidth.IsEmpty)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.defaultWidth.ToString());
					}
				}
				base.RenderChildren(writer);
			}
			else if (!this.IsLightweightRendering)
			{
				if (base.DesignMode)
				{
					this.Calendar.Skin = this.Skin;
					writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this.Calendar));
				}
				writer.AddAttribute("cellspacing", "0");
				if (base.DesignMode || (!this.Browser.IsBrowser("Gecko") && !this.Browser.IsBrowser("Firefox")) || ((this.Browser.IsBrowser("Gecko") || this.Browser.IsBrowser("Firefox")) && !this.Width.IsEmpty && this.Width.Type == UnitType.Percentage))
				{
					writer.AddStyleAttribute("width", "100%");
				}
				else
				{
					writer.AddStyleAttribute("width", this.Width.IsEmpty ? this.defaultWidth.ToString() : this.Width.ToString());
				}
				if (this.DateInput.EnableSingleInputRendering)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcTable rcSingle");
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcTable");
				}
				if (!string.IsNullOrEmpty(this.WrapperTableSummary))
				{
					writer.AddAttribute("summary", this.WrapperTableSummary);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				if (!string.IsNullOrEmpty(this.WrapperTableCaption))
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
					writer.RenderBeginTag(HtmlTextWriterTag.Caption);
					writer.Write(this.WrapperTableCaption);
					writer.RenderEndTag();
				}
				if (!base.DesignMode)
				{
					AccessibilityHelper.RenderAccessibilityRow(writer, this.WrapperTableCaption);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				this.AddControlComponents(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			else
			{
				this.AddControlComponents(writer);
			}
			this.Calendar.Visible = visible;
		}

		// Token: 0x0600A07A RID: 41082 RVA: 0x0023B29C File Offset: 0x0023949C
		protected virtual void AddControlComponents(HtmlTextWriter writer)
		{
			if ((!string.IsNullOrEmpty(this.DateInput.Label) && !this.DateInput.EnableSingleInputRendering) || this.IsLightweightRendering)
			{
				if (!this.IsLightweightRendering)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
				}
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
				if (!this.IsLightweightRendering)
				{
					writer.RenderEndTag();
				}
			}
			if (this.IsLightweightRendering)
			{
				this.DateInput.RenderControl(writer);
				return;
			}
			if (this.DateInput.Visible)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcInputCell");
				if (this.ShouldRenderAdditionalControls && this.Browser.IsBrowser("IE"))
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
				this.DatePopupButton.RenderControl(writer);
			}
			this.Calendar.RenderControl(writer);
			writer.RenderEndTag();
			this.AddAdditionalControlComponents(writer);
		}

		// Token: 0x170032B7 RID: 12983
		// (get) Token: 0x0600A07B RID: 41083 RVA: 0x0023B438 File Offset: 0x00239638
		protected virtual bool ShouldRenderAdditionalControls
		{
			get
			{
				return this.Controls.Count > 3;
			}
		}

		// Token: 0x0600A07C RID: 41084 RVA: 0x0023B44C File Offset: 0x0023964C
		protected virtual void AddAdditionalControlComponents(HtmlTextWriter writer)
		{
			bool flag = false;
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (!(control is RadDateInput) && !(control is RadCalendar) && !(control is CalendarPopupButton))
				{
					if (!flag)
					{
						if (!base.DesignMode && (this.Browser.IsBrowser("Gecko") || this.Browser.IsBrowser("Firefox")))
						{
							writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "relative");
							writer.AddStyleAttribute("outline", "none");
						}
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						flag = true;
					}
					control.RenderControl(writer);
				}
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600A07D RID: 41085 RVA: 0x0023B524 File Offset: 0x00239724
		protected override void Render(HtmlTextWriter writer)
		{
			try
			{
				if (base.DesignMode)
				{
					this.EnsureChildControls();
					this.ConfigureChildren();
					this.SetChildrenSites(base.Site);
				}
				writer = new HtmlTextWriter(writer);
				base.Render(writer);
			}
			finally
			{
				this.SetChildrenSites(null);
			}
		}

		// Token: 0x0600A07E RID: 41086 RVA: 0x0023B57C File Offset: 0x0023977C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderValidationHiddenInput(writer);
			base.RenderContents(writer);
		}

		// Token: 0x0600A07F RID: 41087 RVA: 0x0023B58C File Offset: 0x0023978C
		private void RenderValidationHiddenInput(HtmlTextWriter writer)
		{
			writer.WriteBeginTag(HtmlTextWriterTag.Input.ToString().ToLower(CultureInfo.InvariantCulture));
			writer.WriteAttribute("style", "visibility:hidden;display:block;float:right;margin:0 0 -1px -1px;width:1px;height:1px;overflow:hidden;border:0;padding:0;");
			writer.WriteAttribute("id", this.ClientID);
			writer.WriteAttribute("name", this.UniqueID);
			writer.WriteAttribute("type", "text");
			writer.WriteAttribute("class", "rdfd_ radPreventDecorate");
			writer.WriteAttribute("value", HttpUtility.HtmlEncode(this.ValidationDate));
			writer.WriteAttribute("title", this.HiddenInputTitleAttibute);
			writer.Write(" />");
		}

		// Token: 0x0600A080 RID: 41088 RVA: 0x0023B63C File Offset: 0x0023983C
		internal void SetChildrenSites(ISite site)
		{
			Control[] array = new Control[]
			{
				this.DateInput,
				this.Calendar
			};
			foreach (Control control in array)
			{
				control.Site = site;
			}
		}

		// Token: 0x170032B8 RID: 12984
		// (get) Token: 0x0600A081 RID: 41089 RVA: 0x0023B684 File Offset: 0x00239884
		[Description("Gets the RadCalendar instance.")]
		[Category("Behavior")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual RadCalendar Calendar
		{
			get
			{
				if (this._calendar == null)
				{
					this._calendar = new DatePickingCalendar();
				}
				return this._calendar;
			}
		}

		// Token: 0x170032B9 RID: 12985
		// (get) Token: 0x0600A082 RID: 41090 RVA: 0x0023B69F File Offset: 0x0023989F
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Gets the RadDateInput instance.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual RadDateInput DateInput
		{
			get
			{
				if (this._input == null)
				{
					this._input = new DatePickingInput();
				}
				return this._input;
			}
		}

		// Token: 0x170032BA RID: 12986
		// (get) Token: 0x0600A083 RID: 41091 RVA: 0x0023B6BA File Offset: 0x002398BA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the DatePopupButton instance.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[Browsable(true)]
		public virtual CalendarPopupButton DatePopupButton
		{
			get
			{
				if (this._datePopupButton == null)
				{
					this._datePopupButton = new CalendarPopupButton(this);
				}
				return this._datePopupButton;
			}
		}

		// Token: 0x170032BB RID: 12987
		// (get) Token: 0x0600A084 RID: 41092 RVA: 0x0023B6D6 File Offset: 0x002398D6
		// (set) Token: 0x0600A085 RID: 41093 RVA: 0x0023B6E3 File Offset: 0x002398E3
		[Description("Gets or sets a value indicating whether a postback to the server automatically occurs when the user interacts with the control.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool AutoPostBack
		{
			get
			{
				return this.DateInput.AutoPostBack;
			}
			set
			{
				this.DateInput.AutoPostBack = value;
			}
		}

		// Token: 0x170032BC RID: 12988
		// (get) Token: 0x0600A086 RID: 41094 RVA: 0x0023B6F1 File Offset: 0x002398F1
		// (set) Token: 0x0600A087 RID: 41095 RVA: 0x0023B6F9 File Offset: 0x002398F9
		[NotifyParentProperty(true)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x170032BD RID: 12989
		// (get) Token: 0x0600A088 RID: 41096 RVA: 0x0023B702 File Offset: 0x00239902
		// (set) Token: 0x0600A089 RID: 41097 RVA: 0x0023B70F File Offset: 0x0023990F
		[NotifyParentProperty(true)]
		public override short TabIndex
		{
			get
			{
				return this.DateInput.TabIndex;
			}
			set
			{
				this.DateInput.TabIndex = value;
				this.DatePopupButton.TabIndex = value;
			}
		}

		// Token: 0x170032BE RID: 12990
		// (get) Token: 0x0600A08A RID: 41098 RVA: 0x0023B729 File Offset: 0x00239929
		// (set) Token: 0x0600A08B RID: 41099 RVA: 0x0023B758 File Offset: 0x00239958
		[NotifyParentProperty(true)]
		[Category("Accessibility")]
		[DefaultValue("Visually hidden input created for functionality purposes.")]
		[Description("Gets or sets the title attribute for the hidden field.")]
		[Localizable(true)]
		public virtual string HiddenInputTitleAttibute
		{
			get
			{
				if (this.ViewState["HiddenInputTitleAttibute"] == null)
				{
					return "Visually hidden input created for functionality purposes.";
				}
				return (string)this.ViewState["HiddenInputTitleAttibute"];
			}
			set
			{
				this.ViewState["HiddenInputTitleAttibute"] = value;
			}
		}

		// Token: 0x170032BF RID: 12991
		// (get) Token: 0x0600A08C RID: 41100 RVA: 0x0023B76B File Offset: 0x0023996B
		// (set) Token: 0x0600A08D RID: 41101 RVA: 0x0023B79A File Offset: 0x0023999A
		[Localizable(true)]
		[Category("Accessibility")]
		[DefaultValue("Table holding date picker control for selection of dates.")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the summary attribute for the table which wraps the RadDatePicker controls.")]
		public virtual string WrapperTableSummary
		{
			get
			{
				if (this.ViewState["WrapperTableSummary"] == null)
				{
					return "Table holding date picker control for selection of dates.";
				}
				return (string)this.ViewState["WrapperTableSummary"];
			}
			set
			{
				this.ViewState["WrapperTableSummary"] = value;
			}
		}

		// Token: 0x170032C0 RID: 12992
		// (get) Token: 0x0600A08E RID: 41102 RVA: 0x0023B7AD File Offset: 0x002399AD
		// (set) Token: 0x0600A08F RID: 41103 RVA: 0x0023B7DC File Offset: 0x002399DC
		[NotifyParentProperty(true)]
		[Category("Accessibility")]
		[Description("Gets or sets the caption for the table which wraps the RadDatePicker controls.")]
		[DefaultValue("RadDatePicker")]
		[Localizable(true)]
		public virtual string WrapperTableCaption
		{
			get
			{
				if (this.ViewState["WrapperTableCaption"] == null)
				{
					return "RadDatePicker";
				}
				return (string)this.ViewState["WrapperTableCaption"];
			}
			set
			{
				this.ViewState["WrapperTableCaption"] = value;
			}
		}

		// Token: 0x170032C1 RID: 12993
		// (get) Token: 0x0600A090 RID: 41104 RVA: 0x0023B7EF File Offset: 0x002399EF
		// (set) Token: 0x0600A091 RID: 41105 RVA: 0x0023B81B File Offset: 0x00239A1B
		[Description("Gets or sets the direction in which the popup Calendar (or TimeView) is displayed, with relation to the DatePicker control.")]
		[DefaultValue(DatePickerPopupDirection.BottomRight)]
		[ClientPropertyName("_popupDirection")]
		[Category("Behavior")]
		[ClientControlProperty]
		public DatePickerPopupDirection PopupDirection
		{
			get
			{
				if (this.ViewState["PopupDirection"] == null)
				{
					return DatePickerPopupDirection.BottomRight;
				}
				return (DatePickerPopupDirection)this.ViewState["PopupDirection"];
			}
			set
			{
				this.ViewState["PopupDirection"] = value;
			}
		}

		// Token: 0x170032C2 RID: 12994
		// (get) Token: 0x0600A092 RID: 41106 RVA: 0x0023B833 File Offset: 0x00239A33
		// (set) Token: 0x0600A093 RID: 41107 RVA: 0x0023B85E File Offset: 0x00239A5E
		[DefaultValue(true)]
		[ClientControlProperty]
		[Description("Gets or sets whether the screen boundaries should be taken into consideration when the Calendar or TimeView are displayed.")]
		[Category("Behavior")]
		[ClientPropertyName("_enableScreenBoundaryDetection")]
		public bool EnableScreenBoundaryDetection
		{
			get
			{
				return this.ViewState["EnableScreenBoundaryDetection"] == null || (bool)this.ViewState["EnableScreenBoundaryDetection"];
			}
			set
			{
				this.ViewState["EnableScreenBoundaryDetection"] = value;
			}
		}

		// Token: 0x170032C3 RID: 12995
		// (get) Token: 0x0600A094 RID: 41108 RVA: 0x0023B876 File Offset: 0x00239A76
		// (set) Token: 0x0600A095 RID: 41109 RVA: 0x0023B8A5 File Offset: 0x00239AA5
		[ClientControlProperty]
		[DefaultValue(5000)]
		[Category("Behavior")]
		[ClientPropertyName("_zIndex")]
		[Description("Gets or sets the z-index style of the control's popups")]
		public int ZIndex
		{
			get
			{
				if (this.ViewState["ZIndex"] == null)
				{
					return 5000;
				}
				return (int)this.ViewState["ZIndex"];
			}
			set
			{
				this.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x170032C4 RID: 12996
		// (get) Token: 0x0600A096 RID: 41110 RVA: 0x0023B8BD File Offset: 0x00239ABD
		// (set) Token: 0x0600A097 RID: 41111 RVA: 0x0023B8E8 File Offset: 0x00239AE8
		[Description("Gets or sets whether popup shadows will appear.")]
		[DefaultValue(true)]
		[Category("Appearance")]
		[ClientControlProperty]
		[ClientPropertyName("_enableShadows")]
		public bool EnableShadows
		{
			get
			{
				return this.ViewState["EnableShadows"] == null || (bool)this.ViewState["EnableShadows"];
			}
			set
			{
				this.ViewState["EnableShadows"] = value;
			}
		}

		// Token: 0x170032C5 RID: 12997
		// (get) Token: 0x0600A098 RID: 41112 RVA: 0x0023B900 File Offset: 0x00239B00
		// (set) Token: 0x0600A099 RID: 41113 RVA: 0x0023B918 File Offset: 0x00239B18
		[Description("Gets or sets the selected date.")]
		[Category("Date Selection")]
		[DefaultValue(typeof(DateTime?), null)]
		[Editor("System.ComponentModel.Design.DateTimeEditor", "System.Drawing.Design.UITypeEditor")]
		public virtual DateTime? SelectedDate
		{
			get
			{
				return (DateTime?)this.ViewState["SelectedDate"];
			}
			set
			{
				DateTime? dateTime = null;
				if (value != null)
				{
					dateTime = new DateTime?(this.TruncateTimeComponent(value.Value));
					if (dateTime > this.MaxDate || dateTime < this.MinDate)
					{
						if (!this.SkipMinMaxDateValidationOnServer)
						{
							throw new ArgumentOutOfRangeException("SelectedDate", string.Format("Value of '{0}' is not valid for 'SelectedDate'. 'SelectedDate' should be between 'MinDate' and 'MaxDate'.", dateTime));
						}
						dateTime = null;
					}
				}
				this.ViewState["SelectedDate"] = dateTime;
				if (!this.SkipMinMaxDateValidationOnServer || value == null)
				{
					this.DateInput.SelectedDate = this.SelectedDate;
					return;
				}
				this.DateInput.SelectedDate = value;
			}
		}

		// Token: 0x170032C6 RID: 12998
		// (get) Token: 0x0600A09A RID: 41114 RVA: 0x0023BA07 File Offset: 0x00239C07
		// (set) Token: 0x0600A09B RID: 41115 RVA: 0x0023BA46 File Offset: 0x00239C46
		[Category("Behavior")]
		[Description("Switched on or off the server-side min/max date validation.")]
		[DefaultValue(false)]
		public bool SkipMinMaxDateValidationOnServer
		{
			get
			{
				if (this.ViewState["_skipMMValidation"] == null)
				{
					this.ViewState["_skipMMValidation"] = false;
				}
				return (bool)this.ViewState["_skipMMValidation"];
			}
			set
			{
				this.ViewState["_skipMMValidation"] = value;
				this.DateInput.SkipMinMaxDateValidationOnServer = value;
			}
		}

		// Token: 0x170032C7 RID: 12999
		// (get) Token: 0x0600A09C RID: 41116 RVA: 0x0023BA6C File Offset: 0x00239C6C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string ValidationDate
		{
			get
			{
				if (this.SelectedDate != null)
				{
					return this.SelectedDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
				}
				return this.DateInput.Text;
			}
		}

		// Token: 0x170032C8 RID: 13000
		// (get) Token: 0x0600A09D RID: 41117 RVA: 0x0023BAB5 File Offset: 0x00239CB5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string InvalidTextBoxValue
		{
			get
			{
				return this.DateInput.InvalidTextBoxValue;
			}
		}

		// Token: 0x170032C9 RID: 13001
		// (get) Token: 0x0600A09E RID: 41118 RVA: 0x0023BAC2 File Offset: 0x00239CC2
		// (set) Token: 0x0600A09F RID: 41119 RVA: 0x0023BAD0 File Offset: 0x00239CD0
		[TypeConverter(typeof(DateTimeConverter))]
		[Bindable(true, BindingDirection.TwoWay)]
		[Description("The currently selected date, boxed in object")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual object DbSelectedDate
		{
			get
			{
				return this.SelectedDate;
			}
			set
			{
				string text = value as string;
				if (text != null)
				{
					if (string.IsNullOrEmpty(text))
					{
						this.SelectedDate = null;
						return;
					}
					DateTime dateTime;
					bool flag = DateTime.TryParseExact(text, this.DateInput.DateFormat, this.DateInput.Culture, DateTimeStyles.None, out dateTime);
					if (!flag)
					{
						flag = DateTime.TryParseExact(text, this.DateInput.DisplayDateFormat, this.DateInput.Culture, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParseExact(text, this.DateInput.DateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParseExact(text, this.DateInput.DisplayDateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParse(text, this.DateInput.Culture, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParse(text, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						throw new FormatException("The string was not recognized as a valid DateTime.");
					}
					this.SelectedDate = new DateTime?(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, DateTimeKind.Unspecified));
					return;
				}
				else
				{
					if (value == DBNull.Value)
					{
						this.SelectedDate = null;
						return;
					}
					this.SelectedDate = (DateTime?)value;
					return;
				}
			}
		}

		// Token: 0x170032CA RID: 13002
		// (get) Token: 0x0600A0A0 RID: 41120 RVA: 0x0023BC38 File Offset: 0x00239E38
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.SelectedDate == null;
			}
		}

		// Token: 0x170032CB RID: 13003
		// (get) Token: 0x0600A0A1 RID: 41121 RVA: 0x0023BC56 File Offset: 0x00239E56
		// (set) Token: 0x0600A0A2 RID: 41122 RVA: 0x0023BC66 File Offset: 0x00239E66
		[Description("Enable or disable typing in the date input box.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool EnableTyping
		{
			get
			{
				return !this.DateInput.ReadOnly;
			}
			set
			{
				this.DateInput.ReadOnly = !value;
			}
		}

		// Token: 0x170032CC RID: 13004
		// (get) Token: 0x0600A0A3 RID: 41123 RVA: 0x0023BC77 File Offset: 0x00239E77
		// (set) Token: 0x0600A0A4 RID: 41124 RVA: 0x0023BCA2 File Offset: 0x00239EA2
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_showPopupOnFocus")]
		[Description("Gets or sets whether the popup control (Calendar or TimeView) is displayed when the DateInput textbox is focused.")]
		public bool ShowPopupOnFocus
		{
			get
			{
				return this.ViewState["ShowPopupOnFocus"] != null && (bool)this.ViewState["ShowPopupOnFocus"];
			}
			set
			{
				this.ViewState["ShowPopupOnFocus"] = value;
			}
		}

		// Token: 0x170032CD RID: 13005
		// (get) Token: 0x0600A0A5 RID: 41125 RVA: 0x0023BCBA File Offset: 0x00239EBA
		// (set) Token: 0x0600A0A6 RID: 41126 RVA: 0x0023BCF0 File Offset: 0x00239EF0
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		[ClientControlProperty]
		[Description("Gets or sets the earliest valid date for selection.")]
		[Category("Date Selection")]
		public DateTime MinDate
		{
			get
			{
				if (this.ViewState["MinDate"] == null)
				{
					return new DateTime(1980, 1, 1);
				}
				return (DateTime)this.ViewState["MinDate"];
			}
			set
			{
				DateTime dateTime = this.TruncateTimeComponent(value);
				this.ViewState["MinDate"] = dateTime;
				this.DateInput.MinDate = dateTime;
				this.Calendar.RangeMinDate = dateTime;
				this.SelectedDate = this.RangeSelectedDateProperty(this.SelectedDate);
			}
		}

		// Token: 0x0600A0A7 RID: 41127 RVA: 0x0023BD48 File Offset: 0x00239F48
		protected virtual DateTime? RangeSelectedDateProperty(DateTime? value)
		{
			if (value != null)
			{
				value = new DateTime?((this.MaxDate < value.Value) ? this.MaxDate : value.Value);
				value = new DateTime?((this.MinDate > value.Value) ? this.MinDate : value.Value);
				return value;
			}
			return null;
		}

		// Token: 0x0600A0A8 RID: 41128 RVA: 0x0023BDBD File Offset: 0x00239FBD
		protected virtual DateTime TruncateTimeComponent(DateTime value)
		{
			return value.Subtract(value.TimeOfDay);
		}

		// Token: 0x170032CE RID: 13006
		// (get) Token: 0x0600A0A9 RID: 41129 RVA: 0x0023BDCD File Offset: 0x00239FCD
		// (set) Token: 0x0600A0AA RID: 41130 RVA: 0x0023BE08 File Offset: 0x0023A008
		[Category("Date Selection")]
		[DefaultValue(typeof(DateTime), "12/31/2099")]
		[Description("Gets or sets the latest valid date for selection.")]
		[ClientControlProperty]
		public DateTime MaxDate
		{
			get
			{
				if (this.ViewState["MaxDate"] == null)
				{
					return new DateTime(2099, 12, 31);
				}
				return (DateTime)this.ViewState["MaxDate"];
			}
			set
			{
				DateTime dateTime = this.TruncateTimeComponent(value);
				this.ViewState["MaxDate"] = dateTime;
				this.DateInput.MaxDate = dateTime;
				this.Calendar.RangeMaxDate = dateTime;
				this.SelectedDate = this.RangeSelectedDateProperty(this.SelectedDate);
			}
		}

		// Token: 0x170032CF RID: 13007
		// (get) Token: 0x0600A0AB RID: 41131 RVA: 0x0023BE5D File Offset: 0x0023A05D
		// (set) Token: 0x0600A0AC RID: 41132 RVA: 0x0023BE6A File Offset: 0x0023A06A
		[Description("Culture used by RadDateInput to format the date.")]
		[Category("Behavior")]
		public virtual CultureInfo Culture
		{
			get
			{
				return this.Calendar.Culture;
			}
			set
			{
				this.DateInput.Culture = value;
				this.Calendar.CultureInfo = value;
			}
		}

		// Token: 0x170032D0 RID: 13008
		// (get) Token: 0x0600A0AD RID: 41133 RVA: 0x0023BE84 File Offset: 0x0023A084
		// (set) Token: 0x0600A0AE RID: 41134 RVA: 0x0023BEB1 File Offset: 0x0023A0B1
		[Category("Behavior")]
		[TypeConverter("Telerik.Web.Design.CalendarIdConverter")]
		[DefaultValue("")]
		[Description("The ID of the RadCalendar that will be shared among several RadDatePickers.")]
		public virtual string SharedCalendarID
		{
			get
			{
				object obj = this.ViewState["SharedCalendarID"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["SharedCalendarID"] = value;
			}
		}

		// Token: 0x170032D1 RID: 13009
		// (get) Token: 0x0600A0AF RID: 41135 RVA: 0x0023BEC4 File Offset: 0x0023A0C4
		// (set) Token: 0x0600A0B0 RID: 41136 RVA: 0x0023BF2C File Offset: 0x0023A12C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadCalendar SharedCalendar
		{
			get
			{
				if (this._sharedCalendar == null && !string.IsNullOrEmpty(this.SharedCalendarID))
				{
					this._sharedCalendar = (this.NamingContainer.FindControl(this.SharedCalendarID) as RadCalendar);
					if (this._sharedCalendar == null)
					{
						this._sharedCalendar = (this.Page.FindControl(this.SharedCalendarID) as RadCalendar);
					}
				}
				return this._sharedCalendar;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("You need to pass a valid reference to a RadCalendar instance in order to use the SharedCalendar functionality.");
				}
				this._sharedCalendar = value;
			}
		}

		// Token: 0x170032D2 RID: 13010
		// (get) Token: 0x0600A0B1 RID: 41137 RVA: 0x0023BF44 File Offset: 0x0023A144
		// (set) Token: 0x0600A0B2 RID: 41138 RVA: 0x0023BFBC File Offset: 0x0023A1BC
		[Category("Date Selection")]
		[Description("The calendar uses this date to focus itself whenever the date input component of the datepicker is empty.")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		public DateTime FocusedDate
		{
			get
			{
				DateTime dateTime = base.DesignMode ? new DateTime(1980, 1, 1) : DateTime.Today;
				object obj = this.ViewState["FocusedDate"];
				if (obj is DateTime)
				{
					dateTime = (DateTime)obj;
				}
				if (dateTime > this.MaxDate)
				{
					dateTime = this.MaxDate;
				}
				else if (dateTime < this.MinDate)
				{
					dateTime = this.MinDate;
				}
				return dateTime;
			}
			set
			{
				DateTime dateTime = this.TruncateTimeComponent(value);
				this.ViewState["FocusedDate"] = dateTime;
			}
		}

		// Token: 0x170032D3 RID: 13011
		// (get) Token: 0x0600A0B3 RID: 41139 RVA: 0x0023BFE7 File Offset: 0x0023A1E7
		// (set) Token: 0x0600A0B4 RID: 41140 RVA: 0x0023BFEF File Offset: 0x0023A1EF
		[Description("Sets the render mode of the RadDatePicker and its child controls")]
		public override RenderMode RenderMode
		{
			get
			{
				return base.RenderMode;
			}
			set
			{
				base.RenderMode = value;
				this.SetRenderMode(value);
			}
		}

		// Token: 0x170032D4 RID: 13012
		// (get) Token: 0x0600A0B5 RID: 41141 RVA: 0x0023BFFF File Offset: 0x0023A1FF
		// (set) Token: 0x0600A0B6 RID: 41142 RVA: 0x0023C007 File Offset: 0x0023A207
		[Description("The width of the RadDatePicker control in pixels.")]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x170032D5 RID: 13013
		// (get) Token: 0x0600A0B7 RID: 41143 RVA: 0x0023C010 File Offset: 0x0023A210
		[NotifyParentProperty(true)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CalendarAnimationSettings ShowAnimation
		{
			get
			{
				if (this._showAnimation == null)
				{
					this._showAnimation = new CalendarAnimationSettings("show", this.ViewState);
				}
				return this._showAnimation;
			}
		}

		// Token: 0x170032D6 RID: 13014
		// (get) Token: 0x0600A0B8 RID: 41144 RVA: 0x0023C036 File Offset: 0x0023A236
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		public CalendarAnimationSettings HideAnimation
		{
			get
			{
				if (this._hideAnimation == null)
				{
					this._hideAnimation = new CalendarAnimationSettings("hide", this.ViewState);
				}
				return this._hideAnimation;
			}
		}

		// Token: 0x170032D7 RID: 13015
		// (get) Token: 0x0600A0B9 RID: 41145 RVA: 0x0023C05C File Offset: 0x0023A25C
		// (set) Token: 0x0600A0BA RID: 41146 RVA: 0x0023C07D File Offset: 0x0023A27D
		[Description("When set to true enables support for WAI-ARIA")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x170032D8 RID: 13016
		// (get) Token: 0x0600A0BB RID: 41147 RVA: 0x0023C095 File Offset: 0x0023A295
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("A set of properties that get or set the names of the JavaScript functions that are invoked upon specific client-side events.")]
		[Category("ClientEvents")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DatePickerClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new DatePickerClientEvents(this.ViewState, this);
				}
				return this._clientEvents;
			}
		}

		// Token: 0x170032D9 RID: 13017
		// (get) Token: 0x0600A0BC RID: 41148 RVA: 0x0023C0B8 File Offset: 0x0023A2B8
		// (set) Token: 0x0600A0BD RID: 41149 RVA: 0x0023C0F2 File Offset: 0x0023A2F2
		[Browsable(true)]
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether the picker will create an overlay element to ensure popups are over a flash element or Java applet.")]
		[Bindable(true)]
		[Category("Behavior")]
		public bool Overlay
		{
			get
			{
				bool? flag = this.ViewState["Overlay"] as bool?;
				return flag != null && flag.Value;
			}
			set
			{
				this.ViewState["Overlay"] = value;
			}
		}

		// Token: 0x0600A0BE RID: 41150 RVA: 0x0023C10C File Offset: 0x0023A30C
		public virtual void ConfigureCalendar()
		{
			this.Calendar.EnableMultiSelect = false;
			this.Calendar.EnableAriaSupport = this.EnableAriaSupport;
			if (!base.DesignMode)
			{
				if (!string.IsNullOrEmpty(this.SharedCalendarID) && this.SharedCalendar == null)
				{
					throw new ArgumentException("Could not find a RadCalendar control with ID of '" + this.SharedCalendarID + "'.  \r\nUse the SharedCalendar property to pass a direct reference in your code-behind if the control is not in the current naming container.");
				}
				if (this.SharedCalendar != null)
				{
					this.SharedCalendar.RenderInvisible = true;
					this.Calendar.Visible = false;
					if (this.EnableEmbeddedSkins)
					{
						this.SharedCalendar.EnableEmbeddedSkins = this.Calendar.EnableEmbeddedSkins;
					}
					if (this.EnableEmbeddedScripts)
					{
						this.SharedCalendar.EnableEmbeddedScripts = this.Calendar.EnableEmbeddedScripts;
					}
					if (this.EnableEmbeddedBaseStylesheet)
					{
						this.SharedCalendar.EnableEmbeddedBaseStylesheet = this.Calendar.EnableEmbeddedBaseStylesheet;
					}
					if (!string.IsNullOrEmpty(this.ImagesPath))
					{
						this.SharedCalendar.ImagesPath = this.Calendar.ImagesPath;
					}
					this.SharedCalendar.EnableAriaSupport = this.EnableAriaSupport;
				}
			}
		}

		// Token: 0x0600A0BF RID: 41151 RVA: 0x0023C224 File Offset: 0x0023A424
		public virtual void ConfigureDateInput()
		{
			this.ConfigureDateInput(null);
		}

		// Token: 0x0600A0C0 RID: 41152 RVA: 0x0023C240 File Offset: 0x0023A440
		public virtual void ConfigureDateInput(DateTime? date)
		{
			if (date != null)
			{
				this.DateInput.SelectedDate = date;
			}
			else
			{
				this.DateInput.SelectedDate = this.SelectedDate;
			}
			this.DateInput.EnableAriaSupport = this.EnableAriaSupport;
			if (this.isOnlyInputRendered() && this.Width.Type != UnitType.Percentage)
			{
				if (base.DesignMode)
				{
					this.SetDefaultSize();
				}
				Unit unit = Unit.Empty;
				if (base.Style["height"] != null)
				{
					unit = Unit.Parse(base.Style["height"]);
				}
				this.DateInput.Height = ((!this.Height.IsEmpty) ? this.Height : unit);
				Unit unit2 = Unit.Empty;
				if (base.Style["width"] != null)
				{
					unit2 = Unit.Parse(base.Style["width"]);
				}
				this.DateInput.Width = ((!this.Width.IsEmpty) ? this.Width : ((!unit2.IsEmpty) ? unit2 : this.defaultWidth));
				return;
			}
			if (!this.IsLightweightRendering)
			{
				this.DateInput.Width = Unit.Percentage(100.0);
			}
			this.DateInput.Height = this.Height;
		}

		// Token: 0x0600A0C1 RID: 41153 RVA: 0x0023C39C File Offset: 0x0023A59C
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			this.SetDefaultSize();
			this.ConfigureChildren();
			this.ConfigureDatePicker();
			this.Calendar.Skin = base.RuntimeSkin;
			this.DateInput.Skin = base.RuntimeSkin;
		}

		// Token: 0x0600A0C2 RID: 41154 RVA: 0x0023C3D8 File Offset: 0x0023A5D8
		internal virtual void ConfigureChildren()
		{
			this.ConfigureCalendar();
			this.ConfigureDateInput();
		}

		// Token: 0x0600A0C3 RID: 41155 RVA: 0x0023C3E6 File Offset: 0x0023A5E6
		internal void ConfigureDatePicker()
		{
			if (this.MinDate > this.FocusedDate)
			{
				this.FocusedDate = this.MinDate;
			}
		}

		// Token: 0x170032DA RID: 13018
		// (get) Token: 0x0600A0C4 RID: 41156 RVA: 0x0023C407 File Offset: 0x0023A607
		internal bool IsLightweightRendering
		{
			get
			{
				if (this._isLightweightRendering == null)
				{
					this._isLightweightRendering = new bool?(this.ResolvedRenderMode == RenderMode.Lightweight);
				}
				return this._isLightweightRendering.Value;
			}
		}

		// Token: 0x0600A0C5 RID: 41157 RVA: 0x0023C438 File Offset: 0x0023A638
		private void DateInput_TextChanged(object sender, EventArgs e)
		{
			SelectedDateChangedEventArgs eventArgs = new SelectedDateChangedEventArgs(this._oldSelectedValue, this.SelectedDate);
			this.OnSelectedDateChanged(eventArgs);
		}

		// Token: 0x0600A0C6 RID: 41158 RVA: 0x0023C45E File Offset: 0x0023A65E
		public override void Focus()
		{
			this.EnsureChildControls();
			this.DateInput.Focus();
		}

		// Token: 0x0600A0C7 RID: 41159 RVA: 0x0023C471 File Offset: 0x0023A671
		internal void SelectedDateLoaded(object newDate)
		{
			this._oldSelectedValue = this.SelectedDate;
			this.DbSelectedDate = newDate;
		}

		// Token: 0x0600A0C8 RID: 41160 RVA: 0x0023C486 File Offset: 0x0023A686
		public virtual void Clear()
		{
			this.DateInput.Clear();
			this.ViewState["SelectedDate"] = null;
		}

		// Token: 0x0600A0C9 RID: 41161 RVA: 0x0023C4A4 File Offset: 0x0023A6A4
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.ClientEvents.DescribeEvents(descriptor);
			this.DescribeProperties(descriptor);
		}

		// Token: 0x0600A0CA RID: 41162 RVA: 0x0023C4C0 File Offset: 0x0023A6C0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected virtual void DescribeProperties(IScriptDescriptor descriptor)
		{
			if (this.DateInput.Visible)
			{
				descriptor.AddComponentProperty("dateInput", this.DateInput.ClientID);
			}
			if (!(this is RadTimePicker))
			{
				if (this.SharedCalendar != null)
				{
					descriptor.AddComponentProperty("calendar", this.SharedCalendar.ClientID);
				}
				if (this.Calendar != null && this.Calendar.Visible)
				{
					descriptor.AddComponentProperty("calendar", this.Calendar.ClientID);
				}
			}
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", base.IsEnabled);
			}
			if (this.Overlay)
			{
				descriptor.AddProperty("_overlay", this.Overlay);
			}
			if (this.ShowPopupOnInit)
			{
				descriptor.AddProperty("_showPopupOnInit", this.ShowPopupOnInit);
			}
			base.DescribeRenderMode(descriptor);
			descriptor.AddProperty("_popupControlID", this.DatePopupButton.ClientID);
			descriptor.AddProperty("_enableKeyboardNavigation", this.EnableKeyboardNavigation);
			descriptor.AddScriptProperty("_PopupButtonSettings", string.Format("{{ ResolvedImageUrl : \"{0}\", ResolvedHoverImageUrl : \"{1}\"}}", this.DatePopupButton.ResolvedImageUrl, this.DatePopupButton.ResolvedHoverImageUrl));
			if (this != null && !(this is RadDateTimePicker) && !(this is RadTimePicker))
			{
				string script = string.Format("{{ShowAnimationDuration:{0},ShowAnimationType:{1},HideAnimationDuration:{2},HideAnimationType:{3}}}", new object[]
				{
					this.ShowAnimation.Duration,
					(int)this.ShowAnimation.Type,
					(this.AutoPostBack || this.SharedCalendar != null) ? 0 : this.HideAnimation.Duration,
					(int)this.HideAnimation.Type
				});
				descriptor.AddScriptProperty("_animationSettings", script);
			}
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("enableAriaSupport", this.EnableAriaSupport);
			}
		}

		// Token: 0x0600A0CB RID: 41163 RVA: 0x0023C6AE File Offset: 0x0023A8AE
		protected override void RegisterCssReferences()
		{
		}

		// Token: 0x0600A0CC RID: 41164 RVA: 0x0023C6B0 File Offset: 0x0023A8B0
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			((IPostBackEventHandler)this.DateInput).RaisePostBackEvent(eventArgument);
		}

		// Token: 0x0600A0CD RID: 41165 RVA: 0x0023C6BE File Offset: 0x0023A8BE
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			base.LoadPostData(postDataKey, postCollection);
			return false;
		}

		// Token: 0x0600A0CE RID: 41166 RVA: 0x0023C6CA File Offset: 0x0023A8CA
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
		}

		// Token: 0x0600A0CF RID: 41167 RVA: 0x0023C6CC File Offset: 0x0023A8CC
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			DateTime dateTime;
			if (DateTime.TryParseExact((string)clientState["minDateStr"], "yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
			{
				this.MinDate = dateTime;
			}
			if (DateTime.TryParseExact((string)clientState["maxDateStr"], "yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
			{
				this.MaxDate = dateTime;
			}
		}

		// Token: 0x170032DB RID: 13019
		// (get) Token: 0x0600A0D0 RID: 41168 RVA: 0x0023C736 File Offset: 0x0023A936
		public string ControlId
		{
			get
			{
				return this.DateInput.ClientID;
			}
		}

		// Token: 0x0600A0D1 RID: 41169 RVA: 0x0023C744 File Offset: 0x0023A944
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "_enableScreenBoundaryDetection", this.EnableScreenBoundaryDetection, true);
			base.DescribeProperty<bool>(descriptor, "_enableShadows", this.EnableShadows, true);
			base.DescribeProperty<string>(descriptor, "focusedDate", this.FocusedDate.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture), DateTime.Parse("1/1/1980", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture));
			base.DescribeProperty<string>(descriptor, "maxDate", this.MaxDate.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture), DateTime.Parse("12/31/2099", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture));
			base.DescribeProperty<string>(descriptor, "minDate", this.MinDate.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture), DateTime.Parse("1/1/1980", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture));
			base.DescribeProperty<DatePickerPopupDirection>(descriptor, "_popupDirection", this.PopupDirection, DatePickerPopupDirection.BottomRight);
			base.DescribeProperty<bool>(descriptor, "_showPopupOnFocus", this.ShowPopupOnFocus, false);
			base.DescribeProperty<int>(descriptor, "_zIndex", this.ZIndex, 5000);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600A0D2 RID: 41170 RVA: 0x0023C88D File Offset: 0x0023AA8D
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002CE5 RID: 11493
		private const string hiddenFormat = "yyyy-MM-dd-HH-mm-ss";

		// Token: 0x04002CE6 RID: 11494
		private RadDateInput _input;

		// Token: 0x04002CE7 RID: 11495
		private RadCalendar _calendar;

		// Token: 0x04002CE8 RID: 11496
		private CalendarPopupButton _datePopupButton;

		// Token: 0x04002CE9 RID: 11497
		private DatePickerClientEvents _clientEvents;

		// Token: 0x04002CEA RID: 11498
		private DateTime? _oldSelectedValue;

		// Token: 0x04002CEB RID: 11499
		private CalendarAnimationSettings _showAnimation;

		// Token: 0x04002CEC RID: 11500
		private CalendarAnimationSettings _hideAnimation;

		// Token: 0x04002CED RID: 11501
		internal bool ShowPopupOnInit;

		// Token: 0x04002CEE RID: 11502
		private DatePickerStrings _localization;

		// Token: 0x04002CEF RID: 11503
		private static readonly object EventChildrenCreated = new object();

		// Token: 0x04002CF0 RID: 11504
		private static readonly object EventSelectedDateChanged = new object();

		// Token: 0x04002CF1 RID: 11505
		protected Unit defaultWidth = Unit.Empty;

		// Token: 0x04002CF2 RID: 11506
		private RadCalendar _sharedCalendar;

		// Token: 0x04002CF3 RID: 11507
		private bool? _isLightweightRendering = null;
	}
}
