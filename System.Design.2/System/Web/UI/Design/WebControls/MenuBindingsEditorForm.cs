using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E6 RID: 230
	internal partial class MenuBindingsEditorForm : DesignerForm
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x0002A1AC File Offset: 0x000283AC
		public MenuBindingsEditorForm(IServiceProvider serviceProvider, System.Web.UI.WebControls.Menu menu, MenuDesigner menuDesigner) : base(serviceProvider)
		{
			this._menu = menu;
			this.InitializeComponent();
			this.InitializeUI();
			foreach (object obj in this._menu.DataBindings)
			{
				MenuItemBinding menuItemBinding = (MenuItemBinding)obj;
				MenuItemBinding menuItemBinding2 = (MenuItemBinding)((ICloneable)menuItemBinding).Clone();
				menuDesigner.RegisterClone(menuItemBinding, menuItemBinding2);
				this._bindingsListView.Items.Add(menuItemBinding2);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0002A244 File Offset: 0x00028444
		private IDataSourceSchema Schema
		{
			get
			{
				if (this._schema == null)
				{
					IDesignerHost designerHost = (IDesignerHost)base.ServiceProvider.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						HierarchicalDataBoundControlDesigner hierarchicalDataBoundControlDesigner = designerHost.GetDesigner(this._menu) as HierarchicalDataBoundControlDesigner;
						if (hierarchicalDataBoundControlDesigner != null)
						{
							DesignerHierarchicalDataSourceView designerView = hierarchicalDataBoundControlDesigner.DesignerView;
							if (designerView != null)
							{
								try
								{
									this._schema = designerView.Schema;
								}
								catch (Exception ex)
								{
									IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)base.ServiceProvider.GetService(typeof(IComponentDesignerDebugService));
									if (componentDesignerDebugService != null)
									{
										componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
										{
											"DesignerHierarchicalDataSourceView.Schema",
											ex.Message
										}));
									}
								}
							}
						}
					}
				}
				return this._schema;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002A308 File Offset: 0x00028508
		private void AddBinding()
		{
			System.Windows.Forms.TreeNode selectedNode = this._schemaTreeView.SelectedNode;
			if (selectedNode != null)
			{
				MenuItemBinding menuItemBinding = new MenuItemBinding();
				if (selectedNode.Text != this._schemaTreeView.Nodes[0].Text)
				{
					menuItemBinding.DataMember = selectedNode.Text;
					if (((MenuBindingsEditorForm.SchemaTreeNode)selectedNode).Duplicate)
					{
						menuItemBinding.Depth = selectedNode.FullPath.Split(new char[]
						{
							this._schemaTreeView.PathSeparator[0]
						}).Length - 1;
					}
					((IDataSourceViewSchemaAccessor)menuItemBinding).DataSourceViewSchema = ((MenuBindingsEditorForm.SchemaTreeNode)selectedNode).Schema;
					int num = this._bindingsListView.Items.IndexOf(menuItemBinding);
					if (num == -1)
					{
						this._bindingsListView.Items.Add(menuItemBinding);
						this._bindingsListView.SetSelected(this._bindingsListView.Items.Count - 1, true);
					}
					else
					{
						menuItemBinding = (MenuItemBinding)this._bindingsListView.Items[num];
						this._bindingsListView.SetSelected(num, true);
					}
				}
				else
				{
					this._bindingsListView.Items.Add(menuItemBinding);
					this._bindingsListView.SetSelected(this._bindingsListView.Items.Count - 1, true);
				}
				this._propertyGrid.SelectedObject = menuItemBinding;
				this._propertyGrid.Refresh();
				this.UpdateEnabledStates();
			}
			this._bindingsListView.Focus();
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0002A473 File Offset: 0x00028673
		private void ApplyBindings()
		{
			ControlDesigner.InvokeTransactedChange(this._menu, new TransactedChangeCallback(this.ApplyBindingsChangeCallback), null, SR.GetString("MenuDesigner_EditBindingsTransactionDescription"));
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0002A498 File Offset: 0x00028698
		private bool ApplyBindingsChangeCallback(object context)
		{
			this._menu.DataBindings.Clear();
			foreach (object obj in this._bindingsListView.Items)
			{
				MenuItemBinding binding = (MenuItemBinding)obj;
				this._menu.DataBindings.Add(binding);
			}
			return true;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0002A533 File Offset: 0x00028733
		private IDataSourceViewSchema FindViewSchema(string viewName, int level)
		{
			return TreeViewBindingsEditorForm.FindViewSchemaRecursive(this.Schema, 0, viewName, level, null);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0002A544 File Offset: 0x00028744
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.Menu.BindingsEditorForm";
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0002AD78 File Offset: 0x00028F78
		private void InitializeUI()
		{
			this._bindingsLabel.Text = SR.GetString("MenuBindingsEditor_Bindings");
			this._schemaLabel.Text = SR.GetString("MenuBindingsEditor_Schema");
			this._okButton.Text = SR.GetString("MenuBindingsEditor_OK");
			this._applyButton.Text = SR.GetString("MenuBindingsEditor_Apply");
			this._cancelButton.Text = SR.GetString("MenuBindingsEditor_Cancel");
			this._propertiesLabel.Text = SR.GetString("MenuBindingsEditor_BindingProperties");
			this._addBindingButton.Text = SR.GetString("MenuBindingsEditor_AddBinding");
			this.Text = SR.GetString("MenuBindingsEditor_Title");
			Bitmap bitmap = BitmapSelector.CreateIcon(typeof(MenuBindingsEditorForm), "SortUp.ico").ToBitmap();
			bitmap.MakeTransparent();
			this._moveBindingUpButton.Image = bitmap;
			this._moveBindingUpButton.AccessibleName = SR.GetString("MenuBindingsEditor_MoveBindingUpName");
			this._moveBindingUpButton.AccessibleDescription = SR.GetString("MenuBindingsEditor_MoveBindingUpDescription");
			Bitmap bitmap2 = BitmapSelector.CreateIcon(typeof(MenuBindingsEditorForm), "SortDown.ico").ToBitmap();
			bitmap2.MakeTransparent();
			this._moveBindingDownButton.Image = bitmap2;
			this._moveBindingDownButton.AccessibleName = SR.GetString("MenuBindingsEditor_MoveBindingDownName");
			this._moveBindingDownButton.AccessibleDescription = SR.GetString("MenuBindingsEditor_MoveBindingDownDescription");
			Bitmap bitmap3 = BitmapSelector.CreateIcon(typeof(MenuBindingsEditorForm), "Delete.ico").ToBitmap();
			bitmap3.MakeTransparent();
			this._deleteBindingButton.Image = bitmap3;
			this._deleteBindingButton.AccessibleName = SR.GetString("MenuBindingsEditor_DeleteBindingName");
			this._deleteBindingButton.AccessibleDescription = SR.GetString("MenuBindingsEditor_DeleteBindingDescription");
			base.Icon = null;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0002AF31 File Offset: 0x00029131
		private void OnApplyButtonClick(object sender, EventArgs e)
		{
			this.ApplyBindings();
			this._applyButton.Enabled = false;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0002AF45 File Offset: 0x00029145
		private void OnAddBindingButtonClick(object sender, EventArgs e)
		{
			this._applyButton.Enabled = true;
			this.AddBinding();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0002AF59 File Offset: 0x00029159
		private void OnBindingsListViewGotFocus(object sender, EventArgs e)
		{
			this.UpdateSelectedBinding();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002AF59 File Offset: 0x00029159
		private void OnBindingsListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateSelectedBinding();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002AF70 File Offset: 0x00029170
		private void OnDeleteBindingButtonClick(object sender, EventArgs e)
		{
			if (this._bindingsListView.SelectedIndices.Count > 0)
			{
				this._applyButton.Enabled = true;
				int num = this._bindingsListView.SelectedIndices[0];
				this._bindingsListView.Items.RemoveAt(num);
				if (num >= this._bindingsListView.Items.Count)
				{
					num--;
				}
				if (num >= 0 && this._bindingsListView.Items.Count > 0)
				{
					this._bindingsListView.SetSelected(num, true);
				}
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0002AFFC File Offset: 0x000291FC
		protected override void OnInitialActivated(EventArgs e)
		{
			base.OnInitialActivated(e);
			System.Windows.Forms.TreeNode selectedNode = this._schemaTreeView.Nodes.Add(SR.GetString("MenuBindingsEditor_EmptyBindingText"));
			if (this.Schema != null)
			{
				this.PopulateSchema(this.Schema);
				this._schemaTreeView.ExpandAll();
			}
			this._schemaTreeView.SelectedNode = selectedNode;
			this.UpdateEnabledStates();
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002B05C File Offset: 0x0002925C
		private void OnMoveBindingUpButtonClick(object sender, EventArgs e)
		{
			if (this._bindingsListView.SelectedIndices.Count > 0)
			{
				this._applyButton.Enabled = true;
				int num = this._bindingsListView.SelectedIndices[0];
				if (num > 0)
				{
					MenuItemBinding item = (MenuItemBinding)this._bindingsListView.Items[num];
					this._bindingsListView.Items.RemoveAt(num);
					this._bindingsListView.Items.Insert(num - 1, item);
					this._bindingsListView.SetSelected(num - 1, true);
				}
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002B0EC File Offset: 0x000292EC
		private void OnMoveBindingDownButtonClick(object sender, EventArgs e)
		{
			if (this._bindingsListView.SelectedIndices.Count > 0)
			{
				this._applyButton.Enabled = true;
				int num = this._bindingsListView.SelectedIndices[0];
				if (num + 1 < this._bindingsListView.Items.Count)
				{
					MenuItemBinding item = (MenuItemBinding)this._bindingsListView.Items[num];
					this._bindingsListView.Items.RemoveAt(num);
					this._bindingsListView.Items.Insert(num + 1, item);
					this._bindingsListView.SetSelected(num + 1, true);
				}
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002B18C File Offset: 0x0002938C
		private void OnOKButtonClick(object sender, EventArgs e)
		{
			try
			{
				this.ApplyBindings();
			}
			finally
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002B1C0 File Offset: 0x000293C0
		private void OnPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			this._applyButton.Enabled = true;
			if (e.ChangedItem.PropertyDescriptor.Name == "DataMember")
			{
				string viewName = (string)e.ChangedItem.Value;
				MenuItemBinding menuItemBinding = (MenuItemBinding)this._bindingsListView.Items[this._bindingsListView.SelectedIndex];
				this._bindingsListView.Items[this._bindingsListView.SelectedIndex] = menuItemBinding;
				this._bindingsListView.Refresh();
				IDataSourceViewSchema dataSourceViewSchema = this.FindViewSchema(viewName, menuItemBinding.Depth);
				if (dataSourceViewSchema != null)
				{
					((IDataSourceViewSchemaAccessor)menuItemBinding).DataSourceViewSchema = dataSourceViewSchema;
				}
				this._propertyGrid.SelectedObject = menuItemBinding;
				this._propertyGrid.Refresh();
				return;
			}
			if (e.ChangedItem.PropertyDescriptor.Name == "Depth")
			{
				int level = (int)e.ChangedItem.Value;
				MenuItemBinding menuItemBinding2 = (MenuItemBinding)this._bindingsListView.Items[this._bindingsListView.SelectedIndex];
				IDataSourceViewSchema dataSourceViewSchema2 = this.FindViewSchema(menuItemBinding2.DataMember, level);
				if (dataSourceViewSchema2 != null)
				{
					((IDataSourceViewSchemaAccessor)menuItemBinding2).DataSourceViewSchema = dataSourceViewSchema2;
				}
				this._propertyGrid.SelectedObject = menuItemBinding2;
				this._propertyGrid.Refresh();
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002B305 File Offset: 0x00029505
		private void OnSchemaTreeViewAfterSelect(object sender, TreeViewEventArgs e)
		{
			this.UpdateEnabledStates();
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002B30D File Offset: 0x0002950D
		private void OnSchemaTreeViewGotFocus(object sender, EventArgs e)
		{
			this._propertyGrid.SelectedObject = null;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002B31C File Offset: 0x0002951C
		private void PopulateSchema(IDataSourceSchema schema)
		{
			if (schema == null)
			{
				return;
			}
			IDictionary duplicates = new Hashtable();
			IDataSourceViewSchema[] views = schema.GetViews();
			if (views != null)
			{
				for (int i = 0; i < views.Length; i++)
				{
					this.PopulateSchemaRecursive(this._schemaTreeView.Nodes, views[i], 0, duplicates);
				}
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002B364 File Offset: 0x00029564
		private void PopulateSchemaRecursive(System.Windows.Forms.TreeNodeCollection nodes, IDataSourceViewSchema viewSchema, int depth, IDictionary duplicates)
		{
			if (viewSchema == null)
			{
				return;
			}
			MenuBindingsEditorForm.SchemaTreeNode schemaTreeNode = new MenuBindingsEditorForm.SchemaTreeNode(viewSchema);
			nodes.Add(schemaTreeNode);
			MenuBindingsEditorForm.SchemaTreeNode schemaTreeNode2 = (MenuBindingsEditorForm.SchemaTreeNode)duplicates[viewSchema.Name];
			if (schemaTreeNode2 != null)
			{
				schemaTreeNode2.Duplicate = true;
				schemaTreeNode.Duplicate = true;
			}
			foreach (object obj in this._bindingsListView.Items)
			{
				MenuItemBinding menuItemBinding = (MenuItemBinding)obj;
				if (string.Compare(menuItemBinding.DataMember, viewSchema.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					IDataSourceViewSchemaAccessor dataSourceViewSchemaAccessor = menuItemBinding;
					if (depth == menuItemBinding.Depth || dataSourceViewSchemaAccessor.DataSourceViewSchema == null)
					{
						dataSourceViewSchemaAccessor.DataSourceViewSchema = viewSchema;
					}
				}
			}
			IDataSourceViewSchema[] children = viewSchema.GetChildren();
			if (children != null)
			{
				for (int i = 0; i < children.Length; i++)
				{
					this.PopulateSchemaRecursive(schemaTreeNode.Nodes, children[i], depth + 1, duplicates);
				}
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002B45C File Offset: 0x0002965C
		private void UpdateEnabledStates()
		{
			if (this._bindingsListView.SelectedIndices.Count > 0)
			{
				int num = this._bindingsListView.SelectedIndices[0];
				this._moveBindingDownButton.Enabled = (num + 1 < this._bindingsListView.Items.Count);
				this._moveBindingUpButton.Enabled = (num > 0);
				this._deleteBindingButton.Enabled = true;
			}
			else
			{
				this._moveBindingDownButton.Enabled = false;
				this._moveBindingUpButton.Enabled = false;
				this._deleteBindingButton.Enabled = false;
			}
			this._addBindingButton.Enabled = (this._schemaTreeView.SelectedNode != null);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002B508 File Offset: 0x00029708
		private void UpdateSelectedBinding()
		{
			MenuItemBinding selectedObject = null;
			if (this._bindingsListView.SelectedItems.Count > 0)
			{
				MenuItemBinding menuItemBinding = (MenuItemBinding)this._bindingsListView.SelectedItems[0];
				selectedObject = menuItemBinding;
			}
			this._propertyGrid.SelectedObject = selectedObject;
			this._propertyGrid.Refresh();
			this.UpdateEnabledStates();
		}

		// Token: 0x040004A4 RID: 1188
		private IDataSourceSchema _schema;

		// Token: 0x0200040C RID: 1036
		private class SchemaTreeNode : System.Windows.Forms.TreeNode
		{
			// Token: 0x060027E7 RID: 10215 RVA: 0x000F47B5 File Offset: 0x000F29B5
			public SchemaTreeNode(IDataSourceViewSchema schema) : base(schema.Name)
			{
				this._schema = schema;
			}

			// Token: 0x17000859 RID: 2137
			// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000F47CA File Offset: 0x000F29CA
			// (set) Token: 0x060027E9 RID: 10217 RVA: 0x000F47D2 File Offset: 0x000F29D2
			public bool Duplicate
			{
				get
				{
					return this._duplicate;
				}
				set
				{
					this._duplicate = value;
				}
			}

			// Token: 0x1700085A RID: 2138
			// (get) Token: 0x060027EA RID: 10218 RVA: 0x000F47DB File Offset: 0x000F29DB
			public object Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x04001C79 RID: 7289
			private IDataSourceViewSchema _schema;

			// Token: 0x04001C7A RID: 7290
			private bool _duplicate;
		}
	}
}
