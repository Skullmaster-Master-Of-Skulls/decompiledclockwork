using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.DropDownTree;

namespace Telerik.Web.UI
{
	// Token: 0x02000B35 RID: 2869
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(DropDown))]
	[RequiredScript(typeof(TouchScrollExtender))]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadDropDownTree", "Telerik.Web.UI.DropDownTree.RadDropDownTreeScripts.js")]
	[ValidationProperty("SelectedText")]
	[TelerikToolboxCategory("Data")]
	[EmbeddedSkin("DropDownTree", typeof(RadDropDownTree))]
	[ToolboxBitmap(typeof(RadDropDownTree), "Telerik.Web.UI.DropDownTree.png")]
	[ToolboxData("<{0}:RadDropDownTree runat=\"server\"></{0}:RadDropDownTree>")]
	[EmbeddedSkin("DropDownTree", "Default", typeof(RadDropDownTree))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadDropDownTree))]
	[Designer("Telerik.Web.Design.RadDropDownTreeDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LightweightRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(jQueryPlugins))]
	public class RadDropDownTree : RadDataBoundControl, IDropDownTree, IPostBackEventHandler, INamingContainer, ILocalizableControl
	{
		// Token: 0x06006BC8 RID: 27592 RVA: 0x001915A0 File Offset: 0x0018F7A0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "_autoPostback", this.AutoPostBack, false);
			base.DescribeProperty<DropDownTreeCheckBoxes>(descriptor, "_checkBoxes", this.CheckBoxes, DropDownTreeCheckBoxes.None);
			base.DescribeProperty<bool>(descriptor, "_checkNodeOnClick", this.CheckNodeOnClick, false);
			base.DescribeProperty<string>(descriptor, "_defaultMessage", this.DefaultMessage, "");
			base.DescribeProperty<string>(descriptor, "_defaultValue", this.DefaultValue, "");
			base.DescribeProperty<string>(descriptor, "_embeddedTreeId", this.EmbeddedTreeID, null);
			base.DescribeProperty<bool>(descriptor, "_enableDirectionDetection", this.EnableDirectionDetection, false);
			base.DescribeProperty<bool>(descriptor, "_enableFiltering", this.EnableFiltering, false);
			base.DescribeProperty<bool>(descriptor, "_enableScreenBoundaryDetection", this.EnableScreenBoundaryDetection, true);
			base.DescribeProperty<string>(descriptor, "_entriesDelimiter", this.EntriesDelimiter, "; ");
			base.DescribeProperty<bool>(descriptor, "_expandNodeOnSingleClick", this.ExpandNodeOnSingleClick, false);
			base.DescribeProperty<string>(descriptor, "_fullPathDelimiter", this.FullPathDelimiter, "/");
			base.DescribeProperty<DropDownTreeTextMode>(descriptor, "_textMode", this.TextMode, DropDownTreeTextMode.Default);
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06006BC9 RID: 27593 RVA: 0x001916D0 File Offset: 0x0018F8D0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "clearButtonClicked", this.OnClientClearButtonClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "clearButtonClicking", this.OnClientClearButtonClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosed", this.OnClientDropDownClosed);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosing", this.OnClientDropDownClosing);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpened", this.OnClientDropDownOpened);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpening", this.OnClientDropDownOpening);
			RadDataBoundControl.DescribeEvent(descriptor, "entryAdded", this.OnClientEntryAdded);
			RadDataBoundControl.DescribeEvent(descriptor, "entryAdding", this.OnClientEntryAdding);
			RadDataBoundControl.DescribeEvent(descriptor, "entryRemoved", this.OnClientEntryRemoved);
			RadDataBoundControl.DescribeEvent(descriptor, "entryRemoving", this.OnClientEntryRemoving);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06006BCA RID: 27594 RVA: 0x001917A0 File Offset: 0x0018F9A0
		public RadDropDownTree()
		{
			this._webServiceSettings = new WebServiceSettings(this.ViewState);
			this._dropDownSettings = new DropDownSettings(this.ViewState);
			this._expandAnimation = new AnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new AnimationSettings("Collapse", this.ViewState);
		}

		// Token: 0x17002351 RID: 9041
		// (get) Token: 0x06006BCB RID: 27595 RVA: 0x00191822 File Offset: 0x0018FA22
		private bool HasHeaderTemplate
		{
			get
			{
				return this.HeaderTemplate != null || this.Header.Controls.Count > 0;
			}
		}

		// Token: 0x17002352 RID: 9042
		// (get) Token: 0x06006BCC RID: 27596 RVA: 0x00191841 File Offset: 0x0018FA41
		private bool HasFooterTemplate
		{
			get
			{
				return this.FooterTemplate != null || this.Footer.Controls.Count > 0;
			}
		}

		// Token: 0x06006BCD RID: 27597 RVA: 0x00191860 File Offset: 0x0018FA60
		private void CreateFooter()
		{
			if (this._footer == null)
			{
				this._footer = new DropDownTreeHeaderFooterControl();
				this.Controls.Add(this.Footer);
				this.Footer.ID = "Footer";
				this.Footer.CssClass = "rddtFooter";
			}
		}

		// Token: 0x06006BCE RID: 27598 RVA: 0x001918B4 File Offset: 0x0018FAB4
		private void CreateHeader()
		{
			if (this._header == null)
			{
				this._header = new DropDownTreeHeaderFooterControl();
				this.Controls.Add(this.Header);
				this.Header.ID = "Header";
				this.Header.CssClass = "rddtHeader";
			}
		}

		// Token: 0x17002353 RID: 9043
		// (get) Token: 0x06006BCF RID: 27599 RVA: 0x00191905 File Offset: 0x0018FB05
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x06006BD0 RID: 27600 RVA: 0x00191914 File Offset: 0x0018FB14
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] == null)
			{
				this.Children.Clear();
				return;
			}
			((IStateManager)this.Children).LoadViewState(array[1]);
		}

		// Token: 0x06006BD1 RID: 27601 RVA: 0x00191950 File Offset: 0x0018FB50
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Children).SaveViewState()
			};
			return arrayList.ToArray();
		}

		// Token: 0x06006BD2 RID: 27602 RVA: 0x0019198A File Offset: 0x0018FB8A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Children).TrackViewState();
		}

		// Token: 0x17002354 RID: 9044
		// (get) Token: 0x06006BD3 RID: 27603 RVA: 0x0019199D File Offset: 0x0018FB9D
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002355 RID: 9045
		// (get) Token: 0x06006BD4 RID: 27604 RVA: 0x001919A0 File Offset: 0x0018FBA0
		protected internal DropDownTreeEntryCollection Children
		{
			[DebuggerStepThrough]
			get
			{
				if (this._children == null)
				{
					this._children = this.CreateChildItemCollection();
				}
				return this._children;
			}
		}

		// Token: 0x06006BD5 RID: 27605 RVA: 0x001919BC File Offset: 0x0018FBBC
		protected DropDownTreeEntryCollection CreateChildItemCollection()
		{
			return new DropDownTreeEntryCollection(this);
		}

		// Token: 0x17002356 RID: 9046
		// (get) Token: 0x06006BD6 RID: 27606 RVA: 0x001919C4 File Offset: 0x0018FBC4
		// (set) Token: 0x06006BD7 RID: 27607 RVA: 0x001919E5 File Offset: 0x0018FBE5
		internal EmbeddedTreeAdapter EmbeddedTreeAdapter
		{
			get
			{
				if (this._embeddedTreeAdapter == null)
				{
					this._embeddedTreeAdapter = new EmbeddedTreeAdapter(this, new RadTreeView());
				}
				return this._embeddedTreeAdapter;
			}
			set
			{
				this._embeddedTreeAdapter = value;
			}
		}

		// Token: 0x17002357 RID: 9047
		// (get) Token: 0x06006BD8 RID: 27608 RVA: 0x001919EE File Offset: 0x0018FBEE
		[ClientControlProperty]
		[ClientPropertyName("_embeddedTreeId")]
		internal string EmbeddedTreeID
		{
			get
			{
				return this.EmbeddedTreeAdapter.ClientID;
			}
		}

		// Token: 0x17002358 RID: 9048
		// (get) Token: 0x06006BD9 RID: 27609 RVA: 0x001919FB File Offset: 0x0018FBFB
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17002359 RID: 9049
		// (get) Token: 0x06006BDA RID: 27610 RVA: 0x00191A03 File Offset: 0x0018FC03
		internal bool IsControlEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x06006BDB RID: 27611 RVA: 0x00191A0C File Offset: 0x0018FC0C
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			RadTreeView embeddedTree = this.EmbeddedTreeAdapter.GetEmbeddedTree();
			embeddedTree.ID = "EmbeddedTree";
			embeddedTree.RenderMode = this.RenderMode;
			this.Controls.Add(embeddedTree);
		}

		// Token: 0x06006BDC RID: 27612 RVA: 0x00191A50 File Offset: 0x0018FC50
		protected DropDownTreeEntry FindEntryByTextAndValue(string text, string value)
		{
			foreach (object obj in this.Entries)
			{
				DropDownTreeEntry dropDownTreeEntry = (DropDownTreeEntry)obj;
				if (dropDownTreeEntry.Text == text && dropDownTreeEntry.Value == value)
				{
					return dropDownTreeEntry;
				}
			}
			return null;
		}

		// Token: 0x06006BDD RID: 27613 RVA: 0x00191AC8 File Offset: 0x0018FCC8
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreateHeader();
			if (this.HeaderTemplate != null)
			{
				this.ApplyTemplate(this.Header, this.HeaderTemplate);
			}
			this.CreateFooter();
			if (this.FooterTemplate != null)
			{
				this.ApplyTemplate(this.Footer, this.FooterTemplate);
			}
		}

		// Token: 0x06006BDE RID: 27614 RVA: 0x00191B1C File Offset: 0x0018FD1C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this._isCheckedIndicesSet || this._isSelectedIndicesSet)
			{
				this.Entries.ClearAll();
				this.SyncEmbeddedTree();
			}
			RadTreeView embeddedTree = this.EmbeddedTreeAdapter.GetEmbeddedTree();
			embeddedTree.Skin = base.RuntimeSkin;
			embeddedTree.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
			embeddedTree.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				this.SyncTreeDataBindSettings();
				embeddedTree.PersistLoadOnDemandNodes = false;
			}
			this.EmbeddedTreeAdapter.SyncWebServiceSettings(this.WebServiceSettings);
		}

		// Token: 0x06006BDF RID: 27615 RVA: 0x00191BAC File Offset: 0x0018FDAC
		private void SyncTreeDataBindSettings()
		{
			this.EmbeddedTreeAdapter.DataFieldID = this.DataFieldID;
			this.EmbeddedTreeAdapter.DataFieldParentID = this.DataFieldParentID;
			this.EmbeddedTreeAdapter.DataTextField = this.DataTextField;
			this.EmbeddedTreeAdapter.DataValueField = this.DataValueField;
			this.EmbeddedTreeAdapter.DataSource = this.DataSource;
			this.EmbeddedTreeAdapter.DataSourceID = this.DataSourceID;
			this.EmbeddedTreeAdapter.ClientDataSourceID = this.ClientDataSourceID;
			this.EmbeddedTreeAdapter.SyncDataBindings(this.DataBindings);
			this.EmbeddedTreeAdapter.DropDownTreeNodeDataBound += this.EmbeddedTree_NodeDataBound;
		}

		// Token: 0x06006BE0 RID: 27616 RVA: 0x00191C58 File Offset: 0x0018FE58
		private void EmbeddedTree_NodeDataBound(object sender, DropDownTreeNodeDataBoundEventArguments e)
		{
			DropDownTreeNodeDataBoundEventArguments e2 = new DropDownTreeNodeDataBoundEventArguments(e.DropDownTreeNode);
			this.OnNodeDataBound(e2);
		}

		// Token: 0x06006BE1 RID: 27617 RVA: 0x00191C78 File Offset: 0x0018FE78
		internal void ApplyTemplate(WebControl control, ITemplate template)
		{
			DropDownTreeHeaderFooterControl dropDownTreeHeaderFooterControl = control as DropDownTreeHeaderFooterControl;
			if (dropDownTreeHeaderFooterControl.TemplateInstantiated)
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
			dropDownTreeHeaderFooterControl.TemplateInstantiated = true;
		}

		// Token: 0x06006BE2 RID: 27618 RVA: 0x00191CD4 File Offset: 0x0018FED4
		internal void CreateEntryFromDropDownNode(DropDownTreeNode node)
		{
			DropDownTreeEntry dropDownTreeEntry = new DropDownTreeEntry();
			dropDownTreeEntry.Text = node.Text;
			dropDownTreeEntry.Value = node.Value;
			dropDownTreeEntry.FullPath = node.FullPath;
			if (this.CheckBoxes == DropDownTreeCheckBoxes.None)
			{
				this.Entries.Clear();
				node.Selected = true;
			}
			else if (this.CheckBoxes != DropDownTreeCheckBoxes.TriState)
			{
				node.Checked = true;
			}
			this.Entries.Add(dropDownTreeEntry);
		}

		// Token: 0x06006BE3 RID: 27619 RVA: 0x00191D44 File Offset: 0x0018FF44
		internal void CreateEntryFromRadTreeNode(RadTreeNode node)
		{
			DropDownTreeEntry dropDownTreeEntry = new DropDownTreeEntry();
			dropDownTreeEntry.Text = node.Text;
			dropDownTreeEntry.Value = node.Value;
			dropDownTreeEntry.FullPath = node.GetFullPath(this.FullPathDelimiter);
			this.Entries.Add(dropDownTreeEntry);
		}

		// Token: 0x06006BE4 RID: 27620 RVA: 0x00191D90 File Offset: 0x0018FF90
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new DropDownTreeEntryConverter(),
				new DropDownSettingsConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			if (this.Entries.Count > 0)
			{
				string script = javaScriptSerializer.Serialize(this.Entries);
				descriptor.AddScriptProperty("entryData", script);
			}
			if (this.EnableFiltering)
			{
				if (this.DropDownNodeTemplate != null)
				{
					if (this.FilterSettings.Highlight == DropDownTreeHighlight.Matches)
					{
						this.FilterSettings.FilterTemplate = DropDownTreeFilterTemplate.ByContent;
					}
				}
				else
				{
					this.FilterSettings.FilterTemplate = DropDownTreeFilterTemplate.ByText;
				}
				base.DescribeProperty<string>(descriptor, "_filterEmptyMessage", this.FilterSettings.EmptyMessage, string.Empty);
				base.DescribeProperty<DropDownTreeFilter>(descriptor, "_filter", this.FilterSettings.Filter, DropDownTreeFilter.StartsWith);
				base.DescribeProperty<DropDownTreeHighlight>(descriptor, "_highlight", this.FilterSettings.Highlight, DropDownTreeHighlight.None);
				base.DescribeProperty<DropDownTreeFilterTemplate>(descriptor, "_filterTemplate", this.FilterSettings.FilterTemplate, DropDownTreeFilterTemplate.ByText);
				base.DescribeProperty<int>(descriptor, "_minFilterLength", this.FilterSettings.MinFilterLength, 1);
			}
			descriptor.AddScriptProperty("_dropDownSettings", javaScriptSerializer.Serialize(this.DropDownSettings));
			base.DescribeProperty<bool>(descriptor, "_enableEntryTextHtmlEncoding", this.EnableEntryTextHtmlEncoding, false);
			base.DescribeProperty<bool>(descriptor, "_enabledState", this.IsControlEnabled, true);
			descriptor.AddScriptProperty("localization", javaScriptSerializer.Serialize(this.Localization));
			base.DescribeProperty<bool>(descriptor, "_showClear", this.ButtonSettings.ShowClear, false);
			base.DescribeProperty<bool>(descriptor, "_showCheckAll", this.ButtonSettings.ShowCheckAll, false);
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
		}

		// Token: 0x06006BE5 RID: 27621 RVA: 0x00191F58 File Offset: 0x00190158
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureChildControls();
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(postCollection[this.ClientID + "_ClientState"]))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				this.clientState = javaScriptSerializer.Deserialize<DropDownTreeClientState>(text);
				this.Enabled = this.clientState.Enabled;
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			this.LoadClientState(this.clientState);
			return false;
		}

		// Token: 0x06006BE6 RID: 27622 RVA: 0x00191FF4 File Offset: 0x001901F4
		internal void LoadClientState(DropDownTreeClientState state)
		{
			if (state.LogEntries != null)
			{
				this._fireServerEvents = state.FireServerEvents;
				this.LoadLogEntries(state.LogEntries);
			}
		}

		// Token: 0x06006BE7 RID: 27623 RVA: 0x00192018 File Offset: 0x00190218
		private void LoadLogEntries(ClientStateLogEntry[] entries)
		{
			foreach (ClientStateLogEntry clientStateLogEntry in entries)
			{
				if (clientStateLogEntry.Type == ClientStateLogEntryType.Remove)
				{
					DropDownTreeEntry dropDownTreeEntry = this.FindEntryByTextAndValue(HttpUtility.HtmlDecode(clientStateLogEntry.Data["text"].ToString()), HttpUtility.HtmlDecode(clientStateLogEntry.Data["value"].ToString()));
					if (dropDownTreeEntry != null)
					{
						this.Entries.Remove(dropDownTreeEntry);
						if (this._fireServerEvents)
						{
							this._removedEntries.Add(dropDownTreeEntry);
						}
					}
				}
			}
			foreach (ClientStateLogEntry clientStateLogEntry2 in entries)
			{
				if (clientStateLogEntry2.Type == ClientStateLogEntryType.Insert)
				{
					DropDownTreeEntry dropDownTreeEntry2 = this.CreateEntry();
					dropDownTreeEntry2.LoadFromDictionary(clientStateLogEntry2.Data);
					this.Entries.Add(dropDownTreeEntry2);
					if (this._fireServerEvents)
					{
						this._addedEntries.Add(dropDownTreeEntry2);
					}
				}
			}
		}

		// Token: 0x06006BE8 RID: 27624 RVA: 0x00192101 File Offset: 0x00190301
		private DropDownTreeEntry CreateEntry()
		{
			return new DropDownTreeEntry();
		}

		// Token: 0x1700235A RID: 9050
		// (get) Token: 0x06006BE9 RID: 27625 RVA: 0x00192108 File Offset: 0x00190308
		internal IDropDownEmbeddedTreeRenderer EmbeddedTreeRenderer
		{
			get
			{
				if (this._embeddedTreeRenderer == null)
				{
					this.CreateControlRenderer<IDropDownEmbeddedTreeRenderer>();
				}
				return this._embeddedTreeRenderer;
			}
		}

		// Token: 0x06006BEA RID: 27626 RVA: 0x0019211E File Offset: 0x0019031E
		protected internal override IRenderer CreateControlRenderer()
		{
			return new DropDownTreeRenderer(this);
		}

		// Token: 0x06006BEB RID: 27627 RVA: 0x00192128 File Offset: 0x00190328
		protected internal virtual void CreateControlRenderer<T>()
		{
			if (typeof(T).Name == "IDropDownTreeRenderer")
			{
				this._renderer = new DropDownTreeRenderer(this);
			}
			if (typeof(T).Name == "IDropDownEmbeddedTreeRenderer")
			{
				this._embeddedTreeRenderer = new DropDownEmbeddedTreeRenderer(this);
			}
		}

		// Token: 0x1700235B RID: 9051
		// (get) Token: 0x06006BEC RID: 27628 RVA: 0x00192183 File Offset: 0x00190383
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x1700235C RID: 9052
		// (get) Token: 0x06006BED RID: 27629 RVA: 0x00192190 File Offset: 0x00190390
		protected override string CssClassFormatString
		{
			get
			{
				return "RadDropDownTree RadDropDownTree_{0}";
			}
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x00192197 File Offset: 0x00190397
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06006BEF RID: 27631 RVA: 0x001921A5 File Offset: 0x001903A5
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06006BF0 RID: 27632 RVA: 0x001921AE File Offset: 0x001903AE
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06006BF1 RID: 27633 RVA: 0x001921C4 File Offset: 0x001903C4
		public void RaisePostBackEvent(string eventArgument)
		{
			if (this._fireServerEvents)
			{
				if (this._removedEntries.Count > 0)
				{
					foreach (DropDownTreeEntry entry in this._removedEntries)
					{
						this.OnEntryRemoved(new DropDownTreeEntryEventArgs(entry));
					}
					this.OnEntriesRemoved(new DropDownTreeEntriesEventArgs(this._removedEntries));
				}
				if (this._addedEntries.Count > 0)
				{
					foreach (DropDownTreeEntry entry2 in this._addedEntries)
					{
						this.OnEntryAdded(new DropDownTreeEntryEventArgs(entry2));
					}
					this.OnEntriesAdded(new DropDownTreeEntriesEventArgs(this._addedEntries));
				}
			}
		}

		// Token: 0x06006BF2 RID: 27634 RVA: 0x001922AC File Offset: 0x001904AC
		protected virtual void OnEntryAdded(DropDownTreeEntryEventArgs e)
		{
			this.RaiseEntryEvent(RadDropDownTree.EntryAddedEvent, e);
		}

		// Token: 0x06006BF3 RID: 27635 RVA: 0x001922BA File Offset: 0x001904BA
		protected virtual void OnEntryRemoved(DropDownTreeEntryEventArgs e)
		{
			this.RaiseEntryEvent(RadDropDownTree.EntryRemovedEvent, e);
		}

		// Token: 0x06006BF4 RID: 27636 RVA: 0x001922C8 File Offset: 0x001904C8
		private void RaiseEntryEvent(object eventKey, DropDownTreeEntryEventArgs e)
		{
			DropDownTreeEntryEventHandler dropDownTreeEntryEventHandler = (DropDownTreeEntryEventHandler)base.Events[eventKey];
			if (dropDownTreeEntryEventHandler != null)
			{
				dropDownTreeEntryEventHandler(this, e);
			}
		}

		// Token: 0x06006BF5 RID: 27637 RVA: 0x001922F2 File Offset: 0x001904F2
		protected virtual void OnEntriesAdded(DropDownTreeEntriesEventArgs e)
		{
			this.RaiseEntriesEvent(RadDropDownTree.EntriesAddedEvent, e);
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x00192300 File Offset: 0x00190500
		protected virtual void OnEntriesRemoved(DropDownTreeEntriesEventArgs e)
		{
			this.RaiseEntriesEvent(RadDropDownTree.EntriesRemovedEvent, e);
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x00192310 File Offset: 0x00190510
		private void RaiseEntriesEvent(object eventKey, DropDownTreeEntriesEventArgs e)
		{
			DropDownTreeEntriesEventHandler dropDownTreeEntriesEventHandler = (DropDownTreeEntriesEventHandler)base.Events[eventKey];
			if (dropDownTreeEntriesEventHandler != null)
			{
				dropDownTreeEntriesEventHandler(this, e);
			}
		}

		// Token: 0x1700235D RID: 9053
		// (get) Token: 0x06006BF8 RID: 27640 RVA: 0x0019233A File Offset: 0x0019053A
		// (set) Token: 0x06006BF9 RID: 27641 RVA: 0x00192347 File Offset: 0x00190547
		[SimplePersistenceSetting]
		internal List<string> ExpandedIndices
		{
			get
			{
				return this.EmbeddedTree.ExpandedIndices;
			}
			set
			{
				this.EmbeddedTree.ExpandedIndices = value;
			}
		}

		// Token: 0x1700235E RID: 9054
		// (get) Token: 0x06006BFA RID: 27642 RVA: 0x00192355 File Offset: 0x00190555
		// (set) Token: 0x06006BFB RID: 27643 RVA: 0x00192362 File Offset: 0x00190562
		[SimplePersistenceSetting]
		internal List<string> CheckedIndices
		{
			get
			{
				return this.EmbeddedTree.CheckedIndices;
			}
			set
			{
				this.EmbeddedTree.CheckedIndices = value;
				this._isCheckedIndicesSet = true;
			}
		}

		// Token: 0x1700235F RID: 9055
		// (get) Token: 0x06006BFC RID: 27644 RVA: 0x00192377 File Offset: 0x00190577
		// (set) Token: 0x06006BFD RID: 27645 RVA: 0x00192384 File Offset: 0x00190584
		[SimplePersistenceSetting]
		internal List<string> SelectedIndices
		{
			get
			{
				return this.EmbeddedTree.SelectedIndices;
			}
			set
			{
				this.EmbeddedTree.SelectedIndices = value;
				this._isSelectedIndicesSet = true;
			}
		}

		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x06006BFE RID: 27646 RVA: 0x00192399 File Offset: 0x00190599
		// (remove) Token: 0x06006BFF RID: 27647 RVA: 0x001923AC File Offset: 0x001905AC
		public event DropDownTreeNodeDataBoundEventHandler NodeDataBound
		{
			add
			{
				base.Events.AddHandler(RadDropDownTree.NodeDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownTree.NodeDataBoundEvent, value);
			}
		}

		// Token: 0x06006C00 RID: 27648 RVA: 0x001923C0 File Offset: 0x001905C0
		protected virtual void OnNodeDataBound(DropDownTreeNodeDataBoundEventArguments e)
		{
			DropDownTreeNodeDataBoundEventHandler dropDownTreeNodeDataBoundEventHandler = (DropDownTreeNodeDataBoundEventHandler)base.Events[RadDropDownTree.NodeDataBoundEvent];
			if (dropDownTreeNodeDataBoundEventHandler != null)
			{
				dropDownTreeNodeDataBoundEventHandler(this, e);
			}
		}

		// Token: 0x17002360 RID: 9056
		// (get) Token: 0x06006C01 RID: 27649 RVA: 0x001923EE File Offset: 0x001905EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002361 RID: 9057
		// (get) Token: 0x06006C02 RID: 27650 RVA: 0x001923F1 File Offset: 0x001905F1
		// (set) Token: 0x06006C03 RID: 27651 RVA: 0x001923F9 File Offset: 0x001905F9
		public string DataFieldID { get; set; }

		// Token: 0x17002362 RID: 9058
		// (get) Token: 0x06006C04 RID: 27652 RVA: 0x00192402 File Offset: 0x00190602
		// (set) Token: 0x06006C05 RID: 27653 RVA: 0x0019240A File Offset: 0x0019060A
		public string DataFieldParentID { get; set; }

		// Token: 0x17002363 RID: 9059
		// (get) Token: 0x06006C06 RID: 27654 RVA: 0x00192413 File Offset: 0x00190613
		// (set) Token: 0x06006C07 RID: 27655 RVA: 0x0019241B File Offset: 0x0019061B
		public string DataTextField { get; set; }

		// Token: 0x17002364 RID: 9060
		// (get) Token: 0x06006C08 RID: 27656 RVA: 0x00192424 File Offset: 0x00190624
		// (set) Token: 0x06006C09 RID: 27657 RVA: 0x0019242C File Offset: 0x0019062C
		public string DataValueField { get; set; }

		// Token: 0x17002365 RID: 9061
		// (get) Token: 0x06006C0A RID: 27658 RVA: 0x00192435 File Offset: 0x00190635
		// (set) Token: 0x06006C0B RID: 27659 RVA: 0x00192455 File Offset: 0x00190655
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("Gets or sets the ODataDataSource used for data binding.")]
		[DefaultValue("")]
		public override string ODataDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ODataDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ODataDataSourceID"] = value;
				this.EmbeddedTreeAdapter.ODataDataSourceID = value;
			}
		}

		// Token: 0x17002366 RID: 9062
		// (get) Token: 0x06006C0C RID: 27660 RVA: 0x00192474 File Offset: 0x00190674
		// (set) Token: 0x06006C0D RID: 27661 RVA: 0x0019247C File Offset: 0x0019067C
		[Bindable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[TemplateContainer(typeof(RadTreeNode))]
		public virtual ITemplate DropDownNodeTemplate
		{
			get
			{
				return this._dropDownNodeTemplate;
			}
			set
			{
				this._dropDownNodeTemplate = value;
				this.EmbeddedTreeAdapter.NodeTemplate = value;
			}
		}

		// Token: 0x17002367 RID: 9063
		// (get) Token: 0x06006C0E RID: 27662 RVA: 0x00192491 File Offset: 0x00190691
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public DropDownTreeEntryCollection Entries
		{
			get
			{
				return this.Children;
			}
		}

		// Token: 0x17002368 RID: 9064
		// (get) Token: 0x06006C0F RID: 27663 RVA: 0x00192499 File Offset: 0x00190699
		[ClientPropertyName("_uniqueId")]
		[ClientControlProperty]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x17002369 RID: 9065
		// (get) Token: 0x06006C10 RID: 27664 RVA: 0x001924A1 File Offset: 0x001906A1
		// (set) Token: 0x06006C11 RID: 27665 RVA: 0x001924C1 File Offset: 0x001906C1
		[Localizable(true)]
		[ClientPropertyName("_defaultMessage")]
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		public virtual string DefaultMessage
		{
			get
			{
				return (string)(this.ViewState["DefaultMessage"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DefaultMessage"] = value;
			}
		}

		// Token: 0x1700236A RID: 9066
		// (get) Token: 0x06006C12 RID: 27666 RVA: 0x001924D4 File Offset: 0x001906D4
		// (set) Token: 0x06006C13 RID: 27667 RVA: 0x001924F5 File Offset: 0x001906F5
		[ClientControlProperty]
		[Description("Weather to Html encode the text of entries.")]
		[ClientPropertyName("_enableEntryTextHtmlEncoding")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool EnableEntryTextHtmlEncoding
		{
			get
			{
				return (bool)(this.ViewState["EnableEntryTextHtmlEncoding"] ?? false);
			}
			set
			{
				this.ViewState["EnableEntryTextHtmlEncoding"] = value;
			}
		}

		// Token: 0x1700236B RID: 9067
		// (get) Token: 0x06006C14 RID: 27668 RVA: 0x0019250D File Offset: 0x0019070D
		[Description("DropDown settings")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DropDownSettings DropDownSettings
		{
			get
			{
				return this._dropDownSettings;
			}
		}

		// Token: 0x1700236C RID: 9068
		// (get) Token: 0x06006C15 RID: 27669 RVA: 0x00192518 File Offset: 0x00190718
		// (set) Token: 0x06006C16 RID: 27670 RVA: 0x0019254D File Offset: 0x0019074D
		[ClientControlProperty]
		[DefaultValue(DropDownTreeCheckBoxes.None)]
		[ClientPropertyName("_checkBoxes")]
		public virtual DropDownTreeCheckBoxes CheckBoxes
		{
			get
			{
				DropDownTreeCheckBoxes? dropDownTreeCheckBoxes = (DropDownTreeCheckBoxes?)this.ViewState["CheckBoxes"];
				if (dropDownTreeCheckBoxes == null)
				{
					return DropDownTreeCheckBoxes.None;
				}
				return dropDownTreeCheckBoxes.GetValueOrDefault();
			}
			set
			{
				this.ViewState["CheckBoxes"] = value;
				this.EmbeddedTreeAdapter.CheckBoxes = value;
			}
		}

		// Token: 0x1700236D RID: 9069
		// (get) Token: 0x06006C17 RID: 27671 RVA: 0x00192571 File Offset: 0x00190771
		// (set) Token: 0x06006C18 RID: 27672 RVA: 0x00192579 File Offset: 0x00190779
		[Bindable(false)]
		[TemplateContainer(typeof(RadDropDownTree))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public virtual ITemplate HeaderTemplate { get; set; }

		// Token: 0x1700236E RID: 9070
		// (get) Token: 0x06006C19 RID: 27673 RVA: 0x00192582 File Offset: 0x00190782
		// (set) Token: 0x06006C1A RID: 27674 RVA: 0x0019258A File Offset: 0x0019078A
		[TemplateContainer(typeof(RadDropDownTree))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[Bindable(false)]
		public virtual ITemplate FooterTemplate { get; set; }

		// Token: 0x1700236F RID: 9071
		// (get) Token: 0x06006C1B RID: 27675 RVA: 0x00192593 File Offset: 0x00190793
		[Browsable(false)]
		public WebControl Header
		{
			get
			{
				this.EnsureChildControls();
				return this._header;
			}
		}

		// Token: 0x17002370 RID: 9072
		// (get) Token: 0x06006C1C RID: 27676 RVA: 0x001925A1 File Offset: 0x001907A1
		[Browsable(false)]
		public WebControl Footer
		{
			get
			{
				this.EnsureChildControls();
				return this._footer;
			}
		}

		// Token: 0x17002371 RID: 9073
		// (get) Token: 0x06006C1D RID: 27677 RVA: 0x001925AF File Offset: 0x001907AF
		// (set) Token: 0x06006C1E RID: 27678 RVA: 0x001925D0 File Offset: 0x001907D0
		[ClientControlProperty]
		[Category("Behavior")]
		[ClientPropertyName("_autoPostback")]
		[Description("Whether to postback after an item is selected")]
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

		// Token: 0x17002372 RID: 9074
		// (get) Token: 0x06006C1F RID: 27679 RVA: 0x001925E8 File Offset: 0x001907E8
		// (set) Token: 0x06006C20 RID: 27680 RVA: 0x00192609 File Offset: 0x00190809
		[Category("Behavior")]
		[ClientPropertyName("_textMode")]
		[DefaultValue(DropDownTreeTextMode.Default)]
		[Bindable(false)]
		[ClientControlProperty]
		public DropDownTreeTextMode TextMode
		{
			get
			{
				return (DropDownTreeTextMode)(this.ViewState["ShowFullPath"] ?? DropDownTreeTextMode.Default);
			}
			set
			{
				this.ViewState["ShowFullPath"] = value;
			}
		}

		// Token: 0x17002373 RID: 9075
		// (get) Token: 0x06006C21 RID: 27681 RVA: 0x00192621 File Offset: 0x00190821
		// (set) Token: 0x06006C22 RID: 27682 RVA: 0x00192641 File Offset: 0x00190841
		[Bindable(false)]
		[Category("Behavior")]
		[ClientPropertyName("_fullPathDelimiter")]
		[DefaultValue("/")]
		[ClientControlProperty]
		public string FullPathDelimiter
		{
			get
			{
				return (string)(this.ViewState["FullPathDelimiter"] ?? "/");
			}
			set
			{
				this.ViewState["FullPathDelimiter"] = value;
			}
		}

		// Token: 0x17002374 RID: 9076
		// (get) Token: 0x06006C23 RID: 27683 RVA: 0x00192654 File Offset: 0x00190854
		// (set) Token: 0x06006C24 RID: 27684 RVA: 0x00192674 File Offset: 0x00190874
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue("; ")]
		[ClientPropertyName("_entriesDelimiter")]
		[Bindable(false)]
		public string EntriesDelimiter
		{
			get
			{
				return (string)(this.ViewState["EntriesDelimiter"] ?? "; ");
			}
			set
			{
				this.ViewState["EntriesDelimiter"] = value;
			}
		}

		// Token: 0x17002375 RID: 9077
		// (get) Token: 0x06006C25 RID: 27685 RVA: 0x00192687 File Offset: 0x00190887
		// (set) Token: 0x06006C26 RID: 27686 RVA: 0x001926A7 File Offset: 0x001908A7
		[DefaultValue("")]
		[ClientPropertyName("_defaultValue")]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		public string DefaultValue
		{
			get
			{
				return (string)(this.ViewState["DefaultValue"] ?? "");
			}
			set
			{
				this.ViewState["DefaultValue"] = value;
			}
		}

		// Token: 0x17002376 RID: 9078
		// (get) Token: 0x06006C27 RID: 27687 RVA: 0x001926BC File Offset: 0x001908BC
		// (set) Token: 0x06006C28 RID: 27688 RVA: 0x0019273E File Offset: 0x0019093E
		[Bindable(true, BindingDirection.TwoWay)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Behavior")]
		[Browsable(false)]
		[Themeable(false)]
		public string SelectedText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				int count = this.Entries.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.TextMode == DropDownTreeTextMode.Default)
					{
						stringBuilder.Append(this.Entries[i].Text);
					}
					else
					{
						stringBuilder.Append(this.Entries[i].FullPath);
					}
					if (count > 1 && i != count - 1)
					{
						stringBuilder.Append(this.EntriesDelimiter);
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				this.EmbeddedTreeAdapter.CreateEntry(false, value);
			}
		}

		// Token: 0x17002377 RID: 9079
		// (get) Token: 0x06006C29 RID: 27689 RVA: 0x00192750 File Offset: 0x00190950
		// (set) Token: 0x06006C2A RID: 27690 RVA: 0x001927AF File Offset: 0x001909AF
		[Bindable(true, BindingDirection.TwoWay)]
		[Browsable(false)]
		[Themeable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Behavior")]
		public string SelectedValue
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				int count = this.Entries.Count;
				for (int i = 0; i < count; i++)
				{
					stringBuilder.Append(this.Entries[i].Value);
					if (count > 1 && i != count - 1)
					{
						stringBuilder.Append(",");
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				this.EmbeddedTreeAdapter.CreateEntry(true, value);
			}
		}

		// Token: 0x17002378 RID: 9080
		// (get) Token: 0x06006C2B RID: 27691 RVA: 0x001927BE File Offset: 0x001909BE
		// (set) Token: 0x06006C2C RID: 27692 RVA: 0x001927DF File Offset: 0x001909DF
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_expandNodeOnSingleClick")]
		[DefaultValue(false)]
		public bool ExpandNodeOnSingleClick
		{
			get
			{
				return (bool)(this.ViewState["ExpandNodeOnSingleClick"] ?? false);
			}
			set
			{
				this.ViewState["ExpandNodeOnSingleClick"] = value;
			}
		}

		// Token: 0x17002379 RID: 9081
		// (get) Token: 0x06006C2D RID: 27693 RVA: 0x001927F7 File Offset: 0x001909F7
		// (set) Token: 0x06006C2E RID: 27694 RVA: 0x00192818 File Offset: 0x00190A18
		[ClientPropertyName("_checkNodeOnClick")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[Bindable(false)]
		public bool CheckNodeOnClick
		{
			get
			{
				return (bool)(this.ViewState["CheckNodeOnClick"] ?? false);
			}
			set
			{
				this.ViewState["CheckNodeOnClick"] = value;
			}
		}

		// Token: 0x1700237A RID: 9082
		// (get) Token: 0x06006C2F RID: 27695 RVA: 0x00192830 File Offset: 0x00190A30
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("DropDownTree Button settings")]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public DropDownTreeButtonSettings ButtonSettings
		{
			get
			{
				if (this._buttonSettings == null)
				{
					this._buttonSettings = new DropDownTreeButtonSettings(this.ViewState);
				}
				return this._buttonSettings;
			}
		}

		// Token: 0x1700237B RID: 9083
		// (get) Token: 0x06006C30 RID: 27696 RVA: 0x00192851 File Offset: 0x00190A51
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Description("DropDownTree Filter settings")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public DropDownTreeFilterSettings FilterSettings
		{
			get
			{
				if (this._filterSettings == null)
				{
					this._filterSettings = new DropDownTreeFilterSettings(this.ViewState);
				}
				return this._filterSettings;
			}
		}

		// Token: 0x1700237C RID: 9084
		// (get) Token: 0x06006C31 RID: 27697 RVA: 0x00192872 File Offset: 0x00190A72
		// (set) Token: 0x06006C32 RID: 27698 RVA: 0x00192894 File Offset: 0x00190A94
		[Description("Gets or sets a value indicating where RadListBox will look for its .resx localization files.")]
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

		// Token: 0x1700237D RID: 9085
		// (get) Token: 0x06006C33 RID: 27699 RVA: 0x001928E7 File Offset: 0x00190AE7
		// (set) Token: 0x06006C34 RID: 27700 RVA: 0x00192907 File Offset: 0x00190B07
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
			}
		}

		// Token: 0x1700237E RID: 9086
		// (get) Token: 0x06006C35 RID: 27701 RVA: 0x0019291A File Offset: 0x00190B1A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DropDownTreeButtons Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new DropDownTreeButtons(new LocalizationProvider("RadDropDownTree", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x1700237F RID: 9087
		// (get) Token: 0x06006C36 RID: 27702 RVA: 0x00192959 File Offset: 0x00190B59
		// (set) Token: 0x06006C37 RID: 27703 RVA: 0x0019297A File Offset: 0x00190B7A
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientPropertyName("_enableFiltering")]
		[Bindable(false)]
		[ClientControlProperty]
		public bool EnableFiltering
		{
			get
			{
				return (bool)(this.ViewState["EnableFiltering"] ?? false);
			}
			set
			{
				this.ViewState["EnableFiltering"] = value;
			}
		}

		// Token: 0x17002380 RID: 9088
		// (get) Token: 0x06006C38 RID: 27704 RVA: 0x00192992 File Offset: 0x00190B92
		[Description("The web service to be used for populating nodes with ExpandMode set to WebService.")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				return this._webServiceSettings;
			}
		}

		// Token: 0x17002381 RID: 9089
		// (get) Token: 0x06006C39 RID: 27705 RVA: 0x0019299A File Offset: 0x00190B9A
		[Category("Behavior")]
		[Description("The animation played when the dropdown is opened")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x17002382 RID: 9090
		// (get) Token: 0x06006C3A RID: 27706 RVA: 0x001929A2 File Offset: 0x00190BA2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The animation played when the dropdown is closed")]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public AnimationSettings CollapseAnimation
		{
			get
			{
				return this._collapseAnimation;
			}
		}

		// Token: 0x17002383 RID: 9091
		// (get) Token: 0x06006C3B RID: 27707 RVA: 0x001929AA File Offset: 0x00190BAA
		// (set) Token: 0x06006C3C RID: 27708 RVA: 0x001929CB File Offset: 0x00190BCB
		[Bindable(false)]
		[ClientControlProperty]
		[ClientPropertyName("_enableScreenBoundaryDetection")]
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

		// Token: 0x17002384 RID: 9092
		// (get) Token: 0x06006C3D RID: 27709 RVA: 0x001929E3 File Offset: 0x00190BE3
		// (set) Token: 0x06006C3E RID: 27710 RVA: 0x00192A04 File Offset: 0x00190C04
		[ClientPropertyName("_enableDirectionDetection")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Bindable(false)]
		[DefaultValue(false)]
		public bool EnableDirectionDetection
		{
			get
			{
				return (bool)(this.ViewState["EnableDirectionDetection"] ?? false);
			}
			set
			{
				this.ViewState["EnableDirectionDetection"] = value;
			}
		}

		// Token: 0x17002385 RID: 9093
		// (get) Token: 0x06006C3F RID: 27711 RVA: 0x00192A1C File Offset: 0x00190C1C
		// (set) Token: 0x06006C40 RID: 27712 RVA: 0x00192A37 File Offset: 0x00190C37
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<DropDownNodeBinding> DataBindings
		{
			get
			{
				if (this._dataBindings == null)
				{
					this._dataBindings = new List<DropDownNodeBinding>();
				}
				return this._dataBindings;
			}
			set
			{
				this._dataBindings = value;
			}
		}

		// Token: 0x17002386 RID: 9094
		// (get) Token: 0x06006C41 RID: 27713 RVA: 0x00192A40 File Offset: 0x00190C40
		// (set) Token: 0x06006C42 RID: 27714 RVA: 0x00192A60 File Offset: 0x00190C60
		[Browsable(false)]
		[Category("Client")]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of a DropDownNode when added on the client.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual string ClientNodeTemplate
		{
			get
			{
				return (this.ViewState["ClientNodeTemplate"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientNodeTemplate"] = value;
			}
		}

		// Token: 0x17002387 RID: 9095
		// (get) Token: 0x06006C43 RID: 27715 RVA: 0x00192A73 File Offset: 0x00190C73
		public RadTreeView EmbeddedTree
		{
			get
			{
				return this.EmbeddedTreeAdapter.GetEmbeddedTree();
			}
		}

		// Token: 0x06006C44 RID: 27716 RVA: 0x00192A80 File Offset: 0x00190C80
		public override void DataBind()
		{
			this.SyncTreeDataBindSettings();
			this.Entries.Clear();
			this.EmbeddedTreeAdapter.DataBind();
			base.DataBind();
		}

		// Token: 0x06006C45 RID: 27717 RVA: 0x00192AA4 File Offset: 0x00190CA4
		public void ExpandAllDropDownNodes()
		{
			this.EmbeddedTreeAdapter.ExpandEmbeddedTree();
		}

		// Token: 0x06006C46 RID: 27718 RVA: 0x00192AC4 File Offset: 0x00190CC4
		public void SyncEmbeddedTree()
		{
			IList<RadTreeNode> nodesByCriteria;
			if (this.CheckBoxes == DropDownTreeCheckBoxes.None)
			{
				nodesByCriteria = this.EmbeddedTree.GetNodesByCriteria((RadTreeNode x) => x.Selected, false);
			}
			else
			{
				nodesByCriteria = this.EmbeddedTree.GetNodesByCriteria((RadTreeNode x) => x.Checked, false);
			}
			if (nodesByCriteria.Count > 0)
			{
				this.Entries.ClearAll();
			}
			foreach (RadTreeNode node in nodesByCriteria)
			{
				this.CreateEntryFromRadTreeNode(node);
			}
		}

		// Token: 0x17002388 RID: 9096
		// (get) Token: 0x06006C47 RID: 27719 RVA: 0x00192B80 File Offset: 0x00190D80
		// (set) Token: 0x06006C48 RID: 27720 RVA: 0x00192BA0 File Offset: 0x00190DA0
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		[Description("The JavaScript function executed when RadDropDownTree is initialized")]
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

		// Token: 0x17002389 RID: 9097
		// (get) Token: 0x06006C49 RID: 27721 RVA: 0x00192BB3 File Offset: 0x00190DB3
		// (set) Token: 0x06006C4A RID: 27722 RVA: 0x00192BD3 File Offset: 0x00190DD3
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		[Description("The client-side event this is fired when the drop down is about to be opened.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("dropDownOpening")]
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

		// Token: 0x1700238A RID: 9098
		// (get) Token: 0x06006C4B RID: 27723 RVA: 0x00192BE6 File Offset: 0x00190DE6
		// (set) Token: 0x06006C4C RID: 27724 RVA: 0x00192C06 File Offset: 0x00190E06
		[DefaultValue("")]
		[ClientPropertyName("dropDownOpened")]
		[Category("Client-side events")]
		[Bindable(false)]
		[Description("The client-side event this is fired when the drop down is being opened.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x1700238B RID: 9099
		// (get) Token: 0x06006C4D RID: 27725 RVA: 0x00192C19 File Offset: 0x00190E19
		// (set) Token: 0x06006C4E RID: 27726 RVA: 0x00192C39 File Offset: 0x00190E39
		[ClientPropertyName("dropDownClosing")]
		[ClientControlEvent]
		[Bindable(false)]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The client-side event that is fired when the drop down is about to be closed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x1700238C RID: 9100
		// (get) Token: 0x06006C4F RID: 27727 RVA: 0x00192C4C File Offset: 0x00190E4C
		// (set) Token: 0x06006C50 RID: 27728 RVA: 0x00192C6C File Offset: 0x00190E6C
		[Description("The client-side event that is fired when the drop down is being closed.")]
		[ClientPropertyName("dropDownClosed")]
		[Bindable(false)]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x1700238D RID: 9101
		// (get) Token: 0x06006C51 RID: 27729 RVA: 0x00192C7F File Offset: 0x00190E7F
		// (set) Token: 0x06006C52 RID: 27730 RVA: 0x00192C9F File Offset: 0x00190E9F
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("entryAdding")]
		[Bindable(false)]
		[Category("Client-side events")]
		[Description("The client-side event that is fired when an entry is about to be added.")]
		public string OnClientEntryAdding
		{
			get
			{
				return (string)(this.ViewState["OnClientEntryAdding"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientEntryAdding"] = value;
			}
		}

		// Token: 0x1700238E RID: 9102
		// (get) Token: 0x06006C53 RID: 27731 RVA: 0x00192CB2 File Offset: 0x00190EB2
		// (set) Token: 0x06006C54 RID: 27732 RVA: 0x00192CD2 File Offset: 0x00190ED2
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function called after an entry has been added")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("entryAdded")]
		public string OnClientEntryAdded
		{
			get
			{
				return (string)(this.ViewState["OnClientEntryAdded"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientEntryAdded"] = value;
			}
		}

		// Token: 0x1700238F RID: 9103
		// (get) Token: 0x06006C55 RID: 27733 RVA: 0x00192CE5 File Offset: 0x00190EE5
		// (set) Token: 0x06006C56 RID: 27734 RVA: 0x00192D05 File Offset: 0x00190F05
		[ClientPropertyName("entryRemoving")]
		[Description("Gets or sets the name of the JavaScript function called when an entry is about to be removed")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientEntryRemoving
		{
			get
			{
				return (string)(this.ViewState["OnClientEntryRemoving"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientEntryRemoving"] = value;
			}
		}

		// Token: 0x17002390 RID: 9104
		// (get) Token: 0x06006C57 RID: 27735 RVA: 0x00192D18 File Offset: 0x00190F18
		// (set) Token: 0x06006C58 RID: 27736 RVA: 0x00192D38 File Offset: 0x00190F38
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function called when an entry was removed")]
		[ClientControlEvent]
		[ClientPropertyName("entryRemoved")]
		[Category("Client-side events")]
		public string OnClientEntryRemoved
		{
			get
			{
				return (string)(this.ViewState["OnClientEntryRemoved"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientEntryRemoved"] = value;
			}
		}

		// Token: 0x17002391 RID: 9105
		// (get) Token: 0x06006C59 RID: 27737 RVA: 0x00192D4B File Offset: 0x00190F4B
		// (set) Token: 0x06006C5A RID: 27738 RVA: 0x00192D6B File Offset: 0x00190F6B
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function called on clear button clicking")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("clearButtonClicking")]
		[Category("Client-side events")]
		public string OnClientClearButtonClicking
		{
			get
			{
				return (string)(this.ViewState["OnClientClearButtonClicking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientClearButtonClicking"] = value;
			}
		}

		// Token: 0x17002392 RID: 9106
		// (get) Token: 0x06006C5B RID: 27739 RVA: 0x00192D7E File Offset: 0x00190F7E
		// (set) Token: 0x06006C5C RID: 27740 RVA: 0x00192D9E File Offset: 0x00190F9E
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("clearButtonClicked")]
		[Description("Gets or sets the name of the JavaScript function called on clear button clicked")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientClearButtonClicked
		{
			get
			{
				return (string)(this.ViewState["OnClientClearButtonClicked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientClearButtonClicked"] = value;
			}
		}

		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x06006C5D RID: 27741 RVA: 0x00192DB1 File Offset: 0x00190FB1
		// (remove) Token: 0x06006C5E RID: 27742 RVA: 0x00192DC4 File Offset: 0x00190FC4
		public event DropDownTreeEntryEventHandler EntryAdded
		{
			add
			{
				base.Events.AddHandler(RadDropDownTree.EntryAddedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownTree.EntryAddedEvent, value);
			}
		}

		// Token: 0x140000FA RID: 250
		// (add) Token: 0x06006C5F RID: 27743 RVA: 0x00192DD7 File Offset: 0x00190FD7
		// (remove) Token: 0x06006C60 RID: 27744 RVA: 0x00192DEA File Offset: 0x00190FEA
		public event DropDownTreeEntryEventHandler EntryRemoved
		{
			add
			{
				base.Events.AddHandler(RadDropDownTree.EntryRemovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownTree.EntryRemovedEvent, value);
			}
		}

		// Token: 0x140000FB RID: 251
		// (add) Token: 0x06006C61 RID: 27745 RVA: 0x00192DFD File Offset: 0x00190FFD
		// (remove) Token: 0x06006C62 RID: 27746 RVA: 0x00192E10 File Offset: 0x00191010
		public event DropDownTreeEntriesEventHandler EntriesAdded
		{
			add
			{
				base.Events.AddHandler(RadDropDownTree.EntriesAddedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownTree.EntriesAddedEvent, value);
			}
		}

		// Token: 0x140000FC RID: 252
		// (add) Token: 0x06006C63 RID: 27747 RVA: 0x00192E23 File Offset: 0x00191023
		// (remove) Token: 0x06006C64 RID: 27748 RVA: 0x00192E36 File Offset: 0x00191036
		public event DropDownTreeEntriesEventHandler EntriesRemoved
		{
			add
			{
				base.Events.AddHandler(RadDropDownTree.EntriesRemovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDropDownTree.EntriesRemovedEvent, value);
			}
		}

		// Token: 0x06006C67 RID: 27751 RVA: 0x00192E49 File Offset: 0x00191049
		// Note: this type is marked as 'beforefieldinit'.
		static RadDropDownTree()
		{
			RadDropDownTree.EntryAddedEvent = new object();
			RadDropDownTree.EntryRemovedEvent = new object();
			RadDropDownTree.EntriesAddedEvent = new object();
			RadDropDownTree.EntriesRemovedEvent = new object();
			RadDropDownTree.NodeDataBoundEvent = new object();
		}

		// Token: 0x04001D09 RID: 7433
		internal const string EmbeddedTreeName = "EmbeddedTree";

		// Token: 0x04001D0A RID: 7434
		private DropDownTreeEntryCollection _children;

		// Token: 0x04001D0B RID: 7435
		internal IRenderer _renderer;

		// Token: 0x04001D0C RID: 7436
		internal IDropDownEmbeddedTreeRenderer _embeddedTreeRenderer;

		// Token: 0x04001D0D RID: 7437
		internal EmbeddedTreeAdapter _embeddedTreeAdapter;

		// Token: 0x04001D0E RID: 7438
		private ITemplate _dropDownNodeTemplate;

		// Token: 0x04001D0F RID: 7439
		internal DropDownTreeClientState clientState;

		// Token: 0x04001D14 RID: 7444
		private DropDownTreeHeaderFooterControl _header;

		// Token: 0x04001D15 RID: 7445
		private DropDownTreeHeaderFooterControl _footer;

		// Token: 0x04001D16 RID: 7446
		internal DropDownSettings _dropDownSettings;

		// Token: 0x04001D17 RID: 7447
		internal DropDownTreeFilterSettings _filterSettings;

		// Token: 0x04001D18 RID: 7448
		private DropDownTreeButtonSettings _buttonSettings;

		// Token: 0x04001D19 RID: 7449
		private DropDownTreeButtons _localization;

		// Token: 0x04001D1A RID: 7450
		private WebServiceSettings _webServiceSettings;

		// Token: 0x04001D1B RID: 7451
		private readonly AnimationSettings _expandAnimation;

		// Token: 0x04001D1C RID: 7452
		private readonly AnimationSettings _collapseAnimation;

		// Token: 0x04001D1D RID: 7453
		private List<DropDownNodeBinding> _dataBindings;

		// Token: 0x04001D1E RID: 7454
		private List<DropDownTreeEntry> _removedEntries = new List<DropDownTreeEntry>();

		// Token: 0x04001D1F RID: 7455
		private List<DropDownTreeEntry> _addedEntries = new List<DropDownTreeEntry>();

		// Token: 0x04001D20 RID: 7456
		private bool _isCheckedIndicesSet;

		// Token: 0x04001D21 RID: 7457
		private bool _isSelectedIndicesSet;

		// Token: 0x04001D22 RID: 7458
		private bool _fireServerEvents;

		// Token: 0x04001D23 RID: 7459
		internal List<RadTreeNode> NodesForEntries = new List<RadTreeNode>();
	}
}
