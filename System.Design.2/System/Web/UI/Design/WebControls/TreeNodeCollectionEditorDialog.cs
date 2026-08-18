using System;
using System.Design;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000129 RID: 297
	internal sealed partial class TreeNodeCollectionEditorDialog : CollectionEditorDialog
	{
		// Token: 0x06000AA9 RID: 2729 RVA: 0x00043594 File Offset: 0x00041794
		public TreeNodeCollectionEditorDialog(System.Web.UI.WebControls.TreeView treeView, TreeViewDesigner treeViewDesigner) : base(treeView.Site)
		{
			this._webTreeView = treeView;
			this._treeViewDesigner = treeViewDesigner;
			this._treeViewPanel = new System.Windows.Forms.Panel();
			this._treeView = new System.Windows.Forms.TreeView();
			this._treeViewToolBar = new ToolStrip();
			this._propertyGrid = new VsPropertyGrid(base.ServiceProvider);
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._propertiesLabel = new System.Windows.Forms.Label();
			this._nodesLabel = new System.Windows.Forms.Label();
			this._addRootButton = base.CreatePushButton(SR.GetString("TreeNodeCollectionEditor_AddRoot"), 3);
			this._addChildButton = base.CreatePushButton(SR.GetString("TreeNodeCollectionEditor_AddChild"), 2);
			this._removeButton = base.CreatePushButton(SR.GetString("TreeNodeCollectionEditor_Remove"), 4);
			this._moveUpButton = base.CreatePushButton(SR.GetString("TreeNodeCollectionEditor_MoveUp"), 5);
			this._moveDownButton = base.CreatePushButton(SR.GetString("TreeNodeCollectionEditor_MoveDown"), 6);
			this._indentButton = base.CreatePushButton(SR.GetString("TreeNodeCollectionEditor_Indent"), 1);
			this._unindentButton = base.CreatePushButton(SR.GetString("TreeNodeCollectionEditor_Unindent"), 0);
			this._toolBarSeparator = new ToolStripSeparator();
			this._treeViewPanel.SuspendLayout();
			base.SuspendLayout();
			this._treeViewPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._treeViewPanel.BackColor = SystemColors.ControlDark;
			this._treeViewPanel.Controls.Add(this._treeView);
			this._treeViewPanel.DockPadding.Left = 1;
			this._treeViewPanel.DockPadding.Right = 1;
			this._treeViewPanel.DockPadding.Bottom = 1;
			this._treeViewPanel.DockPadding.Top = 1;
			this._treeViewPanel.Location = new Point(12, 54);
			this._treeViewPanel.Name = "_treeViewPanel";
			this._treeViewPanel.Size = new Size(227, 233);
			this._treeViewPanel.TabIndex = 1;
			this._treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._treeView.Dock = DockStyle.Fill;
			this._treeView.ImageIndex = -1;
			this._treeView.HideSelection = false;
			this._treeView.Location = new Point(1, 1);
			this._treeView.Name = "_treeView";
			this._treeView.SelectedImageIndex = -1;
			this._treeView.TabIndex = 0;
			this._treeView.AfterSelect += this.OnTreeViewAfterSelect;
			this._treeView.KeyDown += this.OnTreeViewKeyDown;
			this._treeViewToolBar.Items.AddRange(new ToolStripItem[]
			{
				this._addRootButton,
				this._addChildButton,
				this._removeButton,
				this._toolBarSeparator,
				this._moveUpButton,
				this._moveDownButton,
				this._unindentButton,
				this._indentButton
			});
			this._treeViewToolBar.Location = new Point(12, 28);
			this._treeViewToolBar.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._treeViewToolBar.AutoSize = false;
			this._treeViewToolBar.Size = new Size(227, 26);
			this._treeViewToolBar.CanOverflow = false;
			Padding padding = this._treeViewToolBar.Padding;
			padding.Left = 2;
			this._treeViewToolBar.Padding = padding;
			this._treeViewToolBar.Name = "_treeViewToolBar";
			this._treeViewToolBar.RenderMode = ToolStripRenderMode.System;
			this._treeViewToolBar.ShowItemToolTips = true;
			this._treeViewToolBar.GripStyle = ToolStripGripStyle.Hidden;
			this._treeViewToolBar.TabIndex = 1;
			this._treeViewToolBar.TabStop = true;
			this._treeViewToolBar.ItemClicked += this.OnTreeViewToolBarButtonClick;
			this._propertyGrid.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this._propertyGrid.CommandsVisibleIfAvailable = true;
			this._propertyGrid.LargeButtons = false;
			this._propertyGrid.LineColor = SystemColors.ScrollBar;
			this._propertyGrid.Location = new Point(260, 28);
			this._propertyGrid.Name = "_propertyGrid";
			this._propertyGrid.PropertySort = PropertySort.Alphabetical;
			this._propertyGrid.Size = new Size(204, 259);
			this._propertyGrid.TabIndex = 3;
			this._propertyGrid.Text = SR.GetString("MenuItemCollectionEditor_PropertyGrid");
			this._propertyGrid.ToolbarVisible = true;
			this._propertyGrid.ViewBackColor = SystemColors.Window;
			this._propertyGrid.ViewForeColor = SystemColors.WindowText;
			this._propertyGrid.PropertyValueChanged += this.OnPropertyGridPropertyValueChanged;
			this._propertyGrid.Site = this._webTreeView.Site;
			this._okButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this._okButton.FlatStyle = FlatStyle.System;
			this._okButton.Location = new Point(309, 296);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new Size(75, 23);
			this._okButton.TabIndex = 9;
			this._okButton.Text = SR.GetString("TreeNodeCollectionEditor_OK");
			this._okButton.Click += this.OnOkButtonClick;
			this._cancelButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this._cancelButton.FlatStyle = FlatStyle.System;
			this._cancelButton.Location = new Point(389, 296);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new Size(75, 23);
			this._cancelButton.TabIndex = 10;
			this._cancelButton.Text = SR.GetString("TreeNodeCollectionEditor_Cancel");
			this._cancelButton.Click += this.OnCancelButtonClick;
			this._propertiesLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this._propertiesLabel.Location = new Point(260, 12);
			this._propertiesLabel.Name = "_propertiesLabel";
			this._propertiesLabel.Size = new Size(204, 14);
			this._propertiesLabel.TabIndex = 2;
			this._propertiesLabel.Text = SR.GetString("TreeNodeCollectionEditor_Properties");
			this._nodesLabel.Location = new Point(12, 12);
			this._nodesLabel.Name = "_nodesLabel";
			this._nodesLabel.Size = new Size(100, 14);
			this._nodesLabel.TabIndex = 0;
			this._nodesLabel.Text = SR.GetString("TreeNodeCollectionEditor_Nodes");
			ImageList imageList = new ImageList();
			imageList.ImageSize = new Size(16, 16);
			imageList.TransparentColor = Color.Magenta;
			imageList.Images.AddStrip(BitmapSelector.CreateBitmap(base.GetType(), "Commands.bmp"));
			this._treeViewToolBar.ImageList = imageList;
			base.ClientSize = new Size(478, 331);
			base.CancelButton = this._cancelButton;
			base.Controls.AddRange(new Control[]
			{
				this._nodesLabel,
				this._propertiesLabel,
				this._cancelButton,
				this._okButton,
				this._propertyGrid,
				this._treeViewPanel,
				this._treeViewToolBar
			});
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MinimumSize = new Size(484, 331);
			base.Name = "TreeNodeEditor";
			base.SizeGripStyle = SizeGripStyle.Hide;
			this.Text = SR.GetString("TreeNodeCollectionEditor_Title");
			base.InitializeForm();
			this._treeViewPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00043D51 File Offset: 0x00041F51
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.TreeView.CollectionEditor";
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00043D58 File Offset: 0x00041F58
		private void LoadNodes(System.Windows.Forms.TreeNodeCollection destNodes, System.Web.UI.WebControls.TreeNodeCollection sourceNodes)
		{
			foreach (object obj in sourceNodes)
			{
				System.Web.UI.WebControls.TreeNode treeNode = (System.Web.UI.WebControls.TreeNode)obj;
				TreeNodeCollectionEditorDialog.TreeNodeContainer treeNodeContainer = new TreeNodeCollectionEditorDialog.TreeNodeContainer();
				destNodes.Add(treeNodeContainer);
				treeNodeContainer.Text = treeNode.Text;
				System.Web.UI.WebControls.TreeNode treeNode2 = (System.Web.UI.WebControls.TreeNode)((ICloneable)treeNode).Clone();
				this._treeViewDesigner.RegisterClone(treeNode, treeNode2);
				treeNodeContainer.WebTreeNode = treeNode2;
				if (treeNode.ChildNodes.Count > 0)
				{
					this.LoadNodes(treeNodeContainer.Nodes, treeNode.ChildNodes);
				}
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00043E04 File Offset: 0x00042004
		private void OnAddRootButtonClick()
		{
			TreeNodeCollectionEditorDialog.TreeNodeContainer treeNodeContainer = new TreeNodeCollectionEditorDialog.TreeNodeContainer();
			this._treeView.Nodes.Add(treeNodeContainer);
			string @string = SR.GetString("TreeNodeCollectionEditor_NewNodeText");
			treeNodeContainer.Text = @string;
			treeNodeContainer.WebTreeNode.Text = @string;
			this._treeView.SelectedNode = treeNodeContainer;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00043E54 File Offset: 0x00042054
		private void OnAddChildButtonClick()
		{
			System.Windows.Forms.TreeNode selectedNode = this._treeView.SelectedNode;
			if (selectedNode != null)
			{
				TreeNodeCollectionEditorDialog.TreeNodeContainer treeNodeContainer = new TreeNodeCollectionEditorDialog.TreeNodeContainer();
				selectedNode.Nodes.Add(treeNodeContainer);
				string @string = SR.GetString("TreeNodeCollectionEditor_NewNodeText");
				treeNodeContainer.Text = @string;
				treeNodeContainer.WebTreeNode.Text = @string;
				if (!selectedNode.IsExpanded)
				{
					selectedNode.Expand();
				}
				this._treeView.SelectedNode = treeNodeContainer;
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00043EBC File Offset: 0x000420BC
		private void OnIndentButtonClick()
		{
			System.Windows.Forms.TreeNode selectedNode = this._treeView.SelectedNode;
			if (selectedNode != null)
			{
				System.Windows.Forms.TreeNode prevNode = selectedNode.PrevNode;
				if (prevNode != null)
				{
					selectedNode.Remove();
					prevNode.Nodes.Add(selectedNode);
					this._treeView.SelectedNode = selectedNode;
				}
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00043F04 File Offset: 0x00042104
		private void OnMoveDownButtonClick()
		{
			System.Windows.Forms.TreeNode selectedNode = this._treeView.SelectedNode;
			if (selectedNode != null)
			{
				System.Windows.Forms.TreeNode nextNode = selectedNode.NextNode;
				System.Windows.Forms.TreeNodeCollection nodes = this._treeView.Nodes;
				if (selectedNode.Parent != null)
				{
					nodes = selectedNode.Parent.Nodes;
				}
				if (nextNode != null)
				{
					selectedNode.Remove();
					nodes.Insert(nextNode.Index + 1, selectedNode);
					this._treeView.SelectedNode = selectedNode;
				}
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00043F6C File Offset: 0x0004216C
		private void OnMoveUpButtonClick()
		{
			System.Windows.Forms.TreeNode selectedNode = this._treeView.SelectedNode;
			if (selectedNode != null)
			{
				System.Windows.Forms.TreeNode prevNode = selectedNode.PrevNode;
				System.Windows.Forms.TreeNodeCollection nodes = this._treeView.Nodes;
				if (selectedNode.Parent != null)
				{
					nodes = selectedNode.Parent.Nodes;
				}
				if (prevNode != null)
				{
					selectedNode.Remove();
					nodes.Insert(prevNode.Index, selectedNode);
					this._treeView.SelectedNode = selectedNode;
				}
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00043FD1 File Offset: 0x000421D1
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			this.SaveNodes(this._webTreeView.Nodes, this._treeView.Nodes);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00043FFC File Offset: 0x000421FC
		private void OnPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			TreeNodeCollectionEditorDialog.TreeNodeContainer treeNodeContainer = (TreeNodeCollectionEditorDialog.TreeNodeContainer)this._treeView.SelectedNode;
			if (treeNodeContainer != null)
			{
				treeNodeContainer.Text = treeNodeContainer.WebTreeNode.Text;
			}
			this._propertyGrid.Refresh();
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0004403C File Offset: 0x0004223C
		private void OnRemoveButtonClick()
		{
			System.Windows.Forms.TreeNode selectedNode = this._treeView.SelectedNode;
			if (selectedNode != null)
			{
				System.Windows.Forms.TreeNodeCollection nodes;
				if (selectedNode.Parent != null)
				{
					nodes = selectedNode.Parent.Nodes;
				}
				else
				{
					nodes = this._treeView.Nodes;
				}
				if (nodes.Count == 1)
				{
					this._treeView.SelectedNode = selectedNode.Parent;
				}
				else if (selectedNode.NextNode != null)
				{
					this._treeView.SelectedNode = selectedNode.NextNode;
				}
				else
				{
					this._treeView.SelectedNode = selectedNode.PrevNode;
				}
				selectedNode.Remove();
				if (this._treeView.SelectedNode == null)
				{
					this._propertyGrid.SelectedObject = null;
				}
				this.UpdateEnabledState();
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000440EC File Offset: 0x000422EC
		protected override void OnInitialActivated(EventArgs e)
		{
			base.OnInitialActivated(e);
			this.LoadNodes(this._treeView.Nodes, this._webTreeView.Nodes);
			if (this._treeView.Nodes.Count > 0)
			{
				this._treeView.SelectedNode = this._treeView.Nodes[0];
			}
			this.UpdateEnabledState();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00044151 File Offset: 0x00042351
		private void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				this._propertyGrid.SelectedObject = ((TreeNodeCollectionEditorDialog.TreeNodeContainer)e.Node).WebTreeNode;
			}
			else
			{
				this._propertyGrid.SelectedObject = null;
			}
			this.UpdateEnabledState();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0004418C File Offset: 0x0004238C
		private void OnTreeViewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Insert)
			{
				if ((Control.ModifierKeys & Keys.Alt) != Keys.None)
				{
					this.OnAddChildButtonClick();
				}
				else
				{
					this.OnAddRootButtonClick();
				}
				e.Handled = true;
				return;
			}
			if (e.KeyCode == Keys.Delete)
			{
				this.OnRemoveButtonClick();
				e.Handled = true;
				return;
			}
			if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
			{
				if (e.KeyCode == Keys.Up)
				{
					this.OnMoveUpButtonClick();
				}
				else if (e.KeyCode == Keys.Down)
				{
					this.OnMoveDownButtonClick();
				}
				else if (e.KeyCode == Keys.Left)
				{
					this.OnUnindentButtonClick();
				}
				else if (e.KeyCode == Keys.Right)
				{
					this.OnIndentButtonClick();
				}
				e.Handled = true;
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00044238 File Offset: 0x00042438
		private void OnTreeViewToolBarButtonClick(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem == this._addRootButton)
			{
				this.OnAddRootButtonClick();
				return;
			}
			if (e.ClickedItem == this._addChildButton)
			{
				this.OnAddChildButtonClick();
				return;
			}
			if (e.ClickedItem == this._removeButton)
			{
				this.OnRemoveButtonClick();
				return;
			}
			if (e.ClickedItem == this._moveUpButton)
			{
				this.OnMoveUpButtonClick();
				return;
			}
			if (e.ClickedItem == this._unindentButton)
			{
				this.OnUnindentButtonClick();
				return;
			}
			if (e.ClickedItem == this._indentButton)
			{
				this.OnIndentButtonClick();
				return;
			}
			if (e.ClickedItem == this._moveDownButton)
			{
				this.OnMoveDownButtonClick();
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000442D8 File Offset: 0x000424D8
		private void OnUnindentButtonClick()
		{
			System.Windows.Forms.TreeNode selectedNode = this._treeView.SelectedNode;
			if (selectedNode != null)
			{
				System.Windows.Forms.TreeNode parent = selectedNode.Parent;
				if (parent != null)
				{
					System.Windows.Forms.TreeNodeCollection nodes = this._treeView.Nodes;
					if (parent.Parent != null)
					{
						nodes = parent.Parent.Nodes;
					}
					if (parent != null)
					{
						selectedNode.Remove();
						nodes.Insert(parent.Index + 1, selectedNode);
						this._treeView.SelectedNode = selectedNode;
					}
				}
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00044344 File Offset: 0x00042544
		private void SaveNodes(System.Web.UI.WebControls.TreeNodeCollection destNodes, System.Windows.Forms.TreeNodeCollection sourceNodes)
		{
			destNodes.Clear();
			foreach (object obj in sourceNodes)
			{
				TreeNodeCollectionEditorDialog.TreeNodeContainer treeNodeContainer = (TreeNodeCollectionEditorDialog.TreeNodeContainer)obj;
				System.Web.UI.WebControls.TreeNode webTreeNode = treeNodeContainer.WebTreeNode;
				destNodes.Add(webTreeNode);
				if (treeNodeContainer.Nodes.Count > 0)
				{
					this.SaveNodes(webTreeNode.ChildNodes, treeNodeContainer.Nodes);
				}
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000443C8 File Offset: 0x000425C8
		private void UpdateEnabledState()
		{
			System.Windows.Forms.TreeNode selectedNode = this._treeView.SelectedNode;
			if (selectedNode != null)
			{
				this._addChildButton.Enabled = true;
				this._removeButton.Enabled = true;
				this._moveUpButton.Enabled = (selectedNode.PrevNode != null);
				this._moveDownButton.Enabled = (selectedNode.NextNode != null);
				this._indentButton.Enabled = (selectedNode.PrevNode != null);
				this._unindentButton.Enabled = (selectedNode.Parent != null);
				return;
			}
			this._addChildButton.Enabled = false;
			this._removeButton.Enabled = false;
			this._moveUpButton.Enabled = false;
			this._moveDownButton.Enabled = false;
			this._indentButton.Enabled = false;
			this._unindentButton.Enabled = false;
		}

		// Token: 0x0400065C RID: 1628
		private System.Windows.Forms.Panel _treeViewPanel;

		// Token: 0x0400065D RID: 1629
		private System.Windows.Forms.TreeView _treeView;

		// Token: 0x0400065E RID: 1630
		private PropertyGrid _propertyGrid;

		// Token: 0x0400065F RID: 1631
		private System.Windows.Forms.Button _okButton;

		// Token: 0x04000660 RID: 1632
		private System.Windows.Forms.Button _cancelButton;

		// Token: 0x04000661 RID: 1633
		private System.Windows.Forms.Label _propertiesLabel;

		// Token: 0x04000662 RID: 1634
		private System.Windows.Forms.Label _nodesLabel;

		// Token: 0x04000663 RID: 1635
		private ToolStripButton _addRootButton;

		// Token: 0x04000664 RID: 1636
		private ToolStripButton _addChildButton;

		// Token: 0x04000665 RID: 1637
		private ToolStripButton _removeButton;

		// Token: 0x04000666 RID: 1638
		private ToolStripButton _moveUpButton;

		// Token: 0x04000667 RID: 1639
		private ToolStripButton _moveDownButton;

		// Token: 0x04000668 RID: 1640
		private ToolStripButton _indentButton;

		// Token: 0x04000669 RID: 1641
		private ToolStripButton _unindentButton;

		// Token: 0x0400066A RID: 1642
		private ToolStripSeparator _toolBarSeparator;

		// Token: 0x0400066B RID: 1643
		private ToolStrip _treeViewToolBar;

		// Token: 0x0400066C RID: 1644
		private System.Web.UI.WebControls.TreeView _webTreeView;

		// Token: 0x0400066D RID: 1645
		private TreeViewDesigner _treeViewDesigner;

		// Token: 0x0200044D RID: 1101
		private class TreeNodeContainer : System.Windows.Forms.TreeNode
		{
			// Token: 0x170008B2 RID: 2226
			// (get) Token: 0x0600292B RID: 10539 RVA: 0x000F9C85 File Offset: 0x000F7E85
			// (set) Token: 0x0600292C RID: 10540 RVA: 0x000F9CA0 File Offset: 0x000F7EA0
			public System.Web.UI.WebControls.TreeNode WebTreeNode
			{
				get
				{
					if (this._webTreeNode == null)
					{
						this._webTreeNode = new System.Web.UI.WebControls.TreeNode();
					}
					return this._webTreeNode;
				}
				set
				{
					this._webTreeNode = value;
				}
			}

			// Token: 0x04001D24 RID: 7460
			private System.Web.UI.WebControls.TreeNode _webTreeNode;
		}
	}
}
