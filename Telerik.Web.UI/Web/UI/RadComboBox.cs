using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.ComboBox;

namespace Telerik.Web.UI
{
	// Token: 0x02000A44 RID: 2628
	[NativeRendering]
	[TelerikToolboxCategory("Data Editing")]
	[XmlRoot("Items")]
	[EmbeddedSkin("ComboBox", "Default", typeof(RadComboBox))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadComboBox))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(OData))]
	[RequiredScript(typeof(DropDown))]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.RadComboBox", "Telerik.Web.UI.ComboBox.RadComboBoxScripts.js")]
	[RequiredScript(typeof(TouchScrollExtender))]
	[RequiredScript(typeof(MaterialRipple))]
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Items")]
	[ToolboxData("<{0}:RadComboBox Runat=\"server\"></{0}:RadComboBox>")]
	[ValidationProperty("Text")]
	[ControlValueProperty("SelectedValue")]
	[EmbeddedSkin("ComboBox", typeof(RadComboBox))]
	[ToolboxBitmap(typeof(RadComboBox), "Telerik.Web.UI.ComboBox.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[LightweightRendering]
	[Designer("Telerik.Web.Design.RadComboBoxDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadComboBox : ControlItemContainer, IPostBackEventHandler, ICallbackEventHandler, IEditableTextControl, ITextControl, ILocalizableControl, IFlatBoundContainer, ILabelableControl
	{
		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x0600645F RID: 25695 RVA: 0x00178C0E File Offset: 0x00176E0E
		// (set) Token: 0x06006460 RID: 25696 RVA: 0x00178C2E File Offset: 0x00176E2E
		internal string TableCaption
		{
			get
			{
				return (string)(this.ViewState["TableCaption"] ?? string.Empty);
			}
			set
			{
				this.ViewState["TableCaption"] = value;
			}
		}

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x06006461 RID: 25697 RVA: 0x00178C41 File Offset: 0x00176E41
		// (set) Token: 0x06006462 RID: 25698 RVA: 0x00178C61 File Offset: 0x00176E61
		internal string TableSummary
		{
			get
			{
				return (string)(this.ViewState["TableSummary"] ?? "combobox");
			}
			set
			{
				this.ViewState["TableSummary"] = value;
			}
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x06006463 RID: 25699 RVA: 0x00178C74 File Offset: 0x00176E74
		// (set) Token: 0x06006464 RID: 25700 RVA: 0x00178C94 File Offset: 0x00176E94
		internal string InputTitle
		{
			get
			{
				return (string)(this.ViewState["InputTitle"] ?? string.Empty);
			}
			set
			{
				this.ViewState["InputTitle"] = value;
			}
		}

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x06006465 RID: 25701 RVA: 0x00178CA7 File Offset: 0x00176EA7
		// (set) Token: 0x06006466 RID: 25702 RVA: 0x00178CC8 File Offset: 0x00176EC8
		internal bool EnableTableHeaders
		{
			get
			{
				return (bool)(this.ViewState["TableHeaders"] ?? false);
			}
			set
			{
				this.ViewState["TableHeaders"] = value;
			}
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x00178CE0 File Offset: 0x00176EE0
		protected virtual bool RequiresControlState()
		{
			return true;
		}

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x06006468 RID: 25704 RVA: 0x00178CE3 File Offset: 0x00176EE3
		public string ControlId
		{
			get
			{
				return this.ClientID + "_Input";
			}
		}

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x06006469 RID: 25705 RVA: 0x00178CF5 File Offset: 0x00176EF5
		internal bool IsControlEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x0600646A RID: 25706 RVA: 0x00178CFD File Offset: 0x00176EFD
		// (set) Token: 0x0600646B RID: 25707 RVA: 0x00178D05 File Offset: 0x00176F05
		internal string FilterText
		{
			get
			{
				return this._filterText;
			}
			set
			{
				this._filterText = value;
			}
		}

		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x0600646C RID: 25708 RVA: 0x00178D0E File Offset: 0x00176F0E
		// (set) Token: 0x0600646D RID: 25709 RVA: 0x00178D16 File Offset: 0x00176F16
		internal int NumberOfItems
		{
			get
			{
				return this._numberOfItems;
			}
			set
			{
				this._numberOfItems = value;
			}
		}

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x0600646E RID: 25710 RVA: 0x00178D1F File Offset: 0x00176F1F
		// (set) Token: 0x0600646F RID: 25711 RVA: 0x00178D27 File Offset: 0x00176F27
		internal bool EndOfItems
		{
			get
			{
				return this._endOfItems;
			}
			set
			{
				this._endOfItems = true;
			}
		}

		// Token: 0x17002104 RID: 8452
		// (get) Token: 0x06006470 RID: 25712 RVA: 0x00178D30 File Offset: 0x00176F30
		// (set) Token: 0x06006471 RID: 25713 RVA: 0x00178D38 File Offset: 0x00176F38
		internal string ShowMoreResultsMessage
		{
			get
			{
				return this._showMoreResultsMessage;
			}
			set
			{
				this._showMoreResultsMessage = value;
			}
		}

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x06006472 RID: 25714 RVA: 0x00178D41 File Offset: 0x00176F41
		internal override bool SupportsOData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x06006473 RID: 25715 RVA: 0x00178D44 File Offset: 0x00176F44
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x06006474 RID: 25716 RVA: 0x00178D47 File Offset: 0x00176F47
		internal bool IsNativeMode
		{
			get
			{
				return this.RenderMode == RenderMode.Native;
			}
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x00178D52 File Offset: 0x00176F52
		protected override void OnInit(EventArgs e)
		{
			if (this.RequiresControlState())
			{
				this.Page.RegisterRequiresControlState(this);
			}
			base.OnInit(e);
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x00178D6F File Offset: 0x00176F6F
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.EnableAutomaticLoadOnDemand)
			{
				base.RequiresDataBinding = false;
			}
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x00178D88 File Offset: 0x00176F88
		protected override void LoadControlState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				this.Text = (string)array[0];
				if (!this.Page.IsCallback && this.SelectedValue != (string)array[1])
				{
					this.SelectedValue = (string)array[1];
					this.cachedSelectedValue = null;
				}
			}
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x00178DE4 File Offset: 0x00176FE4
		protected override object SaveControlState()
		{
			ArrayList arrayList = new ArrayList
			{
				this.Text,
				this.SelectedValue
			};
			return arrayList.ToArray();
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x00178E1C File Offset: 0x0017701C
		public RadComboBox()
		{
			this._webServiceSettings = new NavigationControlWebServiceSettings(this.ViewState);
			this._expandAnimation = new ComboBoxAnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new ComboBoxAnimationSettings("Collapse", this.ViewState);
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x00178EAA File Offset: 0x001770AA
		protected override void RaiseItemCreated(ControlItem item)
		{
			this.OnItemCreated(new RadComboBoxItemEventArgs((RadComboBoxItem)item));
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x00178EBD File Offset: 0x001770BD
		protected virtual void OnItemCreated(RadComboBoxItemEventArgs e)
		{
			this.RaiseEvent(RadComboBox.ItemCreatedEvent, e);
		}

		// Token: 0x0600647C RID: 25724 RVA: 0x00178ECB File Offset: 0x001770CB
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnItemDataBound(new RadComboBoxItemEventArgs((RadComboBoxItem)item));
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x00178EDE File Offset: 0x001770DE
		protected virtual void OnItemDataBound(RadComboBoxItemEventArgs e)
		{
			this.RaiseEvent(RadComboBox.ItemDataBoundEvent, e);
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x00178EEC File Offset: 0x001770EC
		protected virtual void OnItemChecked(RadComboBoxItemEventArgs e)
		{
			this.RaiseEvent(RadComboBox.ItemCheckedEvent, e);
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x00178EFA File Offset: 0x001770FA
		protected override void RaiseTemplateNeeded(ControlItem item)
		{
			this.OnTemplateNeeded(new RadComboBoxItemEventArgs((RadComboBoxItem)item));
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x00178F0D File Offset: 0x0017710D
		protected virtual void OnTemplateNeeded(RadComboBoxItemEventArgs e)
		{
			this.RaiseEvent(RadComboBox.TemplateNeededEvent, e);
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x00178F1C File Offset: 0x0017711C
		private void RaiseEvent(object eventKey, RadComboBoxItemEventArgs e)
		{
			RadComboBoxItemEventHandler radComboBoxItemEventHandler = (RadComboBoxItemEventHandler)base.Events[eventKey];
			if (radComboBoxItemEventHandler != null)
			{
				radComboBoxItemEventHandler(this, e);
			}
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x00178F46 File Offset: 0x00177146
		protected virtual void RaiseSelectedIndexChandedEvent()
		{
			this.OnSelectedIndexChanged();
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x00178F50 File Offset: 0x00177150
		protected virtual void OnSelectedIndexChanged()
		{
			RadComboBoxSelectedIndexChangedEventHandler radComboBoxSelectedIndexChangedEventHandler = (RadComboBoxSelectedIndexChangedEventHandler)base.Events[RadComboBox.SelectedIndexChangedEvent];
			if (radComboBoxSelectedIndexChangedEventHandler != null)
			{
				radComboBoxSelectedIndexChangedEventHandler(this, new RadComboBoxSelectedIndexChangedEventArgs(this.Text, this._oldText, this.SelectedValue, this._oldValue));
			}
			this.OnTextChanged(new EventArgs());
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x00178FA5 File Offset: 0x001771A5
		protected virtual void RaiseTextChandedEvent()
		{
			this.OnTextChanged(new EventArgs());
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x00178FB4 File Offset: 0x001771B4
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadComboBox.TextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x00178FE4 File Offset: 0x001771E4
		protected virtual void OnItemsRequested(RadComboBoxItemsRequestedEventArgs args)
		{
			RadComboBoxItemsRequestedEventHandler radComboBoxItemsRequestedEventHandler = (RadComboBoxItemsRequestedEventHandler)base.Events[RadComboBox.ItemsRequestedEvent];
			if (radComboBoxItemsRequestedEventHandler != null)
			{
				radComboBoxItemsRequestedEventHandler(this, args);
			}
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x00179014 File Offset: 0x00177214
		protected virtual void OnCheckAllCheck(RadComboBoxCheckAllCheckEventArgs e)
		{
			RadComboBoxCheckAllCheckEventHandler radComboBoxCheckAllCheckEventHandler = (RadComboBoxCheckAllCheckEventHandler)base.Events[RadComboBox.CheckAllCheckEvent];
			if (radComboBoxCheckAllCheckEventHandler != null)
			{
				radComboBoxCheckAllCheckEventHandler(this, e);
			}
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x00179042 File Offset: 0x00177242
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadComboBoxItemCollection(this);
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x0017904A File Offset: 0x0017724A
		protected internal override ControlItem CreateItem()
		{
			return new RadComboBoxItem();
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x00179051 File Offset: 0x00177251
		protected override void CreateChildControls()
		{
			this.CreateHeader();
			if (this.HeaderTemplate != null)
			{
				RadComboBox.ApplyTemplate(this.Header, this.HeaderTemplate);
			}
			this.CreateFooter();
			if (this.FooterTemplate != null)
			{
				RadComboBox.ApplyTemplate(this.Footer, this.FooterTemplate);
			}
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x00179094 File Offset: 0x00177294
		private void CreateFooter()
		{
			if (this._footer == null)
			{
				this._footer = new RadComboBoxHeaderFooterControl();
				this.Controls.Add(this.Footer);
				this.Footer.ID = "Footer";
				this.Footer.CssClass = "rcbFooter";
			}
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x001790E8 File Offset: 0x001772E8
		private void CreateHeader()
		{
			if (this._header == null)
			{
				this._header = new RadComboBoxHeaderFooterControl();
				this.Controls.Add(this.Header);
				this.Header.ID = "Header";
				this.Header.CssClass = "rcbHeader";
			}
		}

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x0600648D RID: 25741 RVA: 0x00179139 File Offset: 0x00177339
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x00179147 File Offset: 0x00177347
		protected internal override IRenderer CreateControlRenderer()
		{
			return RendererFactory.CreateRenderer(this);
		}

		// Token: 0x0600648F RID: 25743 RVA: 0x00179150 File Offset: 0x00177350
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Unit height = this.Height;
			this.Height = Unit.Empty;
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			string toolTip = this.ToolTip;
			this.ToolTip = string.Empty;
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			bool enabled = this.Enabled;
			this.Enabled = true;
			Unit width = this.Width;
			if (this.Label.Length > 0 && this.Width.Type != UnitType.Percentage && this.RenderMode == RenderMode.Classic)
			{
				this.Width = Unit.Empty;
			}
			else if (this.Width == Unit.Empty && this.RenderMode != RenderMode.Lightweight)
			{
				this.Width = Unit.Pixel(160);
			}
			Color foreColor = this.ForeColor;
			this.ForeColor = Color.Empty;
			Color backColor = this.BackColor;
			this.BackColor = Color.Empty;
			base.AddAttributesToRender(writer);
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Height = height;
			this.AccessKey = accessKey;
			this.TabIndex = tabIndex;
			this.ToolTip = toolTip;
			this.Width = width;
			this.Enabled = enabled;
			this.RenderBrowserSpecificStyles(writer);
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x00179289 File Offset: 0x00177489
		public override void RenderControl(HtmlTextWriter writer)
		{
			this.ApplyDefaultItem();
			base.RenderControl(writer);
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x06006491 RID: 25745 RVA: 0x00179298 File Offset: 0x00177498
		protected internal HttpBrowserCapabilities Browser
		{
			get
			{
				return this.Context.Request.Browser;
			}
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x06006492 RID: 25746 RVA: 0x001792AA File Offset: 0x001774AA
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x001792B7 File Offset: 0x001774B7
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			if (base.DesignMode)
			{
				this.RenderDesignTimeHtml(writer);
				return;
			}
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06006494 RID: 25748 RVA: 0x001792DC File Offset: 0x001774DC
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			writer.Write("<style type=\"text/css\">");
			writer.Write("html .RadComboBox {display: inline-block;}");
			writer.Write("</style>");
			writer.Write(string.Format("<table cellpadding='0' cellspacing='0' border='0' style='width: {0};'><tr>", (this.Width == Unit.Empty) ? "160px" : this.Width.ToString()));
			if (!string.IsNullOrEmpty(this.Label))
			{
				writer.Write(string.Format("<td width='1%'><label class='rcbLabel'>{0}</label></td>", this.Label.Replace(" ", "&nbsp;")));
			}
			writer.Write("<td>\r\n\t\t\t\t<table style='table-layout: fixed'>\r\n\t\t\t\t\t<tbody>\r\n\t\t\t\t\t\t<tr class='rcbReadOnly'>\r\n\t\t\t\t\t\t\t<td class='rcbInputCell rcbInputCellLeft'><input class='rcbInput' /></td>\r\n\t\t\t\t\t\t\t<td class='rcbArrowCell rcbArrowCellRight'><a>select</a></td>\r\n\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t</tbody>\r\n\t\t\t\t</table>\r\n\t\t\t</td>");
			writer.Write("</tr></table>");
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x0017939B File Offset: 0x0017759B
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			if (this.AccessibilityMode)
			{
				this.RenderNoScript(writer);
			}
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x001793B4 File Offset: 0x001775B4
		private void RenderNoScript(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Noscript);
			writer.AddStyleAttribute("display", "inline");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			DropDownList dropDownList = new DropDownList
			{
				ID = "ComboBox_" + this.ID
			};
			foreach (object obj in this.Items)
			{
				RadComboBoxItem radComboBoxItem = (RadComboBoxItem)obj;
				ListItem item = new ListItem(radComboBoxItem.Text, radComboBoxItem.Value)
				{
					Enabled = radComboBoxItem.Enabled,
					Selected = radComboBoxItem.Selected
				};
				dropDownList.Items.Add(item);
			}
			this.CopyProperties(dropDownList);
			dropDownList.RenderControl(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x001794A0 File Offset: 0x001776A0
		private void CopyProperties(DropDownList dropDown)
		{
			Type type = base.GetType();
			Type type2 = dropDown.GetType();
			PropertyInfo[] properties = type.GetProperties();
			PropertyInfo[] properties2 = type2.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				foreach (PropertyInfo propertyInfo2 in properties2)
				{
					if (propertyInfo2.CanWrite && propertyInfo.Name == propertyInfo2.Name && propertyInfo2.Name != "ID" && propertyInfo2.Name != "Height" && propertyInfo2.Name != "Items")
					{
						propertyInfo2.SetValue(dropDown, propertyInfo.GetValue(this, null), null);
					}
				}
			}
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x00179574 File Offset: 0x00177774
		internal static void ApplyTemplate(WebControl control, ITemplate template)
		{
			RadComboBoxHeaderFooterControl radComboBoxHeaderFooterControl = (RadComboBoxHeaderFooterControl)control;
			if (radComboBoxHeaderFooterControl.TemplateInstantiated)
			{
				return;
			}
			int i = control.Controls.Count;
			if (template != null)
			{
				template.InstantiateIn(control);
			}
			while (i > 0)
			{
				control.Controls.Add(control.Controls[0]);
				i--;
			}
			radComboBoxHeaderFooterControl.TemplateInstantiated = true;
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x001795CF File Offset: 0x001777CF
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(this.TagKey);
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x001795E4 File Offset: 0x001777E4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			bool flag = this.SelectedIndex > -1;
			if ((string.IsNullOrEmpty(this.EmptyMessage) || flag) && (this._defaultItem == null || flag))
			{
				this.ExtractTextAndValue();
			}
			if (this.AccessibilityMode && base.ScriptManager.LoadScriptsBeforeUI)
			{
				string script = string.Format("Telerik.Web.UI.RadComboBox._preInitialize(\"{0}\");", this.ClientID);
				ScriptManager.RegisterStartupScript(this.Page, typeof(RadComboBox), this.ClientID, script, true);
			}
			if (this.CheckBoxes && !this.IsNativeMode)
			{
				if (this.Items.Count == 0 || this.CheckedItems.Count == 0)
				{
					this.Text = "";
					return;
				}
				this.Text = this.GetCheckedItemsTexts();
			}
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x001796A8 File Offset: 0x001778A8
		protected string GetCheckedItemsTexts()
		{
			string text = string.Empty;
			if (this.CheckedItems.Count > 0 && this.CheckedItems.Count == this.Items.Count && this.CheckedItemsTexts == RadComboBoxCheckedItemsTexts.FitInInput)
			{
				text = this.Localization.AllItemsCheckedString;
			}
			else
			{
				foreach (RadComboBoxItem radComboBoxItem in this.CheckedItems)
				{
					text = text + radComboBoxItem.Text + ", ";
				}
			}
			if (text.Length > 0)
			{
				return text.Substring(0, text.Length - 2);
			}
			return text;
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x0017975C File Offset: 0x0017795C
		internal void ExtractTextAndValue()
		{
			int selectedIndex = this.SelectedIndex;
			if (!string.IsNullOrEmpty(this.Text))
			{
				if (this.SelectedIndex >= 0 && !this.LoadOnDemandIsEnabled)
				{
					if (this.Items[this.SelectedIndex].Text == this.Text)
					{
						this.SelectedValue = this.Items[this.SelectedIndex].Value;
						this.Items[selectedIndex].Selected = true;
					}
					else if (string.IsNullOrEmpty(this.AutoCompleteSeparator))
					{
						this.Text = this.Items[this.SelectedIndex].Text;
						this.SelectedValue = this.Items[this.SelectedIndex].Value;
						this.Items[selectedIndex].Selected = true;
					}
				}
				if (this.Items.Count == 0 && this.ReadOnly)
				{
					this.Text = "";
					return;
				}
			}
			else
			{
				if (this.SelectedIndex >= 0)
				{
					this.Text = this.Items[this.SelectedIndex].Text;
					this.SelectedValue = this.Items[this.SelectedIndex].Value;
					this.Items[selectedIndex].Selected = true;
					return;
				}
				if (this.Items.Count > 0 && !this.AllowCustomText)
				{
					RadComboBoxItem radComboBoxItem = this.FindNextAvailableItem(-1);
					if (radComboBoxItem != null)
					{
						this.Text = radComboBoxItem.Text;
						this.SelectedValue = radComboBoxItem.Value;
					}
				}
			}
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x001798F8 File Offset: 0x00177AF8
		private RadComboBoxItem FindNextAvailableItem(int index)
		{
			int i = index;
			bool flag = false;
			while (i < this.Items.Count - 1)
			{
				i++;
				if (this.Items[i].Enabled && !this.Items[i].IsSeparator && this.Items[i].VisibleInternal)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				return this.Items[i];
			}
			if (index == -1)
			{
				return null;
			}
			return this.Items[index];
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x00179980 File Offset: 0x00177B80
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new ComboBoxItemConverter(),
				new AttributeCollectionConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			javaScriptSerializer.RegisterConverters(converters);
			base.DescribeRenderingMode(descriptor);
			descriptor.AddProperty("localization", javaScriptSerializer.Serialize(this.Localization));
			if (this.CheckBoxes && !this.IsNativeMode)
			{
				descriptor.AddScriptProperty("checkedIndices", javaScriptSerializer.Serialize(this.GetCheckedIndices()));
			}
			descriptor.AddProperty("_dropDownWidth", this.DropDownWidth.Value);
			ControlItemContainer.AddProperty(descriptor, "_skin", base.RuntimeSkin, string.Empty);
			if (this._defaultItem != null && !this._isDefaultItemAdded)
			{
				this.ApplyDefaultItem();
			}
			descriptor.AddScriptProperty("itemData", javaScriptSerializer.Serialize(this.Items.VisibleItems));
			if (this.MaxHeight != Unit.Empty)
			{
				descriptor.AddProperty("_maxHeight", this.MaxHeight.Value);
			}
			descriptor.AddProperty("_height", this.Height.Value);
			if (this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			if (base.Events[RadComboBox.ItemCheckedEvent] != null && this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackOnCheck", true);
			}
			if (base.Events[RadComboBox.CheckAllCheckEvent] != null && this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackOnCheckAllCheck", true);
			}
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (!string.IsNullOrEmpty(this.ClientItemTemplate))
			{
				descriptor.AddProperty("_clientTemplate", this.ClientItemTemplate);
			}
			if ((this.IsBoundUsingOData || this.IsBoundUsingClientDataSource) && this.ItemsPerRequest > 0)
			{
				descriptor.AddProperty("_itemsPerRequest", this.ItemsPerRequest);
			}
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
			this.WebServiceSettings.Describe("webServiceSettings", javaScriptSerializer, descriptor);
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			if (this.IsNativeMode)
			{
				this.DisableFullRenderingModeFunctionalities();
			}
			if (this.IsDefaultItemApplicable)
			{
				descriptor.AddProperty("_defaultValue", this._defaultItem.Value);
				descriptor.AddProperty("_defaultText", this._defaultItem.Text);
			}
			base.DescribeProperty<bool>(descriptor, "_shouldFocus", this._shouldFocus, false);
			this.AriaSettings.Describe(descriptor);
			base.DescribeComponent(descriptor);
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x00179C52 File Offset: 0x00177E52
		private void DisableFullRenderingModeFunctionalities()
		{
			this.AllowCustomText = false;
			this.MarkFirstMatch = false;
			this.EnableLoadOnDemand = false;
			this.EnableAutomaticLoadOnDemand = false;
			this.Filter = RadComboBoxFilter.None;
			this.CheckBoxes = false;
			this.EmptyMessage = string.Empty;
		}

		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x060064A0 RID: 25760 RVA: 0x00179C8C File Offset: 0x00177E8C
		private bool IsDefaultItemApplicable
		{
			get
			{
				return this._defaultItem != null && !this.CheckBoxes && !this.EnableLoadOnDemand && !this.EnableAutomaticLoadOnDemand && !this.AllowCustomText && !this.IsNativeMode && !this.MarkFirstMatch && this.Filter == RadComboBoxFilter.None;
			}
		}

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x060064A1 RID: 25761 RVA: 0x00179CDC File Offset: 0x00177EDC
		// (set) Token: 0x060064A2 RID: 25762 RVA: 0x00179CFD File Offset: 0x00177EFD
		private bool IsItemSelected
		{
			get
			{
				return (bool)(this.ViewState["IsItemSelected"] ?? false);
			}
			set
			{
				this.ViewState["IsItemSelected"] = value;
			}
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x00179D18 File Offset: 0x00177F18
		private void ApplyDefaultItem()
		{
			if (this.IsDefaultItemApplicable && !this._isDefaultItemAdded)
			{
				this._defaultItem.CssClass = (this._defaultItem.CssClass + " rcbDefaultItem").Trim();
				this._defaultItem.EnableViewState = false;
				this.Items.InsertItem(0, this._defaultItem);
				if (!this.IsItemSelected || this._defaultItem.Selected)
				{
					this.SelectedIndex = 0;
					this.Text = this._defaultItem.Text;
				}
				if (this._defaultItem.Controls.Count > 0)
				{
					this._defaultItem.Controls.Clear();
				}
				this._isDefaultItemAdded = true;
			}
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x00179DD5 File Offset: 0x00177FD5
		public virtual void ClearSelection()
		{
			if (this.SelectedIndex > -1)
			{
				this.Text = string.Empty;
			}
			this.UnselectAllItems();
			this.SelectedValue = null;
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x00179DF8 File Offset: 0x00177FF8
		internal void InternalClearSelection()
		{
			this.UnselectAllItems();
			this.SelectedValue = null;
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x00179E08 File Offset: 0x00178008
		private void UnselectAllItems()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.Items[i].Selected = false;
			}
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x00179E40 File Offset: 0x00178040
		private int FindByTextInternal(string text)
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Text == text && this.Items[i].Enabled && !this.Items[i].IsSeparator)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060064A8 RID: 25768 RVA: 0x00179EA8 File Offset: 0x001780A8
		protected virtual void RenderBrowserSpecificStyles(HtmlTextWriter writer)
		{
			if (!base.DesignMode && this.AccessibilityMode)
			{
				writer.AddStyleAttribute("display", "none");
				if (this.Browser.IsBrowser("IE"))
				{
					writer.AddStyleAttribute("zoom", "1");
				}
			}
			if (base.DesignMode)
			{
				writer.AddStyleAttribute("margin-left", "-1px");
				writer.AddStyleAttribute("margin-right", "-1px");
			}
			if (base.DesignMode || this.Browser.IsBrowser("Safari") || this.Browser.IsBrowser("Chrome"))
			{
				writer.AddStyleAttribute("white-space", "normal");
			}
		}

		// Token: 0x060064A9 RID: 25769 RVA: 0x00179F5C File Offset: 0x0017815C
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this._oldText = this.Text;
			this._oldValue = this.SelectedValue;
			string text = this.Text;
			string text2 = this.SelectedValue;
			string text3 = postCollection[base.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text3))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				RadComboBoxClientState radComboBoxClientState;
				try
				{
					radComboBoxClientState = javaScriptSerializer.Deserialize<RadComboBoxClientState>(text3);
				}
				catch (InvalidOperationException)
				{
					return false;
				}
				catch (ArgumentException)
				{
					return false;
				}
				this.LoadClientState(radComboBoxClientState);
				text = radComboBoxClientState.Text;
				text2 = radComboBoxClientState.Value;
				if (radComboBoxClientState.EmptyMessage != null)
				{
					this.EmptyMessage = radComboBoxClientState.EmptyMessage;
				}
				goto IL_DD;
			}
			if (string.IsNullOrEmpty(postCollection["ComboBox_" + this.ID]))
			{
				return false;
			}
			RadComboBoxItem radComboBoxItem = this.FindItemByValue(postCollection["ComboBox_" + this.ID]);
			if (radComboBoxItem != null)
			{
				text = radComboBoxItem.Text;
				text2 = radComboBoxItem.Value;
			}
			IL_DD:
			if (text != null)
			{
				this.Text = text;
			}
			int selectedIndex = this.SelectedIndex;
			if (text2 != null && this.SelectedValue != text2)
			{
				this.SelectedValue = text2;
			}
			this.Text = this.Text.Replace('"', '"');
			if (!string.IsNullOrEmpty(this.SelectedValue))
			{
				this.SelectedValue = this.SelectedValue.Replace('"', '"');
				bool flag = true;
				if (string.IsNullOrEmpty(postCollection["__CALLBACKID"]) || !this.UniqueID.Equals(postCollection["__CALLBACKID"]))
				{
					flag = false;
				}
				if (this.Items.Count != 0 || flag)
				{
					this.cachedSelectedValue = null;
				}
			}
			int num = this.FindItemIndexByValue(text2);
			if (num != -1)
			{
				this.IsItemSelected = true;
			}
			else
			{
				int num2 = this.FindByTextInternal(text);
				if (num2 != -1)
				{
					this.IsItemSelected = true;
				}
				else
				{
					this.IsItemSelected = false;
				}
			}
			if (num < 0 && !this.AllowCustomText)
			{
				num = this.FindByTextInternal(this.Text);
				if (num > 0)
				{
					this.Items[num].Selected = true;
				}
			}
			if (!this.LoadOnDemandIsEnabled && !this.CheckBoxes && selectedIndex != num)
			{
				if (selectedIndex > -1 && selectedIndex < this.Items.Count)
				{
					this._oldText = this.Items[selectedIndex].Text;
				}
				this.SetPostDataSelection(num);
				return true;
			}
			if (this._oldText != this.Text)
			{
				return this.RaisePostDataEventsIfTextIsChanged(this._oldText);
			}
			return this._oldValue != this.SelectedValue;
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x0017A1F8 File Offset: 0x001783F8
		protected internal bool RaisePostDataEventsIfTextIsChanged(string oldText)
		{
			return !this.CheckBoxes || this.CheckedItemsTexts != RadComboBoxCheckedItemsTexts.FitInInput || this.CheckIfAllItemsAreChecked(this._oldText);
		}

		// Token: 0x060064AB RID: 25771 RVA: 0x0017A218 File Offset: 0x00178418
		protected internal bool CheckIfAllItemsAreChecked(string oldText)
		{
			bool result = true;
			if ((this.Text == this.Localization.AllItemsCheckedString || this.Text == this.GetCheckedIndices().Length.ToString() + " " + this.Localization.ItemsCheckedString) && oldText == this.GetCheckedItemsTexts())
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x0017A282 File Offset: 0x00178482
		protected void SetPostDataSelection(int selectedIndex)
		{
			if (this.Items.Count != 0 && selectedIndex < this.Items.Count)
			{
				this.InternalClearSelection();
				if (selectedIndex >= 0)
				{
					this.Items[selectedIndex].Selected = true;
				}
			}
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x0017A2BC File Offset: 0x001784BC
		private void LoadClientState(RadComboBoxClientState clientState)
		{
			if (base.IsEnabled)
			{
				this.Enabled = clientState.Enabled;
			}
			if (clientState.LogEntries == null)
			{
				return;
			}
			ClientStateLogPlayer<RadComboBoxItem> clientStateLogPlayer = new ClientStateLogPlayer<RadComboBoxItem>(this);
			this._clientChanges = clientStateLogPlayer.Play(clientState.LogEntries);
			if (clientState.CheckedIndices != null)
			{
				this.LoadCheckedIndices(clientState);
			}
		}

		// Token: 0x060064AE RID: 25774 RVA: 0x0017A310 File Offset: 0x00178510
		private void LoadCheckedIndices(RadComboBoxClientState clientState)
		{
			int[] checkedIndices = this.GetCheckedIndices();
			int[] array = new int[clientState.CheckedIndices.Length];
			Array.Copy(clientState.CheckedIndices, array, array.Length);
			if (checkedIndices.Length != array.Length)
			{
				this.CheckInternal(array);
				return;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != checkedIndices[i])
				{
					this.CheckInternal(array);
					return;
				}
			}
		}

		// Token: 0x060064AF RID: 25775 RVA: 0x0017A370 File Offset: 0x00178570
		internal void CheckInternal(int[] indeces)
		{
			this.ClearCheckedItems();
			for (int num = 0; num != indeces.Length; num++)
			{
				int num2 = indeces[num];
				IEnumerable<RadComboBoxItem> source = this.Items.VisibleItems.Cast<RadComboBoxItem>();
				if (num2 >= 0 && num2 < source.Count<RadComboBoxItem>())
				{
					source.ElementAt(num2).Checked = true;
				}
			}
		}

		// Token: 0x060064B0 RID: 25776 RVA: 0x0017A3C0 File Offset: 0x001785C0
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			ComboBoxPostBackArguments comboBoxPostBackArguments = null;
			try
			{
				comboBoxPostBackArguments = new JavaScriptSerializer().Deserialize<ComboBoxPostBackArguments>(eventArgument);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (comboBoxPostBackArguments == null)
			{
				return;
			}
			string command;
			if ((command = comboBoxPostBackArguments.Command) != null)
			{
				if (command == "Check")
				{
					int index = int.Parse(comboBoxPostBackArguments.Index);
					this.OnItemChecked(new RadComboBoxItemEventArgs(this.Items[index]));
					return;
				}
				if (!(command == "CheckAll"))
				{
					return;
				}
				this.OnCheckAllCheck(new RadComboBoxCheckAllCheckEventArgs(comboBoxPostBackArguments.CheckAllChecked));
			}
		}

		// Token: 0x060064B1 RID: 25777 RVA: 0x0017A460 File Offset: 0x00178660
		protected override void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack)
			{
				this.PerformValidation();
			}
			this.OnSelectedIndexChanged();
		}

		// Token: 0x060064B2 RID: 25778 RVA: 0x0017A476 File Offset: 0x00178676
		private void PerformValidation()
		{
			if (!this.CausesValidation)
			{
				return;
			}
			this.Page.Validate(this.ValidationGroup);
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x0017A494 File Offset: 0x00178694
		string ICallbackEventHandler.GetCallbackResult()
		{
			StringWriter stringWriter = new StringWriter();
			foreach (object obj in this.Items)
			{
				RadComboBoxItem radComboBoxItem = (RadComboBoxItem)obj;
				radComboBoxItem.RenderControl(new HtmlTextWriter(stringWriter));
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new ComboBoxItemConverter()
			});
			string text = javaScriptSerializer.Serialize(this.Items.VisibleItems);
			return string.Concat(new object[]
			{
				text,
				"_$$_",
				stringWriter,
				"_$$_",
				this._showMoreResultsMessage,
				"_$$_",
				this._endOfItems,
				"_$$_",
				this._callbackText
			});
		}

		// Token: 0x060064B4 RID: 25780 RVA: 0x0017A59C File Offset: 0x0017879C
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.RaiseCallbackEvent(eventArgument);
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x0017A5A8 File Offset: 0x001787A8
		protected internal virtual void RaiseCallbackEvent(string eventArgument)
		{
			ComboBoxPostBackArguments comboBoxPostBackArguments = this.DeserializeArguments(eventArgument);
			if (comboBoxPostBackArguments.ClientState != null)
			{
				this.LoadClientState(comboBoxPostBackArguments.ClientState);
			}
			this._callbackText = HttpUtility.UrlEncode(comboBoxPostBackArguments.Text);
			string text = HttpUtility.UrlDecode(comboBoxPostBackArguments.Text);
			this._filterText = text;
			this._numberOfItems = comboBoxPostBackArguments.NumberOfItems;
			if (this.EnableAutomaticLoadOnDemand)
			{
				if (this.DataSource == null && string.IsNullOrEmpty(this.DataSourceID))
				{
					throw new ArgumentNullException("", "There is no assigned data source. Unable to complete callback request.");
				}
				this.DataBind();
			}
			RadComboBoxItemsRequestedEventArgs radComboBoxItemsRequestedEventArgs = new RadComboBoxItemsRequestedEventArgs
			{
				Text = text,
				NumberOfItems = comboBoxPostBackArguments.NumberOfItems,
				Context = comboBoxPostBackArguments.Context,
				Message = this._showMoreResultsMessage,
				EndOfItems = this._endOfItems
			};
			this.OnItemsRequested(radComboBoxItemsRequestedEventArgs);
			this._showMoreResultsMessage = radComboBoxItemsRequestedEventArgs.Message;
			this._endOfItems = radComboBoxItemsRequestedEventArgs.EndOfItems;
		}

		// Token: 0x060064B6 RID: 25782 RVA: 0x0017A694 File Offset: 0x00178894
		private string GetLastWord(string text)
		{
			int num = text.LastIndexOf(this.AutoCompleteSeparator);
			return text.Substring(num + 1, text.Length - num - 1);
		}

		// Token: 0x060064B7 RID: 25783 RVA: 0x0017A6C4 File Offset: 0x001788C4
		internal ComboBoxPostBackArguments DeserializeArguments(string eventArgument)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Deserialize<ComboBoxPostBackArguments>(eventArgument);
		}

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x060064B8 RID: 25784 RVA: 0x0017A6DE File Offset: 0x001788DE
		// (set) Token: 0x060064B9 RID: 25785 RVA: 0x0017A6E6 File Offset: 0x001788E6
		string ITextControl.Text
		{
			get
			{
				return this.SelectedValue;
			}
			set
			{
				this.SelectedValue = value;
			}
		}

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x060064BA RID: 25786 RVA: 0x0017A6EF File Offset: 0x001788EF
		internal bool IsCallback
		{
			get
			{
				return this.Page != null && this.Page.IsCallback;
			}
		}

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x060064BB RID: 25787 RVA: 0x0017A706 File Offset: 0x00178906
		[ClientControlProperty]
		[ClientPropertyName("enableLoadOnDemand")]
		internal bool LoadOnDemandIsEnabled
		{
			get
			{
				return this.EnableLoadOnDemand || this.EnableAutomaticLoadOnDemand;
			}
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x0017A718 File Offset: 0x00178918
		private bool IsBoundToLinqDataSource()
		{
			return this.GetData() is LinqDataSourceView;
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x0017A72C File Offset: 0x0017892C
		private bool IsBoundToEntityDataSource()
		{
			return this.GetData().GetType().ToString() == "System.Web.UI.WebControls.EntityDataSourceView";
		}

		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x0017A755 File Offset: 0x00178955
		// (set) Token: 0x060064BF RID: 25791 RVA: 0x0017A75D File Offset: 0x0017895D
		private protected new DataSourceSelectArguments SelectArguments { protected get; private set; }

		// Token: 0x060064C0 RID: 25792 RVA: 0x0017A768 File Offset: 0x00178968
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			this.SelectArguments = new DataSourceSelectArguments();
			if (this.IsCallback && this.EnableAutomaticLoadOnDemand && (this.IsBoundToLinqDataSource() || this.IsBoundToEntityDataSource()) && (this.ShowMoreResultsBox || this.EnableVirtualScrolling))
			{
				this.SelectArguments.StartRowIndex = this.NumberOfItems;
				this.SelectArguments.MaximumRows = this.ItemsPerRequest;
				if (this.ShowMoreResultsBox)
				{
					this.SelectArguments.RetrieveTotalRowCount = true;
				}
				if (this.IsBoundToEntityDataSource() && (this.ShowMoreResultsBox || this.EnableVirtualScrolling))
				{
					this.SelectArguments.SortExpression = string.Format("it.[{0}]", this.DataTextField);
				}
			}
			return this.SelectArguments;
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x0017A825 File Offset: 0x00178A25
		public override void DataBind()
		{
			if (this.EnableAutomaticLoadOnDemand && !this.IsCallback)
			{
				return;
			}
			base.DataBind();
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x0017A840 File Offset: 0x00178A40
		internal static Control FindControlRecursivelyFromTopToBottom(Control startingControl, string controlID)
		{
			if (startingControl == startingControl.Page)
			{
				return startingControl.FindControl(controlID);
			}
			Control control = startingControl;
			Control control2 = null;
			while (control2 == null && control != startingControl.Page)
			{
				control = control.NamingContainer;
				if (control == null)
				{
					throw new NullReferenceException("Cannot find Control with ID '" + controlID + "'");
				}
				control2 = control.FindControl(controlID);
			}
			return control2;
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x0017A89C File Offset: 0x00178A9C
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			DataSourceView data = this.GetData();
			if (this.IsCallback && this.EnableAutomaticLoadOnDemand)
			{
				LinqDataSourceView linqDataSourceView = data as LinqDataSourceView;
				if (linqDataSourceView != null)
				{
					string text = string.Format("{0}.{{0}}(\"{1}\")", this.DataTextField, this._filterText.Replace("\"", "\"\""));
					text = string.Format(text, (this.Filter == RadComboBoxFilter.Contains) ? "Contains" : "StartsWith");
					if (linqDataSourceView.Where.Length > 0)
					{
						string format = "{0} && {1}";
						string where = linqDataSourceView.Where;
						linqDataSourceView.Where = string.Format(format, where, text);
					}
					else
					{
						linqDataSourceView.Where = text;
					}
				}
				else if (this.IsBoundToEntityDataSource())
				{
					string text2 = string.Format("it.[{0}] LIKE '{{0}}{1}%'", this.DataTextField, this._filterText.Replace("'", "''"));
					text2 = string.Format(text2, (this.Filter == RadComboBoxFilter.Contains) ? "%" : string.Empty);
					DataSourceControl component = (DataSourceControl)RadComboBox.FindControlRecursivelyFromTopToBottom(this, this.DataSourceID);
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Where"];
					object value = propertyDescriptor.GetValue(component);
					string text3 = (value != null) ? value.ToString() : string.Empty;
					if (string.IsNullOrEmpty(text3))
					{
						propertyDescriptor.SetValue(component, text2);
					}
					else
					{
						string arg = string.IsNullOrEmpty(text2) ? string.Empty : string.Format(" AND {0}", text2);
						propertyDescriptor.SetValue(component, string.Format("{0}{1}", text3, arg));
					}
				}
			}
			data.Select(this.CreateDataSourceSelectArguments(), new DataSourceViewSelectCallback(this.OnDataSourceViewSelectCallback));
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x0017AA50 File Offset: 0x00178C50
		private DataView ResolveDataReader(IDataReader dataReader)
		{
			DataTable dataTable = new DataTable();
			for (int i = 0; i < dataReader.FieldCount; i++)
			{
				DataColumn column = new DataColumn(dataReader.GetName(i), dataReader.GetFieldType(i));
				dataTable.Columns.Add(column);
			}
			while (dataReader.Read())
			{
				DataRow dataRow = dataTable.NewRow();
				for (int j = 0; j < dataReader.FieldCount; j++)
				{
					dataRow[j] = dataReader.GetValue(dataReader.GetOrdinal(dataReader.GetName(j)));
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable.DefaultView;
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x0017AAE8 File Offset: 0x00178CE8
		protected void OnDataSourceViewSelectCallback(IEnumerable data)
		{
			if (this.IsCallback && this.EnableAutomaticLoadOnDemand)
			{
				if (this.IsBoundToLinqDataSource() || this.IsBoundToEntityDataSource())
				{
					if (this.ShowMoreResultsBox)
					{
						this.SetShowMoreResultsMessage(this.SelectArguments.TotalRowCount);
					}
					if (this.ShowMoreResultsBox || this.EnableVirtualScrolling)
					{
						this.SetEndOfItems(this.SelectArguments.TotalRowCount);
					}
				}
				else
				{
					IDataReader dataReader = data as IDataReader;
					if (dataReader != null)
					{
						data = this.ResolveDataReader(dataReader);
					}
					data = this.FilterData(data, this._filterText);
					ComboBoxInMemoryEnumerableHelper comboBoxInMemoryEnumerableHelper = new ComboBoxInMemoryEnumerableHelper();
					int count = comboBoxInMemoryEnumerableHelper.GetCount(data);
					if (this.ShowMoreResultsBox)
					{
						this.SetShowMoreResultsMessage(count);
					}
					if (this.ShowMoreResultsBox || this.EnableVirtualScrolling)
					{
						this.SetEndOfItems(count);
						data = this.GetNextItems(data, comboBoxInMemoryEnumerableHelper);
					}
				}
			}
			this.PerformDataBinding(data);
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x0017ABC0 File Offset: 0x00178DC0
		internal virtual void SetShowMoreResultsMessage(int totalItemsCount)
		{
			if (totalItemsCount == 0)
			{
				this._showMoreResultsMessage = this.Localization.NoMatches;
				return;
			}
			int num = (this.ItemsPerRequest == -1) ? totalItemsCount : (this._numberOfItems + this.ItemsPerRequest);
			if (num > totalItemsCount)
			{
				num = totalItemsCount;
			}
			this._showMoreResultsMessage = string.Format(this.Localization.ShowMoreFormatString, num, totalItemsCount);
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x0017AC24 File Offset: 0x00178E24
		internal virtual void SetEndOfItems(int totalItemsCount)
		{
			if (this.ItemsPerRequest < 0)
			{
				this._endOfItems = true;
				return;
			}
			if (this._numberOfItems + this.ItemsPerRequest >= totalItemsCount)
			{
				this._endOfItems = true;
			}
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x0017AC4E File Offset: 0x00178E4E
		internal virtual IEnumerable GetNextItems(IEnumerable data, ComboBoxEnumerableHelper helper)
		{
			if (this.ItemsPerRequest < 0)
			{
				return data;
			}
			return helper.GetPage(data, this._numberOfItems, this.ItemsPerRequest);
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x0017AEE0 File Offset: 0x001790E0
		internal virtual IEnumerable FilterData(IEnumerable data, string filterText)
		{
			foreach (object dataItem in data)
			{
				string dataItemString;
				if (this.DataTextField.Length > 0)
				{
					dataItemString = DataBinder.Eval(dataItem, this.DataTextField).ToString();
				}
				else
				{
					dataItemString = dataItem.ToString();
				}
				if (this.Filter == RadComboBoxFilter.Contains)
				{
					if (dataItemString.ToLowerInvariant().Contains(filterText.ToLowerInvariant()))
					{
						yield return dataItem;
					}
				}
				else if (dataItemString.StartsWith(filterText, StringComparison.InvariantCultureIgnoreCase))
				{
					yield return dataItem;
				}
			}
			yield break;
		}

		// Token: 0x060064CA RID: 25802 RVA: 0x0017AF0B File Offset: 0x0017910B
		protected override void PerformSelect()
		{
			this.OnDataBinding(EventArgs.Empty);
			base.RequiresDataBinding = false;
			base.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		// Token: 0x060064CB RID: 25803 RVA: 0x0017AF30 File Offset: 0x00179130
		protected override void PerformDataBinding(IEnumerable dataSource)
		{
			base.PerformDataBinding(dataSource);
			if (!string.IsNullOrEmpty(this.cachedSelectedValue))
			{
				RadComboBoxItem radComboBoxItem = this.FindItemByValue(this.cachedSelectedValue);
				if (radComboBoxItem == null)
				{
					throw new ArgumentOutOfRangeException("value", "Selection out of range");
				}
				int num = this.Items.IndexOf(radComboBoxItem);
				if (this.cachedSelectedIndex != -1 && this.cachedSelectedIndex != num)
				{
					throw new ArgumentOutOfRangeException("value", "SelectedIndex and SelectedValue are mutually exclusive");
				}
				this.SelectedIndex = num;
				this.cachedSelectedValue = null;
				this.cachedSelectedIndex = -1;
			}
			else if (this.cachedSelectedIndex != -1)
			{
				this.SelectedIndex = this.cachedSelectedIndex;
				this.cachedSelectedIndex = -1;
			}
			this.UpdateFromCachedValueIndex();
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x0017AFDC File Offset: 0x001791DC
		private void UpdateFromCachedValueIndex()
		{
			if (string.IsNullOrEmpty(this.cachedSelectedValue))
			{
				if (this.cachedSelectedIndex != -1)
				{
					this.SelectedIndex = this.cachedSelectedIndex;
					this.cachedSelectedIndex = -1;
				}
				return;
			}
			int num = this.FindItemIndexByValue(this.cachedSelectedValue);
			if (-1 == num)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.SelectedIndex = num;
			this.cachedSelectedValue = null;
			this.cachedSelectedIndex = -1;
		}

		// Token: 0x17002111 RID: 8465
		// (get) Token: 0x060064CD RID: 25805 RVA: 0x0017B044 File Offset: 0x00179244
		// (set) Token: 0x060064CE RID: 25806 RVA: 0x0017B04C File Offset: 0x0017924C
		[SimplePersistenceSetting]
		internal int[] CheckedIndices
		{
			get
			{
				return this.GetCheckedIndices();
			}
			set
			{
				int[] checkedIndices = this.GetCheckedIndices();
				foreach (int index in checkedIndices)
				{
					this.Items[index].Checked = false;
				}
				for (int j = 0; j < value.Length; j++)
				{
					int num = value[j];
					if (num > -1 && num < this.Items.Count)
					{
						this.Items[num].Checked = true;
					}
				}
			}
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x0017B0CC File Offset: 0x001792CC
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "allowCustomText", this.AllowCustomText, false);
			base.DescribeProperty<string>(descriptor, "autoCompleteSeparator", this.AutoCompleteSeparator, "");
			base.DescribeProperty<bool>(descriptor, "changeText", this.ChangeTextOnKeyBoardNavigation, true);
			base.DescribeProperty<bool>(descriptor, "_checkBoxes", this.CheckBoxes, false);
			base.DescribeProperty<RadComboBoxCheckedItemsTexts>(descriptor, "_checkedItemsTexts", this.CheckedItemsTexts, RadComboBoxCheckedItemsTexts.FitInInput);
			base.DescribeProperty<bool>(descriptor, "closeDropDownOnBlur", this.CloseDropDownOnBlur, true);
			base.DescribeProperty<int>(descriptor, "collapseDelay", this.CollapseDelay, 0);
			base.DescribeProperty<RadComboBoxDropDownAutoWidth>(descriptor, "_dropDownAutoWidth", this.DropDownAutoWidth, RadComboBoxDropDownAutoWidth.Disabled);
			base.DescribeProperty<string>(descriptor, "emptyMessage", this.EmptyMessage, "");
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "_lodIsAutomatic", this.EnableAutomaticLoadOnDemand, false);
			base.DescribeProperty<bool>(descriptor, "_enableCheckAllItemsCheckBox", this.EnableCheckAllItemsCheckBox, false);
			base.DescribeProperty<bool>(descriptor, "enableItemCaching", this.EnableItemCaching, false);
			base.DescribeProperty<bool>(descriptor, "_enableOverlay", this.EnableOverlay, true);
			base.DescribeProperty<bool>(descriptor, "_enableScreenBoundaryDetection", this.EnableScreenBoundaryDetection, true);
			base.DescribeProperty<bool>(descriptor, "enableTextSelection", this.EnableTextSelection, true);
			base.DescribeProperty<bool>(descriptor, "_virtualScroll", this.EnableVirtualScrolling, false);
			base.DescribeProperty<int>(descriptor, "expandDelay", this.ExpandDelay, 0);
			base.DescribeProperty<RadComboBoxExpandDirection>(descriptor, "expandDirection", this.ExpandDirection, RadComboBoxExpandDirection.Down);
			base.DescribeProperty<RadComboBoxFilter>(descriptor, "filter", this.Filter, RadComboBoxFilter.None);
			base.DescribeProperty<bool>(descriptor, "highlightTemplatedItems", this.HighlightTemplatedItems, false);
			base.DescribeProperty<bool>(descriptor, "isCaseSensitive", this.IsCaseSensitive, false);
			base.DescribeProperty<bool>(descriptor, "_isTemplated", this.IsTemplated, false);
			base.DescribeProperty<int>(descriptor, "itemRequestTimeout", this.ItemRequestTimeout, 300);
			base.DescribeProperty<string>(descriptor, "loadingMessage", this.LoadingMessage, "Loading...");
			base.DescribeProperty<bool>(descriptor, "enableLoadOnDemand", this.LoadOnDemandIsEnabled, false);
			base.DescribeProperty<bool>(descriptor, "markFirstMatch", this.MarkFirstMatch, false);
			base.DescribeProperty<int>(descriptor, "_minFilterLength", this.MinFilterLength, 0);
			base.DescribeProperty<int>(descriptor, "offsetX", this.OffsetX, 0);
			base.DescribeProperty<int>(descriptor, "offsetY", this.OffsetY, 0);
			base.DescribeProperty<bool>(descriptor, "_openDropDownOnLoad", this.OpenDropDownOnLoad, false);
			base.DescribeProperty<int>(descriptor, "selectedIndex", this.SelectedIndex, -1);
			base.DescribeProperty<string>(descriptor, "_value", this.SelectedValue, "");
			base.DescribeProperty<bool>(descriptor, "_showDropDownOnTextboxClick", this.ShowDropDownOnTextboxClick, true);
			base.DescribeProperty<bool>(descriptor, "_showMoreResultsBox", this.ShowMoreResultsBox, false);
			base.DescribeProperty<string>(descriptor, "_text", this.Text, "");
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x0017B3B8 File Offset: 0x001795B8
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "onClientBlur", this.OnClientBlur);
			RadDataBoundControl.DescribeEvent(descriptor, "checkAllChecked", this.OnClientCheckAllChecked);
			RadDataBoundControl.DescribeEvent(descriptor, "checkAllChecking", this.OnClientCheckAllChecking);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosed", this.OnClientDropDownClosed);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosing", this.OnClientDropDownClosing);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpened", this.OnClientDropDownOpened);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpening", this.OnClientDropDownOpening);
			RadDataBoundControl.DescribeEvent(descriptor, "onClientFocus", this.OnClientFocus);
			RadDataBoundControl.DescribeEvent(descriptor, "itemChecked", this.OnClientItemChecked);
			RadDataBoundControl.DescribeEvent(descriptor, "itemChecking", this.OnClientItemChecking);
			RadDataBoundControl.DescribeEvent(descriptor, "itemDataBound", this.OnClientItemDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequested", this.OnClientItemsRequested);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequestFailed", this.OnClientItemsRequestFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequesting", this.OnClientItemsRequesting);
			RadDataBoundControl.DescribeEvent(descriptor, "keyPressing", this.OnClientKeyPressing);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "selectedIndexChanged", this.OnClientSelectedIndexChanged);
			RadDataBoundControl.DescribeEvent(descriptor, "selectedIndexChanging", this.OnClientSelectedIndexChanging);
			RadDataBoundControl.DescribeEvent(descriptor, "templateDataBound", this.OnClientTemplateDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "textChange", this.OnClientTextChange);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17002112 RID: 8466
		// (get) Token: 0x060064D1 RID: 25809 RVA: 0x0017B520 File Offset: 0x00179720
		[Browsable(false)]
		public IList<ClientOperation<RadComboBoxItem>> ClientChanges
		{
			get
			{
				return this._clientChanges;
			}
		}

		// Token: 0x17002113 RID: 8467
		// (get) Token: 0x060064D2 RID: 25810 RVA: 0x0017B528 File Offset: 0x00179728
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public RadComboBoxItemCollection Items
		{
			get
			{
				return (RadComboBoxItemCollection)base.Children;
			}
		}

		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x060064D3 RID: 25811 RVA: 0x0017B535 File Offset: 0x00179735
		// (set) Token: 0x060064D4 RID: 25812 RVA: 0x0017B560 File Offset: 0x00179760
		[Description("Whether to evaluate databinding expressions for unbound items")]
		[DefaultValue(true)]
		[Category("Data")]
		public bool EnableItemBindingExpressions
		{
			get
			{
				return this.ViewState["EnableItemBindingExpressions"] == null || (bool)this.ViewState["EnableItemBindingExpressions"];
			}
			set
			{
				this.ViewState["EnableItemBindingExpressions"] = value;
			}
		}

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x060064D5 RID: 25813 RVA: 0x0017B578 File Offset: 0x00179778
		// (set) Token: 0x060064D6 RID: 25814 RVA: 0x0017B599 File Offset: 0x00179799
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x060064D7 RID: 25815 RVA: 0x0017B5B1 File Offset: 0x001797B1
		// (set) Token: 0x060064D8 RID: 25816 RVA: 0x0017B5D2 File Offset: 0x001797D2
		[DefaultValue(-1)]
		[Category("Behavior")]
		public int ItemsPerRequest
		{
			get
			{
				return (int)(this.ViewState["ItemsPerRequest"] ?? -1);
			}
			set
			{
				this.ViewState["ItemsPerRequest"] = value;
			}
		}

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x060064D9 RID: 25817 RVA: 0x0017B5EA File Offset: 0x001797EA
		// (set) Token: 0x060064DA RID: 25818 RVA: 0x0017B60B File Offset: 0x0017980B
		[ClientPropertyName("_minFilterLength")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Sets the minimum length of the typed text before the control initiates a request for new Items when EnableLoadOnDemand is True.")]
		[DefaultValue(0)]
		public int MinFilterLength
		{
			get
			{
				return (int)(this.ViewState["MinFilterLength"] ?? 0);
			}
			set
			{
				this.ViewState["MinFilterLength"] = value;
			}
		}

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x0017B623 File Offset: 0x00179823
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.Items.Count <= 0;
			}
		}

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x060064DC RID: 25820 RVA: 0x0017B636 File Offset: 0x00179836
		// (set) Token: 0x060064DD RID: 25821 RVA: 0x0017B661 File Offset: 0x00179861
		[DefaultValue(RadComboBoxImagePosition.Right)]
		[Bindable(false)]
		[Category("Setup")]
		[Description("")]
		public RadComboBoxImagePosition RadComboBoxImagePosition
		{
			get
			{
				if (this.ViewState["RadComboBoxImagePosition"] == null)
				{
					return RadComboBoxImagePosition.Right;
				}
				return (RadComboBoxImagePosition)this.ViewState["RadComboBoxImagePosition"];
			}
			set
			{
				this.ViewState["RadComboBoxImagePosition"] = value;
			}
		}

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x060064DE RID: 25822 RVA: 0x0017B679 File Offset: 0x00179879
		// (set) Token: 0x060064DF RID: 25823 RVA: 0x0017B699 File Offset: 0x00179899
		[DefaultValue("")]
		[Category("Setup")]
		[Bindable(false)]
		[Description("The Text of the ComboBox")]
		[ClientControlProperty]
		[ClientPropertyName("_text")]
		public string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x060064E0 RID: 25824 RVA: 0x0017B6AC File Offset: 0x001798AC
		// (set) Token: 0x060064E1 RID: 25825 RVA: 0x0017B6CC File Offset: 0x001798CC
		[Localizable(true)]
		[ClientPropertyName("loadingMessage")]
		[DefaultValue("Loading...")]
		[ClientControlProperty]
		[Bindable(false)]
		[Category("Setup")]
		[Description("The message that is shown in the More Results Box after items have been loaded from the server.")]
		public string LoadingMessage
		{
			get
			{
				return (string)(this.ViewState["LoadingMessage"] ?? "Loading...");
			}
			set
			{
				this.ViewState["LoadingMessage"] = value;
			}
		}

		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x060064E2 RID: 25826 RVA: 0x0017B6DF File Offset: 0x001798DF
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the settings(service path and method name)for the web service used to populate items.")]
		public NavigationControlWebServiceSettings WebServiceSettings
		{
			get
			{
				return this._webServiceSettings;
			}
		}

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x060064E3 RID: 25827 RVA: 0x0017B6E7 File Offset: 0x001798E7
		// (set) Token: 0x060064E4 RID: 25828 RVA: 0x0017B70C File Offset: 0x0017990C
		[DefaultValue(300)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("itemRequestTimeout")]
		[Bindable(false)]
		public int ItemRequestTimeout
		{
			get
			{
				return (int)(this.ViewState["ItemRequestTimeout"] ?? 300);
			}
			set
			{
				this.ViewState["ItemRequestTimeout"] = value;
			}
		}

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x060064E5 RID: 25829 RVA: 0x0017B724 File Offset: 0x00179924
		// (set) Token: 0x060064E6 RID: 25830 RVA: 0x0017B749 File Offset: 0x00179949
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(6000)]
		public int ZIndex
		{
			get
			{
				return (int)(this.ViewState["ZIndex"] ?? 6000);
			}
			set
			{
				this.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x060064E7 RID: 25831 RVA: 0x0017B761 File Offset: 0x00179961
		// (set) Token: 0x060064E8 RID: 25832 RVA: 0x0017B782 File Offset: 0x00179982
		[Bindable(false)]
		[ClientPropertyName("_openDropDownOnLoad")]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool OpenDropDownOnLoad
		{
			get
			{
				return (bool)(this.ViewState["OpenDropDownOnLoad"] ?? false);
			}
			set
			{
				this.ViewState["OpenDropDownOnLoad"] = value;
			}
		}

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x060064E9 RID: 25833 RVA: 0x0017B79A File Offset: 0x0017999A
		// (set) Token: 0x060064EA RID: 25834 RVA: 0x0017B7C9 File Offset: 0x001799C9
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Behavior")]
		[Themeable(true)]
		[ClientControlProperty]
		[Description("Message shown when the text is empty.")]
		[NotifyParentProperty(true)]
		public virtual string EmptyMessage
		{
			get
			{
				if (this.ViewState["EmptyMessage"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["EmptyMessage"];
			}
			set
			{
				this.ViewState["EmptyMessage"] = value;
			}
		}

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x060064EB RID: 25835 RVA: 0x0017B7DC File Offset: 0x001799DC
		// (set) Token: 0x060064EC RID: 25836 RVA: 0x0017B7FD File Offset: 0x001799FD
		[Bindable(false)]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientPropertyName("isCaseSensitive")]
		public bool IsCaseSensitive
		{
			get
			{
				return (bool)(this.ViewState["IsCaseSensitive"] ?? false);
			}
			set
			{
				this.ViewState["IsCaseSensitive"] = value;
			}
		}

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x060064ED RID: 25837 RVA: 0x0017B815 File Offset: 0x00179A15
		// (set) Token: 0x060064EE RID: 25838 RVA: 0x0017B836 File Offset: 0x00179A36
		[DefaultValue(false)]
		[ClientPropertyName("_showMoreResultsBox")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Bindable(false)]
		public bool ShowMoreResultsBox
		{
			get
			{
				return (bool)(this.ViewState["ShowMoreResultsBox"] ?? false);
			}
			set
			{
				this.ViewState["ShowMoreResultsBox"] = value;
			}
		}

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x060064EF RID: 25839 RVA: 0x0017B84E File Offset: 0x00179A4E
		// (set) Token: 0x060064F0 RID: 25840 RVA: 0x0017B86F File Offset: 0x00179A6F
		[DefaultValue(false)]
		[ClientPropertyName("markFirstMatch")]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool MarkFirstMatch
		{
			get
			{
				return (bool)(this.ViewState["MarkFirstMatch"] ?? false);
			}
			set
			{
				this.ViewState["MarkFirstMatch"] = value;
			}
		}

		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x060064F1 RID: 25841 RVA: 0x0017B887 File Offset: 0x00179A87
		// (set) Token: 0x060064F2 RID: 25842 RVA: 0x0017B8B2 File Offset: 0x00179AB2
		[ClientControlProperty]
		[DefaultValue(RadComboBoxFilter.None)]
		[Bindable(false)]
		[ClientPropertyName("filter")]
		[Category("Behavior")]
		public RadComboBoxFilter Filter
		{
			get
			{
				if (this.ViewState["Filter"] == null)
				{
					return RadComboBoxFilter.None;
				}
				return (RadComboBoxFilter)this.ViewState["Filter"];
			}
			set
			{
				this.ViewState["Filter"] = value;
			}
		}

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x060064F3 RID: 25843 RVA: 0x0017B8CA File Offset: 0x00179ACA
		// (set) Token: 0x060064F4 RID: 25844 RVA: 0x0017B8EB File Offset: 0x00179AEB
		[Bindable(false)]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool EnableLoadOnDemand
		{
			get
			{
				return (bool)(this.ViewState["EnableLoadOnDemand"] ?? false);
			}
			set
			{
				this.ViewState["EnableLoadOnDemand"] = value;
			}
		}

		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x060064F5 RID: 25845 RVA: 0x0017B903 File Offset: 0x00179B03
		// (set) Token: 0x060064F6 RID: 25846 RVA: 0x0017B924 File Offset: 0x00179B24
		[Bindable(false)]
		[DefaultValue(false)]
		[ClientPropertyName("_lodIsAutomatic")]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool EnableAutomaticLoadOnDemand
		{
			get
			{
				return (bool)(this.ViewState["EnableAutomaticLoadOnDemand"] ?? false);
			}
			set
			{
				this.ViewState["EnableAutomaticLoadOnDemand"] = value;
			}
		}

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x060064F7 RID: 25847 RVA: 0x0017B93C File Offset: 0x00179B3C
		// (set) Token: 0x060064F8 RID: 25848 RVA: 0x0017B95D File Offset: 0x00179B5D
		[DefaultValue(false)]
		[ClientPropertyName("enableItemCaching")]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool EnableItemCaching
		{
			get
			{
				return (bool)(this.ViewState["EnableItemCaching"] ?? false);
			}
			set
			{
				this.ViewState["EnableItemCaching"] = value;
			}
		}

		// Token: 0x17002128 RID: 8488
		// (get) Token: 0x060064F9 RID: 25849 RVA: 0x0017B975 File Offset: 0x00179B75
		// (set) Token: 0x060064FA RID: 25850 RVA: 0x0017B996 File Offset: 0x00179B96
		[DefaultValue(false)]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_virtualScroll")]
		public bool EnableVirtualScrolling
		{
			get
			{
				return (bool)(this.ViewState["EnableVirtualScrolling"] ?? false);
			}
			set
			{
				this.ViewState["EnableVirtualScrolling"] = value;
			}
		}

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x060064FB RID: 25851 RVA: 0x0017B9AE File Offset: 0x00179BAE
		// (set) Token: 0x060064FC RID: 25852 RVA: 0x0017B9CF File Offset: 0x00179BCF
		[ClientControlProperty]
		[Category("Behavior")]
		[Bindable(false)]
		[ClientPropertyName("enableTextSelection")]
		[DefaultValue(true)]
		public bool EnableTextSelection
		{
			get
			{
				return (bool)(this.ViewState["EnableTextSelection"] ?? true);
			}
			set
			{
				this.ViewState["EnableTextSelection"] = value;
			}
		}

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x060064FD RID: 25853 RVA: 0x0017B9E7 File Offset: 0x00179BE7
		// (set) Token: 0x060064FE RID: 25854 RVA: 0x0017BA08 File Offset: 0x00179C08
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool ShowToggleImage
		{
			get
			{
				return (bool)(this.ViewState["ShowToggleImage"] ?? true);
			}
			set
			{
				this.ViewState["ShowToggleImage"] = value;
			}
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x060064FF RID: 25855 RVA: 0x0017BA20 File Offset: 0x00179C20
		// (set) Token: 0x06006500 RID: 25856 RVA: 0x0017BA41 File Offset: 0x00179C41
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool NoWrap
		{
			get
			{
				return (bool)(this.ViewState["NoWrap"] ?? false);
			}
			set
			{
				this.ViewState["NoWrap"] = value;
			}
		}

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x06006501 RID: 25857 RVA: 0x0017BA59 File Offset: 0x00179C59
		// (set) Token: 0x06006502 RID: 25858 RVA: 0x0017BA7A File Offset: 0x00179C7A
		[Category("Behavior")]
		[Bindable(false)]
		[ClientControlProperty]
		[ClientPropertyName("closeDropDownOnBlur")]
		[DefaultValue(true)]
		public bool CloseDropDownOnBlur
		{
			get
			{
				return (bool)(this.ViewState["CloseDropDownOnBlur"] ?? true);
			}
			set
			{
				this.ViewState["CloseDropDownOnBlur"] = value;
			}
		}

		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x06006503 RID: 25859 RVA: 0x0017BA92 File Offset: 0x00179C92
		// (set) Token: 0x06006504 RID: 25860 RVA: 0x0017BAB3 File Offset: 0x00179CB3
		[ClientControlProperty]
		[Bindable(false)]
		[ClientPropertyName("allowCustomText")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool AllowCustomText
		{
			get
			{
				return (bool)(this.ViewState["AllowCustomText"] ?? false);
			}
			set
			{
				this.ViewState["AllowCustomText"] = value;
			}
		}

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x06006505 RID: 25861 RVA: 0x0017BACB File Offset: 0x00179CCB
		// (set) Token: 0x06006506 RID: 25862 RVA: 0x0017BAEC File Offset: 0x00179CEC
		[DefaultValue(true)]
		[ClientPropertyName("changeText")]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool ChangeTextOnKeyBoardNavigation
		{
			get
			{
				return (bool)(this.ViewState["ChangeTextOnKeyBoardNavigation"] ?? true);
			}
			set
			{
				this.ViewState["ChangeTextOnKeyBoardNavigation"] = value;
			}
		}

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x06006507 RID: 25863 RVA: 0x0017BB04 File Offset: 0x00179D04
		// (set) Token: 0x06006508 RID: 25864 RVA: 0x0017BB24 File Offset: 0x00179D24
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Behavior")]
		public string ErrorMessage
		{
			get
			{
				return (string)(this.ViewState["ErrorMessage"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ErrorMessage"] = value;
			}
		}

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x06006509 RID: 25865 RVA: 0x0017BB37 File Offset: 0x00179D37
		// (set) Token: 0x0600650A RID: 25866 RVA: 0x0017BB58 File Offset: 0x00179D58
		[ClientPropertyName("_showDropDownOnTextboxClick")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Bindable(false)]
		[DefaultValue(true)]
		public bool ShowDropDownOnTextboxClick
		{
			get
			{
				return (bool)(this.ViewState["ShowDropDownOnTextboxClick"] ?? true);
			}
			set
			{
				this.ViewState["ShowDropDownOnTextboxClick"] = value;
			}
		}

		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x0600650B RID: 25867 RVA: 0x0017BB70 File Offset: 0x00179D70
		// (set) Token: 0x0600650C RID: 25868 RVA: 0x0017BB91 File Offset: 0x00179D91
		[ClientControlProperty]
		[ClientPropertyName("_enableScreenBoundaryDetection")]
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool EnableScreenBoundaryDetection
		{
			get
			{
				return (bool)(this.ViewState["EnableScreenBoundaryDetection"] ?? true);
			}
			set
			{
				this.ViewState["EnableScreenBoundaryDetection"] = value;
			}
		}

		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x0600650D RID: 25869 RVA: 0x0017BBA9 File Offset: 0x00179DA9
		// (set) Token: 0x0600650E RID: 25870 RVA: 0x0017BBD4 File Offset: 0x00179DD4
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("expandDirection")]
		[Description("The expand direction of the RadComboBox dropdown.")]
		[DefaultValue(RadComboBoxExpandDirection.Down)]
		public virtual RadComboBoxExpandDirection ExpandDirection
		{
			get
			{
				if (this.ViewState["ExpandDirection"] == null)
				{
					return RadComboBoxExpandDirection.Down;
				}
				return (RadComboBoxExpandDirection)this.ViewState["ExpandDirection"];
			}
			set
			{
				this.ViewState["ExpandDirection"] = value;
			}
		}

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x0600650F RID: 25871 RVA: 0x0017BBEC File Offset: 0x00179DEC
		// (set) Token: 0x06006510 RID: 25872 RVA: 0x0017BC0D File Offset: 0x00179E0D
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientPropertyName("highlightTemplatedItems")]
		[Bindable(false)]
		public bool HighlightTemplatedItems
		{
			get
			{
				return (bool)(this.ViewState["HighlightTemplatedItems"] ?? false);
			}
			set
			{
				this.ViewState["HighlightTemplatedItems"] = value;
			}
		}

		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x06006511 RID: 25873 RVA: 0x0017BC25 File Offset: 0x00179E25
		// (set) Token: 0x06006512 RID: 25874 RVA: 0x0017BC45 File Offset: 0x00179E45
		[ClientPropertyName("autoCompleteSeparator")]
		[ClientControlProperty]
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue("")]
		public string AutoCompleteSeparator
		{
			get
			{
				return (string)(this.ViewState["AutoCompleteSeparator"] ?? string.Empty);
			}
			set
			{
				this.ViewState["AutoCompleteSeparator"] = value;
			}
		}

		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x06006513 RID: 25875 RVA: 0x0017BC58 File Offset: 0x00179E58
		// (set) Token: 0x06006514 RID: 25876 RVA: 0x0017BC79 File Offset: 0x00179E79
		[DefaultValue(false)]
		[Category("Behavior")]
		[Bindable(true)]
		public bool AccessibilityMode
		{
			get
			{
				return (bool)(this.ViewState["AccessibilityMode"] ?? false);
			}
			set
			{
				this.ViewState["AccessibilityMode"] = value;
			}
		}

		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x06006515 RID: 25877 RVA: 0x0017BC94 File Offset: 0x00179E94
		[Category("Behavior")]
		[Bindable(false)]
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RadComboBoxItem SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex >= 0)
				{
					return this.Items[selectedIndex];
				}
				return null;
			}
		}

		// Token: 0x17002137 RID: 8503
		// (get) Token: 0x06006516 RID: 25878 RVA: 0x0017BCBC File Offset: 0x00179EBC
		// (set) Token: 0x06006517 RID: 25879 RVA: 0x0017BDA8 File Offset: 0x00179FA8
		[Description("SelectedIndex")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		[Bindable(true)]
		[Browsable(false)]
		[Category("Behavior")]
		[DefaultValue(-1)]
		[ClientControlProperty]
		[ClientPropertyName("selectedIndex")]
		public virtual int SelectedIndex
		{
			get
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (this.Items[i].Selected && !this.Items[i].IsSeparator)
					{
						return i;
					}
				}
				if (!string.IsNullOrEmpty(this.EmptyMessage) || this.CheckBoxes)
				{
					return -1;
				}
				if (this._defaultItem != null)
				{
					return -1;
				}
				if (this.Items.Count > 0)
				{
					if (this.ReadOnly || ((this.MarkFirstMatch || this.Filter != RadComboBoxFilter.None) && !this.AllowCustomText && !this.EnableLoadOnDemand))
					{
						RadComboBoxItem radComboBoxItem = this.FindNextAvailableItem(-1);
						if (radComboBoxItem != null)
						{
							radComboBoxItem.Selected = true;
							return radComboBoxItem.Index;
						}
						return -1;
					}
					else
					{
						int num = this.FindByTextInternal(this.Text);
						if (num > -1 && this.MarkFirstMatch)
						{
							this.Items[num].Selected = true;
							return num;
						}
					}
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					if (this.Items.Count != 0)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					value = -1;
				}
				if ((this.Items.Count != 0 && value < this.Items.Count) || value == -1)
				{
					this.InternalClearSelection();
					if (value >= 0)
					{
						this.Items[value].Selected = true;
					}
				}
				else if (this.Items.Count == 0)
				{
					this.cachedSelectedIndex = value;
				}
				this.IsItemSelected = true;
			}
		}

		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x06006518 RID: 25880 RVA: 0x0017BE2C File Offset: 0x0017A02C
		// (set) Token: 0x06006519 RID: 25881 RVA: 0x0017BE57 File Offset: 0x0017A057
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		[ClientControlProperty]
		[ClientPropertyName("offsetX")]
		[Description("Offset along x-axis from child items normal expand positions")]
		public int OffsetX
		{
			get
			{
				if (this.ViewState["OffsetX"] == null)
				{
					return 0;
				}
				return (int)this.ViewState["OffsetX"];
			}
			set
			{
				this.ViewState["OffsetX"] = value;
			}
		}

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x0600651A RID: 25882 RVA: 0x0017BE6F File Offset: 0x0017A06F
		internal bool ReadOnly
		{
			get
			{
				return !this.LoadOnDemandIsEnabled && !this.AllowCustomText && !this.MarkFirstMatch && this.Filter == RadComboBoxFilter.None;
			}
		}

		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x0600651B RID: 25883 RVA: 0x0017BE94 File Offset: 0x0017A094
		// (set) Token: 0x0600651C RID: 25884 RVA: 0x0017BEBF File Offset: 0x0017A0BF
		[DefaultValue(0)]
		[ClientPropertyName("offsetY")]
		[Description("Offset along x-axis from child items normal expand positions")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		public int OffsetY
		{
			get
			{
				if (this.ViewState["OffsetY"] == null)
				{
					return 0;
				}
				return (int)this.ViewState["OffsetY"];
			}
			set
			{
				this.ViewState["OffsetY"] = value;
			}
		}

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x0600651D RID: 25885 RVA: 0x0017BED7 File Offset: 0x0017A0D7
		// (set) Token: 0x0600651E RID: 25886 RVA: 0x0017BEFC File Offset: 0x0017A0FC
		[Category("Layout")]
		[Bindable(true)]
		[DefaultValue(typeof(Unit), "")]
		public Unit DropDownWidth
		{
			get
			{
				return (Unit)(this.ViewState["DropDownWidth"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DropDownWidth"] = value;
			}
		}

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x0600651F RID: 25887 RVA: 0x0017BF31 File Offset: 0x0017A131
		// (set) Token: 0x06006520 RID: 25888 RVA: 0x0017BF52 File Offset: 0x0017A152
		[ClientPropertyName("_dropDownAutoWidth")]
		[Bindable(true)]
		[ClientControlProperty]
		[Description("Gets or sets whether to enable/disable the RadComboBox drop down auto width.")]
		[DefaultValue(RadComboBoxDropDownAutoWidth.Disabled)]
		public RadComboBoxDropDownAutoWidth DropDownAutoWidth
		{
			get
			{
				return (RadComboBoxDropDownAutoWidth)(this.ViewState["DropDownAutoWidth"] ?? RadComboBoxDropDownAutoWidth.Disabled);
			}
			set
			{
				this.ViewState["DropDownAutoWidth"] = value;
			}
		}

		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x06006521 RID: 25889 RVA: 0x0017BF6A File Offset: 0x0017A16A
		// (set) Token: 0x06006522 RID: 25890 RVA: 0x0017BF8A File Offset: 0x0017A18A
		[DefaultValue("")]
		[ClientPersistedProperty]
		[Category("Appearance")]
		[Description("Applied to the Drop Down in addition to the default CSS class.")]
		public virtual string DropDownCssClass
		{
			get
			{
				return (string)(this.ViewState["DropDownCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DropDownCssClass"] = value;
			}
		}

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x06006523 RID: 25891 RVA: 0x0017BF9D File Offset: 0x0017A19D
		// (set) Token: 0x06006524 RID: 25892 RVA: 0x0017BFBD File Offset: 0x0017A1BD
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Applied to the Input in addition to the default CSS class.")]
		public virtual string InputCssClass
		{
			get
			{
				return (string)(this.ViewState["InputCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["InputCssClass"] = value;
			}
		}

		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x06006525 RID: 25893 RVA: 0x0017BFD0 File Offset: 0x0017A1D0
		// (set) Token: 0x06006526 RID: 25894 RVA: 0x0017BFF5 File Offset: 0x0017A1F5
		[Bindable(true)]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit MaxHeight
		{
			get
			{
				return (Unit)(this.ViewState["MaxHeight"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["MaxHeight"] = value;
			}
		}

		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x06006527 RID: 25895 RVA: 0x0017C02C File Offset: 0x0017A22C
		// (set) Token: 0x06006528 RID: 25896 RVA: 0x0017C08C File Offset: 0x0017A28C
		[Bindable(true, BindingDirection.TwoWay)]
		[ClientControlProperty]
		[ClientPropertyName("_value")]
		[Browsable(false)]
		[Themeable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string SelectedValue
		{
			get
			{
				if (!this.CheckBoxes)
				{
					int selectedIndex = this.SelectedIndex;
					if (selectedIndex >= 0)
					{
						return this.Items[selectedIndex].Value;
					}
				}
				if (this.ViewState["cachedSelectedValue"] != null)
				{
					return this.ViewState["cachedSelectedValue"].ToString();
				}
				return string.Empty;
			}
			set
			{
				this.UnselectAllItems();
				this.ViewState["cachedSelectedValue"] = value;
				if (value != null)
				{
					RadComboBoxItem radComboBoxItem = this.FindItemByValue(value);
					if (radComboBoxItem != null)
					{
						radComboBoxItem.Selected = true;
					}
				}
				if (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(this.Text))
				{
					RadComboBoxItem radComboBoxItem2 = this.FindItemByValue(value);
					if (radComboBoxItem2 != null)
					{
						this.Text = radComboBoxItem2.Text;
					}
				}
				this.cachedSelectedValue = value;
			}
		}

		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x06006529 RID: 25897 RVA: 0x0017C0F8 File Offset: 0x0017A2F8
		// (set) Token: 0x0600652A RID: 25898 RVA: 0x0017C100 File Offset: 0x0017A300
		[Browsable(false)]
		[Bindable(false)]
		[TemplateContainer(typeof(RadComboBoxItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual ITemplate HeaderTemplate { get; set; }

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x0600652B RID: 25899 RVA: 0x0017C109 File Offset: 0x0017A309
		// (set) Token: 0x0600652C RID: 25900 RVA: 0x0017C111 File Offset: 0x0017A311
		[TemplateContainer(typeof(RadComboBoxItem))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Bindable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate FooterTemplate { get; set; }

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x0600652D RID: 25901 RVA: 0x0017C11A File Offset: 0x0017A31A
		[Browsable(false)]
		public WebControl Header
		{
			get
			{
				this.EnsureChildControls();
				return this._header;
			}
		}

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x0600652E RID: 25902 RVA: 0x0017C128 File Offset: 0x0017A328
		[Browsable(false)]
		public WebControl Footer
		{
			get
			{
				this.EnsureChildControls();
				return this._footer;
			}
		}

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x0600652F RID: 25903 RVA: 0x0017C136 File Offset: 0x0017A336
		// (set) Token: 0x06006530 RID: 25904 RVA: 0x0017C13E File Offset: 0x0017A33E
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(RadComboBoxItem))]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return base.Template;
			}
			set
			{
				base.Template = value;
			}
		}

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x06006531 RID: 25905 RVA: 0x0017C147 File Offset: 0x0017A347
		[ClientControlProperty]
		[DefaultValue(false)]
		[ClientPropertyName("_isTemplated")]
		internal bool IsTemplated
		{
			get
			{
				return this.ItemTemplate != null;
			}
		}

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x06006532 RID: 25906 RVA: 0x0017C155 File Offset: 0x0017A355
		// (set) Token: 0x06006533 RID: 25907 RVA: 0x0017C175 File Offset: 0x0017A375
		[Description("Gets or sets the template for displaying the items in RadcomboBox")]
		[Browsable(false)]
		[Category("Client")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual string ClientItemTemplate
		{
			get
			{
				return (this.ViewState["ClientItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientItemTemplate"] = value;
			}
		}

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x06006534 RID: 25908 RVA: 0x0017C188 File Offset: 0x0017A388
		// (set) Token: 0x06006535 RID: 25909 RVA: 0x0017C1B1 File Offset: 0x0017A3B1
		[DefaultValue(0)]
		[Category("Behavior")]
		public virtual int MaxLength
		{
			get
			{
				object obj = this.ViewState["MaxLength"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["MaxLength"] = value;
			}
		}

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x06006536 RID: 25910 RVA: 0x0017C1D8 File Offset: 0x0017A3D8
		// (set) Token: 0x06006537 RID: 25911 RVA: 0x0017C1F9 File Offset: 0x0017A3F9
		[Category("Behavior")]
		[Bindable(false)]
		[DefaultValue(true)]
		[Description("Indicates whether the combobox will be visible while loading.")]
		public bool ShowWhileLoading
		{
			get
			{
				return (bool)(this.ViewState["ShowWhileLoading"] ?? true);
			}
			set
			{
				this.ViewState["ShowWhileLoading"] = value;
			}
		}

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x06006538 RID: 25912 RVA: 0x0017C211 File Offset: 0x0017A411
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("The animation played when item is opened")]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x06006539 RID: 25913 RVA: 0x0017C219 File Offset: 0x0017A419
		// (set) Token: 0x0600653A RID: 25914 RVA: 0x0017C23A File Offset: 0x0017A43A
		[Description("Delay in milliseconds between the mouse clicking a input of RadComboBox and its dropdown starting to expand")]
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("expandDelay")]
		public int ExpandDelay
		{
			get
			{
				return (int)(this.ViewState["ExpandDelay"] ?? 0);
			}
			set
			{
				this.ViewState["ExpandDelay"] = value;
			}
		}

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x0600653B RID: 25915 RVA: 0x0017C252 File Offset: 0x0017A452
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("The animation played when item is closed")]
		public AnimationSettings CollapseAnimation
		{
			get
			{
				return this._collapseAnimation;
			}
		}

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x0600653C RID: 25916 RVA: 0x0017C25A File Offset: 0x0017A45A
		// (set) Token: 0x0600653D RID: 25917 RVA: 0x0017C27B File Offset: 0x0017A47B
		[ClientPropertyName("collapseDelay")]
		[DefaultValue(0)]
		[Description("Delay in milliseconds between the mouse clicking the input and the dropdown starting to collapse")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public int CollapseDelay
		{
			get
			{
				return (int)(this.ViewState["CollapseDelay"] ?? 0);
			}
			set
			{
				this.ViewState["CollapseDelay"] = value;
			}
		}

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x0600653E RID: 25918 RVA: 0x0017C293 File Offset: 0x0017A493
		// (set) Token: 0x0600653F RID: 25919 RVA: 0x0017C2B4 File Offset: 0x0017A4B4
		[Category("Effects")]
		[ClientControlProperty]
		[ClientPropertyName("_enableOverlay")]
		[DefaultValue(true)]
		[Description("Sets a value indicating whether the Overlay element is rendered when supported. True by default.")]
		public bool EnableOverlay
		{
			get
			{
				return (bool)(this.ViewState["EnableOverlay"] ?? true);
			}
			set
			{
				this.ViewState["EnableOverlay"] = value;
			}
		}

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x06006540 RID: 25920 RVA: 0x0017C2CC File Offset: 0x0017A4CC
		// (set) Token: 0x06006541 RID: 25921 RVA: 0x0017C2F7 File Offset: 0x0017A4F7
		[Bindable(false)]
		[DefaultValue(RadComboBoxSort.None)]
		[Description("Automatically sorts items alphabetically (based on the Text property) in ascending or descending order")]
		[Category("Setup")]
		public RadComboBoxSort Sort
		{
			get
			{
				if (this.ViewState["Sort"] == null)
				{
					return RadComboBoxSort.None;
				}
				return (RadComboBoxSort)this.ViewState["Sort"];
			}
			set
			{
				this.ViewState["Sort"] = value;
			}
		}

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x06006542 RID: 25922 RVA: 0x0017C30F File Offset: 0x0017A50F
		// (set) Token: 0x06006543 RID: 25923 RVA: 0x0017C33A File Offset: 0x0017A53A
		[Bindable(false)]
		[Category("Setup")]
		[Description("case sensitive")]
		[DefaultValue(true)]
		public bool SortCaseSensitive
		{
			get
			{
				return this.ViewState["SortCaseSensitive"] == null || (bool)this.ViewState["SortCaseSensitive"];
			}
			set
			{
				this.ViewState["SortCaseSensitive"] = value;
			}
		}

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x06006544 RID: 25924 RVA: 0x0017C352 File Offset: 0x0017A552
		// (set) Token: 0x06006545 RID: 25925 RVA: 0x0017C372 File Offset: 0x0017A572
		[Description("The label of the control.")]
		[DefaultValue("")]
		[Category("Appearance")]
		public virtual string Label
		{
			get
			{
				return ((string)this.ViewState["Label"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Label"] = value;
			}
		}

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x06006546 RID: 25926 RVA: 0x0017C385 File Offset: 0x0017A585
		// (set) Token: 0x06006547 RID: 25927 RVA: 0x0017C3A5 File Offset: 0x0017A5A5
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Css class of the label")]
		public virtual string LabelCssClass
		{
			get
			{
				return ((string)this.ViewState["LabelCssClass"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["LabelCssClass"] = value;
			}
		}

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x06006548 RID: 25928 RVA: 0x0017C3B8 File Offset: 0x0017A5B8
		// (set) Token: 0x06006549 RID: 25929 RVA: 0x0017C3D8 File Offset: 0x0017A5D8
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[Category("Misc")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				this.ViewState["Culture"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x0600654A RID: 25930 RVA: 0x0017C3F1 File Offset: 0x0017A5F1
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ComboBoxStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new ComboBoxStrings(new LocalizationProvider("RadComboBox", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x0600654B RID: 25931 RVA: 0x0017C430 File Offset: 0x0017A630
		// (set) Token: 0x0600654C RID: 25932 RVA: 0x0017C450 File Offset: 0x0017A650
		[Description("Gets or sets a value indicating where RadComboBox will look for its .resx localization files.")]
		[Category("Misc")]
		[DefaultValue("")]
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

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x0600654D RID: 25933 RVA: 0x0017C4A3 File Offset: 0x0017A6A3
		// (set) Token: 0x0600654E RID: 25934 RVA: 0x0017C4C4 File Offset: 0x0017A6C4
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("When set to true enables support for WAI-ARIA.")]
		[ClientPropertyName("enableAriaSupport")]
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

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x0600654F RID: 25935 RVA: 0x0017C4DC File Offset: 0x0017A6DC
		// (set) Token: 0x06006550 RID: 25936 RVA: 0x0017C4FD File Offset: 0x0017A6FD
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("_checkBoxes")]
		public bool CheckBoxes
		{
			get
			{
				return (bool)(this.ViewState["CheckBoxes"] ?? false);
			}
			set
			{
				this.ViewState["CheckBoxes"] = value;
			}
		}

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x06006551 RID: 25937 RVA: 0x0017C515 File Offset: 0x0017A715
		// (set) Token: 0x06006552 RID: 25938 RVA: 0x0017C535 File Offset: 0x0017A735
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets a value indicating which datasource field will represent the 'Checked' state of an item checkbox")]
		public string DataCheckedField
		{
			get
			{
				return ((string)this.ViewState["DataCheckedField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataCheckedField"] = value;
			}
		}

		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x06006553 RID: 25939 RVA: 0x0017C548 File Offset: 0x0017A748
		// (set) Token: 0x06006554 RID: 25940 RVA: 0x0017C569 File Offset: 0x0017A769
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("_enableCheckAllItemsCheckBox")]
		public bool EnableCheckAllItemsCheckBox
		{
			get
			{
				return (bool)(this.ViewState["EnableCheckAllItemsCheckBox"] ?? false);
			}
			set
			{
				this.ViewState["EnableCheckAllItemsCheckBox"] = value;
			}
		}

		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x06006555 RID: 25941 RVA: 0x0017C581 File Offset: 0x0017A781
		// (set) Token: 0x06006556 RID: 25942 RVA: 0x0017C5A2 File Offset: 0x0017A7A2
		[Category("Behavior")]
		[DefaultValue(RadComboBoxCheckedItemsTexts.FitInInput)]
		[Description("Gets or sets a value indicating whether the combobox should display the checked items texts in case they do not fit in the control input.")]
		[ClientControlProperty]
		[ClientPropertyName("_checkedItemsTexts")]
		public RadComboBoxCheckedItemsTexts CheckedItemsTexts
		{
			get
			{
				return (RadComboBoxCheckedItemsTexts)(this.ViewState["CheckedItemsTexts"] ?? RadComboBoxCheckedItemsTexts.FitInInput);
			}
			set
			{
				this.ViewState["CheckedItemsTexts"] = value;
			}
		}

		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x06006557 RID: 25943 RVA: 0x0017C5BC File Offset: 0x0017A7BC
		[Browsable(false)]
		public virtual IList<RadComboBoxItem> CheckedItems
		{
			get
			{
				List<RadComboBoxItem> list = new List<RadComboBoxItem>();
				foreach (int index in this.GetCheckedIndices())
				{
					list.Add(this.Items[index]);
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x1700215C RID: 8540
		// (get) Token: 0x06006558 RID: 25944 RVA: 0x0017C600 File Offset: 0x0017A800
		// (set) Token: 0x06006559 RID: 25945 RVA: 0x0017C608 File Offset: 0x0017A808
		[Description("Gets or sets a value indicating whether the ComboBox should render as a <select> element.\nWhen enabled the ComboBox will have its functionality reduced to that of the <select> element.")]
		[Obsolete("This property is obsolete. Use RenderMode.Native instead")]
		[Category("Behavior")]
		[DefaultValue(RadComboBoxRenderingMode.Full)]
		public RadComboBoxRenderingMode RenderingMode
		{
			get
			{
				return this._renderingMode;
			}
			set
			{
				this._renderingMode = value;
				this.RenderMode = ((value == RadComboBoxRenderingMode.Full) ? RenderMode.Classic : RenderMode.Native);
			}
		}

		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x0600655A RID: 25946 RVA: 0x0017C61E File Offset: 0x0017A81E
		// (set) Token: 0x0600655B RID: 25947 RVA: 0x0017C639 File Offset: 0x0017A839
		[Browsable(false)]
		public virtual RadComboBoxDefaultItem DefaultItem
		{
			get
			{
				if (this._defaultItem == null)
				{
					this._defaultItem = new RadComboBoxDefaultItem();
				}
				return this._defaultItem;
			}
			set
			{
				this._defaultItem = value;
			}
		}

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x0600655C RID: 25948 RVA: 0x0017C644 File Offset: 0x0017A844
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the object that controls the Wai-Aria settings applied on the control's element.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[DefaultValue(null)]
		public WaiAriaSettings AriaSettings
		{
			get
			{
				WaiAriaSettings result;
				if ((result = this._ariaSettings) == null)
				{
					result = (this._ariaSettings = new WaiAriaSettings());
				}
				return result;
			}
		}

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x0600655D RID: 25949 RVA: 0x0017C669 File Offset: 0x0017A869
		// (remove) Token: 0x0600655E RID: 25950 RVA: 0x0017C67C File Offset: 0x0017A87C
		public event RadComboBoxItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadComboBox.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadComboBox.ItemCreatedEvent, value);
			}
		}

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x0600655F RID: 25951 RVA: 0x0017C68F File Offset: 0x0017A88F
		// (remove) Token: 0x06006560 RID: 25952 RVA: 0x0017C6A2 File Offset: 0x0017A8A2
		public event RadComboBoxItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadComboBox.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadComboBox.ItemDataBoundEvent, value);
			}
		}

		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06006561 RID: 25953 RVA: 0x0017C6B5 File Offset: 0x0017A8B5
		// (remove) Token: 0x06006562 RID: 25954 RVA: 0x0017C6C8 File Offset: 0x0017A8C8
		public event RadComboBoxItemEventHandler ItemChecked
		{
			add
			{
				base.Events.AddHandler(RadComboBox.ItemCheckedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadComboBox.ItemCheckedEvent, value);
			}
		}

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x06006563 RID: 25955 RVA: 0x0017C6DB File Offset: 0x0017A8DB
		// (remove) Token: 0x06006564 RID: 25956 RVA: 0x0017C6EE File Offset: 0x0017A8EE
		public event RadComboBoxItemEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadComboBox.TemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadComboBox.TemplateNeededEvent, value);
			}
		}

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x06006565 RID: 25957 RVA: 0x0017C701 File Offset: 0x0017A901
		// (remove) Token: 0x06006566 RID: 25958 RVA: 0x0017C714 File Offset: 0x0017A914
		public event RadComboBoxSelectedIndexChangedEventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadComboBox.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadComboBox.SelectedIndexChangedEvent, value);
			}
		}

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06006567 RID: 25959 RVA: 0x0017C727 File Offset: 0x0017A927
		// (remove) Token: 0x06006568 RID: 25960 RVA: 0x0017C73A File Offset: 0x0017A93A
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(RadComboBox.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadComboBox.TextChangedEvent, value);
			}
		}

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06006569 RID: 25961 RVA: 0x0017C74D File Offset: 0x0017A94D
		// (remove) Token: 0x0600656A RID: 25962 RVA: 0x0017C760 File Offset: 0x0017A960
		public event RadComboBoxItemsRequestedEventHandler ItemsRequested
		{
			add
			{
				base.Events.AddHandler(RadComboBox.ItemsRequestedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadComboBox.ItemsRequestedEvent, value);
			}
		}

		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x0600656B RID: 25963 RVA: 0x0017C773 File Offset: 0x0017A973
		// (remove) Token: 0x0600656C RID: 25964 RVA: 0x0017C786 File Offset: 0x0017A986
		public event RadComboBoxCheckAllCheckEventHandler CheckAllCheck
		{
			add
			{
				base.Events.AddHandler(RadComboBox.CheckAllCheckEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadComboBox.CheckAllCheckEvent, value);
			}
		}

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x0600656D RID: 25965 RVA: 0x0017C799 File Offset: 0x0017A999
		// (set) Token: 0x0600656E RID: 25966 RVA: 0x0017C7B9 File Offset: 0x0017A9B9
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		[Description("The client-side event that is fired when the selected index of the combo has changed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("selectedIndexChanging")]
		public string OnClientSelectedIndexChanging
		{
			get
			{
				return (string)(this.ViewState["OnClientSelectedIndexChanging"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientSelectedIndexChanging"] = value;
			}
		}

		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x0600656F RID: 25967 RVA: 0x0017C7CC File Offset: 0x0017A9CC
		// (set) Token: 0x06006570 RID: 25968 RVA: 0x0017C7EC File Offset: 0x0017A9EC
		[Description("The client-side event that is fired after the selected index of the combo has changed.")]
		[Bindable(false)]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("selectedIndexChanged")]
		public string OnClientSelectedIndexChanged
		{
			get
			{
				return (string)(this.ViewState["OnClientSelectedIndexChanged"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientSelectedIndexChanged"] = value;
			}
		}

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x06006571 RID: 25969 RVA: 0x0017C7FF File Offset: 0x0017A9FF
		// (set) Token: 0x06006572 RID: 25970 RVA: 0x0017C81F File Offset: 0x0017AA1F
		[Description("The client-side event that is fired before the items are requested server-side.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemsRequesting")]
		[Bindable(false)]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientItemsRequesting
		{
			get
			{
				return (string)(this.ViewState["OnClientItemsRequesting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemsRequesting"] = value;
			}
		}

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x06006573 RID: 25971 RVA: 0x0017C832 File Offset: 0x0017AA32
		// (set) Token: 0x06006574 RID: 25972 RVA: 0x0017C852 File Offset: 0x0017AA52
		[Description("The name of the javascript function called after the request for items has completed.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemsRequested")]
		[Bindable(false)]
		public string OnClientItemsRequested
		{
			get
			{
				return (string)(this.ViewState["OnClientItemsRequested"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemsRequested"] = value;
			}
		}

		// Token: 0x17002163 RID: 8547
		// (get) Token: 0x06006575 RID: 25973 RVA: 0x0017C865 File Offset: 0x0017AA65
		// (set) Token: 0x06006576 RID: 25974 RVA: 0x0017C885 File Offset: 0x0017AA85
		[Category("Client-side events")]
		[Bindable(false)]
		[DefaultValue("")]
		[Description("The name of the javascript function called after the request for items has failed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemsRequestFailed")]
		public string OnClientItemsRequestFailed
		{
			get
			{
				return (string)(this.ViewState["OnClientItemsRequestFailed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemsRequestFailed"] = value;
			}
		}

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x06006577 RID: 25975 RVA: 0x0017C898 File Offset: 0x0017AA98
		// (set) Token: 0x06006578 RID: 25976 RVA: 0x0017C8B8 File Offset: 0x0017AAB8
		[Description("Gets or sets the name of the JavaScript function called when an Item is created during Web Service Load on Demand")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemDataBound")]
		[Category("Client-side events")]
		public string OnClientItemDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientItemDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemDataBound"] = value;
			}
		}

		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x06006579 RID: 25977 RVA: 0x0017C8CB File Offset: 0x0017AACB
		// (set) Token: 0x0600657A RID: 25978 RVA: 0x0017C8EB File Offset: 0x0017AAEB
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		[Description("The client-side event that is fired when the selected index of the combo has changed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("keyPressing")]
		public string OnClientKeyPressing
		{
			get
			{
				return (string)(this.ViewState["OnClientKeyPressing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientKeyPressing"] = value;
			}
		}

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x0600657B RID: 25979 RVA: 0x0017C8FE File Offset: 0x0017AAFE
		// (set) Token: 0x0600657C RID: 25980 RVA: 0x0017C91E File Offset: 0x0017AB1E
		[Description("The client-side event that is fired when the selected index of the combo has changed.")]
		[Bindable(false)]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("textChange")]
		public string OnClientTextChange
		{
			get
			{
				return (string)(this.ViewState["OnClientTextChange"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTextChange"] = value;
			}
		}

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x0600657D RID: 25981 RVA: 0x0017C931 File Offset: 0x0017AB31
		// (set) Token: 0x0600657E RID: 25982 RVA: 0x0017C951 File Offset: 0x0017AB51
		[DefaultValue("")]
		[Description("The client-side event this is fired when the drop down is being opened.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("dropDownOpening")]
		[Bindable(false)]
		[Category("Client-side events")]
		public string OnClientDropDownOpening
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownOpening"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownOpening"] = value;
			}
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x0600657F RID: 25983 RVA: 0x0017C964 File Offset: 0x0017AB64
		// (set) Token: 0x06006580 RID: 25984 RVA: 0x0017C984 File Offset: 0x0017AB84
		[ClientPropertyName("dropDownOpened")]
		[Category("Client-side events")]
		[Description("The client-side event this is fired when the drop down is being opened.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Bindable(false)]
		public string OnClientDropDownOpened
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownOpened"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownOpened"] = value;
			}
		}

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x06006581 RID: 25985 RVA: 0x0017C997 File Offset: 0x0017AB97
		// (set) Token: 0x06006582 RID: 25986 RVA: 0x0017C9B7 File Offset: 0x0017ABB7
		[Category("Client-side events")]
		[Bindable(false)]
		[DefaultValue("")]
		[Description("The client-side event this is fired when the combo gains focus")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("onClientFocus")]
		public string OnClientFocus
		{
			get
			{
				return (string)(this.ViewState["OnClientFocus"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientFocus"] = value;
			}
		}

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x06006583 RID: 25987 RVA: 0x0017C9CA File Offset: 0x0017ABCA
		// (set) Token: 0x06006584 RID: 25988 RVA: 0x0017C9EA File Offset: 0x0017ABEA
		[ClientPropertyName("onClientBlur")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		[Description("The client-side event this is fired when the combobox loses focus")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientBlur
		{
			get
			{
				return (string)(this.ViewState["OnClientBlur"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientBlur"] = value;
			}
		}

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x06006585 RID: 25989 RVA: 0x0017C9FD File Offset: 0x0017ABFD
		// (set) Token: 0x06006586 RID: 25990 RVA: 0x0017CA1D File Offset: 0x0017AC1D
		[ClientPropertyName("dropDownClosing")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The client-side event that is fired when the drop down is being closed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Bindable(false)]
		public string OnClientDropDownClosing
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownClosing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownClosing"] = value;
			}
		}

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x06006587 RID: 25991 RVA: 0x0017CA30 File Offset: 0x0017AC30
		// (set) Token: 0x06006588 RID: 25992 RVA: 0x0017CA50 File Offset: 0x0017AC50
		[Category("Client-side events")]
		[Bindable(false)]
		[DefaultValue("")]
		[Description("The client-side event that is fired when the drop down is being closed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("dropDownClosed")]
		public string OnClientDropDownClosed
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownClosed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownClosed"] = value;
			}
		}

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x06006589 RID: 25993 RVA: 0x0017CA63 File Offset: 0x0017AC63
		// (set) Token: 0x0600658A RID: 25994 RVA: 0x0017CA83 File Offset: 0x0017AC83
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[Description("The name of the javascript function called after a menu is loaded.")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientLoad
		{
			get
			{
				return (string)(this.ViewState["OnClientLoad"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x0600658B RID: 25995 RVA: 0x0017CA96 File Offset: 0x0017AC96
		[ClientPropertyName("_uniqueId")]
		[ClientControlProperty]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x0600658C RID: 25996 RVA: 0x0017CA9E File Offset: 0x0017AC9E
		// (set) Token: 0x0600658D RID: 25997 RVA: 0x0017CABE File Offset: 0x0017ACBE
		[ClientControlEvent]
		[Bindable(false)]
		[Category("Client-side events")]
		[Description("The client-side event that is fired when a RadComboBox item is about to be checked at client-side.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("itemChecking")]
		public string OnClientItemChecking
		{
			get
			{
				return (string)(this.ViewState["OnClientItemChecking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemChecking"] = value;
			}
		}

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x0600658E RID: 25998 RVA: 0x0017CAD1 File Offset: 0x0017ACD1
		// (set) Token: 0x0600658F RID: 25999 RVA: 0x0017CAF1 File Offset: 0x0017ACF1
		[Category("Client-side events")]
		[Bindable(false)]
		[DefaultValue("")]
		[Description("The client-side event that is fired after item has been checked.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemChecked")]
		public string OnClientItemChecked
		{
			get
			{
				return (string)(this.ViewState["OnClientItemChecked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemChecked"] = value;
			}
		}

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x06006590 RID: 26000 RVA: 0x0017CB04 File Offset: 0x0017AD04
		// (set) Token: 0x06006591 RID: 26001 RVA: 0x0017CB24 File Offset: 0x0017AD24
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("The JavaScript function executed when CheckAll checkbox is checked")]
		[ClientControlEvent]
		[ClientPropertyName("checkAllChecked")]
		[Category("Client-side events")]
		public string OnClientCheckAllChecked
		{
			get
			{
				return (string)(this.ViewState["OnClientCheckAllChecked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientCheckAllChecked"] = value;
			}
		}

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x06006592 RID: 26002 RVA: 0x0017CB37 File Offset: 0x0017AD37
		// (set) Token: 0x06006593 RID: 26003 RVA: 0x0017CB57 File Offset: 0x0017AD57
		[ClientControlEvent]
		[ClientPropertyName("checkAllChecking")]
		[Category("Client-side events")]
		[Description("The JavaScript function executed before CheckAll checkbox is checked")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientCheckAllChecking
		{
			get
			{
				return (string)(this.ViewState["OnClientCheckAllChecking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientCheckAllChecking"] = value;
			}
		}

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x0017CB6A File Offset: 0x0017AD6A
		// (set) Token: 0x06006595 RID: 26005 RVA: 0x0017CB8A File Offset: 0x0017AD8A
		[DefaultValue("")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("templateDataBound")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when the client template for an item is evaluated")]
		public string OnClientTemplateDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientTemplateDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTemplateDataBound"] = value;
			}
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x0017CB9D File Offset: 0x0017AD9D
		public RadComboBoxItem FindItemByText(string text)
		{
			return this.FindItemByText(text, false);
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x0017CBA7 File Offset: 0x0017ADA7
		public RadComboBoxItem FindItemByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadComboBoxItem>(text, ignoreCase);
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x0017CBB1 File Offset: 0x0017ADB1
		public RadComboBoxItem FindItemByValue(string value)
		{
			return this.FindItemByValue(value, false);
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x0017CBBB File Offset: 0x0017ADBB
		public RadComboBoxItem FindItemByValue(string value, bool ignoreCase)
		{
			return this.FindChildByValue<RadComboBoxItem>(value, ignoreCase);
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x0017CBC5 File Offset: 0x0017ADC5
		public int FindItemIndexByText(string text)
		{
			return this.FindItemIndexByText(text, false);
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x0017CBD0 File Offset: 0x0017ADD0
		public int FindItemIndexByText(string text, bool ignoreCase)
		{
			RadComboBoxItem radComboBoxItem = this.FindItemByText(text, ignoreCase);
			if (radComboBoxItem == null)
			{
				return -1;
			}
			return this.Items.IndexOf(radComboBoxItem);
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x0017CBF7 File Offset: 0x0017ADF7
		public int FindItemIndexByValue(string value)
		{
			return this.FindItemIndexByValue(value, false);
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x0017CC04 File Offset: 0x0017AE04
		public int FindItemIndexByValue(string value, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(value))
			{
				return -1;
			}
			RadComboBoxItem radComboBoxItem = this.FindItemByValue(value, ignoreCase);
			if (radComboBoxItem == null)
			{
				return -1;
			}
			return this.Items.IndexOf(radComboBoxItem);
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x0017CC35 File Offset: 0x0017AE35
		public RadComboBoxItem FindItem(Predicate<RadComboBoxItem> match)
		{
			return base.FindChild<RadComboBoxItem>(match);
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x0017CC40 File Offset: 0x0017AE40
		public void LoadContentFile(string fileName)
		{
			string xml = File.ReadAllText(this.Context.Server.MapPath(fileName));
			base.LoadXml(xml);
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x0017CC6B File Offset: 0x0017AE6B
		public void SortItems()
		{
			if (this.Sort != RadComboBoxSort.None)
			{
				this.Items.Sort();
			}
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x0017CC80 File Offset: 0x0017AE80
		public void SortItems(IComparer comparer)
		{
			if (this.Sort != RadComboBoxSort.None)
			{
				this.Items.Sort(comparer);
			}
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x0017CC98 File Offset: 0x0017AE98
		public virtual int[] GetCheckedIndices()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].VisibleInternal && this.Items[i].Checked)
				{
					list.Add(i);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x0017CCF4 File Offset: 0x0017AEF4
		public virtual void ClearCheckedItems()
		{
			foreach (object obj in this.Items)
			{
				RadComboBoxItem radComboBoxItem = (RadComboBoxItem)obj;
				radComboBoxItem.Checked = false;
			}
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x0017CD50 File Offset: 0x0017AF50
		public override void Focus()
		{
			this._shouldFocus = true;
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x0017CD5C File Offset: 0x0017AF5C
		// Note: this type is marked as 'beforefieldinit'.
		static RadComboBox()
		{
			RadComboBox.ItemDataBoundEvent = new object();
			RadComboBox.ItemCreatedEvent = new object();
			RadComboBox.TextChangedEvent = new object();
			RadComboBox.SelectedIndexChangedEvent = new object();
			RadComboBox.ItemsRequestedEvent = new object();
			RadComboBox.ItemCheckedEvent = new object();
			RadComboBox.CheckAllCheckEvent = new object();
			RadComboBox.TemplateNeededEvent = new object();
		}

		// Token: 0x0400185D RID: 6237
		internal const string NoMatchesMessage = "No matches";

		// Token: 0x0400185E RID: 6238
		internal const string ShowMoreResultsMessageTemplate = "Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>";

		// Token: 0x0400185F RID: 6239
		internal const string AllItemsCheckedString = "";

		// Token: 0x04001860 RID: 6240
		internal const string ItemsCheckedString = "";

		// Token: 0x04001861 RID: 6241
		internal const string OptionTagName = "option";

		// Token: 0x04001862 RID: 6242
		internal const string RightBracket = ">";

		// Token: 0x04001863 RID: 6243
		internal const string WhiteSpace = " ";

		// Token: 0x04001864 RID: 6244
		internal const string SelectText = "select";

		// Token: 0x0400186D RID: 6253
		private string _oldText = string.Empty;

		// Token: 0x0400186E RID: 6254
		private string _oldValue = string.Empty;

		// Token: 0x0400186F RID: 6255
		private bool _endOfItems;

		// Token: 0x04001870 RID: 6256
		private string _showMoreResultsMessage;

		// Token: 0x04001871 RID: 6257
		private string _filterText = string.Empty;

		// Token: 0x04001872 RID: 6258
		private string _callbackText = string.Empty;

		// Token: 0x04001873 RID: 6259
		private int _numberOfItems;

		// Token: 0x04001874 RID: 6260
		private ComboBoxStrings _localization;

		// Token: 0x04001875 RID: 6261
		private readonly AnimationSettings _expandAnimation;

		// Token: 0x04001876 RID: 6262
		private readonly AnimationSettings _collapseAnimation;

		// Token: 0x04001877 RID: 6263
		private readonly NavigationControlWebServiceSettings _webServiceSettings;

		// Token: 0x04001878 RID: 6264
		internal bool _checkedItemsTextOverflows;

		// Token: 0x04001879 RID: 6265
		private int cachedSelectedIndex = -1;

		// Token: 0x0400187A RID: 6266
		private string cachedSelectedValue;

		// Token: 0x0400187B RID: 6267
		private RadComboBoxHeaderFooterControl _header;

		// Token: 0x0400187C RID: 6268
		private RadComboBoxHeaderFooterControl _footer;

		// Token: 0x0400187D RID: 6269
		private RadComboBoxDefaultItem _defaultItem;

		// Token: 0x0400187E RID: 6270
		private bool _isDefaultItemAdded;

		// Token: 0x0400187F RID: 6271
		private bool _shouldFocus;

		// Token: 0x04001880 RID: 6272
		private IList<ClientOperation<RadComboBoxItem>> _clientChanges = new List<ClientOperation<RadComboBoxItem>>();

		// Token: 0x04001881 RID: 6273
		private RadComboBoxRenderingMode _renderingMode;

		// Token: 0x04001882 RID: 6274
		private WaiAriaSettings _ariaSettings;
	}
}
