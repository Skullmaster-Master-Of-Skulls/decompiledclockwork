using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000366 RID: 870
	internal class TreeNodeCollectionEditor : CollectionEditor
	{
		// Token: 0x060023C9 RID: 9161 RVA: 0x000DFE2D File Offset: 0x000DE02D
		public TreeNodeCollectionEditor() : base(typeof(TreeNodeCollection))
		{
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000DFE3F File Offset: 0x000DE03F
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			return new TreeNodeCollectionEditor.TreeNodeCollectionForm(this);
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x000DFE47 File Offset: 0x000DE047
		protected override string HelpTopic
		{
			get
			{
				return "net.ComponentModel.TreeNodeCollectionEditor";
			}
		}

		// Token: 0x020005A0 RID: 1440
		private class TreeNodeCollectionForm : CollectionEditor.CollectionForm
		{
			// Token: 0x0600336E RID: 13166 RVA: 0x00119388 File Offset: 0x00117588
			public TreeNodeCollectionForm(CollectionEditor editor) : base(editor)
			{
				this.editor = (TreeNodeCollectionEditor)editor;
				this.InitializeComponent();
				this.HookEvents();
				this.intialNextNode = this.NextNode;
				this.SetButtonsState();
				DesignerUtils.ApplyTreeViewThemeStyles(this.treeView1);
				if (DpiHelper.IsScalingRequired)
				{
					DpiHelper.ScaleButtonImageLogicalToDevice(this.moveDownButton);
					DpiHelper.ScaleButtonImageLogicalToDevice(this.moveUpButton);
					DpiHelper.ScaleButtonImageLogicalToDevice(this.btnDelete);
				}
			}

			// Token: 0x17000A09 RID: 2569
			// (get) Token: 0x0600336F RID: 13167 RVA: 0x001193FC File Offset: 0x001175FC
			private TreeNode LastNode
			{
				get
				{
					TreeNode treeNode = this.treeView1.Nodes[this.treeView1.Nodes.Count - 1];
					while (treeNode.Nodes.Count > 0)
					{
						treeNode = treeNode.Nodes[treeNode.Nodes.Count - 1];
					}
					return treeNode;
				}
			}

			// Token: 0x17000A0A RID: 2570
			// (get) Token: 0x06003370 RID: 13168 RVA: 0x00119456 File Offset: 0x00117656
			private TreeView TreeView
			{
				get
				{
					if (base.Context != null && base.Context.Instance is TreeView)
					{
						return (TreeView)base.Context.Instance;
					}
					return null;
				}
			}

			// Token: 0x17000A0B RID: 2571
			// (get) Token: 0x06003371 RID: 13169 RVA: 0x00119484 File Offset: 0x00117684
			// (set) Token: 0x06003372 RID: 13170 RVA: 0x00119504 File Offset: 0x00117704
			private int NextNode
			{
				get
				{
					if (this.TreeView != null && this.TreeView.Site != null)
					{
						IDictionaryService dictionaryService = (IDictionaryService)this.TreeView.Site.GetService(typeof(IDictionaryService));
						if (dictionaryService != null)
						{
							object value = dictionaryService.GetValue(TreeNodeCollectionEditor.TreeNodeCollectionForm.NextNodeKey);
							if (value != null)
							{
								this.nextNode = (int)value;
							}
							else
							{
								this.nextNode = 0;
								dictionaryService.SetValue(TreeNodeCollectionEditor.TreeNodeCollectionForm.NextNodeKey, 0);
							}
						}
					}
					return this.nextNode;
				}
				set
				{
					this.nextNode = value;
					if (this.TreeView != null && this.TreeView.Site != null)
					{
						IDictionaryService dictionaryService = (IDictionaryService)this.TreeView.Site.GetService(typeof(IDictionaryService));
						if (dictionaryService != null)
						{
							dictionaryService.SetValue(TreeNodeCollectionEditor.TreeNodeCollectionForm.NextNodeKey, this.nextNode);
						}
					}
				}
			}

			// Token: 0x06003373 RID: 13171 RVA: 0x00119568 File Offset: 0x00117768
			private void Add(TreeNode parent)
			{
				string @string = SR.GetString("BaseNodeName");
				TreeNode treeNode;
				if (parent == null)
				{
					TreeNodeCollection nodes = this.treeView1.Nodes;
					string str = @string;
					int num = this.NextNode;
					this.NextNode = num + 1;
					treeNode = nodes.Add(str + num.ToString(CultureInfo.InvariantCulture));
					treeNode.Name = treeNode.Text;
				}
				else
				{
					TreeNodeCollection nodes2 = parent.Nodes;
					string str2 = @string;
					int num = this.NextNode;
					this.NextNode = num + 1;
					treeNode = nodes2.Add(str2 + num.ToString(CultureInfo.InvariantCulture));
					treeNode.Name = treeNode.Text;
					parent.Expand();
				}
				if (parent != null)
				{
					this.treeView1.SelectedNode = parent;
					return;
				}
				this.treeView1.SelectedNode = treeNode;
				this.SetNodeProps(treeNode);
			}

			// Token: 0x06003374 RID: 13172 RVA: 0x0011962C File Offset: 0x0011782C
			private void HookEvents()
			{
				this.okButton.Click += this.BtnOK_click;
				this.btnCancel.Click += this.BtnCancel_click;
				this.btnAddChild.Click += this.BtnAddChild_click;
				this.btnAddRoot.Click += this.BtnAddRoot_click;
				this.btnDelete.Click += this.BtnDelete_click;
				this.propertyGrid1.PropertyValueChanged += this.PropertyGrid_propertyValueChanged;
				this.treeView1.AfterSelect += this.treeView1_afterSelect;
				this.treeView1.DragEnter += this.treeView1_DragEnter;
				this.treeView1.ItemDrag += this.treeView1_ItemDrag;
				this.treeView1.DragDrop += this.treeView1_DragDrop;
				this.treeView1.DragOver += this.treeView1_DragOver;
				base.HelpButtonClicked += this.TreeNodeCollectionEditor_HelpButtonClicked;
				this.moveDownButton.Click += this.moveDownButton_Click;
				this.moveUpButton.Click += this.moveUpButton_Click;
			}

			// Token: 0x06003375 RID: 13173 RVA: 0x00119778 File Offset: 0x00117978
			private void InitializeComponent()
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TreeNodeCollectionEditor));
				this.okCancelPanel = new TableLayoutPanel();
				this.okButton = new Button();
				this.btnCancel = new Button();
				this.nodeControlPanel = new TableLayoutPanel();
				this.btnAddRoot = new Button();
				this.btnAddChild = new Button();
				this.btnDelete = new Button();
				this.moveDownButton = new Button();
				this.moveUpButton = new Button();
				this.propertyGrid1 = new VsPropertyGrid(base.Context);
				this.label2 = new Label();
				this.treeView1 = new TreeView();
				this.label1 = new Label();
				this.overarchingTableLayoutPanel = new TableLayoutPanel();
				this.navigationButtonsTableLayoutPanel = new TableLayoutPanel();
				this.okCancelPanel.SuspendLayout();
				this.nodeControlPanel.SuspendLayout();
				this.overarchingTableLayoutPanel.SuspendLayout();
				this.navigationButtonsTableLayoutPanel.SuspendLayout();
				base.SuspendLayout();
				componentResourceManager.ApplyResources(this.okCancelPanel, "okCancelPanel");
				this.okCancelPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.okCancelPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.okCancelPanel.Controls.Add(this.okButton, 0, 0);
				this.okCancelPanel.Controls.Add(this.btnCancel, 1, 0);
				this.okCancelPanel.Margin = new Padding(3, 3, 0, 0);
				this.okCancelPanel.Name = "okCancelPanel";
				this.okCancelPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.okButton, "okButton");
				this.okButton.DialogResult = DialogResult.OK;
				this.okButton.Margin = new Padding(0, 0, 3, 0);
				this.okButton.Name = "okButton";
				componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
				this.btnCancel.DialogResult = DialogResult.Cancel;
				this.btnCancel.Margin = new Padding(3, 0, 0, 0);
				this.btnCancel.Name = "btnCancel";
				componentResourceManager.ApplyResources(this.nodeControlPanel, "nodeControlPanel");
				this.nodeControlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.nodeControlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.nodeControlPanel.Controls.Add(this.btnAddRoot, 0, 0);
				this.nodeControlPanel.Controls.Add(this.btnAddChild, 1, 0);
				this.nodeControlPanel.Margin = new Padding(0, 3, 3, 3);
				this.nodeControlPanel.Name = "nodeControlPanel";
				this.nodeControlPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.btnAddRoot, "btnAddRoot");
				this.btnAddRoot.Margin = new Padding(0, 0, 3, 0);
				this.btnAddRoot.Name = "btnAddRoot";
				componentResourceManager.ApplyResources(this.btnAddChild, "btnAddChild");
				this.btnAddChild.Margin = new Padding(3, 0, 0, 0);
				this.btnAddChild.Name = "btnAddChild";
				componentResourceManager.ApplyResources(this.btnDelete, "btnDelete");
				this.btnDelete.Margin = new Padding(0, 3, 0, 0);
				this.btnDelete.Name = "btnDelete";
				componentResourceManager.ApplyResources(this.moveDownButton, "moveDownButton");
				this.moveDownButton.Margin = new Padding(0, 1, 0, 3);
				this.moveDownButton.Name = "moveDownButton";
				componentResourceManager.ApplyResources(this.moveUpButton, "moveUpButton");
				this.moveUpButton.Margin = new Padding(0, 0, 0, 1);
				this.moveUpButton.Name = "moveUpButton";
				componentResourceManager.ApplyResources(this.propertyGrid1, "propertyGrid1");
				this.propertyGrid1.LineColor = SystemColors.ScrollBar;
				this.propertyGrid1.Margin = new Padding(3, 3, 0, 3);
				this.propertyGrid1.Name = "propertyGrid1";
				this.overarchingTableLayoutPanel.SetRowSpan(this.propertyGrid1, 2);
				componentResourceManager.ApplyResources(this.label2, "label2");
				this.label2.Margin = new Padding(3, 1, 0, 0);
				this.label2.Name = "label2";
				this.treeView1.AllowDrop = true;
				componentResourceManager.ApplyResources(this.treeView1, "treeView1");
				this.treeView1.HideSelection = false;
				this.treeView1.Margin = new Padding(0, 3, 3, 3);
				this.treeView1.Name = "treeView1";
				componentResourceManager.ApplyResources(this.label1, "label1");
				this.label1.Margin = new Padding(0, 1, 3, 0);
				this.label1.Name = "label1";
				componentResourceManager.ApplyResources(this.overarchingTableLayoutPanel, "overarchingTableLayoutPanel");
				this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
				this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
				this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
				this.overarchingTableLayoutPanel.Controls.Add(this.navigationButtonsTableLayoutPanel, 1, 1);
				this.overarchingTableLayoutPanel.Controls.Add(this.label2, 2, 0);
				this.overarchingTableLayoutPanel.Controls.Add(this.propertyGrid1, 2, 1);
				this.overarchingTableLayoutPanel.Controls.Add(this.treeView1, 0, 1);
				this.overarchingTableLayoutPanel.Controls.Add(this.label1, 0, 0);
				this.overarchingTableLayoutPanel.Controls.Add(this.nodeControlPanel, 0, 2);
				this.overarchingTableLayoutPanel.Controls.Add(this.okCancelPanel, 2, 3);
				this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
				this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
				this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
				this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
				this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.navigationButtonsTableLayoutPanel, "navigationButtonsTableLayoutPanel");
				this.navigationButtonsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
				this.navigationButtonsTableLayoutPanel.Controls.Add(this.moveUpButton, 0, 0);
				this.navigationButtonsTableLayoutPanel.Controls.Add(this.btnDelete, 0, 2);
				this.navigationButtonsTableLayoutPanel.Controls.Add(this.moveDownButton, 0, 1);
				this.navigationButtonsTableLayoutPanel.Margin = new Padding(3, 3, 18, 3);
				this.navigationButtonsTableLayoutPanel.Name = "navigationButtonsTableLayoutPanel";
				this.navigationButtonsTableLayoutPanel.RowStyles.Add(new RowStyle());
				this.navigationButtonsTableLayoutPanel.RowStyles.Add(new RowStyle());
				this.navigationButtonsTableLayoutPanel.RowStyles.Add(new RowStyle());
				base.AcceptButton = this.okButton;
				componentResourceManager.ApplyResources(this, "$this");
				base.AutoScaleMode = AutoScaleMode.Font;
				base.CancelButton = this.btnCancel;
				base.Controls.Add(this.overarchingTableLayoutPanel);
				base.HelpButton = true;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.Name = "TreeNodeCollectionEditor";
				base.ShowIcon = false;
				base.ShowInTaskbar = false;
				base.SizeGripStyle = SizeGripStyle.Show;
				this.okCancelPanel.ResumeLayout(false);
				this.okCancelPanel.PerformLayout();
				this.nodeControlPanel.ResumeLayout(false);
				this.nodeControlPanel.PerformLayout();
				this.overarchingTableLayoutPanel.ResumeLayout(false);
				this.overarchingTableLayoutPanel.PerformLayout();
				this.navigationButtonsTableLayoutPanel.ResumeLayout(false);
				base.ResumeLayout(false);
			}

			// Token: 0x06003376 RID: 13174 RVA: 0x00119F80 File Offset: 0x00118180
			protected override void OnEditValueChanged()
			{
				if (base.EditValue != null)
				{
					object[] items = base.Items;
					this.propertyGrid1.Site = new CollectionEditor.PropertyGridSite(base.Context, this.propertyGrid1);
					TreeNode[] array = new TreeNode[items.Length];
					for (int i = 0; i < items.Length; i++)
					{
						array[i] = (TreeNode)((TreeNode)items[i]).Clone();
					}
					this.treeView1.Nodes.Clear();
					this.treeView1.Nodes.AddRange(array);
					this.curNode = null;
					this.btnAddChild.Enabled = false;
					this.btnDelete.Enabled = false;
					TreeView treeView = this.TreeView;
					if (treeView != null)
					{
						this.SetImageProps(treeView);
					}
					if (items.Length != 0 && array[0] != null)
					{
						this.treeView1.SelectedNode = array[0];
					}
				}
			}

			// Token: 0x06003377 RID: 13175 RVA: 0x0011A04F File Offset: 0x0011824F
			private void PropertyGrid_propertyValueChanged(object sender, PropertyValueChangedEventArgs e)
			{
				this.label2.Text = SR.GetString("CollectionEditorProperties", new object[]
				{
					this.treeView1.SelectedNode.Text
				});
			}

			// Token: 0x06003378 RID: 13176 RVA: 0x0011A080 File Offset: 0x00118280
			private void SetImageProps(TreeView actualTreeView)
			{
				if (actualTreeView.ImageList != null)
				{
					this.treeView1.ImageList = actualTreeView.ImageList;
					this.treeView1.ImageIndex = actualTreeView.ImageIndex;
					this.treeView1.SelectedImageIndex = actualTreeView.SelectedImageIndex;
				}
				else
				{
					this.treeView1.ImageList = null;
					this.treeView1.ImageIndex = -1;
					this.treeView1.SelectedImageIndex = -1;
				}
				if (actualTreeView.StateImageList != null)
				{
					this.treeView1.StateImageList = actualTreeView.StateImageList;
				}
				else
				{
					this.treeView1.StateImageList = null;
				}
				this.treeView1.CheckBoxes = actualTreeView.CheckBoxes;
			}

			// Token: 0x06003379 RID: 13177 RVA: 0x0011A128 File Offset: 0x00118328
			private void SetNodeProps(TreeNode node)
			{
				if (node != null)
				{
					this.label2.Text = SR.GetString("CollectionEditorProperties", new object[]
					{
						node.Name.ToString()
					});
				}
				else
				{
					this.label2.Text = SR.GetString("CollectionEditorPropertiesNone");
				}
				this.propertyGrid1.SelectedObject = node;
			}

			// Token: 0x0600337A RID: 13178 RVA: 0x0011A184 File Offset: 0x00118384
			private void treeView1_afterSelect(object sender, TreeViewEventArgs e)
			{
				this.curNode = e.Node;
				this.SetNodeProps(this.curNode);
				this.SetButtonsState();
			}

			// Token: 0x0600337B RID: 13179 RVA: 0x0011A1A4 File Offset: 0x001183A4
			private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
			{
				TreeNode data = (TreeNode)e.Item;
				base.DoDragDrop(data, DragDropEffects.Move);
			}

			// Token: 0x0600337C RID: 13180 RVA: 0x0011A1C6 File Offset: 0x001183C6
			private void treeView1_DragEnter(object sender, DragEventArgs e)
			{
				if (e.Data.GetDataPresent(typeof(TreeNode)))
				{
					e.Effect = DragDropEffects.Move;
					return;
				}
				e.Effect = DragDropEffects.None;
			}

			// Token: 0x0600337D RID: 13181 RVA: 0x0011A1F0 File Offset: 0x001183F0
			private void treeView1_DragDrop(object sender, DragEventArgs e)
			{
				TreeNode treeNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
				Point point = new Point(0, 0);
				point.X = e.X;
				point.Y = e.Y;
				point = this.treeView1.PointToClient(point);
				TreeNode nodeAt = this.treeView1.GetNodeAt(point);
				if (treeNode != nodeAt)
				{
					this.treeView1.Nodes.Remove(treeNode);
					if (nodeAt != null && !this.CheckParent(nodeAt, treeNode))
					{
						nodeAt.Nodes.Add(treeNode);
						return;
					}
					this.treeView1.Nodes.Add(treeNode);
				}
			}

			// Token: 0x0600337E RID: 13182 RVA: 0x0011A297 File Offset: 0x00118497
			private bool CheckParent(TreeNode child, TreeNode parent)
			{
				while (child != null)
				{
					if (parent == child.Parent)
					{
						return true;
					}
					child = child.Parent;
				}
				return false;
			}

			// Token: 0x0600337F RID: 13183 RVA: 0x0011A2B4 File Offset: 0x001184B4
			private void treeView1_DragOver(object sender, DragEventArgs e)
			{
				Point point = new Point(0, 0);
				point.X = e.X;
				point.Y = e.Y;
				point = this.treeView1.PointToClient(point);
				TreeNode nodeAt = this.treeView1.GetNodeAt(point);
				this.treeView1.SelectedNode = nodeAt;
			}

			// Token: 0x06003380 RID: 13184 RVA: 0x0011A30A File Offset: 0x0011850A
			private void BtnAddChild_click(object sender, EventArgs e)
			{
				this.Add(this.curNode);
				this.SetButtonsState();
			}

			// Token: 0x06003381 RID: 13185 RVA: 0x0011A31E File Offset: 0x0011851E
			private void BtnAddRoot_click(object sender, EventArgs e)
			{
				this.Add(null);
				this.SetButtonsState();
			}

			// Token: 0x06003382 RID: 13186 RVA: 0x0011A32D File Offset: 0x0011852D
			private void BtnDelete_click(object sender, EventArgs e)
			{
				this.curNode.Remove();
				if (this.treeView1.Nodes.Count == 0)
				{
					this.curNode = null;
					this.SetNodeProps(null);
				}
				this.SetButtonsState();
			}

			// Token: 0x06003383 RID: 13187 RVA: 0x0011A360 File Offset: 0x00118560
			private void BtnOK_click(object sender, EventArgs e)
			{
				object[] array = new object[this.treeView1.Nodes.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.treeView1.Nodes[i].Clone();
				}
				base.Items = array;
				this.treeView1.Dispose();
				this.treeView1 = null;
			}

			// Token: 0x06003384 RID: 13188 RVA: 0x0011A3C4 File Offset: 0x001185C4
			private void moveDownButton_Click(object sender, EventArgs e)
			{
				TreeNode treeNode = this.curNode;
				TreeNode parent = this.curNode.Parent;
				if (parent == null)
				{
					this.treeView1.Nodes.RemoveAt(treeNode.Index);
					this.treeView1.Nodes[treeNode.Index].Nodes.Insert(0, treeNode);
				}
				else
				{
					parent.Nodes.RemoveAt(treeNode.Index);
					if (treeNode.Index < parent.Nodes.Count)
					{
						parent.Nodes[treeNode.Index].Nodes.Insert(0, treeNode);
					}
					else if (parent.Parent == null)
					{
						this.treeView1.Nodes.Insert(parent.Index + 1, treeNode);
					}
					else
					{
						parent.Parent.Nodes.Insert(parent.Index + 1, treeNode);
					}
				}
				this.treeView1.SelectedNode = treeNode;
				this.curNode = treeNode;
			}

			// Token: 0x06003385 RID: 13189 RVA: 0x0011A4B4 File Offset: 0x001186B4
			private void moveUpButton_Click(object sender, EventArgs e)
			{
				TreeNode treeNode = this.curNode;
				TreeNode parent = this.curNode.Parent;
				if (parent == null)
				{
					this.treeView1.Nodes.RemoveAt(treeNode.Index);
					this.treeView1.Nodes[treeNode.Index - 1].Nodes.Add(treeNode);
				}
				else
				{
					parent.Nodes.RemoveAt(treeNode.Index);
					if (treeNode.Index == 0)
					{
						if (parent.Parent == null)
						{
							this.treeView1.Nodes.Insert(parent.Index, treeNode);
						}
						else
						{
							parent.Parent.Nodes.Insert(parent.Index, treeNode);
						}
					}
					else
					{
						parent.Nodes[treeNode.Index - 1].Nodes.Add(treeNode);
					}
				}
				this.treeView1.SelectedNode = treeNode;
				this.curNode = treeNode;
			}

			// Token: 0x06003386 RID: 13190 RVA: 0x0011A598 File Offset: 0x00118798
			private void SetButtonsState()
			{
				bool flag = this.treeView1.Nodes.Count > 0;
				this.btnAddChild.Enabled = flag;
				this.btnDelete.Enabled = flag;
				this.moveDownButton.Enabled = (flag && (this.curNode != this.LastNode || this.curNode.Level > 0) && this.curNode != this.treeView1.Nodes[this.treeView1.Nodes.Count - 1]);
				this.moveUpButton.Enabled = (flag && this.curNode != this.treeView1.Nodes[0]);
			}

			// Token: 0x06003387 RID: 13191 RVA: 0x0011A658 File Offset: 0x00118858
			private void TreeNodeCollectionEditor_HelpButtonClicked(object sender, CancelEventArgs e)
			{
				e.Cancel = true;
				this.editor.ShowHelp();
			}

			// Token: 0x06003388 RID: 13192 RVA: 0x0011A66C File Offset: 0x0011886C
			private void BtnCancel_click(object sender, EventArgs e)
			{
				if (this.NextNode != this.intialNextNode)
				{
					this.NextNode = this.intialNextNode;
				}
			}

			// Token: 0x0400226D RID: 8813
			private int nextNode;

			// Token: 0x0400226E RID: 8814
			private TreeNode curNode;

			// Token: 0x0400226F RID: 8815
			private TreeNodeCollectionEditor editor;

			// Token: 0x04002270 RID: 8816
			private Button okButton;

			// Token: 0x04002271 RID: 8817
			private Button btnCancel;

			// Token: 0x04002272 RID: 8818
			private Button btnAddChild;

			// Token: 0x04002273 RID: 8819
			private Button btnAddRoot;

			// Token: 0x04002274 RID: 8820
			private Button btnDelete;

			// Token: 0x04002275 RID: 8821
			private Button moveDownButton;

			// Token: 0x04002276 RID: 8822
			private Button moveUpButton;

			// Token: 0x04002277 RID: 8823
			private Label label1;

			// Token: 0x04002278 RID: 8824
			private TreeView treeView1;

			// Token: 0x04002279 RID: 8825
			private Label label2;

			// Token: 0x0400227A RID: 8826
			private VsPropertyGrid propertyGrid1;

			// Token: 0x0400227B RID: 8827
			private TableLayoutPanel okCancelPanel;

			// Token: 0x0400227C RID: 8828
			private TableLayoutPanel nodeControlPanel;

			// Token: 0x0400227D RID: 8829
			private TableLayoutPanel overarchingTableLayoutPanel;

			// Token: 0x0400227E RID: 8830
			private TableLayoutPanel navigationButtonsTableLayoutPanel;

			// Token: 0x0400227F RID: 8831
			private static object NextNodeKey = new object();

			// Token: 0x04002280 RID: 8832
			private int intialNextNode;
		}
	}
}
