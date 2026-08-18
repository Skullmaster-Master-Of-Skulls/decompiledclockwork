using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.OrgChart.WebServiceBindings;

namespace Telerik.Web.UI
{
	// Token: 0x02000C07 RID: 3079
	[Designer("Telerik.Web.Design.RadOrgChartDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[EmbeddedSkin("OrgChart", "Default", typeof(RadOrgChart))]
	[LightweightRendering]
	[EmbeddedSkin("OrgChart", typeof(RadOrgChart))]
	[ToolboxBitmap(typeof(RadOrgChart), "Telerik.Web.UI.OrgChart.png")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadOrgChart))]
	[TelerikToolboxCategory("Visualization")]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.RadOrgChart", "Telerik.Web.UI.OrgChart.RadOrgChartScripts.js")]
	[ToolboxData("<{0}:RadOrgChart runat=\"server\"></{0}:RadOrgChart>")]
	[RequiredScript(typeof(MaterialRipple))]
	public class RadOrgChart : RadDataBoundControl, IOrgChartNodeContainer, IXmlSerializable, IPostBackEventHandler, ICallbackEventHandler
	{
		// Token: 0x1700261F RID: 9759
		// (get) Token: 0x060074F3 RID: 29939 RVA: 0x001B3474 File Offset: 0x001B1674
		internal Dictionary<object, List<OrgChartGroupItem>> ItemsHash
		{
			get
			{
				if (this._itemsHash == null)
				{
					this._itemsHash = new Dictionary<object, List<OrgChartGroupItem>>();
				}
				return this._itemsHash;
			}
		}

		// Token: 0x17002620 RID: 9760
		// (get) Token: 0x060074F4 RID: 29940 RVA: 0x001B348F File Offset: 0x001B168F
		internal bool IsGroupEnabledBinding
		{
			get
			{
				return this.DataSourceID.Length == 0 && this.DataSource == null && this._groupEnabledBinding != null;
			}
		}

		// Token: 0x17002621 RID: 9761
		// (get) Token: 0x060074F5 RID: 29941 RVA: 0x001B34B4 File Offset: 0x001B16B4
		internal bool IsSimpleBinding
		{
			get
			{
				return (this.DataSource != null && this._groupEnabledBinding == null) || this.DataSourceID.Length != 0;
			}
		}

		// Token: 0x17002622 RID: 9762
		// (get) Token: 0x060074F6 RID: 29942 RVA: 0x001B34D9 File Offset: 0x001B16D9
		// (set) Token: 0x060074F7 RID: 29943 RVA: 0x001B34F0 File Offset: 0x001B16F0
		internal int[] DrilledNodeIndexes
		{
			get
			{
				return (int[])this.ViewState["DrilledNodeIndexes"];
			}
			set
			{
				this.ViewState["DrilledNodeIndexes"] = value;
			}
		}

		// Token: 0x17002623 RID: 9763
		// (get) Token: 0x060074F8 RID: 29944 RVA: 0x001B3504 File Offset: 0x001B1704
		// (set) Token: 0x060074F9 RID: 29945 RVA: 0x001B3539 File Offset: 0x001B1739
		internal bool IsDrillDown
		{
			get
			{
				return ((bool?)this.ViewState["IsDrillDown"]) ?? false;
			}
			set
			{
				this.ViewState["IsDrillDown"] = value;
			}
		}

		// Token: 0x17002624 RID: 9764
		// (get) Token: 0x060074FA RID: 29946 RVA: 0x001B3551 File Offset: 0x001B1751
		// (set) Token: 0x060074FB RID: 29947 RVA: 0x001B3559 File Offset: 0x001B1759
		internal int DrillDownLevel { get; set; }

