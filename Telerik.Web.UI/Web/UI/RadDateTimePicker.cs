using System;
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
	// Token: 0x0200100A RID: 4106
	[DefaultEvent("SelectedDateChanged")]
	[ValidationProperty("ValidationDate")]
	[ClientScriptResource("Telerik.Web.UI.RadDateTimePicker", "Telerik.Web.UI.Calendar.RadDateTimePickerScript.js")]
	[ClientScriptResource("Telerik.Web.UI.RadDateTimePicker", "Telerik.Web.UI.Calendar.RadTimeViewScripts.js")]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Designer("Telerik.Web.Design.RadDateTimePickerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[PersistChildren(false)]
	[EmbeddedSkin("DateTimePicker", "Default", typeof(RadDateTimePicker))]
	[ToolboxBitmap(typeof(RadDateTimePicker), "Telerik.Web.UI.DateTimePicker.png")]
	[ToolboxData("<{0}:RadDateTimePicker Runat=\"server\"></{0}:RadDateTimePicker>")]
	[TelerikToolboxCategory("Date/Color Picker")]
	[EmbeddedSkin("DateTimePicker", typeof(RadDateTimePicker))]
	[DefaultProperty("SelectedDate")]
	[Description("Telerik RadCalendar")]
	[ParseChildren(true)]
	[ControlValueProperty("SelectedDate")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadDateTimePicker : RadDatePicker
	{
		// Token: 0x170032DC RID: 13020
		// (get) Token: 0x0600A0D5 RID: 41173 RVA: 0x0023C8B4 File Offset: 0x0023AAB4
		internal new DateTimePickerStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new DateTimePickerStrings(new LocalizationProvider("RadDateTimePicker.Main", this, base.DesignMode ? "" : base.LocalizationPath));
				}
				return this._localization;
			}
		}

		// Token: 0x170032DD RID: 13021
		// (get) Token: 0x0600A0D6 RID: 41174 RVA: 0x0023C8EF File Offset: 0x0023AAEF
		// (set) Token: 0x0600A0D7 RID: 41175 RVA: 0x0023C8F7 File Offset: 0x0023AAF7
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

		// Token: 0x0600A0D8 RID: 41176 RVA: 0x0023C900 File Offset: 0x0023AB00
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.Calendar.Skin = base.RuntimeSkin;
			this.DateInput.Skin = base.RuntimeSkin;
			this.TimeView.Skin = base.RuntimeSkin;
		}

		// Token: 0x170032DE RID: 13022
		// (get) Token: 0x0600A0D9 RID: 41177 RVA: 0x0023C93C File Offset: 0x0023AB3C
		// (set) Token: 0x0600A0DA RID: 41178 RVA: 0x0023C944 File Offset: 0x0023AB44
		public override string ImagesPath
		{
			get
			{
				return base.ImagesPath;
			}
			set
			{
				this.TimeView.ImagesPath = value;
				this.Calendar.ImagesPath = value;
				base.ImagesPath = value;
			}
		}

		// Token: 0x170032DF RID: 13023
		// (get) Token: 0x0600A0DB RID: 41179 RVA: 0x0023C965 File Offset: 0x0023AB65
		// (set) Token: 0x0600A0DC RID: 41180 RVA: 0x0023C96D File Offset: 0x0023AB6D
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
				this.TimeView.EnableEmbeddedSkins = value;
				base.EnableEmbeddedSkins = value;
			}
		}

		// Token: 0x170032E0 RID: 13024
		// (get) Token: 0x0600A0DD RID: 41181 RVA: 0x0023C99A File Offset: 0x0023AB9A
		// (set) Token: 0x0600A0DE RID: 41182 RVA: 0x0023C9A2 File Offset: 0x0023ABA2
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
				this.TimeView.EnableEmbeddedScripts = value;
				base.EnableEmbeddedScripts = value;
			}
		}

		// Token: 0x170032E1 RID: 13025
		// (get) Token: 0x0600A0DF RID: 41183 RVA: 0x0023C9CF File Offset: 0x0023ABCF
		// (set) Token: 0x0600A0E0 RID: 41184 RVA: 0x0023C9D7 File Offset: 0x0023ABD7
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
				this.TimeView.EnableEmbeddedBaseStylesheet = value;
				base.EnableEmbeddedBaseStylesheet = value;
			}
		}

		// Token: 0x170032E2 RID: 13026
		// (get) Token: 0x0600A0E1 RID: 41185 RVA: 0x0023CA04 File Offset: 0x0023AC04
		// (set) Token: 0x0600A0E2 RID: 41186 RVA: 0x0023CA0C File Offset: 0x0023AC0C
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
				this.TimeView.RegisterWithScriptManager = value;
				base.RegisterWithScriptManager = value;
			}
		}

		// Token: 0x170032E3 RID: 13027
		// (get) Token: 0x0600A0E3 RID: 41187 RVA: 0x0023CA3C File Offset: 0x0023AC3C
		// (set) Token: 0x0600A0E4 RID: 41188 RVA: 0x0023CA65 File Offset: 0x0023AC65
		[Description("Enable client side navigation with keyboard")]
		[DefaultValue(false)]
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
				this.Calendar.EnableKeyboardNavigation = value;
			}
		}

		// Token: 0x170032E4 RID: 13028
		// (get) Token: 0x0600A0E5 RID: 41189 RVA: 0x0023CA98 File Offset: 0x0023AC98
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string ValidationDate
		{
			get
			{
				if (this.SelectedDate != null)
				{
					return this.SelectedDate.Value.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
				}
				return "";
			}
		}

		// Token: 0x170032E5 RID: 13029
		// (get) Token: 0x0600A0E6 RID: 41190 RVA: 0x0023CADB File Offset: 0x0023ACDB
		[Description("Gets the RadTimeView instance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual RadTimeView TimeView
		{
			get
			{
				if (this._timeView == null)
				{
					this._timeView = new RadTimeView();
				}
				return this._timeView;
			}
		}

		// Token: 0x170032E6 RID: 13030
		// (get) Token: 0x0600A0E7 RID: 41191 RVA: 0x0023CAF6 File Offset: 0x0023ACF6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[Description("Gets the TimePopupButton instance.")]
		public virtual TimePopupButton TimePopupButton
		{
			get
			{
				if (this._popupButton == null)
				{
					this._popupButton = new TimePopupButton(this);
				}
				return this._popupButton;
			}
		}

		// Token: 0x170032E7 RID: 13031
		// (get) Token: 0x0600A0E8 RID: 41192 RVA: 0x0023CB12 File Offset: 0x0023AD12
		// (set) Token: 0x0600A0E9 RID: 41193 RVA: 0x0023CB1A File Offset: 0x0023AD1A
		[Browsable(false)]
		public override bool AutoPostBack
		{
			get
			{
				return base.AutoPostBack;
			}
			set
			{
				base.AutoPostBack = value;
			}
		}

		// Token: 0x170032E8 RID: 13032
		// (get) Token: 0x0600A0EA RID: 41194 RVA: 0x0023CB23 File Offset: 0x0023AD23
		// (set) Token: 0x0600A0EB RID: 41195 RVA: 0x0023CB30 File Offset: 0x0023AD30
		public override CultureInfo Culture
		{
			get
			{
				return this.Calendar.Culture;
			}
			set
			{
				this.DateInput.Culture = value;
				this.TimeView.Culture = value;
				this.Calendar.CultureInfo = value;
			}
		}

		// Token: 0x170032E9 RID: 13033
		// (get) Token: 0x0600A0EC RID: 41196 RVA: 0x0023CB58 File Offset: 0x0023AD58
		// (set) Token: 0x0600A0ED RID: 41197 RVA: 0x0023CBA9 File Offset: 0x0023ADA9
		[Category("Behavior")]
		[DefaultValue(typeof(AutoPostBackControl), "None")]
		[ClientControlProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Description("Gets or sets the AutoPostBack.")]
		public virtual AutoPostBackControl AutoPostBackControl
		{
			get
			{
				if (this.ViewState["AutoPostBackControl"] == null)
				{
					return AutoPostBackControl.None;
				}
				this.SetAutoPostBack((AutoPostBackControl)this.ViewState["AutoPostBackControl"]);
				return (AutoPostBackControl)this.ViewState["AutoPostBackControl"];
			}
			set
			{
				this.SetAutoPostBack(value);
				this.ViewState["AutoPostBackControl"] = value;
			}
		}

		// Token: 0x170032EA RID: 13034
		// (get) Token: 0x0600A0EE RID: 41198 RVA: 0x0023CBC8 File Offset: 0x0023ADC8
		// (set) Token: 0x0600A0EF RID: 41199 RVA: 0x0023CBF5 File Offset: 0x0023ADF5
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("The ID of the RadTimeView that will be shared among several RadDateTimePickers.")]
		[TypeConverter("Telerik.Web.Design.TimeViewIdConverter")]
		public virtual string SharedTimeViewID
		{
			get
			{
				object obj = this.ViewState["SharedTimeViewID"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["SharedTimeViewID"] = value;
			}
		}

		// Token: 0x170032EB RID: 13035
		// (get) Token: 0x0600A0F0 RID: 41200 RVA: 0x0023CC08 File Offset: 0x0023AE08
		// (set) Token: 0x0600A0F1 RID: 41201 RVA: 0x0023CC70 File Offset: 0x0023AE70
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadTimeView SharedTimeView
		{
			get
			{
				if (this._sharedTimeView == null && !string.IsNullOrEmpty(this.SharedTimeViewID))
				{
					this._sharedTimeView = (this.NamingContainer.FindControl(this.SharedTimeViewID) as RadTimeView);
					if (this._sharedTimeView == null)
					{
						this._sharedTimeView = (this.Page.FindControl(this.SharedTimeViewID) as RadTimeView);
					}
				}
				return this._sharedTimeView;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("You need to pass a valid reference to a RadTimeView instance in order to use the SharedTimeView functionality.");
				}
				this._sharedTimeView = value;
			}
		}

		// Token: 0x0600A0F2 RID: 41202 RVA: 0x0023CC87 File Offset: 0x0023AE87
		private void dataList_ItemCreated(object sender, DataListItemEventArgs e)
		{
			this.OnItemCreated(new TimePickerEventArgs(e.Item));
		}

		// Token: 0x0600A0F3 RID: 41203 RVA: 0x0023CC9A File Offset: 0x0023AE9A
		private void dataList_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			this.OnItemDataBound(new TimePickerEventArgs(e.Item));
		}

		// Token: 0x1400017D RID: 381
		// (add) Token: 0x0600A0F4 RID: 41204 RVA: 0x0023CCAD File Offset: 0x0023AEAD
		// (remove) Token: 0x0600A0F5 RID: 41205 RVA: 0x0023CCC0 File Offset: 0x0023AEC0
		public event RadDateTimePicker.TimeItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadDateTimePicker.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDateTimePicker.EventItemDataBound, value);
			}
		}

		// Token: 0x0600A0F6 RID: 41206 RVA: 0x0023CCD4 File Offset: 0x0023AED4
		protected virtual void OnItemDataBound(TimePickerEventArgs e)
		{
			RadDateTimePicker.TimeItemEventHandler timeItemEventHandler = (RadDateTimePicker.TimeItemEventHandler)base.Events[RadDateTimePicker.EventItemDataBound];
			if (timeItemEventHandler != null)
			{
				timeItemEventHandler(this, e);
			}
		}

		// Token: 0x0600A0F7 RID: 41207 RVA: 0x0023CD04 File Offset: 0x0023AF04
		protected virtual void OnItemCreated(TimePickerEventArgs e)
		{
			RadDateTimePicker.TimeItemEventHandler timeItemEventHandler = (RadDateTimePicker.TimeItemEventHandler)base.Events[RadDateTimePicker.EventItemCreated];
			if (timeItemEventHandler != null)
			{
				timeItemEventHandler(this, e);
			}
		}

		// Token: 0x1400017E RID: 382
		// (add) Token: 0x0600A0F8 RID: 41208 RVA: 0x0023CD32 File Offset: 0x0023AF32
		// (remove) Token: 0x0600A0F9 RID: 41209 RVA: 0x0023CD45 File Offset: 0x0023AF45
		public event RadDateTimePicker.TimeItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadDateTimePicker.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDateTimePicker.EventItemCreated, value);
			}
		}

		// Token: 0x0600A0FA RID: 41210 RVA: 0x0023CD58 File Offset: 0x0023AF58
		protected override void CreateTimeControls()
		{
			if (!base.DesignMode && this.DateInput.DisplayDateFormat == this.DateInput.Culture.DateTimeFormat.ShortDatePattern)
			{
				this.DateInput.DisplayDateFormat = "g";
			}
			if (!base.DesignMode && this.DateInput.DateFormat == this.DateInput.Culture.DateTimeFormat.ShortDatePattern)
			{
				this.DateInput.DateFormat = "g";
			}
			this.TimePopupButton.ID = "timePopupLink";
			this.Controls.Add(this.TimePopupButton);
			this.TimeView.ID = "timeView";
			this.Controls.Add(this.TimeView);
			this.TimeView.DataList.ItemDataBound += this.dataList_ItemDataBound;
			this.TimeView.DataList.ItemCreated += this.dataList_ItemCreated;
		}

		// Token: 0x0600A0FB RID: 41211 RVA: 0x0023CE5D File Offset: 0x0023B05D
		protected override DateTime TruncateTimeComponent(DateTime value)
		{
			return value;
		}

		// Token: 0x0600A0FC RID: 41212 RVA: 0x0023CE60 File Offset: 0x0023B060
		protected override void SetRenderMode(RenderMode mode)
		{
			this.TimeView.RenderMode = mode;
			base.SetRenderMode(mode);
		}

		// Token: 0x0600A0FD RID: 41213 RVA: 0x0023CE75 File Offset: 0x0023B075
		private void SetAutoPostBack(AutoPostBackControl autoPostBackControl)
		{
			if (autoPostBackControl != AutoPostBackControl.None)
			{
				this.DateInput.AutoPostBack = true;
				return;
			}
			this.DateInput.AutoPostBack = false;
		}

		// Token: 0x0600A0FE RID: 41214 RVA: 0x0023CE93 File Offset: 0x0023B093
		internal override void ConfigureChildren()
		{
			base.ConfigureChildren();
			this.ConfigureTimeView();
		}

		// Token: 0x0600A0FF RID: 41215 RVA: 0x0023CEA4 File Offset: 0x0023B0A4
		internal void ConfigureTimeView()
		{
			this.TimeView.EnableAriaSupport = base.EnableAriaSupport;
			if (!base.DesignMode)
			{
				if (!string.IsNullOrEmpty(this.SharedTimeViewID) && this.SharedTimeView == null)
				{
					throw new ArgumentException("Could not find a RadTimeView control with ID of '" + this.SharedTimeViewID + "'.  \r\nUse the SharedTimeView property to pass a direct reference in your code-behind if the control is not in the current naming container.");
				}
				if (this.SharedTimeView != null)
				{
					this.TimeView.Visible = false;
					if (this.EnableEmbeddedSkins)
					{
						this.SharedTimeView.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
					}
					if (this.EnableEmbeddedScripts)
					{
						this.SharedTimeView.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
					}
					if (this.EnableEmbeddedBaseStylesheet)
					{
						this.SharedTimeView.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
					}
					if (!string.IsNullOrEmpty(this.ImagesPath))
					{
						this.SharedTimeView.ImagesPath = this.ImagesPath;
					}
					if (base.EnableAriaSupport)
					{
						this.SharedTimeView.EnableAriaSupport = base.EnableAriaSupport;
					}
				}
			}
		}

		// Token: 0x0600A100 RID: 41216 RVA: 0x0023CF96 File Offset: 0x0023B196
		protected override void SetDefaultSize()
		{
			this.defaultWidth = Unit.Pixel(160);
		}

		// Token: 0x170032EC RID: 13036
		// (get) Token: 0x0600A101 RID: 41217 RVA: 0x0023CFA8 File Offset: 0x0023B1A8
		protected override string CssClassFormatString
		{
			get
			{
				if (!string.IsNullOrEmpty(base.RuntimeSkin))
				{
					return "RadPicker RadDateTimePicker RadPicker_{0}";
				}
				return "RadPicker RadDateTimePicker";
			}
		}

		// Token: 0x0600A102 RID: 41218 RVA: 0x0023CFC4 File Offset: 0x0023B1C4
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			bool visible = this.Calendar.Visible;
			bool visible2 = this.TimeView.Visible;
			if (base.DesignMode)
			{
				this.Calendar.Visible = false;
				this.TimeView.Visible = false;
			}
			base.RenderChildren(writer);
			this.Calendar.Visible = visible;
			this.TimeView.Visible = visible2;
		}

		// Token: 0x0600A103 RID: 41219 RVA: 0x0023D028 File Offset: 0x0023B228
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
					this.DatePopupButton.RenderControl(writer);
				}
				this.Calendar.RenderControl(writer);
				writer.RenderEndTag();
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

		// Token: 0x170032ED RID: 13037
		// (get) Token: 0x0600A104 RID: 41220 RVA: 0x0023D1C0 File Offset: 0x0023B3C0
		protected override bool ShouldRenderAdditionalControls
		{
			get
			{
				return this.Controls.Count > 5;
			}
		}

		// Token: 0x0600A105 RID: 41221 RVA: 0x0023D1D4 File Offset: 0x0023B3D4
		protected override void AddAdditionalControlComponents(HtmlTextWriter writer)
		{
			bool flag = false;
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (!(control is RadDateInput) && !(control is RadCalendar) && !(control is CalendarPopupButton) && !(control is TimePopupButton) && !(control is RadTimeView))
				{
					if (!flag)
					{
						if (!base.DesignMode && (base.Browser.IsBrowser("Gecko") || base.Browser.IsBrowser("Firefox")))
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

		// Token: 0x0600A106 RID: 41222 RVA: 0x0023D2BC File Offset: 0x0023B4BC
		protected internal override bool isOnlyInputRendered()
		{
			return !this.DatePopupButton.Visible && !this.TimePopupButton.Visible && this.Controls.Count == 5;
		}

		// Token: 0x0600A107 RID: 41223 RVA: 0x0023D2E8 File Offset: 0x0023B4E8
		protected override void DescribeProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperties(descriptor);
			descriptor.AddComponentProperty("timeView", (this.SharedTimeView != null) ? this.SharedTimeView.ClientID : this.TimeView.ClientID);
			descriptor.AddProperty("_timePopupControlID", this.TimePopupButton.ClientID);
			descriptor.AddScriptProperty("_TimePopupButtonSettings", string.Format("{{  ResolvedImageUrl : \"{0}\", ResolvedHoverImageUrl : \"{1}\"}}", this.TimePopupButton.ResolvedImageUrl, this.TimePopupButton.ResolvedHoverImageUrl));
			if (this != null && !(this is RadTimePicker))
			{
				string script = string.Format("{{ ShowAnimationDuration:{0},ShowAnimationType:{1},HideAnimationDuration:{2},HideAnimationType:{3}}}", new object[]
				{
					base.ShowAnimation.Duration,
					(int)base.ShowAnimation.Type,
					(this.AutoPostBackControl != AutoPostBackControl.None || base.SharedCalendar != null || this.SharedTimeView != null) ? 0 : base.HideAnimation.Duration,
					(int)base.HideAnimation.Type
				});
				descriptor.AddScriptProperty("_animationSettings", script);
			}
		}

		// Token: 0x0600A108 RID: 41224 RVA: 0x0023D3FF File Offset: 0x0023B5FF
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<object>(descriptor, "autoPostBackControl", this.AutoPostBackControl, Enum.Parse(typeof(AutoPostBackControl), "None"));
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600A109 RID: 41225 RVA: 0x0023D433 File Offset: 0x0023B633
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002CF4 RID: 11508
		private TimePopupButton _popupButton;

		// Token: 0x04002CF5 RID: 11509
		private RadTimeView _timeView;

		// Token: 0x04002CF6 RID: 11510
		private static readonly object EventItemDataBound = new object();

		// Token: 0x04002CF7 RID: 11511
		private static readonly object EventItemCreated = new object();

		// Token: 0x04002CF8 RID: 11512
		private DateTimePickerStrings _localization;

		// Token: 0x04002CF9 RID: 11513
		private RadTimeView _sharedTimeView;

		// Token: 0x0200100B RID: 4107
		// (Invoke) Token: 0x0600A10C RID: 41228
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public delegate void TimeItemEventHandler(object sender, TimePickerEventArgs e);
	}
}
