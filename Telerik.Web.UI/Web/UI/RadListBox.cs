using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.ListBox.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x0200039F RID: 927
	[EmbeddedSkin("ListBox", "Default", typeof(RadListBox))]
	[RequiredScript(typeof(jQueryPlugins), 1)]
	[ControlValueProperty("SelectedValue")]
	[RequiredScript(typeof(TouchScrollExtender), 2)]
	[TelerikToolboxCategory("Data")]
	[LightweightRendering]
	[ValidationProperty("SelectedValue")]
	[ToolboxBitmap(typeof(RadListBox), "Telerik.Web.UI.ListBox.png")]
	[ToolboxData("<{0}:RadListBox runat=\"server\"></{0}:RadListBox>")]
	[RequiredScript(typeof(OData), 3)]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadListBox", "Telerik.Web.UI.ListBox.RadListBoxScripts.js")]
	[XmlRoot("Items")]
	[DefaultProperty("Items")]
	[DefaultEvent("SelectedIndexChanged")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadListBox))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[Designer("Telerik.Web.Design.RadListBoxDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[EmbeddedSkin("ListBox", typeof(RadListBox))]
	public class RadListBox : ControlItemContainer, ICallbackEventHandler, IPostBackEventHandler, ILocalizableControl, IFlatBoundContainer
	{
		// Token: 0x060021B3 RID: 8627 RVA: 0x000714A0 File Offset: 0x0006F6A0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "allowDelete", this.AllowDelete, false);
			base.DescribeProperty<bool>(descriptor, "allowReorder", this.AllowReorder, false);
			base.DescribeProperty<bool>(descriptor, "allowTransfer", this.AllowTransfer, false);
			base.DescribeProperty<bool>(descriptor, "allowTransferDuplicates", this.AllowTransferDuplicates, false);
			base.DescribeProperty<bool>(descriptor, "_allowTransferOnDblClick", this.AllowTransferOnDoubleClick, false);
			base.DescribeProperty<bool>(descriptor, "autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<bool>(descriptor, "autoPostBackOnDelete", this.AutoPostBackOnDelete, false);
			base.DescribeProperty<bool>(descriptor, "autoPostBackOnReorder", this.AutoPostBackOnReorder, false);
			base.DescribeProperty<bool>(descriptor, "autoPostBackOnTransfer", this.AutoPostBackOnTransfer, false);
			base.DescribeProperty<bool>(descriptor, "_checkBoxes", this.CheckBoxes, false);
			base.DescribeProperty<bool>(descriptor, "enableDragAndDrop", this.EnableDragAndDrop, false);
			base.DescribeProperty<string>(descriptor, "_loadingPanelID", this.LoadingPanelID, "");
			base.DescribeProperty<bool>(descriptor, "_persistChanges", this.PersistClientChanges, true);
			base.DescribeProperty<int>(descriptor, "_scrollPosition", this.ScrollPosition, 0);
			base.DescribeProperty<string>(descriptor, "_selectedValue", this.SelectedValue, "");
			base.DescribeProperty<ListBoxSelectionMode>(descriptor, "selectionMode", this.SelectionMode, ListBoxSelectionMode.Single);
			base.DescribeProperty<bool>(descriptor, "_showCheckAll", this.ShowCheckAll, false);
			base.DescribeProperty<ListBoxTransferMode>(descriptor, "transferMode", this.TransferMode, ListBoxTransferMode.Move);
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x00071628 File Offset: 0x0006F828
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "checkAllChecked", this.OnClientCheckAllChecked);
			RadDataBoundControl.DescribeEvent(descriptor, "checkAllChecking", this.OnClientCheckAllChecking);
			RadDataBoundControl.DescribeEvent(descriptor, "contextMenu", this.OnClientContextMenu);
			RadDataBoundControl.DescribeEvent(descriptor, "deleted", this.OnClientDeleted);
			RadDataBoundControl.DescribeEvent(descriptor, "deleting", this.OnClientDeleting);
			RadDataBoundControl.DescribeEvent(descriptor, "dragging", this.OnClientDragging);
			RadDataBoundControl.DescribeEvent(descriptor, "dragStart", this.OnClientDragStart);
			RadDataBoundControl.DescribeEvent(descriptor, "dropped", this.OnClientDropped);
			RadDataBoundControl.DescribeEvent(descriptor, "dropping", this.OnClientDropping);
			RadDataBoundControl.DescribeEvent(descriptor, "itemChecked", this.OnClientItemChecked);
			RadDataBoundControl.DescribeEvent(descriptor, "itemChecking", this.OnClientItemChecking);
			RadDataBoundControl.DescribeEvent(descriptor, "itemDataBound", this.OnClientItemDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "itemDoubleClicked", this.OnClientItemDoubleClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "itemDoubleClicking", this.OnClientItemDoubleClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequested", this.OnClientItemsRequested);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequestFailed", this.OnClientItemsRequestFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequesting", this.OnClientItemsRequesting);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOut", this.OnClientMouseOut);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOver", this.OnClientMouseOver);
			RadDataBoundControl.DescribeEvent(descriptor, "reordered", this.OnClientReordered);
			RadDataBoundControl.DescribeEvent(descriptor, "reordering", this.OnClientReordering);
			RadDataBoundControl.DescribeEvent(descriptor, "selectedIndexChanged", this.OnClientSelectedIndexChanged);
			RadDataBoundControl.DescribeEvent(descriptor, "selectedIndexChanging", this.OnClientSelectedIndexChanging);
			RadDataBoundControl.DescribeEvent(descriptor, "templateDataBound", this.OnClientTemplateDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "transferred", this.OnClientTransferred);
			RadDataBoundControl.DescribeEvent(descriptor, "transferring", this.OnClientTransferring);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x00071807 File Offset: 0x0006FA07
		internal bool HasHeaderTemplate
		{
			get
			{
				return this.HeaderTemplate != null || this.Header.Controls.Count > 0;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x00071826 File Offset: 0x0006FA26
		internal bool HasFooterTemplate
		{
			get
			{
				return this.FooterTemplate != null || this.Footer.Controls.Count > 0;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x00071845 File Offset: 0x0006FA45
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x0007184D File Offset: 0x0006FA4D
		internal int DataSourceItemsCount { get; set; }

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x00071856 File Offset: 0x0006FA56
		internal override bool SupportsOData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x00071859 File Offset: 0x0006FA59
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x0007185C File Offset: 0x0006FA5C
		internal bool IsControlEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x00071864 File Offset: 0x0006FA64
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x00071867 File Offset: 0x0006FA67
		internal HttpContext ControlContext
		{
			get
			{
				return this.Context;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x0007186F File Offset: 0x0006FA6F
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x00071877 File Offset: 0x0006FA77
		[ClientPropertyName("_scrollPosition")]
		[ClientControlProperty]
		internal int ScrollPosition { get; set; }

		// Token: 0x060021C0 RID: 8640 RVA: 0x00071880 File Offset: 0x0006FA80
		public RadListBox()
		{
			this._webServiceSettings = new NavigationControlWebServiceSettings(this.ViewState);
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000718B6 File Offset: 0x0006FAB6
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnItemDataBound(new RadListBoxItemEventArgs((RadListBoxItem)item));
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000718C9 File Offset: 0x0006FAC9
		protected override void RaiseItemCreated(ControlItem item)
		{
			this.OnItemCreated(new RadListBoxItemEventArgs((RadListBoxItem)item));
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000718DC File Offset: 0x0006FADC
		protected override void RaiseTemplateNeeded(ControlItem item)
		{
			this.OnTemplateNeeded(new RadListBoxItemEventArgs((RadListBoxItem)item));
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000718EF File Offset: 0x0006FAEF
		protected internal override ControlItem CreateItem()
		{
			return new RadListBoxItem();
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000718F6 File Offset: 0x0006FAF6
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadListBoxItemCollection(this);
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x00071900 File Offset: 0x0006FB00
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.ScriptManager.LoadScriptsBeforeUI)
			{
				return;
			}
			string text = string.Format("Telerik.Web.UI.RadListBox._preInitialize(\"{0}\",\"{1}\");", this.ClientID, this.ScrollPosition);
			ScriptManager.RegisterStartupScript(this.Page, typeof(RadListBox), this.ClientID + text, text, true);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x00071961 File Offset: 0x0006FB61
		protected internal override IRenderer CreateControlRenderer()
		{
			return RendererFactory.CreateListBoxRenderer(this);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x00071969 File Offset: 0x0006FB69
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x00071972 File Offset: 0x0006FB72
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x00071986 File Offset: 0x0006FB86
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00071993 File Offset: 0x0006FB93
		protected override void CreateChildControls()
		{
			this.CreateHeader();
			if (this.HeaderTemplate != null)
			{
				RadListBox.ApplyTemplate(this.Header, this.HeaderTemplate);
			}
			this.CreateFooter();
			if (this.FooterTemplate != null)
			{
				RadListBox.ApplyTemplate(this.Footer, this.FooterTemplate);
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000719D4 File Offset: 0x0006FBD4
		private static void ApplyTemplate(WebControl control, ITemplate template)
		{
			DefaultHeaderFooterControl defaultHeaderFooterControl = control as DefaultHeaderFooterControl;
			if (defaultHeaderFooterControl.TemplateInstantiated)
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
			defaultHeaderFooterControl.TemplateInstantiated = true;
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x00071A2F File Offset: 0x0006FC2F
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x00071A40 File Offset: 0x0006FC40
		private void CreateFooter()
		{
			if (this._footer == null)
			{
				this._footer = new DefaultHeaderFooterControl();
				this.Controls.Add(this.Footer);
				this.Footer.ID = "Footer";
				this.Footer.CssClass = "rlbFooter";
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x00071A94 File Offset: 0x0006FC94
		private void CreateHeader()
		{
			if (this._header == null)
			{
				this._header = new DefaultHeaderFooterControl();
				this.Controls.Add(this.Header);
				this.Header.ID = "Header";
				this.Header.CssClass = "rlbHeader";
			}
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x00071AE8 File Offset: 0x0006FCE8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			Color borderColor = this.BorderColor;
			this.BorderColor = Color.Empty;
			base.AddAttributesToRender(writer);
			this.TabIndex = tabIndex;
			this.AccessKey = accessKey;
			this.BorderColor = borderColor;
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x00071B44 File Offset: 0x0006FD44
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureChildControls();
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			RadListBoxClientState radListBoxClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				radListBoxClientState = javaScriptSerializer.Deserialize<RadListBoxClientState>(text);
				if (base.IsEnabled)
				{
					this.Enabled = radListBoxClientState.IsEnabled;
				}
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (!this.Enabled)
			{
				return false;
			}
			if (radListBoxClientState == null)
			{
				return false;
			}
			this.LoadClientState(radListBoxClientState);
			if (radListBoxClientState.SelectedIndices == null || radListBoxClientState.SelectedIndices.Length == 0)
			{
				if (this.SelectedIndex != -1)
				{
					this.SetPostDataSelection(-1);
					return true;
				}
				return false;
			}
			else if (this.SelectionMode == ListBoxSelectionMode.Single)
			{
				int num = radListBoxClientState.SelectedIndices[0];
				if (num != this.SelectedIndex)
				{
					this.SetPostDataSelection(num);
					return true;
				}
				return false;
			}
			else
			{
				int[] selectedIndices = this.GetSelectedIndices();
				int[] array = new int[radListBoxClientState.SelectedIndices.Length];
				for (int i = 0; i < radListBoxClientState.SelectedIndices.Length; i++)
				{
					int num2 = radListBoxClientState.SelectedIndices[i];
					array[i] = num2;
				}
				bool flag = false;
				if (array.Length == selectedIndices.Length)
				{
					for (int j = 0; j < radListBoxClientState.SelectedIndices.Length; j++)
					{
						if (array[j] != selectedIndices[j])
						{
							flag = true;
							break;
						}
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					this.SelectInternal(array);
					return true;
				}
				return false;
			}
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x00071CA0 File Offset: 0x0006FEA0
		private void LoadCheckedIndices(RadListBoxClientState clientState)
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

		// Token: 0x060021D3 RID: 8659 RVA: 0x00071CFF File Offset: 0x0006FEFF
		private void LoadClientState(RadListBoxClientState clientState)
		{
			if (clientState.LogEntries != null)
			{
				this.LoadLogEntries(clientState);
			}
			if (clientState.CheckedIndices != null)
			{
				this.LoadCheckedIndices(clientState);
			}
			this.ScrollPosition = clientState.ScrollPosition;
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00071D2C File Offset: 0x0006FF2C
		private void LoadLogEntries(RadListBoxClientState clientState)
		{
			ClientStateLogPlayer<RadListBoxItem> clientStateLogPlayer = new ClientStateLogPlayer<RadListBoxItem>(this);
			this._clientChanges = clientStateLogPlayer.Play(clientState.LogEntries);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00071D52 File Offset: 0x0006FF52
		protected override void RaisePostDataChangedEvent()
		{
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x00071D60 File Offset: 0x0006FF60
		private void RaiseItemEvent(object eventKey, RadListBoxItemEventArgs e)
		{
			RadListBoxItemEventHandler radListBoxItemEventHandler = (RadListBoxItemEventHandler)base.Events[eventKey];
			if (radListBoxItemEventHandler != null)
			{
				radListBoxItemEventHandler(this, e);
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00071D8C File Offset: 0x0006FF8C
		private void RaiseEvent(object eventKey, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[eventKey];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00071DB6 File Offset: 0x0006FFB6
		protected void SetPostDataSelection(int selectedIndex)
		{
			if (this.Items.Count != 0 && selectedIndex < this.Items.Count)
			{
				this.ClearSelection();
				if (selectedIndex >= 0)
				{
					this.Items[selectedIndex].Selected = true;
				}
			}
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x00071DEF File Offset: 0x0006FFEF
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00071E08 File Offset: 0x00070008
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			ListBoxPostBackCommand listBoxPostBackCommand = null;
			try
			{
				listBoxPostBackCommand = new JavaScriptSerializer().Deserialize<ListBoxPostBackCommand>(eventArgument);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (listBoxPostBackCommand == null)
			{
				return;
			}
			switch (listBoxPostBackCommand.Type)
			{
			case ListBoxCommand.Reorder:
				this.OnReorder(listBoxPostBackCommand);
				return;
			case ListBoxCommand.Transfer:
				this.OnTransfer(listBoxPostBackCommand, (RadListBox listBox) => listBox.SelectedItems);
				return;
			case ListBoxCommand.TransferAll:
				this.OnTransfer(listBoxPostBackCommand, (RadListBox listBox) => listBox.Items);
				return;
			case ListBoxCommand.Delete:
				this.OnDelete(this.SelectedItems);
				return;
			case ListBoxCommand.Drop:
				this.OnDrop(listBoxPostBackCommand);
				return;
			case ListBoxCommand.SelectedIndexChanged:
				break;
			case ListBoxCommand.ItemChecked:
				this.OnItemCheck(new RadListBoxItemEventArgs(this.Items[listBoxPostBackCommand.ItemIndex]));
				return;
			case ListBoxCommand.CheckAllCheck:
				this.OnCheckAllCheck(new RadListBoxCheckAllCheckEventArgs(listBoxPostBackCommand.CheckAllChecked));
				break;
			default:
				return;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x00071F10 File Offset: 0x00070110
		internal bool RequiresPostBack
		{
			get
			{
				return this.AutoPostBack || this.AutoPostBackOnDelete || this.AutoPostBackOnReorder || this.AutoPostBackOnTransfer || this.AutoPostBackOnDrop;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x00071F3A File Offset: 0x0007013A
		private List<object> DataKeysArrayList
		{
			get
			{
				if (this.ViewState["DataKeysArrayList"] == null)
				{
					this.ViewState["DataKeysArrayList"] = new List<object>();
				}
				return (List<object>)this.ViewState["DataKeysArrayList"];
			}
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00071F78 File Offset: 0x00070178
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptConverter[] converters = new JavaScriptConverter[]
			{
				new ListBoxItemConverter(),
				new AttributeCollectionConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			javaScriptSerializer.RegisterConverters(converters);
			base.DescribeRenderingMode(descriptor);
			descriptor.AddScriptProperty("itemData", javaScriptSerializer.Serialize(this.Items.VisibleItems));
			descriptor.AddScriptProperty("selectedIndices", javaScriptSerializer.Serialize(this.GetSelectedIndices()));
			descriptor.AddScriptProperty("checkedIndices", javaScriptSerializer.Serialize(this.GetCheckedIndices()));
			if (this.EnableLoadOnDemand)
			{
				descriptor.AddProperty("_totalItemsCount", this.DataSourceItemsCount);
			}
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (this.HasHeaderTemplate || this.HasFooterTemplate)
			{
				descriptor.AddProperty("_hasHeaderFooter", true);
			}
			if (this.RequiresPostBack)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			if (base.Events[RadListBox.ItemCheckEvent] != null && this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackOnCheck", true);
			}
			if (base.Events[RadListBox.CheckAllCheckEvent] != null && this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackOnCheckAllCheck", true);
			}
			if (this.AutoPostBackOnDrop)
			{
				descriptor.AddProperty("autoPostBackOnDrop", true);
			}
			if (this.TransferToListBox != null)
			{
				descriptor.AddComponentProperty("transferTo", this.TransferToListBox.ClientID);
			}
			if (this.ItemTemplate != null)
			{
				descriptor.AddProperty("_isTemplated", true);
			}
			if (this.EnableMarkMatches)
			{
				descriptor.AddProperty("_enableMarkMatches", true);
			}
			if (this.EnableLoadOnDemand)
			{
				descriptor.AddProperty("_enableLoadOnDemand", true);
			}
			if (this.EnableLoadOnDemand || base.Events[RadListBox.ItemsRequestedEvent] != null)
			{
				this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			}
			if (!string.IsNullOrEmpty(this.ClientItemTemplate))
			{
				descriptor.AddProperty("_clientTemplate", this.ClientItemTemplate);
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				descriptor.AddProperty("_dataKeyField", this.DataKeyField);
			}
			this.WebServiceSettings.Describe("webServiceSettings", javaScriptSerializer, descriptor);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000721E0 File Offset: 0x000703E0
		internal void Describe(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000721EC File Offset: 0x000703EC
		private static int Count(IEnumerable enumerable)
		{
			int num = 0;
			if (enumerable == null)
			{
				return num;
			}
			IEnumerator enumerator = enumerable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				num++;
			}
			return num;
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00072218 File Offset: 0x00070418
		internal IList<Dictionary<string, object>> ExtractDataItemValuesInRange(int start, int end, IEnumerable dataSource)
		{
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			int num = -1;
			foreach (object dataItem in dataSource)
			{
				num++;
				if (num >= start)
				{
					if (num > end)
					{
						break;
					}
					list.Add(this.DataItemToDictionary(dataItem));
				}
			}
			return list;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00072288 File Offset: 0x00070488
		internal Dictionary<string, object> DataItemToDictionary(object dataItem)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				dictionary[propertyDescriptor.Name] = propertyDescriptor.GetValue(dataItem);
			}
			return dictionary;
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0007231C File Offset: 0x0007051C
		private IEnumerable Select()
		{
			DataSourceView data2 = this.GetData();
			IEnumerable dataSource = null;
			AutoResetEvent selectAutoResetEvent = new AutoResetEvent(false);
			data2.Select(base.SelectArguments, delegate(IEnumerable data)
			{
				dataSource = data;
				selectAutoResetEvent.Set();
			});
			selectAutoResetEvent.WaitOne();
			return dataSource;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x00072373 File Offset: 0x00070573
		internal IEnumerable GetCurrentDataSource()
		{
			if (this.currentDataSource == null)
			{
				this.currentDataSource = this.Select();
			}
			return this.currentDataSource;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x00072390 File Offset: 0x00070590
		private IEnumerable GetPage(IEnumerable data, int startIndex, int count)
		{
			ListViewInMemoryEnumerableHelper listViewInMemoryEnumerableHelper = new ListViewInMemoryEnumerableHelper();
			return listViewInMemoryEnumerableHelper.GetPage(data, startIndex, count);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0007241C File Offset: 0x0007061C
		private void RetrieveData()
		{
			DataSourceView data2 = this.GetData();
			DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
			if (this.EnableLoadOnDemand && this.Page != null && !this.Page.IsCallback)
			{
				data2.Select(DataSourceSelectArguments.Empty, delegate(IEnumerable data)
				{
					IBindingList bindingList = data as IBindingList;
					IQueryable queryable = data as IQueryable;
					if (bindingList != null)
					{
						this.DataSourceItemsCount = bindingList.Count;
						return;
					}
					if (queryable != null)
					{
						this.DataSourceItemsCount = queryable.Count();
						return;
					}
					this.DataSourceItemsCount = RadListBox.Count(data);
				});
				return;
			}
			if (!this.EnableLoadOnDemand || this.Page == null || !this.Page.IsCallback)
			{
				data2.Select(dataSourceSelectArguments, new DataSourceViewSelectCallback(this.PerformDataBinding));
				return;
			}
			if (data2.CanPage)
			{
				dataSourceSelectArguments.StartRowIndex = this._callbackArgument.StartIndex;
				dataSourceSelectArguments.MaximumRows = this._callbackArgument.Count;
				data2.Select(dataSourceSelectArguments, new DataSourceViewSelectCallback(this.PerformDataBinding));
				return;
			}
			data2.Select(dataSourceSelectArguments, delegate(IEnumerable data)
			{
				data = this.GetPage(data, this._callbackArgument.StartIndex, this._callbackArgument.Count);
				this.PerformDataBinding(data);
			});
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000724FD File Offset: 0x000706FD
		protected override void PerformSelect()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.RetrieveData();
			base.RequiresDataBinding = false;
			if (!this.EnableLoadOnDemand)
			{
				base.MarkAsDataBound();
			}
			this.OnDataBound(EventArgs.Empty);
			this.currentDataSource = null;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00072537 File Offset: 0x00070737
		private void ValidateSelection(int index)
		{
			if (index == -1)
			{
				throw new ArgumentOutOfRangeException("index", "Selection out of range");
			}
			if (this._cachedSelectedIndex != -1 && this._cachedSelectedIndex != index)
			{
				throw new ArgumentException("SelectedIndex and SelectedValue are mutually exclusive", "index");
			}
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x00072570 File Offset: 0x00070770
		protected override void PerformDataBinding(IEnumerable data)
		{
			this.DataKeysArrayList.Clear();
			base.PerformDataBinding(data);
			if (!string.IsNullOrEmpty(this._cachedSelectedValue))
			{
				int num = this.FindItemIndexByValue(this._cachedSelectedValue);
				this.ValidateSelection(num);
				this.SelectedIndex = num;
				this._cachedSelectedIndex = -1;
				this._cachedSelectedValue = null;
			}
			else if (this._cachedSelectedIndex != -1)
			{
				this.SelectedIndex = this._cachedSelectedIndex;
				this._cachedSelectedIndex = -1;
			}
			if (this._selectedItemKeys.Count < 1)
			{
				return;
			}
			foreach (object item in this._selectedItemKeys)
			{
				int num2 = this.DataKeysArrayList.IndexOf(item);
				if (num2 > -1)
				{
					this.Items[num2].Selected = true;
				}
			}
			this._selectedItemKeys.Clear();
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x00072660 File Offset: 0x00070860
		internal void AddDataKey(object dataKey)
		{
			this.DataKeysArrayList.Add(dataKey);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00072670 File Offset: 0x00070870
		private void DeserializeCallbackArgument(string argument)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			this._callbackArgument = javaScriptSerializer.Deserialize<ListBoxCallbackArgument>(argument);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00072690 File Offset: 0x00070890
		private string GetResults(StringWriter output, List<RadListBoxItem> itemsToSerialize, JavaScriptSerializer serializer)
		{
			foreach (object obj in this.Items)
			{
				RadListBoxItem radListBoxItem = (RadListBoxItem)obj;
				radListBoxItem.RenderControl(new HtmlTextWriter(output));
				itemsToSerialize.Add(radListBoxItem);
			}
			string arg = serializer.Serialize(itemsToSerialize);
			return string.Format("{0}{1}{2}", arg, "_$$_", output.ToString());
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00072714 File Offset: 0x00070914
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)base.Children).SaveViewState(),
				this.GetSelectedIndices()
			};
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x00072754 File Offset: 0x00070954
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			ArrayList arrayList = (ArrayList)savedState;
			base.LoadViewState(arrayList[0]);
			((IStateManager)this.Items).LoadViewState(arrayList[1]);
			int[] array = (int[])arrayList[2];
			if (array != null)
			{
				this.SelectInternal(array);
			}
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000727A4 File Offset: 0x000709A4
		internal void SelectInternal(int[] indeces)
		{
			this.ClearSelection();
			foreach (int num in indeces)
			{
				if (num >= 0 && num < this.Items.Count)
				{
					this.Items[num].Selected = true;
				}
			}
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000727F0 File Offset: 0x000709F0
		internal void CheckInternal(int[] indeces)
		{
			this.ClearChecked();
			for (int num = 0; num != indeces.Length; num++)
			{
				int num2 = indeces[num];
				if (num2 >= 0 && num2 < this.Items.Count)
				{
					this.Items[num2].Checked = true;
				}
			}
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0007283C File Offset: 0x00070A3C
		private void OnReorder(ListBoxPostBackCommand command)
		{
			if (command.NumberOfItems > 0)
			{
				this.Reorder(this.GetLastNItems(command.NumberOfItems), command.Offset);
				return;
			}
			if (command.Offset != 0)
			{
				this.Reorder(this.SelectedItems, command.Offset);
				return;
			}
			List<RadListBoxItem> list = new List<RadListBoxItem>(this.SelectedItems);
			int destinationIndex = command.DestinationIndex;
			if (list.Count > 1)
			{
				RadListBoxItem radListBoxItem = list[0];
				RadListBoxItem radListBoxItem2 = list[list.Count - 1];
				if (radListBoxItem.Index + 1 == destinationIndex && command.DropPosition == ListBoxDropPosition.Above)
				{
					list.Remove(radListBoxItem);
				}
				else if (radListBoxItem2.Index - 1 == destinationIndex && command.DropPosition == ListBoxDropPosition.Below)
				{
					list.Remove(radListBoxItem2);
				}
			}
			this.ReorderToIndex(list, destinationIndex);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000728FC File Offset: 0x00070AFC
		private IList<RadListBoxItem> GetLastNItems(int numberOfItems)
		{
			List<RadListBoxItem> list = new List<RadListBoxItem>(numberOfItems);
			while (numberOfItems > 0)
			{
				list.Add(this.Items[this.Items.Count - numberOfItems]);
				numberOfItems--;
			}
			return list;
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0007293A File Offset: 0x00070B3A
		private void PerformReorder(IList<RadListBoxItem> items, int offset)
		{
			this.PerformReorder(items, offset, -1);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x00072948 File Offset: 0x00070B48
		private void PerformReorder(IList<RadListBoxItem> items, int offset, int targetIndex)
		{
			IList<RadListBoxItem> items2 = this.ItemsToUpdate(items, offset, targetIndex);
			this.OnUpdating(new RadListBoxUpdatingEventArgs(items2));
			foreach (RadListBoxItem radListBoxItem in items)
			{
				if (base.IsBoundUsingDataSourceID && radListBoxItem.DataKey != null && radListBoxItem.Selected)
				{
					this._selectedItemKeys.Add(radListBoxItem.DataKey);
				}
				int newIndex = (offset == 0) ? targetIndex : (radListBoxItem.Index + offset);
				if (base.IsBoundUsingDataSourceID && this.AllowAutomaticUpdates)
				{
					this.ReorderDataSource(radListBoxItem.Index, newIndex, this.GetCurrentDataSource(), this.GetData());
				}
				else
				{
					RadListBox.Swap<object>(this.DataKeysArrayList, radListBoxItem.Index, newIndex);
					RadListBox.Swap<RadListBoxItem>(this.Items, radListBoxItem.Index, newIndex);
					this._dataKeyArray = null;
				}
			}
			this.OnUpdated(new RadListBoxEventArgs(items2));
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x00072A6C File Offset: 0x00070C6C
		private IList<RadListBoxItem> ItemsToUpdate(IList<RadListBoxItem> items, int offset, int targetIndex)
		{
			int max = items[0].Index;
			int min = max;
			foreach (RadListBoxItem radListBoxItem in items)
			{
				if (radListBoxItem.Index > max)
				{
					max = radListBoxItem.Index;
				}
				if (radListBoxItem.Index < min)
				{
					min = radListBoxItem.Index;
				}
			}
			if (offset == 0)
			{
				if (targetIndex > max)
				{
					offset = targetIndex - max;
				}
				else
				{
					offset = targetIndex - min;
				}
			}
			if (offset > 0)
			{
				max += offset;
			}
			else
			{
				min += offset;
			}
			return this.Items.FindAll((RadListBoxItem i) => i.Index >= min && i.Index <= max);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00072B60 File Offset: 0x00070D60
		private static void Swap<T>(IList<T> list, int oldIndex, int newIndex)
		{
			if (list.Count < 1)
			{
				return;
			}
			T item = list[oldIndex];
			list.RemoveAt(oldIndex);
			list.Insert(newIndex, item);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00072BA0 File Offset: 0x00070DA0
		internal void ReorderDataSource(int oldIndex, int newIndex, IEnumerable dataSource, DataSourceView dataSourceView)
		{
			if (dataSource == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.DataKeyField))
			{
				throw new NotSupportedException("DataKeyField is empty");
			}
			if (string.IsNullOrEmpty(this.DataSortField))
			{
				throw new NotSupportedException("DataSortField is empty");
			}
			int num = Math.Min(oldIndex, newIndex);
			int end = Math.Max(oldIndex, newIndex);
			IList<Dictionary<string, object>> list = this.ExtractDataItemValuesInRange(num, end, dataSource);
			oldIndex -= num;
			newIndex -= num;
			RadListBox.Swap<Dictionary<string, object>>(list, oldIndex, newIndex);
			int num2 = this.GetStartOrder(list);
			foreach (Dictionary<string, object> dictionary in list)
			{
				Dictionary<string, object> keys = new Dictionary<string, object>
				{
					{
						this.DataKeyField,
						dictionary[this.DataKeyField]
					}
				};
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>(dictionary);
				dictionary2[this.DataSortField] = num2;
				dictionary2.Remove(this.DataKeyField);
				dictionary.Remove(this.DataKeyField);
				dataSourceView.Update(keys, dictionary2, dictionary, delegate(int affectedRows, Exception error)
				{
					if (error != null)
					{
						throw new Exception("Failed to update sort order", error);
					}
					return true;
				});
				num2++;
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00072CE0 File Offset: 0x00070EE0
		internal int GetStartOrder(IList<Dictionary<string, object>> items)
		{
			int num = int.MaxValue;
			foreach (Dictionary<string, object> dictionary in items)
			{
				try
				{
					if (!dictionary.ContainsKey(this.DataSortField))
					{
						throw new NotSupportedException("Cannot find the property specified by DataSortField");
					}
					int num2 = Convert.ToInt32(dictionary[this.DataSortField]);
					if (num2 < num)
					{
						num = num2;
					}
				}
				catch (FormatException)
				{
					throw new NotSupportedException("The property specified by DataSortField cannot be converted to Int32");
				}
			}
			return num;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00072D7C File Offset: 0x00070F7C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void PerformInsert(IList<RadListBoxItem> items, int position)
		{
			RadListBoxInsertingEventArgs radListBoxInsertingEventArgs = new RadListBoxInsertingEventArgs(items);
			this.OnInserting(radListBoxInsertingEventArgs);
			if (radListBoxInsertingEventArgs.Cancel)
			{
				return;
			}
			IList<int> list = new List<int>();
			DataSourceView data = this.GetData();
			int num = 0;
			if (base.IsBoundUsingDataSourceID && this.AllowAutomaticUpdates)
			{
				num = RadListBox.Count(this.GetCurrentDataSource());
			}
			foreach (RadListBoxItem radListBoxItem in items)
			{
				if (base.IsBoundUsingDataSourceID && this.AllowAutomaticUpdates)
				{
					IDictionary dictionary = radListBoxItem.ExtractValues();
					if (!string.IsNullOrEmpty(this.DataSortField))
					{
						dictionary[this.DataSortField] = num;
						list.Add(num);
					}
					data.Insert(dictionary, (int affectedRows, Exception error) => error == null);
				}
				else
				{
					this.Items.Insert(position, radListBoxItem);
					if (this.DataKeysArrayList.Count > 0)
					{
						this.DataKeysArrayList.Insert(position, null);
					}
					position++;
				}
			}
			if (list.Count > 0)
			{
				List<RadListBoxItem> list2 = new List<RadListBoxItem>();
				foreach (int num2 in list)
				{
					if (num2 < this.Items.Count)
					{
						list2.Add(this.Items[num2]);
					}
				}
				if (list2.Count > 0)
				{
					this.Reorder(list2, position);
				}
			}
			this.OnInserted(new RadListBoxEventArgs(items));
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00072F28 File Offset: 0x00071128
		private void PerformTransfer(RadListBox sourceListBox, RadListBox destinationListBox, IList<RadListBoxItem> items)
		{
			RadListBoxItem radListBoxItem = null;
			int count = destinationListBox.Items.Count;
			if (this.TransferMode == ListBoxTransferMode.Move || sourceListBox == this.TransferToListBox)
			{
				radListBoxItem = (items[items.Count - 1].NextItem ?? items[0].PreviousItem);
				if (sourceListBox == this.TransferToListBox && this.TransferMode != ListBoxTransferMode.Move)
				{
					sourceListBox.ClearSelection();
					sourceListBox.Delete(items);
				}
			}
			if (this.TransferMode == ListBoxTransferMode.Move)
			{
				List<RadListBoxItem> list = new List<RadListBoxItem>();
				foreach (RadListBoxItem radListBoxItem2 in items)
				{
					bool flag = false;
					if (!this.AllowTransferDuplicates)
					{
						foreach (object obj in destinationListBox.Items)
						{
							RadListBoxItem radListBoxItem3 = (RadListBoxItem)obj;
							if (radListBoxItem3.Text == radListBoxItem2.Text && radListBoxItem3.Value == radListBoxItem2.Value)
							{
								radListBoxItem = null;
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						radListBoxItem2.Selected = false;
						sourceListBox.Delete(radListBoxItem2);
						radListBoxItem2.TemplateInstantiated = false;
						radListBoxItem2.Controls.Clear();
						list.Add(radListBoxItem2);
					}
				}
				destinationListBox.PerformInsert(list, destinationListBox.Items.Count);
			}
			else if (this.TransferMode == ListBoxTransferMode.Copy && destinationListBox != this)
			{
				List<RadListBoxItem> list2 = new List<RadListBoxItem>();
				foreach (RadListBoxItem radListBoxItem4 in items)
				{
					bool flag2 = false;
					if (!this.AllowTransferDuplicates)
					{
						foreach (object obj2 in destinationListBox.Items)
						{
							RadListBoxItem radListBoxItem5 = (RadListBoxItem)obj2;
							if (radListBoxItem5.Text == radListBoxItem4.Text && radListBoxItem5.Value == radListBoxItem4.Value)
							{
								flag2 = true;
								break;
							}
						}
					}
					if (!flag2)
					{
						if (destinationListBox.IsBoundUsingDataSourceID && destinationListBox.AllowAutomaticUpdates)
						{
							list2.Add(radListBoxItem4);
						}
						else
						{
							list2.Add(radListBoxItem4.Clone());
						}
					}
				}
				if (list2.Count > 0)
				{
					destinationListBox.PerformInsert(list2, destinationListBox.Items.Count);
				}
			}
			if (destinationListBox.DataKeysArrayList.Count > 0)
			{
				int num = destinationListBox.Items.Count - count;
				for (int i = 0; i < num; i++)
				{
					destinationListBox.AddDataKey(null);
				}
			}
			if (radListBoxItem == null)
			{
				return;
			}
			if (radListBoxItem.Enabled)
			{
				radListBoxItem.Selected = true;
			}
			if (!string.IsNullOrEmpty(sourceListBox.DataKeyField))
			{
				this._selectedItemKeys.Add(radListBoxItem.DataKey);
			}
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00073234 File Offset: 0x00071434
		private void OnTransfer(ListBoxPostBackCommand command, RadListBox.Func<RadListBox, IList<RadListBoxItem>> callback)
		{
			RadListBox radListBox = (command.SourceListBox == this.ClientID) ? this : this.TransferToListBox;
			if (this.TransferToListBox == null)
			{
				throw new NotSupportedException("TransferToID is not set");
			}
			RadListBox radListBox2 = (command.DestinationListBox == this.TransferToListBox.ClientID) ? this.TransferToListBox : this;
			IList<RadListBoxItem> itemsToTransfer = callback(radListBox);
			int num = radListBox2.Items.Count;
			this.Transfer(itemsToTransfer, radListBox, radListBox2);
			num = radListBox2.Items.Count - num;
			if (command.Offset == 0 || num == 0)
			{
				return;
			}
			List<RadListBoxItem> list = new List<RadListBoxItem>(num);
			for (int i = 0; i < num; i++)
			{
				list.Add(radListBox2.Items[radListBox2.Items.Count - num + i]);
			}
			radListBox2.Reorder(list, command.Offset);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00073318 File Offset: 0x00071518
		private void PerformDelete(IEnumerable<RadListBoxItem> items)
		{
			DataSourceView data = this.GetData();
			foreach (RadListBoxItem radListBoxItem in items)
			{
				if (this.Items.IndexOf(radListBoxItem) < 0)
				{
					throw new NotSupportedException("Deleted item must belong to listbox");
				}
				if (base.IsBoundUsingDataSourceID && this.AllowAutomaticUpdates)
				{
					if (string.IsNullOrEmpty(this.DataKeyField))
					{
						throw new NotSupportedException("DataKeyField is not set");
					}
					Dictionary<string, object> keys = new Dictionary<string, object>
					{
						{
							this.DataKeyField,
							this.DataKeys[this.Items.IndexOf(radListBoxItem)]
						}
					};
					data.Delete(keys, radListBoxItem.ExtractValues(), (int affectedItems, Exception error) => error == null);
				}
				else
				{
					if (this.DataKeysArrayList.Count > 0)
					{
						this.DataKeysArrayList.Remove(radListBoxItem.DataKey);
					}
					this.Items.Remove(radListBoxItem);
				}
			}
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00073430 File Offset: 0x00071630
		private void OnDelete(IList<RadListBoxItem> items)
		{
			this.Delete(items);
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x00073439 File Offset: 0x00071639
		private bool AutoPostBackOnDrop
		{
			get
			{
				return base.Events[RadListBox.DroppingEvent] != null || base.Events[RadListBox.DroppedEvent] != null;
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00073468 File Offset: 0x00071668
		private void OnDrop(ListBoxPostBackCommand command)
		{
			RadListBoxDroppingEventArgs radListBoxDroppingEventArgs = new RadListBoxDroppingEventArgs(command.HtmlElementId, this.SelectedItems);
			this.OnDropping(radListBoxDroppingEventArgs);
			if (radListBoxDroppingEventArgs.Cancel)
			{
				return;
			}
			this.OnDropped(new RadListBoxDroppedEventArgs(radListBoxDroppingEventArgs));
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x000734A3 File Offset: 0x000716A3
		// (set) Token: 0x06002200 RID: 8704 RVA: 0x000734AC File Offset: 0x000716AC
		[SimplePersistenceSetting]
		internal int[] SelectedIndices
		{
			get
			{
				return this.GetSelectedIndices();
			}
			set
			{
				if (this.SelectionMode == ListBoxSelectionMode.Multiple)
				{
					int[] selectedIndices = this.GetSelectedIndices();
					foreach (int index in selectedIndices)
					{
						this.Items[index].Selected = false;
					}
					for (int j = 0; j < value.Length; j++)
					{
						int num = value[j];
						if (num > -1 && num < this.Items.Count)
						{
							this.Items[num].Selected = true;
						}
					}
					return;
				}
				if (value.Length > 0)
				{
					this.ClearSelection();
					int num2 = value[0];
					if (num2 > -1 && num2 < this.Items.Count)
					{
						this.Items[num2].Selected = true;
					}
				}
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x0007356C File Offset: 0x0007176C
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x00073574 File Offset: 0x00071774
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

		// Token: 0x06002203 RID: 8707 RVA: 0x000735F3 File Offset: 0x000717F3
		public void LoadContentFile(string xmlFileName)
		{
			base.LoadXml(File.ReadAllText(this.Context.Server.MapPath(xmlFileName)));
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x00073614 File Offset: 0x00071814
		public void Delete(RadListBoxItem item)
		{
			this.Delete(new RadListBoxItem[]
			{
				item
			});
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00073634 File Offset: 0x00071834
		public virtual void Delete(IList<RadListBoxItem> items)
		{
			RadListBoxDeletingEventArgs radListBoxDeletingEventArgs = new RadListBoxDeletingEventArgs(items);
			this.OnDeleting(radListBoxDeletingEventArgs);
			if (radListBoxDeletingEventArgs.Cancel)
			{
				return;
			}
			this.PerformDelete(items);
			this.OnDeleted(new RadListBoxEventArgs(items));
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0007366C File Offset: 0x0007186C
		public virtual void Transfer(IList<RadListBoxItem> itemsToTransfer, RadListBox sourceListBox, RadListBox destinationListBox)
		{
			if (sourceListBox != this && sourceListBox != this.TransferToListBox)
			{
				throw new ArgumentOutOfRangeException("sourceListBox");
			}
			if (destinationListBox != this && destinationListBox != this.TransferToListBox)
			{
				throw new ArgumentOutOfRangeException("destinationListBox");
			}
			if (sourceListBox == destinationListBox)
			{
				throw new NotSupportedException("Source and destination listbox cannot be the same");
			}
			RadListBoxItem[] array = new RadListBoxItem[itemsToTransfer.Count];
			itemsToTransfer.CopyTo(array, 0);
			RadListBoxTransferringEventArgs radListBoxTransferringEventArgs = new RadListBoxTransferringEventArgs
			{
				SourceListBox = sourceListBox,
				DestinationListBox = destinationListBox,
				Items = array
			};
			this.OnTransferring(radListBoxTransferringEventArgs);
			if (radListBoxTransferringEventArgs.Cancel)
			{
				return;
			}
			if (array.Length > 0)
			{
				this.PerformTransfer(sourceListBox, destinationListBox, array);
			}
			this.OnTransferred(new RadListBoxTransferredEventArgs
			{
				SourceListBox = sourceListBox,
				DestinationListBox = destinationListBox,
				Items = array
			});
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0007372C File Offset: 0x0007192C
		public void Transfer(RadListBoxItem item, RadListBox sourceListBox, RadListBox destinationListBox)
		{
			this.Transfer(new RadListBoxItem[]
			{
				item
			}, sourceListBox, destinationListBox);
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00073750 File Offset: 0x00071950
		public void Reorder(int oldIndex, int newIndex)
		{
			if (oldIndex < 0 || oldIndex > this.Items.Count || oldIndex == newIndex)
			{
				return;
			}
			this.Reorder(new RadListBoxItem[]
			{
				this.Items[oldIndex]
			}, newIndex - oldIndex);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00073794 File Offset: 0x00071994
		public void ReorderToIndex(int oldIndex, int newIndex)
		{
			if (oldIndex < 0 || oldIndex > this.Items.Count || oldIndex == newIndex)
			{
				return;
			}
			this.ReorderToIndex(new RadListBoxItem[]
			{
				this.Items[oldIndex]
			}, newIndex);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000737D8 File Offset: 0x000719D8
		public virtual void Reorder(IList<RadListBoxItem> items, int offset)
		{
			RadListBoxReorderingEventArgs radListBoxReorderingEventArgs = new RadListBoxReorderingEventArgs
			{
				Items = items,
				Offset = offset
			};
			this.OnReordering(radListBoxReorderingEventArgs);
			if (radListBoxReorderingEventArgs.Cancel)
			{
				return;
			}
			if (offset > 0)
			{
				List<RadListBoxItem> list = new List<RadListBoxItem>(items);
				list.Reverse();
				items = list;
			}
			if (items.Count > 0)
			{
				this.PerformReorder(items, offset);
			}
			this.OnReordered(new RadListBoxEventArgs(items));
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0007383C File Offset: 0x00071A3C
		public virtual void ReorderToIndex(IList<RadListBoxItem> items, int targetIndex)
		{
			RadListBoxReorderingEventArgs radListBoxReorderingEventArgs = new RadListBoxReorderingEventArgs
			{
				Index = targetIndex,
				Items = items
			};
			this.OnReordering(radListBoxReorderingEventArgs);
			if (radListBoxReorderingEventArgs.Cancel)
			{
				return;
			}
			IList<RadListBoxItem> items2 = items;
			if (items[0].Index > targetIndex)
			{
				List<RadListBoxItem> list = new List<RadListBoxItem>(items);
				list.Reverse();
				items2 = list;
			}
			this.PerformReorder(items2, 0, targetIndex);
			this.OnReordered(new RadListBoxEventArgs(items));
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000738A4 File Offset: 0x00071AA4
		public RadListBoxItem FindItem(Predicate<RadListBoxItem> predicate)
		{
			return base.FindChild<RadListBoxItem>(predicate);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000738AD File Offset: 0x00071AAD
		public RadListBoxItem FindItemByText(string text)
		{
			return base.FindChildByText<RadListBoxItem>(text);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000738B6 File Offset: 0x00071AB6
		public RadListBoxItem FindItemByValue(string value)
		{
			return this.FindChildByValue<RadListBoxItem>(value);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000738C0 File Offset: 0x00071AC0
		public virtual void ClearSelection()
		{
			foreach (object obj in this.Items)
			{
				RadListBoxItem radListBoxItem = (RadListBoxItem)obj;
				radListBoxItem.Selected = false;
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0007391C File Offset: 0x00071B1C
		public virtual void ClearChecked()
		{
			foreach (object obj in this.Items)
			{
				RadListBoxItem radListBoxItem = (RadListBoxItem)obj;
				radListBoxItem.Checked = false;
			}
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x00073978 File Offset: 0x00071B78
		public virtual int[] GetSelectedIndices()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Selected)
				{
					list.Add(i);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000739C4 File Offset: 0x00071BC4
		public virtual int[] GetCheckedIndices()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Checked)
				{
					list.Add(i);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00073A0D File Offset: 0x00071C0D
		public int FindItemIndexByValue(string value)
		{
			return this.FindItemIndexByValue(value, false);
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00073A18 File Offset: 0x00071C18
		public int FindItemIndexByValue(string value, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(value))
			{
				return -1;
			}
			RadListBoxItem radListBoxItem = this.FindChildByValue<RadListBoxItem>(value, ignoreCase);
			if (radListBoxItem == null)
			{
				return -1;
			}
			return radListBoxItem.Index;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x00073A43 File Offset: 0x00071C43
		public void SortItems()
		{
			if (this.Sort != RadListBoxSort.None)
			{
				this.Items.Sort();
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x00073A58 File Offset: 0x00071C58
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.DeserializeCallbackArgument(eventArgument);
			if (base.IsBoundUsingDataSourceID || this.DataSource != null)
			{
				this.DataBind();
			}
			this.OnItemsRequested(new RadListBoxItemsRequestedEventArgs
			{
				StartIndex = this._callbackArgument.StartIndex,
				Count = this._callbackArgument.Count,
				Context = this._callbackArgument.UserContext
			});
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00073AC4 File Offset: 0x00071CC4
		string ICallbackEventHandler.GetCallbackResult()
		{
			if (this.Items == null)
			{
				return string.Empty;
			}
			StringWriter output = new StringWriter();
			List<RadListBoxItem> itemsToSerialize = new List<RadListBoxItem>();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new ListBoxItemConverter()
			});
			return this.GetResults(output, itemsToSerialize, javaScriptSerializer);
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x00073B20 File Offset: 0x00071D20
		// (set) Token: 0x06002219 RID: 8729 RVA: 0x00073B41 File Offset: 0x00071D41
		[DefaultValue(false)]
		[Description("")]
		[Category("Appearance")]
		public bool EnableMarkMatches
		{
			get
			{
				return (bool)(this.ViewState["EnableMarkMatches"] ?? false);
			}
			set
			{
				this.ViewState["EnableMarkMatches"] = value;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x00073B59 File Offset: 0x00071D59
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ListBoxButtons Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new ListBoxButtons(new LocalizationProvider("RadListBox", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x00073B98 File Offset: 0x00071D98
		// (set) Token: 0x0600221C RID: 8732 RVA: 0x00073BB8 File Offset: 0x00071DB8
		[Category("Misc")]
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where RadListBox will look for its .resx localization files.")]
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

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x00073C0B File Offset: 0x00071E0B
		// (set) Token: 0x0600221E RID: 8734 RVA: 0x00073C2B File Offset: 0x00071E2B
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Category("Misc")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x00073C3E File Offset: 0x00071E3E
		// (set) Token: 0x06002220 RID: 8736 RVA: 0x00073C5F File Offset: 0x00071E5F
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Whether to update the database or not")]
		public bool AllowAutomaticUpdates
		{
			get
			{
				return (bool)(this.ViewState["AllowAutomaticUpdates"] ?? false);
			}
			set
			{
				this.ViewState["AllowAutomaticUpdates"] = value;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x00073C77 File Offset: 0x00071E77
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual DataKeyCollection DataKeys
		{
			get
			{
				if (this._dataKeyArray == null)
				{
					this._dataKeyArray = new DataKeyCollection(ArrayList.Adapter(this.DataKeysArrayList));
				}
				return this._dataKeyArray;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x00073C9D File Offset: 0x00071E9D
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x00073CBE File Offset: 0x00071EBE
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Specifies whether to postback after delete")]
		public bool AutoPostBackOnDelete
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBackOnDelete"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBackOnDelete"] = value;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x00073CD6 File Offset: 0x00071ED6
		// (set) Token: 0x06002225 RID: 8741 RVA: 0x00073CF7 File Offset: 0x00071EF7
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Specifies whether to postback after transfer")]
		public bool AutoPostBackOnTransfer
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBackOnTransfer"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBackOnTransfer"] = value;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x00073D0F File Offset: 0x00071F0F
		// (set) Token: 0x06002227 RID: 8743 RVA: 0x00073D30 File Offset: 0x00071F30
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Specifies whether to postback after reorder")]
		public bool AutoPostBackOnReorder
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBackOnReorder"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBackOnReorder"] = value;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x00073D48 File Offset: 0x00071F48
		// (set) Token: 0x06002229 RID: 8745 RVA: 0x00073D69 File Offset: 0x00071F69
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Specifies whether to move or copy items during transfer")]
		[DefaultValue(ListBoxTransferMode.Move)]
		public ListBoxTransferMode TransferMode
		{
			get
			{
				return (ListBoxTransferMode)(this.ViewState["TransferMode"] ?? ListBoxTransferMode.Move);
			}
			set
			{
				this.ViewState["TransferMode"] = value;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x00073D81 File Offset: 0x00071F81
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x00073DA2 File Offset: 0x00071FA2
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("_allowTransferOnDblClick")]
		[Description("Allow items to be transfered with double click")]
		[Category("Behavior")]
		public bool AllowTransferOnDoubleClick
		{
			get
			{
				return (bool)(this.ViewState["AllowTransferOnDoubleClick"] ?? false);
			}
			set
			{
				this.ViewState["AllowTransferOnDoubleClick"] = value;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x00073DBA File Offset: 0x00071FBA
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x00073DDB File Offset: 0x00071FDB
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Allow one item to be transferred more than once")]
		public bool AllowTransferDuplicates
		{
			get
			{
				return (bool)(this.ViewState["AllowTransferDuplicates"] ?? false);
			}
			set
			{
				this.ViewState["AllowTransferDuplicates"] = value;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x00073DF4 File Offset: 0x00071FF4
		[Browsable(false)]
		public RadListBox TransferToListBox
		{
			get
			{
				if (string.IsNullOrEmpty(this.TransferToID) || base.DesignMode)
				{
					return null;
				}
				Control control = ChildControlHelper.FindControlRecursive(this, this.TransferToID, null);
				if (control == null)
				{
					throw new ArgumentException("Cannot find a RadListBox control specified by the TransferToID property");
				}
				RadListBox radListBox = control as RadListBox;
				if (radListBox == null)
				{
					throw new NotSupportedException("The control specified by the TransferToID property is not RadListBox");
				}
				return radListBox;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x00073E4A File Offset: 0x0007204A
		// (set) Token: 0x06002230 RID: 8752 RVA: 0x00073E6A File Offset: 0x0007206A
		[Description("The ID of the transfer RadListBox")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string TransferToID
		{
			get
			{
				return (string)(this.ViewState["TransferToID"] ?? "");
			}
			set
			{
				this.ViewState["TransferToID"] = value;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x00073E7D File Offset: 0x0007207D
		// (set) Token: 0x06002232 RID: 8754 RVA: 0x00073E9E File Offset: 0x0007209E
		[Description("Whether to persist the client-side changes after postback")]
		[ClientPropertyName("_persistChanges")]
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool PersistClientChanges
		{
			get
			{
				return (bool)(this.ViewState["PersistClientChanges"] ?? true);
			}
			set
			{
				this.ViewState["PersistClientChanges"] = value;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x00073EB6 File Offset: 0x000720B6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Button settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public ListBoxButtonSettings ButtonSettings
		{
			get
			{
				if (this._buttonSettings == null)
				{
					this._buttonSettings = new ListBoxButtonSettings(this.ViewState);
				}
				return this._buttonSettings;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x00073ED7 File Offset: 0x000720D7
		// (set) Token: 0x06002235 RID: 8757 RVA: 0x00073EF8 File Offset: 0x000720F8
		[Category("Behavior")]
		[Description("Gets or sets a value that indicates whether the CheckAll checkbox is shown in ListBox.")]
		[ClientPropertyName("_showCheckAll")]
		[DefaultValue(false)]
		[ClientControlProperty]
		public bool ShowCheckAll
		{
			get
			{
				return (bool)(this.ViewState["ShowCheckAll"] ?? false);
			}
			set
			{
				this.ViewState["ShowCheckAll"] = value;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x00073F10 File Offset: 0x00072110
		// (set) Token: 0x06002237 RID: 8759 RVA: 0x00073F31 File Offset: 0x00072131
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("Whether to show the reordering buttons")]
		[Category("Behavior")]
		public bool AllowReorder
		{
			get
			{
				return (bool)(this.ViewState["AllowReorder"] ?? false);
			}
			set
			{
				this.ViewState["AllowReorder"] = value;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x00073F49 File Offset: 0x00072149
		// (set) Token: 0x06002239 RID: 8761 RVA: 0x00073F6A File Offset: 0x0007216A
		[Description("Whether to show the delete button")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool AllowDelete
		{
			get
			{
				return (bool)(this.ViewState["AllowDelete"] ?? false);
			}
			set
			{
				this.ViewState["AllowDelete"] = value;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x00073F82 File Offset: 0x00072182
		// (set) Token: 0x0600223B RID: 8763 RVA: 0x00073FA3 File Offset: 0x000721A3
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Whether to show the transfer buttons")]
		[Category("Behavior")]
		public bool AllowTransfer
		{
			get
			{
				return (bool)(this.ViewState["AllowTransfer"] ?? false);
			}
			set
			{
				this.ViewState["AllowTransfer"] = value;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x00073FBB File Offset: 0x000721BB
		// (set) Token: 0x0600223D RID: 8765 RVA: 0x00073FDC File Offset: 0x000721DC
		[Category("Behavior")]
		[Description("Whether to postback after the selection changes")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Bindable(false)]
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

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x00073FF4 File Offset: 0x000721F4
		[Browsable(false)]
		public virtual IList<RadListBoxItem> CheckedItems
		{
			get
			{
				List<RadListBoxItem> list = new List<RadListBoxItem>();
				foreach (int index in this.GetCheckedIndices())
				{
					list.Add(this.Items[index]);
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x00074038 File Offset: 0x00072238
		// (set) Token: 0x06002240 RID: 8768 RVA: 0x00074059 File Offset: 0x00072259
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true enables Drag-and-drop functionality")]
		public bool EnableDragAndDrop
		{
			get
			{
				return (bool)(this.ViewState["EnableDragAndDrop"] ?? false);
			}
			set
			{
				this.ViewState["EnableDragAndDrop"] = value;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x00074071 File Offset: 0x00072271
		// (set) Token: 0x06002242 RID: 8770 RVA: 0x00074091 File Offset: 0x00072291
		[Description("Specifies the message that is displayed if there are no items in the RadListBox")]
		[DefaultValue("")]
		[Category("Appearance")]
		public string EmptyMessage
		{
			get
			{
				return (string)(this.ViewState["EmptyMessage"] ?? string.Empty);
			}
			set
			{
				this.ViewState["EmptyMessage"] = value;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x000740A4 File Offset: 0x000722A4
		// (set) Token: 0x06002244 RID: 8772 RVA: 0x000740D2 File Offset: 0x000722D2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[Bindable(false)]
		[TemplateContainer(typeof(RadListBox))]
		public virtual ITemplate EmptyMessageTemplate
		{
			get
			{
				if (this._emptyMessageTemplate == null && !string.IsNullOrEmpty(this.EmptyMessage))
				{
					this._emptyMessageTemplate = new DefaultEmptyMessageTemplate(this.EmptyMessage);
				}
				return this._emptyMessageTemplate;
			}
			set
			{
				this._emptyMessageTemplate = value;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000740DB File Offset: 0x000722DB
		// (set) Token: 0x06002246 RID: 8774 RVA: 0x000740E3 File Offset: 0x000722E3
		[Bindable(false)]
		[TemplateContainer(typeof(RadListBox))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public virtual ITemplate HeaderTemplate { get; set; }

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x000740EC File Offset: 0x000722EC
		// (set) Token: 0x06002248 RID: 8776 RVA: 0x000740F4 File Offset: 0x000722F4
		[TemplateContainer(typeof(RadListBox))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Bindable(false)]
		[Browsable(false)]
		public virtual ITemplate FooterTemplate { get; set; }

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x000740FD File Offset: 0x000722FD
		[Browsable(false)]
		public WebControl Header
		{
			get
			{
				this.EnsureChildControls();
				return this._header;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x0007410B File Offset: 0x0007230B
		[Browsable(false)]
		public WebControl Footer
		{
			get
			{
				this.EnsureChildControls();
				return this._footer;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x00074119 File Offset: 0x00072319
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x00074121 File Offset: 0x00072321
		[TemplateContainer(typeof(RadListBoxItem))]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x0007412A File Offset: 0x0007232A
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x0007414A File Offset: 0x0007234A
		[Category("Client")]
		[Browsable(false)]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of a RadListBoxItem when added on the client.")]
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

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0007415D File Offset: 0x0007235D
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The items of the listbox")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadListBoxItemCollection Items
		{
			[DebuggerStepThrough]
			get
			{
				return (RadListBoxItemCollection)base.Children;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0007416C File Offset: 0x0007236C
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x000741A8 File Offset: 0x000723A8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Behavior")]
		[DefaultValue(-1)]
		[Bindable(true)]
		[Description("SelectedIndex")]
		public virtual int SelectedIndex
		{
			get
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (this.Items[i].Selected)
					{
						return i;
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
						throw new ArgumentOutOfRangeException("value", value, "The index was set to less than -1, or greater than or equal to the number of items on the list at the time the list is rendered.");
					}
					value = -1;
				}
				if ((this.Items.Count != 0 && value < this.Items.Count) || value == -1)
				{
					this.ClearSelection();
					if (value >= 0)
					{
						this.Items[value].Selected = true;
					}
				}
				this._cachedSelectedIndex = value;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x00074224 File Offset: 0x00072424
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Bindable(false)]
		[Browsable(false)]
		public virtual RadListBoxItem SelectedItem
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

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x0007424C File Offset: 0x0007244C
		[Browsable(false)]
		public virtual IList<RadListBoxItem> SelectedItems
		{
			get
			{
				List<RadListBoxItem> list = new List<RadListBoxItem>();
				foreach (int index in this.GetSelectedIndices())
				{
					list.Add(this.Items[index]);
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x00074290 File Offset: 0x00072490
		// (set) Token: 0x06002255 RID: 8789 RVA: 0x000742C0 File Offset: 0x000724C0
		[Bindable(true, BindingDirection.TwoWay)]
		[Browsable(false)]
		[Themeable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientControlProperty]
		[ClientPropertyName("_selectedValue")]
		public virtual string SelectedValue
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex < 0)
				{
					return string.Empty;
				}
				return this.Items[selectedIndex].Value;
			}
			set
			{
				if (this.Items.Count != 0)
				{
					if (value == null || (base.DesignMode && value.Length == 0))
					{
						this.ClearSelection();
						return;
					}
					RadListBoxItem radListBoxItem = this.FindItemByValue(value);
					if (radListBoxItem != null)
					{
						this.ClearSelection();
						radListBoxItem.Selected = true;
					}
				}
				this._cachedSelectedValue = value;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x00074313 File Offset: 0x00072513
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x00074334 File Offset: 0x00072534
		[DefaultValue(ListBoxSelectionMode.Single)]
		[Description("The Selection Mode of the RadListBox")]
		[ClientPersistedProperty]
		[ClientPropertyName("selectionMode")]
		[Category("Behavior")]
		[ClientControlProperty]
		public ListBoxSelectionMode SelectionMode
		{
			get
			{
				return (ListBoxSelectionMode)(this.ViewState["SelectionMode"] ?? ListBoxSelectionMode.Single);
			}
			set
			{
				this.ViewState["SelectionMode"] = value;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x0007434C File Offset: 0x0007254C
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x00074377 File Offset: 0x00072577
		[DefaultValue(RadListBoxSort.None)]
		[Bindable(false)]
		[Category("Setup")]
		[Description("Automatically sorts items alphabetically (based on the Text property) in ascending or descending order")]
		public RadListBoxSort Sort
		{
			get
			{
				if (this.ViewState["Sort"] == null)
				{
					return RadListBoxSort.None;
				}
				return (RadListBoxSort)this.ViewState["Sort"];
			}
			set
			{
				this.ViewState["Sort"] = value;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x0007438F File Offset: 0x0007258F
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x000743BA File Offset: 0x000725BA
		[Category("Setup")]
		[DefaultValue(true)]
		[Bindable(false)]
		[Description("case sensitive")]
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

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x000743D2 File Offset: 0x000725D2
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x000743F3 File Offset: 0x000725F3
		[Description("When set to true displays a checkbox next to each item.")]
		[ClientPropertyName("_checkBoxes")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
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

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x0007440B File Offset: 0x0007260B
		[Browsable(false)]
		public IList<ClientOperation<RadListBoxItem>> ClientChanges
		{
			get
			{
				return this._clientChanges;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x00074413 File Offset: 0x00072613
		// (set) Token: 0x06002260 RID: 8800 RVA: 0x00074433 File Offset: 0x00072633
		[Category("Data")]
		[Description("Gets or sets the key field in the data source")]
		[DefaultValue("")]
		public string DataKeyField
		{
			get
			{
				return (string)(this.ViewState["DataKeyField"] ?? "");
			}
			set
			{
				this.ViewState["DataKeyField"] = value;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x00074446 File Offset: 0x00072646
		// (set) Token: 0x06002262 RID: 8802 RVA: 0x00074466 File Offset: 0x00072666
		[Category("Data")]
		[DefaultValue("")]
		[Description("Gets or sets the sord field in the data source")]
		public string DataSortField
		{
			get
			{
				return (string)(this.ViewState["DataSortField"] ?? "");
			}
			set
			{
				this.ViewState["DataSortField"] = value;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x00074479 File Offset: 0x00072679
		[ClientControlProperty]
		[ClientPropertyName("_uniqueId")]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x00074481 File Offset: 0x00072681
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x000744A1 File Offset: 0x000726A1
		[ClientControlProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[TypeConverter("Telerik.Web.Design.AjaxLoadingPanelIDConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[ClientPropertyName("_loadingPanelID")]
		public string LoadingPanelID
		{
			get
			{
				return (string)(this.ViewState["LoadingPanelID"] ?? "");
			}
			set
			{
				this.ViewState["LoadingPanelID"] = value;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x000744B4 File Offset: 0x000726B4
		// (set) Token: 0x06002267 RID: 8807 RVA: 0x000744D5 File Offset: 0x000726D5
		[DefaultValue(false)]
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

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x000744ED File Offset: 0x000726ED
		[Description("Gets the settings(service path and method name)for the web service used to populate items.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		public NavigationControlWebServiceSettings WebServiceSettings
		{
			get
			{
				return this._webServiceSettings;
			}
		}

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06002269 RID: 8809 RVA: 0x000744F5 File Offset: 0x000726F5
		// (remove) Token: 0x0600226A RID: 8810 RVA: 0x00074508 File Offset: 0x00072708
		public event RadListBoxUpdatingEventHandler Updating
		{
			add
			{
				base.Events.AddHandler(RadListBox.UpdatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.UpdatingEvent, value);
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0007451C File Offset: 0x0007271C
		protected virtual void OnUpdating(RadListBoxUpdatingEventArgs e)
		{
			RadListBoxUpdatingEventHandler radListBoxUpdatingEventHandler = (RadListBoxUpdatingEventHandler)base.Events[RadListBox.UpdatingEvent];
			if (radListBoxUpdatingEventHandler != null)
			{
				radListBoxUpdatingEventHandler(this, e);
			}
		}

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x0600226C RID: 8812 RVA: 0x0007454A File Offset: 0x0007274A
		// (remove) Token: 0x0600226D RID: 8813 RVA: 0x0007455D File Offset: 0x0007275D
		public event RadListBoxEventHandler Updated
		{
			add
			{
				base.Events.AddHandler(RadListBox.UpdatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.UpdatedEvent, value);
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x00074570 File Offset: 0x00072770
		protected virtual void OnUpdated(RadListBoxEventArgs e)
		{
			RadListBoxEventHandler radListBoxEventHandler = (RadListBoxEventHandler)base.Events[RadListBox.UpdatedEvent];
			if (radListBoxEventHandler != null)
			{
				radListBoxEventHandler(this, e);
			}
		}

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x0600226F RID: 8815 RVA: 0x0007459E File Offset: 0x0007279E
		// (remove) Token: 0x06002270 RID: 8816 RVA: 0x000745B1 File Offset: 0x000727B1
		public event RadListBoxEventHandler Deleted
		{
			add
			{
				base.Events.AddHandler(RadListBox.DeletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.DeletedEvent, value);
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000745C4 File Offset: 0x000727C4
		protected virtual void OnDeleted(RadListBoxEventArgs e)
		{
			RadListBoxEventHandler radListBoxEventHandler = (RadListBoxEventHandler)base.Events[RadListBox.DeletedEvent];
			if (radListBoxEventHandler != null)
			{
				radListBoxEventHandler(this, e);
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06002272 RID: 8818 RVA: 0x000745F2 File Offset: 0x000727F2
		// (remove) Token: 0x06002273 RID: 8819 RVA: 0x00074605 File Offset: 0x00072805
		public event RadListBoxDeletingEventHandler Deleting
		{
			add
			{
				base.Events.AddHandler(RadListBox.DeletingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.DeletingEvent, value);
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00074618 File Offset: 0x00072818
		protected virtual void OnDeleting(RadListBoxDeletingEventArgs e)
		{
			RadListBoxDeletingEventHandler radListBoxDeletingEventHandler = (RadListBoxDeletingEventHandler)base.Events[RadListBox.DeletingEvent];
			if (radListBoxDeletingEventHandler != null)
			{
				radListBoxDeletingEventHandler(this, e);
			}
		}

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06002275 RID: 8821 RVA: 0x00074646 File Offset: 0x00072846
		// (remove) Token: 0x06002276 RID: 8822 RVA: 0x00074659 File Offset: 0x00072859
		public event RadListBoxTransferredEventHandler Transferred
		{
			add
			{
				base.Events.AddHandler(RadListBox.TransferredEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.TransferredEvent, value);
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0007466C File Offset: 0x0007286C
		protected virtual void OnTransferred(RadListBoxTransferredEventArgs e)
		{
			RadListBoxTransferredEventHandler radListBoxTransferredEventHandler = (RadListBoxTransferredEventHandler)base.Events[RadListBox.TransferredEvent];
			if (radListBoxTransferredEventHandler != null)
			{
				radListBoxTransferredEventHandler(this, e);
			}
		}

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06002278 RID: 8824 RVA: 0x0007469A File Offset: 0x0007289A
		// (remove) Token: 0x06002279 RID: 8825 RVA: 0x000746AD File Offset: 0x000728AD
		public event RadListBoxTransferringEventHandler Transferring
		{
			add
			{
				base.Events.AddHandler(RadListBox.TransferringEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.TransferringEvent, value);
			}
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000746C0 File Offset: 0x000728C0
		protected virtual void OnTransferring(RadListBoxTransferringEventArgs e)
		{
			RadListBoxTransferringEventHandler radListBoxTransferringEventHandler = (RadListBoxTransferringEventHandler)base.Events[RadListBox.TransferringEvent];
			if (radListBoxTransferringEventHandler != null)
			{
				radListBoxTransferringEventHandler(this, e);
			}
		}

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x0600227B RID: 8827 RVA: 0x000746EE File Offset: 0x000728EE
		// (remove) Token: 0x0600227C RID: 8828 RVA: 0x00074701 File Offset: 0x00072901
		public event RadListBoxEventHandler Inserted
		{
			add
			{
				base.Events.AddHandler(RadListBox.InsertedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.InsertedEvent, value);
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00074714 File Offset: 0x00072914
		protected virtual void OnInserted(RadListBoxEventArgs e)
		{
			RadListBoxEventHandler radListBoxEventHandler = (RadListBoxEventHandler)base.Events[RadListBox.InsertedEvent];
			if (radListBoxEventHandler != null)
			{
				radListBoxEventHandler(this, e);
			}
		}

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x0600227E RID: 8830 RVA: 0x00074742 File Offset: 0x00072942
		// (remove) Token: 0x0600227F RID: 8831 RVA: 0x00074755 File Offset: 0x00072955
		public event RadListBoxInsertingEventHandler Inserting
		{
			add
			{
				base.Events.AddHandler(RadListBox.InsertingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.InsertingEvent, value);
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00074768 File Offset: 0x00072968
		protected virtual void OnInserting(RadListBoxInsertingEventArgs e)
		{
			RadListBoxInsertingEventHandler radListBoxInsertingEventHandler = (RadListBoxInsertingEventHandler)base.Events[RadListBox.InsertingEvent];
			if (radListBoxInsertingEventHandler != null)
			{
				radListBoxInsertingEventHandler(this, e);
			}
		}

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06002281 RID: 8833 RVA: 0x00074796 File Offset: 0x00072996
		// (remove) Token: 0x06002282 RID: 8834 RVA: 0x000747A9 File Offset: 0x000729A9
		public event RadListBoxEventHandler Reordered
		{
			add
			{
				base.Events.AddHandler(RadListBox.ReorderedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.ReorderedEvent, value);
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000747BC File Offset: 0x000729BC
		protected virtual void OnReordered(RadListBoxEventArgs e)
		{
			RadListBoxEventHandler radListBoxEventHandler = (RadListBoxEventHandler)base.Events[RadListBox.ReorderedEvent];
			if (radListBoxEventHandler != null)
			{
				radListBoxEventHandler(this, e);
			}
		}

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06002284 RID: 8836 RVA: 0x000747EA File Offset: 0x000729EA
		// (remove) Token: 0x06002285 RID: 8837 RVA: 0x000747FD File Offset: 0x000729FD
		public event RadListBoxReorderingEventHandler Reordering
		{
			add
			{
				base.Events.AddHandler(RadListBox.ReorderingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.ReorderingEvent, value);
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x00074810 File Offset: 0x00072A10
		protected virtual void OnReordering(RadListBoxReorderingEventArgs e)
		{
			RadListBoxReorderingEventHandler radListBoxReorderingEventHandler = (RadListBoxReorderingEventHandler)base.Events[RadListBox.ReorderingEvent];
			if (radListBoxReorderingEventHandler != null)
			{
				radListBoxReorderingEventHandler(this, e);
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06002287 RID: 8839 RVA: 0x0007483E File Offset: 0x00072A3E
		// (remove) Token: 0x06002288 RID: 8840 RVA: 0x00074851 File Offset: 0x00072A51
		public event RadListBoxDroppingEventHandler Dropping
		{
			add
			{
				base.Events.AddHandler(RadListBox.DroppingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.DroppingEvent, value);
			}
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x00074864 File Offset: 0x00072A64
		protected virtual void OnDropping(RadListBoxDroppingEventArgs e)
		{
			RadListBoxDroppingEventHandler radListBoxDroppingEventHandler = (RadListBoxDroppingEventHandler)base.Events[RadListBox.DroppingEvent];
			if (radListBoxDroppingEventHandler != null)
			{
				radListBoxDroppingEventHandler(this, e);
			}
		}

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x0600228A RID: 8842 RVA: 0x00074892 File Offset: 0x00072A92
		// (remove) Token: 0x0600228B RID: 8843 RVA: 0x000748A5 File Offset: 0x00072AA5
		public event RadListBoxDroppedEventHandler Dropped
		{
			add
			{
				base.Events.AddHandler(RadListBox.DroppedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.DroppedEvent, value);
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000748B8 File Offset: 0x00072AB8
		protected virtual void OnDropped(RadListBoxDroppedEventArgs e)
		{
			RadListBoxDroppedEventHandler radListBoxDroppedEventHandler = (RadListBoxDroppedEventHandler)base.Events[RadListBox.DroppedEvent];
			if (radListBoxDroppedEventHandler != null)
			{
				radListBoxDroppedEventHandler(this, e);
			}
		}

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x0600228D RID: 8845 RVA: 0x000748E6 File Offset: 0x00072AE6
		// (remove) Token: 0x0600228E RID: 8846 RVA: 0x000748F9 File Offset: 0x00072AF9
		public event RadListBoxItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadListBox.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.ItemDataBoundEvent, value);
			}
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0007490C File Offset: 0x00072B0C
		protected virtual void OnItemDataBound(RadListBoxItemEventArgs e)
		{
			this.RaiseItemEvent(RadListBox.ItemDataBoundEvent, e);
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06002290 RID: 8848 RVA: 0x0007491A File Offset: 0x00072B1A
		// (remove) Token: 0x06002291 RID: 8849 RVA: 0x0007492D File Offset: 0x00072B2D
		public event RadListBoxItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadListBox.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.ItemCreatedEvent, value);
			}
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x00074940 File Offset: 0x00072B40
		protected virtual void OnItemCreated(RadListBoxItemEventArgs e)
		{
			this.RaiseItemEvent(RadListBox.ItemCreatedEvent, e);
		}

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06002293 RID: 8851 RVA: 0x0007494E File Offset: 0x00072B4E
		// (remove) Token: 0x06002294 RID: 8852 RVA: 0x00074961 File Offset: 0x00072B61
		public event RadListBoxItemEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadListBox.TemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.TemplateNeededEvent, value);
			}
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x00074974 File Offset: 0x00072B74
		protected virtual void OnTemplateNeeded(RadListBoxItemEventArgs e)
		{
			this.RaiseItemEvent(RadListBox.TemplateNeededEvent, e);
		}

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06002296 RID: 8854 RVA: 0x00074982 File Offset: 0x00072B82
		// (remove) Token: 0x06002297 RID: 8855 RVA: 0x00074995 File Offset: 0x00072B95
		public event RadListBoxItemEventHandler ItemCheck
		{
			add
			{
				base.Events.AddHandler(RadListBox.ItemCheckEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.ItemCheckEvent, value);
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000749A8 File Offset: 0x00072BA8
		protected virtual void OnItemCheck(RadListBoxItemEventArgs e)
		{
			this.RaiseItemEvent(RadListBox.ItemCheckEvent, e);
		}

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06002299 RID: 8857 RVA: 0x000749B6 File Offset: 0x00072BB6
		// (remove) Token: 0x0600229A RID: 8858 RVA: 0x000749C9 File Offset: 0x00072BC9
		public event RadListBoxCheckAllCheckEventHandler CheckAllCheck
		{
			add
			{
				base.Events.AddHandler(RadListBox.CheckAllCheckEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.CheckAllCheckEvent, value);
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000749DC File Offset: 0x00072BDC
		protected virtual void OnCheckAllCheck(RadListBoxCheckAllCheckEventArgs e)
		{
			RadListBoxCheckAllCheckEventHandler radListBoxCheckAllCheckEventHandler = (RadListBoxCheckAllCheckEventHandler)base.Events[RadListBox.CheckAllCheckEvent];
			if (radListBoxCheckAllCheckEventHandler != null)
			{
				radListBoxCheckAllCheckEventHandler(this, e);
			}
		}

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x0600229C RID: 8860 RVA: 0x00074A0A File Offset: 0x00072C0A
		// (remove) Token: 0x0600229D RID: 8861 RVA: 0x00074A1D File Offset: 0x00072C1D
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadListBox.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.SelectedIndexChangedEvent, value);
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x00074A30 File Offset: 0x00072C30
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			this.RaiseEvent(RadListBox.SelectedIndexChangedEvent, e);
			this.OnTextChanged(e);
		}

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x0600229F RID: 8863 RVA: 0x00074A45 File Offset: 0x00072C45
		// (remove) Token: 0x060022A0 RID: 8864 RVA: 0x00074A58 File Offset: 0x00072C58
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(RadListBox.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.TextChangedEvent, value);
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x00074A6B File Offset: 0x00072C6B
		protected virtual void OnTextChanged(EventArgs e)
		{
			this.RaiseEvent(RadListBox.TextChangedEvent, e);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x00074A7C File Offset: 0x00072C7C
		protected virtual void OnItemsRequested(RadListBoxItemsRequestedEventArgs e)
		{
			RadListBoxItemsRequestedEventHandler radListBoxItemsRequestedEventHandler = (RadListBoxItemsRequestedEventHandler)base.Events[RadListBox.ItemsRequestedEvent];
			if (radListBoxItemsRequestedEventHandler != null)
			{
				radListBoxItemsRequestedEventHandler(this, e);
			}
		}

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060022A3 RID: 8867 RVA: 0x00074AAA File Offset: 0x00072CAA
		// (remove) Token: 0x060022A4 RID: 8868 RVA: 0x00074ABD File Offset: 0x00072CBD
		public event RadListBoxItemsRequestedEventHandler ItemsRequested
		{
			add
			{
				base.Events.AddHandler(RadListBox.ItemsRequestedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListBox.ItemsRequestedEvent, value);
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x00074AD0 File Offset: 0x00072CD0
		// (set) Token: 0x060022A6 RID: 8870 RVA: 0x00074AF0 File Offset: 0x00072CF0
		[ClientPropertyName("itemsRequesting")]
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		[Description("The client-side event that is fired before the items are requested server-side.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x00074B03 File Offset: 0x00072D03
		// (set) Token: 0x060022A8 RID: 8872 RVA: 0x00074B23 File Offset: 0x00072D23
		[DefaultValue("")]
		[Description("The name of the javascript function called after the request for items has completed.")]
		[Bindable(false)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemsRequested")]
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

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x00074B36 File Offset: 0x00072D36
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x00074B56 File Offset: 0x00072D56
		[DefaultValue("")]
		[Description("The name of the javascript function called after the request for items has failed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("itemsRequestFailed")]
		[Bindable(false)]
		[Category("Client-side events")]
		[ClientControlEvent]
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

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x00074B69 File Offset: 0x00072D69
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x00074B89 File Offset: 0x00072D89
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function called when an Item is created during Web Service Load on Demand")]
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

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x00074B9C File Offset: 0x00072D9C
		// (set) Token: 0x060022AE RID: 8878 RVA: 0x00074BBC File Offset: 0x00072DBC
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The JavaScript function executed when double clicking on an item")]
		[ClientPropertyName("itemDoubleClicking")]
		[Category("Client-side events")]
		public string OnClientItemDoubleClicking
		{
			get
			{
				return (string)(this.ViewState["OnClientItemDoubleClicking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemDoubleClicking"] = value;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x00074BCF File Offset: 0x00072DCF
		// (set) Token: 0x060022B0 RID: 8880 RVA: 0x00074BEF File Offset: 0x00072DEF
		[Description("The JavaScript function executed when item was double clicked")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemDoubleClicked")]
		[Category("Client-side events")]
		public string OnClientItemDoubleClicked
		{
			get
			{
				return (string)(this.ViewState["OnClientItemDoubleClicked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemDoubleClicked"] = value;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x00074C02 File Offset: 0x00072E02
		// (set) Token: 0x060022B2 RID: 8882 RVA: 0x00074C22 File Offset: 0x00072E22
		[Description("The JavaScript function executed when the selected index changes")]
		[Category("Client-side events")]
		[DefaultValue("")]
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

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x00074C35 File Offset: 0x00072E35
		// (set) Token: 0x060022B4 RID: 8884 RVA: 0x00074C55 File Offset: 0x00072E55
		[ClientControlEvent]
		[Description("The JavaScript function executed when the selected index changes")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x00074C68 File Offset: 0x00072E68
		// (set) Token: 0x060022B6 RID: 8886 RVA: 0x00074C88 File Offset: 0x00072E88
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[ClientPropertyName("contextMenu")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Description("The name of the javascript function called before context panel shows.")]
		public string OnClientContextMenu
		{
			get
			{
				return (string)(this.ViewState["OnClientContextMenu"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientContextMenu"] = value;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00074C9B File Offset: 0x00072E9B
		// (set) Token: 0x060022B8 RID: 8888 RVA: 0x00074CBB File Offset: 0x00072EBB
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The JavaScript function executed before checking an item")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x00074CCE File Offset: 0x00072ECE
		// (set) Token: 0x060022BA RID: 8890 RVA: 0x00074CEE File Offset: 0x00072EEE
		[Description("The JavaScript function executed when item is checked")]
		[Category("Client-side events")]
		[DefaultValue("")]
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

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x00074D01 File Offset: 0x00072F01
		// (set) Token: 0x060022BC RID: 8892 RVA: 0x00074D21 File Offset: 0x00072F21
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("The JavaScript function executed when CheckAll checkbox is checked")]
		[ClientControlEvent]
		[ClientPropertyName("checkAllChecked")]
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

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x00074D34 File Offset: 0x00072F34
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x00074D54 File Offset: 0x00072F54
		[DefaultValue("")]
		[Description("The JavaScript function executed before CheckAll checkbox is checked")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("checkAllChecking")]
		[Category("Client-side events")]
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

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x00074D67 File Offset: 0x00072F67
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x00074D87 File Offset: 0x00072F87
		[ClientControlEvent]
		[Description("The JavaScript function executed before deleting items")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientPropertyName("deleting")]
		public string OnClientDeleting
		{
			get
			{
				return (string)(this.ViewState["OnClientDeleting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDeleting"] = value;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x00074D9A File Offset: 0x00072F9A
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x00074DBA File Offset: 0x00072FBA
		[Description("The JavaScript function executed when items are deleted")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("deleted")]
		public string OnClientDeleted
		{
			get
			{
				return (string)(this.ViewState["OnClientDeleted"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDeleted"] = value;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00074DCD File Offset: 0x00072FCD
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x00074DED File Offset: 0x00072FED
		[ClientPropertyName("transferring")]
		[Description("The JavaScript function executed before transferring an item")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientTransferring
		{
			get
			{
				return (string)(this.ViewState["OnClientTransferring"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTransferring"] = value;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x00074E00 File Offset: 0x00073000
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x00074E20 File Offset: 0x00073020
		[DefaultValue("")]
		[ClientPropertyName("transferred")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Description("The JavaScript function executed when item is transferred")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientTransferred
		{
			get
			{
				return (string)(this.ViewState["OnClientTransferred"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTransferred"] = value;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x00074E33 File Offset: 0x00073033
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x00074E53 File Offset: 0x00073053
		[DefaultValue("")]
		[ClientPropertyName("reordering")]
		[Description("The JavaScript function executed before reordering an item")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientReordering
		{
			get
			{
				return (string)(this.ViewState["OnClientReordering"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientReordering"] = value;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x00074E66 File Offset: 0x00073066
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x00074E86 File Offset: 0x00073086
		[Category("Client-side events")]
		[Description("The JavaScript function executed when item is reordered")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("reordered")]
		public string OnClientReordered
		{
			get
			{
				return (string)(this.ViewState["OnClientReordered"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientReordered"] = value;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x00074E99 File Offset: 0x00073099
		// (set) Token: 0x060022CC RID: 8908 RVA: 0x00074EB9 File Offset: 0x000730B9
		[DefaultValue("")]
		[Description("The JavaScript function executed when the user hovers an item")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("mouseOver")]
		public string OnClientMouseOver
		{
			get
			{
				return (string)(this.ViewState["OnClientMouseOver"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientMouseOver"] = value;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x00074ECC File Offset: 0x000730CC
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x00074EEC File Offset: 0x000730EC
		[Description("The JavaScript function executed when the mouse moves away an item")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("mouseOut")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientMouseOut
		{
			get
			{
				return (string)(this.ViewState["OnClientMouseOut"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientMouseOut"] = value;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x00074EFF File Offset: 0x000730FF
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x00074F1F File Offset: 0x0007311F
		[ClientControlEvent]
		[Description("The JavaScript function executed when RadListBox is initialized")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientPropertyName("load")]
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

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x00074F32 File Offset: 0x00073132
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x00074F52 File Offset: 0x00073152
		[DefaultValue("")]
		[ClientPropertyName("dragStart")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Description("The JavaScript function executed when the user starts dragging an item")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientDragStart
		{
			get
			{
				return (string)(this.ViewState["OnClientDragStart"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDragStart"] = value;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x00074F65 File Offset: 0x00073165
		// (set) Token: 0x060022D4 RID: 8916 RVA: 0x00074F85 File Offset: 0x00073185
		[Description("The JavaScript function executed when the user moves the mouse while dragging an item.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("dragging")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		public string OnClientDragging
		{
			get
			{
				return (string)(this.ViewState["OnClientDragging"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDragging"] = value;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x00074F98 File Offset: 0x00073198
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x00074FB8 File Offset: 0x000731B8
		[ClientPropertyName("dropping")]
		[Description("The JavaScript function executed when the user drops an item onto another item.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientDropping
		{
			get
			{
				return (string)(this.ViewState["OnClientDropping"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropping"] = value;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x00074FCB File Offset: 0x000731CB
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x00074FEB File Offset: 0x000731EB
		[Category("Client-side events")]
		[Description("The JavaScript function executed after the user drops an item onto another item.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("dropped")]
		public string OnClientDropped
		{
			get
			{
				return (string)(this.ViewState["OnClientDropped"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropped"] = value;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x00074FFE File Offset: 0x000731FE
		// (set) Token: 0x060022DA RID: 8922 RVA: 0x0007501E File Offset: 0x0007321E
		[ClientControlEvent]
		[Description("The name of the JavaScript function called when the client template for a node is evaluated")]
		[Category("Client-side events")]
		[ClientPropertyName("templateDataBound")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x060022E2 RID: 8930 RVA: 0x00075034 File Offset: 0x00073234
		// Note: this type is marked as 'beforefieldinit'.
		static RadListBox()
		{
			RadListBox.UpdatingEvent = new object();
			RadListBox.UpdatedEvent = new object();
			RadListBox.DeletedEvent = new object();
			RadListBox.DeletingEvent = new object();
			RadListBox.TransferredEvent = new object();
			RadListBox.TransferringEvent = new object();
			RadListBox.InsertedEvent = new object();
			RadListBox.InsertingEvent = new object();
			RadListBox.ReorderedEvent = new object();
			RadListBox.ReorderingEvent = new object();
			RadListBox.DroppingEvent = new object();
			RadListBox.DroppedEvent = new object();
			RadListBox.ItemDataBoundEvent = new object();
			RadListBox.ItemCreatedEvent = new object();
			RadListBox.TemplateNeededEvent = new object();
			RadListBox.ItemCheckEvent = new object();
			RadListBox.CheckAllCheckEvent = new object();
			RadListBox.SelectedIndexChangedEvent = new object();
			RadListBox.TextChangedEvent = new object();
			RadListBox.ItemsRequestedEvent = new object();
		}

		// Token: 0x040008B1 RID: 2225
		internal const string DefaultButtonAreaWidth = "30px";

		// Token: 0x040008B2 RID: 2226
		internal const string DefaultButtonAreaHeight = "30px";

		// Token: 0x040008B3 RID: 2227
		private const string CallbackResultSeparator = "_$$_";

		// Token: 0x040008B4 RID: 2228
		private string _cachedSelectedValue;

		// Token: 0x040008B5 RID: 2229
		private int _cachedSelectedIndex = -1;

		// Token: 0x040008B6 RID: 2230
		private readonly List<object> _selectedItemKeys = new List<object>();

		// Token: 0x040008B7 RID: 2231
		private IList<ClientOperation<RadListBoxItem>> _clientChanges = new List<ClientOperation<RadListBoxItem>>();

		// Token: 0x040008B8 RID: 2232
		private DefaultHeaderFooterControl _header;

		// Token: 0x040008B9 RID: 2233
		private DefaultHeaderFooterControl _footer;

		// Token: 0x040008BA RID: 2234
		private readonly NavigationControlWebServiceSettings _webServiceSettings;

		// Token: 0x040008BB RID: 2235
		private IEnumerable currentDataSource;

		// Token: 0x040008BC RID: 2236
		private ListBoxCallbackArgument _callbackArgument;

		// Token: 0x040008BD RID: 2237
		private ListBoxButtons _localization;

		// Token: 0x040008BE RID: 2238
		private DataKeyCollection _dataKeyArray;

		// Token: 0x040008BF RID: 2239
		private ListBoxButtonSettings _buttonSettings;

		// Token: 0x040008C0 RID: 2240
		private ITemplate _emptyMessageTemplate;

		// Token: 0x020003A0 RID: 928
		internal static class Styles
		{
			// Token: 0x040008DE RID: 2270
			public const string ListClass = "rlbGroup";

			// Token: 0x040008DF RID: 2271
			public const string ButtonAreaCssClass = "RadListBoxButtonArea";

			// Token: 0x040008E0 RID: 2272
			public const string ScrollableCssClass = "RadListBoxScrollable";

			// Token: 0x040008E1 RID: 2273
			public const string ButtonCssClass = "rlbButton";

			// Token: 0x040008E2 RID: 2274
			public const string ButtonTextCssClass = "rlbButtonText";

			// Token: 0x040008E3 RID: 2275
			public const string NoButtonTextCssClass = "rlbNoButtonText";

			// Token: 0x040008E4 RID: 2276
			public const string MoveUpCssClass = "rlbMoveUp";

			// Token: 0x040008E5 RID: 2277
			public const string MoveDownCssClass = "rlbMoveDown";

			// Token: 0x040008E6 RID: 2278
			public const string MoveToTopCssClass = "rlbMoveToTop";

			// Token: 0x040008E7 RID: 2279
			public const string MoveToBottomCssClass = "rlbMoveToBottom";

			// Token: 0x040008E8 RID: 2280
			public const string DeleteCssClass = "rlbDelete";

			// Token: 0x040008E9 RID: 2281
			public const string TransferFromCssClass = "rlbTransferFrom";

			// Token: 0x040008EA RID: 2282
			public const string TransferToCssClass = "rlbTransferTo";

			// Token: 0x040008EB RID: 2283
			public const string TransferAllFromCssClass = "rlbTransferAllFrom";

			// Token: 0x040008EC RID: 2284
			public const string TransferAllToCssClass = "rlbTransferAllTo";

			// Token: 0x040008ED RID: 2285
			public const string EmptyMessageCssClass = "rlbEmptyMessage";

			// Token: 0x040008EE RID: 2286
			public const string HeaderCssClass = "rlbHeader";

			// Token: 0x040008EF RID: 2287
			public const string FooterCssClass = "rlbFooter";

			// Token: 0x040008F0 RID: 2288
			public const string TableTemplateCssClass = "rlbTemplateTable";

			// Token: 0x040008F1 RID: 2289
			public const string TableTemplateContainerCssClass = "rlbTemplateContainer";

			// Token: 0x040008F2 RID: 2290
			public const string GroupCellCssClass = "rlbGroupCell";

			// Token: 0x040008F3 RID: 2291
			public const string TemplateCellCssClass = "rlbTemplateCell";

			// Token: 0x040008F4 RID: 2292
			public const string GroupContainerCssClass = "rlbGroupContainer";

			// Token: 0x040008F5 RID: 2293
			public const string TemplateCssClass = "rlbTemplate";

			// Token: 0x040008F6 RID: 2294
			public const string ItemCssClass = "rlbItem";

			// Token: 0x040008F7 RID: 2295
			public const string TextCssClass = "rlbText";

			// Token: 0x040008F8 RID: 2296
			public const string ImageCssClass = "rlbImage";

			// Token: 0x040008F9 RID: 2297
			public const string CheckBoxCssClass = "rlbCheck";

			// Token: 0x040008FA RID: 2298
			public const string SelectedCssClass = "rlbSelected";

			// Token: 0x040008FB RID: 2299
			public const string DisabledListBoxCssClass = "rlbDisabled";

			// Token: 0x040008FC RID: 2300
			public const string HiddenItemCssClass = "rlbItemHidden";

			// Token: 0x040008FD RID: 2301
			public const string LiteDeleteCssClass = "rlbIconDelete";

			// Token: 0x040008FE RID: 2302
			public const string LiteDoubleArrowUpCssClass = "rlbIconArrowDoubleMoveUp";

			// Token: 0x040008FF RID: 2303
			public const string LiteDoubleArrowDownCssClass = "rlbIconArrowDoubleMoveDown";

			// Token: 0x04000900 RID: 2304
			public const string LiteDoubleArrowLeftCssClass = "rlbIconArrowDoubleMoveLeft";

			// Token: 0x04000901 RID: 2305
			public const string LiteDoubleArrowRightCssClass = "rlbIconArrowDoubleMoveRight";

			// Token: 0x04000902 RID: 2306
			public const string LiteMoveUpCssClass = "rlbIconMoveUp";

			// Token: 0x04000903 RID: 2307
			public const string LiteMoveDownCssClass = "rlbIconMoveDown";

			// Token: 0x04000904 RID: 2308
			public const string LiteMoveToTopCssClass = "rlbIconMoveToTop";

			// Token: 0x04000905 RID: 2309
			public const string LiteMoveToBottomCssClass = "rlbIconMoveToBottom";
		}

		// Token: 0x020003A1 RID: 929
		// (Invoke) Token: 0x060022E4 RID: 8932
		private delegate TResult Func<T, TResult>(T arg);
	}
}
