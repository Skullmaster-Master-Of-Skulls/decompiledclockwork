using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.Navigation;

namespace Telerik.Web.UI
{
	// Token: 0x0200061C RID: 1564
	[EmbeddedSkin("Navigation", "Default", typeof(RadNavigation))]
	[LightweightRendering]
	[ToolboxBitmap(typeof(RadNavigation), "Telerik.Web.UI.Navigation.png")]
	[ToolboxData("<{0}:RadNavigation runat=\"server\"></{0}:RadNavigation>")]
	[TelerikToolboxCategory("Navigation")]
	[Designer("Telerik.Web.Design.RadNavigationDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins), 1)]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadNavigation))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(MaterialRipple))]
	[EmbeddedSkin("Navigation", typeof(RadNavigation))]
	[RequiredScript(typeof(DropDown), 2)]
	[ClientScriptResource("Telerik.Web.UI.RadNavigation", "Telerik.Web.UI.Navigation.RadNavigationScripts.js")]
	public class RadNavigation : RadDataBoundControl, IHierarchicalItemContainer, IItemContainer, INavigationNodeContainer
	{
		// Token: 0x06003896 RID: 14486 RVA: 0x000BA440 File Offset: 0x000B8640
		private IList<NavigationNode> GetAllChildren()
		{
			IList<NavigationNode> allNodes = new List<NavigationNode>();
			this.Nodes.ForEach(delegate(NavigationNode node)
			{
				this.AddNodes(node, allNodes);
			});
			return allNodes;
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000BA4A0 File Offset: 0x000B86A0
		private void AddNodes(NavigationNode node, IList<NavigationNode> allNodes)
		{
			allNodes.Add(node);
			node.Nodes.ForEach(delegate(NavigationNode subNode)
			{
				this.AddNodes(subNode, allNodes);
			});
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000BA4E4 File Offset: 0x000B86E4
		internal void AssignReferencesToInnerTree(NavigationNodeCollection nodes, RadNavigation radNavigation)
		{
			foreach (NavigationNode navigationNode in nodes)
			{
				navigationNode.Owner = radNavigation;
				if (!navigationNode.IsTemplateInstantiated)
				{
					if (!((Control)nodes.NodesContainer).Controls.Contains(navigationNode))
					{
						((Control)nodes.NodesContainer).Controls.Add(navigationNode);
					}
					navigationNode.ApplyTemplate(navigationNode.TemplateToApply);
				}
				if (navigationNode.ContentTemplate != null && !navigationNode.IsContentTemplateInstantiated)
				{
					if (!navigationNode.Controls.Contains(navigationNode.ContentTemplateContainer))
					{
						navigationNode.Controls.Add(navigationNode.ContentTemplateContainer);
					}
					navigationNode.ApplyContentTemplate();
				}
				this.AssignReferencesToInnerTree(navigationNode.Nodes, radNavigation);
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06003899 RID: 14489 RVA: 0x000BA5C4 File Offset: 0x000B87C4
		// (set) Token: 0x0600389A RID: 14490 RVA: 0x000BA5CC File Offset: 0x000B87CC
		internal string CurrentSiteMapUrl { get; set; }

		// Token: 0x0600389B RID: 14491 RVA: 0x000BA5D5 File Offset: 0x000B87D5
		internal void CreateTemplates()
		{
			if (this.Nodes.Count > 0)
			{
				this.AssignReferencesToInnerTree(this.Nodes, this);
			}
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000BA5F2 File Offset: 0x000B87F2
		internal void CreateContentTemplates()
		{
			if (this.Nodes.Count > 0)
			{
				this.AssignReferencesToInnerTree(this.Nodes, this);
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000BA610 File Offset: 0x000B8810
		IItem IItemContainer.CreateItem()
		{
			NavigationNode navigationNode = new NavigationNode(this);
			this.RaiseTemplateNeeded(navigationNode);
			return navigationNode;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000BA62C File Offset: 0x000B882C
		protected void RaiseTemplateNeeded(NavigationNode node)
		{
			this.RaiseTemplateNeededEvent(node);
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000BA638 File Offset: 0x000B8838
		private void RaiseTemplateNeededEvent(IItem node)
		{
			NavigationNodeEventHandler navigationNodeEventHandler = (NavigationNodeEventHandler)base.Events[RadNavigation.templateNeededEvent];
			NavigationNode node2 = node as NavigationNode;
			NavigationNodeEventArguments e = new NavigationNodeEventArguments(node2);
			if (navigationNodeEventHandler != null)
			{
				navigationNodeEventHandler(this, e);
			}
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000BA674 File Offset: 0x000B8874
		void IItemContainer.RaiseItemDataBound(IItem node)
		{
			NavigationNodeEventHandler navigationNodeEventHandler = (NavigationNodeEventHandler)base.Events[RadNavigation.navigationNodeDataBoundEvent];
			NavigationNode navigationNode = node as NavigationNode;
			navigationNode.Owner = this;
			navigationNode.TemplateData = new Dictionary<string, object>();
			foreach (string text in this.DataKeyNames)
			{
				try
				{
					object value = DataBinder.Eval(navigationNode.DataItem, text);
					navigationNode.TemplateData.Add(text, value);
				}
				catch (Exception)
				{
					throw new Exception("The data Node does not contain the " + text + "data field");
				}
			}
			NavigationNodeEventArguments e = new NavigationNodeEventArguments(navigationNode);
			if (navigationNodeEventHandler != null)
			{
				navigationNodeEventHandler(this, e);
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x000BA728 File Offset: 0x000B8928
		IList IItemContainer.Children
		{
			get
			{
				return this.Nodes;
			}
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000BA730 File Offset: 0x000B8930
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				foreach (NavigationNode navigationNode in this.Nodes)
				{
					navigationNode.DataBind();
				}
				return;
			}
			this.Nodes.Clear();
			ControlDataBinder controlDataBinder = new ControlDataBinder(this);
			IHierarchicalEnumerable hierarchyData = this.GetHierarchyData(data);
			if (hierarchyData != null)
			{
				controlDataBinder.BindToHierarchicalData(hierarchyData);
				return;
			}
			DataView dataView = data as DataView;
			if (dataView != null && !base.DesignMode && !string.IsNullOrEmpty(this.DataFieldID) && !string.IsNullOrEmpty(this.DataFieldParentID))
			{
				controlDataBinder.BindToDataTable(dataView.ToTable(), this.DataFieldID, this.DataFieldParentID);
				return;
			}
			controlDataBinder.BindToEnumerableData(data, this.DataFieldID, this.DataFieldParentID);
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000BA80C File Offset: 0x000B8A0C
		private Control FindDataSourceControl()
		{
			Control control = this;
			Control control2 = null;
			while (control2 == null && control != this.Page)
			{
				control = control.NamingContainer;
				if (control == null)
				{
					break;
				}
				control2 = control.FindControl(this.DataSourceID);
			}
			return control2;
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000BA844 File Offset: 0x000B8A44
		protected override IDataSource GetDataSource()
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				return base.GetDataSource();
			}
			Control control = this.FindDataSourceControl();
			IHierarchicalDataSource hierarchicalDataSource = control as IHierarchicalDataSource;
			if (hierarchicalDataSource != null)
			{
				SiteMapDataSource siteMapDataSource = control as SiteMapDataSource;
				if (siteMapDataSource != null)
				{
					IHierarchyData currentNode = siteMapDataSource.Provider.CurrentNode;
					if (currentNode != null)
					{
						this.CurrentSiteMapUrl = currentNode.Path;
					}
				}
				return new DecoratingDataSource(hierarchicalDataSource);
			}
			return base.GetDataSource();
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000BA8A4 File Offset: 0x000B8AA4
		private IHierarchicalEnumerable GetHierarchyData(IEnumerable data)
		{
			IHierarchicalEnumerable result = null;
			IHierarchicalEnumerable hierarchicalEnumerable = data as IHierarchicalEnumerable;
			if (this.GetDataSource() is IHierarchicalDataSource)
			{
				IHierarchicalDataSource hierarchicalDataSource = (IHierarchicalDataSource)this.GetDataSource();
				result = hierarchicalDataSource.GetHierarchicalView("").Select();
			}
			else if (this.DataSource is IHierarchicalDataSource)
			{
				IHierarchicalDataSource hierarchicalDataSource2 = (IHierarchicalDataSource)this.DataSource;
				result = hierarchicalDataSource2.GetHierarchicalView("").Select();
			}
			else if (hierarchicalEnumerable != null)
			{
				result = hierarchicalEnumerable;
			}
			return result;
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000BA918 File Offset: 0x000B8B18
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptConverter[] converters = new JavaScriptConverter[]
			{
				new NavigationNodeJavaScriptConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			if (this.Nodes.Count > 0)
			{
				descriptor.AddScriptProperty("nodesData", javaScriptSerializer.Serialize(this.Nodes));
			}
			descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
			this.AriaSettings.Describe(descriptor);
			this.AriaSettings.MenuButton.Describe(descriptor, "_menuButtonAriaSettings");
			if (this._keyboardNavigationSettings != null)
			{
				this.KeyboardNavigationSettings.Describe(descriptor);
			}
			base.DescribeProperty<string>(descriptor, "_skin", base.RuntimeSkin, string.Empty);
			base.DescribeProperty<bool>(descriptor, "_enabled", this.Enabled, true);
			base.DescribeProperty<RadNavigationMenuButtonPostion>(descriptor, "_sandwichPosition", this.MenuButtonPosition, RadNavigationMenuButtonPostion.Right);
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			if (!string.IsNullOrEmpty(this.ClientNodeTemplate))
			{
				descriptor.AddProperty("_clientTemplate", this.ClientNodeTemplate);
			}
			RadDataBoundControl.DescribeEvent(descriptor, "templateDataBound", this.OnClientTemplateDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeMouseLeave", this.OnClientNodeMouseLeave);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeMouseEnter", this.OnClientNodeMouseEnter);
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				if (!string.IsNullOrEmpty(this.DataTextField))
				{
					descriptor.AddProperty("_dataTextField", this.DataTextField);
				}
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("_clientDataSourceID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("_clientDataSourceID", this.ClientDataSourceID);
				}
				descriptor.AddProperty("_dataFieldParentID", this.DataFieldParentID);
				descriptor.AddProperty("_dataFieldID", this.DataFieldID);
				descriptor.AddProperty("_dataNavigateUrlField", this.DataNavigateUrlField);
			}
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000BAB30 File Offset: 0x000B8D30
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "nodeExpanding", this.OnClientNodeExpanding);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeExpanded", this.OnClientNodeExpanded);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeCollapsing", this.OnClientNodeCollapsing);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeCollapsed", this.OnClientNodeCollapsed);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeClicking", this.OnClientNodeClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeClicked", this.OnClientNodeClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "nodesPopulating", this.OnClientNodesPopulating);
			RadDataBoundControl.DescribeEvent(descriptor, "nodesPopulated", this.OnClientNodesPopulated);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x000BABDD File Offset: 0x000B8DDD
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000BABE0 File Offset: 0x000B8DE0
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Nodes.Count > 0)
			{
				this.RenderRootElements(writer);
			}
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000BABF8 File Offset: 0x000B8DF8
		protected internal void RenderRootElements(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnvRootGroupWrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this.Nodes.Count > 0)
			{
				this.RenderNodes(writer);
			}
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnvHiddenGroups");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.Write(string.Empty);
			writer.RenderEndTag();
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000BAC5C File Offset: 0x000B8E5C
		protected internal void RenderNodes(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RadNavigation.Styles.Combine(new string[]
			{
				"rnvRootGroup",
				"rnvGroup"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			for (int i = 0; i < this.Nodes.Count; i++)
			{
				NavigationNode navigationNode = this.Nodes[i];
				if (i == 0)
				{
					navigationNode.IsFirst = true;
				}
				if (i == this.Nodes.Count - 1)
				{
					navigationNode.IsLast = true;
				}
				navigationNode.IsRoot = true;
				navigationNode.Renderer.RenderContents(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000BACF3 File Offset: 0x000B8EF3
		protected override void OnInit(EventArgs e)
		{
			if (this.Page != null && this.Page.IsPostBack)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000BAD11 File Offset: 0x000B8F11
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.RequiresDataBinding)
			{
				this.DataBind();
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x060038AE RID: 14510 RVA: 0x000BAD28 File Offset: 0x000B8F28
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadNavigation_{0}";
				if (!this.Enabled)
				{
					text = RadNavigation.Styles.Combine(new string[]
					{
						text,
						"rnvDisabled"
					});
				}
				if (base.Attributes["dir"] == "rtl")
				{
					text = RadNavigation.Styles.Combine(new string[]
					{
						"RadNavigation",
						"RadNavigation_rtl",
						text
					});
				}
				else
				{
					text = RadNavigation.Styles.Combine(new string[]
					{
						"RadNavigation",
						text
					});
				}
				return text;
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x060038AF RID: 14511 RVA: 0x000BADB8 File Offset: 0x000B8FB8
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x000BADBC File Offset: 0x000B8FBC
		// (set) Token: 0x060038B1 RID: 14513 RVA: 0x000BADDD File Offset: 0x000B8FDD
		[Description("Maximum levels to populate from the datasource")]
		[Category("Data")]
		[DefaultValue(-1)]
		public virtual int MaxDataBindDepth
		{
			get
			{
				return (int)(this.ViewState["MaxDataBindDepth"] ?? -1);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["MaxDataBindDepth"] = value;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000BAE04 File Offset: 0x000B9004
		// (set) Token: 0x060038B3 RID: 14515 RVA: 0x000BAE24 File Offset: 0x000B9024
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets or sets the template for displaying the nodes in RadNavigation.")]
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

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000BAE37 File Offset: 0x000B9037
		// (set) Token: 0x060038B5 RID: 14517 RVA: 0x000BAE62 File Offset: 0x000B9062
		[Description("Indicating the position of the image within the node.")]
		[DefaultValue(RadNavigationImagePostion.Left)]
		public RadNavigationImagePostion ImagePosition
		{
			get
			{
				if (this.ViewState["ImagePosition"] == null)
				{
					return RadNavigationImagePostion.Left;
				}
				return (RadNavigationImagePostion)this.ViewState["ImagePosition"];
			}
			set
			{
				this.ViewState["ImagePosition"] = value;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000BAE7A File Offset: 0x000B907A
		// (set) Token: 0x060038B7 RID: 14519 RVA: 0x000BAEA5 File Offset: 0x000B90A5
		[Description("Indicating the position of the menuButton.")]
		[DefaultValue(RadNavigationMenuButtonPostion.Right)]
		public RadNavigationMenuButtonPostion MenuButtonPosition
		{
			get
			{
				if (this.ViewState["MenuButtonPosition"] == null)
				{
					return RadNavigationMenuButtonPostion.Right;
				}
				return (RadNavigationMenuButtonPostion)this.ViewState["MenuButtonPosition"];
			}
			set
			{
				this.ViewState["MenuButtonPosition"] = value;
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x060038B8 RID: 14520 RVA: 0x000BAEBD File Offset: 0x000B90BD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The animation played when the dropdown is opened")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Category("Behavior")]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x060038B9 RID: 14521 RVA: 0x000BAEC5 File Offset: 0x000B90C5
		[Description("The animation played when the dropdown is closed")]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public AnimationSettings CollapseAnimation
		{
			get
			{
				return this._collapseAnimation;
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x000BAECD File Offset: 0x000B90CD
		// (set) Token: 0x060038BB RID: 14523 RVA: 0x000BAEED File Offset: 0x000B90ED
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataFieldID
		{
			get
			{
				return (string)(this.ViewState["DataFieldID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataFieldID"] = value;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x000BAF00 File Offset: 0x000B9100
		// (set) Token: 0x060038BD RID: 14525 RVA: 0x000BAF20 File Offset: 0x000B9120
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataFieldParentID
		{
			get
			{
				return (string)(this.ViewState["DataFieldParentID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataFieldParentID"] = value;
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000BAF33 File Offset: 0x000B9133
		// (set) Token: 0x060038BF RID: 14527 RVA: 0x000BAF53 File Offset: 0x000B9153
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataTextField
		{
			get
			{
				return (string)(this.ViewState["DataTextField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x000BAF66 File Offset: 0x000B9166
		// (set) Token: 0x060038C1 RID: 14529 RVA: 0x000BAF86 File Offset: 0x000B9186
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataTextFormatString
		{
			get
			{
				return (string)(this.ViewState["DataTextFormatString"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataTextFormatString"] = value;
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x000BAF99 File Offset: 0x000B9199
		// (set) Token: 0x060038C3 RID: 14531 RVA: 0x000BAFB9 File Offset: 0x000B91B9
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataNavigateUrlField
		{
			get
			{
				return (string)(this.ViewState["DataNavigateUrlField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataNavigateUrlField"] = value;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x000BAFCC File Offset: 0x000B91CC
		// (set) Token: 0x060038C5 RID: 14533 RVA: 0x000BAFED File Offset: 0x000B91ED
		[Description("Comma delimited list of data-field Names")]
		[TypeConverter(typeof(ListConverter))]
		[Category("Data")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string[] DataKeyNames
		{
			get
			{
				return (string[])(this.ViewState["DataKeyNames"] ?? new string[0]);
			}
			set
			{
				this.ViewState["DataKeyNames"] = value;
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x000BB000 File Offset: 0x000B9200
		// (set) Token: 0x060038C7 RID: 14535 RVA: 0x000BB008 File Offset: 0x000B9208
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[TemplateContainer(typeof(NavigationNode))]
		public ITemplate NodeTemplate { get; set; }

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x000BB011 File Offset: 0x000B9211
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public NavigationNodeCollection Nodes
		{
			get
			{
				if (this._nodes == null)
				{
					this._nodes = new NavigationNodeCollection(this, this);
				}
				return this._nodes;
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x060038C9 RID: 14537 RVA: 0x000BB02E File Offset: 0x000B922E
		public override RenderMode ResolvedRenderMode
		{
			get
			{
				return RenderMode.Lightweight;
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x000BB031 File Offset: 0x000B9231
		// (set) Token: 0x060038CB RID: 14539 RVA: 0x000BB052 File Offset: 0x000B9252
		[Description("When set to true enables support for WAI-ARIA.")]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientPropertyName("enableAriaSupport")]
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

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x000BB06C File Offset: 0x000B926C
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the object that controls the Wai-Aria settings applied on the control's element.")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public NavigationWaiAriaSettings AriaSettings
		{
			get
			{
				NavigationWaiAriaSettings result;
				if ((result = this._ariaSettings) == null)
				{
					result = (this._ariaSettings = new NavigationWaiAriaSettings());
				}
				return result;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x060038CD RID: 14541 RVA: 0x000BB094 File Offset: 0x000B9294
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[Description("Keyboard navigation settings")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		public KeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				KeyboardNavigationSettings result;
				if ((result = this._keyboardNavigationSettings) == null)
				{
					result = (this._keyboardNavigationSettings = new KeyboardNavigationSettings());
				}
				return result;
			}
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000BB0B9 File Offset: 0x000B92B9
		public RadNavigation()
		{
			this._expandAnimation = new AnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new AnimationSettings("Collapse", this.ViewState);
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000BB0ED File Offset: 0x000B92ED
		public override void DataBind()
		{
			base.DataBind();
			this.CreateTemplates();
			this.CreateContentTemplates();
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000BB101 File Offset: 0x000B9301
		public IList<NavigationNode> GetAllNodes()
		{
			return this.GetAllChildren();
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000BB124 File Offset: 0x000B9324
		public NavigationNode FindNodeByText(string text)
		{
			return this.GetAllChildren().FirstOrDefault((NavigationNode node) => node.Text == text);
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000BB170 File Offset: 0x000B9370
		public NavigationNode FindNodeByUrl(string url)
		{
			return this.GetAllChildren().FirstOrDefault((NavigationNode node) => node.NavigateUrl == url);
		}

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x060038D3 RID: 14547 RVA: 0x000BB1A1 File Offset: 0x000B93A1
		// (remove) Token: 0x060038D4 RID: 14548 RVA: 0x000BB1B4 File Offset: 0x000B93B4
		public event NavigationNodeEventHandler NodeDataBound
		{
			add
			{
				base.Events.AddHandler(RadNavigation.navigationNodeDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadNavigation.navigationNodeDataBoundEvent, value);
			}
		}

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x060038D5 RID: 14549 RVA: 0x000BB1C7 File Offset: 0x000B93C7
		// (remove) Token: 0x060038D6 RID: 14550 RVA: 0x000BB1DA File Offset: 0x000B93DA
		public event NavigationNodeEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadNavigation.templateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadNavigation.templateNeededEvent, value);
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000BB1ED File Offset: 0x000B93ED
		// (set) Token: 0x060038D8 RID: 14552 RVA: 0x000BB20D File Offset: 0x000B940D
		[Category("Client-side events")]
		[Description("The JavaScript function executed when RadNavigation is initialized")]
		[ClientPropertyName("load")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x060038D9 RID: 14553 RVA: 0x000BB220 File Offset: 0x000B9420
		// (set) Token: 0x060038DA RID: 14554 RVA: 0x000BB240 File Offset: 0x000B9440
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("templateDataBound")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when the client template for a node is evaluated")]
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

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000BB253 File Offset: 0x000B9453
		// (set) Token: 0x060038DC RID: 14556 RVA: 0x000BB273 File Offset: 0x000B9473
		[ClientPropertyName("nodeMouseEnter")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when the mouse enters a node")]
		public string OnClientNodeMouseEnter
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeMouseEnter"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeMouseEnter"] = value;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000BB286 File Offset: 0x000B9486
		// (set) Token: 0x060038DE RID: 14558 RVA: 0x000BB2A6 File Offset: 0x000B94A6
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when the mouse leaves a node")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("nodeMouseLeave")]
		public string OnClientNodeMouseLeave
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeMouseLeave"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeMouseLeave"] = value;
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000BB2B9 File Offset: 0x000B94B9
		// (set) Token: 0x060038E0 RID: 14560 RVA: 0x000BB2D9 File Offset: 0x000B94D9
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientPropertyName("nodeClicking")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The JavaScript function executed on NavigationNode clicking")]
		public string OnClientNodeClicking
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeClicking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeClicking"] = value;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000BB2EC File Offset: 0x000B94EC
		// (set) Token: 0x060038E2 RID: 14562 RVA: 0x000BB30C File Offset: 0x000B950C
		[ClientPropertyName("nodeClicked")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The JavaScript function executed on NavigationNode clicked")]
		public string OnClientNodeClicked
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeClicked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeClicked"] = value;
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000BB31F File Offset: 0x000B951F
		// (set) Token: 0x060038E4 RID: 14564 RVA: 0x000BB33F File Offset: 0x000B953F
		[ClientPropertyName("nodesPopulating")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Description("The JavaScript function executed on NavigationNodes populating")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientNodesPopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientNodesPopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodesPopulating"] = value;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000BB352 File Offset: 0x000B9552
		// (set) Token: 0x060038E6 RID: 14566 RVA: 0x000BB372 File Offset: 0x000B9572
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("nodesPopulated")]
		[Category("Client-side events")]
		[Description("The JavaScript function executed on NavigationNodes populated")]
		[DefaultValue("")]
		public string OnClientNodesPopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientNodesPopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodesPopulated"] = value;
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000BB385 File Offset: 0x000B9585
		// (set) Token: 0x060038E8 RID: 14568 RVA: 0x000BB3A5 File Offset: 0x000B95A5
		[Description("The client-side event this is fired when the Node is about to be expanded.")]
		[ClientPropertyName("nodeExpanding")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Bindable(false)]
		public string OnClientNodeExpanding
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeExpanding"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeExpanding"] = value;
			}
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x060038E9 RID: 14569 RVA: 0x000BB3B8 File Offset: 0x000B95B8
		// (set) Token: 0x060038EA RID: 14570 RVA: 0x000BB3D8 File Offset: 0x000B95D8
		[Description("The client-side event this is fired when the Node is being expanded.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("nodeExpanded")]
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		public string OnClientNodeExpanded
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeExpanded"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeExpanded"] = value;
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x060038EB RID: 14571 RVA: 0x000BB3EB File Offset: 0x000B95EB
		// (set) Token: 0x060038EC RID: 14572 RVA: 0x000BB40B File Offset: 0x000B960B
		[DefaultValue("")]
		[ClientPropertyName("nodeCollapsing")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(false)]
		[Description("The client-side event that is fired when the Node is about to be collapsed.")]
		public string OnClientNodeCollapsing
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeCollapsing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeCollapsing"] = value;
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x060038ED RID: 14573 RVA: 0x000BB41E File Offset: 0x000B961E
		// (set) Token: 0x060038EE RID: 14574 RVA: 0x000BB43E File Offset: 0x000B963E
		[Description("The client-side event that is fired when the Node is being collapsed.")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("nodeCollapsed")]
		[DefaultValue("")]
		[Bindable(false)]
		public string OnClientNodeCollapsed
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeCollapsed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeCollapsed"] = value;
			}
		}

		// Token: 0x04000F0A RID: 3850
		private NavigationNodeCollection _nodes;

		// Token: 0x04000F0B RID: 3851
		private readonly AnimationSettings _expandAnimation;

		// Token: 0x04000F0C RID: 3852
		private readonly AnimationSettings _collapseAnimation;

		// Token: 0x04000F0D RID: 3853
		private static readonly object navigationNodeDataBoundEvent = new object();

		// Token: 0x04000F0E RID: 3854
		private static readonly object templateNeededEvent = new object();

		// Token: 0x04000F0F RID: 3855
		private NavigationWaiAriaSettings _ariaSettings;

		// Token: 0x04000F10 RID: 3856
		private KeyboardNavigationSettings _keyboardNavigationSettings;

		// Token: 0x0200061D RID: 1565
		internal static class Styles
		{
			// Token: 0x060038F0 RID: 14576 RVA: 0x000BB467 File Offset: 0x000B9667
			internal static string Combine(params string[] classNames)
			{
				return string.Join(" ", classNames).Trim();
			}

			// Token: 0x04000F13 RID: 3859
			internal const string RadNavigationCssClass = "RadNavigation";

			// Token: 0x04000F14 RID: 3860
			internal const string RadNavigationRTLCssClass = "RadNavigation_rtl";

			// Token: 0x04000F15 RID: 3861
			internal const string Horizontal = "rnvHorizontal";

			// Token: 0x04000F16 RID: 3862
			internal const string Vertical = "rnvVertical";

			// Token: 0x04000F17 RID: 3863
			internal const string RootGroup = "rnvRootGroup";

			// Token: 0x04000F18 RID: 3864
			internal const string Group = "rnvGroup";

			// Token: 0x04000F19 RID: 3865
			internal const string Slide = "rnvSlide";

			// Token: 0x04000F1A RID: 3866
			internal const string Popup = "radPopup rnvPopup";

			// Token: 0x04000F1B RID: 3867
			internal const string RTLPopup = "rnvPopup_rtl";

			// Token: 0x04000F1C RID: 3868
			internal const string Ul = "rnvUL";

			// Token: 0x04000F1D RID: 3869
			internal const string Header = "rnvHeader";

			// Token: 0x04000F1E RID: 3870
			internal const string Footer = "rnvFooter";

			// Token: 0x04000F1F RID: 3871
			internal const string First = "rnvFirst";

			// Token: 0x04000F20 RID: 3872
			internal const string Last = "rnvLast";

			// Token: 0x04000F21 RID: 3873
			internal const string Sprite = "rwzSprite";

			// Token: 0x04000F22 RID: 3874
			internal const string RootGroupWrapper = "rnvRootGroupWrapper";

			// Token: 0x04000F23 RID: 3875
			internal const string HiddenGroups = "rnvHiddenGroups";

			// Token: 0x04000F24 RID: 3876
			internal const string Toggle = "rnvToggle";

			// Token: 0x04000F25 RID: 3877
			internal const string Focused = "rnvFocused";

			// Token: 0x04000F26 RID: 3878
			internal const string Expanded = "rnvExpanded";

			// Token: 0x04000F27 RID: 3879
			internal const string Disabled = "rnvDisabled";

			// Token: 0x04000F28 RID: 3880
			internal const string Selected = "rnvSelected";

			// Token: 0x04000F29 RID: 3881
			internal const string Item = "rnvItem";

			// Token: 0x04000F2A RID: 3882
			internal const string Image = "radImage";

			// Token: 0x04000F2B RID: 3883
			internal const string Icon = "radIcon";

			// Token: 0x04000F2C RID: 3884
			internal const string Text = "rnvText";

			// Token: 0x04000F2D RID: 3885
			internal const string Description = "rnvDescription";

			// Token: 0x04000F2E RID: 3886
			internal const string Link = "rnvLink";

			// Token: 0x04000F2F RID: 3887
			internal const string RootLink = "rnvRootLink";

			// Token: 0x04000F30 RID: 3888
			internal const string Template = "rnvTemplate";

			// Token: 0x04000F31 RID: 3889
			internal const string ContentTemplate = "rnvContentTemplate";
		}
	}
}