		// Token: 0x060074FC RID: 29948 RVA: 0x001B3564 File Offset: 0x001B1764
		protected override void OnInit(EventArgs e)
		{
			if (this.Page != null && this.Page.IsPostBack)
			{
				base.RequiresDataBinding = true;
				string text = this.Page.Request.Form[base.ClientStateFieldID];
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						this.ParseClientState(new JavaScriptSerializer().Deserialize<OrgChartClientState>(text));
					}
					catch (InvalidOperationException)
					{
					}
					catch (ArgumentException)
					{
					}
				}
			}
			base.OnInit(e);
		}

		// Token: 0x060074FD RID: 29949 RVA: 0x001B35F0 File Offset: 0x001B17F0
		protected override void OnLoad(EventArgs e)
		{
			this.CreateRenderingTree();
			base.OnLoad(e);
		}

		// Token: 0x060074FE RID: 29950 RVA: 0x001B3600 File Offset: 0x001B1800
		public override void DataBind()
		{
			OrgChartNodeBinder orgChartNodeBinder = new OrgChartNodeBinder(this, this._groupEnabledBinding);
			this.Controls.Add(orgChartNodeBinder);
			orgChartNodeBinder.DataBind();
			this.CreateRenderingTree();
			this.Controls.Remove(orgChartNodeBinder);
			base.DataBind();
		}

		// Token: 0x060074FF RID: 29951 RVA: 0x001B3644 File Offset: 0x001B1844
		internal void RaiseGroupItemDataBound(OrgChartGroupItem item)
		{
			OrgChartGroupItemDataBoundEventArguments e = new OrgChartGroupItemDataBoundEventArguments(item);
			this.OnGroupItemDataBound(e);
		}

		// Token: 0x06007500 RID: 29952 RVA: 0x001B3660 File Offset: 0x001B1860
		internal void RaiseNodeDataBound(OrgChartNode node)
		{
			this.CreateRenderingTree();
			if (node != null)
			{
				OrgChartNodeDataBoundEventArguments e = new OrgChartNodeDataBoundEventArguments(node);
				foreach (OrgChartGroupItem item in node.GroupItems)
				{
					this.RaiseGroupItemDataBound(item);
				}
				this.OnNodeDataBound(e);
			}
		}

		// Token: 0x06007501 RID: 29953 RVA: 0x001B36CC File Offset: 0x001B18CC
		internal void CleanItemsHash()
		{
			this._itemsHash = null;
		}

		// Token: 0x06007502 RID: 29954 RVA: 0x001B36D5 File Offset: 0x001B18D5
		internal void ParseClientState(OrgChartClientState clientState)
		{
			this.DrillDownLevel = clientState.drillDownLevel;
			this._expandedNodesIndexes = clientState.expandedNodes;
			this._collapsedNodesIndexes = clientState.collapsedNodes;
			this._expandedGroupsIndexes = clientState.expandedGroups;
			this._collapsedGroupsIndexes = clientState.collapsedGroups;
		}

		// Token: 0x17002625 RID: 9765
		// (get) Token: 0x06007503 RID: 29955 RVA: 0x001B3713 File Offset: 0x001B1913
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007504 RID: 29956 RVA: 0x001B3718 File Offset: 0x001B1918
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			bool flag = this._groupEnabledBinding != null && !string.IsNullOrEmpty(this._groupEnabledBinding.NodeBindingSettings.DataSourceID) && !string.IsNullOrEmpty(this._groupEnabledBinding.GroupItemBindingSettings.DataSourceID);
			if (flag && base.RequiresDataBinding)
			{
				this.DataBind();
			}
			this.ApplyExpandCollapseState();
			this.ExecuteDrillDown();
			this.CreateRenderingTree();
			this.ExecuteLoadOnDemand();
			this.ExcecuteWebServiceBindings();
			if (this.Orientation == Orientation.Horizontal && this.RenderMode == RenderMode.Classic)
			{
				throw new Exception("The Horizontal orientation is available only in Lightweight rendering. Please set RenderMode to Lightweight.");
			}
		}

		// Token: 0x06007505 RID: 29957 RVA: 0x001B37B4 File Offset: 0x001B19B4
		internal void CreateRenderingTree()
		{
			if (this.Nodes.Count > 0)
			{
				this.Nodes.AssignReferencesToInnerTree(this.Nodes, this);
				if (!this.Controls.Contains(this.Nodes.Renderer))
				{
					this.Controls.Add(this.Nodes.Renderer);
				}
				this.Nodes.SyncRenderedProperties();
				this.ExpandRenderingTrees(this.Nodes);
			}
		}

		// Token: 0x06007506 RID: 29958 RVA: 0x001B3828 File Offset: 0x001B1A28
		private void ExpandRenderingTrees(OrgChartNodeCollection nodes)
		{
			foreach (OrgChartNode orgChartNode in nodes)
			{
				if (!nodes.Renderer.Controls.Contains(orgChartNode.Renderer))
				{
					nodes.Renderer.Controls.Add(orgChartNode.Renderer);
				}
				orgChartNode.SyncRenderedProperties();
				if (!orgChartNode.Renderer.Controls.Contains(orgChartNode.GroupItems.Renderer))
				{
					orgChartNode.Renderer.Controls.Add(orgChartNode.GroupItems.Renderer);
				}
				orgChartNode.GroupItems.SyncRenderedProperties();
				foreach (OrgChartGroupItem orgChartGroupItem in orgChartNode.GroupItems)
				{
					if (!orgChartNode.GroupItems.Renderer.Controls.Contains(orgChartGroupItem.Renderer))
					{
						orgChartNode.GroupItems.Renderer.Controls.Add(orgChartGroupItem.Renderer);
					}
					orgChartGroupItem.SyncRenderedProperties();
				}
				if (orgChartNode.Nodes.Count > 0)
				{
					if (!orgChartNode.Renderer.Controls.Contains(orgChartNode.Nodes.Renderer))
					{
						orgChartNode.Renderer.Controls.Add(orgChartNode.Nodes.Renderer);
					}
					orgChartNode.Nodes.SyncRenderedProperties();
					this.ExpandRenderingTrees(orgChartNode.Nodes);
				}
			}
		}

		// Token: 0x17002626 RID: 9766
		// (get) Token: 0x06007507 RID: 29959 RVA: 0x001B39DC File Offset: 0x001B1BDC
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002627 RID: 9767
		// (get) Token: 0x06007508 RID: 29960 RVA: 0x001B39E0 File Offset: 0x001B1BE0
		protected override string CssClassFormatString
		{
			get
			{
				List<string> list = new List<string>
				{
					"RadOrgChart",
					"RadOrgChart_{0}"
				};
				if (this.IsSimpleBinding)
				{
					list.Add("rocSimple");
					if (this.Page.Request != null && this.Page.Request.Browser.Browser == "IE" && this.Page.Request.Browser.MajorVersion <= 7)
					{
						list.Remove("rocSimple");
					}
				}
				if (this.Orientation == Orientation.Horizontal)
				{
					list.Add("rocHorizontal");
				}
				return string.Join(" ", list.ToArray()).Trim();
			}
		}

		// Token: 0x06007509 RID: 29961 RVA: 0x001B3A98 File Offset: 0x001B1C98
		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocViewPort");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.RenderContents(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600750A RID: 29962 RVA: 0x001B3ACC File Offset: 0x001B1CCC
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			string text = this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this, "arguments")
			{
				ClientSubmit = true
			});
			text = text.Replace("\"", "'");
			descriptor.AddProperty("_postBackReference", text);
			descriptor.AddProperty("_isSimpleBinding", this.IsSimpleBinding);
			descriptor.AddProperty("_clientStateFieldID", base.ClientStateFieldID);
			descriptor.AddProperty("_uniqueId", this.UniqueID);
			if (this.DisableDefaultImage)
			{
				descriptor.AddProperty("_disableDefaultImage", this.DisableDefaultImage);
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			this.DescribeNodeData(descriptor, javaScriptSerializer);
			if (this._webServiceBindings != null)
			{
				JavaScriptConverter[] converters = new JavaScriptConverter[]
				{
					new OrgChartGroupItemServiceSettingsConverter(),
					new OrgChartNodeServiceSettingsConverter()
				};
				javaScriptSerializer.RegisterConverters(converters);
				descriptor.AddProperty("_webServiceBindings", javaScriptSerializer.Serialize(this.WebServiceBindings));
				if (!this.DisableDefaultImage)
				{
					descriptor.AddProperty("_defaultImagePath", this.Page.ClientScript.GetWebResourceUrl(typeof(RadOrgChart), "Telerik.Web.UI.Skins.Common.OrgChart.rocItemDefaultPicture.png"));
				}
			}
			this.DescribeDragAndDrop(descriptor);
			this.DescribeCollapsing(descriptor);
			this.DescribeDrillDown(descriptor);
			this.DescribeLoadOnDemand(descriptor);
			if (!this.PersistExpandCollapseState)
			{
				descriptor.AddProperty("_persistExpandCollapseState", this.PersistExpandCollapseState);
			}
			this.DescribeExpandCollapseEvents(descriptor);
			if (this.RenderMode != RenderMode.Classic)
			{
				descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			}
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600750B RID: 29963 RVA: 0x001B3C5C File Offset: 0x001B1E5C
		private void DescribeExpandCollapseEvents(IScriptDescriptor descriptor)
		{
			if ((OrgChartNodeExpandCollapseEventHandler)base.Events[RadOrgChart.NodeExpandCollapseEvent] != null)
			{
				descriptor.AddProperty("_postbackOnNodeExpandCollapse", true);
			}
			if ((OrgChartGroupExpandCollapseEventHandler)base.Events[RadOrgChart.GroupExpandCollapseEvent] != null)
			{
				descriptor.AddProperty("_postbackOnGroupExpandCollapse", true);
			}
		}

		// Token: 0x0600750C RID: 29964 RVA: 0x001B3CB9 File Offset: 0x001B1EB9
		private void DescribeLoadOnDemand(IScriptDescriptor descriptor)
		{
			if (this.LoadOnDemand != OrgChartLoadOnDemand.None)
			{
				this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
				descriptor.AddProperty("_loadOnDemand", this.LoadOnDemand);
			}
		}

		// Token: 0x0600750D RID: 29965 RVA: 0x001B3CF0 File Offset: 0x001B1EF0
		private void DescribeDrillDown(IScriptDescriptor descriptor)
		{
			if (this.EnableDrillDown)
			{
				descriptor.AddProperty("_isDrilledOnLevels", this.MaxDataBindDepth > 0);
				descriptor.AddProperty("_enableDrillDown", this.EnableDrillDown);
				descriptor.AddProperty("_drilledNodeHierarchicalIndex", this._drilledNodeHierarchicalIndex);
			}
		}

		// Token: 0x0600750E RID: 29966 RVA: 0x001B3D45 File Offset: 0x001B1F45
		private void DescribeCollapsing(IScriptDescriptor descriptor)
		{
			if (this.EnableCollapsing)
			{
				descriptor.AddProperty("_enableCollapsing", this.EnableCollapsing);
			}
			if (this.EnableGroupCollapsing)
			{
				descriptor.AddProperty("_enableGroupCollapsing", this.EnableGroupCollapsing);
			}
		}

		// Token: 0x0600750F RID: 29967 RVA: 0x001B3D84 File Offset: 0x001B1F84
		private void DescribeDragAndDrop(IScriptDescriptor descriptor)
		{
			if (this.EnableDragAndDrop)
			{
				descriptor.AddProperty("_enableDragAndDrop", this.EnableDragAndDrop);
				bool flag = !this.IsSimpleBinding && this.AllowGroupItemDragging;
				descriptor.AddProperty("_allowGroupItemDragging", flag);
			}
		}

		// Token: 0x06007510 RID: 29968 RVA: 0x001B3DD4 File Offset: 0x001B1FD4
		private void DescribeNodeData(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			if (this.Nodes.Count > 0)
			{
				JavaScriptConverter[] converters = new JavaScriptConverter[]
				{
					new OrgChartNodeJavaScriptConverter(),
					new OrgChartGroupItemJavaScriptConverter()
				};
				serializer.RegisterConverters(converters);
				descriptor.AddProperty("_nodeData", serializer.Serialize(new List<OrgChartNode>
				{
					this.Nodes[0]
				}));
			}
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x001B3E3C File Offset: 0x001B203C
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			if ((this.LoadOnDemand & OrgChartLoadOnDemand.Nodes) == OrgChartLoadOnDemand.Nodes || this._webServiceBindings != null)
			{
				RadDataBoundControl.DescribeEvent(descriptor, "nodePopulating", this.OnClientNodePopulating);
				RadDataBoundControl.DescribeEvent(descriptor, "nodePopulated", this.OnClientNodePopulated);
				RadDataBoundControl.DescribeEvent(descriptor, "nodePopulationFailed", this.OnClientNodePopulationFailed);
			}
			if ((this.LoadOnDemand & OrgChartLoadOnDemand.Groups) == OrgChartLoadOnDemand.Groups || this._webServiceBindings != null)
			{
				RadDataBoundControl.DescribeEvent(descriptor, "groupPopulating", this.OnClientGroupPopulating);
				RadDataBoundControl.DescribeEvent(descriptor, "groupPopulated", this.OnClientGroupPopulated);
				RadDataBoundControl.DescribeEvent(descriptor, "groupPopulationFailed", this.OnClientGroupPopulationFailed);
			}
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06007512 RID: 29970 RVA: 0x001B3EDC File Offset: 0x001B20DC
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007513 RID: 29971 RVA: 0x001B3EE4 File Offset: 0x001B20E4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("GroupColumnCount");
				if (attribute != null)
				{
					this.GroupColumnCount = int.Parse(attribute);
				}
				string attribute2 = reader.GetAttribute("EnableDragAndDrop");
				if (attribute2 != null)
				{
					this.EnableDragAndDrop = bool.Parse(attribute2);
				}
				string attribute3 = reader.GetAttribute("EnableCollapsing");
				if (attribute3 != null)
				{
					this.EnableCollapsing = bool.Parse(attribute3);
				}
				string attribute4 = reader.GetAttribute("EnableGroupCollapsing");
				if (attribute4 != null)
				{
					this.EnableGroupCollapsing = bool.Parse(attribute4);
				}
				string attribute5 = reader.GetAttribute("EnableDrillDown");
				if (attribute5 != null)
				{
					this.EnableDrillDown = bool.Parse(attribute5);
				}
				string attribute6 = reader.GetAttribute("LoadOnDemand");
				if (attribute6 != null)
				{
					this.LoadOnDemand = (OrgChartLoadOnDemand)Enum.Parse(typeof(OrgChartLoadOnDemand), attribute6);
				}
			}
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartNodeCollection));
						OrgChartNodeCollection orgChartNodeCollection = (OrgChartNodeCollection)xmlSerializer.Deserialize(xmlReader);
						this.Nodes.Clear();
						foreach (OrgChartNode node in orgChartNodeCollection)
						{
							this.Nodes.Add(node);
						}
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06007514 RID: 29972 RVA: 0x001B4080 File Offset: 0x001B2280
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlForOwnedAttributes(writer);
			this.WriteXmlForInnerContent(writer);
		}

		// Token: 0x06007515 RID: 29973 RVA: 0x001B4090 File Offset: 0x001B2290
		private void WriteXmlForOwnedAttributes(XmlWriter writer)
		{
			if (this._groupColumnCount > 0)
			{
				writer.WriteAttributeString("GroupColumnCount", this._groupColumnCount.ToString());
			}
			if (this.EnableDragAndDrop)
			{
				writer.WriteAttributeString("EnableDragAndDrop", this.EnableDragAndDrop.ToString());
			}
			if (this.EnableCollapsing)
			{
				writer.WriteAttributeString("EnableCollapsing", this.EnableCollapsing.ToString());
			}
			if (this.EnableGroupCollapsing)
			{
				writer.WriteAttributeString("EnableGroupCollapsing", this.EnableGroupCollapsing.ToString());
			}
			if (this.EnableDrillDown)
			{
				writer.WriteAttributeString("EnableDrillDown", this.EnableDrillDown.ToString());
			}
			if (this.LoadOnDemand != OrgChartLoadOnDemand.None)
			{
				writer.WriteAttributeString("LoadOnDemand", this.LoadOnDemand.ToString());
			}
		}

		// Token: 0x06007516 RID: 29974 RVA: 0x001B4164 File Offset: 0x001B2364
		private void WriteXmlForInnerContent(XmlWriter writer)
		{
			if (this.Nodes.Count > 0)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartNodeCollection));
				xmlSerializer.Serialize(writer, this.Nodes);
			}
		}

		// Token: 0x06007517 RID: 29975 RVA: 0x001B419C File Offset: 0x001B239C
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x001B41A8 File Offset: 0x001B23A8
		private void RaisePostBackEvent(string eventArgument)
		{
			OrgChartPostBackArguments orgChartPostBackArguments = null;
			try
			{
				orgChartPostBackArguments = new JavaScriptSerializer().Deserialize<OrgChartPostBackArguments>(eventArgument);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			this.postbackArgs = orgChartPostBackArguments;
			if (orgChartPostBackArguments == null)
			{
				return;
			}
			this.CorrespondPostbackCommand(orgChartPostBackArguments);
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x001B41FC File Offset: 0x001B23FC
		private void CorrespondPostbackCommand(OrgChartPostBackArguments arguments)
		{
			int[] array = this.ParseNodeHierarchicalIndexes(arguments.sourceNodeHierarchicalIndex);
			int[] nodeUpIndexes = this.ParseNodeHierarchicalIndexes(arguments.destinationNodeHierarchicalIndex);
			if (base.RequiresDataBinding)
			{
				this.DataBind();
			}
			switch (arguments.command)
			{
			case OrgChartPostBackCommand.NodeDrop:
				this.PrepareOrgChartForDragAndDrop();
				this.SetupNodeDrop(array, nodeUpIndexes);
				return;
			case OrgChartPostBackCommand.GroupItemDrop:
				this.PrepareOrgChartForDragAndDrop();
				this.SetupGroupItemDrop(array, nodeUpIndexes, arguments.sourceGroupItemIndex);
				return;
			case OrgChartPostBackCommand.DrillDown:
				if (arguments.sourceNodeHierarchicalIndex != "0")
				{
					this._drilledNodeHierarchicalIndex = arguments.sourceNodeHierarchicalIndex;
				}
				this._drilledNodeIndexes = (this.DrilledNodeIndexes = array);
				this._isDrillDown = (this.IsDrillDown = true);
				return;
			case OrgChartPostBackCommand.NodeExpanded:
				this.SetupNodeExpandCollapse(array, OrgChartNodeExpandCollapseState.NodeExpanded);
				return;
			case OrgChartPostBackCommand.NodeCollapsed:
				this.SetupNodeExpandCollapse(array, OrgChartNodeExpandCollapseState.NodeCollapsed);
				return;
			case OrgChartPostBackCommand.GroupExpanded:
				this.SetupGroupExpandCollapse(array, OrgChartGroupExpandCollapseState.GroupExpanded);
				return;
			case OrgChartPostBackCommand.GroupCollapsed:
				this.SetupGroupExpandCollapse(array, OrgChartGroupExpandCollapseState.GroupCollapsed);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600751A RID: 29978 RVA: 0x001B42E4 File Offset: 0x001B24E4
		protected override string SaveClientState()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (this.DrillDownLevel > 0)
			{
				dictionary.Add("drillDownLevel", this.DrillDownLevel);
			}
			if (this._collapsedNodesIndexes != null && this._collapsedNodesIndexes.Length > 0)
			{
				dictionary.Add("collapsedNodes", this._collapsedNodesIndexes);
			}
			if (this._expandedNodesIndexes != null && this._expandedNodesIndexes.Length > 0)
			{
				dictionary.Add("expandedNodes", this._expandedNodesIndexes);
			}
			if (this._collapsedGroupsIndexes != null && this._collapsedGroupsIndexes.Length > 0)
			{
				dictionary.Add("collapsedGroups", this._collapsedGroupsIndexes);
			}
			if (this._expandedGroupsIndexes != null && this._expandedGroupsIndexes.Length > 0)
			{
				dictionary.Add("expandedGroups", this._expandedGroupsIndexes);
			}
			if (dictionary.Count > 0)
			{
				return new JavaScriptSerializer().Serialize(dictionary);
			}
			return null;
		}

		// Token: 0x0600751B RID: 29979 RVA: 0x001B43BC File Offset: 0x001B25BC
		private int[] ParseNodeHierarchicalIndexes(string hierarchicalIndexes)
		{
			if (hierarchicalIndexes != null)
			{
				string[] array = hierarchicalIndexes.Split(new char[]
				{
					':'
				});
				int[] array2 = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					int.TryParse(array[i], out array2[i]);
				}
				return array2;
			}
			return null;
		}

		// Token: 0x0600751C RID: 29980 RVA: 0x001B440C File Offset: 0x001B260C
		private void ApplyExpandCollapseState()
		{
			if (this.PersistExpandCollapseState)
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				List<string> list3 = new List<string>();
				List<string> list4 = new List<string>();
				if (this.EnableCollapsing)
				{
					this.LoadExpandCollapseIndexesByNodeID(true, list, list2);
				}
				if (this.EnableGroupCollapsing)
				{
					this.LoadExpandCollapseIndexesByNodeID(false, list3, list4);
				}
				if (!this._isExpandCollapseStateApplied)
				{
					if (this.EnableCollapsing)
					{
						this.ExpandCollpase(this._expandedNodesIndexes, true, false);
						this.ExpandCollpase(this._collapsedNodesIndexes, true, true);
					}
					if (this.EnableGroupCollapsing)
					{
						this.ExpandCollpase(this._expandedGroupsIndexes, false, false);
						this.ExpandCollpase(this._collapsedGroupsIndexes, false, true);
					}
					this._isExpandCollapseStateApplied = true;
				}
				if (list.Count > 0)
				{
					this._expandedNodesIndexes = list.ToArray();
				}
				if (list2.Count > 0)
				{
					this._collapsedNodesIndexes = list2.ToArray();
				}
				if (list3.Count > 0)
				{
					this._expandedGroupsIndexes = list3.ToArray();
				}
				if (list4.Count > 0)
				{
					this._collapsedGroupsIndexes = list4.ToArray();
				}
			}
		}

		// Token: 0x0600751D RID: 29981 RVA: 0x001B450C File Offset: 0x001B270C
		private void ExpandCollpase(string[] nodeIndexes, bool forNodes, bool isCollapsed)
		{
			if (nodeIndexes != null)
			{
				for (int i = 0; i < nodeIndexes.Length; i++)
				{
					int[] nodeIndexes2 = this.ParseNodeHierarchicalIndexes(nodeIndexes[i]);
					OrgChartNode nodeByHierarchicalIndex = this.GetNodeByHierarchicalIndex(nodeIndexes2);
					if (nodeByHierarchicalIndex != null)
					{
						if (forNodes)
						{
							nodeByHierarchicalIndex.Collapsed = isCollapsed;
							if (this.EnableDragAndDrop && !this.expandCollapseNodesID.ContainsKey(nodeByHierarchicalIndex.ID))
							{
								this.expandCollapseNodesID.Add(nodeByHierarchicalIndex.ID, isCollapsed);
							}
						}
						else
						{
							nodeByHierarchicalIndex.GroupCollapsed = isCollapsed;
							if (this.EnableDragAndDrop && !this.expandCollapseGroupsID.ContainsKey(nodeByHierarchicalIndex.ID))
							{
								this.expandCollapseGroupsID.Add(nodeByHierarchicalIndex.ID, isCollapsed);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600751E RID: 29982 RVA: 0x001B45E8 File Offset: 0x001B27E8
		private void LoadExpandCollapseIndexesByNodeID(bool forNodes, List<string> expandedIndexes, List<string> collapsedIndexes)
		{
			Dictionary<string, bool> dictionary;
			if (forNodes)
			{
				dictionary = this.expandCollapseNodesID;
			}
			else
			{
				dictionary = this.expandCollapseGroupsID;
			}
			if (dictionary != null && dictionary.Count > 0)
			{
				using (Dictionary<string, bool>.Enumerator enumerator = dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, bool> element = enumerator.Current;
						IEnumerable<OrgChartNode> nodes = this.GetNodes(delegate(OrgChartNode x)
						{
							string id = x.ID;
							KeyValuePair<string, bool> element4 = element;
							return id == element4.Key;
						});
						foreach (OrgChartNode orgChartNode in nodes)
						{
							if (forNodes)
							{
								OrgChartNode orgChartNode2 = orgChartNode;
								KeyValuePair<string, bool> element5 = element;
								orgChartNode2.Collapsed = element5.Value;
							}
							else
							{
								OrgChartNode orgChartNode3 = orgChartNode;
								KeyValuePair<string, bool> element2 = element;
								orgChartNode3.GroupCollapsed = element2.Value;
							}
							KeyValuePair<string, bool> element3 = element;
							if (!element3.Value)
							{
								expandedIndexes.Add(orgChartNode.GetHierarchicalIndex());
							}
							else
							{
								collapsedIndexes.Add(orgChartNode.GetHierarchicalIndex());
							}
						}
					}
				}
			}
		}

		// Token: 0x0600751F RID: 29983 RVA: 0x001B4718 File Offset: 0x001B2918
		private void SetupNodeExpandCollapse(int[] sourceNodeIndexes, OrgChartNodeExpandCollapseState state)
		{
			OrgChartNode nodeByHierarchicalIndex = this.GetNodeByHierarchicalIndex(sourceNodeIndexes);
			OrgChartNodeExpandCollapseEventArguments e = new OrgChartNodeExpandCollapseEventArguments(nodeByHierarchicalIndex, state);
			this.OnNodeExpandCollapse(e);
		}

		// Token: 0x06007520 RID: 29984 RVA: 0x001B473C File Offset: 0x001B293C
		private void SetupGroupExpandCollapse(int[] sourceNodeIndexes, OrgChartGroupExpandCollapseState state)
		{
			OrgChartNode nodeByHierarchicalIndex = this.GetNodeByHierarchicalIndex(sourceNodeIndexes);
			OrgChartGroupExpandCollapseEventArguments e = new OrgChartGroupExpandCollapseEventArguments(nodeByHierarchicalIndex, state);
			this.OnGroupExpandCollapse(e);
		}

		// Token: 0x06007521 RID: 29985 RVA: 0x001B4760 File Offset: 0x001B2960
		private void PrepareOrgChartForDragAndDrop()
		{
			this.ApplyExpandCollapseState();
			this._expandedNodesIndexes = null;
			this._collapsedNodesIndexes = null;
			this._expandedGroupsIndexes = null;
			this._collapsedGroupsIndexes = null;
		}

		// Token: 0x06007522 RID: 29986 RVA: 0x001B4784 File Offset: 0x001B2984
		private void SetupGroupItemDrop(int[] nodeDownIndexes, int[] nodeUpIndexes, int groupItemDownIndex)
		{
			OrgChartNode nodeByHierarchicalIndex = this.GetNodeByHierarchicalIndex(nodeDownIndexes);
			OrgChartNode nodeByHierarchicalIndex2 = this.GetNodeByHierarchicalIndex(nodeUpIndexes);
			OrgChartGroupItem sourceGroupItem = nodeByHierarchicalIndex.GroupItems[groupItemDownIndex];
			OrgChartGroupItemDropEventArguments e = new OrgChartGroupItemDropEventArguments(sourceGroupItem, nodeByHierarchicalIndex2);
			this.OnGroupItemDrop(e);
		}

		// Token: 0x06007523 RID: 29987 RVA: 0x001B47C0 File Offset: 0x001B29C0
		private void SetupNodeDrop(int[] nodeDownIndexes, int[] nodeUpIndexes)
		{
			OrgChartNode nodeByHierarchicalIndex = this.GetNodeByHierarchicalIndex(nodeDownIndexes);
			OrgChartNode nodeByHierarchicalIndex2 = this.GetNodeByHierarchicalIndex(nodeUpIndexes);
			OrgChartNodeDropEventArguments e = new OrgChartNodeDropEventArguments(nodeByHierarchicalIndex, nodeByHierarchicalIndex2);
			this.OnNodeDrop(e);
		}

		// Token: 0x06007524 RID: 29988 RVA: 0x001B47EC File Offset: 0x001B29EC
		private void ExecuteDrillDown()
		{
			if (this.EnableDrillDown)
			{
				if (this._isDrillDown)
				{
					this.SetupDrillDown(this._drilledNodeIndexes);
					return;
				}
				if (this.IsDrillDown && this.DrilledNodeIndexes != null && this.DrilledNodeIndexes.Length > 0)
				{
					this._isDrillDown = this.IsDrillDown;
					string[] array = new string[this.DrilledNodeIndexes.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this.DrilledNodeIndexes[i].ToString();
					}
					this._drilledNodeHierarchicalIndex = string.Join(":", array);
					this._drilledNodeIndexes = this.DrilledNodeIndexes;
					this.SetupDrillDown(this.DrilledNodeIndexes);
				}
			}
		}

		// Token: 0x06007525 RID: 29989 RVA: 0x001B489C File Offset: 0x001B2A9C
		internal void SetupDrillDown(int[] drilledNodeIndexes)
		{
			OrgChartNode nodeByHierarchicalIndex = this.GetNodeByHierarchicalIndex(drilledNodeIndexes);
			OrgChartDrillDownEventArguments e = new OrgChartDrillDownEventArguments(nodeByHierarchicalIndex);
			if (this.postbackArgs != null && this.postbackArgs.command == OrgChartPostBackCommand.DrillDown)
			{
				this.OnDrillDown(e);
			}
			this.Nodes.Clear();
			this.Nodes.Add(nodeByHierarchicalIndex);
		}

		// Token: 0x06007526 RID: 29990 RVA: 0x001B48EC File Offset: 0x001B2AEC
		private void ExecuteLoadOnDemand()
		{
			if (this.LoadOnDemand != OrgChartLoadOnDemand.None && this.Nodes.Count > 0)
			{
				this.StripCollapsedNodesForLoadOnDemand(this.Nodes[0]);
			}
		}

		// Token: 0x06007527 RID: 29991 RVA: 0x001B4918 File Offset: 0x001B2B18
		private void StripCollapsedNodesForLoadOnDemand(OrgChartNode currentNode)
		{
			Queue<OrgChartNode> queue = new Queue<OrgChartNode>();
			queue.Enqueue(currentNode);
			while (queue.Count > 0)
			{
				OrgChartNode orgChartNode = queue.Dequeue();
				if (orgChartNode.Collapsed && (this.LoadOnDemand & OrgChartLoadOnDemand.Nodes) == OrgChartLoadOnDemand.Nodes && orgChartNode.Nodes.Count > 0)
				{
					orgChartNode.Renderer.Controls.Remove(orgChartNode.Nodes.Renderer);
				}
				if (orgChartNode.GroupCollapsed && (this.LoadOnDemand & OrgChartLoadOnDemand.Groups) == OrgChartLoadOnDemand.Groups && orgChartNode.GroupItems.Count > 0)
				{
					for (int i = 1; i < orgChartNode.GroupItems.Count; i++)
					{
						orgChartNode.GroupItems.Renderer.Controls.Remove(orgChartNode.GroupItems[i].Renderer);
					}
				}
				for (int j = 0; j < orgChartNode.Nodes.Count; j++)
				{
					queue.Enqueue(orgChartNode.Nodes[j]);
				}
			}
		}

		// Token: 0x06007528 RID: 29992 RVA: 0x001B4A0B File Offset: 0x001B2C0B
		private void ExcecuteWebServiceBindings()
		{
			if (this._webServiceBindings != null && this.Nodes.Count > 0)
			{
				this.StripCollapsedNodesForWebServiceBinding(this.Nodes[0]);
			}
		}

		// Token: 0x06007529 RID: 29993 RVA: 0x001B4A38 File Offset: 0x001B2C38
		private void StripCollapsedNodesForWebServiceBinding(OrgChartNode currentNode)
		{
			Queue<OrgChartNode> queue = new Queue<OrgChartNode>();
			queue.Enqueue(currentNode);
			while (queue.Count > 0)
			{
				OrgChartNode orgChartNode = queue.Dequeue();
				if (orgChartNode.Collapsed && orgChartNode.Nodes.Count > 0)
				{
					orgChartNode.Renderer.Controls.Remove(orgChartNode.Nodes.Renderer);
				}
				if (orgChartNode.GroupCollapsed && orgChartNode.GroupItems.Count > 0)
				{
					int index = orgChartNode.GroupItems.Renderer.Controls.IndexOf(orgChartNode.GroupItems[0].Renderer);
					for (int i = 0; i < orgChartNode.GroupItems.Count; i++)
					{
						orgChartNode.GroupItems.Renderer.Controls.Remove(orgChartNode.GroupItems[i].Renderer);
					}
					orgChartNode.GroupItems.Renderer.Controls.AddAt(index, new OrgChartNotLoadedGroupItemRenderer());
				}
				for (int j = 0; j < orgChartNode.Nodes.Count; j++)
				{
					queue.Enqueue(orgChartNode.Nodes[j]);
				}
			}
		}

		// Token: 0x0600752A RID: 29994 RVA: 0x001B4B60 File Offset: 0x001B2D60
		public OrgChartNode GetNodeByHierarchicalIndex(int[] nodeIndexes)
		{
			this._hierarchicalLevel = nodeIndexes.Length;
			this._hierarchicalIndexes = nodeIndexes;
			int hierarchicalLevelCounter = 0;
			OrgChartNode hierarchicalNode = this.GetHierarchicalNode(this.Nodes, hierarchicalLevelCounter);
			this._hierarchicalLevel = 0;
			this._hierarchicalIndexes = null;
			return hierarchicalNode;
		}

		// Token: 0x0600752B RID: 29995 RVA: 0x001B4B9C File Offset: 0x001B2D9C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private OrgChartNode GetHierarchicalNode(OrgChartNodeCollection nodes, int hierarchicalLevelCounter)
		{
			if (hierarchicalLevelCounter == this._hierarchicalLevel - 1)
			{
				if (this._hierarchicalIndexes[hierarchicalLevelCounter] >= 0 && this._hierarchicalIndexes[hierarchicalLevelCounter] < nodes.Count)
				{
					return nodes[this._hierarchicalIndexes[hierarchicalLevelCounter]];
				}
				return null;
			}
			else
			{
				if (this._hierarchicalIndexes[hierarchicalLevelCounter] >= 0 && this._hierarchicalIndexes[hierarchicalLevelCounter] < nodes.Count)
				{
					return this.GetHierarchicalNode(nodes[this._hierarchicalIndexes[hierarchicalLevelCounter]].Nodes, hierarchicalLevelCounter + 1);
				}
				return null;
			}
		}

		// Token: 0x0600752C RID: 29996 RVA: 0x001B4C19 File Offset: 0x001B2E19
		string ICallbackEventHandler.GetCallbackResult()
		{
			return this.GetCallbackResult();
		}

		// Token: 0x0600752D RID: 29997 RVA: 0x001B4C24 File Offset: 0x001B2E24
		internal string GetCallbackResult()
		{
			if (base.RequiresDataBinding)
			{
				this.DataBind();
			}
			this.ApplyExpandCollapseState();
			this.CreateRenderingTree();
			string result = string.Empty;
			switch (this._callbackArguments.loadCommand)
			{
			case OrgChartLoadOnDemandCommand.LoadNodes:
				result = this.RenderDamandedNodes(this._callbackArguments.sourceNodeHierarchicalIndex);
				break;
			case OrgChartLoadOnDemandCommand.LoadGroupItems:
				result = this.RenderDemandedGroupItems(this._callbackArguments.sourceNodeHierarchicalIndex);
				break;
			}
			return result;
		}

		// Token: 0x0600752E RID: 29998 RVA: 0x001B4C94 File Offset: 0x001B2E94
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.RaiseCallbackEvent(eventArgument);
		}

		// Token: 0x0600752F RID: 29999 RVA: 0x001B4CA0 File Offset: 0x001B2EA0
		internal void RaiseCallbackEvent(string eventArgument)
		{
			try
			{
				this._callbackArguments = new JavaScriptSerializer().Deserialize<OrgChartCallbackArguments>(eventArgument);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x06007530 RID: 30000 RVA: 0x001B4CE4 File Offset: 0x001B2EE4
		private string RenderDamandedNodes(string nodeHierarchicalIndex)
		{
			int[] nodeIndexes = this.ParseNodeHierarchicalIndexes(nodeHierarchicalIndex);
			OrgChartNode nodeByHierarchicalIndex = this.GetNodeByHierarchicalIndex(nodeIndexes);
			foreach (OrgChartNode currentNode in nodeByHierarchicalIndex.Nodes)
			{
				this.StripCollapsedNodesForLoadOnDemand(currentNode);
			}
			StringWriter stringWriter = new StringWriter();
			nodeByHierarchicalIndex.Nodes.Renderer.RenderControl(new HtmlTextWriter(stringWriter));
			nodeByHierarchicalIndex.Renderer.RenderLines(new HtmlTextWriter(stringWriter));
			return stringWriter.ToString();
		}

		// Token: 0x06007531 RID: 30001 RVA: 0x001B4D7C File Offset: 0x001B2F7C
		private string RenderDemandedGroupItems(string nodeHierarchicalIndex)
		{
			int[] nodeIndexes = this.ParseNodeHierarchicalIndexes(nodeHierarchicalIndex);
			OrgChartNode nodeByHierarchicalIndex = this.GetNodeByHierarchicalIndex(nodeIndexes);
			StringWriter stringWriter = new StringWriter();
			for (int i = 1; i < nodeByHierarchicalIndex.GroupItems.Count; i++)
			{
				nodeByHierarchicalIndex.GroupItems[i].Renderer.RenderControl(new HtmlTextWriter(stringWriter));
			}
			return stringWriter.ToString();
		}

		// Token: 0x17002628 RID: 9768
		// (get) Token: 0x06007532 RID: 30002 RVA: 0x001B4DE0 File Offset: 0x001B2FE0
		// (set) Token: 0x06007533 RID: 30003 RVA: 0x001B4E12 File Offset: 0x001B3012
		[SimplePersistenceSetting]
		internal List<string> GroupCollapsedIndices
		{
			get
			{
				return this.GetNodesHierarchicalIndicesByCriteria((OrgChartNode x) => x.GroupCollapsed);
			}
			set
			{
				this.ManupulateNodesByCriteria(value, false);
			}
		}

		// Token: 0x17002629 RID: 9769
		// (get) Token: 0x06007534 RID: 30004 RVA: 0x001B4E24 File Offset: 0x001B3024
		// (set) Token: 0x06007535 RID: 30005 RVA: 0x001B4E56 File Offset: 0x001B3056
		[SimplePersistenceSetting]
		internal List<string> CollapsedIndices
		{
			get
			{
				return this.GetNodesHierarchicalIndicesByCriteria((OrgChartNode x) => x.Collapsed);
			}
			set
			{
				this.ManupulateNodesByCriteria(value, true);
			}
		}

		// Token: 0x06007536 RID: 30006 RVA: 0x001B4E70 File Offset: 0x001B3070
		private void ManupulateNodesByCriteria(List<string> indices, bool isCollapsed)
		{
			IEnumerable<OrgChartNode> nodes;
			if (isCollapsed)
			{
				nodes = this.GetNodes((OrgChartNode x) => x.Collapsed);
			}
			else
			{
				nodes = this.GetNodes((OrgChartNode x) => x.GroupCollapsed);
			}
			foreach (OrgChartNode orgChartNode in nodes)
			{
				if (isCollapsed)
				{
					orgChartNode.Collapsed = false;
				}
				else
				{
					orgChartNode.GroupCollapsed = false;
				}
			}
			foreach (string hierarchicalIndexes in indices)
			{
				int[] nodeIndexes = this.ParseNodeHierarchicalIndexes(hierarchicalIndexes);
				OrgChartNode nodeByHierarchicalIndex = this.GetNodeByHierarchicalIndex(nodeIndexes);
				if (nodeByHierarchicalIndex != null)
				{
					if (nodeByHierarchicalIndex != null && isCollapsed)
					{
						nodeByHierarchicalIndex.Collapsed = true;
					}
					else if (nodeByHierarchicalIndex != null && !isCollapsed)
					{
						nodeByHierarchicalIndex.GroupCollapsed = true;
					}
				}
			}
		}

		// Token: 0x06007537 RID: 30007 RVA: 0x001B4F84 File Offset: 0x001B3184
		private List<string> GetNodesHierarchicalIndicesByCriteria(RadOrgChart.OrgChartNodeCriteria criteria)
		{
			List<string> list = new List<string>();
			IEnumerable<OrgChartNode> nodes = this.GetNodes(criteria);
			foreach (OrgChartNode orgChartNode in nodes)
			{
				list.Add(orgChartNode.GetHierarchicalIndex());
			}
			return list;
		}

		// Token: 0x1400011A RID: 282
		// (add) Token: 0x06007538 RID: 30008 RVA: 0x001B4FE0 File Offset: 0x001B31E0
		// (remove) Token: 0x06007539 RID: 30009 RVA: 0x001B4FF3 File Offset: 0x001B31F3
		public event OrgChartNodeDataBoundEventHandler NodeDataBound
		{
			add
			{
				base.Events.AddHandler(RadOrgChart.NodeDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadOrgChart.NodeDataBoundEvent, value);
			}
		}

		// Token: 0x0600753A RID: 30010 RVA: 0x001B5008 File Offset: 0x001B3208
		protected virtual void OnNodeDataBound(OrgChartNodeDataBoundEventArguments e)
		{
			OrgChartNodeDataBoundEventHandler orgChartNodeDataBoundEventHandler = (OrgChartNodeDataBoundEventHandler)base.Events[RadOrgChart.NodeDataBoundEvent];
			if (orgChartNodeDataBoundEventHandler != null)
			{
				orgChartNodeDataBoundEventHandler(this, e);
			}
		}

		// Token: 0x1400011B RID: 283
		// (add) Token: 0x0600753B RID: 30011 RVA: 0x001B5036 File Offset: 0x001B3236
		// (remove) Token: 0x0600753C RID: 30012 RVA: 0x001B5049 File Offset: 0x001B3249
		public event OrgChartGroupItemDataBoundEventHandler GroupItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadOrgChart.GroupItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadOrgChart.GroupItemDataBoundEvent, value);
			}
		}

		// Token: 0x0600753D RID: 30013 RVA: 0x001B505C File Offset: 0x001B325C
		protected virtual void OnGroupItemDataBound(OrgChartGroupItemDataBoundEventArguments e)
		{
			OrgChartGroupItemDataBoundEventHandler orgChartGroupItemDataBoundEventHandler = (OrgChartGroupItemDataBoundEventHandler)base.Events[RadOrgChart.GroupItemDataBoundEvent];
			if (orgChartGroupItemDataBoundEventHandler != null)
			{
				orgChartGroupItemDataBoundEventHandler(this, e);
			}
		}

		// Token: 0x1400011C RID: 284
		// (add) Token: 0x0600753E RID: 30014 RVA: 0x001B508A File Offset: 0x001B328A
		// (remove) Token: 0x0600753F RID: 30015 RVA: 0x001B509D File Offset: 0x001B329D
		public event OrgChartNodeDropEventHandler NodeDrop
		{
			add
			{
				base.Events.AddHandler(RadOrgChart.NodeDropEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadOrgChart.NodeDropEvent, value);
			}
		}

		// Token: 0x06007540 RID: 30016 RVA: 0x001B50B0 File Offset: 0x001B32B0
		protected virtual void OnNodeDrop(OrgChartNodeDropEventArguments e)
		{
			OrgChartNodeDropEventHandler orgChartNodeDropEventHandler = (OrgChartNodeDropEventHandler)base.Events[RadOrgChart.NodeDropEvent];
			if (orgChartNodeDropEventHandler != null)
			{
				orgChartNodeDropEventHandler(this, e);
			}
		}

		// Token: 0x1400011D RID: 285
		// (add) Token: 0x06007541 RID: 30017 RVA: 0x001B50DE File Offset: 0x001B32DE
		// (remove) Token: 0x06007542 RID: 30018 RVA: 0x001B50F1 File Offset: 0x001B32F1
		public event OrgChartGroupItemDropEventHandler GroupItemDrop
		{
			add
			{
				base.Events.AddHandler(RadOrgChart.GroupItemDropEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadOrgChart.GroupItemDropEvent, value);
			}
		}

		// Token: 0x06007543 RID: 30019 RVA: 0x001B5104 File Offset: 0x001B3304
		protected virtual void OnGroupItemDrop(OrgChartGroupItemDropEventArguments e)
		{
			OrgChartGroupItemDropEventHandler orgChartGroupItemDropEventHandler = (OrgChartGroupItemDropEventHandler)base.Events[RadOrgChart.GroupItemDropEvent];
			if (orgChartGroupItemDropEventHandler != null)
			{
				orgChartGroupItemDropEventHandler(this, e);
			}
		}

		// Token: 0x1400011E RID: 286
		// (add) Token: 0x06007544 RID: 30020 RVA: 0x001B5132 File Offset: 0x001B3332
		// (remove) Token: 0x06007545 RID: 30021 RVA: 0x001B5145 File Offset: 0x001B3345
		public event OrgChartDrillDownEventHandler DrillDown
		{
			add
			{
				base.Events.AddHandler(RadOrgChart.DrillDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadOrgChart.DrillDownEvent, value);
			}
		}

		// Token: 0x06007546 RID: 30022 RVA: 0x001B5158 File Offset: 0x001B3358
		protected virtual void OnDrillDown(OrgChartDrillDownEventArguments e)
		{
			OrgChartDrillDownEventHandler orgChartDrillDownEventHandler = (OrgChartDrillDownEventHandler)base.Events[RadOrgChart.DrillDownEvent];
			if (orgChartDrillDownEventHandler != null)
			{
				orgChartDrillDownEventHandler(this, e);
			}
		}

		// Token: 0x1400011F RID: 287
		// (add) Token: 0x06007547 RID: 30023 RVA: 0x001B5186 File Offset: 0x001B3386
		// (remove) Token: 0x06007548 RID: 30024 RVA: 0x001B5199 File Offset: 0x001B3399
		public event OrgChartNodeExpandCollapseEventHandler NodeExpandCollapse
		{
			add
			{
				base.Events.AddHandler(RadOrgChart.NodeExpandCollapseEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadOrgChart.NodeExpandCollapseEvent, value);
			}
		}

		// Token: 0x06007549 RID: 30025 RVA: 0x001B51AC File Offset: 0x001B33AC
		protected virtual void OnNodeExpandCollapse(OrgChartNodeExpandCollapseEventArguments e)
		{
			OrgChartNodeExpandCollapseEventHandler orgChartNodeExpandCollapseEventHandler = (OrgChartNodeExpandCollapseEventHandler)base.Events[RadOrgChart.NodeExpandCollapseEvent];
			if (orgChartNodeExpandCollapseEventHandler != null)
			{
				orgChartNodeExpandCollapseEventHandler(this, e);
			}
		}

		// Token: 0x14000120 RID: 288
		// (add) Token: 0x0600754A RID: 30026 RVA: 0x001B51DA File Offset: 0x001B33DA
		// (remove) Token: 0x0600754B RID: 30027 RVA: 0x001B51ED File Offset: 0x001B33ED
		public event OrgChartGroupExpandCollapseEventHandler GroupExpandCollapse
		{
			add
			{
				base.Events.AddHandler(RadOrgChart.GroupExpandCollapseEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadOrgChart.GroupExpandCollapseEvent, value);
			}
		}

		// Token: 0x0600754C RID: 30028 RVA: 0x001B5200 File Offset: 0x001B3400
		protected virtual void OnGroupExpandCollapse(OrgChartGroupExpandCollapseEventArguments e)
		{
			OrgChartGroupExpandCollapseEventHandler orgChartGroupExpandCollapseEventHandler = (OrgChartGroupExpandCollapseEventHandler)base.Events[RadOrgChart.GroupExpandCollapseEvent];
			if (orgChartGroupExpandCollapseEventHandler != null)
			{
				orgChartGroupExpandCollapseEventHandler(this, e);
			}
		}

		// Token: 0x1700262A RID: 9770
		// (get) Token: 0x0600754D RID: 30029 RVA: 0x001B522E File Offset: 0x001B342E
		// (set) Token: 0x0600754E RID: 30030 RVA: 0x001B5236 File Offset: 0x001B3436
		public string DefaultImageUrl { get; set; }

		// Token: 0x1700262B RID: 9771
		// (get) Token: 0x0600754F RID: 30031 RVA: 0x001B523F File Offset: 0x001B343F
		// (set) Token: 0x06007550 RID: 30032 RVA: 0x001B5247 File Offset: 0x001B3447
		public bool DisableDefaultImage { get; set; }

		// Token: 0x1700262C RID: 9772
		// (get) Token: 0x06007551 RID: 30033 RVA: 0x001B5250 File Offset: 0x001B3450
		// (set) Token: 0x06007552 RID: 30034 RVA: 0x001B5258 File Offset: 0x001B3458
		public string DataFieldID { get; set; }

		// Token: 0x1700262D RID: 9773
		// (get) Token: 0x06007553 RID: 30035 RVA: 0x001B5261 File Offset: 0x001B3461
		// (set) Token: 0x06007554 RID: 30036 RVA: 0x001B5269 File Offset: 0x001B3469
		public string DataFieldParentID { get; set; }

		// Token: 0x1700262E RID: 9774
		// (get) Token: 0x06007555 RID: 30037 RVA: 0x001B5272 File Offset: 0x001B3472
		// (set) Token: 0x06007556 RID: 30038 RVA: 0x001B527A File Offset: 0x001B347A
		public string DataImageUrlField { get; set; }

		// Token: 0x1700262F RID: 9775
		// (get) Token: 0x06007557 RID: 30039 RVA: 0x001B5283 File Offset: 0x001B3483
		// (set) Token: 0x06007558 RID: 30040 RVA: 0x001B528B File Offset: 0x001B348B
		public string DataImageAltTextField { get; set; }

		// Token: 0x17002630 RID: 9776
		// (get) Token: 0x06007559 RID: 30041 RVA: 0x001B5294 File Offset: 0x001B3494
		// (set) Token: 0x0600755A RID: 30042 RVA: 0x001B529C File Offset: 0x001B349C
		public string DataCollapsedField { get; set; }

		// Token: 0x17002631 RID: 9777
		// (get) Token: 0x0600755B RID: 30043 RVA: 0x001B52A5 File Offset: 0x001B34A5
		// (set) Token: 0x0600755C RID: 30044 RVA: 0x001B52AD File Offset: 0x001B34AD
		public string DataTextField { get; set; }

		// Token: 0x17002632 RID: 9778
		// (get) Token: 0x0600755D RID: 30045 RVA: 0x001B52B6 File Offset: 0x001B34B6
		// (set) Token: 0x0600755E RID: 30046 RVA: 0x001B52D1 File Offset: 0x001B34D1
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartGroupEnabledBinding GroupEnabledBinding
		{
			get
			{
				if (this._groupEnabledBinding == null)
				{
					this._groupEnabledBinding = new OrgChartGroupEnabledBinding();
				}
				return this._groupEnabledBinding;
			}
			set
			{
				this._groupEnabledBinding = value;
			}
		}

		// Token: 0x17002633 RID: 9779
		// (get) Token: 0x0600755F RID: 30047 RVA: 0x001B52DA File Offset: 0x001B34DA
		// (set) Token: 0x06007560 RID: 30048 RVA: 0x001B52FB File Offset: 0x001B34FB
		[DefaultValue(-1)]
		public int MaxDataBindDepth
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

		// Token: 0x17002634 RID: 9780
		// (get) Token: 0x06007561 RID: 30049 RVA: 0x001B5322 File Offset: 0x001B3522
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartRenderedFieldsSettings RenderedFields
		{
			get
			{
				if (this._renderedFields == null)
				{
					this._renderedFields = new OrgChartRenderedFieldsSettings();
				}
				return this._renderedFields;
			}
		}

		// Token: 0x17002635 RID: 9781
		// (get) Token: 0x06007562 RID: 30050 RVA: 0x001B533D File Offset: 0x001B353D
		// (set) Token: 0x06007563 RID: 30051 RVA: 0x001B5345 File Offset: 0x001B3545
		[TemplateContainer(typeof(OrgChartGroupItemRendererBase))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate ItemTemplate { get; set; }

		// Token: 0x17002636 RID: 9782
		// (get) Token: 0x06007564 RID: 30052 RVA: 0x001B534E File Offset: 0x001B354E
		// (set) Token: 0x06007565 RID: 30053 RVA: 0x001B5361 File Offset: 0x001B3561
		[DefaultValue(-1)]
		public int GroupColumnCount
		{
			get
			{
				if (this._groupColumnCount >= 0)
				{
					return this._groupColumnCount;
				}
				return -1;
			}
			set
			{
				this._groupColumnCount = value;
			}
		}

		// Token: 0x17002637 RID: 9783
		// (get) Token: 0x06007566 RID: 30054 RVA: 0x001B536C File Offset: 0x001B356C
		// (set) Token: 0x06007567 RID: 30055 RVA: 0x001B53A1 File Offset: 0x001B35A1
		[DefaultValue(false)]
		public bool EnableDragAndDrop
		{
			get
			{
				return ((bool?)this.ViewState["EnableDragAndDrop"]) ?? false;
			}
			set
			{
				this.ViewState["EnableDragAndDrop"] = value;
			}
		}

		// Token: 0x17002638 RID: 9784
		// (get) Token: 0x06007568 RID: 30056 RVA: 0x001B53BC File Offset: 0x001B35BC
		// (set) Token: 0x06007569 RID: 30057 RVA: 0x001B53F1 File Offset: 0x001B35F1
		public bool AllowGroupItemDragging
		{
			get
			{
				return ((bool?)this.ViewState["GroupItemDragging"]) ?? true;
			}
			set
			{
				this.ViewState["GroupItemDragging"] = value;
			}
		}

		// Token: 0x17002639 RID: 9785
		// (get) Token: 0x0600756A RID: 30058 RVA: 0x001B540C File Offset: 0x001B360C
		// (set) Token: 0x0600756B RID: 30059 RVA: 0x001B5441 File Offset: 0x001B3641
		[DefaultValue(false)]
		public bool EnableCollapsing
		{
			get
			{
				return ((bool?)this.ViewState["EnableCollapsing"]) ?? false;
			}
			set
			{
				this.ViewState["EnableCollapsing"] = value;
			}
		}

		// Token: 0x1700263A RID: 9786
		// (get) Token: 0x0600756C RID: 30060 RVA: 0x001B545C File Offset: 0x001B365C
		// (set) Token: 0x0600756D RID: 30061 RVA: 0x001B5491 File Offset: 0x001B3691
		[DefaultValue(false)]
		public bool EnableGroupCollapsing
		{
			get
			{
				return ((bool?)this.ViewState["EnableGroupCollapsing"]) ?? false;
			}
			set
			{
				this.ViewState["EnableGroupCollapsing"] = value;
			}
		}

		// Token: 0x1700263B RID: 9787
		// (get) Token: 0x0600756E RID: 30062 RVA: 0x001B54AC File Offset: 0x001B36AC
		// (set) Token: 0x0600756F RID: 30063 RVA: 0x001B54E1 File Offset: 0x001B36E1
		[DefaultValue(true)]
		public bool PersistExpandCollapseState
		{
			get
			{
				return ((bool?)this.ViewState["PersistExpandCollapseState"]) ?? true;
			}
			set
			{
				this.ViewState["PersistExpandCollapseState"] = value;
			}
		}

		// Token: 0x1700263C RID: 9788
		// (get) Token: 0x06007570 RID: 30064 RVA: 0x001B54FC File Offset: 0x001B36FC
		// (set) Token: 0x06007571 RID: 30065 RVA: 0x001B5531 File Offset: 0x001B3731
		[DefaultValue(false)]
		public bool EnableDrillDown
		{
			get
			{
				return ((bool?)this.ViewState["EnableDrillDown"]) ?? false;
			}
			set
			{
				this.ViewState["EnableDrillDown"] = value;
			}
		}

		// Token: 0x1700263D RID: 9789
		// (get) Token: 0x06007572 RID: 30066 RVA: 0x001B554C File Offset: 0x001B374C
		// (set) Token: 0x06007573 RID: 30067 RVA: 0x001B5581 File Offset: 0x001B3781
		public virtual OrgChartLoadOnDemand LoadOnDemand
		{
			get
			{
				OrgChartLoadOnDemand? orgChartLoadOnDemand = (OrgChartLoadOnDemand?)this.ViewState["LoadOnDemand"];
				if (orgChartLoadOnDemand == null)
				{
					return OrgChartLoadOnDemand.None;
				}
				return orgChartLoadOnDemand.GetValueOrDefault();
			}
			set
			{
				if ((value & OrgChartLoadOnDemand.Nodes) == OrgChartLoadOnDemand.Nodes)
				{
					this.EnableCollapsing = true;
				}
				if ((value & OrgChartLoadOnDemand.Groups) == OrgChartLoadOnDemand.Groups)
				{
					this.EnableGroupCollapsing = true;
				}
				this.ViewState["LoadOnDemand"] = value;
			}
		}

		// Token: 0x1700263E RID: 9790
		// (get) Token: 0x06007574 RID: 30068 RVA: 0x001B55B3 File Offset: 0x001B37B3
		// (set) Token: 0x06007575 RID: 30069 RVA: 0x001B55DC File Offset: 0x001B37DC
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartWebServiceBindings WebServiceBindings
		{
			get
			{
				if (this._webServiceBindings == null)
				{
					this._webServiceBindings = new OrgChartWebServiceBindings();
					this.EnableCollapsing = true;
					this.EnableGroupCollapsing = true;
				}
				return this._webServiceBindings;
			}
			set
			{
				this.EnableCollapsing = true;
				this.EnableGroupCollapsing = true;
				this._webServiceBindings = value;
			}
		}

		// Token: 0x1700263F RID: 9791
		// (get) Token: 0x06007576 RID: 30070 RVA: 0x001B55F4 File Offset: 0x001B37F4
		// (set) Token: 0x06007577 RID: 30071 RVA: 0x001B5629 File Offset: 0x001B3829
		public Orientation Orientation
		{
			get
			{
				Orientation? orientation = (Orientation?)this.ViewState["Orientation"];
				if (orientation == null)
				{
					return Orientation.Vertical;
				}
				return orientation.GetValueOrDefault();
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x06007578 RID: 30072 RVA: 0x001B564A File Offset: 0x001B384A
		public IEnumerable<OrgChartGroupItem> GetAllGroupItems()
		{
			return this.GetGroupItems((OrgChartGroupItem x) => x != null);
		}

		// Token: 0x06007579 RID: 30073 RVA: 0x001B566F File Offset: 0x001B386F
		[Obsolete("This method is obsolete. Please use the GetGroupItems(OrgChartGroupItemCriteria criteria) method instead.")]
		public IEnumerable<OrgChartGroupItem> GetAllGroupItems(RadOrgChart.OrgChartGroupItemCriteria criteria)
		{
			return this.GetGroupItems(criteria);
		}

		// Token: 0x0600757A RID: 30074 RVA: 0x001B5908 File Offset: 0x001B3B08
		public IEnumerable<OrgChartGroupItem> GetGroupItems(RadOrgChart.OrgChartGroupItemCriteria criteria)
		{
			Queue<OrgChartNode> nodesQueue = new Queue<OrgChartNode>();
			using (List<OrgChartNode>.Enumerator enumerator = this.Nodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OrgChartNode item2 = enumerator.Current;
					nodesQueue.Enqueue(item2);
				}
				goto IL_133;
			}
			IL_6D:
			OrgChartNode node = nodesQueue.Dequeue();
			foreach (OrgChartNode item3 in node.Nodes)
			{
				nodesQueue.Enqueue(item3);
			}
			foreach (OrgChartGroupItem item in node.GroupItems)
			{
				if (criteria(item))
				{
					yield return item;
				}
			}
			IL_133:
			if (nodesQueue.Count <= 0)
			{
				yield break;
			}
			goto IL_6D;
		}

		// Token: 0x0600757B RID: 30075 RVA: 0x001B5935 File Offset: 0x001B3B35
		public IEnumerable<OrgChartNode> GetAllNodes()
		{
			return this.GetNodes((OrgChartNode x) => x != null);
		}

		// Token: 0x0600757C RID: 30076 RVA: 0x001B5B1C File Offset: 0x001B3D1C
		public IEnumerable<OrgChartNode> GetNodes(RadOrgChart.OrgChartNodeCriteria criteria)
		{
			Queue<OrgChartNode> nodesQueue = new Queue<OrgChartNode>();
			using (List<OrgChartNode>.Enumerator enumerator = this.Nodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OrgChartNode item = enumerator.Current;
					nodesQueue.Enqueue(item);
				}
				goto IL_ED;
			}
			IL_6C:
			OrgChartNode node = nodesQueue.Dequeue();
			foreach (OrgChartNode item2 in node.Nodes)
			{
				nodesQueue.Enqueue(item2);
			}
			if (criteria(node))
			{
				yield return node;
			}
			IL_ED:
			if (nodesQueue.Count <= 0)
			{
				yield break;
			}
			goto IL_6C;
		}

		// Token: 0x0600757D RID: 30077 RVA: 0x001B5B49 File Offset: 0x001B3D49
		public void CollapseAllNodes()
		{
			this.CollapseNodes((OrgChartNode x) => x != null);
		}

		// Token: 0x0600757E RID: 30078 RVA: 0x001B5B70 File Offset: 0x001B3D70
		public void CollapseNodes(RadOrgChart.OrgChartExpandCollapseNodeCriteria criteria)
		{
			Queue<OrgChartNode> queue = new Queue<OrgChartNode>();
			using (List<OrgChartNode>.Enumerator enumerator = this.Nodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OrgChartNode item = enumerator.Current;
					queue.Enqueue(item);
				}
				goto IL_8B;
			}
			IL_3D:
			OrgChartNode orgChartNode = queue.Dequeue();
			foreach (OrgChartNode item2 in orgChartNode.Nodes)
			{
				queue.Enqueue(item2);
			}
			if (criteria(orgChartNode))
			{
				orgChartNode.Collapsed = true;
			}
			IL_8B:
			if (queue.Count <= 0)
			{
				return;
			}
			goto IL_3D;
		}

		// Token: 0x0600757F RID: 30079 RVA: 0x001B5C39 File Offset: 0x001B3E39
		public void ExpandAllNodes()
		{
			this.ExpandNodes((OrgChartNode x) => x != null);
		}

		// Token: 0x06007580 RID: 30080 RVA: 0x001B5C60 File Offset: 0x001B3E60
		public void ExpandNodes(RadOrgChart.OrgChartExpandCollapseNodeCriteria criteria)
		{
			Queue<OrgChartNode> queue = new Queue<OrgChartNode>();
			using (List<OrgChartNode>.Enumerator enumerator = this.Nodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OrgChartNode item = enumerator.Current;
					queue.Enqueue(item);
				}
				goto IL_8B;
			}
			IL_3D:
			OrgChartNode orgChartNode = queue.Dequeue();
			foreach (OrgChartNode item2 in orgChartNode.Nodes)
			{
				queue.Enqueue(item2);
			}
			if (criteria(orgChartNode))
			{
				orgChartNode.Collapsed = false;
			}
			IL_8B:
			if (queue.Count <= 0)
			{
				return;
			}
			goto IL_3D;
		}

		// Token: 0x06007581 RID: 30081 RVA: 0x001B5D29 File Offset: 0x001B3F29
		public void CollapseAllGroups()
		{
			this.CollapseGroups((OrgChartNode x) => x != null);
		}

		// Token: 0x06007582 RID: 30082 RVA: 0x001B5D50 File Offset: 0x001B3F50
		public void CollapseGroups(RadOrgChart.OrgChartExpandCollapseNodeCriteria criteria)
		{
			Queue<OrgChartNode> queue = new Queue<OrgChartNode>();
			using (List<OrgChartNode>.Enumerator enumerator = this.Nodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OrgChartNode item = enumerator.Current;
					queue.Enqueue(item);
				}
				goto IL_8B;
			}
			IL_3D:
			OrgChartNode orgChartNode = queue.Dequeue();
			foreach (OrgChartNode item2 in orgChartNode.Nodes)
			{
				queue.Enqueue(item2);
			}
			if (criteria(orgChartNode))
			{
				orgChartNode.GroupCollapsed = true;
			}
			IL_8B:
			if (queue.Count <= 0)
			{
				return;
			}
			goto IL_3D;
		}

		// Token: 0x06007583 RID: 30083 RVA: 0x001B5E19 File Offset: 0x001B4019
		public void ExpandAllGroups()
		{
			this.ExpandGroups((OrgChartNode x) => x != null);
		}

		// Token: 0x06007584 RID: 30084 RVA: 0x001B5E40 File Offset: 0x001B4040
		public void ExpandGroups(RadOrgChart.OrgChartExpandCollapseNodeCriteria criteria)
		{
			Queue<OrgChartNode> queue = new Queue<OrgChartNode>();
			using (List<OrgChartNode>.Enumerator enumerator = this.Nodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OrgChartNode item = enumerator.Current;
					queue.Enqueue(item);
				}
				goto IL_8B;
			}
			IL_3D:
			OrgChartNode orgChartNode = queue.Dequeue();
			foreach (OrgChartNode item2 in orgChartNode.Nodes)
			{
				queue.Enqueue(item2);
			}
			if (criteria(orgChartNode))
			{
				orgChartNode.GroupCollapsed = false;
			}
			IL_8B:
			if (queue.Count <= 0)
			{
				return;
			}
			goto IL_3D;
		}

		// Token: 0x17002640 RID: 9792
		// (get) Token: 0x06007585 RID: 30085 RVA: 0x001B5F00 File Offset: 0x001B4100
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartNodeCollection Nodes
		{
			get
			{
				if (this._nodes == null)
				{
					this._nodes = new OrgChartNodeCollection(this, this);
				}
				return this._nodes;
			}
		}

		// Token: 0x17002641 RID: 9793
		// (get) Token: 0x06007586 RID: 30086 RVA: 0x001B5F1D File Offset: 0x001B411D
		// (set) Token: 0x06007587 RID: 30087 RVA: 0x001B5F3D File Offset: 0x001B413D
		public string OnClientNodePopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientNodePopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodePopulating"] = value;
			}
		}

		// Token: 0x17002642 RID: 9794
		// (get) Token: 0x06007588 RID: 30088 RVA: 0x001B5F50 File Offset: 0x001B4150
		// (set) Token: 0x06007589 RID: 30089 RVA: 0x001B5F70 File Offset: 0x001B4170
		public string OnClientNodePopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientNodePopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodePopulated"] = value;
			}
		}

		// Token: 0x17002643 RID: 9795
		// (get) Token: 0x0600758A RID: 30090 RVA: 0x001B5F83 File Offset: 0x001B4183
		// (set) Token: 0x0600758B RID: 30091 RVA: 0x001B5FA3 File Offset: 0x001B41A3
		public string OnClientNodePopulationFailed
		{
			get
			{
				return (string)(this.ViewState["OnClientNodePopulatingFailed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodePopulatingFailed"] = value;
			}
		}

		// Token: 0x17002644 RID: 9796
		// (get) Token: 0x0600758C RID: 30092 RVA: 0x001B5FB6 File Offset: 0x001B41B6
		// (set) Token: 0x0600758D RID: 30093 RVA: 0x001B5FD6 File Offset: 0x001B41D6
		public string OnClientGroupPopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientGroupPopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientGroupPopulating"] = value;
			}
		}

		// Token: 0x17002645 RID: 9797
		// (get) Token: 0x0600758E RID: 30094 RVA: 0x001B5FE9 File Offset: 0x001B41E9
		// (set) Token: 0x0600758F RID: 30095 RVA: 0x001B6009 File Offset: 0x001B4209
		public string OnClientGroupPopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientGroupPopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientGroupPopulated"] = value;
			}
		}

		// Token: 0x17002646 RID: 9798
		// (get) Token: 0x06007590 RID: 30096 RVA: 0x001B601C File Offset: 0x001B421C
		// (set) Token: 0x06007591 RID: 30097 RVA: 0x001B603C File Offset: 0x001B423C
		public string OnClientGroupPopulationFailed
		{
			get
			{
				return (string)(this.ViewState["OnClientGroupPopulatingFailed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientGroupPopulatingFailed"] = value;
			}
		}

		// Token: 0x06007592 RID: 30098 RVA: 0x001B6050 File Offset: 0x001B4250
		public string GetXml()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(RadOrgChart));
			StringWriter stringWriter = new StringWriter();
			xmlSerializer.Serialize(stringWriter, this);
			return stringWriter.ToString();
		}

		// Token: 0x06007593 RID: 30099 RVA: 0x001B6081 File Offset: 0x001B4281
		public void LoadContentFile(string xmlFileName)
		{
			this.LoadXml(File.ReadAllText(this.Context.Server.MapPath(xmlFileName)));
		}

		// Token: 0x06007594 RID: 30100 RVA: 0x001B60A0 File Offset: 0x001B42A0
		public void LoadXml(string xml)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(RadOrgChart));
			RadOrgChart radOrgChart = (RadOrgChart)xmlSerializer.Deserialize(new StringReader(xml));
			this.GroupColumnCount = radOrgChart.GroupColumnCount;
			this.EnableDragAndDrop = radOrgChart.EnableDragAndDrop;
			this.EnableCollapsing = radOrgChart.EnableCollapsing;
			this.EnableGroupCollapsing = radOrgChart.EnableGroupCollapsing;
			this.EnableDrillDown = radOrgChart.EnableDrillDown;
			this.LoadOnDemand = radOrgChart.LoadOnDemand;
			this.Nodes.Clear();
			foreach (OrgChartNode node in radOrgChart.Nodes)
			{
				this.Nodes.Add(node);
			}
		}

		// Token: 0x060075A0 RID: 30112 RVA: 0x001B6170 File Offset: 0x001B4370
		// Note: this type is marked as 'beforefieldinit'.
		static RadOrgChart()
		{
			RadOrgChart.NodeDataBoundEvent = new object();
			RadOrgChart.GroupItemDataBoundEvent = new object();
			RadOrgChart.NodeDropEvent = new object();
			RadOrgChart.GroupItemDropEvent = new object();
			RadOrgChart.DrillDownEvent = new object();
			RadOrgChart.NodeExpandCollapseEvent = new object();
			RadOrgChart.GroupExpandCollapseEvent = new object();
		}

		// Token: 0x04002011 RID: 8209
		private OrgChartNodeCollection _nodes;

		// Token: 0x04002012 RID: 8210
		private OrgChartGroupEnabledBinding _groupEnabledBinding;

		// Token: 0x04002013 RID: 8211
		private Dictionary<object, List<OrgChartGroupItem>> _itemsHash;

		// Token: 0x04002014 RID: 8212
		private OrgChartRenderedFieldsSettings _renderedFields;

		// Token: 0x04002015 RID: 8213
		private int _groupColumnCount = -1;

		// Token: 0x04002016 RID: 8214
		private int _hierarchicalLevel;

		// Token: 0x04002017 RID: 8215
		private int[] _hierarchicalIndexes;

		// Token: 0x04002018 RID: 8216
		private OrgChartCallbackArguments _callbackArguments;

		// Token: 0x04002019 RID: 8217
		private string[] _expandedNodesIndexes;

		// Token: 0x0400201A RID: 8218
		private string[] _collapsedNodesIndexes;

		// Token: 0x0400201B RID: 8219
		private string[] _expandedGroupsIndexes;

		// Token: 0x0400201C RID: 8220
		private string[] _collapsedGroupsIndexes;

		// Token: 0x0400201D RID: 8221
		private bool _isExpandCollapseStateApplied;

		// Token: 0x0400201E RID: 8222
		internal Dictionary<string, bool> expandCollapseNodesID = new Dictionary<string, bool>();

		// Token: 0x0400201F RID: 8223
		internal Dictionary<string, bool> expandCollapseGroupsID = new Dictionary<string, bool>();

		// Token: 0x04002020 RID: 8224
		internal string _drilledNodeHierarchicalIndex;

		// Token: 0x04002021 RID: 8225
		internal bool _isDrillDown;

		// Token: 0x04002022 RID: 8226
		internal int[] _drilledNodeIndexes;

		// Token: 0x04002023 RID: 8227
		internal OrgChartWebServiceBindings _webServiceBindings;

		// Token: 0x04002024 RID: 8228
		internal OrgChartPostBackArguments postbackArgs;

		// Token: 0x02000C08 RID: 3080
		// (Invoke) Token: 0x060075A2 RID: 30114
		public delegate bool OrgChartGroupItemCriteria(OrgChartGroupItem item);

		// Token: 0x02000C09 RID: 3081
		// (Invoke) Token: 0x060075A6 RID: 30118
		public delegate bool OrgChartNodeCriteria(OrgChartNode node);

		// Token: 0x02000C0A RID: 3082
		// (Invoke) Token: 0x060075AA RID: 30122
		public delegate bool OrgChartExpandCollapseNodeCriteria(OrgChartNode node);
	}
}
