using System;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E9 RID: 233
	internal sealed partial class MenuItemCollectionEditorDialog : CollectionEditorDialog
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x0002BBB8 File Offset: 0x00029DB8
		public MenuItemCollectionEditorDialog(System.Web.UI.WebControls.Menu menu, MenuDesigner menuDesigner) : base(menu.Site)
		{
			this._webMenu = menu;
			this._menuDesigner = menuDesigner;
			this._treeViewPanel = new System.Windows.Forms.Panel();
			this._treeView = new System.Windows.Forms.TreeView();
			this._treeViewToolBar = new ToolStrip();
			this._propertyGrid = new VsPropertyGrid(base.ServiceProvider);
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._propertiesLabel = new System.Windows.Forms.Label();
			this._nodesLabel = new System.Windows.Forms.Label();
			this._addRootButton = base.CreatePushButton(SR.GetString("MenuItemCollectionEditor_AddRoot"), 3);
			this._addChildButton = base.CreatePushButton(SR.GetString("MenuItemCollectionEditor_AddChild"), 2);
			this._removeButton = base.CreatePushButton(SR.GetString("MenuItemCollectionEditor_Remove"), 4);
			this._moveUpButton = base.CreatePushButton(SR.GetString("MenuItemCollectionEditor_MoveUp"), 5);
			this._moveDownButton = base.CreatePushButton(SR.GetString("MenuItemCollectionEditor_MoveDown"), 6);
			this._indentButton = base.CreatePushButton(SR.GetString("MenuItemCollectionEditor_Indent"), 1);
			this._unindentButton = base.CreatePushButton(SR.GetString("MenuItemCollectionEditor_Unindent"), 0);
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
			this._propertyGrid.Site = this._webMenu.Site;
			this._okButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this._okButton.FlatStyle = FlatStyle.System;
			this._okButton.Location = new Point(309, 296);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new Size(75, 23);
			this._okButton.TabIndex = 9;
			this._okButton.Text = SR.GetString("MenuItemCollectionEditor_OK");
			this._okButton.Click += this.OnOkButtonClick;
			this._cancelButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this._cancelButton.FlatStyle = FlatStyle.System;
			this._cancelButton.Location = new Point(389, 296);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new Size(75, 23);
			this._cancelButton.TabIndex = 10;
			this._cancelButton.Text = SR.GetString("MenuItemCollectionEditor_Cancel");
			this._cancelButton.Click += this.OnCancelButtonClick;
			this._propertiesLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this._propertiesLabel.Location = new Point(260, 12);
			this._propertiesLabel.Name = "_propertiesLabel";
			this._propertiesLabel.Size = new Size(204, 14);
			this._propertiesLabel.TabIndex = 2;
			this._propertiesLabel.Text = SR.GetString("MenuItemCollectionEditor_Properties");
			this._nodesLabel.Location = new Point(12, 12);
			this._nodesLabel.Name = "_nodesLabel";
			this._nodesLabel.Size = new Size(100, 14);
			this._nodesLabel.TabIndex = 0;
			this._nodesLabel.Text = SR.GetString("MenuItemCollectionEditor_Nodes");
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
			this.Text = SR.GetString("MenuItemCollectionEditor_Title");
			this._treeViewPanel.ResumeLayout(false);
			base.InitializeForm();
			base.ResumeLayout(false);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0002C375 File Offset: 0x0002A575
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.Menu.CollectionEditor";
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002C37C File Offset: 0x0002A57C
		private void LoadNodes(System.Windows.Forms.TreeNodeCollection destNodes, MenuItemCollection sourceNodes)
		{
			foreach (object obj in sourceNodes)
			{
				System.Web.UI.WebControls.MenuItem menuItem = (System.Web.UI.WebControls.MenuItem)obj;
				MenuItemCollectionEditorDialog.MenuItemContainer menuItemContainer = new MenuItemCollectionEditorDialog.MenuItemContainer();
				destNodes.Add(menuItemContainer);
				menuItemContainer.Text = menuItem.Text;
				System.Web.UI.WebControls.MenuItem menuItem2 = (System.Web.UI.WebControls.MenuItem)((ICloneable)menuItem).Clone();
				this._menuDesigner.RegisterClone(menuItem, menuItem2);
				menuItemContainer.WebMenuItem = menuItem2;
				if (menuItem.ChildItems.Count > 0)
				{
					this.LoadNodes(menuItemContainer.Nodes, menuItem.ChildItems);
				}
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002C428 File Offset: 0x0002A628
		private void OnAddChildButtonClick()
		{
			this.ValidatePropertyGrid();
			System.Windows.Forms.TreeNode selectedNode = this._treeView.SelectedNode;
			if (selectedNode != null)
			{
				MenuItemCollectionEditorDialog.MenuItemContainer menuItemContainer = new MenuItemCollectionEditorDialog.MenuItemContainer();
				selectedNode.Nodes.Add(menuItemContainer);
				string @string = SR.GetString("MenuItemCollectionEditor_NewNodeText");
				menuItemContainer.Text = @string;
				menuItemContainer.WebMenuItem.Text = @string;
				selectedNode.Expand();
				this._treeView.SelectedNode = menuItemContainer;
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0002C490 File Offset: 0x0002A690
		private void OnAddRootButtonClick()
		{
			this.ValidatePropertyGrid();
			MenuItemCollectionEditorDialog.MenuItemContainer menuItemContainer = new MenuItemCollectionEditorDialog.MenuItemContainer();
			this._treeView.Nodes.Add(menuItemContainer);
			string @string = SR.GetString("MenuItemCollectionEditor_NewNodeText");
			menuItemContainer.Text = @string;
			menuItemContainer.WebMenuItem.Text = @string;
			this._treeView.SelectedNode = menuItemContainer;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002C4E8 File Offset: 0x0002A6E8
		private void OnIndentButtonClick()
		{
			this.ValidatePropertyGrid();
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

		// Token: 0x060007FC RID: 2044 RVA: 0x0002C534 File Offset: 0x0002A734
		protected override void OnInitialActivated(EventArgs e)
		{
			base.OnInitialActivated(e);
			this.LoadNodes(this._treeView.Nodes, this._webMenu.Items);
			if (this._treeView.Nodes.Count > 0)
			{
				this._treeView.SelectedNode = this._treeView.Nodes[0];
			}
			this.UpdateEnabledState();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002C599 File Offset: 0x0002A799
		private void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				this._propertyGrid.SelectedObject = ((MenuItemCollectionEditorDialog.MenuItemContainer)e.Node).WebMenuItem;
			}
			else
			{
				this._propertyGrid.SelectedObject = null;
			}
			this.UpdateEnabledState();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002C5D4 File Offset: 0x0002A7D4
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

		// Token: 0x060007FF RID: 2047 RVA: 0x0002C680 File Offset: 0x0002A880
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

		// Token: 0x06000800 RID: 2048 RVA: 0x0002C720 File Offset: 0x0002A920
		private void OnMoveDownButtonClick()
		{
			this.ValidatePropertyGrid();
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

		// Token: 0x06000801 RID: 2049 RVA: 0x0002C790 File Offset: 0x0002A990
		private void OnMoveUpButtonClick()
		{
			this.ValidatePropertyGrid();
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

		// Token: 0x06000802 RID: 2050 RVA: 0x0002C7FB File Offset: 0x0002A9FB
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			this.ValidatePropertyGrid();
			this.SaveNodes(this._webMenu.Items, this._treeView.Nodes);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0002C82C File Offset: 0x0002AA2C
		private void OnPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			this.ValidatePropertyGrid();
			MenuItemCollectionEditorDialog.MenuItemContainer menuItemContainer = (MenuItemCollectionEditorDialog.MenuItemContainer)this._treeView.SelectedNode;
			if (menuItemContainer != null)
			{
				menuItemContainer.Text = menuItemContainer.WebMenuItem.Text;
			}
			this._propertyGrid.Refresh();
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0002C870 File Offset: 0x0002AA70
		private void OnRemoveButtonClick()
		{
			this.ValidatePropertyGrid();
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

		// Token: 0x06000805 RID: 2053 RVA: 0x0002C928 File Offset: 0x0002AB28
		private void OnUnindentButtonClick()
		{
			this.ValidatePropertyGrid();
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

		// Token: 0x06000806 RID: 2054 RVA: 0x0002C998 File Offset: 0x0002AB98
		private void SaveNodes(MenuItemCollection destNodes, System.Windows.Forms.TreeNodeCollection sourceNodes)
		{
			this.ValidatePropertyGrid();
			destNodes.Clear();
			foreach (object obj in sourceNodes)
			{
				MenuItemCollectionEditorDialog.MenuItemContainer menuItemContainer = (MenuItemCollectionEditorDialog.MenuItemContainer)obj;
				System.Web.UI.WebControls.MenuItem webMenuItem = menuItemContainer.WebMenuItem;
				destNodes.Add(webMenuItem);
				if (menuItemContainer.Nodes.Count > 0)
				{
					this.SaveNodes(webMenuItem.ChildItems, menuItemContainer.Nodes);
				}
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0002CA20 File Offset: 0x0002AC20
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

		// Token: 0x06000808 RID: 2056 RVA: 0x0002CAF0 File Offset: 0x0002ACF0
		private void ValidatePropertyGrid()
		{
			MenuItemCollectionEditorDialog.MenuItemContainer menuItemContainer = (MenuItemCollectionEditorDialog.MenuItemContainer)this._treeView.SelectedNode;
			if (menuItemContainer != null)
			{
				menuItemContainer.Text = menuItemContainer.WebMenuItem.Text;
				if (menuItemContainer.WebMenuItem.Selected && (!menuItemContainer.WebMenuItem.Selectable || !menuItemContainer.WebMenuItem.Enabled))
				{
					UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("MenuItemCollectionEditor_CantSelect"));
					menuItemContainer.WebMenuItem.Selected = false;
					this._propertyGrid.Refresh();
				}
			}
		}

		// Token: 0x040004AF RID: 1199
		private System.Windows.Forms.Panel _treeViewPanel;

		// Token: 0x040004B0 RID: 1200
		private System.Windows.Forms.TreeView _treeView;

		// Token: 0x040004B1 RID: 1201
		private PropertyGrid _propertyGrid;

		// Token: 0x040004B2 RID: 1202
		private System.Windows.Forms.Button _okButton;

		// Token: 0x040004B3 RID: 1203
		private System.Windows.Forms.Button _cancelButton;

		// Token: 0x040004B4 RID: 1204
		private System.Windows.Forms.Label _propertiesLabel;

		// Token: 0x040004B5 RID: 1205
		private System.Windows.Forms.Label _nodesLabel;

		// Token: 0x040004B6 RID: 1206
		private ToolStripButton _addRootButton;

		// Token: 0x040004B7 RID: 1207
		private ToolStripButton _addChildButton;

		// Token: 0x040004B8 RID: 1208
		private ToolStripButton _removeButton;

		// Token: 0x040004B9 RID: 1209
		private ToolStripButton _moveUpButton;

		// Token: 0x040004BA RID: 1210
		private ToolStripButton _moveDownButton;

		// Token: 0x040004BB RID: 1211
		private ToolStripButton _indentButton;

		// Token: 0x040004BC RID: 1212
		private ToolStripButton _unindentButton;

		// Token: 0x040004BD RID: 1213
		private ToolStripSeparator _toolBarSeparator;

		// Token: 0x040004BE RID: 1214
		private ToolStrip _treeViewToolBar;

		// Token: 0x040004BF RID: 1215
		private System.Web.UI.WebControls.Menu _webMenu;

		// Token: 0x040004C0 RID: 1216
		private MenuDesigner _menuDesigner;

		// Token: 0x02000413 RID: 1043
		private class MenuItemContainer : System.Windows.Forms.TreeNode
		{
			// Token: 0x17000862 RID: 2146
			// (get) Token: 0x0600280B RID: 10251 RVA: 0x000F4EDF File Offset: 0x000F30DF
			// (set) Token: 0x0600280C RID: 10252 RVA: 0x000F4EFA File Offset: 0x000F30FA
			public System.Web.UI.WebControls.MenuItem WebMenuItem
			{
				get
				{
					if (this._webMenuNode == null)
					{
						this._webMenuNode = new System.Web.UI.WebControls.MenuItem();
					}
					return this._webMenuNode;
				}
				set
				{
					this._webMenuNode = value;
				}
			}

			// Token: 0x04001C89 RID: 7305
			private System.Web.UI.WebControls.MenuItem _webMenuNode;
		}
	}
}
