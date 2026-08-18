using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.ToolBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000F17 RID: 3863
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadToolBar))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadToolBar), "Telerik.Web.UI.ToolBar.png")]
	[ToolboxData("<{0}:RadToolBar Runat=\"server\"></{0}:RadToolBar>")]
	[Designer("Telerik.Web.Design.RadToolBarDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[DefaultProperty("Items")]
	[DefaultEvent("ButtonClick")]
	[XmlRoot("ToolBar")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[ClientScriptResource("Telerik.Web.UI.RadToolBar", "Telerik.Web.UI.ToolBar.RadToolBarScripts.js")]
	[EmbeddedSkin("ToolBar", "Default", typeof(RadToolBar))]
	[EmbeddedSkin("ToolBar", typeof(RadToolBar))]
	[LightweightRendering]
	[RequiredScript(typeof(Core))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(DropDown))]
	[RequiredScript(typeof(MaterialRipple))]
	public class RadToolBar : ControlItemContainer, IPostBackEventHandler, IRadToolBarItemContainer, IControlItemContainer
	{
		// Token: 0x060092F6 RID: 37622 RVA: 0x002107E0 File Offset: 0x0020E9E0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "causesValidation", this.CausesValidation, true);
			base.DescribeProperty<string>(descriptor, "_cssClass", this.CssClass, null);
			base.DescribeProperty<bool>(descriptor, "enableImageSprites", this.EnableImageSprites, false);
			base.DescribeProperty<bool>(descriptor, "enableRoundedCorners", this.EnableRoundedCorners, false);
			base.DescribeProperty<bool>(descriptor, "enableShadows", this.EnableShadows, false);
			base.DescribeProperty<Orientation>(descriptor, "orientation", this.Orientation, Orientation.Horizontal);
			base.DescribeProperty<string>(descriptor, "postBackUrl", base.ResolveClientUrl(this.PostBackUrl), "");
			base.DescribeProperty<string>(descriptor, "validationGroup", this.ValidationGroup, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060092F7 RID: 37623 RVA: 0x0021089C File Offset: 0x0020EA9C
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "buttonClicked", this.OnClientButtonClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "buttonClicking", this.OnClientButtonClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "checkedStateChanged", this.OnClientCheckedStateChanged);
			RadDataBoundControl.DescribeEvent(descriptor, "checkedStateChanging", this.OnClientCheckedStateChanging);
			RadDataBoundControl.DescribeEvent(descriptor, "contextMenu", this.OnClientContextMenu);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosed", this.OnClientDropDownClosed);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosing", this.OnClientDropDownClosing);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpened", this.OnClientDropDownOpened);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpening", this.OnClientDropDownOpening);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOut", this.OnClientMouseOut);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOver", this.OnClientMouseOver);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x060092F8 RID: 37624 RVA: 0x0021097C File Offset: 0x0020EB7C
		public RadToolBar()
		{
			this._expandAnimation = new ToolBarAnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new ToolBarAnimationSettings("Collapse", this.ViewState);
		}

		// Token: 0x17002E7A RID: 11898
		// (get) Token: 0x060092F9 RID: 37625 RVA: 0x002109DC File Offset: 0x0020EBDC
		internal bool PostBack
		{
			get
			{
				return (RadToolBarEventHandler)base.Events[RadToolBar.ButtonClickEvent] != null || !string.IsNullOrEmpty(this.PostBackUrl) || this.AutoPostBack;
			}
		}

		// Token: 0x17002E7B RID: 11899
		// (get) Token: 0x060092FA RID: 37626 RVA: 0x00210A17 File Offset: 0x0020EC17
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060092FB RID: 37627 RVA: 0x00210A1A File Offset: 0x0020EC1A
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060092FC RID: 37628 RVA: 0x00210A24 File Offset: 0x0020EC24
		protected internal virtual void RaisePostBackEvent(string eventArgument)
		{
			RadToolBarItem radToolBarItem = (RadToolBarItem)this.FindItemByHierarchicalIndex(eventArgument);
			IRadToolBarButton radToolBarButton = radToolBarItem as IRadToolBarButton;
			if (radToolBarButton == null)
			{
				throw new InvalidOperationException(string.Format("Only items of type {0} can initiate postbacks", typeof(IRadToolBarButton)));
			}
			if (radToolBarItem != null && radToolBarItem.Enabled)
			{
				this.PerformValidation(radToolBarButton);
				this.OnButtonClick(new RadToolBarEventArgs(radToolBarItem));
			}
		}

		// Token: 0x060092FD RID: 37629 RVA: 0x00210A80 File Offset: 0x0020EC80
		private void PerformValidation(IRadToolBarButton button)
		{
			if (!button.CausesValidation)
			{
				return;
			}
			if (this.ValidationGroup != button.ValidationGroup)
			{
				this.Page.Validate(button.ValidationGroup);
				return;
			}
			this.Page.Validate(this.ValidationGroup);
		}

		// Token: 0x060092FE RID: 37630 RVA: 0x00210ACC File Offset: 0x0020ECCC
		protected virtual void OnButtonClick(RadToolBarEventArgs e)
		{
			this.RaiseEvent(RadToolBar.ButtonClickEvent, e);
			IRadToolBarButton radToolBarButton = (IRadToolBarButton)e.Item;
			base.RaiseBubbleEvent(this, new CommandEventArgs(radToolBarButton.CommandName, radToolBarButton.CommandArgument));
		}

		// Token: 0x060092FF RID: 37631 RVA: 0x00210B0C File Offset: 0x0020ED0C
		private void RaiseEvent(object eventKey, RadToolBarEventArgs e)
		{
			RadToolBarEventHandler radToolBarEventHandler = base.Events[eventKey] as RadToolBarEventHandler;
			if (radToolBarEventHandler != null)
			{
				radToolBarEventHandler(this, e);
			}
		}

		// Token: 0x06009300 RID: 37632 RVA: 0x00210B36 File Offset: 0x0020ED36
		protected internal override ControlItem CreateItem()
		{
			return new RadToolBarButton();
		}

		// Token: 0x06009301 RID: 37633 RVA: 0x00210B40 File Offset: 0x0020ED40
		protected override Type GetItemTypeFromXmlTagName(string xmlTagName)
		{
			Type[] array = new Type[]
			{
				typeof(RadToolBarButton),
				typeof(RadToolBarDropDown),
				typeof(RadToolBarSplitButton)
			};
			foreach (Type type in array)
			{
				if (type.Name.EndsWith(xmlTagName))
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06009302 RID: 37634 RVA: 0x00210BAE File Offset: 0x0020EDAE
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnItemDataBound(new RadToolBarButtonEventArgs((RadToolBarButton)item));
		}

		// Token: 0x06009303 RID: 37635 RVA: 0x00210BC1 File Offset: 0x0020EDC1
		protected override void RaiseItemCreated(ControlItem item)
		{
			this.OnItemCreated(new RadToolBarEventArgs((RadToolBarItem)item));
		}

		// Token: 0x06009304 RID: 37636 RVA: 0x00210BD4 File Offset: 0x0020EDD4
		protected virtual void OnItemCreated(RadToolBarEventArgs e)
		{
			this.RaiseEvent(RadToolBar.ItemCreatedEvent, e);
		}

		// Token: 0x06009305 RID: 37637 RVA: 0x00210BE2 File Offset: 0x0020EDE2
		protected override void RaiseTemplateNeeded(ControlItem item)
		{
			this.OnTemplateNeeded(new RadToolBarEventArgs((RadToolBarItem)item));
		}

		// Token: 0x06009306 RID: 37638 RVA: 0x00210BF8 File Offset: 0x0020EDF8
		protected virtual void OnItemDataBound(RadToolBarButtonEventArgs e)
		{
			RadToolBarButtonEventHandler radToolBarButtonEventHandler = base.Events[RadToolBar.ButtonDataBoundEvent] as RadToolBarButtonEventHandler;
			if (radToolBarButtonEventHandler != null)
			{
				radToolBarButtonEventHandler(this, e);
			}
		}

		// Token: 0x06009307 RID: 37639 RVA: 0x00210C26 File Offset: 0x0020EE26
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadToolBarItemCollection(this);
		}

		// Token: 0x06009308 RID: 37640 RVA: 0x00210C30 File Offset: 0x0020EE30
		protected internal override ControlItem CreateItem(ClientStateLogEntry logEntry)
		{
			switch (this.GetItemType(logEntry))
			{
			case RadToolBarItemType.DropDown:
				return new RadToolBarDropDown();
			case RadToolBarItemType.SplitButton:
				return new RadToolBarSplitButton();
			default:
				return new RadToolBarButton();
			}
		}

		// Token: 0x06009309 RID: 37641 RVA: 0x00210C68 File Offset: 0x0020EE68
		private RadToolBarItemType GetItemType(ClientStateLogEntry logEntry)
		{
			string empty = string.Empty;
			string key = "itemType";
			if (!logEntry.Data.ContainsKey(key))
			{
				return RadToolBarItemType.Button;
			}
			return (RadToolBarItemType)logEntry.Data[key];
		}

		// Token: 0x17002E7C RID: 11900
		// (get) Token: 0x0600930A RID: 37642 RVA: 0x00210CA2 File Offset: 0x0020EEA2
		protected internal override IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateControlRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x0600930B RID: 37643 RVA: 0x00210CBE File Offset: 0x0020EEBE
		protected internal override IRenderer CreateControlRenderer()
		{
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new ToolBarLiteRenderer(this);
			}
			return new ToolBarClassicRenderer(this);
		}

		// Token: 0x17002E7D RID: 11901
		// (get) Token: 0x0600930C RID: 37644 RVA: 0x00210CD8 File Offset: 0x0020EED8
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadToolBar RadToolBar_{0} RadToolBar_{{0}} RadToolBar_{{0}}_{0}";
				if (base.Attributes["dir"] == "rtl")
				{
					text += " RadToolBar_rtl RadToolBar_{{0}}_rtl";
				}
				return string.Format(text, this.Orientation.ToString("F"));
			}
		}

		// Token: 0x0600930D RID: 37645 RVA: 0x00210D2E File Offset: 0x0020EF2E
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			this.RenderTrialMessage(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				writer.Write("<style type='text/css'>\r\n.RadToolBar,\r\n.RadToolBar .rtbOuter,\r\n.RadToolBar .rtbMiddle,\r\n.RadToolBar .rtbInner,\r\n.RadToolBar .rtbUL,\r\n.RadToolBar .rtbItem,\r\n.RadToolBar .rtbWrap,\r\n.RadToolBar .rtbOut,\r\n.RadToolBar .rtbMid,\r\n.RadToolBar .rtbIn,\r\n.RadToolBar .rtbText\r\n{\r\n    *display: inline-block;\r\n    *float: left;\r\n}\r\n</style>");
			}
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x0600930E RID: 37646 RVA: 0x00210D68 File Offset: 0x0020EF68
		internal void BaseRenderChildren(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}

		// Token: 0x0600930F RID: 37647 RVA: 0x00210D74 File Offset: 0x0020EF74
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			List<JavaScriptConverter> list = new List<JavaScriptConverter>();
			list.Add(new RadToolBarItemConverter());
			list.Add(new AttributeCollectionConverter());
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(list);
			descriptor.AddScriptProperty("itemData", javaScriptSerializer.Serialize(this.Items.VisibleItems));
			if (this.ResolvedRenderMode != RenderMode.Classic)
			{
				descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			}
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (this.PostBack)
			{
				string postbackEventReference = this.GetPostbackEventReference();
				if (!this.DoesRequireValidation && string.IsNullOrEmpty(this.PostBackUrlParameter))
				{
					descriptor.AddProperty("_postBackReference", postbackEventReference);
					descriptor.AddProperty("_simplePostBack", true);
				}
				else
				{
					descriptor.AddProperty("_rawPostBackReference", postbackEventReference);
				}
			}
			ControlItemContainer.AddProperty(descriptor, "_singleClick", this.SingleClick, ToolBarSingleClick.None);
			ControlItemContainer.AddProperty(descriptor, "_skin", base.RuntimeSkin, string.Empty);
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
			base.DescribeComponent(descriptor);
		}

		// Token: 0x06009310 RID: 37648 RVA: 0x00210EB6 File Offset: 0x0020F0B6
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this._toolBarWidth = this.Width;
			this._toolBarHeight = this.Height;
			this.Renderer.AddAttributesToRender(writer);
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06009311 RID: 37649 RVA: 0x00210EE4 File Offset: 0x0020F0E4
		protected override string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(base.GetPostBackOptions(this, "{0}", "{1}", this.PostBackUrlParameter));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x06009312 RID: 37650 RVA: 0x00210F29 File Offset: 0x0020F129
		private string GetPostBackUrlParameter()
		{
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				return "{2}";
			}
			return this.GetChildPostBackUrlParameter(this.Items, "{2}");
		}

		// Token: 0x06009313 RID: 37651 RVA: 0x00210F50 File Offset: 0x0020F150
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private string GetChildPostBackUrlParameter(RadToolBarItemCollection items, string existingParameterValue)
		{
			foreach (object obj in items)
			{
				RadToolBarItem radToolBarItem = (RadToolBarItem)obj;
				if (radToolBarItem is IRadToolBarButton)
				{
					IRadToolBarButton radToolBarButton = (IRadToolBarButton)radToolBarItem;
					if (!string.IsNullOrEmpty(radToolBarButton.PostBackUrl))
					{
						return existingParameterValue;
					}
				}
				if (radToolBarItem is IRadToolBarButtonContainer)
				{
					IRadToolBarButtonContainer radToolBarButtonContainer = (IRadToolBarButtonContainer)radToolBarItem;
					string childPostBackUrlParameter = this.GetChildPostBackUrlParameter(radToolBarButtonContainer.Buttons, existingParameterValue);
					if (!string.IsNullOrEmpty(childPostBackUrlParameter))
					{
						return childPostBackUrlParameter;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06009314 RID: 37652 RVA: 0x00210FF8 File Offset: 0x0020F1F8
		internal override bool RequiresValidation()
		{
			if (base.RequiresValidation())
			{
				this.DoesRequireValidation = true;
			}
			else
			{
				this.DoesRequireValidation = this.DoesChildRequireValidation(this.Items);
			}
			return this.DoesRequireValidation;
		}

		// Token: 0x06009315 RID: 37653 RVA: 0x00211024 File Offset: 0x0020F224
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private bool DoesChildRequireValidation(RadToolBarItemCollection items)
		{
			foreach (object obj in items)
			{
				RadToolBarItem radToolBarItem = (RadToolBarItem)obj;
				if (radToolBarItem is IRadToolBarButton)
				{
					IRadToolBarButton radToolBarButton = (IRadToolBarButton)radToolBarItem;
					if (radToolBarButton.CausesValidation && this.Page.GetValidators(radToolBarButton.ValidationGroup).Count > 0)
					{
						return true;
					}
				}
				if (radToolBarItem is IRadToolBarButtonContainer)
				{
					IRadToolBarButtonContainer radToolBarButtonContainer = (IRadToolBarButtonContainer)radToolBarItem;
					bool flag = this.DoesChildRequireValidation(radToolBarButtonContainer.Buttons);
					if (flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17002E7E RID: 11902
		// (get) Token: 0x06009316 RID: 37654 RVA: 0x002110D8 File Offset: 0x0020F2D8
		// (set) Token: 0x06009317 RID: 37655 RVA: 0x002110F4 File Offset: 0x0020F2F4
		private bool DoesRequireValidation
		{
			get
			{
				if (!this._doesRequireValidationSet)
				{
					this.DoesRequireValidation = this.RequiresValidation();
				}
				return this._doesRequireValidation;
			}
			set
			{
				this._doesRequireValidation = value;
				this._doesRequireValidationSet = true;
			}
		}

		// Token: 0x17002E7F RID: 11903
		// (get) Token: 0x06009318 RID: 37656 RVA: 0x00211104 File Offset: 0x0020F304
		private string PostBackUrlParameter
		{
			get
			{
				if (this._postBackUrlParameter == null)
				{
					this._postBackUrlParameter = this.GetPostBackUrlParameter();
				}
				return this._postBackUrlParameter;
			}
		}

		// Token: 0x06009319 RID: 37657 RVA: 0x00211120 File Offset: 0x0020F320
		protected internal override ControlItem FindItemByHierarchicalIndex(string hierarchicalIndex)
		{
			string[] array = hierarchicalIndex.Split(new char[]
			{
				':'
			});
			if (array.Length == 0)
			{
				return null;
			}
			IList<ControlItem> visibleItems = this.Items.VisibleItems;
			if (array.Length != 1)
			{
				IRadToolBarButtonContainer radToolBarButtonContainer = (IRadToolBarButtonContainer)this.Items.VisibleItems[int.Parse(array[0])];
				return radToolBarButtonContainer.Buttons.VisibleItems[int.Parse(array[1])];
			}
			int num = int.Parse(array[0]);
			if (num >= visibleItems.Count)
			{
				return null;
			}
			return this.Items.VisibleItems[num];
		}

		// Token: 0x0600931A RID: 37658 RVA: 0x002111BC File Offset: 0x0020F3BC
		internal void ClearGroupButtonsCheckedState(string group)
		{
			foreach (RadToolBarButton radToolBarButton in this.GetGroupButtons(group))
			{
				if (radToolBarButton.CheckOnClick)
				{
					radToolBarButton.Checked = false;
				}
			}
		}

		// Token: 0x0600931B RID: 37659 RVA: 0x00211214 File Offset: 0x0020F414
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (string.IsNullOrEmpty(postCollection[base.ClientStateFieldID]))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				this.LoadClientState(javaScriptSerializer.Deserialize<ToolBarClientState>(postCollection[base.ClientStateFieldID]));
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			return false;
		}

		// Token: 0x0600931C RID: 37660 RVA: 0x0021127C File Offset: 0x0020F47C
		private void LoadClientState(ToolBarClientState state)
		{
			if (state.LogEntries != null)
			{
				this.LoadLogEntries(state);
			}
		}

		// Token: 0x0600931D RID: 37661 RVA: 0x00211290 File Offset: 0x0020F490
		private void LoadLogEntries(ToolBarClientState state)
		{
			ClientStateLogPlayer<RadToolBarItem> clientStateLogPlayer = new ClientStateLogPlayer<RadToolBarItem>(this);
			this._clientChanges = clientStateLogPlayer.Play(state.LogEntries);
		}

		// Token: 0x17002E80 RID: 11904
		// (get) Token: 0x0600931E RID: 37662 RVA: 0x002112B6 File Offset: 0x0020F4B6
		// (set) Token: 0x0600931F RID: 37663 RVA: 0x002112BE File Offset: 0x0020F4BE
		[SimplePersistenceSetting]
		internal List<int> CheckedIndices
		{
			get
			{
				return this.GetCheckedButtonsIndices();
			}
			set
			{
				this.CheckButtons(value);
			}
		}

		// Token: 0x06009320 RID: 37664 RVA: 0x002112E0 File Offset: 0x0020F4E0
		private void CheckButtons(List<int> indices)
		{
			IList<RadToolBarItem> allItems = this.GetAllItems();
			int count = allItems.Count;
			int i;
			for (i = 0; i < count; i++)
			{
				RadToolBarButton radToolBarButton = allItems[i] as RadToolBarButton;
				if (radToolBarButton != null)
				{
					if (indices.Exists((int x) => x.Equals(i)))
					{
						radToolBarButton.Checked = true;
					}
					else
					{
						radToolBarButton.Checked = false;
					}
				}
			}
		}

		// Token: 0x06009321 RID: 37665 RVA: 0x00211368 File Offset: 0x0020F568
		private List<int> GetCheckedButtonsIndices()
		{
			IList<RadToolBarItem> allItems = this.GetAllItems();
			List<int> list = new List<int>();
			for (int i = 0; i < allItems.Count; i++)
			{
				RadToolBarButton radToolBarButton = allItems[i] as RadToolBarButton;
				if (radToolBarButton != null && radToolBarButton.Checked)
				{
					list.Add(i);
				}
			}
			return list;
		}

		// Token: 0x17002E81 RID: 11905
		// (get) Token: 0x06009322 RID: 37666 RVA: 0x002113B3 File Offset: 0x0020F5B3
		[Browsable(false)]
		public IList<ClientOperation<RadToolBarItem>> ClientChanges
		{
			get
			{
				return this._clientChanges;
			}
		}

		// Token: 0x17002E82 RID: 11906
		// (get) Token: 0x06009323 RID: 37667 RVA: 0x002113BB File Offset: 0x0020F5BB
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadToolBarItemCollection Items
		{
			get
			{
				return (RadToolBarItemCollection)base.Children;
			}
		}

		// Token: 0x17002E83 RID: 11907
		// (get) Token: 0x06009324 RID: 37668 RVA: 0x002113C8 File Offset: 0x0020F5C8
		// (set) Token: 0x06009325 RID: 37669 RVA: 0x002113E9 File Offset: 0x0020F5E9
		[DefaultValue(Orientation.Horizontal)]
		[ClientControlProperty]
		[ClientPropertyName("orientation")]
		[Category("Behavior")]
		public Orientation Orientation
		{
			get
			{
				return (Orientation)(this.ViewState["Orientation"] ?? Orientation.Horizontal);
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x17002E84 RID: 11908
		// (get) Token: 0x06009326 RID: 37670 RVA: 0x00211401 File Offset: 0x0020F601
		[NotifyParentProperty(true)]
		[Description("The animation played when a dropdown item is opened")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x17002E85 RID: 11909
		// (get) Token: 0x06009327 RID: 37671 RVA: 0x00211409 File Offset: 0x0020F609
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The animation played when a dropdown item is closed")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		public AnimationSettings CollapseAnimation
		{
			get
			{
				return this._collapseAnimation;
			}
		}

		// Token: 0x17002E86 RID: 11910
		// (get) Token: 0x06009328 RID: 37672 RVA: 0x00211411 File Offset: 0x0020F611
		// (set) Token: 0x06009329 RID: 37673 RVA: 0x00211419 File Offset: 0x0020F619
		[ClientPropertyName("validationGroup")]
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the validation group to which this validation control belongs.")]
		[ClientControlProperty]
		public override string ValidationGroup
		{
			get
			{
				return base.ValidationGroup;
			}
			set
			{
				base.ValidationGroup = value;
			}
		}

		// Token: 0x17002E87 RID: 11911
		// (get) Token: 0x0600932A RID: 37674 RVA: 0x00211422 File Offset: 0x0020F622
		// (set) Token: 0x0600932B RID: 37675 RVA: 0x0021142A File Offset: 0x0020F62A
		[UrlProperty("*.aspx")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("postBackUrl")]
		public override string PostBackUrl
		{
			get
			{
				return base.PostBackUrl;
			}
			set
			{
				base.PostBackUrl = value;
			}
		}

		// Token: 0x17002E88 RID: 11912
		// (get) Token: 0x0600932C RID: 37676 RVA: 0x00211433 File Offset: 0x0020F633
		// (set) Token: 0x0600932D RID: 37677 RVA: 0x0021143B File Offset: 0x0020F63B
		[ClientControlProperty]
		[ClientPropertyName("causesValidation")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public override bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		// Token: 0x17002E89 RID: 11913
		// (get) Token: 0x0600932E RID: 37678 RVA: 0x00211444 File Offset: 0x0020F644
		// (set) Token: 0x0600932F RID: 37679 RVA: 0x00211465 File Offset: 0x0020F665
		[Description("Postback to the server when tabs are clicked.")]
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

		// Token: 0x17002E8A RID: 11914
		// (get) Token: 0x06009330 RID: 37680 RVA: 0x0021147D File Offset: 0x0020F67D
		// (set) Token: 0x06009331 RID: 37681 RVA: 0x0021149E File Offset: 0x0020F69E
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("Specifying if child items should have rounded corners.")]
		[Category("Appearance")]
		[ClientPropertyName("enableRoundedCorners")]
		public bool EnableRoundedCorners
		{
			get
			{
				return (bool)(this.ViewState["EnableRoundedCorners"] ?? false);
			}
			set
			{
				this.ViewState["EnableRoundedCorners"] = value;
			}
		}

		// Token: 0x17002E8B RID: 11915
		// (get) Token: 0x06009332 RID: 37682 RVA: 0x002114B6 File Offset: 0x0020F6B6
		// (set) Token: 0x06009333 RID: 37683 RVA: 0x002114D7 File Offset: 0x0020F6D7
		[ClientPropertyName("enableShadows")]
		[DefaultValue(false)]
		[Category("Appearance")]
		[ClientControlProperty]
		[Description("Specifying if child items should have shadows.")]
		public bool EnableShadows
		{
			get
			{
				return (bool)(this.ViewState["EnableShadows"] ?? false);
			}
			set
			{
				this.ViewState["EnableShadows"] = value;
			}
		}

		// Token: 0x17002E8C RID: 11916
		// (get) Token: 0x06009334 RID: 37684 RVA: 0x002114EF File Offset: 0x0020F6EF
		// (set) Token: 0x06009335 RID: 37685 RVA: 0x00211510 File Offset: 0x0020F710
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[ClientPropertyName("enableImageSprites")]
		[Description("A value indicating if an image sprite containers should be used instead of the default images")]
		public bool EnableImageSprites
		{
			get
			{
				return (bool)(this.ViewState["EnableImageSprites"] ?? false);
			}
			set
			{
				this.ViewState["EnableImageSprites"] = value;
			}
		}

		// Token: 0x17002E8D RID: 11917
		// (get) Token: 0x06009336 RID: 37686 RVA: 0x00211528 File Offset: 0x0020F728
		// (set) Token: 0x06009337 RID: 37687 RVA: 0x00211530 File Offset: 0x0020F730
		[ClientPropertyName("_cssClass")]
		[ClientControlProperty]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x17002E8E RID: 11918
		// (get) Token: 0x06009338 RID: 37688 RVA: 0x0021153C File Offset: 0x0020F73C
		// (set) Token: 0x06009339 RID: 37689 RVA: 0x00211571 File Offset: 0x0020F771
		[DefaultValue(ToolBarSingleClick.None)]
		public virtual ToolBarSingleClick SingleClick
		{
			get
			{
				ToolBarSingleClick? toolBarSingleClick = (ToolBarSingleClick?)this.ViewState["SingleClick"];
				if (toolBarSingleClick == null)
				{
					return ToolBarSingleClick.None;
				}
				return toolBarSingleClick.GetValueOrDefault();
			}
			set
			{
				this.ViewState["SingleClick"] = value;
			}
		}

		// Token: 0x17002E8F RID: 11919
		// (get) Token: 0x0600933A RID: 37690 RVA: 0x00211589 File Offset: 0x0020F789
		// (set) Token: 0x0600933B RID: 37691 RVA: 0x002115A9 File Offset: 0x0020F7A9
		[Description("The name of the javascript function called when the control is fully initialized on the client side.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
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

		// Token: 0x17002E90 RID: 11920
		// (get) Token: 0x0600933C RID: 37692 RVA: 0x002115BC File Offset: 0x0020F7BC
		// (set) Token: 0x0600933D RID: 37693 RVA: 0x002115DC File Offset: 0x0020F7DC
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("The name of the javascript function called before a button item is clicked.")]
		[ClientControlEvent]
		[ClientPropertyName("buttonClicking")]
		[Category("Client-side events")]
		public string OnClientButtonClicking
		{
			get
			{
				return (string)(this.ViewState["OnClientButtonClicking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientButtonClicking"] = value;
			}
		}

		// Token: 0x17002E91 RID: 11921
		// (get) Token: 0x0600933E RID: 37694 RVA: 0x002115EF File Offset: 0x0020F7EF
		// (set) Token: 0x0600933F RID: 37695 RVA: 0x0021160F File Offset: 0x0020F80F
		[DefaultValue("")]
		[Description("The name of the javascript function called after a button item has been clicked.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("buttonClicked")]
		[Category("Client-side events")]
		public string OnClientButtonClicked
		{
			get
			{
				return (string)(this.ViewState["OnClientButtonClicked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientButtonClicked"] = value;
			}
		}

		// Token: 0x17002E92 RID: 11922
		// (get) Token: 0x06009340 RID: 37696 RVA: 0x00211622 File Offset: 0x0020F822
		// (set) Token: 0x06009341 RID: 37697 RVA: 0x00211642 File Offset: 0x0020F842
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("dropDownOpening")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called before a dropdown item is opened.")]
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

		// Token: 0x17002E93 RID: 11923
		// (get) Token: 0x06009342 RID: 37698 RVA: 0x00211655 File Offset: 0x0020F855
		// (set) Token: 0x06009343 RID: 37699 RVA: 0x00211675 File Offset: 0x0020F875
		[ClientPropertyName("dropDownOpened")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after a dropdown item is opened.")]
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

		// Token: 0x17002E94 RID: 11924
		// (get) Token: 0x06009344 RID: 37700 RVA: 0x00211688 File Offset: 0x0020F888
		// (set) Token: 0x06009345 RID: 37701 RVA: 0x002116A8 File Offset: 0x0020F8A8
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("dropDownClosing")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called before a dropdown item is closed.")]
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

		// Token: 0x17002E95 RID: 11925
		// (get) Token: 0x06009346 RID: 37702 RVA: 0x002116BB File Offset: 0x0020F8BB
		// (set) Token: 0x06009347 RID: 37703 RVA: 0x002116DB File Offset: 0x0020F8DB
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("dropDownClosed")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after a dropdown item is closed.")]
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

		// Token: 0x17002E96 RID: 11926
		// (get) Token: 0x06009348 RID: 37704 RVA: 0x002116EE File Offset: 0x0020F8EE
		// (set) Token: 0x06009349 RID: 37705 RVA: 0x0021170E File Offset: 0x0020F90E
		[Description("The name of the javascript function called before context menu shows.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("contextMenu")]
		[Category("Client-side events")]
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

		// Token: 0x17002E97 RID: 11927
		// (get) Token: 0x0600934A RID: 37706 RVA: 0x00211721 File Offset: 0x0020F921
		// (set) Token: 0x0600934B RID: 37707 RVA: 0x00211741 File Offset: 0x0020F941
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called after the mouse has hovered a toolbar item.")]
		[ClientPropertyName("mouseOver")]
		[Category("Client-side events")]
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

		// Token: 0x17002E98 RID: 11928
		// (get) Token: 0x0600934C RID: 37708 RVA: 0x00211754 File Offset: 0x0020F954
		// (set) Token: 0x0600934D RID: 37709 RVA: 0x00211774 File Offset: 0x0020F974
		[Description("The name of the javascript function called after the mouse has left an item.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("mouseOut")]
		[Category("Client-side events")]
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

		// Token: 0x17002E99 RID: 11929
		// (get) Token: 0x0600934E RID: 37710 RVA: 0x00211787 File Offset: 0x0020F987
		// (set) Token: 0x0600934F RID: 37711 RVA: 0x002117A7 File Offset: 0x0020F9A7
		[DefaultValue("")]
		[Description("The name of the javascript function called before button is checked.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("checkedStateChanging")]
		[Category("Client-side events")]
		public string OnClientCheckedStateChanging
		{
			get
			{
				return (string)(this.ViewState["OnClientCheckedStateChanging"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientCheckedStateChanging"] = value;
			}
		}

		// Token: 0x17002E9A RID: 11930
		// (get) Token: 0x06009350 RID: 37712 RVA: 0x002117BA File Offset: 0x0020F9BA
		// (set) Token: 0x06009351 RID: 37713 RVA: 0x002117DA File Offset: 0x0020F9DA
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("checkedStateChanged")]
		[Description("The name of the javascript function called after a button checked state is changed.")]
		public string OnClientCheckedStateChanged
		{
			get
			{
				return (string)(this.ViewState["OnClientCheckedStateChanged"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientCheckedStateChanged"] = value;
			}
		}

		// Token: 0x14000165 RID: 357
		// (add) Token: 0x06009352 RID: 37714 RVA: 0x002117ED File Offset: 0x0020F9ED
		// (remove) Token: 0x06009353 RID: 37715 RVA: 0x00211800 File Offset: 0x0020FA00
		public event RadToolBarEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadToolBar.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadToolBar.ItemCreatedEvent, value);
			}
		}

		// Token: 0x14000166 RID: 358
		// (add) Token: 0x06009354 RID: 37716 RVA: 0x00211813 File Offset: 0x0020FA13
		// (remove) Token: 0x06009355 RID: 37717 RVA: 0x00211826 File Offset: 0x0020FA26
		public event RadToolBarEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadToolBar.TemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadToolBar.TemplateNeededEvent, value);
			}
		}

		// Token: 0x06009356 RID: 37718 RVA: 0x00211839 File Offset: 0x0020FA39
		protected virtual void OnTemplateNeeded(RadToolBarEventArgs e)
		{
			this.RaiseEvent(RadToolBar.TemplateNeededEvent, e);
		}

		// Token: 0x14000167 RID: 359
		// (add) Token: 0x06009357 RID: 37719 RVA: 0x00211847 File Offset: 0x0020FA47
		// (remove) Token: 0x06009358 RID: 37720 RVA: 0x0021185A File Offset: 0x0020FA5A
		public event RadToolBarButtonEventHandler ButtonDataBound
		{
			add
			{
				base.Events.AddHandler(RadToolBar.ButtonDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadToolBar.ButtonDataBoundEvent, value);
			}
		}

		// Token: 0x14000168 RID: 360
		// (add) Token: 0x06009359 RID: 37721 RVA: 0x0021186D File Offset: 0x0020FA6D
		// (remove) Token: 0x0600935A RID: 37722 RVA: 0x00211880 File Offset: 0x0020FA80
		public event RadToolBarEventHandler ButtonClick
		{
			add
			{
				base.Events.AddHandler(RadToolBar.ButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadToolBar.ButtonClickEvent, value);
			}
		}

		// Token: 0x0600935B RID: 37723 RVA: 0x00211894 File Offset: 0x0020FA94
		public void LoadContentFile(string xmlFileName)
		{
			string xml = File.ReadAllText(this.Context.Server.MapPath(xmlFileName));
			base.LoadXml(xml);
		}

		// Token: 0x0600935C RID: 37724 RVA: 0x002118BF File Offset: 0x0020FABF
		public IList<RadToolBarItem> GetAllItems()
		{
			return base.GetAllChildren<RadToolBarItem>();
		}

		// Token: 0x0600935D RID: 37725 RVA: 0x002118C8 File Offset: 0x0020FAC8
		public IList<RadToolBarButton> GetGroupButtons(string group)
		{
			List<RadToolBarButton> list = new List<RadToolBarButton>();
			foreach (RadToolBarItem radToolBarItem in this.GetAllItems())
			{
				RadToolBarButton radToolBarButton = radToolBarItem as RadToolBarButton;
				if (radToolBarItem.ItemType == RadToolBarItemType.Button && radToolBarButton.Group == group)
				{
					list.Add(radToolBarButton);
				}
			}
			return list;
		}

		// Token: 0x0600935E RID: 37726 RVA: 0x0021193C File Offset: 0x0020FB3C
		public RadToolBarButton GetCheckedGroupButton(string group)
		{
			foreach (RadToolBarButton radToolBarButton in this.GetGroupButtons(group))
			{
				if (radToolBarButton.CheckOnClick && radToolBarButton.Checked)
				{
					return radToolBarButton;
				}
			}
			return null;
		}

		// Token: 0x0600935F RID: 37727 RVA: 0x0021199C File Offset: 0x0020FB9C
		public RadToolBarItem FindItemByText(string text)
		{
			return base.FindChildByText<RadToolBarItem>(text);
		}

		// Token: 0x06009360 RID: 37728 RVA: 0x002119A5 File Offset: 0x0020FBA5
		public RadToolBarItem FindItemByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadToolBarItem>(text, ignoreCase);
		}

		// Token: 0x06009361 RID: 37729 RVA: 0x002119AF File Offset: 0x0020FBAF
		public RadToolBarItem FindItemByValue(string value)
		{
			return this.FindItemByValue(value, false);
		}

		// Token: 0x06009362 RID: 37730 RVA: 0x002119BC File Offset: 0x0020FBBC
		public RadToolBarItem FindItemByValue(string value, bool ignoreCase)
		{
			IList<RadToolBarItem> allChildren = base.GetAllChildren<RadToolBarItem>();
			foreach (RadToolBarItem radToolBarItem in allChildren)
			{
				if (!(radToolBarItem is RadToolBarDropDown) && string.Compare(radToolBarItem.Value, value, ignoreCase) == 0)
				{
					return radToolBarItem;
				}
			}
			return null;
		}

		// Token: 0x06009363 RID: 37731 RVA: 0x00211A48 File Offset: 0x0020FC48
		public override TControlItem FindChildByValue<TControlItem>(string value)
		{
			return this.FindChildByValue<TControlItem>((TControlItem item) => item.Value == value);
		}

		// Token: 0x06009364 RID: 37732 RVA: 0x00211AA0 File Offset: 0x0020FCA0
		public override TControlItem FindChildByValue<TControlItem>(string value, bool ignoreCase)
		{
			return this.FindChildByValue<TControlItem>((TControlItem item) => string.Compare(item.Value, value, ignoreCase) == 0);
		}

		// Token: 0x06009365 RID: 37733 RVA: 0x00211AD4 File Offset: 0x0020FCD4
		private TControlItem FindChildByValue<TControlItem>(Predicate<TControlItem> predicate) where TControlItem : ControlItem
		{
			IList<RadToolBarItem> allItems = this.GetAllItems();
			foreach (RadToolBarItem radToolBarItem in allItems)
			{
				TControlItem tcontrolItem = radToolBarItem as TControlItem;
				if (tcontrolItem != null && predicate(tcontrolItem))
				{
					return tcontrolItem;
				}
			}
			return default(TControlItem);
		}

		// Token: 0x06009366 RID: 37734 RVA: 0x00211B50 File Offset: 0x0020FD50
		public RadToolBarItem FindItemByUrl(string url)
		{
			IList<RadToolBarItem> allItems = this.GetAllItems();
			foreach (RadToolBarItem radToolBarItem in allItems)
			{
				IRadToolBarButton radToolBarButton = radToolBarItem as IRadToolBarButton;
				if (radToolBarButton != null && string.Compare(radToolBarItem.ResolveUrl(radToolBarButton.NavigateUrl), HttpUtility.UrlDecode(url), true) == 0)
				{
					return radToolBarItem;
				}
			}
			return null;
		}

		// Token: 0x06009367 RID: 37735 RVA: 0x00211BC8 File Offset: 0x0020FDC8
		public IRadToolBarButton FindButtonByCommandName(string commandName)
		{
			IList<RadToolBarItem> allItems = this.GetAllItems();
			foreach (RadToolBarItem radToolBarItem in allItems)
			{
				IRadToolBarButton radToolBarButton = radToolBarItem as IRadToolBarButton;
				if (radToolBarButton != null && string.Compare(radToolBarButton.CommandName, commandName, true) == 0)
				{
					return radToolBarButton;
				}
			}
			return null;
		}

		// Token: 0x06009368 RID: 37736 RVA: 0x00211C38 File Offset: 0x0020FE38
		public RadToolBarItem FindItem(Predicate<RadToolBarItem> match)
		{
			return base.FindChild<RadToolBarItem>(match);
		}

		// Token: 0x06009369 RID: 37737 RVA: 0x00211C41 File Offset: 0x0020FE41
		// Note: this type is marked as 'beforefieldinit'.
		static RadToolBar()
		{
			RadToolBar.ItemCreatedEvent = new object();
			RadToolBar.TemplateNeededEvent = new object();
			RadToolBar.ButtonDataBoundEvent = new object();
			RadToolBar.ButtonClickEvent = new object();
		}

		// Token: 0x04002A45 RID: 10821
		private IRenderer _renderer;

		// Token: 0x04002A46 RID: 10822
		private AnimationSettings _expandAnimation;

		// Token: 0x04002A47 RID: 10823
		private AnimationSettings _collapseAnimation;

		// Token: 0x04002A48 RID: 10824
		internal Unit _toolBarWidth = Unit.Empty;

		// Token: 0x04002A49 RID: 10825
		internal Unit _toolBarHeight = Unit.Empty;

		// Token: 0x04002A4A RID: 10826
		private bool _doesRequireValidation;

		// Token: 0x04002A4B RID: 10827
		private bool _doesRequireValidationSet;

		// Token: 0x04002A4C RID: 10828
		private string _postBackUrlParameter;

		// Token: 0x04002A4D RID: 10829
		private IList<ClientOperation<RadToolBarItem>> _clientChanges = new List<ClientOperation<RadToolBarItem>>();
	}
}
