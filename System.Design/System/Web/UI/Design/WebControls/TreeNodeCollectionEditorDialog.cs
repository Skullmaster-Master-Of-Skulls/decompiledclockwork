using System;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004EE RID: 1262
	internal sealed partial class TreeNodeCollectionEditorDialog : CollectionEditorDialog
	{
		// Token: 0x06002D1B RID: 11547 RVA: 0x000FE9B0 File Offset: 0x000FD9B0
		public TreeNodeCollectionEditorDialog(System.Web.UI.WebControls.TreeView treeView, TreeViewDesigner treeViewDesigner) : base(treeView.Site)
		{
			this._webTreeView = treeView;
			this._treeViewDesigner = treeViewDesigner;
			this._treeViewPanel = new System.Windows.Forms.Panel();
			this._treeView = new System.Windows.Forms.TreeView();
			this._treeViewToolBar = new ToolStrip();
			ToolStripRenderer toolStripRenderer = UIServiceHelper.GetToolStripRenderer(base.ServiceProvider);
			if (toolStripRenderer != null)
			{
				this._treeViewToolBar.Renderer = toolStripRenderer;
			}
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
			imageList.Images.AddStrip(new Bitmap(base.GetType(), "Commands.bmp"));
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

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000FF189 File Offset: 0x000FE189
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.TreeView.CollectionEditor";
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000FF190 File Offset: 0x000FE190
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

		// Token: 0x06002D1E RID: 11550 RVA: 0x000FF23C File Offset: 0x000FE23C
		private void OnAddRootButtonClick()
		{
			TreeNodeCollectionEditorDialog.TreeNodeContainer treeNodeContainer = new TreeNodeCollectionEditorDialog.TreeNodeContainer();
			this._treeView.Nodes.Add(treeNodeContainer);
			string @string = SR.GetString("TreeNodeCollectionEditor_NewNodeText");
			treeNodeContainer.Text = @string;
			treeNodeContainer.WebTreeNode.Text = @string;
			this._treeView.SelectedNode = treeNodeContainer;
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000FF28C File Offset: 0x000FE28C
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

		// Token: 0x06002D20 RID: 11552 RVA: 0x000FF2F3 File Offset: 0x000FE2F3
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000FF304 File Offset: 0x000FE304
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

		// Token: 0x06002D22 RID: 11554 RVA: 0x000FF34C File Offset: 0x000FE34C
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

		// Token: 0x06002D23 RID: 11555 RVA: 0x000FF3B4 File Offset: 0x000FE3B4
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

		// Token: 0x06002D24 RID: 11556 RVA: 0x000FF419 File Offset: 0x000FE419
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			this.SaveNodes(this._webTreeView.Nodes, this._treeView.Nodes);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000FF444 File Offset: 0x000FE444
		private void OnPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			TreeNodeCollectionEditorDialog.TreeNodeContainer treeNodeContainer = (TreeNodeCollectionEditorDialog.TreeNodeContainer)this._treeView.SelectedNode;
			if (treeNodeContainer != null)
			{
				treeNodeContainer.Text = treeNodeContainer.WebTreeNode.Text;
			}
			this._propertyGrid.Refresh();
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000FF484 File Offset: 0x000FE484
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

		// Token: 0x06002D27 RID: 11559 RVA: 0x000FF534 File Offset: 0x000FE534
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

		// Token: 0x06002D28 RID: 11560 RVA: 0x000FF599 File Offset: 0x000FE599
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

		// Token: 0x06002D29 RID: 11561 RVA: 0x000FF5D4 File Offset: 0x000FE5D4
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

		// Token: 0x06002D2A RID: 11562 RVA: 0x000FF680 File Offset: 0x000FE680
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

		// Token: 0x06002D2B RID: 11563 RVA: 0x000FF720 File Offset: 0x000FE720
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

		// Token: 0x06002D2C RID: 11564 RVA: 0x000FF78C File Offset: 0x000FE78C
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

		// Token: 0x06002D2D RID: 11565 RVA: 0x000FF810 File Offset: 0x000FE810
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

		// Token: 0x04001EB4 RID: 7860
		private System.Windows.Forms.Panel _treeViewPanel;

		// Token: 0x04001EB5 RID: 7861
		private System.Windows.Forms.TreeView _treeView;

		// Token: 0x04001EB6 RID: 7862
		private PropertyGrid _propertyGrid;

		// Token: 0x04001EB7 RID: 7863
		private System.Windows.Forms.Button _okButton;

		// Token: 0x04001EB8 RID: 7864
		private System.Windows.Forms.Button _cancelButton;

		// Token: 0x04001EB9 RID: 7865
		private System.Windows.Forms.Label _propertiesLabel;

		// Token: 0x04001EBA RID: 7866
		private System.Windows.Forms.Label _nodesLabel;

		// Token: 0x04001EBB RID: 7867
		private ToolStripButton _addRootButton;

		// Token: 0x04001EBC RID: 7868
		private ToolStripButton _addChildButton;

		// Token: 0x04001EBD RID: 7869
		private ToolStripButton _removeButton;

		// Token: 0x04001EBE RID: 7870
		private ToolStripButton _moveUpButton;

		// Token: 0x04001EBF RID: 7871
		private ToolStripButton _moveDownButton;

		// Token: 0x04001EC0 RID: 7872
		private ToolStripButton _indentButton;

		// Token: 0x04001EC1 RID: 7873
		private ToolStripButton _unindentButton;

		// Token: 0x04001EC2 RID: 7874
		private ToolStripSeparator _toolBarSeparator;

		// Token: 0x04001EC3 RID: 7875
		private ToolStrip _treeViewToolBar;

		// Token: 0x04001EC4 RID: 7876
		private System.Web.UI.WebControls.TreeView _webTreeView;

		// Token: 0x04001EC5 RID: 7877
		private TreeViewDesigner _treeViewDesigner;

		// Token: 0x020004EF RID: 1263
		private class TreeNodeContainer : System.Windows.Forms.TreeNode
		{
			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000FF8E9 File Offset: 0x000FE8E9
			// (set) Token: 0x06002D2F RID: 11567 RVA: 0x000FF904 File Offset: 0x000FE904
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

			// Token: 0x04001EC6 RID: 7878
			private System.Web.UI.WebControls.TreeNode _webTreeNode;
		}
	}
}
