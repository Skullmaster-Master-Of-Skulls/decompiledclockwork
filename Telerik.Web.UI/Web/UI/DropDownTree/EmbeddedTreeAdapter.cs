using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.DropDownTree
{
	// Token: 0x02000B2A RID: 2858
	internal class EmbeddedTreeAdapter : IEmbeddedTreeAdapter<RadTreeView>
	{
		// Token: 0x06006B38 RID: 27448 RVA: 0x00190868 File Offset: 0x0018EA68
		public IList<DropDownTreeNode> GetAllNodes()
		{
			IList<RadTreeNode> allNodes = this._treeView.GetAllNodes();
			List<DropDownTreeNode> list = new List<DropDownTreeNode>();
			foreach (RadTreeNode treeViewNode in allNodes)
			{
				list.Add(new DropDownTreeNode(this._dropDownTree)
				{
					_treeNodeAdapter = new TreeNodeAdapter(treeViewNode)
				});
			}
			return list;
		}

		// Token: 0x1700231D RID: 8989
		// (get) Token: 0x06006B39 RID: 27449 RVA: 0x001908E0 File Offset: 0x0018EAE0
		public string ClientID
		{
			get
			{
				return this._treeView.ClientID;
			}
		}

		// Token: 0x1700231E RID: 8990
		// (get) Token: 0x06006B3A RID: 27450 RVA: 0x001908ED File Offset: 0x0018EAED
		// (set) Token: 0x06006B3B RID: 27451 RVA: 0x001908FA File Offset: 0x0018EAFA
		public string DataFieldID
		{
			get
			{
				return this._treeView.DataFieldID;
			}
			set
			{
				this._treeView.DataFieldID = value;
			}
		}

		// Token: 0x1700231F RID: 8991
		// (get) Token: 0x06006B3C RID: 27452 RVA: 0x00190908 File Offset: 0x0018EB08
		// (set) Token: 0x06006B3D RID: 27453 RVA: 0x00190915 File Offset: 0x0018EB15
		public string DataFieldParentID
		{
			get
			{
				return this._treeView.DataFieldParentID;
			}
			set
			{
				this._treeView.DataFieldParentID = value;
			}
		}

		// Token: 0x17002320 RID: 8992
		// (get) Token: 0x06006B3E RID: 27454 RVA: 0x00190923 File Offset: 0x0018EB23
		// (set) Token: 0x06006B3F RID: 27455 RVA: 0x00190930 File Offset: 0x0018EB30
		public string DataTextField
		{
			get
			{
				return this._treeView.DataTextField;
			}
			set
			{
				this._treeView.DataTextField = value;
			}
		}

		// Token: 0x17002321 RID: 8993
		// (get) Token: 0x06006B40 RID: 27456 RVA: 0x0019093E File Offset: 0x0018EB3E
		// (set) Token: 0x06006B41 RID: 27457 RVA: 0x0019094B File Offset: 0x0018EB4B
		public string DataValueField
		{
			get
			{
				return this._treeView.DataValueField;
			}
			set
			{
				this._treeView.DataValueField = value;
			}
		}

		// Token: 0x17002322 RID: 8994
		// (get) Token: 0x06006B42 RID: 27458 RVA: 0x00190959 File Offset: 0x0018EB59
		// (set) Token: 0x06006B43 RID: 27459 RVA: 0x00190966 File Offset: 0x0018EB66
		public object DataSource
		{
			get
			{
				return this._treeView.DataSource;
			}
			set
			{
				this._treeView.DataSource = value;
			}
		}

		// Token: 0x17002323 RID: 8995
		// (get) Token: 0x06006B44 RID: 27460 RVA: 0x00190974 File Offset: 0x0018EB74
		// (set) Token: 0x06006B45 RID: 27461 RVA: 0x00190981 File Offset: 0x0018EB81
		public string DataSourceID
		{
			get
			{
				return this._treeView.DataSourceID;
			}
			set
			{
				this._treeView.DataSourceID = value;
			}
		}

		// Token: 0x17002324 RID: 8996
		// (get) Token: 0x06006B46 RID: 27462 RVA: 0x0019098F File Offset: 0x0018EB8F
		// (set) Token: 0x06006B47 RID: 27463 RVA: 0x0019099C File Offset: 0x0018EB9C
		public string ODataDataSourceID
		{
			get
			{
				return this._treeView.ODataDataSourceID;
			}
			set
			{
				this._treeView.ODataDataSourceID = value;
			}
		}

		// Token: 0x17002325 RID: 8997
		// (get) Token: 0x06006B48 RID: 27464 RVA: 0x001909AA File Offset: 0x0018EBAA
		// (set) Token: 0x06006B49 RID: 27465 RVA: 0x001909B7 File Offset: 0x0018EBB7
		public string ClientDataSourceID
		{
			get
			{
				return this._treeView.ClientDataSourceID;
			}
			set
			{
				this._treeView.ClientDataSourceID = value;
			}
		}

		// Token: 0x17002326 RID: 8998
		// (get) Token: 0x06006B4A RID: 27466 RVA: 0x001909C5 File Offset: 0x0018EBC5
		// (set) Token: 0x06006B4B RID: 27467 RVA: 0x001909D2 File Offset: 0x0018EBD2
		public ITemplate NodeTemplate
		{
			get
			{
				return this._treeView.NodeTemplate;
			}
			set
			{
				this._treeView.NodeTemplate = value;
			}
		}

		// Token: 0x17002327 RID: 8999
		// (set) Token: 0x06006B4C RID: 27468 RVA: 0x001909E0 File Offset: 0x0018EBE0
		public virtual DropDownTreeCheckBoxes CheckBoxes
		{
			set
			{
				if (value != DropDownTreeCheckBoxes.None)
				{
					this._treeView.CheckBoxes = true;
					switch (value)
					{
					case DropDownTreeCheckBoxes.CheckChildNodes:
						this._treeView.CheckChildNodes = true;
						return;
					case DropDownTreeCheckBoxes.TriState:
						this._treeView.CheckChildNodes = true;
						this._treeView.TriStateCheckBoxes = true;
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06006B4D RID: 27469 RVA: 0x00190A34 File Offset: 0x0018EC34
		// (remove) Token: 0x06006B4E RID: 27470 RVA: 0x00190A6C File Offset: 0x0018EC6C
		public event DropDownTreeNodeDataBoundEventHandler DropDownTreeNodeDataBound;

		// Token: 0x06006B4F RID: 27471 RVA: 0x00190ADC File Offset: 0x0018ECDC
		private List<RadTreeNode> FindNodes(bool findByValue, string searchedContext)
		{
			List<RadTreeNode> list = new List<RadTreeNode>();
			string[] array;
			if (this._dropDownTree.CheckBoxes != DropDownTreeCheckBoxes.None)
			{
				if (findByValue)
				{
					array = searchedContext.Split(new string[]
					{
						","
					}, StringSplitOptions.None);
				}
				else
				{
					array = searchedContext.Split(new string[]
					{
						this._dropDownTree.EntriesDelimiter.Trim()
					}, StringSplitOptions.None);
				}
			}
			else
			{
				array = new string[]
				{
					searchedContext
				};
			}
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string context = array2[i];
				IList<RadTreeNode> nodesByCriteria;
				if (findByValue)
				{
					nodesByCriteria = this._treeView.GetNodesByCriteria((RadTreeNode x) => x.Value == context.Trim(), true);
				}
				else
				{
					nodesByCriteria = this._treeView.GetNodesByCriteria((RadTreeNode x) => x.Text == context.Trim(), true);
				}
				if (nodesByCriteria.Count > 0 && !list.Contains(nodesByCriteria[0]))
				{
					list.Add(nodesByCriteria[0]);
				}
			}
			return list;
		}

		// Token: 0x06006B50 RID: 27472 RVA: 0x00190BF4 File Offset: 0x0018EDF4
		private void ProcessNodesToEntries(List<RadTreeNode> nodes)
		{
			if (nodes.Count > 0)
			{
				this._dropDownTree.Entries.Clear();
				if (this._dropDownTree.CheckBoxes == DropDownTreeCheckBoxes.None)
				{
					this._dropDownTree.CreateEntryFromRadTreeNode(nodes[0]);
					nodes[0].Selected = true;
					return;
				}
				this.CreateEntriesFromNodes(nodes);
			}
		}

		// Token: 0x06006B51 RID: 27473 RVA: 0x00190C50 File Offset: 0x0018EE50
		public void CreateEntry(bool byValue, string context)
		{
			List<RadTreeNode> nodes = this.FindNodes(byValue, context);
			this.ProcessNodesToEntries(nodes);
		}

		// Token: 0x06006B52 RID: 27474 RVA: 0x00190C70 File Offset: 0x0018EE70
		public void ClearNodesState()
		{
			RadTreeView embeddedTree = this.GetEmbeddedTree();
			IList<RadTreeNode> allNodes = embeddedTree.GetAllNodes();
			int count = allNodes.Count;
			for (int i = 0; i < count; i++)
			{
				allNodes[i].Selected = false;
				allNodes[i].Checked = false;
			}
		}

		// Token: 0x06006B53 RID: 27475 RVA: 0x00190CB8 File Offset: 0x0018EEB8
		public EmbeddedTreeAdapter(RadDropDownTree dropDownTree, RadTreeView treeView)
		{
			this._treeView = treeView;
			this._dropDownTree = dropDownTree;
		}

		// Token: 0x06006B54 RID: 27476 RVA: 0x00190CD0 File Offset: 0x0018EED0
		public DropDownTreeNode FindNodeByHierarchicalIndex(string hierarchicalIndex)
		{
			RadTreeNode treeViewNode = this._treeView.FindItemByHierarchicalIndex(hierarchicalIndex) as RadTreeNode;
			return new DropDownTreeNode(this._dropDownTree)
			{
				_treeNodeAdapter = new TreeNodeAdapter(treeViewNode)
			};
		}

		// Token: 0x06006B55 RID: 27477 RVA: 0x00190D08 File Offset: 0x0018EF08
		public void DataBind()
		{
			this._treeView.NodeDataBound += this.TreeView_NodeDataBound;
			this._treeView.DataBound += this.TreeView_DataBound;
			this._treeView.DataBind();
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x00190D43 File Offset: 0x0018EF43
		private void TreeView_DataBound(object sender, EventArgs e)
		{
			this.CreateEntriesFromNodes(this._dropDownTree.NodesForEntries);
		}

		// Token: 0x06006B57 RID: 27479 RVA: 0x00190D64 File Offset: 0x0018EF64
		internal void CreateEntriesFromNodes(IList<RadTreeNode> nodes)
		{
			foreach (RadTreeNode radTreeNode in nodes)
			{
				radTreeNode.Checked = true;
			}
			IList<RadTreeNode> list;
			if (this._dropDownTree.CheckBoxes == DropDownTreeCheckBoxes.TriState)
			{
				list = this._treeView.GetNodesByCriteria((RadTreeNode x) => x.CheckState == TreeNodeCheckState.Checked, false);
			}
			else
			{
				list = nodes;
			}
			foreach (RadTreeNode node in list)
			{
				this._dropDownTree.CreateEntryFromRadTreeNode(node);
			}
		}

		// Token: 0x06006B58 RID: 27480 RVA: 0x00190E2C File Offset: 0x0018F02C
		private void TreeView_NodeDataBound(object sender, RadTreeNodeEventArgs e)
		{
			if (this.DropDownTreeNodeDataBound != null)
			{
				DropDownTreeNode dropDownTreeNode = new DropDownTreeNode(this._dropDownTree);
				dropDownTreeNode._treeNodeAdapter = new TreeNodeAdapter(e.Node);
				this.DropDownTreeNodeDataBound(sender, new DropDownTreeNodeDataBoundEventArguments(dropDownTreeNode));
			}
		}

		// Token: 0x06006B59 RID: 27481 RVA: 0x00190E70 File Offset: 0x0018F070
		public void RenderEmbeddedTree(HtmlTextWriter writer)
		{
			RadTreeView embeddedTree = this.GetEmbeddedTree();
			embeddedTree.RenderControl(writer);
		}

		// Token: 0x06006B5A RID: 27482 RVA: 0x00190E8B File Offset: 0x0018F08B
		public RadTreeView GetEmbeddedTree()
		{
			return this._treeView;
		}

		// Token: 0x06006B5B RID: 27483 RVA: 0x00190E94 File Offset: 0x0018F094
		public void ExpandEmbeddedTree()
		{
			RadTreeView embeddedTree = this.GetEmbeddedTree();
			embeddedTree.ExpandAllNodes();
		}

		// Token: 0x06006B5C RID: 27484 RVA: 0x00190EB0 File Offset: 0x0018F0B0
		public void SyncDataBindings(List<DropDownNodeBinding> dataBindings)
		{
			if (dataBindings != null && dataBindings.Count > 0)
			{
				this._treeView.DataBindings.Clear();
				foreach (DropDownNodeBinding dropDownNodeBinding in dataBindings)
				{
					TreeNodeExpandMode expandMode = (dropDownNodeBinding.ExpandMode == DropDownTreeNodeExpandMode.ClientSide) ? TreeNodeExpandMode.ClientSide : TreeNodeExpandMode.WebService;
					this._treeView.DataBindings.Add(new RadTreeNodeBinding
					{
						Depth = dropDownNodeBinding.Depth,
						ExpandMode = expandMode
					});
				}
			}
		}

		// Token: 0x06006B5D RID: 27485 RVA: 0x00190F4C File Offset: 0x0018F14C
		public void SyncWebServiceSettings(WebServiceSettings webServiceSettings)
		{
			if ((webServiceSettings.Path != "" || webServiceSettings.Method != "") && this._dropDownTree.EnableFiltering)
			{
				throw new Exception("It is not possible to combine Web Service Binding and Filtering");
			}
			this._treeView.WebServiceSettings.Path = webServiceSettings.Path;
			this._treeView.WebServiceSettings.Method = webServiceSettings.Method;
			this._treeView.WebServiceSettings.UseHttpGet = webServiceSettings.UseHttpGet;
			this._treeView.ClientNodeTemplate = this._dropDownTree.ClientNodeTemplate;
		}

		// Token: 0x04001CF4 RID: 7412
		private RadTreeView _treeView;

		// Token: 0x04001CF5 RID: 7413
		private RadDropDownTree _dropDownTree;
	}
}
