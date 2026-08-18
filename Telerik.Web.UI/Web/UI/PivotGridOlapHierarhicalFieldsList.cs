using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI
{
	// Token: 0x02000C43 RID: 3139
	internal class PivotGridOlapHierarhicalFieldsList
	{
		// Token: 0x170026A0 RID: 9888
		// (get) Token: 0x060076BA RID: 30394 RVA: 0x001B8EC8 File Offset: 0x001B70C8
		// (set) Token: 0x060076BB RID: 30395 RVA: 0x001B8ED0 File Offset: 0x001B70D0
		private RadPivotGrid OwnerPivotGrid { get; set; }

		// Token: 0x170026A1 RID: 9889
		// (get) Token: 0x060076BC RID: 30396 RVA: 0x001B8ED9 File Offset: 0x001B70D9
		// (set) Token: 0x060076BD RID: 30397 RVA: 0x001B8EE1 File Offset: 0x001B70E1
		private RadTreeView TreeView { get; set; }

		// Token: 0x170026A2 RID: 9890
		// (get) Token: 0x060076BE RID: 30398 RVA: 0x001B8EEA File Offset: 0x001B70EA
		// (set) Token: 0x060076BF RID: 30399 RVA: 0x001B8F28 File Offset: 0x001B7128
		private List<PivotGridOlapHierarhicalFieldsList.OlapItemInfo> NodesData
		{
			get
			{
				if (HttpRuntime.Cache[this.NodesDataKey] == null)
				{
					HttpRuntime.Cache[this.NodesDataKey] = new List<PivotGridOlapHierarhicalFieldsList.OlapItemInfo>();
				}
				return (List<PivotGridOlapHierarhicalFieldsList.OlapItemInfo>)HttpRuntime.Cache[this.NodesDataKey];
			}
			set
			{
				HttpRuntime.Cache[this.NodesDataKey] = value;
			}
		}

		// Token: 0x170026A3 RID: 9891
		// (get) Token: 0x060076C0 RID: 30400 RVA: 0x001B8F3C File Offset: 0x001B713C
		private string NodesDataKey
		{
			get
			{
				string result = string.Empty;
				if (this.OwnerPivotGrid.Page != null && this.OwnerPivotGrid.Page.Request != null)
				{
					result = string.Format("{0}_{1}_{2}", this.OwnerPivotGrid.Page.Request.Url, "NodesData", this.OwnerPivotGrid.UniqueID);
				}
				else
				{
					result = string.Format("{0}_{1}", "NodesData", this.OwnerPivotGrid.UniqueID);
				}
				return result;
			}
		}

		// Token: 0x060076C1 RID: 30401 RVA: 0x001B8FBC File Offset: 0x001B71BC
		public PivotGridOlapHierarhicalFieldsList(PivotGridConfigurationPanel configurationPanel)
		{
			this.OwnerPivotGrid = configurationPanel.OwnerPivotGrid;
			this.TreeView = configurationPanel.TreeView;
			this.configurationPanel = configurationPanel;
			this.TreeView.NodeDataBound += this.treeView_NodeDataBound;
			this.TreeView.PreRender += this.TreeView_PreRender;
			if (this.OwnerPivotGrid.ConfigurationPanelSettings.EnableOlapTreeViewLoadOnDemand)
			{
				this.TreeView.NodeExpand += this.TreeView_NodeExpand;
			}
			this.TreeView.CheckBoxes = true;
			this.TreeView.Height = 600;
		}

		// Token: 0x060076C2 RID: 30402 RVA: 0x001B90A0 File Offset: 0x001B72A0
		public void Initialize()
		{
			RadTreeView treeView = this.configurationPanel.TreeView;
			this.OwnerPivotGrid = this.configurationPanel.OwnerPivotGrid;
			treeView.EnableDragAndDrop = this.OwnerPivotGrid.ConfigurationPanelSettings.EnableDragDrop;
			this.BindToFieldsInfoData(treeView);
		}

		// Token: 0x060076C3 RID: 30403 RVA: 0x001B9100 File Offset: 0x001B7300
		private void TreeView_NodeExpand(object sender, RadTreeNodeEventArgs e)
		{
			int itemInfoId = int.Parse(e.Node.Attributes["nodeId"]);
			IEnumerable<PivotGridOlapHierarhicalFieldsList.OlapItemInfo> enumerable = from n in this.NodesData
			where n.ParentID == itemInfoId
			select n;
			foreach (PivotGridOlapHierarhicalFieldsList.OlapItemInfo olapItemInfo in enumerable)
			{
				RadTreeNode node = new RadTreeNode(olapItemInfo.Text, olapItemInfo.FieldUniqueName);
				this.PrepareNode(node, olapItemInfo);
				e.Node.Nodes.Add(node);
			}
			this.SetHighligtedState(this.NodesData, enumerable);
		}

		// Token: 0x060076C4 RID: 30404 RVA: 0x001B91CC File Offset: 0x001B73CC
		private void TreeView_PreRender(object sender, EventArgs e)
		{
			foreach (RadTreeNode radTreeNode in this.TreeView.GetAllNodes())
			{
				if (radTreeNode.Checkable)
				{
					string fieldUniqueName = Regex.Replace(radTreeNode.Value, "\\s", string.Empty);
					PivotGridField pivotGridField = this.OwnerPivotGrid.Fields[fieldUniqueName];
					radTreeNode.Checked = (pivotGridField != null && !pivotGridField.IsHidden);
				}
				else if (radTreeNode.Expanded && radTreeNode.ContentCssClass.Contains(this.FolderNodeClassName))
				{
					radTreeNode.ContentCssClass = radTreeNode.ContentCssClass.Replace(this.FolderNodeClassName, this.ExpandedFolderNodeClassName);
				}
				if (string.IsNullOrEmpty(radTreeNode.Attributes[this.CheckedChildNodesAttributeName]))
				{
					radTreeNode.Attributes.Add(this.CheckedChildNodesAttributeName, "0");
				}
				radTreeNode.Attributes.Remove(this.CheckedChildNodesAttributeName);
			}
			if (this.OwnerPivotGrid.ConfigurationPanelSettings.EnableOlapTreeViewLoadOnDemand)
			{
				this.SetHighligtedState(this.NodesData, from i in this.NodesData
				where i.ParentID == 0
				select i);
				return;
			}
			this.SetHighligtedState(this.TreeView.Children);
		}

		// Token: 0x060076C5 RID: 30405 RVA: 0x001B9338 File Offset: 0x001B7538
		private void treeView_NodeDataBound(object sender, RadTreeNodeEventArgs e)
		{
			this.PrepareNode(e.Node, e.Node.DataItem as PivotGridOlapHierarhicalFieldsList.OlapItemInfo);
		}

		// Token: 0x060076C6 RID: 30406 RVA: 0x001B9374 File Offset: 0x001B7574
		private void PrepareNode(RadTreeNode node, PivotGridOlapHierarhicalFieldsList.OlapItemInfo itemInfo)
		{
			if (itemInfo.Role == ContainerNodeRole.Selectable)
			{
				PivotGridField pivotGridField = this.OwnerPivotGrid.Fields[itemInfo.FieldUniqueName];
				node.Checked = (pivotGridField != null && !pivotGridField.IsHidden);
				node.Attributes.Add("field", itemInfo.FieldUniqueName);
				if (!node.Checked)
				{
					node.Attributes.Add("zoneType", ((int)itemInfo.ZoneType).ToString());
				}
			}
			else
			{
				node.AllowDrag = false;
				node.Checkable = false;
			}
			if (this.OwnerPivotGrid.ConfigurationPanelSettings.EnableOlapTreeViewLoadOnDemand && this.NodesData.Exists((PivotGridOlapHierarhicalFieldsList.OlapItemInfo i) => i.ParentID == itemInfo.ID))
			{
				node.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
			}
			node.Attributes.Add("nodeId", itemInfo.ID.ToString());
			node.ContentCssClass = "rpg" + itemInfo.Role.ToString();
		}

		// Token: 0x060076C7 RID: 30407 RVA: 0x001B94A0 File Offset: 0x001B76A0
		private void BindToFieldsInfoData(RadTreeView treeView)
		{
			IDataProvider provider = this.OwnerPivotGrid.provider;
			if (provider == null)
			{
				return;
			}
			IFieldInfoData fieldInfos = provider.FieldInfos;
			if (fieldInfos != null && treeView.Nodes.Count == 0)
			{
				treeView.DataValueField = "FieldUniqueName";
				treeView.DataTextField = "Text";
				treeView.DataFieldID = "ID";
				treeView.DataFieldParentID = "ParentID";
				int num = 1;
				foreach (PivotGridField pivotGridField in this.OwnerPivotGrid.Fields)
				{
					if (pivotGridField.FieldInfoNode != null && !this.fieldCaptionsByUniqueName.ContainsKey(pivotGridField.FieldInfoNode.Name))
					{
						this.fieldCaptionsByUniqueName.Add(pivotGridField.FieldInfoNode.Name, pivotGridField);
					}
				}
				if (this.OwnerPivotGrid.ConfigurationPanelSettings.EnableOlapTreeViewLoadOnDemand)
				{
					treeView.MaxDataBindDepth = 1;
				}
				if (this.OwnerPivotGrid.ConfigurationPanelSettings.EnableOlapTreeViewLoadOnDemand)
				{
					this.NodesData.Clear();
					this.GenerateTreeViewDataSource(fieldInfos.RootFieldInfo, this.NodesData, ref num, 0);
					treeView.DataSource = this.NodesData;
				}
				else
				{
					List<PivotGridOlapHierarhicalFieldsList.OlapItemInfo> list = new List<PivotGridOlapHierarhicalFieldsList.OlapItemInfo>();
					this.GenerateTreeViewDataSource(fieldInfos.RootFieldInfo, list, ref num, 0);
					treeView.DataSource = list;
				}
				treeView.DataBind();
			}
		}

		// Token: 0x060076C8 RID: 30408 RVA: 0x001B9604 File Offset: 0x001B7804
		private void GenerateTreeViewDataSource(ContainerNode node, List<PivotGridOlapHierarhicalFieldsList.OlapItemInfo> result, ref int id, int parentId)
		{
			foreach (ContainerNode containerNode in node.Children)
			{
				PivotGridOlapHierarhicalFieldsList.OlapItemInfo olapItemInfo = new PivotGridOlapHierarhicalFieldsList.OlapItemInfo();
				olapItemInfo.ID = id++;
				olapItemInfo.ParentID = parentId;
				FieldInfoNode fieldInfoNode = containerNode as FieldInfoNode;
				if (fieldInfoNode == null)
				{
					olapItemInfo.FieldUniqueName = Regex.Replace(containerNode.Caption, "\\s", string.Empty);
					olapItemInfo.Text = containerNode.Caption;
				}
				else
				{
					olapItemInfo.ZoneType = this.GetZoneTypeFromFieldRoles(fieldInfoNode.FieldInfo.PreferredRole);
					if (this.fieldCaptionsByUniqueName.ContainsKey(fieldInfoNode.FieldInfo.Name))
					{
						PivotGridField pivotGridField = this.fieldCaptionsByUniqueName[fieldInfoNode.FieldInfo.Name];
						olapItemInfo.FieldUniqueName = pivotGridField.UniqueName;
						if (string.IsNullOrEmpty(pivotGridField.Caption))
						{
							olapItemInfo.Text = pivotGridField.DataField;
						}
						else
						{
							olapItemInfo.Text = pivotGridField.Caption;
						}
						this.ownerField = pivotGridField;
					}
					else
					{
						olapItemInfo.FieldUniqueName = Regex.Replace(fieldInfoNode.FieldInfo.Name, "\\s", string.Empty);
						olapItemInfo.Text = fieldInfoNode.Caption;
						if (this.ownerField != null)
						{
							OlapHierarchyFieldInfo olapHierarchyFieldInfo = fieldInfoNode.FieldInfo as OlapHierarchyFieldInfo;
							if (olapHierarchyFieldInfo != null)
							{
								if (olapHierarchyFieldInfo.PreferredRole == FieldRoles.None)
								{
									this.ownerField.FlatChildOlapInfoNames.Add(olapHierarchyFieldInfo.Name);
								}
								else
								{
									this.ownerField = null;
								}
							}
						}
					}
				}
				olapItemInfo.Role = containerNode.Role;
				olapItemInfo.Text = HttpUtility.HtmlEncode(olapItemInfo.Text);
				result.Add(olapItemInfo);
				this.GenerateTreeViewDataSource(containerNode, result, ref id, olapItemInfo.ID);
			}
		}

		// Token: 0x060076C9 RID: 30409 RVA: 0x001B97DC File Offset: 0x001B79DC
		private PivotGridFieldZoneType GetZoneTypeFromFieldRoles(FieldRoles roles)
		{
			if (roles == FieldRoles.All || roles == FieldRoles.Row || roles == FieldRoles.None)
			{
				return PivotGridFieldZoneType.Row;
			}
			if (roles == FieldRoles.Value)
			{
				return PivotGridFieldZoneType.Aggregate;
			}
			if (roles == FieldRoles.Column)
			{
				return PivotGridFieldZoneType.Column;
			}
			if (roles == FieldRoles.Filter)
			{
				return PivotGridFieldZoneType.Filter;
			}
			return PivotGridFieldZoneType.Row;
		}

		// Token: 0x060076CA RID: 30410 RVA: 0x001B9800 File Offset: 0x001B7A00
		private int SetHighligtedState(ControlItemCollection nodes)
		{
			int num = 0;
			foreach (object obj in nodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)obj;
				if (string.IsNullOrEmpty(radTreeNode.Attributes[this.CheckedChildNodesAttributeName]))
				{
					radTreeNode.Attributes.Add(this.CheckedChildNodesAttributeName, "0");
				}
				int num2 = int.Parse(radTreeNode.Attributes[this.CheckedChildNodesAttributeName]);
				if (radTreeNode.Checked)
				{
					num2++;
				}
				int num3 = this.SetHighligtedState(radTreeNode.Children);
				num += num3 + num2;
				radTreeNode.Attributes[this.CheckedChildNodesAttributeName] = num3.ToString();
				if (num3 > 0 || radTreeNode.Checked)
				{
					if (!radTreeNode.ContentCssClass.Contains(this.HighlightedNodeClassName))
					{
						RadTreeNode radTreeNode2 = radTreeNode;
						radTreeNode2.ContentCssClass = radTreeNode2.ContentCssClass + " " + this.HighlightedNodeClassName;
					}
				}
				else
				{
					radTreeNode.ContentCssClass = radTreeNode.ContentCssClass.Replace(" " + this.HighlightedNodeClassName, string.Empty);
				}
			}
			return num;
		}

		// Token: 0x060076CB RID: 30411 RVA: 0x001B9994 File Offset: 0x001B7B94
		private int SetHighligtedState(List<PivotGridOlapHierarhicalFieldsList.OlapItemInfo> allItems, IEnumerable<PivotGridOlapHierarhicalFieldsList.OlapItemInfo> items)
		{
			int num = 0;
			int num2 = 0;
			IEnumerable<RadTreeNode> allNodes = this.TreeView.GetAllNodes();
			using (IEnumerator<PivotGridOlapHierarhicalFieldsList.OlapItemInfo> enumerator = items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PivotGridOlapHierarhicalFieldsList.OlapItemInfo item = enumerator.Current;
					int num3 = 0;
					RadTreeNode radTreeNode = allNodes.FirstOrDefault((RadTreeNode n) => n.Attributes["nodeId"] == item.ID.ToString());
					if (radTreeNode != null)
					{
						if (string.IsNullOrEmpty(radTreeNode.Attributes[this.CheckedChildNodesAttributeName]))
						{
							radTreeNode.Attributes.Add(this.CheckedChildNodesAttributeName, "0");
						}
						num3 = int.Parse(radTreeNode.Attributes[this.CheckedChildNodesAttributeName]);
					}
					PivotGridField pivotGridField = this.OwnerPivotGrid.Fields[item.FieldUniqueName];
					if (pivotGridField != null && !pivotGridField.IsHidden)
					{
						num3++;
					}
					if (item.Role != ContainerNodeRole.Selectable)
					{
						num = this.SetHighligtedState(allItems, from i in allItems
						where i.ParentID == item.ID
						select i);
					}
					num2 += num + num3;
					if (radTreeNode != null)
					{
						radTreeNode.Attributes[this.CheckedChildNodesAttributeName] = num.ToString();
						if (num > 0 || radTreeNode.Checked)
						{
							if (!radTreeNode.ContentCssClass.Contains(this.HighlightedNodeClassName))
							{
								RadTreeNode radTreeNode2 = radTreeNode;
								radTreeNode2.ContentCssClass = radTreeNode2.ContentCssClass + " " + this.HighlightedNodeClassName;
							}
						}
						else
						{
							radTreeNode.ContentCssClass = radTreeNode.ContentCssClass.Replace(" " + this.HighlightedNodeClassName, string.Empty);
						}
					}
				}
			}
			return num2;
		}

		// Token: 0x040020A4 RID: 8356
		private readonly string FolderNodeClassName = "rpgFolder";

		// Token: 0x040020A5 RID: 8357
		private readonly string ExpandedFolderNodeClassName = "rpgOpenFolder";

		// Token: 0x040020A6 RID: 8358
		private readonly string HighlightedNodeClassName = "rpgHighlighted";

		// Token: 0x040020A7 RID: 8359
		private readonly string CheckedChildNodesAttributeName = "checkedChildNodes";

		// Token: 0x040020A8 RID: 8360
		private PivotGridConfigurationPanel configurationPanel;

		// Token: 0x040020A9 RID: 8361
		private Dictionary<string, PivotGridField> fieldCaptionsByUniqueName = new Dictionary<string, PivotGridField>();

		// Token: 0x040020AA RID: 8362
		private PivotGridField ownerField;

		// Token: 0x02000C44 RID: 3140
		[Serializable]
		private class OlapItemInfo
		{
			// Token: 0x170026A4 RID: 9892
			// (get) Token: 0x060076CD RID: 30413 RVA: 0x001B9B6C File Offset: 0x001B7D6C
			// (set) Token: 0x060076CE RID: 30414 RVA: 0x001B9B74 File Offset: 0x001B7D74
			public string Text { get; set; }

			// Token: 0x170026A5 RID: 9893
			// (get) Token: 0x060076CF RID: 30415 RVA: 0x001B9B7D File Offset: 0x001B7D7D
			// (set) Token: 0x060076D0 RID: 30416 RVA: 0x001B9B85 File Offset: 0x001B7D85
			public int ID { get; set; }

			// Token: 0x170026A6 RID: 9894
			// (get) Token: 0x060076D1 RID: 30417 RVA: 0x001B9B8E File Offset: 0x001B7D8E
			// (set) Token: 0x060076D2 RID: 30418 RVA: 0x001B9B96 File Offset: 0x001B7D96
			public int ParentID { get; set; }

			// Token: 0x170026A7 RID: 9895
			// (get) Token: 0x060076D3 RID: 30419 RVA: 0x001B9B9F File Offset: 0x001B7D9F
			// (set) Token: 0x060076D4 RID: 30420 RVA: 0x001B9BA7 File Offset: 0x001B7DA7
			public ContainerNodeRole Role { get; set; }

			// Token: 0x170026A8 RID: 9896
			// (get) Token: 0x060076D5 RID: 30421 RVA: 0x001B9BB0 File Offset: 0x001B7DB0
			// (set) Token: 0x060076D6 RID: 30422 RVA: 0x001B9BB8 File Offset: 0x001B7DB8
			public string FieldUniqueName { get; set; }

			// Token: 0x170026A9 RID: 9897
			// (get) Token: 0x060076D7 RID: 30423 RVA: 0x001B9BC1 File Offset: 0x001B7DC1
			// (set) Token: 0x060076D8 RID: 30424 RVA: 0x001B9BC9 File Offset: 0x001B7DC9
			public PivotGridFieldZoneType ZoneType { get; set; }
		}
	}
}
