using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.TreeView.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x0200129B RID: 4763
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadTreeView))]
	[RequiredScript(typeof(TouchScrollExtender))]
	[RequiredScript(typeof(MaterialRipple))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("TreeView", typeof(RadTreeView))]
	[EmbeddedSkin("TreeView", "Default", typeof(RadTreeView))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(OData))]
	[ClientScriptResource("Telerik.Web.UI.RadTreeView", "Telerik.Web.UI.TreeView.RadTreeViewScripts.js")]
	[XmlRoot("Tree")]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadTreeView), "Telerik.Web.UI.TreeView.png")]
	[Designer("Telerik.Web.Design.RadTreeViewDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadTreeView Runat=\"server\"></{0}:RadTreeView>")]
	[ControlValueProperty("SelectedValue")]
	[ValidationProperty("SelectedValue")]
	[DefaultEvent("NodeClick")]
	[LightweightRendering]
	public class RadTreeView : HierarchicalControlItemContainer, IRadTreeNodeContainer, IPostBackEventHandler, ICallbackEventHandler
	{
		// Token: 0x0600C667 RID: 50791 RVA: 0x002C40BD File Offset: 0x002C22BD
		protected override NavigationItemBindingCollection CreateDataBindings()
		{
			return new RadTreeNodeBindingCollection();
		}

		// Token: 0x17004014 RID: 16404
		// (get) Token: 0x0600C668 RID: 50792 RVA: 0x002C40C4 File Offset: 0x002C22C4
		// (set) Token: 0x0600C669 RID: 50793 RVA: 0x002C40CC File Offset: 0x002C22CC
		[ClientPropertyName("_scrollPosition")]
		[ClientControlProperty]
		internal int ScrollPosition
		{
			get
			{
				return this._scrollPosition;
			}
			set
			{
				this._scrollPosition = value;
			}
		}

		// Token: 0x17004015 RID: 16405
		// (get) Token: 0x0600C66A RID: 50794 RVA: 0x002C40D8 File Offset: 0x002C22D8
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadTreeView RadTreeView_{0}";
				if (base.Attributes["dir"] == "rtl")
				{
					text = "RadTreeView RadTreeView_rtl RadTreeView_{0} RadTreeView_{0}_rtl";
				}
				if (!base.IsEnabled)
				{
					text += " RadTreeView_{0}_disabled";
				}
				if (base.DesignMode)
				{
					text += " RadTreeView_designtime RadTreeView_{0}_designtime";
				}
				return text;
			}
		}

		// Token: 0x17004016 RID: 16406
		// (get) Token: 0x0600C66B RID: 50795 RVA: 0x002C4136 File Offset: 0x002C2336
		internal bool IsControlEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x17004017 RID: 16407
		// (get) Token: 0x0600C66C RID: 50796 RVA: 0x002C413E File Offset: 0x002C233E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004018 RID: 16408
		// (get) Token: 0x0600C66D RID: 50797 RVA: 0x002C4141 File Offset: 0x002C2341
		internal override bool SupportsOData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17004019 RID: 16409
		// (get) Token: 0x0600C66E RID: 50798 RVA: 0x002C4144 File Offset: 0x002C2344
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600C66F RID: 50799 RVA: 0x002C4147 File Offset: 0x002C2347
		protected override void RaiseItemCreated(ControlItem item)
		{
			this.OnNodeCreated(new RadTreeNodeEventArgs((RadTreeNode)item));
		}

		// Token: 0x0600C670 RID: 50800 RVA: 0x002C415A File Offset: 0x002C235A
		protected override void RaiseTemplateNeeded(ControlItem item)
		{
			this.OnTemplateNeeded(new RadTreeNodeEventArgs((RadTreeNode)item));
		}

		// Token: 0x0600C671 RID: 50801 RVA: 0x002C416D File Offset: 0x002C236D
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnNodeDataBound(new RadTreeNodeEventArgs((RadTreeNode)item));
		}

		// Token: 0x0600C672 RID: 50802 RVA: 0x002C4180 File Offset: 0x002C2380
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadTreeNodeCollection(this);
		}

		// Token: 0x0600C673 RID: 50803 RVA: 0x002C4188 File Offset: 0x002C2388
		protected internal override ControlItem CreateItem()
		{
			return new RadTreeNode();
		}

		// Token: 0x0600C674 RID: 50804 RVA: 0x002C418F File Offset: 0x002C238F
		protected internal override IRenderer CreateControlRenderer()
		{
			return RendererFactory.CreateTreeViewRenderer(this);
		}

		// Token: 0x0600C675 RID: 50805 RVA: 0x002C4198 File Offset: 0x002C2398
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.TabIndex == 0 && this.ViewState["TabIndex"] != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString(NumberFormatInfo.InvariantInfo));
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600C676 RID: 50806 RVA: 0x002C41E1 File Offset: 0x002C23E1
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x1700401A RID: 16410
		// (get) Token: 0x0600C677 RID: 50807 RVA: 0x002C41EF File Offset: 0x002C23EF
		IRadTreeNodeContainer IRadTreeNodeContainer.Owner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600C678 RID: 50808 RVA: 0x002C41F2 File Offset: 0x002C23F2
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x1700401B RID: 16411
		// (get) Token: 0x0600C679 RID: 50809 RVA: 0x002C4200 File Offset: 0x002C2400
		internal bool ShouldRenderPostBackReference
		{
			get
			{
				return base.Events[RadTreeView.NodeClickEvent] != null || base.Events[RadTreeView.NodeDropEvent] != null || base.Events[RadTreeView.NodeCheckEvent] != null || base.Events[RadTreeView.NodeEditEvent] != null || base.Events[RadTreeView.NodeCollapseEvent] != null || base.Events[RadTreeView.NodeExpandEvent] != null || base.Events[RadTreeView.ContextMenuItemClickEvent] != null;
			}
		}

		// Token: 0x0600C67A RID: 50810 RVA: 0x002C4294 File Offset: 0x002C2494
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptConverter[] converters = new JavaScriptConverter[]
			{
				new TreeNodeJavaScriptConverter(),
				new AttributeCollectionConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			descriptor.AddScriptProperty("nodeData", javaScriptSerializer.Serialize(this.Nodes.VisibleItems));
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (!string.IsNullOrEmpty(this.ClientNodeTemplate))
			{
				descriptor.AddProperty("_clientTemplate", this.ClientNodeTemplate);
			}
			if (this.ShouldRegisterCallbackEventReference())
			{
				this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			}
			if (this.IsBoundUsingOData && this.DataBindings.Count > 0)
			{
				descriptor.AddScriptProperty("dataBindings", javaScriptSerializer.Serialize(DataBindingsCollection.FromStateManagedCollection(this.DataBindings)));
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				descriptor.AddProperty("_dataFieldParentID", this.DataFieldParentID);
				descriptor.AddProperty("_dataFieldID", this.DataFieldID);
				descriptor.AddProperty("_dataNavigateUrlField", this.DataNavigateUrlField);
			}
			base.DescribeRenderingMode(descriptor);
			this.DescribePostBack(descriptor);
			this.DescribeSelectedNodes(descriptor, javaScriptSerializer);
			this.DescribeCheckedNodes(descriptor, javaScriptSerializer);
			this.DescribeContextMenuIDs(descriptor, javaScriptSerializer);
			this.DescribeLoadingTemplate(descriptor);
			this.DescribeExpandedAndCollapsedNodes(descriptor, javaScriptSerializer);
			this.WebServiceSettings.Describe("webServiceSettings", javaScriptSerializer, descriptor);
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
		}

		// Token: 0x0600C67B RID: 50811 RVA: 0x002C4444 File Offset: 0x002C2644
		private bool ShouldRegisterCallbackEventReference()
		{
			foreach (RadTreeNode radTreeNode in this.GetAllNodes())
			{
				if (radTreeNode.ExpandMode == TreeNodeExpandMode.ServerSideCallBack)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600C67C RID: 50812 RVA: 0x002C449C File Offset: 0x002C269C
		private bool ShouldSerializeTriStateCheckBoxes()
		{
			return this.CheckBoxes;
		}

		// Token: 0x0600C67D RID: 50813 RVA: 0x002C44A4 File Offset: 0x002C26A4
		private void DescribeLoadingTemplate(IScriptDescriptor descriptor)
		{
			if (this.LoadingStatusTemplate != null)
			{
				Control control = new Control();
				this.Controls.Add(control);
				this.LoadingStatusTemplate.InstantiateIn(control);
				StringWriter stringWriter = new StringWriter();
				control.RenderControl(new HtmlTextWriter(stringWriter));
				descriptor.AddProperty("loadingMessage", stringWriter.ToString());
				this.Controls.Remove(control);
			}
		}

		// Token: 0x0600C67E RID: 50814 RVA: 0x002C4508 File Offset: 0x002C2708
		private void DescribePostBack(IScriptDescriptor descriptor)
		{
			if (base.Events[RadTreeView.NodeCheckEvent] != null)
			{
				descriptor.AddProperty("_postBackOnCheck", true);
			}
			if (base.Events[RadTreeView.NodeClickEvent] != null)
			{
				descriptor.AddProperty("_postBackOnClick", true);
			}
			if (base.Events[RadTreeView.NodeExpandEvent] != null)
			{
				descriptor.AddProperty("_postBackOnExpand", true);
			}
			if (base.Events[RadTreeView.NodeEditEvent] != null)
			{
				descriptor.AddProperty("_postBackOnEdit", true);
			}
			if (base.Events[RadTreeView.NodeCollapseEvent] != null)
			{
				descriptor.AddProperty("_postBackOnCollapse", true);
			}
			if (base.Events[RadTreeView.ContextMenuItemClickEvent] != null)
			{
				descriptor.AddProperty("_postBackOnContextMenuItemClick", true);
			}
			if (this.ShouldRenderPostBackReference)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
		}

		// Token: 0x0600C67F RID: 50815 RVA: 0x002C4600 File Offset: 0x002C2800
		private void DescribeExpandedAndCollapsedNodes(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			if (base.IsViewStateEnabled)
			{
				return;
			}
			if (this._expandedNodes.Count > 0)
			{
				descriptor.AddScriptProperty("expandedIndexes", serializer.Serialize(this._expandedNodes));
			}
			if (this._collapsedNodes.Count > 0)
			{
				descriptor.AddScriptProperty("collapsedIndexes", serializer.Serialize(this._collapsedNodes));
			}
		}

		// Token: 0x0600C680 RID: 50816 RVA: 0x002C4660 File Offset: 0x002C2860
		private void DescribeSelectedNodes(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			IList<RadTreeNode> selectedNodes = this.SelectedNodes;
			if (selectedNodes.Count > 0)
			{
				List<string> list = new List<string>();
				foreach (RadTreeNode radTreeNode in selectedNodes)
				{
					if (radTreeNode.Visible)
					{
						list.Add(radTreeNode.HierarchicalIndex);
					}
				}
				descriptor.AddScriptProperty("selectedIndexes", serializer.Serialize(list));
			}
		}

		// Token: 0x0600C681 RID: 50817 RVA: 0x002C46E0 File Offset: 0x002C28E0
		private void DescribeCheckedNodes(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			if (this.CheckBoxes)
			{
				IList<RadTreeNode> checkedNodes = this.CheckedNodes;
				if (checkedNodes.Count > 0)
				{
					List<string> list = new List<string>();
					foreach (RadTreeNode radTreeNode in checkedNodes)
					{
						if (radTreeNode.Visible)
						{
							list.Add(radTreeNode.HierarchicalIndex);
						}
					}
					descriptor.AddScriptProperty("checkedIndexes", serializer.Serialize(list));
				}
			}
		}

		// Token: 0x0600C682 RID: 50818 RVA: 0x002C4768 File Offset: 0x002C2968
		private void DescribeContextMenuIDs(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			if (this.ContextMenus.Count > 0)
			{
				List<string> list = new List<string>();
				foreach (object obj in this.ContextMenus)
				{
					RadTreeViewContextMenu radTreeViewContextMenu = (RadTreeViewContextMenu)obj;
					if (radTreeViewContextMenu.Visible)
					{
						list.Add(radTreeViewContextMenu.ID);
					}
				}
				descriptor.AddScriptProperty("contextMenuIDs", serializer.Serialize(list));
			}
		}

		// Token: 0x0600C683 RID: 50819 RVA: 0x002C47F4 File Offset: 0x002C29F4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.ScriptManager.LoadScriptsBeforeUI)
			{
				string text = string.Format("Telerik.Web.UI.RadTreeView._preInitialize(\"{0}\",\"{1}\");", this.ClientID, this.ScrollPosition);
				ScriptManager.RegisterStartupScript(this.Page, typeof(RadTreeView), this.ClientID + text, text, true);
			}
			if (base.ScriptManager.IsInAsyncPostBack && base.IsViewStateEnabled)
			{
				string text2 = string.Format("Telerik.Web.UI.RadTreeView._clearLog(\"{0}\");", this.ClientID);
				ScriptManager.RegisterStartupScript(this.Page, typeof(RadTreeView), this.ClientID + text2, text2, true);
			}
		}

		// Token: 0x0600C684 RID: 50820 RVA: 0x002C489D File Offset: 0x002C2A9D
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x0600C685 RID: 50821 RVA: 0x002C48A8 File Offset: 0x002C2AA8
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			TreeViewPostBackArguments treeViewPostBackArguments = javaScriptSerializer.Deserialize<TreeViewPostBackArguments>(eventArgument);
			RadTreeNode radTreeNode = (RadTreeNode)this.FindItemByHierarchicalIndex(treeViewPostBackArguments.Index);
			List<RadTreeNode> list = new List<RadTreeNode>();
			if (treeViewPostBackArguments.SourceNodesIndices != null)
			{
				foreach (string hierarchicalIndex in treeViewPostBackArguments.SourceNodesIndices)
				{
					list.Add((RadTreeNode)this.FindItemByHierarchicalIndex(hierarchicalIndex));
				}
			}
			switch (treeViewPostBackArguments.CommandName)
			{
			case TreeViewPostBackCommand.Click:
				if (radTreeNode != null)
				{
					this.OnNodeClick(new RadTreeNodeEventArgs(radTreeNode));
					return;
				}
				break;
			case TreeViewPostBackCommand.Check:
				if (radTreeNode != null)
				{
					this.OnNodeCheck(new RadTreeNodeEventArgs(radTreeNode));
					return;
				}
				break;
			case TreeViewPostBackCommand.Collapse:
				if (radTreeNode != null)
				{
					this.OnNodeCollapse(new RadTreeNodeEventArgs(radTreeNode));
					return;
				}
				break;
			case TreeViewPostBackCommand.NodeEdit:
				if (radTreeNode != null)
				{
					string text = HttpUtility.UrlDecode(treeViewPostBackArguments.NodeEditText);
					text = text.Replace("&squote", "'");
					this.OnNodeEdit(new RadTreeNodeEditEventArgs(radTreeNode, text));
				}
				break;
			case TreeViewPostBackCommand.NodeDrop:
			{
				RadTreeNode destinationNode = (RadTreeNode)this.FindItemByHierarchicalIndex(treeViewPostBackArguments.DestIndex);
				this.OnNodeDrop(new RadTreeNodeDragDropEventArgs(list, destinationNode, treeViewPostBackArguments.DropPosition));
				return;
			}
			case TreeViewPostBackCommand.NodeDropOnTree:
			{
				RadTreeView radTreeView = this.Page.FindControl(treeViewPostBackArguments.TreeId) as RadTreeView;
				if (radTreeView != null)
				{
					RadTreeNode destinationNode2 = (RadTreeNode)radTreeView.FindItemByHierarchicalIndex(treeViewPostBackArguments.DestIndex);
					this.OnNodeDrop(new RadTreeNodeDragDropEventArgs(list, destinationNode2, treeViewPostBackArguments.DropPosition));
					return;
				}
				break;
			}
			case TreeViewPostBackCommand.NodeDropOnHtmlElement:
				this.OnNodeDrop(new RadTreeNodeDragDropEventArgs(list, treeViewPostBackArguments.HtmlElementId));
				return;
			case TreeViewPostBackCommand.LOD:
				break;
			case TreeViewPostBackCommand.Expand:
				if (radTreeNode != null)
				{
					this.OnNodeExpand(new RadTreeNodeEventArgs(radTreeNode));
					return;
				}
				break;
			case TreeViewPostBackCommand.ContextMenuItemClick:
			{
				RadContextMenu radContextMenu = this.ContextMenus.FindByClientId(treeViewPostBackArguments.ContextMenuID);
				RadMenuItem menuItem = (RadMenuItem)radContextMenu.FindItemByHierarchicalIndex(treeViewPostBackArguments.MenuItemIndex);
				this.OnContextMenuItemClick(new RadTreeViewContextMenuEventArgs(radTreeNode, menuItem));
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0600C686 RID: 50822 RVA: 0x002C4AA8 File Offset: 0x002C2CA8
		private void RaiseEvent(object eventKey, RadTreeNodeEventArgs e)
		{
			RadTreeViewEventHandler radTreeViewEventHandler = (RadTreeViewEventHandler)base.Events[eventKey];
			if (radTreeViewEventHandler != null)
			{
				radTreeViewEventHandler(this, e);
			}
		}

		// Token: 0x0600C687 RID: 50823 RVA: 0x002C4AD4 File Offset: 0x002C2CD4
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				this.LoadClientState(javaScriptSerializer.Deserialize<TreeViewClientState>(text));
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			return false;
		}

		// Token: 0x0600C688 RID: 50824 RVA: 0x002C4B30 File Offset: 0x002C2D30
		private void LoadClientState(TreeViewClientState clientState)
		{
			if (clientState.LogEntries != null)
			{
				this.LoadLogEntries(clientState);
			}
			if (clientState.CollapsedNodes != null)
			{
				this.LoadCollapsedState(clientState);
			}
			if (clientState.ExpandedNodes != null)
			{
				this.LoadExpandedState(clientState);
			}
			if (clientState.SelectedNodes != null)
			{
				this.LoadSelectedState(clientState);
			}
			if (clientState.CheckedNodes != null)
			{
				this.LoadCheckedState(clientState);
			}
			this.ScrollPosition = clientState.ScrollPosition;
		}

		// Token: 0x0600C689 RID: 50825 RVA: 0x002C4B94 File Offset: 0x002C2D94
		private void LoadCheckedState(TreeViewClientState clientState)
		{
			bool checkChildNodes = this.CheckChildNodes;
			this.CheckChildNodes = false;
			foreach (RadTreeNode radTreeNode in this.GetAllNodes())
			{
				if (radTreeNode.Visible)
				{
					radTreeNode.Checked = false;
				}
			}
			foreach (string hierarchicalIndex in clientState.CheckedNodes)
			{
				RadTreeNode radTreeNode2 = (RadTreeNode)this.FindItemByHierarchicalIndex(hierarchicalIndex);
				if (radTreeNode2 != null)
				{
					radTreeNode2.Checked = true;
				}
			}
			this.CheckChildNodes = checkChildNodes;
		}

		// Token: 0x0600C68A RID: 50826 RVA: 0x002C4C3C File Offset: 0x002C2E3C
		private void LoadSelectedState(TreeViewClientState clientState)
		{
			this.UnselectAllNodes();
			foreach (string hierarchicalIndex in clientState.SelectedNodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)this.FindItemByHierarchicalIndex(hierarchicalIndex);
				if (radTreeNode != null)
				{
					radTreeNode.Selected = true;
				}
			}
		}

		// Token: 0x0600C68B RID: 50827 RVA: 0x002C4C80 File Offset: 0x002C2E80
		private void LoadCollapsedState(TreeViewClientState clientState)
		{
			foreach (string text in clientState.CollapsedNodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)this.FindItemByHierarchicalIndex(text);
				if (radTreeNode != null)
				{
					radTreeNode.Expanded = false;
					this._collapsedNodes.Add(text);
				}
			}
		}

		// Token: 0x0600C68C RID: 50828 RVA: 0x002C4CCC File Offset: 0x002C2ECC
		private void LoadExpandedState(TreeViewClientState clientState)
		{
			foreach (string text in clientState.ExpandedNodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)this.FindItemByHierarchicalIndex(text);
				if (radTreeNode != null)
				{
					radTreeNode.Expanded = true;
					this._expandedNodes.Add(text);
				}
			}
		}

		// Token: 0x0600C68D RID: 50829 RVA: 0x002C4D18 File Offset: 0x002C2F18
		private void LoadLogEntries(TreeViewClientState clientState)
		{
			ClientStateLogPlayer<RadTreeNode> clientStateLogPlayer = new ClientStateLogPlayer<RadTreeNode>(this);
			this._clientChanges = clientStateLogPlayer.Play(clientState.LogEntries);
		}

		// Token: 0x0600C68E RID: 50830 RVA: 0x002C4D40 File Offset: 0x002C2F40
		string ICallbackEventHandler.GetCallbackResult()
		{
			bool flag = false;
			if (this._expandedNode == null)
			{
				return string.Empty;
			}
			if (this._expandedNode.TreeView == null)
			{
				flag = true;
				this.Nodes.Add(this._expandedNode);
			}
			StringWriter stringWriter = new StringWriter();
			for (int i = 0; i < this._expandedNode.Nodes.Count; i++)
			{
				this._expandedNode.Nodes[i].Render(i, new HtmlTextWriter(stringWriter));
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer
			{
				MaxJsonLength = int.MaxValue
			};
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new TreeNodeJavaScriptConverter()
			});
			string arg = javaScriptSerializer.Serialize(this._expandedNode.Nodes.VisibleItems);
			if (flag)
			{
				this._expandedNode.Remove();
			}
			this._expandedNode = null;
			return arg + "_$$_" + stringWriter;
		}

		// Token: 0x0600C68F RID: 50831 RVA: 0x002C4E24 File Offset: 0x002C3024
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.EnsureDataBound();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer
			{
				MaxJsonLength = int.MaxValue
			};
			TreeViewPostBackArguments treeViewPostBackArguments = javaScriptSerializer.Deserialize<TreeViewPostBackArguments>(eventArgument);
			if (treeViewPostBackArguments.ClientState != null)
			{
				this.LoadClientState(treeViewPostBackArguments.ClientState);
			}
			this._expandedNode = (RadTreeNode)this.FindItemByHierarchicalIndex(treeViewPostBackArguments.Index);
			if (this._expandedNode == null)
			{
				this._expandedNode = new RadTreeNode();
				this._expandedNode.LoadFromDictionary(treeViewPostBackArguments.Data);
			}
			this._expandedNode.Expanded = true;
			RadTreeView.ExcludeNodeFromLogging(this._expandedNode);
			this.OnNodeExpand(new RadTreeNodeEventArgs(this._expandedNode));
		}

		// Token: 0x0600C690 RID: 50832 RVA: 0x002C4ECC File Offset: 0x002C30CC
		private static void ExcludeNodeFromLogging(RadTreeNode node)
		{
			foreach (object obj in node.Nodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)obj;
				radTreeNode.SkipLogging = true;
				RadTreeView.ExcludeNodeFromLogging(radTreeNode);
			}
		}

		// Token: 0x0600C691 RID: 50833 RVA: 0x002C4F2C File Offset: 0x002C312C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			((IStateManager)this.ContextMenus).LoadViewState(array[1]);
		}

		// Token: 0x0600C692 RID: 50834 RVA: 0x002C4F58 File Offset: 0x002C3158
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.ContextMenus).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600C693 RID: 50835 RVA: 0x002C4F90 File Offset: 0x002C3190
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ContextMenus).TrackViewState();
		}

		// Token: 0x1700401C RID: 16412
		// (get) Token: 0x0600C694 RID: 50836 RVA: 0x002C4FAC File Offset: 0x002C31AC
		// (set) Token: 0x0600C695 RID: 50837 RVA: 0x002C4FE8 File Offset: 0x002C31E8
		[SimplePersistenceSetting]
		internal List<string> ExpandedIndices
		{
			get
			{
				return this.GetNodesHierarchicalIndicesByCriteria((RadTreeNode x) => x.Expanded);
			}
			set
			{
				IList<RadTreeNode> nodesByCriteria = this.GetNodesByCriteria((RadTreeNode x) => x.Expanded, false);
				foreach (RadTreeNode radTreeNode in nodesByCriteria)
				{
					radTreeNode.Expanded = false;
				}
				foreach (string hierarchicalIndex in value)
				{
					RadTreeNode radTreeNode2 = this.FindNodeByHierarchicalIndex(hierarchicalIndex);
					if (radTreeNode2 != null)
					{
						radTreeNode2.Expanded = true;
					}
				}
			}
		}

		// Token: 0x1700401D RID: 16413
		// (get) Token: 0x0600C696 RID: 50838 RVA: 0x002C50AC File Offset: 0x002C32AC
		// (set) Token: 0x0600C697 RID: 50839 RVA: 0x002C50E8 File Offset: 0x002C32E8
		[SimplePersistenceSetting]
		internal List<string> CheckedIndices
		{
			get
			{
				return this.GetNodesHierarchicalIndicesByCriteria((RadTreeNode x) => x.Checked);
			}
			set
			{
				IList<RadTreeNode> nodesByCriteria = this.GetNodesByCriteria((RadTreeNode x) => x.Checked, false);
				foreach (RadTreeNode radTreeNode in nodesByCriteria)
				{
					radTreeNode.Checked = false;
				}
				foreach (string hierarchicalIndex in value)
				{
					RadTreeNode radTreeNode2 = this.FindNodeByHierarchicalIndex(hierarchicalIndex);
					if (radTreeNode2 != null)
					{
						radTreeNode2.Checked = true;
					}
				}
			}
		}

		// Token: 0x1700401E RID: 16414
		// (get) Token: 0x0600C698 RID: 50840 RVA: 0x002C51AC File Offset: 0x002C33AC
		// (set) Token: 0x0600C699 RID: 50841 RVA: 0x002C51E8 File Offset: 0x002C33E8
		[SimplePersistenceSetting]
		internal List<string> SelectedIndices
		{
			get
			{
				return this.GetNodesHierarchicalIndicesByCriteria((RadTreeNode x) => x.Selected);
			}
			set
			{
				IList<RadTreeNode> nodesByCriteria = this.GetNodesByCriteria((RadTreeNode x) => x.Selected, false);
				foreach (RadTreeNode radTreeNode in nodesByCriteria)
				{
					radTreeNode.Selected = false;
				}
				foreach (string hierarchicalIndex in value)
				{
					RadTreeNode radTreeNode2 = this.FindNodeByHierarchicalIndex(hierarchicalIndex);
					if (radTreeNode2 != null)
					{
						radTreeNode2.Selected = true;
					}
				}
			}
		}

		// Token: 0x0600C69A RID: 50842 RVA: 0x002C52A4 File Offset: 0x002C34A4
		private List<string> GetNodesHierarchicalIndicesByCriteria(RadTreeView.TreeViewNodeCriteria criteria)
		{
			List<string> list = new List<string>();
			IList<RadTreeNode> nodesByCriteria = this.GetNodesByCriteria(criteria, false);
			for (int i = 0; i < nodesByCriteria.Count; i++)
			{
				list.Add(nodesByCriteria[i].GetHierarchicalIndex());
			}
			return list;
		}

		// Token: 0x0600C69B RID: 50843 RVA: 0x002C52E4 File Offset: 0x002C34E4
		internal IList<RadTreeNode> GetNodesByCriteria(RadTreeView.TreeViewNodeCriteria criteria, bool returnOnlyFirstMatch = false)
		{
			IList<RadTreeNode> allNodes = this.GetAllNodes();
			List<RadTreeNode> list = new List<RadTreeNode>();
			foreach (RadTreeNode radTreeNode in allNodes)
			{
				if (criteria(radTreeNode))
				{
					list.Add(radTreeNode);
					if (returnOnlyFirstMatch)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x0600C69C RID: 50844 RVA: 0x002C5348 File Offset: 0x002C3548
		internal RadTreeNode FindNodeByHierarchicalIndex(string hierarchicalIndex)
		{
			return this.FindItemByHierarchicalIndex(hierarchicalIndex) as RadTreeNode;
		}

		// Token: 0x0600C69D RID: 50845 RVA: 0x002C5364 File Offset: 0x002C3564
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "allowNodeEditing", this.AllowNodeEditing, false);
			base.DescribeProperty<bool>(descriptor, "_checkBoxes", this.CheckBoxes, false);
			base.DescribeProperty<bool>(descriptor, "_checkChildNodes", this.CheckChildNodes, false);
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "enableDragAndDrop", this.EnableDragAndDrop, false);
			base.DescribeProperty<bool>(descriptor, "enableDragAndDropBetweenNodes", this.EnableDragAndDropBetweenNodes, false);
			base.DescribeProperty<bool>(descriptor, "_enableNodeTextHtmlEncoding", this.EnableNodeTextHtmlEncoding, false);
			base.DescribeProperty<string>(descriptor, "loadingMessage", this.LoadingMessage, "Loading ...");
			base.DescribeProperty<TreeViewLoadingStatusPosition>(descriptor, "loadingStatusPosition", this.LoadingStatusPosition, TreeViewLoadingStatusPosition.BeforeNodeText);
			base.DescribeProperty<bool>(descriptor, "multipleSelect", this.MultipleSelect, false);
			base.DescribeProperty<bool>(descriptor, "persistLoadOnDemandNodes", this.PersistLoadOnDemandNodes, true);
			base.DescribeProperty<int>(descriptor, "_scrollPosition", this.ScrollPosition, 0);
			base.DescribeProperty<string>(descriptor, "_selectedValue", this.SelectedValue, "");
			base.DescribeProperty<bool>(descriptor, "_showLineImages", this.ShowLineImages, true);
			base.DescribeProperty<bool>(descriptor, "singleExpandPath", this.SingleExpandPath, false);
			if (this.ShouldSerializeTriStateCheckBoxes())
			{
				base.DescribeProperty<bool>(descriptor, "_threeState", this.TriStateCheckBoxes, false);
			}
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600C69E RID: 50846 RVA: 0x002C54CC File Offset: 0x002C36CC
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "contextMenuItemClicked", this.OnClientContextMenuItemClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "contextMenuItemClicking", this.OnClientContextMenuItemClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "contextMenuShowing", this.OnClientContextMenuShowing);
			RadDataBoundControl.DescribeEvent(descriptor, "contextMenuShown", this.OnClientContextMenuShown);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeDoubleClick", this.OnClientDoubleClick);
			RadDataBoundControl.DescribeEvent(descriptor, "keyPressing", this.OnClientKeyPressing);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOut", this.OnClientMouseOut);
			RadDataBoundControl.DescribeEvent(descriptor, "mouseOver", this.OnClientMouseOver);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeAnimationEnd", this.OnClientNodeAnimationEnd);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeChecked", this.OnClientNodeChecked);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeChecking", this.OnClientNodeChecking);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeClicked", this.OnClientNodeClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeClicking", this.OnClientNodeClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeCollapsed", this.OnClientNodeCollapsed);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeCollapsing", this.OnClientNodeCollapsing);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeDataBound", this.OnClientNodeDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "templateDataBound", this.OnClientTemplateDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeDragging", this.OnClientNodeDragging);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeDragStart", this.OnClientNodeDragStart);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeDropped", this.OnClientNodeDropped);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeDropping", this.OnClientNodeDropping);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeEdited", this.OnClientNodeEdited);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeEditing", this.OnClientNodeEditing);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeEditStart", this.OnClientNodeEditStart);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeExpanded", this.OnClientNodeExpanded);
			RadDataBoundControl.DescribeEvent(descriptor, "nodeExpanding", this.OnClientNodeExpanding);
			RadDataBoundControl.DescribeEvent(descriptor, "nodePopulated", this.OnClientNodePopulated);
			RadDataBoundControl.DescribeEvent(descriptor, "nodePopulating", this.OnClientNodePopulating);
			RadDataBoundControl.DescribeEvent(descriptor, "nodePopulationFailed", this.OnClientNodePopulationFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "treePopulated", this.OnClientTreePopulated);
			RadDataBoundControl.DescribeEvent(descriptor, "treePopulating", this.OnClientTreePopulating);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0600C69F RID: 50847 RVA: 0x002C5700 File Offset: 0x002C3900
		public RadTreeView()
		{
			this._webServiceSettings = new NavigationControlWebServiceSettings(this.ViewState);
			this._expandAnimation = new TreeViewAnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new TreeViewAnimationSettings("Collapse", this.ViewState);
		}

		// Token: 0x1700401F RID: 16415
		// (get) Token: 0x0600C6A0 RID: 50848 RVA: 0x002C5771 File Offset: 0x002C3971
		[Browsable(false)]
		public IList<ClientOperation<RadTreeNode>> ClientChanges
		{
			get
			{
				return this._clientChanges;
			}
		}

		// Token: 0x17004020 RID: 16416
		// (get) Token: 0x0600C6A1 RID: 50849 RVA: 0x002C577C File Offset: 0x002C397C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IList<RadTreeNode> CheckedNodes
		{
			get
			{
				List<RadTreeNode> list = new List<RadTreeNode>();
				foreach (RadTreeNode radTreeNode in this.GetAllNodes())
				{
					if (radTreeNode.Checked)
					{
						list.Add(radTreeNode);
					}
				}
				return list;
			}
		}

		// Token: 0x17004021 RID: 16417
		// (get) Token: 0x0600C6A2 RID: 50850 RVA: 0x002C57D8 File Offset: 0x002C39D8
		[ClientPropertyName("_selectedValue")]
		[DefaultValue("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientControlProperty]
		public string SelectedValue
		{
			get
			{
				RadTreeNode selectedNode = this.SelectedNode;
				if (selectedNode != null)
				{
					string text = selectedNode.Value;
					if (string.IsNullOrEmpty(text))
					{
						text = selectedNode.Text;
					}
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x17004022 RID: 16418
		// (get) Token: 0x0600C6A3 RID: 50851 RVA: 0x002C580C File Offset: 0x002C3A0C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IList<RadTreeNode> SelectedNodes
		{
			get
			{
				List<RadTreeNode> list = new List<RadTreeNode>();
				foreach (RadTreeNode radTreeNode in this.GetAllNodes())
				{
					if (radTreeNode.Selected)
					{
						list.Add(radTreeNode);
					}
				}
				return list;
			}
		}

		// Token: 0x17004023 RID: 16419
		// (get) Token: 0x0600C6A4 RID: 50852 RVA: 0x002C5868 File Offset: 0x002C3A68
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadTreeNode SelectedNode
		{
			get
			{
				IList<RadTreeNode> selectedNodes = this.SelectedNodes;
				if (selectedNodes.Count > 0)
				{
					return selectedNodes[0];
				}
				return null;
			}
		}

		// Token: 0x17004024 RID: 16420
		// (get) Token: 0x0600C6A5 RID: 50853 RVA: 0x002C588E File Offset: 0x002C3A8E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.Nodes.Count == 0;
			}
		}

		// Token: 0x17004025 RID: 16421
		// (get) Token: 0x0600C6A6 RID: 50854 RVA: 0x002C589E File Offset: 0x002C3A9E
		// (set) Token: 0x0600C6A7 RID: 50855 RVA: 0x002C58A6 File Offset: 0x002C3AA6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[Bindable(false)]
		[TemplateContainer(typeof(RadTreeNode))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual ITemplate NodeTemplate
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

		// Token: 0x17004026 RID: 16422
		// (get) Token: 0x0600C6A8 RID: 50856 RVA: 0x002C58AF File Offset: 0x002C3AAF
		// (set) Token: 0x0600C6A9 RID: 50857 RVA: 0x002C58CF File Offset: 0x002C3ACF
		[Description("Gets or sets the HTML template of a RadTreeNode when added on the client.")]
		[Category("Client")]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
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

		// Token: 0x17004027 RID: 16423
		// (get) Token: 0x0600C6AA RID: 50858 RVA: 0x002C58E2 File Offset: 0x002C3AE2
		// (set) Token: 0x0600C6AB RID: 50859 RVA: 0x002C58EA File Offset: 0x002C3AEA
		[Bindable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ITemplate LoadingStatusTemplate
		{
			get
			{
				return this._loadingStatusTemplate;
			}
			set
			{
				this._loadingStatusTemplate = value;
			}
		}

		// Token: 0x17004028 RID: 16424
		// (get) Token: 0x0600C6AC RID: 50860 RVA: 0x002C58F3 File Offset: 0x002C3AF3
		// (set) Token: 0x0600C6AD RID: 50861 RVA: 0x002C5913 File Offset: 0x002C3B13
		[DefaultValue("Loading ...")]
		[Category("Behavior")]
		[Description("Specifies ")]
		[ClientControlProperty]
		[ClientPropertyName("loadingMessage")]
		public string LoadingMessage
		{
			get
			{
				return (string)(this.ViewState["LoadingMessage"] ?? "Loading ...");
			}
			set
			{
				this.ViewState["LoadingMessage"] = value;
			}
		}

		// Token: 0x17004029 RID: 16425
		// (get) Token: 0x0600C6AE RID: 50862 RVA: 0x002C5926 File Offset: 0x002C3B26
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public RadTreeNodeCollection Nodes
		{
			get
			{
				return (RadTreeNodeCollection)base.Children;
			}
		}

		// Token: 0x1700402A RID: 16426
		// (get) Token: 0x0600C6AF RID: 50863 RVA: 0x002C5933 File Offset: 0x002C3B33
		// (set) Token: 0x0600C6B0 RID: 50864 RVA: 0x002C5954 File Offset: 0x002C3B54
		[DefaultValue(TreeViewLoadingStatusPosition.BeforeNodeText)]
		[Category("Behavior")]
		[Description("Loading status position.")]
		[ClientPropertyName("loadingStatusPosition")]
		[ClientControlProperty]
		public TreeViewLoadingStatusPosition LoadingStatusPosition
		{
			get
			{
				return (TreeViewLoadingStatusPosition)(this.ViewState["LoadingStatusPosition"] ?? TreeViewLoadingStatusPosition.BeforeNodeText);
			}
			set
			{
				this.ViewState["LoadingStatusPosition"] = value;
			}
		}

		// Token: 0x1700402B RID: 16427
		// (get) Token: 0x0600C6B1 RID: 50865 RVA: 0x002C596C File Offset: 0x002C3B6C
		// (set) Token: 0x0600C6B2 RID: 50866 RVA: 0x002C598D File Offset: 0x002C3B8D
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Allow node editing client-side (pressing F2 on a selected node or clicking an already selected node.")]
		[ClientControlProperty]
		[ClientPropertyName("allowNodeEditing")]
		public bool AllowNodeEditing
		{
			get
			{
				return (bool)(this.ViewState["AllowNodeEditing"] ?? false);
			}
			set
			{
				this.ViewState["AllowNodeEditing"] = value;
			}
		}

		// Token: 0x1700402C RID: 16428
		// (get) Token: 0x0600C6B3 RID: 50867 RVA: 0x002C59A5 File Offset: 0x002C3BA5
		// (set) Token: 0x0600C6B4 RID: 50868 RVA: 0x002C59C6 File Offset: 0x002C3BC6
		[ClientPropertyName("_showLineImages")]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Shows / Hides the treeview control line images.")]
		[DefaultValue(true)]
		public bool ShowLineImages
		{
			get
			{
				return (bool)(this.ViewState["ShowLineImages"] ?? true);
			}
			set
			{
				this.ViewState["ShowLineImages"] = value;
			}
		}

		// Token: 0x1700402D RID: 16429
		// (get) Token: 0x0600C6B5 RID: 50869 RVA: 0x002C59DE File Offset: 0x002C3BDE
		// (set) Token: 0x0600C6B6 RID: 50870 RVA: 0x002C59FF File Offset: 0x002C3BFF
		[ClientPropertyName("singleExpandPath")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true, automatically collapses all nodes that are not part of the last expanded node path.")]
		[ClientControlProperty]
		public bool SingleExpandPath
		{
			get
			{
				return (bool)(this.ViewState["SingleExpandPath"] ?? false);
			}
			set
			{
				this.ViewState["SingleExpandPath"] = value;
			}
		}

		// Token: 0x1700402E RID: 16430
		// (get) Token: 0x0600C6B7 RID: 50871 RVA: 0x002C5A17 File Offset: 0x002C3C17
		// (set) Token: 0x0600C6B8 RID: 50872 RVA: 0x002C5A38 File Offset: 0x002C3C38
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("When set to true displays a checkbox next to each treenode.")]
		[ClientPropertyName("_checkBoxes")]
		[Category("Behavior")]
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

		// Token: 0x1700402F RID: 16431
		// (get) Token: 0x0600C6B9 RID: 50873 RVA: 0x002C5A50 File Offset: 0x002C3C50
		// (set) Token: 0x0600C6BA RID: 50874 RVA: 0x002C5A71 File Offset: 0x002C3C71
		[DefaultValue(false)]
		[Description("Wether checking a node should check its child nodes.")]
		[ClientControlProperty]
		[ClientPropertyName("_checkChildNodes")]
		[Category("Behavior")]
		public bool CheckChildNodes
		{
			get
			{
				return (bool)(this.ViewState["CheckChildNodes"] ?? false);
			}
			set
			{
				this.ViewState["CheckChildNodes"] = value;
			}
		}

		// Token: 0x17004030 RID: 16432
		// (get) Token: 0x0600C6BB RID: 50875 RVA: 0x002C5A89 File Offset: 0x002C3C89
		// (set) Token: 0x0600C6BC RID: 50876 RVA: 0x002C5AAA File Offset: 0x002C3CAA
		[ClientControlProperty]
		[Description("Wether to display three state checkboxes.")]
		[DefaultValue(false)]
		[ClientPropertyName("_threeState")]
		[Category("Behavior")]
		public bool TriStateCheckBoxes
		{
			get
			{
				return (bool)(this.ViewState["TriStateCheckBoxes"] ?? false);
			}
			set
			{
				this.ViewState["TriStateCheckBoxes"] = value;
			}
		}

		// Token: 0x17004031 RID: 16433
		// (get) Token: 0x0600C6BD RID: 50877 RVA: 0x002C5AC2 File Offset: 0x002C3CC2
		// (set) Token: 0x0600C6BE RID: 50878 RVA: 0x002C5AE3 File Offset: 0x002C3CE3
		[ClientPropertyName("multipleSelect")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true the treeview allows multiple node selection (by holding down ctrl key while selecting nodes)")]
		[ClientControlProperty]
		public bool MultipleSelect
		{
			get
			{
				return (bool)(this.ViewState["MultipleSelect"] ?? false);
			}
			set
			{
				this.ViewState["MultipleSelect"] = value;
			}
		}

		// Token: 0x17004032 RID: 16434
		// (get) Token: 0x0600C6BF RID: 50879 RVA: 0x002C5AFB File Offset: 0x002C3CFB
		// (set) Token: 0x0600C6C0 RID: 50880 RVA: 0x002C5B1C File Offset: 0x002C3D1C
		[ClientControlProperty]
		[Description("When set to true enables drag-and-drop functionality")]
		[DefaultValue(false)]
		[ClientPropertyName("enableDragAndDrop")]
		[Category("Behavior")]
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

		// Token: 0x17004033 RID: 16435
		// (get) Token: 0x0600C6C1 RID: 50881 RVA: 0x002C5B34 File Offset: 0x002C3D34
		// (set) Token: 0x0600C6C2 RID: 50882 RVA: 0x002C5B55 File Offset: 0x002C3D55
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("When set to true enables drag-and-drop visual clue (underline) between nodes while draggin")]
		[ClientPropertyName("enableDragAndDropBetweenNodes")]
		[DefaultValue(false)]
		public bool EnableDragAndDropBetweenNodes
		{
			get
			{
				return (bool)(this.ViewState["DragAndDropBetweenNodes"] ?? false);
			}
			set
			{
				this.ViewState["DragAndDropBetweenNodes"] = value;
			}
		}

		// Token: 0x17004034 RID: 16436
		// (get) Token: 0x0600C6C3 RID: 50883 RVA: 0x002C5B6D File Offset: 0x002C3D6D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public RadTreeViewContextMenuCollection ContextMenus
		{
			get
			{
				if (this._contextMenus == null)
				{
					this._contextMenus = new RadTreeViewContextMenuCollection(this);
				}
				return this._contextMenus;
			}
		}

		// Token: 0x17004035 RID: 16437
		// (get) Token: 0x0600C6C4 RID: 50884 RVA: 0x002C5B89 File Offset: 0x002C3D89
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The web service to be used for populating nodes with ExpandMode set to WebService.")]
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

		// Token: 0x17004036 RID: 16438
		// (get) Token: 0x0600C6C5 RID: 50885 RVA: 0x002C5B91 File Offset: 0x002C3D91
		// (set) Token: 0x0600C6C6 RID: 50886 RVA: 0x002C5BB2 File Offset: 0x002C3DB2
		[Category("Behavior")]
		[Description("When set to true, the nodes populated through Load On Demand are persisted on the server")]
		[DefaultValue(true)]
		[ClientControlProperty]
		[ClientPropertyName("persistLoadOnDemandNodes")]
		public bool PersistLoadOnDemandNodes
		{
			get
			{
				return (bool)(this.ViewState["PersistLoadOnDemandNodes"] ?? true);
			}
			set
			{
				this.ViewState["PersistLoadOnDemandNodes"] = value;
			}
		}

		// Token: 0x17004037 RID: 16439
		// (get) Token: 0x0600C6C7 RID: 50887 RVA: 0x002C5BCA File Offset: 0x002C3DCA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The animation played when a node is opened")]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x17004038 RID: 16440
		// (get) Token: 0x0600C6C8 RID: 50888 RVA: 0x002C5BD2 File Offset: 0x002C3DD2
		[Description("The animation played when a node is closed")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17004039 RID: 16441
		// (get) Token: 0x0600C6C9 RID: 50889 RVA: 0x002C5BDA File Offset: 0x002C3DDA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Data")]
		public RadTreeNodeBindingCollection DataBindings
		{
			get
			{
				return (RadTreeNodeBindingCollection)base.NavigationItemBindings;
			}
		}

		// Token: 0x1700403A RID: 16442
		// (get) Token: 0x0600C6CA RID: 50890 RVA: 0x002C5BE7 File Offset: 0x002C3DE7
		// (set) Token: 0x0600C6CB RID: 50891 RVA: 0x002C5BEF File Offset: 0x002C3DEF
		public override int MaxDataBindDepth
		{
			get
			{
				return base.MaxDataBindDepth;
			}
			set
			{
				base.MaxDataBindDepth = value;
			}
		}

		// Token: 0x1700403B RID: 16443
		// (get) Token: 0x0600C6CC RID: 50892 RVA: 0x002C5BF8 File Offset: 0x002C3DF8
		// (set) Token: 0x0600C6CD RID: 50893 RVA: 0x002C5C00 File Offset: 0x002C3E00
		[UrlProperty("*.aspx")]
		[Category("Behavior")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
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

		// Token: 0x1700403C RID: 16444
		// (get) Token: 0x0600C6CE RID: 50894 RVA: 0x002C5C09 File Offset: 0x002C3E09
		[ClientPropertyName("_uniqueId")]
		[ClientControlProperty]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x1700403D RID: 16445
		// (get) Token: 0x0600C6CF RID: 50895 RVA: 0x002C5C11 File Offset: 0x002C3E11
		// (set) Token: 0x0600C6D0 RID: 50896 RVA: 0x002C5C32 File Offset: 0x002C3E32
		[ClientControlProperty]
		[Description("When set to true enables support for WAI-ARIA")]
		[DefaultValue(false)]
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

		// Token: 0x1700403E RID: 16446
		// (get) Token: 0x0600C6D1 RID: 50897 RVA: 0x002C5C4A File Offset: 0x002C3E4A
		// (set) Token: 0x0600C6D2 RID: 50898 RVA: 0x002C5C6B File Offset: 0x002C3E6B
		[Description("Wether to Html encode the text of nodes.")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("_enableNodeTextHtmlEncoding")]
		[Category("Behavior")]
		public bool EnableNodeTextHtmlEncoding
		{
			get
			{
				return (bool)(this.ViewState["EnableNodeTextHtmlEncoding"] ?? false);
			}
			set
			{
				this.ViewState["EnableNodeTextHtmlEncoding"] = value;
			}
		}

		// Token: 0x1700403F RID: 16447
		// (get) Token: 0x0600C6D3 RID: 50899 RVA: 0x002C5C83 File Offset: 0x002C3E83
		// (set) Token: 0x0600C6D4 RID: 50900 RVA: 0x002C5CA3 File Offset: 0x002C3EA3
		[ClientPropertyName("nodeAnimationEnd")]
		[Description("The name of the JavaScript function called when a node's expand/collapse animation finishes")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientNodeAnimationEnd
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeAnimationEnd"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeAnimationEnd"] = value;
			}
		}

		// Token: 0x17004040 RID: 16448
		// (get) Token: 0x0600C6D5 RID: 50901 RVA: 0x002C5CB6 File Offset: 0x002C3EB6
		// (set) Token: 0x0600C6D6 RID: 50902 RVA: 0x002C5CD6 File Offset: 0x002C3ED6
		[ClientControlEvent]
		[Description("The name of the JavaScript function called when a node starts being edited")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("nodeEditStart")]
		public string OnClientNodeEditStart
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeEditStart"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeEditStart"] = value;
			}
		}

		// Token: 0x17004041 RID: 16449
		// (get) Token: 0x0600C6D7 RID: 50903 RVA: 0x002C5CE9 File Offset: 0x002C3EE9
		// (set) Token: 0x0600C6D8 RID: 50904 RVA: 0x002C5D09 File Offset: 0x002C3F09
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when the client template for a node is evaluated")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("templateDataBound")]
		[Category("Client-side events")]
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

		// Token: 0x17004042 RID: 16450
		// (get) Token: 0x0600C6D9 RID: 50905 RVA: 0x002C5D1C File Offset: 0x002C3F1C
		// (set) Token: 0x0600C6DA RID: 50906 RVA: 0x002C5D3C File Offset: 0x002C3F3C
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when a node is databound during load on demand")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("nodeDataBound")]
		public string OnClientNodeDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeDataBound"] = value;
			}
		}

		// Token: 0x17004043 RID: 16451
		// (get) Token: 0x0600C6DB RID: 50907 RVA: 0x002C5D4F File Offset: 0x002C3F4F
		// (set) Token: 0x0600C6DC RID: 50908 RVA: 0x002C5D6F File Offset: 0x002C3F6F
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when the control is fully initialized on the client side.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x17004044 RID: 16452
		// (get) Token: 0x0600C6DD RID: 50909 RVA: 0x002C5D82 File Offset: 0x002C3F82
		// (set) Token: 0x0600C6DE RID: 50910 RVA: 0x002C5DA2 File Offset: 0x002C3FA2
		[Category("Client-side events")]
		[ClientPropertyName("nodeClicking")]
		[Description("The name of the JavaScript function that will be called upon click on a treenode.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
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

		// Token: 0x17004045 RID: 16453
		// (get) Token: 0x0600C6DF RID: 50911 RVA: 0x002C5DB5 File Offset: 0x002C3FB5
		// (set) Token: 0x0600C6E0 RID: 50912 RVA: 0x002C5DD5 File Offset: 0x002C3FD5
		[ClientPropertyName("nodeClicked")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called after click on a treenode. Used for AJAX/callback hooks.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x17004046 RID: 16454
		// (get) Token: 0x0600C6E1 RID: 50913 RVA: 0x002C5DE8 File Offset: 0x002C3FE8
		// (set) Token: 0x0600C6E2 RID: 50914 RVA: 0x002C5E08 File Offset: 0x002C4008
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the JavaScript function that will be called when the user highlights a treenode.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
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

		// Token: 0x17004047 RID: 16455
		// (get) Token: 0x0600C6E3 RID: 50915 RVA: 0x002C5E1B File Offset: 0x002C401B
		// (set) Token: 0x0600C6E4 RID: 50916 RVA: 0x002C5E3B File Offset: 0x002C403B
		[DefaultValue("")]
		[Description("The name of the JavaScript function that will be called when the user double clicks on a node.")]
		[ClientPropertyName("nodeDoubleClick")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientDoubleClick
		{
			get
			{
				return (string)(this.ViewState["OnClientDoubleClick"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDoubleClick"] = value;
			}
		}

		// Token: 0x17004048 RID: 16456
		// (get) Token: 0x0600C6E5 RID: 50917 RVA: 0x002C5E4E File Offset: 0x002C404E
		// (set) Token: 0x0600C6E6 RID: 50918 RVA: 0x002C5E6E File Offset: 0x002C406E
		[Category("Client-side events")]
		[ClientControlEvent]
		[Description("The name of the JavaScript function that will be called when the mouse hovers away from the TreeView.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("mouseOut")]
		[DefaultValue("")]
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

		// Token: 0x17004049 RID: 16457
		// (get) Token: 0x0600C6E7 RID: 50919 RVA: 0x002C5E81 File Offset: 0x002C4081
		// (set) Token: 0x0600C6E8 RID: 50920 RVA: 0x002C5EA1 File Offset: 0x002C40A1
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("nodeEditing")]
		[Description("The name of the JavaScript function that will be called before the user edits a node.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientNodeEditing
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeEditing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeEditing"] = value;
			}
		}

		// Token: 0x1700404A RID: 16458
		// (get) Token: 0x0600C6E9 RID: 50921 RVA: 0x002C5EB4 File Offset: 0x002C40B4
		// (set) Token: 0x0600C6EA RID: 50922 RVA: 0x002C5ED4 File Offset: 0x002C40D4
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the JavaScript function that will be called after the user edits a node.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("nodeEdited")]
		public string OnClientNodeEdited
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeEdited"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeEdited"] = value;
			}
		}

		// Token: 0x1700404B RID: 16459
		// (get) Token: 0x0600C6EB RID: 50923 RVA: 0x002C5EE7 File Offset: 0x002C40E7
		// (set) Token: 0x0600C6EC RID: 50924 RVA: 0x002C5F07 File Offset: 0x002C4107
		[Description("The name of the JavaScript function that will be called when the user expands a treenode.")]
		[ClientPropertyName("nodeExpanding")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[ClientControlEvent]
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

		// Token: 0x1700404C RID: 16460
		// (get) Token: 0x0600C6ED RID: 50925 RVA: 0x002C5F1A File Offset: 0x002C411A
		// (set) Token: 0x0600C6EE RID: 50926 RVA: 0x002C5F3A File Offset: 0x002C413A
		[ClientPropertyName("nodeExpanded")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called after the user expands a treenode.")]
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

		// Token: 0x1700404D RID: 16461
		// (get) Token: 0x0600C6EF RID: 50927 RVA: 0x002C5F4D File Offset: 0x002C414D
		// (set) Token: 0x0600C6F0 RID: 50928 RVA: 0x002C5F6D File Offset: 0x002C416D
		[Description("The name of the JavaScript function that will be called when the user collapses a treenode.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("nodeCollapsing")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x1700404E RID: 16462
		// (get) Token: 0x0600C6F1 RID: 50929 RVA: 0x002C5F80 File Offset: 0x002C4180
		// (set) Token: 0x0600C6F2 RID: 50930 RVA: 0x002C5FA0 File Offset: 0x002C41A0
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called after the user collapses a treenode.")]
		[ClientPropertyName("nodeCollapsed")]
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

		// Token: 0x1700404F RID: 16463
		// (get) Token: 0x0600C6F3 RID: 50931 RVA: 0x002C5FB3 File Offset: 0x002C41B3
		// (set) Token: 0x0600C6F4 RID: 50932 RVA: 0x002C5FD3 File Offset: 0x002C41D3
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the JavaScript function that will be called when the user drops a node onto another node.")]
		[ClientPropertyName("nodeDropping")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientNodeDropping
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeDropping"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeDropping"] = value;
			}
		}

		// Token: 0x17004050 RID: 16464
		// (get) Token: 0x0600C6F5 RID: 50933 RVA: 0x002C5FE6 File Offset: 0x002C41E6
		// (set) Token: 0x0600C6F6 RID: 50934 RVA: 0x002C6006 File Offset: 0x002C4206
		[ClientPropertyName("nodeDropped")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called after the user drops a node onto another node.")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientNodeDropped
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeDropped"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeDropped"] = value;
			}
		}

		// Token: 0x17004051 RID: 16465
		// (get) Token: 0x0600C6F7 RID: 50935 RVA: 0x002C6019 File Offset: 0x002C4219
		// (set) Token: 0x0600C6F8 RID: 50936 RVA: 0x002C6039 File Offset: 0x002C4239
		[Description("The name of the JavaScript function that will be called when the user checks a treenode.")]
		[ClientPropertyName("nodeChecking")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientNodeChecking
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeChecking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeChecking"] = value;
			}
		}

		// Token: 0x17004052 RID: 16466
		// (get) Token: 0x0600C6F9 RID: 50937 RVA: 0x002C604C File Offset: 0x002C424C
		// (set) Token: 0x0600C6FA RID: 50938 RVA: 0x002C606C File Offset: 0x002C426C
		[ClientPropertyName("nodeChecked")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the JavaScript function that will be called after the user checks (checkbox) a treenode.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientNodeChecked
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeChecked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeChecked"] = value;
			}
		}

		// Token: 0x17004053 RID: 16467
		// (get) Token: 0x0600C6FB RID: 50939 RVA: 0x002C607F File Offset: 0x002C427F
		// (set) Token: 0x0600C6FC RID: 50940 RVA: 0x002C609F File Offset: 0x002C429F
		[DefaultValue("")]
		[ClientPropertyName("nodeDragStart")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called before the user starts dragging a node.")]
		[ClientControlEvent]
		public string OnClientNodeDragStart
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeDragStart"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeDragStart"] = value;
			}
		}

		// Token: 0x17004054 RID: 16468
		// (get) Token: 0x0600C6FD RID: 50941 RVA: 0x002C60B2 File Offset: 0x002C42B2
		// (set) Token: 0x0600C6FE RID: 50942 RVA: 0x002C60D2 File Offset: 0x002C42D2
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called when the user moves the mouse while dragging a node.")]
		[ClientPropertyName("nodeDragging")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientNodeDragging
		{
			get
			{
				return (string)(this.ViewState["OnClientNodeDragging"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodeDragging"] = value;
			}
		}

		// Token: 0x17004055 RID: 16469
		// (get) Token: 0x0600C6FF RID: 50943 RVA: 0x002C60E5 File Offset: 0x002C42E5
		// (set) Token: 0x0600C700 RID: 50944 RVA: 0x002C6105 File Offset: 0x002C4305
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("contextMenuItemClicking")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called when the user clicks on a context menu item.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientContextMenuItemClicking
		{
			get
			{
				return (string)(this.ViewState["OnClientContextMenuItemClicking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientContextMenuItemClicking"] = value;
			}
		}

		// Token: 0x17004056 RID: 16470
		// (get) Token: 0x0600C701 RID: 50945 RVA: 0x002C6118 File Offset: 0x002C4318
		// (set) Token: 0x0600C702 RID: 50946 RVA: 0x002C6138 File Offset: 0x002C4338
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("contextMenuItemClicked")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called after the user clicks on a context menu item.")]
		[DefaultValue("")]
		public string OnClientContextMenuItemClicked
		{
			get
			{
				return (string)(this.ViewState["OnClientContextMenuItemClicked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientContextMenuItemClicked"] = value;
			}
		}

		// Token: 0x17004057 RID: 16471
		// (get) Token: 0x0600C703 RID: 50947 RVA: 0x002C614B File Offset: 0x002C434B
		// (set) Token: 0x0600C704 RID: 50948 RVA: 0x002C616B File Offset: 0x002C436B
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called when a context menu is to be displayed.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("contextMenuShowing")]
		public string OnClientContextMenuShowing
		{
			get
			{
				return (string)(this.ViewState["OnClientContextMenuShowing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientContextMenuShowing"] = value;
			}
		}

		// Token: 0x17004058 RID: 16472
		// (get) Token: 0x0600C705 RID: 50949 RVA: 0x002C617E File Offset: 0x002C437E
		// (set) Token: 0x0600C706 RID: 50950 RVA: 0x002C619E File Offset: 0x002C439E
		[Category("Client-side events")]
		[Description("The name of the JavaScript function that will be called after context menu is displayed.")]
		[ClientPropertyName("contextMenuShown")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientContextMenuShown
		{
			get
			{
				return (string)(this.ViewState["OnClientContextMenuShown"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientContextMenuShown"] = value;
			}
		}

		// Token: 0x17004059 RID: 16473
		// (get) Token: 0x0600C707 RID: 50951 RVA: 0x002C61B1 File Offset: 0x002C43B1
		// (set) Token: 0x0600C708 RID: 50952 RVA: 0x002C61D1 File Offset: 0x002C43D1
		[DefaultValue("")]
		[ClientPropertyName("treePopulating")]
		[Description("The name of the javascript function called before RadTreeView is populated. The event fires only in odata binding scenarios")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientTreePopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientTreePopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTreePopulating"] = value;
			}
		}

		// Token: 0x1700405A RID: 16474
		// (get) Token: 0x0600C709 RID: 50953 RVA: 0x002C61E4 File Offset: 0x002C43E4
		// (set) Token: 0x0600C70A RID: 50954 RVA: 0x002C6204 File Offset: 0x002C4404
		[Description("The name of the javascript function called before RadTreeView is populated. The event fires only in odata binding scenarios")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("treePopulated")]
		[Category("Client-side events")]
		public string OnClientTreePopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientTreePopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTreePopulated"] = value;
			}
		}

		// Token: 0x1700405B RID: 16475
		// (get) Token: 0x0600C70B RID: 50955 RVA: 0x002C6217 File Offset: 0x002C4417
		// (set) Token: 0x0600C70C RID: 50956 RVA: 0x002C6237 File Offset: 0x002C4437
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("nodePopulating")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the javascript function called before the children of a tree node are about to be populated.")]
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

		// Token: 0x1700405C RID: 16476
		// (get) Token: 0x0600C70D RID: 50957 RVA: 0x002C624A File Offset: 0x002C444A
		// (set) Token: 0x0600C70E RID: 50958 RVA: 0x002C626A File Offset: 0x002C446A
		[Category("Client-side events")]
		[ClientControlEvent]
		[Description("The name of the javascript function called after the children of a tree node were populated.")]
		[DefaultValue("")]
		[ClientPropertyName("nodePopulated")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x1700405D RID: 16477
		// (get) Token: 0x0600C70F RID: 50959 RVA: 0x002C627D File Offset: 0x002C447D
		// (set) Token: 0x0600C710 RID: 50960 RVA: 0x002C629D File Offset: 0x002C449D
		[ClientControlEvent]
		[ClientPropertyName("nodePopulationFailed")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the operation to populate the children of a tree node has failed.")]
		[DefaultValue("")]
		public string OnClientNodePopulationFailed
		{
			get
			{
				return (string)(this.ViewState["OnClientNodePopulationFailed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNodePopulationFailed"] = value;
			}
		}

		// Token: 0x1700405E RID: 16478
		// (get) Token: 0x0600C711 RID: 50961 RVA: 0x002C62B0 File Offset: 0x002C44B0
		// (set) Token: 0x0600C712 RID: 50962 RVA: 0x002C62D0 File Offset: 0x002C44D0
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientPropertyName("keyPressing")]
		[Description("The name of the JavaScript function that will be called when a key is pressed.")]
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

		// Token: 0x0600C713 RID: 50963 RVA: 0x002C62E3 File Offset: 0x002C44E3
		public IList<RadTreeNode> GetAllNodes()
		{
			return base.GetAllChildren<RadTreeNode>();
		}

		// Token: 0x0600C714 RID: 50964 RVA: 0x002C62EB File Offset: 0x002C44EB
		public RadTreeNode FindNodeByText(string text)
		{
			return base.FindChildByText<RadTreeNode>(text);
		}

		// Token: 0x0600C715 RID: 50965 RVA: 0x002C62F4 File Offset: 0x002C44F4
		public RadTreeNode FindNodeByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadTreeNode>(text, ignoreCase);
		}

		// Token: 0x0600C716 RID: 50966 RVA: 0x002C62FE File Offset: 0x002C44FE
		public RadTreeNode FindNodeByValue(string value)
		{
			return this.FindChildByValue<RadTreeNode>(value);
		}

		// Token: 0x0600C717 RID: 50967 RVA: 0x002C6307 File Offset: 0x002C4507
		public RadTreeNode FindNodeByValue(string value, bool ignoreCase)
		{
			return this.FindChildByValue<RadTreeNode>(value, ignoreCase);
		}

		// Token: 0x0600C718 RID: 50968 RVA: 0x002C6311 File Offset: 0x002C4511
		public RadTreeNode FindNodeByUrl(string url)
		{
			return base.FindChildByUrl<RadTreeNode>(url);
		}

		// Token: 0x0600C719 RID: 50969 RVA: 0x002C631A File Offset: 0x002C451A
		public RadTreeNode FindNode(Predicate<RadTreeNode> match)
		{
			return base.FindChild<RadTreeNode>(match);
		}

		// Token: 0x0600C71A RID: 50970 RVA: 0x002C6323 File Offset: 0x002C4523
		public void LoadXmlString(string xml)
		{
			base.LoadXml(xml);
		}

		// Token: 0x0600C71B RID: 50971 RVA: 0x002C632C File Offset: 0x002C452C
		public override void LoadContentFile(string fileName)
		{
			base.LoadContentFile(fileName);
		}

		// Token: 0x0600C71C RID: 50972 RVA: 0x002C6335 File Offset: 0x002C4535
		public RadTreeNode FindNodeByAttribute(string attributeName, string attributeValue)
		{
			return base.FindChildByAttribute<RadTreeNode>(attributeName, attributeValue);
		}

		// Token: 0x0600C71D RID: 50973 RVA: 0x002C633F File Offset: 0x002C453F
		[Obsolete("This method should no longer be used, please use UnselectAllNodes instead")]
		public void ClearSelectedNodes()
		{
			this.UnselectAllNodes();
		}

		// Token: 0x0600C71E RID: 50974 RVA: 0x002C6348 File Offset: 0x002C4548
		public void UnselectAllNodes()
		{
			foreach (RadTreeNode radTreeNode in this.GetAllNodes())
			{
				radTreeNode.Selected = false;
			}
		}

		// Token: 0x0600C71F RID: 50975 RVA: 0x002C6398 File Offset: 0x002C4598
		[Obsolete("This method should no longer be used, please use UncheckAllNodes instead")]
		public void ClearCheckedNodes()
		{
			this.UncheckAllNodes();
		}

		// Token: 0x0600C720 RID: 50976 RVA: 0x002C63A0 File Offset: 0x002C45A0
		public void UncheckAllNodes()
		{
			foreach (RadTreeNode radTreeNode in this.GetAllNodes())
			{
				radTreeNode.Checked = false;
			}
		}

		// Token: 0x0600C721 RID: 50977 RVA: 0x002C63F0 File Offset: 0x002C45F0
		public void CheckAllNodes()
		{
			foreach (object obj in this.Nodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)obj;
				if (radTreeNode.Checkable)
				{
					radTreeNode.Checked = true;
				}
				radTreeNode.CheckChildNodes();
			}
		}

		// Token: 0x0600C722 RID: 50978 RVA: 0x002C6458 File Offset: 0x002C4658
		public void ExpandAllNodes()
		{
			foreach (RadTreeNode radTreeNode in this.GetAllNodes())
			{
				radTreeNode.Expanded = true;
			}
		}

		// Token: 0x0600C723 RID: 50979 RVA: 0x002C64A8 File Offset: 0x002C46A8
		public void CollapseAllNodes()
		{
			foreach (RadTreeNode radTreeNode in this.GetAllNodes())
			{
				radTreeNode.Expanded = false;
			}
		}

		// Token: 0x1400019D RID: 413
		// (add) Token: 0x0600C724 RID: 50980 RVA: 0x002C64F8 File Offset: 0x002C46F8
		// (remove) Token: 0x0600C725 RID: 50981 RVA: 0x002C650B File Offset: 0x002C470B
		public event RadTreeViewEventHandler NodeClick
		{
			add
			{
				base.Events.AddHandler(RadTreeView.NodeClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.NodeClickEvent, value);
			}
		}

		// Token: 0x0600C726 RID: 50982 RVA: 0x002C651E File Offset: 0x002C471E
		protected virtual void OnNodeClick(RadTreeNodeEventArgs e)
		{
			this.RaiseEvent(RadTreeView.NodeClickEvent, e);
		}

		// Token: 0x1400019E RID: 414
		// (add) Token: 0x0600C727 RID: 50983 RVA: 0x002C652C File Offset: 0x002C472C
		// (remove) Token: 0x0600C728 RID: 50984 RVA: 0x002C653F File Offset: 0x002C473F
		public event RadTreeViewEventHandler NodeDataBound
		{
			add
			{
				base.Events.AddHandler(RadTreeView.NodeDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.NodeDataBoundEvent, value);
			}
		}

		// Token: 0x0600C729 RID: 50985 RVA: 0x002C6552 File Offset: 0x002C4752
		protected virtual void OnNodeDataBound(RadTreeNodeEventArgs e)
		{
			this.RaiseEvent(RadTreeView.NodeDataBoundEvent, e);
		}

		// Token: 0x1400019F RID: 415
		// (add) Token: 0x0600C72A RID: 50986 RVA: 0x002C6560 File Offset: 0x002C4760
		// (remove) Token: 0x0600C72B RID: 50987 RVA: 0x002C6573 File Offset: 0x002C4773
		public event RadTreeViewEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadTreeView.TemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.TemplateNeededEvent, value);
			}
		}

		// Token: 0x0600C72C RID: 50988 RVA: 0x002C6586 File Offset: 0x002C4786
		protected virtual void OnTemplateNeeded(RadTreeNodeEventArgs e)
		{
			this.RaiseEvent(RadTreeView.TemplateNeededEvent, e);
		}

		// Token: 0x140001A0 RID: 416
		// (add) Token: 0x0600C72D RID: 50989 RVA: 0x002C6594 File Offset: 0x002C4794
		// (remove) Token: 0x0600C72E RID: 50990 RVA: 0x002C65A7 File Offset: 0x002C47A7
		public event RadTreeViewEventHandler NodeCreated
		{
			add
			{
				base.Events.AddHandler(RadTreeView.NodeCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.NodeCreatedEvent, value);
			}
		}

		// Token: 0x0600C72F RID: 50991 RVA: 0x002C65BA File Offset: 0x002C47BA
		protected virtual void OnNodeCreated(RadTreeNodeEventArgs e)
		{
			this.RaiseEvent(RadTreeView.NodeCreatedEvent, e);
		}

		// Token: 0x140001A1 RID: 417
		// (add) Token: 0x0600C730 RID: 50992 RVA: 0x002C65C8 File Offset: 0x002C47C8
		// (remove) Token: 0x0600C731 RID: 50993 RVA: 0x002C65DB File Offset: 0x002C47DB
		public event RadTreeViewEventHandler NodeExpand
		{
			add
			{
				base.Events.AddHandler(RadTreeView.NodeExpandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.NodeExpandEvent, value);
			}
		}

		// Token: 0x0600C732 RID: 50994 RVA: 0x002C65EE File Offset: 0x002C47EE
		protected virtual void OnNodeExpand(RadTreeNodeEventArgs e)
		{
			this.RaiseEvent(RadTreeView.NodeExpandEvent, e);
		}

		// Token: 0x140001A2 RID: 418
		// (add) Token: 0x0600C733 RID: 50995 RVA: 0x002C65FC File Offset: 0x002C47FC
		// (remove) Token: 0x0600C734 RID: 50996 RVA: 0x002C660F File Offset: 0x002C480F
		public event RadTreeViewEventHandler NodeCollapse
		{
			add
			{
				base.Events.AddHandler(RadTreeView.NodeCollapseEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.NodeCollapseEvent, value);
			}
		}

		// Token: 0x0600C735 RID: 50997 RVA: 0x002C6622 File Offset: 0x002C4822
		protected virtual void OnNodeCollapse(RadTreeNodeEventArgs e)
		{
			this.RaiseEvent(RadTreeView.NodeCollapseEvent, e);
		}

		// Token: 0x140001A3 RID: 419
		// (add) Token: 0x0600C736 RID: 50998 RVA: 0x002C6630 File Offset: 0x002C4830
		// (remove) Token: 0x0600C737 RID: 50999 RVA: 0x002C6643 File Offset: 0x002C4843
		public event RadTreeViewEventHandler NodeCheck
		{
			add
			{
				base.Events.AddHandler(RadTreeView.NodeCheckEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.NodeCheckEvent, value);
			}
		}

		// Token: 0x0600C738 RID: 51000 RVA: 0x002C6656 File Offset: 0x002C4856
		protected virtual void OnNodeCheck(RadTreeNodeEventArgs e)
		{
			this.RaiseEvent(RadTreeView.NodeCheckEvent, e);
		}

		// Token: 0x140001A4 RID: 420
		// (add) Token: 0x0600C739 RID: 51001 RVA: 0x002C6664 File Offset: 0x002C4864
		// (remove) Token: 0x0600C73A RID: 51002 RVA: 0x002C6677 File Offset: 0x002C4877
		public event RadTreeViewDragDropEventHandler NodeDrop
		{
			add
			{
				base.Events.AddHandler(RadTreeView.NodeDropEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.NodeDropEvent, value);
			}
		}

		// Token: 0x0600C73B RID: 51003 RVA: 0x002C668C File Offset: 0x002C488C
		protected virtual void OnNodeDrop(RadTreeNodeDragDropEventArgs e)
		{
			RadTreeViewDragDropEventHandler radTreeViewDragDropEventHandler = (RadTreeViewDragDropEventHandler)base.Events[RadTreeView.NodeDropEvent];
			if (radTreeViewDragDropEventHandler != null)
			{
				radTreeViewDragDropEventHandler(this, e);
			}
		}

		// Token: 0x140001A5 RID: 421
		// (add) Token: 0x0600C73C RID: 51004 RVA: 0x002C66BA File Offset: 0x002C48BA
		// (remove) Token: 0x0600C73D RID: 51005 RVA: 0x002C66CD File Offset: 0x002C48CD
		public event RadTreeViewEditEventHandler NodeEdit
		{
			add
			{
				base.Events.AddHandler(RadTreeView.NodeEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.NodeEditEvent, value);
			}
		}

		// Token: 0x0600C73E RID: 51006 RVA: 0x002C66E0 File Offset: 0x002C48E0
		protected virtual void OnNodeEdit(RadTreeNodeEditEventArgs e)
		{
			RadTreeViewEditEventHandler radTreeViewEditEventHandler = (RadTreeViewEditEventHandler)base.Events[RadTreeView.NodeEditEvent];
			if (radTreeViewEditEventHandler != null)
			{
				radTreeViewEditEventHandler(this, e);
			}
		}

		// Token: 0x140001A6 RID: 422
		// (add) Token: 0x0600C73F RID: 51007 RVA: 0x002C670E File Offset: 0x002C490E
		// (remove) Token: 0x0600C740 RID: 51008 RVA: 0x002C6721 File Offset: 0x002C4921
		[Description("Fired after a menu item is clicked.")]
		public event RadTreeViewContextMenuEventHandler ContextMenuItemClick
		{
			add
			{
				base.Events.AddHandler(RadTreeView.ContextMenuItemClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeView.ContextMenuItemClickEvent, value);
			}
		}

		// Token: 0x0600C741 RID: 51009 RVA: 0x002C6734 File Offset: 0x002C4934
		protected virtual void OnContextMenuItemClick(RadTreeViewContextMenuEventArgs e)
		{
			RadTreeViewContextMenuEventHandler radTreeViewContextMenuEventHandler = (RadTreeViewContextMenuEventHandler)base.Events[RadTreeView.ContextMenuItemClickEvent];
			if (radTreeViewContextMenuEventHandler != null)
			{
				radTreeViewContextMenuEventHandler(this, e);
			}
		}

		// Token: 0x0600C748 RID: 51016 RVA: 0x002C6764 File Offset: 0x002C4964
		// Note: this type is marked as 'beforefieldinit'.
		static RadTreeView()
		{
			RadTreeView.NodeClickEvent = new object();
			RadTreeView.NodeDataBoundEvent = new object();
			RadTreeView.TemplateNeededEvent = new object();
			RadTreeView.NodeCreatedEvent = new object();
			RadTreeView.NodeExpandEvent = new object();
			RadTreeView.NodeCollapseEvent = new object();
			RadTreeView.NodeCheckEvent = new object();
			RadTreeView.NodeDropEvent = new object();
			RadTreeView.NodeEditEvent = new object();
			RadTreeView.ContextMenuItemClickEvent = new object();
		}

		// Token: 0x04003470 RID: 13424
		private readonly List<string> _expandedNodes = new List<string>();

		// Token: 0x04003471 RID: 13425
		private readonly List<string> _collapsedNodes = new List<string>();

		// Token: 0x04003472 RID: 13426
		private RadTreeViewContextMenuCollection _contextMenus;

		// Token: 0x04003473 RID: 13427
		private int _scrollPosition;

		// Token: 0x04003474 RID: 13428
		private AnimationSettings _expandAnimation;

		// Token: 0x04003475 RID: 13429
		private AnimationSettings _collapseAnimation;

		// Token: 0x04003476 RID: 13430
		private NavigationControlWebServiceSettings _webServiceSettings;

		// Token: 0x04003477 RID: 13431
		private RadTreeNode _expandedNode;

		// Token: 0x04003478 RID: 13432
		private IList<ClientOperation<RadTreeNode>> _clientChanges = new List<ClientOperation<RadTreeNode>>();

		// Token: 0x04003479 RID: 13433
		private ITemplate _loadingStatusTemplate;

		// Token: 0x0200129C RID: 4764
		// (Invoke) Token: 0x0600C74A RID: 51018
		internal delegate bool TreeViewNodeCriteria(RadTreeNode node);
	}
}
