using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI.Calendar.Collections;
using Telerik.Web.UI.Calendar.Persistence;
using Telerik.Web.UI.Calendar.Utils;
using Telerik.Web.UI.Design.DatePickerAttributes;

namespace Telerik.Web.UI
{
	// Token: 0x02000A37 RID: 2615
	[ClientScriptResource("Telerik.Web.UI.RadMonthYearPicker", "Telerik.Web.UI.Calendar.RadCalendarScript.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Calendar", typeof(RadCalendar))]
	[ClientScriptResource("Telerik.Web.UI.RadMonthYearPicker", "Telerik.Web.UI.Calendar.RadCalendarCommonScript.js")]
	[ClientScriptResource("Telerik.Web.UI.RadMonthYearPicker", "Telerik.Web.UI.Calendar.RadMonthYearPickerScript.js")]
	[ClientScriptResource("Telerik.Web.UI.RadMonthYearPicker", "Telerik.Web.UI.Calendar.RadPickersPopupDirectionEnumeration.js")]
	[ClientScriptResource("Telerik.Web.UI.RadMonthYearPicker", "Telerik.Web.UI.Common.Navigation.OverlayScript.js")]
	[RequiredScript(typeof(jQuery))]
	[RequiredScript(typeof(MaterialRipple))]
	[EmbeddedSkin("Calendar", "Default", typeof(RadCalendar))]
	[ToolboxBitmap(typeof(RadDatePicker), "Telerik.Web.UI.MonthyearPicker.png")]
	[ToolboxData("<{0}:RadMonthYearPicker Runat=\"server\"></{0}:RadMonthYearPicker>")]
	[LightweightRendering]
	[TelerikToolboxCategory("Date/Color Picker")]
	[Designer("Telerik.Web.Design.RadMonthYearPickerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ParseChildren(true)]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadMonthYearPicker))]
	[PersistChildren(false)]
	[ValidationProperty("ValidationDate")]
	[DefaultEvent("SelectedDateChanged")]
	[DefaultProperty("SelectedDate")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadMonthYearPicker))]
	[Description("Telerik RadMonthYearPicker")]
	[ControlValueProperty("SelectedDate")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadMonthYearPicker : PropertiesControl, INamingContainer, ILocalizableControl, IPostBackEventHandler, IPostBackDataHandler, ILabelableControl
	{
		// Token: 0x17002092 RID: 8338
		// (get) Token: 0x0600633D RID: 25405 RVA: 0x00174C6C File Offset: 0x00172E6C
		internal MonthYearPickerStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new MonthYearPickerStrings(new LocalizationProvider("RadMonthYearPicker.Main", this, base.DesignMode ? "" : this.LocalizationPath));
				}
				return this._localization;
			}
		}

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x0600633E RID: 25406 RVA: 0x00174CA7 File Offset: 0x00172EA7
		// (set) Token: 0x0600633F RID: 25407 RVA: 0x00174CC8 File Offset: 0x00172EC8
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where RadMonthYearPicker will look for its .resx localization files.")]
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

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x06006340 RID: 25408 RVA: 0x00174D1B File Offset: 0x00172F1B
		// (set) Token: 0x06006341 RID: 25409 RVA: 0x00174D23 File Offset: 0x00172F23
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

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x06006342 RID: 25410 RVA: 0x00174D2C File Offset: 0x00172F2C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The style applied to month cells.")]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle MonthCellsStyle
		{
			get
			{
				if (this.monthCellsStyle == null)
				{
					this.monthCellsStyle = new TableItemStyle();
				}
				return this.monthCellsStyle;
			}
		}

		// Token: 0x17002096 RID: 8342
		// (get) Token: 0x06006343 RID: 25411 RVA: 0x00174D47 File Offset: 0x00172F47
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The style applied to year cells.")]
		public virtual TableItemStyle YearCellsStyle
		{
			get
			{
				if (this.yearCellsStyle == null)
				{
					this.yearCellsStyle = new TableItemStyle();
				}
				return this.yearCellsStyle;
			}
		}

		// Token: 0x17002097 RID: 8343
		// (get) Token: 0x06006344 RID: 25412 RVA: 0x00174D62 File Offset: 0x00172F62
		// (set) Token: 0x06006345 RID: 25413 RVA: 0x00174D6A File Offset: 0x00172F6A
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return base.EnableEmbeddedSkins;
			}
			set
			{
				this.DateInput.EnableEmbeddedSkins = value;
				base.EnableEmbeddedSkins = value;
			}
		}

		// Token: 0x17002098 RID: 8344
		// (get) Token: 0x06006346 RID: 25414 RVA: 0x00174D7F File Offset: 0x00172F7F
		protected internal bool EmptySkin
		{
			get
			{
				return string.IsNullOrEmpty(base.RuntimeSkin);
			}
		}

		// Token: 0x17002099 RID: 8345
		// (get) Token: 0x06006347 RID: 25415 RVA: 0x00174D8C File Offset: 0x00172F8C
		// (set) Token: 0x06006348 RID: 25416 RVA: 0x00174D94 File Offset: 0x00172F94
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedScripts
		{
			get
			{
				return base.EnableEmbeddedScripts;
			}
			set
			{
				this.DateInput.EnableEmbeddedScripts = value;
				base.EnableEmbeddedScripts = value;
			}
		}

		// Token: 0x1700209A RID: 8346
		// (get) Token: 0x06006349 RID: 25417 RVA: 0x00174DA9 File Offset: 0x00172FA9
		// (set) Token: 0x0600634A RID: 25418 RVA: 0x00174DB1 File Offset: 0x00172FB1
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return base.EnableEmbeddedBaseStylesheet;
			}
			set
			{
				this.DateInput.EnableEmbeddedBaseStylesheet = value;
				base.EnableEmbeddedBaseStylesheet = value;
			}
		}

		// Token: 0x1700209B RID: 8347
		// (get) Token: 0x0600634B RID: 25419 RVA: 0x00174DC8 File Offset: 0x00172FC8
		// (set) Token: 0x0600634C RID: 25420 RVA: 0x00174DF1 File Offset: 0x00172FF1
		[DefaultValue(false)]
		[Description("Enable client side navigation with keyboard")]
		public bool EnableKeyboardNavigation
		{
			get
			{
				object obj = this.ViewState["EnableKeyboardNavigation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableKeyboardNavigation"] = value;
			}
		}

		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x0600634D RID: 25421 RVA: 0x00174E09 File Offset: 0x00173009
		// (set) Token: 0x0600634E RID: 25422 RVA: 0x00174E11 File Offset: 0x00173011
		[NotifyParentProperty(true)]
		public override bool RegisterWithScriptManager
		{
			get
			{
				return base.RegisterWithScriptManager;
			}
			set
			{
				this.DateInput.RegisterWithScriptManager = value;
				base.RegisterWithScriptManager = value;
			}
		}

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x0600634F RID: 25423 RVA: 0x00174E26 File Offset: 0x00173026
		// (remove) Token: 0x06006350 RID: 25424 RVA: 0x00174E39 File Offset: 0x00173039
		[Description("Occurs after all child controls of the RadMonthYearPicker control have been created.")]
		public event EventHandler ChildrenCreated
		{
			add
			{
				base.Events.AddHandler(RadMonthYearPicker.EventChildrenCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMonthYearPicker.EventChildrenCreated, value);
			}
		}

		// Token: 0x06006351 RID: 25425 RVA: 0x00174E4C File Offset: 0x0017304C
		protected virtual void OnChildrenCreated()
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadMonthYearPicker.EventChildrenCreated];
			if (eventHandler != null)
			{
				eventHandler(this, new EventArgs());
			}
		}

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06006352 RID: 25426 RVA: 0x00174E7E File Offset: 0x0017307E
		// (remove) Token: 0x06006353 RID: 25427 RVA: 0x00174E91 File Offset: 0x00173091
		[Description("Occurs when the selected date of the RadMonthYearPicker changes between posts to the server.")]
		public event SelectedDateChangedEventHandler SelectedDateChanged
		{
			add
			{
				base.Events.AddHandler(RadMonthYearPicker.EventSelectedDateChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMonthYearPicker.EventSelectedDateChanged, value);
			}
		}

		// Token: 0x06006354 RID: 25428 RVA: 0x00174EA4 File Offset: 0x001730A4
		protected virtual void OnSelectedDateChanged(SelectedDateChangedEventArgs eventArgs)
		{
			SelectedDateChangedEventHandler selectedDateChangedEventHandler = (SelectedDateChangedEventHandler)base.Events[RadMonthYearPicker.EventSelectedDateChanged];
			if (selectedDateChangedEventHandler != null)
			{
				selectedDateChangedEventHandler(this, eventArgs);
			}
		}

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06006355 RID: 25429 RVA: 0x00174ED2 File Offset: 0x001730D2
		// (remove) Token: 0x06006356 RID: 25430 RVA: 0x00174EE5 File Offset: 0x001730E5
		[Description("Occurs when the selected date of the RadMonthYearPicker changes between posts to the server.")]
		public event MonthYearViewCellCreatedEventHandler ViewCellCreated
		{
			add
			{
				base.Events.AddHandler(RadMonthYearPicker.EventViewCellCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMonthYearPicker.EventViewCellCreated, value);
			}
		}

		// Token: 0x06006357 RID: 25431 RVA: 0x00174EF8 File Offset: 0x001730F8
		protected virtual void OnViewCellCreated(MonthYearViewCellCreatedEventArgs eventArgs)
		{
			MonthYearViewCellCreatedEventHandler monthYearViewCellCreatedEventHandler = (MonthYearViewCellCreatedEventHandler)base.Events[RadMonthYearPicker.EventViewCellCreated];
			if (monthYearViewCellCreatedEventHandler != null)
			{
				monthYearViewCellCreatedEventHandler(this, eventArgs);
			}
		}

		// Token: 0x06006358 RID: 25432 RVA: 0x00174F26 File Offset: 0x00173126
		internal void FireViewCellCreated(MonthYearViewCellCreatedEventArgs eventArgs)
		{
			this.OnViewCellCreated(eventArgs);
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x00174F2F File Offset: 0x0017312F
		internal new void EnsureChildControls()
		{
			base.EnsureChildControls();
		}

		// Token: 0x0600635A RID: 25434 RVA: 0x00174F37 File Offset: 0x00173137
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		// Token: 0x0600635B RID: 25435 RVA: 0x00174F40 File Offset: 0x00173140
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.EnsureChildControls();
			this.MonthYearTableView.ID = (this.IsLightweightRendering ? (this.ClientID + "_MonthYearTableViewID") : "MonthYearTableViewID");
			this.MonthYearTableView.RenderInvisible = true;
			if (!this.IsLightweightRendering)
			{
				this.Controls.Add(this.MonthYearTableView);
				this.MonthYearTableView.Initialize();
			}
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x00174FB4 File Offset: 0x001731B4
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.DateInput.ID = "dateInput";
			this.DateInput.TextChanged += this.DateInput_TextChanged;
			this.Controls.Add(this.DateInput);
			this.DatePopupButton.ID = "popupButton";
			if (!this.IsLightweightRendering)
			{
				this.Controls.Add(this.DatePopupButton);
			}
			else
			{
				this.DatePopupButton.ID = this.ClientID + "_popupButton";
				this.DateInput.Controls.Add(this.DatePopupButton);
				this.DateInput.Controls.Add(this.MonthYearTableView);
				this.MonthYearTableView.Initialize();
			}
			this.OnChildrenCreated();
		}

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x0600635D RID: 25437 RVA: 0x00175082 File Offset: 0x00173282
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x0600635E RID: 25438 RVA: 0x00175086 File Offset: 0x00173286
		protected HttpBrowserCapabilities Browser
		{
			get
			{
				return this.Context.Request.Browser;
			}
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x00175098 File Offset: 0x00173298
		protected virtual void SetRenderMode(RenderMode mode)
		{
			this.DateInput.RenderMode = mode;
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x001750A6 File Offset: 0x001732A6
		protected internal virtual bool IsOnlyInputRendered()
		{
			return !this.DatePopupButton.Visible && this.Controls.Count == 3;
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x001750C5 File Offset: 0x001732C5
		protected virtual void SetDefaultSize()
		{
			this.defaultWidth = Unit.Pixel(160);
		}

		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x06006362 RID: 25442 RVA: 0x001750D7 File Offset: 0x001732D7
		protected override string CssClassFormatString
		{
			get
			{
				if (!string.IsNullOrEmpty(base.RuntimeSkin))
				{
					return "RadPicker RadMonthYearPicker RadPicker_{0}";
				}
				return "RadPicker RadMonthYearPicker";
			}
		}

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x06006363 RID: 25443 RVA: 0x001750F1 File Offset: 0x001732F1
		private bool IsLightweightRendering
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

		// Token: 0x06006364 RID: 25444 RVA: 0x00175120 File Offset: 0x00173320
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
			if (this.IsOnlyInputRendered())
			{
				writer.AddStyleAttribute("display", "inline");
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
				else if (!base.DesignMode && (this.Browser.IsBrowser("Gecko") || this.Browser.IsBrowser("Firefox")) && !this.Browser.IsBrowser("Safari") && !this.Browser.IsBrowser("Chrome"))
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "20px");
				}
			}
			base.AddAttributesToRender(writer);
			this.ID = id;
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x00175304 File Offset: 0x00173504
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
			if (this.Browser.IsBrowser("IE") || this.Browser.IsBrowser("Opera") || this.Browser.IsBrowser("Safari") || this.Browser.IsBrowser("Chrome"))
			{
				writer.AddStyleAttribute("display", "inline-block");
				return;
			}
			if (this.Browser.IsBrowser("Gecko") || this.Browser.IsBrowser("Firefox"))
			{
				writer.AddStyleAttribute("display", "-moz-inline-stack");
				return;
			}
			if (this.Browser.IsBrowser("Safari") || this.Browser.IsBrowser("Chrome"))
			{
				writer.AddStyleAttribute("white-space", "normal");
			}
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x00175458 File Offset: 0x00173658
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (this.IsOnlyInputRendered())
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
				return;
			}
			if (!this.IsLightweightRendering)
			{
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
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				this.AddControlComponents(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
				return;
			}
			this.AddControlComponents(writer);
		}

		// Token: 0x06006367 RID: 25447 RVA: 0x0017565C File Offset: 0x0017385C
		protected virtual void AddControlComponents(HtmlTextWriter writer)
		{
			if (this.IsLightweightRendering)
			{
				if (this.DateInput.Visible)
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
					this.DateInput.RenderControl(writer);
					return;
				}
			}
			else
			{
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
				if (!this.IsOnlyInputRendered())
				{
					this.DatePopupButton.RenderControl(writer);
				}
				writer.RenderEndTag();
				this.AddAdditionalControlComponents(writer);
			}
		}

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x06006368 RID: 25448 RVA: 0x001757B7 File Offset: 0x001739B7
		protected virtual bool ShouldRenderAdditionalControls
		{
			get
			{
				return this.Controls.Count > 3;
			}
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x001757CC File Offset: 0x001739CC
		protected virtual void AddAdditionalControlComponents(HtmlTextWriter writer)
		{
			bool flag = false;
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (!(control is RadDateInput) && !(control is CalendarPopupButton))
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

		// Token: 0x0600636A RID: 25450 RVA: 0x0017589C File Offset: 0x00173A9C
		protected override void Render(HtmlTextWriter writer)
		{
			try
			{
				if (base.DesignMode)
				{
					this.EnsureChildControls();
					this.ConfigureChildren();
					this.SetChildrenSites(base.Site);
					writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				}
				writer = new HtmlTextWriter(writer);
				this.RenderAuxiliaryDates(writer);
				base.Render(writer);
			}
			finally
			{
				this.SetChildrenSites(null);
			}
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x00175908 File Offset: 0x00173B08
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderValidationHiddenInput(writer);
			base.RenderContents(writer);
		}

		// Token: 0x0600636C RID: 25452 RVA: 0x00175918 File Offset: 0x00173B18
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

		// Token: 0x0600636D RID: 25453 RVA: 0x001759C8 File Offset: 0x00173BC8
		internal void SetChildrenSites(ISite site)
		{
			Control[] array = new Control[]
			{
				this.DateInput
			};
			foreach (Control control in array)
			{
				control.Site = site;
			}
		}

		// Token: 0x170020A2 RID: 8354
		// (get) Token: 0x0600636E RID: 25454 RVA: 0x00175A07 File Offset: 0x00173C07
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Category("Behavior")]
		[Description("Gets the MonthYearView instance.")]
		[NotifyParentProperty(true)]
		public virtual MonthYearView MonthYearTableView
		{
			get
			{
				if (this._monthYearTableView == null)
				{
					this._monthYearTableView = new MonthYearView(this);
				}
				return this._monthYearTableView;
			}
		}

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x0600636F RID: 25455 RVA: 0x00175A23 File Offset: 0x00173C23
		[Description("Gets the RadDateInput instance.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x170020A4 RID: 8356
		// (get) Token: 0x06006370 RID: 25456 RVA: 0x00175A3E File Offset: 0x00173C3E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Gets the DatePopupButton instance.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual MonthYearPopupButton DatePopupButton
		{
			get
			{
				if (this._datePopupButton == null)
				{
					this._datePopupButton = new MonthYearPopupButton(this);
				}
				return this._datePopupButton;
			}
		}

		// Token: 0x170020A5 RID: 8357
		// (get) Token: 0x06006371 RID: 25457 RVA: 0x00175A5A File Offset: 0x00173C5A
		// (set) Token: 0x06006372 RID: 25458 RVA: 0x00175A67 File Offset: 0x00173C67
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

		// Token: 0x170020A6 RID: 8358
		// (get) Token: 0x06006373 RID: 25459 RVA: 0x00175A75 File Offset: 0x00173C75
		// (set) Token: 0x06006374 RID: 25460 RVA: 0x00175A7D File Offset: 0x00173C7D
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

		// Token: 0x170020A7 RID: 8359
		// (get) Token: 0x06006375 RID: 25461 RVA: 0x00175A86 File Offset: 0x00173C86
		// (set) Token: 0x06006376 RID: 25462 RVA: 0x00175A93 File Offset: 0x00173C93
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

		// Token: 0x170020A8 RID: 8360
		// (get) Token: 0x06006377 RID: 25463 RVA: 0x00175AAD File Offset: 0x00173CAD
		// (set) Token: 0x06006378 RID: 25464 RVA: 0x00175ADC File Offset: 0x00173CDC
		[Localizable(true)]
		[Category("Accessibility")]
		[DefaultValue("Title and navigation")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the title attribute for the hidden field.")]
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

		// Token: 0x170020A9 RID: 8361
		// (get) Token: 0x06006379 RID: 25465 RVA: 0x00175AEF File Offset: 0x00173CEF
		// (set) Token: 0x0600637A RID: 25466 RVA: 0x00175B1E File Offset: 0x00173D1E
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Accessibility")]
		[Description("Gets or sets the summary attribute for the table which wraps the RadMonthYearPicker controls.")]
		[DefaultValue("Table holding date picker control for selection of dates.")]
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

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x0600637B RID: 25467 RVA: 0x00175B31 File Offset: 0x00173D31
		// (set) Token: 0x0600637C RID: 25468 RVA: 0x00175B60 File Offset: 0x00173D60
		[Localizable(true)]
		[Category("Accessibility")]
		[DefaultValue("RadMonthYearPicker")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the caption for the table which wraps the RadMonthYearPicker controls.")]
		public virtual string WrapperTableCaption
		{
			get
			{
				if (this.ViewState["WrapperTableCaption"] == null)
				{
					return "RadMonthYearPicker";
				}
				return (string)this.ViewState["WrapperTableCaption"];
			}
			set
			{
				this.ViewState["WrapperTableCaption"] = value;
			}
		}

		// Token: 0x170020AB RID: 8363
		// (get) Token: 0x0600637D RID: 25469 RVA: 0x00175B73 File Offset: 0x00173D73
		// (set) Token: 0x0600637E RID: 25470 RVA: 0x00175B9F File Offset: 0x00173D9F
		[Category("Behavior")]
		[ClientPropertyName("_popupDirection")]
		[ClientControlProperty]
		[Description("Gets or sets the direction in which the popup MonthYearView is displayed, with relation to the RadMonthYearPicker control.")]
		[DefaultValue(DatePickerPopupDirection.BottomRight)]
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

		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x0600637F RID: 25471 RVA: 0x00175BB7 File Offset: 0x00173DB7
		[Description("The subproperties can be used to modify the fast Month/Year navigation popup settings.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Navigation Management")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MonthYearNavigationSettings MonthYearNavigationSettings
		{
			get
			{
				if (this._monthYearNavigationSettings == null)
				{
					this._monthYearNavigationSettings = new MonthYearNavigationSettings(this.ViewState, this);
				}
				return this._monthYearNavigationSettings;
			}
		}

		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x06006380 RID: 25472 RVA: 0x00175BD9 File Offset: 0x00173DD9
		// (set) Token: 0x06006381 RID: 25473 RVA: 0x00175C08 File Offset: 0x00173E08
		[Category("Behavior")]
		[Description("Gets or sets the z-index style of the control's popups")]
		[ClientControlProperty]
		[ClientPropertyName("_zIndex")]
		[DefaultValue(5000)]
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

		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x06006382 RID: 25474 RVA: 0x00175C20 File Offset: 0x00173E20
		// (set) Token: 0x06006383 RID: 25475 RVA: 0x00175C28 File Offset: 0x00173E28
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

		// Token: 0x170020AF RID: 8367
		// (get) Token: 0x06006384 RID: 25476 RVA: 0x00175C38 File Offset: 0x00173E38
		// (set) Token: 0x06006385 RID: 25477 RVA: 0x00175C63 File Offset: 0x00173E63
		[DefaultValue(true)]
		[ClientControlProperty]
		[Description("Gets or sets whether popup shadows will appear.")]
		[Category("Appearance")]
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

		// Token: 0x170020B0 RID: 8368
		// (get) Token: 0x06006386 RID: 25478 RVA: 0x00175C7C File Offset: 0x00173E7C
		// (set) Token: 0x06006387 RID: 25479 RVA: 0x00175CB6 File Offset: 0x00173EB6
		[Browsable(true)]
		[Description("Gets or sets a value indicating whether the picker will create an overlay element to ensure popups are over a flash element or Java applet.")]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
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

		// Token: 0x170020B1 RID: 8369
		// (get) Token: 0x06006388 RID: 25480 RVA: 0x00175CCE File Offset: 0x00173ECE
		// (set) Token: 0x06006389 RID: 25481 RVA: 0x00175CE8 File Offset: 0x00173EE8
		[DefaultValue(typeof(DateTime?), null)]
		[Description("Gets or sets the selected date.")]
		[Editor("System.ComponentModel.Design.DateTimeEditor", "System.Drawing.Design.UITypeEditor")]
		[Category("Date Selection")]
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
						throw new ArgumentOutOfRangeException("SelectedDate", string.Format("Value of '{0}' is not valid for 'SelectedDate'. 'SelectedDate' should be between 'MinDate' and 'MaxDate'.", dateTime));
					}
				}
				this.ViewState["SelectedDate"] = dateTime;
				this.DateInput.SelectedDate = this.SelectedDate;
			}
		}

		// Token: 0x170020B2 RID: 8370
		// (get) Token: 0x0600638A RID: 25482 RVA: 0x00175DA8 File Offset: 0x00173FA8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x0600638B RID: 25483 RVA: 0x00175DF1 File Offset: 0x00173FF1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string InvalidTextBoxValue
		{
			get
			{
				return this.DateInput.InvalidTextBoxValue;
			}
		}

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x0600638C RID: 25484 RVA: 0x00175DFE File Offset: 0x00173FFE
		// (set) Token: 0x0600638D RID: 25485 RVA: 0x00175E0C File Offset: 0x0017400C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(DateTimeConverter))]
		[Bindable(true, BindingDirection.TwoWay)]
		[Description("The currently selected date, boxed in object")]
		public object DbSelectedDate
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
					bool flag = DateTime.TryParseExact(text, this.DateInput.DateFormat, this.Culture, DateTimeStyles.None, out dateTime);
					if (!flag)
					{
						flag = DateTime.TryParseExact(text, this.DateInput.DisplayDateFormat, this.Culture, DateTimeStyles.None, out dateTime);
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
						flag = DateTime.TryParse(text, this.Culture, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParse(text, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
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

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x0600638E RID: 25486 RVA: 0x00175F54 File Offset: 0x00174154
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.SelectedDate == null;
			}
		}

		// Token: 0x170020B6 RID: 8374
		// (get) Token: 0x0600638F RID: 25487 RVA: 0x00175F72 File Offset: 0x00174172
		// (set) Token: 0x06006390 RID: 25488 RVA: 0x00175F82 File Offset: 0x00174182
		[Category("Behavior")]
		[Description("Enable or disable typing in the date input box.")]
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

		// Token: 0x170020B7 RID: 8375
		// (get) Token: 0x06006391 RID: 25489 RVA: 0x00175F93 File Offset: 0x00174193
		// (set) Token: 0x06006392 RID: 25490 RVA: 0x00175FBE File Offset: 0x001741BE
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("Gets or sets whether the popup control is displayed when the DateInput textbox is focused.")]
		[ClientPropertyName("_showPopupOnFocus")]
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

		// Token: 0x170020B8 RID: 8376
		// (get) Token: 0x06006393 RID: 25491 RVA: 0x00175FD6 File Offset: 0x001741D6
		// (set) Token: 0x06006394 RID: 25492 RVA: 0x0017600C File Offset: 0x0017420C
		[Description("Gets or sets the earliest valid date for selection.")]
		[ClientControlProperty]
		[DefaultValue(typeof(DateTime), "1/1/1980")]
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
				this.SelectedDate = this.RangeSelectedDateProperty(this.SelectedDate);
			}
		}

		// Token: 0x06006395 RID: 25493 RVA: 0x00176058 File Offset: 0x00174258
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

		// Token: 0x06006396 RID: 25494 RVA: 0x001760CD File Offset: 0x001742CD
		protected virtual DateTime TruncateTimeComponent(DateTime value)
		{
			return value.Subtract(value.TimeOfDay);
		}

		// Token: 0x170020B9 RID: 8377
		// (get) Token: 0x06006397 RID: 25495 RVA: 0x001760DD File Offset: 0x001742DD
		// (set) Token: 0x06006398 RID: 25496 RVA: 0x00176118 File Offset: 0x00174318
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
				this.SelectedDate = this.RangeSelectedDateProperty(this.SelectedDate);
			}
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x06006399 RID: 25497 RVA: 0x00176161 File Offset: 0x00174361
		// (set) Token: 0x0600639A RID: 25498 RVA: 0x00176181 File Offset: 0x00174381
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[Category("Appearance")]
		public virtual CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				if (value != this.ViewState["Culture"])
				{
					this._localization = null;
				}
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x0600639B RID: 25499 RVA: 0x001761B0 File Offset: 0x001743B0
		// (set) Token: 0x0600639C RID: 25500 RVA: 0x00176228 File Offset: 0x00174428
		[Description("The MonthYearView uses this date to focus itself whenever the date input component of the RadMonthYearPicker is empty.")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		[Category("Date Selection")]
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

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x0600639D RID: 25501 RVA: 0x00176253 File Offset: 0x00174453
		// (set) Token: 0x0600639E RID: 25502 RVA: 0x0017625B File Offset: 0x0017445B
		[Description("The width of the RadMonthYearPicker control in pixels.")]
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

		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x0600639F RID: 25503 RVA: 0x00176264 File Offset: 0x00174464
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		[NotifyParentProperty(true)]
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

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x060063A0 RID: 25504 RVA: 0x0017628A File Offset: 0x0017448A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
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

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x060063A1 RID: 25505 RVA: 0x001762B0 File Offset: 0x001744B0
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("A set of properties that get or set the names of the JavaScript functions that are invoked upon specific client-side events.")]
		[Category("ClientEvents")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MonthYearPickerClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new MonthYearPickerClientEvents(this.ViewState, this);
				}
				return this._clientEvents;
			}
		}

		// Token: 0x170020C0 RID: 8384
		// (get) Token: 0x060063A2 RID: 25506 RVA: 0x001762D2 File Offset: 0x001744D2
		// (set) Token: 0x060063A3 RID: 25507 RVA: 0x001762F3 File Offset: 0x001744F3
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

		// Token: 0x060063A4 RID: 25508 RVA: 0x0017630C File Offset: 0x0017450C
		public virtual void ConfigureDateInput()
		{
			this.DateInput.SelectedDate = this.SelectedDate;
			this.DateInput.EnableAriaSupport = this.EnableAriaSupport;
			if (this.IsOnlyInputRendered())
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

		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x060063A5 RID: 25509 RVA: 0x0017643C File Offset: 0x0017463C
		// (set) Token: 0x060063A6 RID: 25510 RVA: 0x0017646B File Offset: 0x0017466B
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Specifies default path for the RadMonthYearPicker images when EnableEmbeddedSkins is set to false.")]
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
				this.ViewState["ImagesPath"] = value;
			}
		}

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x060063A7 RID: 25511 RVA: 0x0017647E File Offset: 0x0017467E
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x060063A8 RID: 25512 RVA: 0x00176486 File Offset: 0x00174686
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060063A9 RID: 25513 RVA: 0x0017648C File Offset: 0x0017468C
		protected internal string GetImage(string fileName)
		{
			if (this.IsWebResourceUrl(fileName))
			{
				return fileName;
			}
			if (!VirtualPathUtility.IsAbsolute(fileName) && VirtualPathUtility.IsAppRelative(fileName))
			{
				if (this.IsDesignMode)
				{
					return base.ResolveClientUrl(VirtualPathUtility.ToAppRelative(fileName));
				}
				return VirtualPathUtility.ToAbsolute(fileName);
			}
			else
			{
				if (!string.IsNullOrEmpty(this.ImagesPath.Trim()) && fileName.IndexOf("/") == -1 && fileName.IndexOf("\\") == -1)
				{
					return base.ResolveUrl(Path.Combine(this.ImagesPath.Trim(), fileName));
				}
				if (!string.IsNullOrEmpty(this.ImagesPath.Trim()))
				{
					return base.ResolveUrl(fileName);
				}
				return string.Empty;
			}
		}

		// Token: 0x060063AA RID: 25514 RVA: 0x00176535 File Offset: 0x00174735
		private bool IsWebResourceUrl(string path)
		{
			return path != null && path.IndexOf("WebResource.axd") != -1;
		}

		// Token: 0x060063AB RID: 25515 RVA: 0x0017654D File Offset: 0x0017474D
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			this.SetDefaultSize();
			this.ConfigureChildren();
			this.ConfigureMonthYearPicker();
			this.DateInput.Skin = base.RuntimeSkin;
			if (!this.EmptySkin)
			{
				this.SetDefaultItemStyles();
			}
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x00176588 File Offset: 0x00174788
		private void SetDefaultItemStyles()
		{
			this.MonthYearTableView.CssClass = this.FormatCssClass("RadCalendarMonthView", this.FastNavigationStyle.CssClass);
			this.FastNavigationStyle.CssClass = this.FormatCssClass("RadCalendarMonthView", this.FastNavigationStyle.CssClass);
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x001765D8 File Offset: 0x001747D8
		internal string FormatCssClass(string prefix, string userDefined)
		{
			string text;
			if (prefix == "RadCalendar" || prefix == "RadCalendarMonthView" || prefix == "RadCalendarMultiView")
			{
				text = (this.EmptySkin ? prefix : string.Format("{0} {0}_{1}", prefix, base.RuntimeSkin));
			}
			else
			{
				text = prefix;
			}
			if (!string.IsNullOrEmpty(prefix))
			{
				userDefined = Regex.Replace(userDefined, prefix + "_\\S+\\s?", "").Trim();
			}
			if (userDefined.IndexOf(text) >= 0)
			{
				return userDefined;
			}
			if (string.IsNullOrEmpty(userDefined) || userDefined == prefix)
			{
				return text;
			}
			return string.Format("{0} {1}", text, userDefined);
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x00176683 File Offset: 0x00174883
		internal virtual void ConfigureChildren()
		{
			this.ConfigureDateInput();
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x0017668B File Offset: 0x0017488B
		internal void ConfigureMonthYearPicker()
		{
			if (this.MinDate > this.FocusedDate)
			{
				this.FocusedDate = this.MinDate;
			}
			this.RangeMaxDate = this.MaxDate;
			this.RangeMinDate = this.MinDate;
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x001766C4 File Offset: 0x001748C4
		private void DateInput_TextChanged(object sender, EventArgs e)
		{
			SelectedDateChangedEventArgs eventArgs = new SelectedDateChangedEventArgs(this._oldSelectedValue, this.SelectedDate);
			this.OnSelectedDateChanged(eventArgs);
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x001766EA File Offset: 0x001748EA
		public override void Focus()
		{
			this.EnsureChildControls();
			this.DateInput.Focus();
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x001766FD File Offset: 0x001748FD
		internal void SelectedDateLoaded(object newDate)
		{
			this._oldSelectedValue = this.SelectedDate;
			this.DbSelectedDate = newDate;
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x00176712 File Offset: 0x00174912
		public virtual void Clear()
		{
			this.DateInput.Clear();
			this.ViewState["SelectedDate"] = null;
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x00176730 File Offset: 0x00174930
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderMode(descriptor);
			this.ClientEvents.DescribeEvents(descriptor);
			this.DescribeProperties(descriptor);
		}

		// Token: 0x170020C4 RID: 8388
		// (get) Token: 0x060063B5 RID: 25525 RVA: 0x00176753 File Offset: 0x00174953
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The style applied to the month/year fast navigation.")]
		[RefreshProperties(RefreshProperties.All)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Appearance")]
		public TableItemStyle FastNavigationStyle
		{
			get
			{
				if (this.fastNavigationStyle == null)
				{
					this.fastNavigationStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.fastNavigationStyle).TrackViewState();
					}
				}
				return this.fastNavigationStyle;
			}
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x00176784 File Offset: 0x00174984
		private string GetStyles()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(Utility.GetStyle("FastNavigationStyle", this.FastNavigationStyle));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x001767CC File Offset: 0x001749CC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected virtual void DescribeProperties(IScriptDescriptor descriptor)
		{
			if (!this.EmptySkin)
			{
				descriptor.AddProperty("skin", base.RuntimeSkin);
			}
			descriptor.AddScriptProperty("stylesHash", this.GetStyles());
			if (this.DateInput.Visible)
			{
				descriptor.AddComponentProperty("dateInput", this.DateInput.ClientID);
			}
			descriptor.AddProperty("_culture", this.Culture.ToString());
			base.DescribeProperty<bool>(descriptor, "_enableShadows", this.EnableShadows, true);
			base.DescribeProperty<string>(descriptor, "focusedDate", this.FocusedDate.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture), DateTime.Parse("1/1/1980", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture));
			base.DescribeProperty<string>(descriptor, "maxDate", this.MaxDate.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture), DateTime.Parse("12/31/2099", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture));
			base.DescribeProperty<string>(descriptor, "minDate", this.MinDate.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture), DateTime.Parse("1/1/1980", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture));
			base.DescribeProperty<DatePickerPopupDirection>(descriptor, "_popupDirection", this.PopupDirection, DatePickerPopupDirection.BottomRight);
			base.DescribeProperty<bool>(descriptor, "_showPopupOnFocus", this.ShowPopupOnFocus, false);
			base.DescribeProperty<int>(descriptor, "_zIndex", this.ZIndex, 5000);
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", base.IsEnabled);
			}
			if (this.Overlay)
			{
				descriptor.AddProperty("_overlay", this.Overlay);
			}
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			descriptor.AddScriptProperty("_FormatInfoArray", javaScriptSerializer.Serialize(this.GetClientDateFormatInfo()));
			bool flag = true;
			descriptor.AddScriptProperty("monthYearNavigationSettings", Utility.ConvertToClientArray1D(new string[]
			{
				this.MonthYearNavigationSettings.TodayButtonCaption,
				this.MonthYearNavigationSettings.OkButtonCaption,
				this.MonthYearNavigationSettings.CancelButtonCaption,
				string.IsNullOrEmpty(this.MonthYearNavigationSettings.DateIsOutOfRangeMessage) ? " " : this.MonthYearNavigationSettings.DateIsOutOfRangeMessage,
				flag.ToString(),
				this.MonthYearNavigationSettings.EnableScreenBoundaryDetection.ToString(),
				this.MonthYearNavigationSettings.ShowAnimation.Duration.ToString(),
				((int)this.MonthYearNavigationSettings.ShowAnimation.Type).ToString(),
				this.MonthYearNavigationSettings.HideAnimation.Duration.ToString(),
				((int)this.MonthYearNavigationSettings.HideAnimation.Type).ToString(),
				this.MonthYearNavigationSettings.DisableOutOfRangeMonths.ToString()
			}));
			descriptor.AddProperty("_popupControlID", this.DatePopupButton.ClientID);
			descriptor.AddProperty("_enableKeyboardNavigation", this.EnableKeyboardNavigation);
			descriptor.AddScriptProperty("_PopupButtonSettings", string.Format("{{ ResolvedImageUrl : \"{0}\", ResolvedHoverImageUrl : \"{1}\"}}", this.DatePopupButton.ResolvedImageUrl, this.DatePopupButton.ResolvedHoverImageUrl));
			if (this != null)
			{
				string script = string.Format("{{ShowAnimationDuration:{0},ShowAnimationType:{1},HideAnimationDuration:{2},HideAnimationType:{3}}}", new object[]
				{
					this.ShowAnimation.Duration,
					(int)this.ShowAnimation.Type,
					this.AutoPostBack ? 0 : this.HideAnimation.Duration,
					(int)this.HideAnimation.Type
				});
				descriptor.AddScriptProperty("_animationSettings", script);
			}
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x00176BD4 File Offset: 0x00174DD4
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.ClientID + "_AD"];
			if (text != null)
			{
				DateTimeCollection dateTimeCollection = new DateTimeCollection();
				Utility.ConvertToServerDateTimeCollection(dateTimeCollection, text);
				this.RangeMinDate = dateTimeCollection[0].Date;
				this.RangeMaxDate = dateTimeCollection[1].Date;
				this.FocusedDate = dateTimeCollection[2].Date;
			}
			return false;
		}

		// Token: 0x170020C5 RID: 8389
		// (get) Token: 0x060063B9 RID: 25529 RVA: 0x00176C50 File Offset: 0x00174E50
		// (set) Token: 0x060063BA RID: 25530 RVA: 0x00176C8C File Offset: 0x00174E8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NotifyParentProperty(true)]
		[DatePickerBrowsable(false)]
		[Category("Dates Management")]
		[Browsable(false)]
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		[Description("Gets or sets the minimal date valid for selection by Telerik RadMonthYearPicker. Must be interpreted as the Lower bound of the valid dates range available for selection. Telerik RadMonthYearPicker will not allow navigation or selection prior to this date.")]
		private DateTime RangeMinDate
		{
			get
			{
				DateTime result = new DateTime(1980, 1, 1);
				object obj = this.Properties["MinD"];
				if (!(obj is DateTime))
				{
					return result;
				}
				return (DateTime)obj;
			}
			set
			{
				DateTime dateTime = this.TruncateTimeComponent(value);
				this.Properties["MinD"] = dateTime;
			}
		}

		// Token: 0x170020C6 RID: 8390
		// (get) Token: 0x060063BB RID: 25531 RVA: 0x00176CB8 File Offset: 0x00174EB8
		// (set) Token: 0x060063BC RID: 25532 RVA: 0x00176CF8 File Offset: 0x00174EF8
		[Description("Gets or sets the maximal date valid for selection by Telerik RadMonthYearPicker. Must be interpreted as the Higher bound of the valid dates range available for selection. Telerik RadMonthYearPicker will not allow navigation or selection past this date.")]
		[DefaultValue(typeof(DateTime), "12/30/2099")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NotifyParentProperty(true)]
		[DatePickerBrowsable(false)]
		[Browsable(false)]
		[Category("Dates Management")]
		private DateTime RangeMaxDate
		{
			get
			{
				DateTime result = new DateTime(2099, 12, 30);
				object obj = this.Properties["MaxD"];
				if (!(obj is DateTime))
				{
					return result;
				}
				return (DateTime)obj;
			}
			set
			{
				DateTime dateTime = this.TruncateTimeComponent(value);
				this.Properties["MaxD"] = dateTime;
			}
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x00176D24 File Offset: 0x00174F24
		private void RenderAuxiliaryDates(HtmlTextWriter writer)
		{
			object[] data = new object[]
			{
				this.RangeMinDate,
				this.RangeMaxDate,
				this.FocusedDate
			};
			this.WriteHiddenFieldRegistration(writer, "_AD", data);
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x00176D71 File Offset: 0x00174F71
		public void WriteHiddenFieldRegistration(HtmlTextWriter writer, string fieldPostfix, object data)
		{
			writer.Write(this.GetHiddenRegistration(fieldPostfix, data));
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x00176D84 File Offset: 0x00174F84
		public string GetHiddenRegistration(string fieldPostfix, object data)
		{
			string clientID = this.ClientID;
			return string.Format(string.Concat(new string[]
			{
				"<input type=\"hidden\" name=\"{0}",
				fieldPostfix,
				"\" id=\"{0}",
				fieldPostfix,
				"\" value=\"{1}\" />"
			}), clientID, Utility.ConvertToClientArray1D(data));
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x00176DDC File Offset: 0x00174FDC
		internal ArrayList GetClientDateFormatInfo()
		{
			return new ArrayList
			{
				this.Culture.DateTimeFormat.DayNames,
				this.Culture.DateTimeFormat.AbbreviatedDayNames,
				this.Culture.DateTimeFormat.MonthNames,
				this.Culture.DateTimeFormat.AbbreviatedMonthNames,
				this.Culture.DateTimeFormat.FullDateTimePattern,
				this.Culture.DateTimeFormat.LongDatePattern,
				this.Culture.DateTimeFormat.LongTimePattern,
				this.Culture.DateTimeFormat.MonthDayPattern,
				this.Culture.DateTimeFormat.RFC1123Pattern,
				this.Culture.DateTimeFormat.ShortDatePattern,
				this.Culture.DateTimeFormat.ShortTimePattern,
				this.Culture.DateTimeFormat.SortableDateTimePattern,
				this.Culture.DateTimeFormat.UniversalSortableDateTimePattern,
				this.Culture.DateTimeFormat.YearMonthPattern,
				this.Culture.DateTimeFormat.AMDesignator,
				this.Culture.DateTimeFormat.PMDesignator,
				this.Culture.DateTimeFormat.DateSeparator,
				this.Culture.DateTimeFormat.TimeSeparator,
				this.Culture.DateTimeFormat.FirstDayOfWeek
			};
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x00176FAC File Offset: 0x001751AC
		protected override void RegisterCssReferences()
		{
			RadStyleSheetManager current = RadStyleSheetManager.GetCurrent(this.Page);
			if (current == null)
			{
				SkinRegistrar.RegisterCssReferences(this);
				return;
			}
			current.RegisterSkinnableControl(this);
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x00176FD6 File Offset: 0x001751D6
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			((IPostBackEventHandler)this.DateInput).RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x00176FE4 File Offset: 0x001751E4
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			base.LoadPostData(postDataKey, postCollection);
			return false;
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x00176FF0 File Offset: 0x001751F0
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x00176FF4 File Offset: 0x001751F4
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.MinDate = DateTime.Parse((string)clientState["minDateStr"], CultureInfo.InvariantCulture);
			this.MaxDate = DateTime.Parse((string)clientState["maxDateStr"], CultureInfo.InvariantCulture);
		}

		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x060063C6 RID: 25542 RVA: 0x00177048 File Offset: 0x00175248
		public string ControlId
		{
			get
			{
				return this.DateInput.ClientID;
			}
		}

		// Token: 0x04001833 RID: 6195
		internal const string RangeMaxDateID = "MaxD";

		// Token: 0x04001834 RID: 6196
		internal const string RangeMinDateID = "MinD";

		// Token: 0x04001835 RID: 6197
		internal const string FocusedDateID = "FocD";

		// Token: 0x04001836 RID: 6198
		private RadDateInput _input;

		// Token: 0x04001837 RID: 6199
		private MonthYearView _monthYearTableView;

		// Token: 0x04001838 RID: 6200
		private MonthYearPopupButton _datePopupButton;

		// Token: 0x04001839 RID: 6201
		private MonthYearPickerClientEvents _clientEvents;

		// Token: 0x0400183A RID: 6202
		private DateTime? _oldSelectedValue;

		// Token: 0x0400183B RID: 6203
		private CalendarAnimationSettings _showAnimation;

		// Token: 0x0400183C RID: 6204
		private CalendarAnimationSettings _hideAnimation;

		// Token: 0x0400183D RID: 6205
		private MonthYearPickerStrings _localization;

		// Token: 0x0400183E RID: 6206
		private TableItemStyle monthCellsStyle;

		// Token: 0x0400183F RID: 6207
		private TableItemStyle yearCellsStyle;

		// Token: 0x04001840 RID: 6208
		private static readonly object EventChildrenCreated = new object();

		// Token: 0x04001841 RID: 6209
		private static readonly object EventSelectedDateChanged = new object();

		// Token: 0x04001842 RID: 6210
		private static readonly object EventViewCellCreated = new object();

		// Token: 0x04001843 RID: 6211
		protected Unit defaultWidth = Unit.Empty;

		// Token: 0x04001844 RID: 6212
		private bool? _isLightweightRendering = null;

		// Token: 0x04001845 RID: 6213
		private MonthYearNavigationSettings _monthYearNavigationSettings;

		// Token: 0x04001846 RID: 6214
		private TableItemStyle fastNavigationStyle;
	}
}
