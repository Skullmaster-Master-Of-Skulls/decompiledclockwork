using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls.ListControls
{
	// Token: 0x0200050E RID: 1294
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class DataGridColumnsPage : BaseDataListPage
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x00105AFB File Offset: 0x00104AFB
		protected override string HelpKeyword
		{
			get
			{
				return "net.Asp.DataGridProperties.Columns";
			}
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x00105B04 File Offset: 0x00104B04
		private void InitForm()
		{
			this.autoColumnCheck = new System.Windows.Forms.CheckBox();
			GroupLabel groupLabel = new GroupLabel();
			System.Windows.Forms.Label label = new System.Windows.Forms.Label();
			this.availableColumnsTree = new System.Windows.Forms.TreeView();
			this.addColumnButton = new System.Windows.Forms.Button();
			System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
			this.selColumnsList = new ListView();
			this.moveColumnUpButton = new System.Windows.Forms.Button();
			this.moveColumnDownButton = new System.Windows.Forms.Button();
			this.deleteColumnButton = new System.Windows.Forms.Button();
			this.columnPropsGroup = new GroupLabel();
			System.Windows.Forms.Label label3 = new System.Windows.Forms.Label();
			this.columnHeaderTextEdit = new System.Windows.Forms.TextBox();
			System.Windows.Forms.Label label4 = new System.Windows.Forms.Label();
			this.columnHeaderImageEdit = new System.Windows.Forms.TextBox();
			this.columnHeaderImagePickerButton = new System.Windows.Forms.Button();
			System.Windows.Forms.Label label5 = new System.Windows.Forms.Label();
			this.columnFooterTextEdit = new System.Windows.Forms.TextBox();
			System.Windows.Forms.Label label6 = new System.Windows.Forms.Label();
			this.columnSortExprCombo = new ComboBox();
			this.columnVisibleCheck = new System.Windows.Forms.CheckBox();
			this.boundColumnEditor = new DataGridColumnsPage.BoundColumnEditor();
			this.buttonColumnEditor = new DataGridColumnsPage.ButtonColumnEditor();
			this.hyperLinkColumnEditor = new DataGridColumnsPage.HyperLinkColumnEditor();
			this.editCommandColumnEditor = new DataGridColumnsPage.EditCommandColumnEditor();
			this.templatizeLink = new LinkLabel();
			System.Drawing.Image value = new Bitmap(base.GetType(), "ColumnNodes.bmp");
			ImageList imageList = new ImageList();
			imageList.TransparentColor = Color.Magenta;
			imageList.Images.AddStrip(value);
			this.autoColumnCheck.SetBounds(4, 4, 400, 16);
			this.autoColumnCheck.Text = SR.GetString("DGCol_AutoGen");
			this.autoColumnCheck.TabIndex = 0;
			this.autoColumnCheck.TextAlign = ContentAlignment.MiddleLeft;
			this.autoColumnCheck.FlatStyle = FlatStyle.System;
			this.autoColumnCheck.CheckedChanged += this.OnCheckChangedAutoColumn;
			this.autoColumnCheck.Name = "AutoColumnCheckBox";
			groupLabel.SetBounds(4, 24, 431, 14);
			groupLabel.Text = SR.GetString("DGCol_ColListGroup");
			groupLabel.TabStop = false;
			groupLabel.TabIndex = 1;
			groupLabel.Name = "ColumnListGroup";
			label.SetBounds(12, 40, 184, 16);
			label.Text = SR.GetString("DGCol_AvailableCols");
			label.TabStop = false;
			label.TabIndex = 2;
			label.Name = "AvailableColumnsLabel";
			this.availableColumnsTree.SetBounds(12, 58, 170, 88);
			this.availableColumnsTree.ImageList = imageList;
			this.availableColumnsTree.Indent = 5;
			this.availableColumnsTree.HideSelection = false;
			this.availableColumnsTree.TabIndex = 3;
			this.availableColumnsTree.AfterSelect += this.OnSelChangedAvailableColumns;
			this.availableColumnsTree.Name = "AvailableColumnsTree";
			this.addColumnButton.SetBounds(187, 82, 31, 24);
			this.addColumnButton.Text = ">";
			this.addColumnButton.TabIndex = 4;
			this.addColumnButton.FlatStyle = FlatStyle.System;
			this.addColumnButton.Click += this.OnClickAddColumn;
			this.addColumnButton.Name = "AddColumnButton";
			this.addColumnButton.AccessibleName = SR.GetString("DGCol_AddColButtonDesc");
			label2.SetBounds(226, 40, 200, 14);
			label2.Text = SR.GetString("DGCol_SelectedCols");
			label2.TabStop = false;
			label2.TabIndex = 5;
			label2.Name = "SelectedColumnsLabel";
			ColumnHeader columnHeader = new ColumnHeader();
			columnHeader.Width = 176;
			this.selColumnsList.SetBounds(222, 58, 180, 88);
			this.selColumnsList.Columns.Add(columnHeader);
			this.selColumnsList.SmallImageList = imageList;
			this.selColumnsList.View = System.Windows.Forms.View.Details;
			this.selColumnsList.HeaderStyle = ColumnHeaderStyle.None;
			this.selColumnsList.LabelWrap = false;
			this.selColumnsList.HideSelection = false;
			this.selColumnsList.MultiSelect = false;
			this.selColumnsList.TabIndex = 6;
			this.selColumnsList.SelectedIndexChanged += this.OnSelIndexChangedSelColumnsList;
			this.selColumnsList.KeyDown += this.OnSelColumnsListKeyDown;
			this.selColumnsList.Name = "SelectedColumnsList";
			this.moveColumnUpButton.SetBounds(406, 58, 28, 27);
			this.moveColumnUpButton.TabIndex = 7;
			Bitmap bitmap = new Icon(base.GetType(), "SortUp.ico").ToBitmap();
			bitmap.MakeTransparent();
			this.moveColumnUpButton.Image = bitmap;
			this.moveColumnUpButton.Click += this.OnClickMoveColumnUp;
			this.moveColumnUpButton.Name = "MoveColumnUpButton";
			this.moveColumnUpButton.AccessibleName = SR.GetString("DGCol_MoveColumnUpButtonDesc");
			this.moveColumnDownButton.SetBounds(406, 88, 28, 27);
			this.moveColumnDownButton.TabIndex = 8;
			Bitmap bitmap2 = new Icon(base.GetType(), "SortDown.ico").ToBitmap();
			bitmap2.MakeTransparent();
			this.moveColumnDownButton.Image = bitmap2;
			this.moveColumnDownButton.Click += this.OnClickMoveColumnDown;
			this.moveColumnDownButton.Name = "MoveColumnDownButton";
			this.moveColumnDownButton.AccessibleName = SR.GetString("DGCol_MoveColumnDownButtonDesc");
			this.deleteColumnButton.SetBounds(406, 118, 28, 27);
			this.deleteColumnButton.TabIndex = 9;
			Bitmap bitmap3 = new Icon(base.GetType(), "Delete.ico").ToBitmap();
			bitmap3.MakeTransparent();
			this.deleteColumnButton.Image = bitmap3;
			this.deleteColumnButton.Click += this.OnClickDeleteColumn;
			this.deleteColumnButton.Name = "DeleteColumnButton";
			this.deleteColumnButton.AccessibleName = SR.GetString("DGCol_DeleteColumnButtonDesc");
			this.columnPropsGroup.SetBounds(8, 150, 431, 14);
			this.columnPropsGroup.Text = SR.GetString("DGCol_ColumnPropsGroup1");
			this.columnPropsGroup.TabStop = false;
			this.columnPropsGroup.TabIndex = 10;
			label3.SetBounds(20, 166, 180, 14);
			label3.Text = SR.GetString("DGCol_HeaderText");
			label3.TabStop = false;
			label3.TabIndex = 11;
			label3.Name = "ColumnHeaderTextLabel";
			this.columnHeaderTextEdit.SetBounds(20, 182, 182, 24);
			this.columnHeaderTextEdit.TabIndex = 12;
			this.columnHeaderTextEdit.TextChanged += this.OnTextChangedColHeaderText;
			this.columnHeaderTextEdit.LostFocus += this.OnLostFocusColHeaderText;
			this.columnHeaderTextEdit.Name = "ColumnHeaderTextEdit";
			label4.SetBounds(20, 208, 180, 14);
			label4.Text = SR.GetString("DGCol_HeaderImage");
			label4.TabStop = false;
			label4.TabIndex = 13;
			label4.Name = "ColumnHeaderImageLabel";
			this.columnHeaderImageEdit.SetBounds(20, 224, 156, 24);
			this.columnHeaderImageEdit.TabIndex = 14;
			this.columnHeaderImageEdit.TextChanged += this.OnChangedColumnProperties;
			this.columnHeaderImageEdit.Name = "ColumnHeaderImageEdit";
			this.columnHeaderImagePickerButton.SetBounds(180, 223, 24, 23);
			this.columnHeaderImagePickerButton.Text = "...";
			this.columnHeaderImagePickerButton.TabIndex = 15;
			this.columnHeaderImagePickerButton.FlatStyle = FlatStyle.System;
			this.columnHeaderImagePickerButton.Click += this.OnClickColHeaderImagePicker;
			this.columnHeaderImagePickerButton.Name = "ColumnHeaderImagePickerButton";
			this.columnHeaderImagePickerButton.AccessibleName = SR.GetString("DGCol_HeaderImagePickerDesc");
			label5.SetBounds(220, 166, 180, 14);
			label5.Text = SR.GetString("DGCol_FooterText");
			label5.TabStop = false;
			label5.TabIndex = 16;
			label5.Name = "ColumnFooterTextLabel";
			this.columnFooterTextEdit.SetBounds(220, 182, 182, 24);
			this.columnFooterTextEdit.TabIndex = 17;
			this.columnFooterTextEdit.TextChanged += this.OnChangedColumnProperties;
			this.columnFooterTextEdit.Name = "ColumnFooterTextEdit";
			label6.SetBounds(220, 208, 144, 16);
			label6.Text = SR.GetString("DGCol_SortExpr");
			label6.TabStop = false;
			label6.TabIndex = 18;
			label6.Name = "ColumnSortExprLabel";
			this.columnSortExprCombo.SetBounds(220, 224, 140, 21);
			this.columnSortExprCombo.TabIndex = 19;
			this.columnSortExprCombo.TextChanged += this.OnChangedColumnProperties;
			this.columnSortExprCombo.SelectedIndexChanged += this.OnChangedColumnProperties;
			this.columnSortExprCombo.Name = "ColumnSortExprCombo";
			this.columnVisibleCheck.SetBounds(368, 222, 100, 40);
			this.columnVisibleCheck.Text = SR.GetString("DGCol_Visible");
			this.columnVisibleCheck.TabIndex = 20;
			this.columnVisibleCheck.FlatStyle = FlatStyle.System;
			this.columnVisibleCheck.CheckAlign = ContentAlignment.TopLeft;
			this.columnVisibleCheck.TextAlign = ContentAlignment.TopLeft;
			this.columnVisibleCheck.CheckedChanged += this.OnChangedColumnProperties;
			this.columnVisibleCheck.Name = "ColumnVisibleCheckBox";
			this.boundColumnEditor.SetBounds(20, 250, 416, 164);
			this.boundColumnEditor.TabIndex = 21;
			this.boundColumnEditor.Visible = false;
			this.boundColumnEditor.Changed += this.OnChangedColumnProperties;
			this.buttonColumnEditor.SetBounds(20, 250, 416, 164);
			this.buttonColumnEditor.TabIndex = 22;
			this.buttonColumnEditor.Visible = false;
			this.buttonColumnEditor.Changed += this.OnChangedColumnProperties;
			this.hyperLinkColumnEditor.SetBounds(20, 250, 416, 164);
			this.hyperLinkColumnEditor.TabIndex = 23;
			this.hyperLinkColumnEditor.Visible = false;
			this.hyperLinkColumnEditor.Changed += this.OnChangedColumnProperties;
			this.editCommandColumnEditor.SetBounds(20, 250, 416, 164);
			this.editCommandColumnEditor.TabIndex = 24;
			this.editCommandColumnEditor.Visible = false;
			this.editCommandColumnEditor.Changed += this.OnChangedColumnProperties;
			this.templatizeLink.SetBounds(18, 414, 400, 16);
			this.templatizeLink.TabIndex = 25;
			this.templatizeLink.Text = SR.GetString("DGCol_Templatize");
			this.templatizeLink.Visible = false;
			this.templatizeLink.LinkClicked += this.OnClickTemplatize;
			this.templatizeLink.Name = "TemplatizeLink";
			this.Text = SR.GetString("DGCol_Text");
			base.AccessibleDescription = SR.GetString("DGCol_Desc");
			base.Size = new Size(464, 432);
			base.CommitOnDeactivate = true;
			base.Icon = new Icon(base.GetType(), "DataGridColumnsPage.ico");
			base.Controls.Clear();
			base.Controls.AddRange(new Control[]
			{
				this.templatizeLink,
				this.editCommandColumnEditor,
				this.hyperLinkColumnEditor,
				this.buttonColumnEditor,
				this.boundColumnEditor,
				this.columnVisibleCheck,
				this.columnSortExprCombo,
				label6,
				this.columnFooterTextEdit,
				label5,
				this.columnHeaderImagePickerButton,
				this.columnHeaderImageEdit,
				label4,
				this.columnHeaderTextEdit,
				label3,
				this.columnPropsGroup,
				this.deleteColumnButton,
				this.moveColumnDownButton,
				this.moveColumnUpButton,
				this.selColumnsList,
				label2,
				this.addColumnButton,
				this.availableColumnsTree,
				label,
				groupLabel,
				this.autoColumnCheck
			});
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x00106784 File Offset: 0x00105784
		private void InitPage()
		{
			this.currentDataSource = null;
			this.autoColumnCheck.Checked = false;
			this.selectedDataSourceNode = null;
			this.availableColumnsTree.Nodes.Clear();
			this.selColumnsList.Items.Clear();
			this.currentColumnItem = null;
			this.columnSortExprCombo.Items.Clear();
			this.currentColumnEditor = null;
			this.boundColumnEditor.ClearDataFields();
			this.buttonColumnEditor.ClearDataFields();
			this.hyperLinkColumnEditor.ClearDataFields();
			this.editCommandColumnEditor.ClearDataFields();
			this.propChangesPending = false;
			this.headerTextChanged = false;
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x00106824 File Offset: 0x00105824
		private void LoadColumnProperties()
		{
			string @string = SR.GetString("DGCol_ColumnPropsGroup1");
			if (this.currentColumnItem != null)
			{
				base.EnterLoadingMode();
				this.columnHeaderTextEdit.Text = this.currentColumnItem.HeaderText;
				this.columnHeaderImageEdit.Text = this.currentColumnItem.HeaderImageUrl;
				this.columnFooterTextEdit.Text = this.currentColumnItem.FooterText;
				this.columnSortExprCombo.Text = this.currentColumnItem.SortExpression;
				this.columnVisibleCheck.Checked = this.currentColumnItem.Visible;
				this.currentColumnEditor = null;
				if (this.currentColumnItem is DataGridColumnsPage.BoundColumnItem)
				{
					this.currentColumnEditor = this.boundColumnEditor;
					@string = SR.GetString("DGCol_ColumnPropsGroup2", new object[]
					{
						"BoundColumn"
					});
				}
				else if (this.currentColumnItem is DataGridColumnsPage.ButtonColumnItem)
				{
					this.currentColumnEditor = this.buttonColumnEditor;
					@string = SR.GetString("DGCol_ColumnPropsGroup2", new object[]
					{
						"ButtonColumn"
					});
				}
				else if (this.currentColumnItem is DataGridColumnsPage.HyperLinkColumnItem)
				{
					this.currentColumnEditor = this.hyperLinkColumnEditor;
					@string = SR.GetString("DGCol_ColumnPropsGroup2", new object[]
					{
						"HyperLinkColumn"
					});
				}
				else if (this.currentColumnItem is DataGridColumnsPage.EditCommandColumnItem)
				{
					this.currentColumnEditor = this.editCommandColumnEditor;
					@string = SR.GetString("DGCol_ColumnPropsGroup2", new object[]
					{
						"EditCommandColumn"
					});
				}
				else if (this.currentColumnItem is DataGridColumnsPage.TemplateColumnItem)
				{
					@string = SR.GetString("DGCol_ColumnPropsGroup2", new object[]
					{
						"TemplateColumn"
					});
				}
				if (this.currentColumnEditor != null)
				{
					this.currentColumnEditor.LoadColumn(this.currentColumnItem);
				}
				base.ExitLoadingMode();
			}
			this.columnPropsGroup.Text = @string;
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x001069FC File Offset: 0x001059FC
		private void LoadColumns()
		{
			System.Web.UI.WebControls.DataGrid dataGrid = (System.Web.UI.WebControls.DataGrid)base.GetBaseControl();
			DataGridColumnCollection columns = dataGrid.Columns;
			if (columns != null)
			{
				int count = columns.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridColumn dataGridColumn = columns[i];
					DataGridColumnsPage.ColumnItem columnItem;
					if (dataGridColumn is BoundColumn)
					{
						columnItem = new DataGridColumnsPage.BoundColumnItem((BoundColumn)dataGridColumn);
					}
					else if (dataGridColumn is ButtonColumn)
					{
						columnItem = new DataGridColumnsPage.ButtonColumnItem((ButtonColumn)dataGridColumn);
					}
					else if (dataGridColumn is HyperLinkColumn)
					{
						columnItem = new DataGridColumnsPage.HyperLinkColumnItem((HyperLinkColumn)dataGridColumn);
					}
					else if (dataGridColumn is TemplateColumn)
					{
						columnItem = new DataGridColumnsPage.TemplateColumnItem((TemplateColumn)dataGridColumn);
					}
					else if (dataGridColumn is EditCommandColumn)
					{
						columnItem = new DataGridColumnsPage.EditCommandColumnItem((EditCommandColumn)dataGridColumn);
					}
					else
					{
						columnItem = new DataGridColumnsPage.CustomColumnItem(dataGridColumn);
					}
					columnItem.LoadColumnInfo();
					this.selColumnsList.Items.Add(columnItem);
				}
				if (this.selColumnsList.Items.Count != 0)
				{
					this.currentColumnItem = (DataGridColumnsPage.ColumnItem)this.selColumnsList.Items[0];
					this.currentColumnItem.Selected = true;
				}
			}
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x00106B24 File Offset: 0x00105B24
		protected override void LoadComponent()
		{
			this.InitPage();
			System.Web.UI.WebControls.DataGrid dataGrid = (System.Web.UI.WebControls.DataGrid)base.GetBaseControl();
			this.LoadDataSourceItem();
			this.LoadAvailableColumnsTree();
			this.LoadDataSourceFields();
			this.autoColumnCheck.Checked = dataGrid.AutoGenerateColumns;
			this.LoadColumns();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x00106B74 File Offset: 0x00105B74
		private void LoadDataSourceItem()
		{
			System.Web.UI.WebControls.DataGrid dataGrid = (System.Web.UI.WebControls.DataGrid)base.GetBaseControl();
			DataGridDesigner dataGridDesigner = (DataGridDesigner)base.GetBaseDesigner();
			string dataSource = dataGridDesigner.DataSource;
			if (dataSource != null)
			{
				ISite site = dataGrid.Site;
				IContainer container = (IContainer)site.GetService(typeof(IContainer));
				if (container != null)
				{
					IComponent component = container.Components[dataSource];
					if (component != null)
					{
						if (component is IListSource)
						{
							this.currentDataSource = new BaseDataListPage.ListSourceDataSourceItem(dataSource, (IListSource)component)
							{
								CurrentDataMember = dataGridDesigner.DataMember
							};
							return;
						}
						if (component is IEnumerable)
						{
							this.currentDataSource = new BaseDataListPage.DataSourceItem(dataSource, (IEnumerable)component);
						}
					}
				}
			}
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x00106C24 File Offset: 0x00105C24
		private void LoadDataSourceFields()
		{
			base.EnterLoadingMode();
			if (this.currentDataSource != null)
			{
				PropertyDescriptorCollection fields = this.currentDataSource.Fields;
				if (fields != null)
				{
					int count = fields.Count;
					if (count > 0)
					{
						DataGridColumnsPage.DataFieldNode dataFieldNode = new DataGridColumnsPage.DataFieldNode();
						this.selectedDataSourceNode.Nodes.Add(dataFieldNode);
						foreach (object obj in fields)
						{
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
							if (BaseDataList.IsBindableType(propertyDescriptor.PropertyType))
							{
								string name = propertyDescriptor.Name;
								DataGridColumnsPage.DataFieldNode node = new DataGridColumnsPage.DataFieldNode(name);
								this.selectedDataSourceNode.Nodes.Add(node);
								this.boundColumnEditor.AddDataField(name);
								this.buttonColumnEditor.AddDataField(name);
								this.hyperLinkColumnEditor.AddDataField(name);
								this.editCommandColumnEditor.AddDataField(name);
								this.columnSortExprCombo.Items.Add(name);
							}
						}
						this.availableColumnsTree.SelectedNode = dataFieldNode;
						dataFieldNode.EnsureVisible();
					}
				}
			}
			else
			{
				DataGridColumnsPage.DataFieldNode dataFieldNode2 = new DataGridColumnsPage.DataFieldNode(null);
				this.availableColumnsTree.Nodes.Insert(0, dataFieldNode2);
				this.availableColumnsTree.SelectedNode = dataFieldNode2;
				dataFieldNode2.EnsureVisible();
			}
			base.ExitLoadingMode();
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x00106D64 File Offset: 0x00105D64
		private void LoadAvailableColumnsTree()
		{
			if (this.currentDataSource != null)
			{
				this.selectedDataSourceNode = new DataGridColumnsPage.DataSourceNode();
				this.availableColumnsTree.Nodes.Add(this.selectedDataSourceNode);
			}
			DataGridColumnsPage.ButtonNode buttonNode = new DataGridColumnsPage.ButtonNode();
			this.availableColumnsTree.Nodes.Add(buttonNode);
			DataGridColumnsPage.ButtonNode node = new DataGridColumnsPage.ButtonNode("Select", SR.GetString("DGCol_SelectButton"), SR.GetString("DGCol_Node_Select"));
			buttonNode.Nodes.Add(node);
			DataGridColumnsPage.EditCommandNode node2 = new DataGridColumnsPage.EditCommandNode();
			buttonNode.Nodes.Add(node2);
			DataGridColumnsPage.ButtonNode node3 = new DataGridColumnsPage.ButtonNode("Delete", SR.GetString("DGCol_DeleteButton"), SR.GetString("DGCol_Node_Delete"));
			buttonNode.Nodes.Add(node3);
			DataGridColumnsPage.HyperLinkNode node4 = new DataGridColumnsPage.HyperLinkNode();
			this.availableColumnsTree.Nodes.Add(node4);
			DataGridColumnsPage.TemplateNode node5 = new DataGridColumnsPage.TemplateNode();
			this.availableColumnsTree.Nodes.Add(node5);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x00106E52 File Offset: 0x00105E52
		private void OnChangedColumnProperties(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.propChangesPending = true;
			this.SetDirty();
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x00106E6A File Offset: 0x00105E6A
		private void OnCheckChangedAutoColumn(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.SetDirty();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x00106E84 File Offset: 0x00105E84
		private void OnClickAddColumn(object source, EventArgs e)
		{
			DataGridColumnsPage.AvailableColumnNode availableColumnNode = (DataGridColumnsPage.AvailableColumnNode)this.availableColumnsTree.SelectedNode;
			if (this.propChangesPending)
			{
				this.SaveColumnProperties();
			}
			if (!availableColumnNode.CreatesMultipleColumns)
			{
				DataGridColumnsPage.ColumnItem value = availableColumnNode.CreateColumn();
				this.selColumnsList.Items.Add(value);
				this.currentColumnItem = value;
				this.currentColumnItem.Selected = true;
				this.currentColumnItem.EnsureVisible();
			}
			else
			{
				DataGridColumnsPage.ColumnItem[] array = availableColumnNode.CreateColumns(this.currentDataSource.Fields);
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					this.selColumnsList.Items.Add(array[i]);
				}
				this.currentColumnItem = array[num - 1];
				this.currentColumnItem.Selected = true;
				this.currentColumnItem.EnsureVisible();
			}
			this.selColumnsList.Focus();
			this.SetDirty();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x00106F68 File Offset: 0x00105F68
		private void OnClickColHeaderImagePicker(object source, EventArgs e)
		{
			string text = this.columnHeaderImageEdit.Text.Trim();
			string @string = SR.GetString("DGCol_URLPCaption");
			string string2 = SR.GetString("DGCol_URLPFilter");
			text = UrlBuilder.BuildUrl(base.GetBaseControl(), this, text, @string, string2);
			if (text != null)
			{
				this.columnHeaderImageEdit.Text = text;
				this.OnChangedColumnProperties(this.columnHeaderImageEdit, EventArgs.Empty);
			}
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x00106FCC File Offset: 0x00105FCC
		private void OnClickDeleteColumn(object source, EventArgs e)
		{
			int index = this.currentColumnItem.Index;
			int num = -1;
			int count = this.selColumnsList.Items.Count;
			if (count > 1)
			{
				if (index == count - 1)
				{
					num = index - 1;
				}
				else
				{
					num = index;
				}
			}
			this.propChangesPending = false;
			this.currentColumnItem.Remove();
			this.currentColumnItem = null;
			if (num != -1)
			{
				this.currentColumnItem = (DataGridColumnsPage.ColumnItem)this.selColumnsList.Items[num];
				this.currentColumnItem.Selected = true;
				this.currentColumnItem.EnsureVisible();
			}
			this.SetDirty();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x00107068 File Offset: 0x00106068
		private void OnClickMoveColumnDown(object source, EventArgs e)
		{
			if (this.propChangesPending)
			{
				this.SaveColumnProperties();
			}
			int index = this.currentColumnItem.Index;
			ListViewItem item = this.selColumnsList.Items[index];
			this.selColumnsList.Items.RemoveAt(index);
			this.selColumnsList.Items.Insert(index + 1, item);
			this.currentColumnItem = (DataGridColumnsPage.ColumnItem)this.selColumnsList.Items[index + 1];
			this.currentColumnItem.Selected = true;
			this.currentColumnItem.EnsureVisible();
			this.SetDirty();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x00107108 File Offset: 0x00106108
		private void OnClickMoveColumnUp(object source, EventArgs e)
		{
			if (this.propChangesPending)
			{
				this.SaveColumnProperties();
			}
			int index = this.currentColumnItem.Index;
			ListViewItem item = this.selColumnsList.Items[index];
			this.selColumnsList.Items.RemoveAt(index);
			this.selColumnsList.Items.Insert(index - 1, item);
			this.currentColumnItem = (DataGridColumnsPage.ColumnItem)this.selColumnsList.Items[index - 1];
			this.currentColumnItem.Selected = true;
			this.currentColumnItem.EnsureVisible();
			this.SetDirty();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x001071A8 File Offset: 0x001061A8
		private void OnClickTemplatize(object source, LinkLabelLinkClickedEventArgs e)
		{
			if (this.currentColumnItem == null)
			{
				return;
			}
			if (this.propChangesPending)
			{
				this.SaveColumnProperties();
			}
			this.currentColumnItem.SaveColumnInfo();
			TemplateColumn templateColumn = this.currentColumnItem.GetTemplateColumn((System.Web.UI.WebControls.DataGrid)base.GetBaseControl());
			DataGridColumnsPage.TemplateColumnItem templateColumnItem = new DataGridColumnsPage.TemplateColumnItem(templateColumn);
			templateColumnItem.LoadColumnInfo();
			this.selColumnsList.Items[this.currentColumnItem.Index] = templateColumnItem;
			this.currentColumnItem = templateColumnItem;
			this.currentColumnItem.Selected = true;
			this.SetDirty();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x00107236 File Offset: 0x00106236
		private void OnLostFocusColHeaderText(object source, EventArgs e)
		{
			if (this.headerTextChanged)
			{
				this.headerTextChanged = false;
				if (this.currentColumnItem != null)
				{
					this.currentColumnItem.HeaderText = this.columnHeaderTextEdit.Text;
				}
			}
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x00107265 File Offset: 0x00106265
		private void OnSelChangedAvailableColumns(object source, TreeViewEventArgs e)
		{
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x0010726D File Offset: 0x0010626D
		private void OnSelColumnsListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete && this.currentColumnItem != null)
			{
				this.OnClickDeleteColumn(sender, e);
			}
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x0010728C File Offset: 0x0010628C
		private void OnSelIndexChangedSelColumnsList(object source, EventArgs e)
		{
			if (this.propChangesPending)
			{
				this.SaveColumnProperties();
			}
			if (this.selColumnsList.SelectedItems.Count == 0)
			{
				this.currentColumnItem = null;
			}
			else
			{
				this.currentColumnItem = (DataGridColumnsPage.ColumnItem)this.selColumnsList.SelectedItems[0];
			}
			this.LoadColumnProperties();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x001072EA File Offset: 0x001062EA
		private void OnTextChangedColHeaderText(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.headerTextChanged = true;
			this.propChangesPending = true;
			this.SetDirty();
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x0010730C File Offset: 0x0010630C
		private void SaveColumnProperties()
		{
			if (this.currentColumnItem != null)
			{
				this.currentColumnItem.HeaderText = this.columnHeaderTextEdit.Text;
				this.currentColumnItem.HeaderImageUrl = this.columnHeaderImageEdit.Text.Trim();
				this.currentColumnItem.FooterText = this.columnFooterTextEdit.Text;
				this.currentColumnItem.SortExpression = this.columnSortExprCombo.Text.Trim();
				this.currentColumnItem.Visible = this.columnVisibleCheck.Checked;
				if (this.currentColumnEditor != null)
				{
					this.currentColumnEditor.SaveColumn();
				}
			}
			this.propChangesPending = false;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x001073B8 File Offset: 0x001063B8
		protected override void SaveComponent()
		{
			if (this.propChangesPending)
			{
				this.SaveColumnProperties();
			}
			System.Web.UI.WebControls.DataGrid dataGrid = (System.Web.UI.WebControls.DataGrid)base.GetBaseControl();
			DataGridDesigner dataGridDesigner = (DataGridDesigner)base.GetBaseDesigner();
			dataGrid.AutoGenerateColumns = this.autoColumnCheck.Checked;
			DataGridColumnCollection columns = dataGrid.Columns;
			columns.Clear();
			int count = this.selColumnsList.Items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridColumnsPage.ColumnItem columnItem = (DataGridColumnsPage.ColumnItem)this.selColumnsList.Items[i];
				columnItem.SaveColumnInfo();
				columns.Add(columnItem.RuntimeColumn);
			}
			dataGridDesigner.OnColumnsChanged();
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x0010745D File Offset: 0x0010645D
		public override void SetComponent(IComponent component)
		{
			base.SetComponent(component);
			this.InitForm();
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x0010746C File Offset: 0x0010646C
		private void UpdateEnabledVisibleState()
		{
			DataGridColumnsPage.AvailableColumnNode availableColumnNode = (DataGridColumnsPage.AvailableColumnNode)this.availableColumnsTree.SelectedNode;
			int count = this.selColumnsList.Items.Count;
			int count2 = this.selColumnsList.SelectedItems.Count;
			DataGridColumnsPage.ColumnItem columnItem = null;
			int num = -1;
			if (count2 != 0)
			{
				columnItem = (DataGridColumnsPage.ColumnItem)this.selColumnsList.SelectedItems[0];
			}
			if (columnItem != null)
			{
				num = columnItem.Index;
			}
			bool flag = num != -1;
			this.addColumnButton.Enabled = (availableColumnNode != null && availableColumnNode.IsColumnCreator);
			this.moveColumnUpButton.Enabled = (num > 0);
			this.moveColumnDownButton.Enabled = (num >= 0 && num < count - 1);
			this.deleteColumnButton.Enabled = flag;
			this.columnHeaderTextEdit.Enabled = flag;
			this.columnHeaderImageEdit.Enabled = flag;
			this.columnHeaderImagePickerButton.Enabled = flag;
			this.columnFooterTextEdit.Enabled = flag;
			this.columnSortExprCombo.Enabled = flag;
			this.columnVisibleCheck.Enabled = flag;
			this.boundColumnEditor.Visible = (this.currentColumnEditor == this.boundColumnEditor && flag);
			this.buttonColumnEditor.Visible = (this.currentColumnEditor == this.buttonColumnEditor && flag);
			this.hyperLinkColumnEditor.Visible = (this.currentColumnEditor == this.hyperLinkColumnEditor && flag);
			this.editCommandColumnEditor.Visible = (this.currentColumnEditor == this.editCommandColumnEditor && flag);
			this.templatizeLink.Visible = (count != 0 && (this.boundColumnEditor.Visible || this.buttonColumnEditor.Visible || this.hyperLinkColumnEditor.Visible || this.editCommandColumnEditor.Visible));
		}

		// Token: 0x04001F62 RID: 8034
		private const int ILI_DATASOURCE = 0;

		// Token: 0x04001F63 RID: 8035
		private const int ILI_BOUND = 1;

		// Token: 0x04001F64 RID: 8036
		private const int ILI_ALL = 2;

		// Token: 0x04001F65 RID: 8037
		private const int ILI_CUSTOM = 3;

		// Token: 0x04001F66 RID: 8038
		private const int ILI_BUTTON = 4;

		// Token: 0x04001F67 RID: 8039
		private const int ILI_SELECTBUTTON = 5;

		// Token: 0x04001F68 RID: 8040
		private const int ILI_EDITBUTTON = 6;

		// Token: 0x04001F69 RID: 8041
		private const int ILI_DELETEBUTTON = 7;

		// Token: 0x04001F6A RID: 8042
		private const int ILI_HYPERLINK = 8;

		// Token: 0x04001F6B RID: 8043
		private const int ILI_TEMPLATE = 9;

		// Token: 0x04001F6C RID: 8044
		private System.Windows.Forms.CheckBox autoColumnCheck;

		// Token: 0x04001F6D RID: 8045
		private System.Windows.Forms.TreeView availableColumnsTree;

		// Token: 0x04001F6E RID: 8046
		private System.Windows.Forms.Button addColumnButton;

		// Token: 0x04001F6F RID: 8047
		private ListView selColumnsList;

		// Token: 0x04001F70 RID: 8048
		private System.Windows.Forms.Button moveColumnUpButton;

		// Token: 0x04001F71 RID: 8049
		private System.Windows.Forms.Button moveColumnDownButton;

		// Token: 0x04001F72 RID: 8050
		private System.Windows.Forms.Button deleteColumnButton;

		// Token: 0x04001F73 RID: 8051
		private GroupLabel columnPropsGroup;

		// Token: 0x04001F74 RID: 8052
		private System.Windows.Forms.TextBox columnHeaderTextEdit;

		// Token: 0x04001F75 RID: 8053
		private System.Windows.Forms.TextBox columnHeaderImageEdit;

		// Token: 0x04001F76 RID: 8054
		private System.Windows.Forms.TextBox columnFooterTextEdit;

		// Token: 0x04001F77 RID: 8055
		private ComboBox columnSortExprCombo;

		// Token: 0x04001F78 RID: 8056
		private System.Windows.Forms.CheckBox columnVisibleCheck;

		// Token: 0x04001F79 RID: 8057
		private System.Windows.Forms.Button columnHeaderImagePickerButton;

		// Token: 0x04001F7A RID: 8058
		private LinkLabel templatizeLink;

		// Token: 0x04001F7B RID: 8059
		private DataGridColumnsPage.BoundColumnEditor boundColumnEditor;

		// Token: 0x04001F7C RID: 8060
		private DataGridColumnsPage.ButtonColumnEditor buttonColumnEditor;

		// Token: 0x04001F7D RID: 8061
		private DataGridColumnsPage.HyperLinkColumnEditor hyperLinkColumnEditor;

		// Token: 0x04001F7E RID: 8062
		private DataGridColumnsPage.EditCommandColumnEditor editCommandColumnEditor;

		// Token: 0x04001F7F RID: 8063
		private BaseDataListPage.DataSourceItem currentDataSource;

		// Token: 0x04001F80 RID: 8064
		private DataGridColumnsPage.DataSourceNode selectedDataSourceNode;

		// Token: 0x04001F81 RID: 8065
		private DataGridColumnsPage.ColumnItem currentColumnItem;

		// Token: 0x04001F82 RID: 8066
		private DataGridColumnsPage.ColumnItemEditor currentColumnEditor;

		// Token: 0x04001F83 RID: 8067
		private bool propChangesPending;

		// Token: 0x04001F84 RID: 8068
		private bool headerTextChanged;

		// Token: 0x0200050F RID: 1295
		private abstract class AvailableColumnNode : System.Windows.Forms.TreeNode
		{
			// Token: 0x06002E41 RID: 11841 RVA: 0x00107642 File Offset: 0x00106642
			public AvailableColumnNode(string text, int icon) : base(text, icon, icon)
			{
			}

			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x06002E42 RID: 11842 RVA: 0x0010764D File Offset: 0x0010664D
			public virtual bool CreatesMultipleColumns
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x06002E43 RID: 11843 RVA: 0x00107650 File Offset: 0x00106650
			public virtual bool IsColumnCreator
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002E44 RID: 11844 RVA: 0x00107653 File Offset: 0x00106653
			public virtual DataGridColumnsPage.ColumnItem CreateColumn()
			{
				return null;
			}

			// Token: 0x06002E45 RID: 11845 RVA: 0x00107656 File Offset: 0x00106656
			public virtual DataGridColumnsPage.ColumnItem[] CreateColumns(PropertyDescriptorCollection fields)
			{
				return null;
			}
		}

		// Token: 0x02000510 RID: 1296
		private class DataSourceNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x06002E46 RID: 11846 RVA: 0x00107659 File Offset: 0x00106659
			public DataSourceNode() : base(SR.GetString("DGCol_Node_DataFields"), 0)
			{
			}

			// Token: 0x170008C1 RID: 2241
			// (get) Token: 0x06002E47 RID: 11847 RVA: 0x0010766C File Offset: 0x0010666C
			public override bool IsColumnCreator
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x02000511 RID: 1297
		private class DataFieldNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x06002E48 RID: 11848 RVA: 0x0010766F File Offset: 0x0010666F
			public DataFieldNode() : base(SR.GetString("DGCol_Node_AllFields"), 2)
			{
				this.fieldName = null;
				this.allFields = true;
			}

			// Token: 0x06002E49 RID: 11849 RVA: 0x00107690 File Offset: 0x00106690
			public DataFieldNode(string fieldName) : base(fieldName, 1)
			{
				this.fieldName = fieldName;
				if (fieldName == null)
				{
					this.genericBoundColumn = true;
					base.Text = SR.GetString("DGCol_Node_Bound");
				}
			}

			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x06002E4A RID: 11850 RVA: 0x001076BB File Offset: 0x001066BB
			public override bool CreatesMultipleColumns
			{
				get
				{
					return this.allFields;
				}
			}

			// Token: 0x06002E4B RID: 11851 RVA: 0x001076C4 File Offset: 0x001066C4
			public override DataGridColumnsPage.ColumnItem CreateColumn()
			{
				BoundColumn boundColumn = new BoundColumn();
				if (!this.genericBoundColumn)
				{
					boundColumn.HeaderText = this.fieldName;
					boundColumn.DataField = this.fieldName;
					boundColumn.SortExpression = this.fieldName;
				}
				DataGridColumnsPage.ColumnItem columnItem = new DataGridColumnsPage.BoundColumnItem(boundColumn);
				columnItem.LoadColumnInfo();
				return columnItem;
			}

			// Token: 0x06002E4C RID: 11852 RVA: 0x00107714 File Offset: 0x00106714
			public override DataGridColumnsPage.ColumnItem[] CreateColumns(PropertyDescriptorCollection fields)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in fields)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if (BaseDataList.IsBindableType(propertyDescriptor.PropertyType))
					{
						DataGridColumnsPage.ColumnItem columnItem = new DataGridColumnsPage.BoundColumnItem(new BoundColumn
						{
							HeaderText = propertyDescriptor.Name,
							DataField = propertyDescriptor.Name
						});
						columnItem.LoadColumnInfo();
						arrayList.Add(columnItem);
					}
				}
				return (DataGridColumnsPage.ColumnItem[])arrayList.ToArray(typeof(DataGridColumnsPage.ColumnItem));
			}

			// Token: 0x04001F85 RID: 8069
			protected string fieldName;

			// Token: 0x04001F86 RID: 8070
			private bool genericBoundColumn;

			// Token: 0x04001F87 RID: 8071
			private bool allFields;
		}

		// Token: 0x02000512 RID: 1298
		private class ButtonNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x06002E4D RID: 11853 RVA: 0x0010779C File Offset: 0x0010679C
			public ButtonNode() : this(string.Empty, SR.GetString("DGCol_Button"), SR.GetString("DGCol_Node_Button"))
			{
			}

			// Token: 0x06002E4E RID: 11854 RVA: 0x001077BD File Offset: 0x001067BD
			public ButtonNode(string command, string buttonText, string text) : base(text, 4)
			{
				this.command = command;
				this.buttonText = buttonText;
			}

			// Token: 0x06002E4F RID: 11855 RVA: 0x001077D8 File Offset: 0x001067D8
			public override DataGridColumnsPage.ColumnItem CreateColumn()
			{
				DataGridColumnsPage.ColumnItem columnItem = new DataGridColumnsPage.ButtonColumnItem(new ButtonColumn
				{
					Text = this.buttonText,
					CommandName = this.command
				});
				columnItem.LoadColumnInfo();
				return columnItem;
			}

			// Token: 0x04001F88 RID: 8072
			private string command;

			// Token: 0x04001F89 RID: 8073
			private string buttonText;
		}

		// Token: 0x02000513 RID: 1299
		private class EditCommandNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x06002E50 RID: 11856 RVA: 0x00107811 File Offset: 0x00106811
			public EditCommandNode() : base(SR.GetString("DGCol_Node_Edit"), 4)
			{
			}

			// Token: 0x06002E51 RID: 11857 RVA: 0x00107824 File Offset: 0x00106824
			public override DataGridColumnsPage.ColumnItem CreateColumn()
			{
				DataGridColumnsPage.ColumnItem columnItem = new DataGridColumnsPage.EditCommandColumnItem(new EditCommandColumn
				{
					EditText = SR.GetString("DGCol_EditButton"),
					UpdateText = SR.GetString("DGCol_UpdateButton"),
					CancelText = SR.GetString("DGCol_CancelButton")
				});
				columnItem.LoadColumnInfo();
				return columnItem;
			}
		}

		// Token: 0x02000514 RID: 1300
		private class HyperLinkNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x06002E52 RID: 11858 RVA: 0x00107875 File Offset: 0x00106875
			public HyperLinkNode() : this(SR.GetString("DGCol_HyperLink"))
			{
			}

			// Token: 0x06002E53 RID: 11859 RVA: 0x00107887 File Offset: 0x00106887
			public HyperLinkNode(string hyperLinkText) : base(SR.GetString("DGCol_Node_HyperLink"), 8)
			{
				this.hyperLinkText = hyperLinkText;
			}

			// Token: 0x06002E54 RID: 11860 RVA: 0x001078A4 File Offset: 0x001068A4
			public override DataGridColumnsPage.ColumnItem CreateColumn()
			{
				HyperLinkColumn runtimeColumn = new HyperLinkColumn();
				DataGridColumnsPage.ColumnItem columnItem = new DataGridColumnsPage.HyperLinkColumnItem(runtimeColumn);
				columnItem.Text = this.hyperLinkText;
				columnItem.LoadColumnInfo();
				return columnItem;
			}

			// Token: 0x04001F8A RID: 8074
			private string hyperLinkText;
		}

		// Token: 0x02000515 RID: 1301
		private class TemplateNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x06002E55 RID: 11861 RVA: 0x001078D1 File Offset: 0x001068D1
			public TemplateNode() : base(SR.GetString("DGCol_Node_Template"), 9)
			{
			}

			// Token: 0x06002E56 RID: 11862 RVA: 0x001078E8 File Offset: 0x001068E8
			public override DataGridColumnsPage.ColumnItem CreateColumn()
			{
				TemplateColumn runtimeColumn = new TemplateColumn();
				DataGridColumnsPage.ColumnItem columnItem = new DataGridColumnsPage.TemplateColumnItem(runtimeColumn);
				columnItem.LoadColumnInfo();
				return columnItem;
			}
		}

		// Token: 0x02000516 RID: 1302
		private abstract class ColumnItem : ListViewItem
		{
			// Token: 0x06002E57 RID: 11863 RVA: 0x00107909 File Offset: 0x00106909
			public ColumnItem(DataGridColumn runtimeColumn, int image) : base(string.Empty, image)
			{
				this.runtimeColumn = runtimeColumn;
				this.headerText = this.GetDefaultHeaderText();
				base.Text = this.GetNodeText(null);
			}

			// Token: 0x170008C3 RID: 2243
			// (get) Token: 0x06002E58 RID: 11864 RVA: 0x00107937 File Offset: 0x00106937
			public virtual DataGridColumnsPage.ColumnItemEditor ColumnEditor
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170008C4 RID: 2244
			// (get) Token: 0x06002E59 RID: 11865 RVA: 0x0010793A File Offset: 0x0010693A
			// (set) Token: 0x06002E5A RID: 11866 RVA: 0x00107942 File Offset: 0x00106942
			public string HeaderText
			{
				get
				{
					return this.headerText;
				}
				set
				{
					this.headerText = value;
					this.UpdateDisplayText();
				}
			}

			// Token: 0x170008C5 RID: 2245
			// (get) Token: 0x06002E5B RID: 11867 RVA: 0x00107951 File Offset: 0x00106951
			// (set) Token: 0x06002E5C RID: 11868 RVA: 0x00107959 File Offset: 0x00106959
			public string HeaderImageUrl
			{
				get
				{
					return this.headerImageUrl;
				}
				set
				{
					this.headerImageUrl = value;
				}
			}

			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x06002E5D RID: 11869 RVA: 0x00107962 File Offset: 0x00106962
			// (set) Token: 0x06002E5E RID: 11870 RVA: 0x0010796A File Offset: 0x0010696A
			public string FooterText
			{
				get
				{
					return this.footerText;
				}
				set
				{
					this.footerText = value;
				}
			}

			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x06002E5F RID: 11871 RVA: 0x00107973 File Offset: 0x00106973
			public DataGridColumn RuntimeColumn
			{
				get
				{
					return this.runtimeColumn;
				}
			}

			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x06002E60 RID: 11872 RVA: 0x0010797B File Offset: 0x0010697B
			// (set) Token: 0x06002E61 RID: 11873 RVA: 0x00107983 File Offset: 0x00106983
			public string SortExpression
			{
				get
				{
					return this.sortExpression;
				}
				set
				{
					this.sortExpression = value;
				}
			}

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x06002E62 RID: 11874 RVA: 0x0010798C File Offset: 0x0010698C
			// (set) Token: 0x06002E63 RID: 11875 RVA: 0x00107994 File Offset: 0x00106994
			public bool Visible
			{
				get
				{
					return this.visible;
				}
				set
				{
					this.visible = value;
				}
			}

			// Token: 0x06002E64 RID: 11876 RVA: 0x0010799D File Offset: 0x0010699D
			protected virtual string GetDefaultHeaderText()
			{
				return SR.GetString("DGCol_Node");
			}

			// Token: 0x06002E65 RID: 11877 RVA: 0x001079A9 File Offset: 0x001069A9
			public virtual string GetNodeText(string headerText)
			{
				if (headerText == null || headerText.Length == 0)
				{
					return this.GetDefaultHeaderText();
				}
				return headerText;
			}

			// Token: 0x06002E66 RID: 11878 RVA: 0x001079C0 File Offset: 0x001069C0
			protected ITemplate GetTemplate(System.Web.UI.WebControls.DataGrid dataGrid, string templateContent)
			{
				ITemplate result;
				try
				{
					ISite site = dataGrid.Site;
					IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
					result = ControlParser.ParseTemplate(designerHost, templateContent, null);
				}
				catch (Exception)
				{
					result = null;
				}
				return result;
			}

			// Token: 0x06002E67 RID: 11879 RVA: 0x00107A0C File Offset: 0x00106A0C
			public virtual TemplateColumn GetTemplateColumn(System.Web.UI.WebControls.DataGrid dataGrid)
			{
				return new TemplateColumn
				{
					HeaderText = this.headerText,
					HeaderImageUrl = this.headerImageUrl
				};
			}

			// Token: 0x06002E68 RID: 11880 RVA: 0x00107A38 File Offset: 0x00106A38
			public virtual void LoadColumnInfo()
			{
				this.headerText = this.runtimeColumn.HeaderText;
				this.headerImageUrl = this.runtimeColumn.HeaderImageUrl;
				this.footerText = this.runtimeColumn.FooterText;
				this.visible = this.runtimeColumn.Visible;
				this.sortExpression = this.runtimeColumn.SortExpression;
				this.UpdateDisplayText();
			}

			// Token: 0x06002E69 RID: 11881 RVA: 0x00107AA0 File Offset: 0x00106AA0
			public virtual void SaveColumnInfo()
			{
				this.runtimeColumn.HeaderText = this.headerText;
				this.runtimeColumn.HeaderImageUrl = this.headerImageUrl;
				this.runtimeColumn.FooterText = this.footerText;
				this.runtimeColumn.Visible = this.visible;
				this.runtimeColumn.SortExpression = this.sortExpression;
			}

			// Token: 0x06002E6A RID: 11882 RVA: 0x00107B02 File Offset: 0x00106B02
			protected void UpdateDisplayText()
			{
				base.Text = this.GetNodeText(this.headerText);
			}

			// Token: 0x04001F8B RID: 8075
			protected DataGridColumn runtimeColumn;

			// Token: 0x04001F8C RID: 8076
			protected string headerText;

			// Token: 0x04001F8D RID: 8077
			protected string headerImageUrl;

			// Token: 0x04001F8E RID: 8078
			protected string footerText;

			// Token: 0x04001F8F RID: 8079
			protected bool visible;

			// Token: 0x04001F90 RID: 8080
			protected string sortExpression;
		}

		// Token: 0x02000517 RID: 1303
		private class BoundColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x06002E6B RID: 11883 RVA: 0x00107B16 File Offset: 0x00106B16
			public BoundColumnItem(BoundColumn runtimeColumn) : base(runtimeColumn, 1)
			{
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x06002E6C RID: 11884 RVA: 0x00107B20 File Offset: 0x00106B20
			// (set) Token: 0x06002E6D RID: 11885 RVA: 0x00107B28 File Offset: 0x00106B28
			public string DataField
			{
				get
				{
					return this.dataField;
				}
				set
				{
					this.dataField = value;
					base.UpdateDisplayText();
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x06002E6E RID: 11886 RVA: 0x00107B37 File Offset: 0x00106B37
			// (set) Token: 0x06002E6F RID: 11887 RVA: 0x00107B3F File Offset: 0x00106B3F
			public string DataFormatString
			{
				get
				{
					return this.dataFormatString;
				}
				set
				{
					this.dataFormatString = value;
				}
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x06002E70 RID: 11888 RVA: 0x00107B48 File Offset: 0x00106B48
			// (set) Token: 0x06002E71 RID: 11889 RVA: 0x00107B50 File Offset: 0x00106B50
			public bool ReadOnly
			{
				get
				{
					return this.readOnly;
				}
				set
				{
					this.readOnly = value;
				}
			}

			// Token: 0x06002E72 RID: 11890 RVA: 0x00107B59 File Offset: 0x00106B59
			protected override string GetDefaultHeaderText()
			{
				if (this.dataField != null && this.dataField.Length != 0)
				{
					return this.dataField;
				}
				return SR.GetString("DGCol_Node_Bound");
			}

			// Token: 0x06002E73 RID: 11891 RVA: 0x00107B84 File Offset: 0x00106B84
			public override TemplateColumn GetTemplateColumn(System.Web.UI.WebControls.DataGrid dataGrid)
			{
				TemplateColumn templateColumn = base.GetTemplateColumn(dataGrid);
				templateColumn.ItemTemplate = base.GetTemplate(dataGrid, this.GetTemplateContent(false));
				if (!this.readOnly)
				{
					templateColumn.EditItemTemplate = base.GetTemplate(dataGrid, this.GetTemplateContent(true));
				}
				return templateColumn;
			}

			// Token: 0x06002E74 RID: 11892 RVA: 0x00107BCC File Offset: 0x00106BCC
			private string GetTemplateContent(bool editMode)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string value = editMode ? "TextBox" : "Label";
				stringBuilder.Append("<asp:");
				stringBuilder.Append(value);
				stringBuilder.Append(" runat=\"server\"");
				string text = ((BoundColumn)base.RuntimeColumn).DataField;
				if (text.Length != 0)
				{
					stringBuilder.Append(" Text='<%# DataBinder.Eval(Container, \"DataItem.");
					stringBuilder.Append(text);
					stringBuilder.Append("\"");
					if (this.dataFormatString.Length != 0)
					{
						stringBuilder.Append(", \"");
						stringBuilder.Append(this.dataFormatString);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append(") %>'");
				}
				stringBuilder.Append("></asp:");
				stringBuilder.Append(value);
				stringBuilder.Append(">");
				return stringBuilder.ToString();
			}

			// Token: 0x06002E75 RID: 11893 RVA: 0x00107CAC File Offset: 0x00106CAC
			public override void LoadColumnInfo()
			{
				base.LoadColumnInfo();
				BoundColumn boundColumn = (BoundColumn)base.RuntimeColumn;
				this.dataField = boundColumn.DataField;
				this.dataFormatString = boundColumn.DataFormatString;
				this.readOnly = boundColumn.ReadOnly;
				base.UpdateDisplayText();
			}

			// Token: 0x06002E76 RID: 11894 RVA: 0x00107CF8 File Offset: 0x00106CF8
			public override void SaveColumnInfo()
			{
				base.SaveColumnInfo();
				BoundColumn boundColumn = (BoundColumn)base.RuntimeColumn;
				boundColumn.DataField = this.dataField;
				boundColumn.DataFormatString = this.dataFormatString;
				boundColumn.ReadOnly = this.readOnly;
			}

			// Token: 0x04001F91 RID: 8081
			protected string dataField;

			// Token: 0x04001F92 RID: 8082
			protected string dataFormatString;

			// Token: 0x04001F93 RID: 8083
			protected bool readOnly;
		}

		// Token: 0x02000518 RID: 1304
		private class ButtonColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x06002E77 RID: 11895 RVA: 0x00107D3B File Offset: 0x00106D3B
			public ButtonColumnItem(ButtonColumn runtimeColumn) : base(runtimeColumn, 4)
			{
			}

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x06002E78 RID: 11896 RVA: 0x00107D45 File Offset: 0x00106D45
			// (set) Token: 0x06002E79 RID: 11897 RVA: 0x00107D4D File Offset: 0x00106D4D
			public string Command
			{
				get
				{
					return this.command;
				}
				set
				{
					this.command = value;
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x06002E7A RID: 11898 RVA: 0x00107D56 File Offset: 0x00106D56
			// (set) Token: 0x06002E7B RID: 11899 RVA: 0x00107D5E File Offset: 0x00106D5E
			public string ButtonText
			{
				get
				{
					return this.buttonText;
				}
				set
				{
					this.buttonText = value;
					base.UpdateDisplayText();
				}
			}

			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x06002E7C RID: 11900 RVA: 0x00107D6D File Offset: 0x00106D6D
			// (set) Token: 0x06002E7D RID: 11901 RVA: 0x00107D75 File Offset: 0x00106D75
			public ButtonColumnType ButtonType
			{
				get
				{
					return this.buttonType;
				}
				set
				{
					this.buttonType = value;
				}
			}

			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x06002E7E RID: 11902 RVA: 0x00107D7E File Offset: 0x00106D7E
			// (set) Token: 0x06002E7F RID: 11903 RVA: 0x00107D86 File Offset: 0x00106D86
			public string ButtonDataTextField
			{
				get
				{
					return this.buttonDataTextField;
				}
				set
				{
					this.buttonDataTextField = value;
				}
			}

			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x06002E80 RID: 11904 RVA: 0x00107D8F File Offset: 0x00106D8F
			// (set) Token: 0x06002E81 RID: 11905 RVA: 0x00107D97 File Offset: 0x00106D97
			public string ButtonDataTextFormatString
			{
				get
				{
					return this.buttonDataTextFormatString;
				}
				set
				{
					this.buttonDataTextFormatString = value;
				}
			}

			// Token: 0x06002E82 RID: 11906 RVA: 0x00107DA0 File Offset: 0x00106DA0
			protected override string GetDefaultHeaderText()
			{
				if (this.buttonText != null && this.buttonText.Length != 0)
				{
					return this.buttonText;
				}
				return SR.GetString("DGCol_Node_Button");
			}

			// Token: 0x06002E83 RID: 11907 RVA: 0x00107DC8 File Offset: 0x00106DC8
			public override TemplateColumn GetTemplateColumn(System.Web.UI.WebControls.DataGrid dataGrid)
			{
				TemplateColumn templateColumn = base.GetTemplateColumn(dataGrid);
				StringBuilder stringBuilder = new StringBuilder();
				string value = (this.buttonType == ButtonColumnType.LinkButton) ? "LinkButton" : "Button";
				stringBuilder.Append("<asp:");
				stringBuilder.Append(value);
				stringBuilder.Append(" runat=\"server\"");
				if (this.buttonDataTextField.Length != 0)
				{
					stringBuilder.Append(" Text='<%# DataBinder.Eval(Container, \"DataItem.");
					stringBuilder.Append(this.buttonDataTextField);
					stringBuilder.Append("\"");
					if (this.buttonDataTextFormatString.Length != 0)
					{
						stringBuilder.Append(", \"");
						stringBuilder.Append(this.buttonDataTextFormatString);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append(") %>'");
				}
				else
				{
					stringBuilder.Append(" Text=\"");
					stringBuilder.Append(this.buttonText);
					stringBuilder.Append("\"");
				}
				stringBuilder.Append(" CommandName=\"");
				stringBuilder.Append(this.command);
				stringBuilder.Append("\"");
				stringBuilder.Append(" CausesValidation=\"false\"></asp:");
				stringBuilder.Append(value);
				stringBuilder.Append(">");
				templateColumn.ItemTemplate = base.GetTemplate(dataGrid, stringBuilder.ToString());
				return templateColumn;
			}

			// Token: 0x06002E84 RID: 11908 RVA: 0x00107F08 File Offset: 0x00106F08
			public override void LoadColumnInfo()
			{
				base.LoadColumnInfo();
				ButtonColumn buttonColumn = (ButtonColumn)base.RuntimeColumn;
				this.command = buttonColumn.CommandName;
				this.buttonText = buttonColumn.Text;
				this.buttonDataTextField = buttonColumn.DataTextField;
				this.buttonDataTextFormatString = buttonColumn.DataTextFormatString;
				this.buttonType = buttonColumn.ButtonType;
				base.UpdateDisplayText();
			}

			// Token: 0x06002E85 RID: 11909 RVA: 0x00107F6C File Offset: 0x00106F6C
			public override void SaveColumnInfo()
			{
				base.SaveColumnInfo();
				ButtonColumn buttonColumn = (ButtonColumn)base.RuntimeColumn;
				buttonColumn.CommandName = this.command;
				buttonColumn.Text = this.buttonText;
				buttonColumn.DataTextField = this.buttonDataTextField;
				buttonColumn.DataTextFormatString = this.buttonDataTextFormatString;
				buttonColumn.ButtonType = this.buttonType;
			}

			// Token: 0x04001F94 RID: 8084
			protected string command;

			// Token: 0x04001F95 RID: 8085
			protected string buttonText;

			// Token: 0x04001F96 RID: 8086
			protected string buttonDataTextField;

			// Token: 0x04001F97 RID: 8087
			protected string buttonDataTextFormatString;

			// Token: 0x04001F98 RID: 8088
			protected ButtonColumnType buttonType;
		}

		// Token: 0x02000519 RID: 1305
		private class HyperLinkColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x06002E86 RID: 11910 RVA: 0x00107FC7 File Offset: 0x00106FC7
			public HyperLinkColumnItem(HyperLinkColumn runtimeColumn) : base(runtimeColumn, 8)
			{
			}

			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x06002E87 RID: 11911 RVA: 0x00107FD1 File Offset: 0x00106FD1
			// (set) Token: 0x06002E88 RID: 11912 RVA: 0x00107FD9 File Offset: 0x00106FD9
			public string AnchorText
			{
				get
				{
					return this.anchorText;
				}
				set
				{
					this.anchorText = value;
					base.UpdateDisplayText();
				}
			}

			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x06002E89 RID: 11913 RVA: 0x00107FE8 File Offset: 0x00106FE8
			// (set) Token: 0x06002E8A RID: 11914 RVA: 0x00107FF0 File Offset: 0x00106FF0
			public string AnchorDataTextField
			{
				get
				{
					return this.anchorDataTextField;
				}
				set
				{
					this.anchorDataTextField = value;
				}
			}

			// Token: 0x170008D4 RID: 2260
			// (get) Token: 0x06002E8B RID: 11915 RVA: 0x00107FF9 File Offset: 0x00106FF9
			// (set) Token: 0x06002E8C RID: 11916 RVA: 0x00108001 File Offset: 0x00107001
			public string AnchorDataTextFormatString
			{
				get
				{
					return this.anchorDataTextFormatString;
				}
				set
				{
					this.anchorDataTextFormatString = value;
				}
			}

			// Token: 0x170008D5 RID: 2261
			// (get) Token: 0x06002E8D RID: 11917 RVA: 0x0010800A File Offset: 0x0010700A
			// (set) Token: 0x06002E8E RID: 11918 RVA: 0x00108012 File Offset: 0x00107012
			public string Url
			{
				get
				{
					return this.url;
				}
				set
				{
					this.url = value;
				}
			}

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x06002E8F RID: 11919 RVA: 0x0010801B File Offset: 0x0010701B
			// (set) Token: 0x06002E90 RID: 11920 RVA: 0x00108023 File Offset: 0x00107023
			public string DataUrlField
			{
				get
				{
					return this.dataUrlField;
				}
				set
				{
					this.dataUrlField = value;
				}
			}

			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x06002E91 RID: 11921 RVA: 0x0010802C File Offset: 0x0010702C
			// (set) Token: 0x06002E92 RID: 11922 RVA: 0x00108034 File Offset: 0x00107034
			public string DataUrlFormatString
			{
				get
				{
					return this.dataUrlFormatString;
				}
				set
				{
					this.dataUrlFormatString = value;
				}
			}

			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x06002E93 RID: 11923 RVA: 0x0010803D File Offset: 0x0010703D
			// (set) Token: 0x06002E94 RID: 11924 RVA: 0x00108045 File Offset: 0x00107045
			public string Target
			{
				get
				{
					return this.target;
				}
				set
				{
					this.target = value;
				}
			}

			// Token: 0x06002E95 RID: 11925 RVA: 0x0010804E File Offset: 0x0010704E
			protected override string GetDefaultHeaderText()
			{
				if (this.anchorText != null && this.anchorText.Length != 0)
				{
					return this.anchorText;
				}
				return SR.GetString("DGCol_Node_HyperLink");
			}

			// Token: 0x06002E96 RID: 11926 RVA: 0x00108078 File Offset: 0x00107078
			public override TemplateColumn GetTemplateColumn(System.Web.UI.WebControls.DataGrid dataGrid)
			{
				TemplateColumn templateColumn = base.GetTemplateColumn(dataGrid);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<asp:HyperLink");
				stringBuilder.Append(" runat=\"server\"");
				if (this.anchorDataTextField.Length != 0)
				{
					stringBuilder.Append(" Text='<%# DataBinder.Eval(Container, \"DataItem.");
					stringBuilder.Append(this.anchorDataTextField);
					stringBuilder.Append("\"");
					if (this.anchorDataTextFormatString.Length != 0)
					{
						stringBuilder.Append(", \"");
						stringBuilder.Append(this.anchorDataTextFormatString);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append(") %>'");
				}
				else
				{
					stringBuilder.Append(" Text=\"");
					stringBuilder.Append(this.anchorText);
					stringBuilder.Append("\"");
				}
				if (this.dataUrlField.Length != 0)
				{
					stringBuilder.Append(" NavigateUrl='<%# DataBinder.Eval(Container, \"DataItem.");
					stringBuilder.Append(this.dataUrlField);
					stringBuilder.Append("\"");
					if (this.dataUrlFormatString.Length != 0)
					{
						stringBuilder.Append(", \"");
						stringBuilder.Append(this.dataUrlFormatString);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append(") %>'");
				}
				else
				{
					stringBuilder.Append(" NavigateUrl=\"");
					stringBuilder.Append(this.url);
					stringBuilder.Append("\"");
				}
				if (this.target.Length != 0)
				{
					stringBuilder.Append(" Target=\"");
					stringBuilder.Append(this.target);
					stringBuilder.Append("\"");
				}
				stringBuilder.Append("></asp:HyperLink>");
				templateColumn.ItemTemplate = base.GetTemplate(dataGrid, stringBuilder.ToString());
				return templateColumn;
			}

			// Token: 0x06002E97 RID: 11927 RVA: 0x0010822C File Offset: 0x0010722C
			public override void LoadColumnInfo()
			{
				base.LoadColumnInfo();
				HyperLinkColumn hyperLinkColumn = (HyperLinkColumn)base.RuntimeColumn;
				this.anchorText = hyperLinkColumn.Text;
				this.anchorDataTextField = hyperLinkColumn.DataTextField;
				this.anchorDataTextFormatString = hyperLinkColumn.DataTextFormatString;
				this.url = hyperLinkColumn.NavigateUrl;
				this.dataUrlField = hyperLinkColumn.DataNavigateUrlField;
				this.dataUrlFormatString = hyperLinkColumn.DataNavigateUrlFormatString;
				this.target = hyperLinkColumn.Target;
				base.UpdateDisplayText();
			}

			// Token: 0x06002E98 RID: 11928 RVA: 0x001082A8 File Offset: 0x001072A8
			public override void SaveColumnInfo()
			{
				base.SaveColumnInfo();
				HyperLinkColumn hyperLinkColumn = (HyperLinkColumn)base.RuntimeColumn;
				hyperLinkColumn.Text = this.anchorText;
				hyperLinkColumn.DataTextField = this.anchorDataTextField;
				hyperLinkColumn.DataTextFormatString = this.anchorDataTextFormatString;
				hyperLinkColumn.NavigateUrl = this.url;
				hyperLinkColumn.DataNavigateUrlField = this.dataUrlField;
				hyperLinkColumn.DataNavigateUrlFormatString = this.dataUrlFormatString;
				hyperLinkColumn.Target = this.target;
			}

			// Token: 0x04001F99 RID: 8089
			protected string anchorText;

			// Token: 0x04001F9A RID: 8090
			protected string anchorDataTextField;

			// Token: 0x04001F9B RID: 8091
			protected string anchorDataTextFormatString;

			// Token: 0x04001F9C RID: 8092
			protected string url;

			// Token: 0x04001F9D RID: 8093
			protected string dataUrlField;

			// Token: 0x04001F9E RID: 8094
			protected string dataUrlFormatString;

			// Token: 0x04001F9F RID: 8095
			protected string target;
		}

		// Token: 0x0200051A RID: 1306
		private class TemplateColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x06002E99 RID: 11929 RVA: 0x0010831B File Offset: 0x0010731B
			public TemplateColumnItem(TemplateColumn runtimeColumn) : base(runtimeColumn, 9)
			{
			}

			// Token: 0x06002E9A RID: 11930 RVA: 0x00108326 File Offset: 0x00107326
			protected override string GetDefaultHeaderText()
			{
				return SR.GetString("DGCol_Node_Template");
			}
		}

		// Token: 0x0200051B RID: 1307
		private class EditCommandColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x06002E9B RID: 11931 RVA: 0x00108332 File Offset: 0x00107332
			public EditCommandColumnItem(EditCommandColumn runtimeColumn) : base(runtimeColumn, 4)
			{
			}

			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x06002E9C RID: 11932 RVA: 0x0010833C File Offset: 0x0010733C
			// (set) Token: 0x06002E9D RID: 11933 RVA: 0x00108344 File Offset: 0x00107344
			public ButtonColumnType ButtonType
			{
				get
				{
					return this.buttonType;
				}
				set
				{
					this.buttonType = value;
				}
			}

			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x06002E9E RID: 11934 RVA: 0x0010834D File Offset: 0x0010734D
			// (set) Token: 0x06002E9F RID: 11935 RVA: 0x00108355 File Offset: 0x00107355
			public string CancelText
			{
				get
				{
					return this.cancelText;
				}
				set
				{
					this.cancelText = value;
				}
			}

			// Token: 0x170008DB RID: 2267
			// (get) Token: 0x06002EA0 RID: 11936 RVA: 0x0010835E File Offset: 0x0010735E
			// (set) Token: 0x06002EA1 RID: 11937 RVA: 0x00108366 File Offset: 0x00107366
			public string EditText
			{
				get
				{
					return this.editText;
				}
				set
				{
					this.editText = value;
				}
			}

			// Token: 0x170008DC RID: 2268
			// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x0010836F File Offset: 0x0010736F
			// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x00108377 File Offset: 0x00107377
			public string UpdateText
			{
				get
				{
					return this.updateText;
				}
				set
				{
					this.updateText = value;
				}
			}

			// Token: 0x06002EA4 RID: 11940 RVA: 0x00108380 File Offset: 0x00107380
			protected override string GetDefaultHeaderText()
			{
				return SR.GetString("DGCol_Node_Edit");
			}

			// Token: 0x06002EA5 RID: 11941 RVA: 0x0010838C File Offset: 0x0010738C
			public override TemplateColumn GetTemplateColumn(System.Web.UI.WebControls.DataGrid dataGrid)
			{
				TemplateColumn templateColumn = base.GetTemplateColumn(dataGrid);
				templateColumn.ItemTemplate = base.GetTemplate(dataGrid, this.GetTemplateContent(false));
				templateColumn.EditItemTemplate = base.GetTemplate(dataGrid, this.GetTemplateContent(true));
				return templateColumn;
			}

			// Token: 0x06002EA6 RID: 11942 RVA: 0x001083CC File Offset: 0x001073CC
			private string GetTemplateContent(bool editMode)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string value = (this.buttonType == ButtonColumnType.LinkButton) ? "LinkButton" : "Button";
				stringBuilder.Append("<asp:");
				stringBuilder.Append(value);
				stringBuilder.Append(" runat=\"server\"");
				stringBuilder.Append(" Text=\"");
				if (!editMode)
				{
					stringBuilder.Append(this.editText);
				}
				else
				{
					stringBuilder.Append(this.updateText);
				}
				stringBuilder.Append("\"");
				stringBuilder.Append(" CommandName=\"");
				if (!editMode)
				{
					stringBuilder.Append("Edit\"");
					stringBuilder.Append(" CausesValidation=\"false\"");
				}
				else
				{
					stringBuilder.Append("Update\"");
				}
				stringBuilder.Append("></asp:");
				stringBuilder.Append(value);
				stringBuilder.Append(">");
				if (editMode)
				{
					stringBuilder.Append("&nbsp;");
					stringBuilder.Append("<asp:");
					stringBuilder.Append(value);
					stringBuilder.Append(" runat=\"server\"");
					stringBuilder.Append(" Text=\"");
					stringBuilder.Append(this.cancelText);
					stringBuilder.Append("\"");
					stringBuilder.Append(" CommandName=\"");
					stringBuilder.Append("Cancel\"");
					stringBuilder.Append(" CausesValidation=\"false\"></asp:");
					stringBuilder.Append(value);
					stringBuilder.Append(">");
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06002EA7 RID: 11943 RVA: 0x00108538 File Offset: 0x00107538
			public override void LoadColumnInfo()
			{
				base.LoadColumnInfo();
				EditCommandColumn editCommandColumn = (EditCommandColumn)base.RuntimeColumn;
				this.editText = editCommandColumn.EditText;
				this.updateText = editCommandColumn.UpdateText;
				this.cancelText = editCommandColumn.CancelText;
				this.buttonType = editCommandColumn.ButtonType;
			}

			// Token: 0x06002EA8 RID: 11944 RVA: 0x00108588 File Offset: 0x00107588
			public override void SaveColumnInfo()
			{
				base.SaveColumnInfo();
				EditCommandColumn editCommandColumn = (EditCommandColumn)base.RuntimeColumn;
				editCommandColumn.EditText = this.editText;
				editCommandColumn.UpdateText = this.updateText;
				editCommandColumn.CancelText = this.cancelText;
				editCommandColumn.ButtonType = this.buttonType;
			}

			// Token: 0x04001FA0 RID: 8096
			private string editText;

			// Token: 0x04001FA1 RID: 8097
			private string updateText;

			// Token: 0x04001FA2 RID: 8098
			private string cancelText;

			// Token: 0x04001FA3 RID: 8099
			private ButtonColumnType buttonType;
		}

		// Token: 0x0200051C RID: 1308
		private class CustomColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x06002EA9 RID: 11945 RVA: 0x001085D7 File Offset: 0x001075D7
			public CustomColumnItem(DataGridColumn runtimeColumn) : base(runtimeColumn, 3)
			{
			}
		}

		// Token: 0x0200051D RID: 1309
		private abstract class ColumnItemEditor : System.Windows.Forms.Panel
		{
			// Token: 0x06002EAA RID: 11946 RVA: 0x001085E1 File Offset: 0x001075E1
			public ColumnItemEditor()
			{
				this.InitPanel();
			}

			// Token: 0x06002EAB RID: 11947 RVA: 0x001085EF File Offset: 0x001075EF
			public virtual void AddDataField(string fieldName)
			{
				this.dataFieldsAvailable = true;
			}

			// Token: 0x14000046 RID: 70
			// (add) Token: 0x06002EAC RID: 11948 RVA: 0x001085F8 File Offset: 0x001075F8
			// (remove) Token: 0x06002EAD RID: 11949 RVA: 0x00108611 File Offset: 0x00107611
			public event EventHandler Changed
			{
				add
				{
					this.onChangedHandler = (EventHandler)Delegate.Combine(this.onChangedHandler, value);
				}
				remove
				{
					this.onChangedHandler = (EventHandler)Delegate.Remove(this.onChangedHandler, value);
				}
			}

			// Token: 0x06002EAE RID: 11950 RVA: 0x0010862A File Offset: 0x0010762A
			public virtual void ClearDataFields()
			{
				this.dataFieldsAvailable = false;
			}

			// Token: 0x06002EAF RID: 11951 RVA: 0x00108633 File Offset: 0x00107633
			protected virtual void InitPanel()
			{
			}

			// Token: 0x06002EB0 RID: 11952 RVA: 0x00108635 File Offset: 0x00107635
			public virtual void LoadColumn(DataGridColumnsPage.ColumnItem columnItem)
			{
				this.columnItem = columnItem;
			}

			// Token: 0x06002EB1 RID: 11953 RVA: 0x0010863E File Offset: 0x0010763E
			protected virtual void OnChanged(EventArgs e)
			{
				if (this.onChangedHandler != null)
				{
					this.onChangedHandler(this, e);
				}
			}

			// Token: 0x06002EB2 RID: 11954 RVA: 0x00108655 File Offset: 0x00107655
			public virtual void SaveColumn()
			{
			}

			// Token: 0x04001FA4 RID: 8100
			protected DataGridColumnsPage.ColumnItem columnItem;

			// Token: 0x04001FA5 RID: 8101
			protected EventHandler onChangedHandler;

			// Token: 0x04001FA6 RID: 8102
			protected bool dataFieldsAvailable;
		}

		// Token: 0x0200051E RID: 1310
		private class BoundColumnEditor : DataGridColumnsPage.ColumnItemEditor
		{
			// Token: 0x06002EB4 RID: 11956 RVA: 0x00108660 File Offset: 0x00107660
			protected override void InitPanel()
			{
				System.Windows.Forms.Label label = new System.Windows.Forms.Label();
				this.dataFieldEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
				this.dataFormatStringEdit = new System.Windows.Forms.TextBox();
				this.readOnlyCheck = new System.Windows.Forms.CheckBox();
				label.SetBounds(0, 0, 160, 14);
				label.Text = SR.GetString("DGCol_DFC_DataField");
				label.TabStop = false;
				label.TabIndex = 1;
				label.Name = "BoundColumnDataFieldLabel";
				this.dataFieldEdit.SetBounds(0, 16, 182, 20);
				this.dataFieldEdit.TabIndex = 2;
				this.dataFieldEdit.ReadOnly = true;
				this.dataFieldEdit.TextChanged += this.OnColumnChanged;
				this.dataFieldEdit.Name = "BoundColumnDataFieldEdit";
				label2.SetBounds(0, 40, 182, 14);
				label2.Text = SR.GetString("DGCol_DFC_DataFormat");
				label2.TabStop = false;
				label2.TabIndex = 3;
				label2.Name = "BoundColumnDataFormatStringLabel";
				this.dataFormatStringEdit.SetBounds(0, 56, 182, 20);
				this.dataFormatStringEdit.TabIndex = 4;
				this.dataFormatStringEdit.TextChanged += this.OnColumnChanged;
				this.dataFormatStringEdit.Name = "BoundColumnDataFormatStringEdit";
				this.readOnlyCheck.SetBounds(0, 80, 160, 16);
				this.readOnlyCheck.Text = SR.GetString("DGCol_DFC_ReadOnly");
				this.readOnlyCheck.TabIndex = 5;
				this.readOnlyCheck.TextAlign = ContentAlignment.MiddleLeft;
				this.readOnlyCheck.FlatStyle = FlatStyle.System;
				this.readOnlyCheck.CheckedChanged += this.OnColumnChanged;
				this.readOnlyCheck.Name = "BoundColumnReadOnlyCheck";
				base.Controls.Clear();
				base.Controls.AddRange(new Control[]
				{
					this.readOnlyCheck,
					this.dataFormatStringEdit,
					label2,
					this.dataFieldEdit,
					label
				});
			}

			// Token: 0x06002EB5 RID: 11957 RVA: 0x00108860 File Offset: 0x00107860
			public override void LoadColumn(DataGridColumnsPage.ColumnItem columnItem)
			{
				base.LoadColumn(columnItem);
				DataGridColumnsPage.BoundColumnItem boundColumnItem = (DataGridColumnsPage.BoundColumnItem)columnItem;
				this.dataFieldEdit.Text = boundColumnItem.DataField;
				this.dataFormatStringEdit.Text = boundColumnItem.DataFormatString;
				this.readOnlyCheck.Checked = boundColumnItem.ReadOnly;
				this.dataFieldEdit.ReadOnly = this.dataFieldsAvailable;
			}

			// Token: 0x06002EB6 RID: 11958 RVA: 0x001088BF File Offset: 0x001078BF
			private void OnColumnChanged(object source, EventArgs e)
			{
				this.OnChanged(EventArgs.Empty);
			}

			// Token: 0x06002EB7 RID: 11959 RVA: 0x001088CC File Offset: 0x001078CC
			public override void SaveColumn()
			{
				base.SaveColumn();
				DataGridColumnsPage.BoundColumnItem boundColumnItem = (DataGridColumnsPage.BoundColumnItem)this.columnItem;
				boundColumnItem.DataFormatString = this.dataFormatStringEdit.Text;
				boundColumnItem.ReadOnly = this.readOnlyCheck.Checked;
				if (!this.dataFieldsAvailable)
				{
					boundColumnItem.DataField = this.dataFieldEdit.Text.Trim();
				}
			}

			// Token: 0x04001FA7 RID: 8103
			private System.Windows.Forms.TextBox dataFieldEdit;

			// Token: 0x04001FA8 RID: 8104
			private System.Windows.Forms.TextBox dataFormatStringEdit;

			// Token: 0x04001FA9 RID: 8105
			private System.Windows.Forms.CheckBox readOnlyCheck;
		}

		// Token: 0x0200051F RID: 1311
		private class ButtonColumnEditor : DataGridColumnsPage.ColumnItemEditor
		{
			// Token: 0x06002EB9 RID: 11961 RVA: 0x00108933 File Offset: 0x00107933
			public override void AddDataField(string fieldName)
			{
				this.dataTextFieldCombo.AddItem(fieldName);
				base.AddDataField(fieldName);
			}

			// Token: 0x06002EBA RID: 11962 RVA: 0x00108948 File Offset: 0x00107948
			public override void ClearDataFields()
			{
				this.dataTextFieldCombo.Items.Clear();
				this.dataTextFieldCombo.EnsureNotSetItem();
				base.ClearDataFields();
			}

			// Token: 0x06002EBB RID: 11963 RVA: 0x0010896C File Offset: 0x0010796C
			protected override void InitPanel()
			{
				System.Windows.Forms.Label label = new System.Windows.Forms.Label();
				this.textEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
				this.dataTextFieldCombo = new UnsettableComboBox();
				this.dataTextFieldEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label3 = new System.Windows.Forms.Label();
				this.dataTextFormatStringEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label4 = new System.Windows.Forms.Label();
				this.commandEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label5 = new System.Windows.Forms.Label();
				this.buttonTypeCombo = new ComboBox();
				label.SetBounds(0, 0, 160, 14);
				label.Text = SR.GetString("DGCol_BC_Text");
				label.TabStop = false;
				label.TabIndex = 1;
				label.Name = "ButtonColumnTextLabel";
				this.textEdit.SetBounds(0, 16, 182, 24);
				this.textEdit.TabIndex = 2;
				this.textEdit.TextChanged += this.OnColumnChanged;
				this.textEdit.Name = "ButtonColumnTextEdit";
				label2.SetBounds(0, 40, 160, 14);
				label2.Text = SR.GetString("DGCol_BC_DataTextField");
				label2.TabStop = false;
				label2.TabIndex = 3;
				label2.Name = "ButtonColumnDataTextFieldLabel";
				this.dataTextFieldCombo.SetBounds(0, 56, 182, 21);
				this.dataTextFieldCombo.TabIndex = 4;
				this.dataTextFieldCombo.DropDownStyle = ComboBoxStyle.DropDownList;
				this.dataTextFieldCombo.SelectedIndexChanged += this.OnColumnChanged;
				this.dataTextFieldCombo.Name = "ButtonColumnDataTextFieldCombo";
				this.dataTextFieldEdit.SetBounds(0, 56, 182, 14);
				this.dataTextFieldEdit.TabIndex = 4;
				this.dataTextFieldEdit.TextChanged += this.OnColumnChanged;
				this.dataTextFieldEdit.Name = "ButtonColumnDataTextFieldEdit";
				label3.SetBounds(0, 82, 182, 14);
				label3.Text = SR.GetString("DGCol_BC_DataTextFormat");
				label3.TabIndex = 5;
				label3.TabStop = false;
				label3.Name = "ButtonColumnDataTextFormatStringLabel";
				this.dataTextFormatStringEdit.SetBounds(0, 98, 182, 14);
				this.dataTextFormatStringEdit.TabIndex = 6;
				this.dataTextFormatStringEdit.TextChanged += this.OnColumnChanged;
				this.dataTextFormatStringEdit.Name = "ButtonColumDataTextFormatStringEdit";
				label4.SetBounds(200, 0, 160, 14);
				label4.Text = SR.GetString("DGCol_BC_Command");
				label4.TabStop = false;
				label4.TabIndex = 8;
				label4.Name = "ButtonColumnCommandLabel";
				this.commandEdit.SetBounds(200, 16, 182, 24);
				this.commandEdit.TabIndex = 9;
				this.commandEdit.TextChanged += this.OnColumnChanged;
				this.commandEdit.Name = "ButtonColumnCommandEdit";
				label5.SetBounds(200, 40, 160, 14);
				label5.Text = SR.GetString("DGCol_BC_ButtonType");
				label5.TabStop = false;
				label5.TabIndex = 10;
				label5.Name = "ButtonColumnButtonTypeLabel";
				this.buttonTypeCombo.SetBounds(200, 56, 182, 21);
				this.buttonTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
				this.buttonTypeCombo.Items.AddRange(new object[]
				{
					SR.GetString("DGCol_BC_BT_Link"),
					SR.GetString("DGCol_BC_BT_Push")
				});
				this.buttonTypeCombo.TabIndex = 11;
				this.buttonTypeCombo.SelectedIndexChanged += this.OnColumnChanged;
				this.buttonTypeCombo.Name = "ButtonColumnButtonTypeCombo";
				base.Controls.Clear();
				base.Controls.AddRange(new Control[]
				{
					this.buttonTypeCombo,
					label5,
					this.commandEdit,
					label4,
					this.dataTextFormatStringEdit,
					label3,
					this.dataTextFieldEdit,
					this.dataTextFieldCombo,
					label2,
					this.textEdit,
					label
				});
			}

			// Token: 0x06002EBC RID: 11964 RVA: 0x00108D84 File Offset: 0x00107D84
			public override void LoadColumn(DataGridColumnsPage.ColumnItem columnItem)
			{
				base.LoadColumn(columnItem);
				DataGridColumnsPage.ButtonColumnItem buttonColumnItem = (DataGridColumnsPage.ButtonColumnItem)this.columnItem;
				this.commandEdit.Text = buttonColumnItem.Command;
				this.textEdit.Text = buttonColumnItem.ButtonText;
				if (this.dataFieldsAvailable)
				{
					if (buttonColumnItem.ButtonDataTextField != null)
					{
						int selectedIndex = this.dataTextFieldCombo.FindStringExact(buttonColumnItem.ButtonDataTextField);
						this.dataTextFieldCombo.SelectedIndex = selectedIndex;
					}
					this.dataTextFieldCombo.Visible = true;
					this.dataTextFieldEdit.Visible = false;
				}
				else
				{
					this.dataTextFieldEdit.Text = buttonColumnItem.ButtonDataTextField;
					this.dataTextFieldEdit.Visible = true;
					this.dataTextFieldCombo.Visible = false;
				}
				this.dataTextFormatStringEdit.Text = buttonColumnItem.ButtonDataTextFormatString;
				switch (buttonColumnItem.ButtonType)
				{
				case ButtonColumnType.LinkButton:
					this.buttonTypeCombo.SelectedIndex = 0;
					break;
				case ButtonColumnType.PushButton:
					this.buttonTypeCombo.SelectedIndex = 1;
					break;
				}
				this.UpdateEnabledState();
			}

			// Token: 0x06002EBD RID: 11965 RVA: 0x00108E7F File Offset: 0x00107E7F
			private void OnColumnChanged(object source, EventArgs e)
			{
				this.OnChanged(EventArgs.Empty);
				if (source == this.dataTextFieldCombo || source == this.dataTextFieldEdit)
				{
					this.UpdateEnabledState();
				}
			}

			// Token: 0x06002EBE RID: 11966 RVA: 0x00108EA4 File Offset: 0x00107EA4
			public override void SaveColumn()
			{
				base.SaveColumn();
				DataGridColumnsPage.ButtonColumnItem buttonColumnItem = (DataGridColumnsPage.ButtonColumnItem)this.columnItem;
				buttonColumnItem.Command = this.commandEdit.Text.Trim();
				buttonColumnItem.ButtonText = this.textEdit.Text;
				if (this.dataFieldsAvailable)
				{
					if (this.dataTextFieldCombo.IsSet())
					{
						buttonColumnItem.ButtonDataTextField = this.dataTextFieldCombo.Text;
					}
					else
					{
						buttonColumnItem.ButtonDataTextField = string.Empty;
					}
				}
				else
				{
					buttonColumnItem.ButtonDataTextField = this.dataTextFieldEdit.Text.Trim();
				}
				buttonColumnItem.ButtonDataTextFormatString = this.dataTextFormatStringEdit.Text;
				switch (this.buttonTypeCombo.SelectedIndex)
				{
				case 0:
					buttonColumnItem.ButtonType = ButtonColumnType.LinkButton;
					return;
				case 1:
					buttonColumnItem.ButtonType = ButtonColumnType.PushButton;
					return;
				default:
					return;
				}
			}

			// Token: 0x06002EBF RID: 11967 RVA: 0x00108F70 File Offset: 0x00107F70
			private void UpdateEnabledState()
			{
				if (this.dataFieldsAvailable)
				{
					this.dataTextFormatStringEdit.Enabled = this.dataTextFieldCombo.IsSet();
					return;
				}
				this.dataTextFormatStringEdit.Enabled = (this.dataTextFieldEdit.Text.Trim().Length != 0);
			}

			// Token: 0x04001FAA RID: 8106
			private const int IDX_TYPE_LINKBUTTON = 0;

			// Token: 0x04001FAB RID: 8107
			private const int IDX_TYPE_PUSHBUTTON = 1;

			// Token: 0x04001FAC RID: 8108
			private System.Windows.Forms.TextBox commandEdit;

			// Token: 0x04001FAD RID: 8109
			private System.Windows.Forms.TextBox textEdit;

			// Token: 0x04001FAE RID: 8110
			private UnsettableComboBox dataTextFieldCombo;

			// Token: 0x04001FAF RID: 8111
			private System.Windows.Forms.TextBox dataTextFieldEdit;

			// Token: 0x04001FB0 RID: 8112
			private System.Windows.Forms.TextBox dataTextFormatStringEdit;

			// Token: 0x04001FB1 RID: 8113
			private ComboBox buttonTypeCombo;
		}

		// Token: 0x02000520 RID: 1312
		private class HyperLinkColumnEditor : DataGridColumnsPage.ColumnItemEditor
		{
			// Token: 0x06002EC1 RID: 11969 RVA: 0x00108FCA File Offset: 0x00107FCA
			public override void AddDataField(string fieldName)
			{
				this.dataTextFieldCombo.AddItem(fieldName);
				this.dataUrlFieldCombo.AddItem(fieldName);
				base.AddDataField(fieldName);
			}

			// Token: 0x06002EC2 RID: 11970 RVA: 0x00108FEB File Offset: 0x00107FEB
			public override void ClearDataFields()
			{
				this.dataTextFieldCombo.Items.Clear();
				this.dataUrlFieldCombo.Items.Clear();
				this.dataTextFieldCombo.EnsureNotSetItem();
				this.dataUrlFieldCombo.EnsureNotSetItem();
				base.ClearDataFields();
			}

			// Token: 0x06002EC3 RID: 11971 RVA: 0x0010902C File Offset: 0x0010802C
			protected override void InitPanel()
			{
				System.Windows.Forms.Label label = new System.Windows.Forms.Label();
				this.textEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
				this.dataTextFieldCombo = new UnsettableComboBox();
				this.dataTextFieldEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label3 = new System.Windows.Forms.Label();
				this.dataTextFormatStringEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label4 = new System.Windows.Forms.Label();
				this.targetCombo = new ComboBox();
				System.Windows.Forms.Label label5 = new System.Windows.Forms.Label();
				this.urlEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label6 = new System.Windows.Forms.Label();
				this.dataUrlFieldCombo = new UnsettableComboBox();
				this.dataUrlFieldEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label7 = new System.Windows.Forms.Label();
				this.dataUrlFormatStringEdit = new System.Windows.Forms.TextBox();
				label.SetBounds(0, 0, 160, 14);
				label.Text = SR.GetString("DGCol_HC_Text");
				label.TabStop = false;
				label.TabIndex = 1;
				label.Name = "HyperlinkColumnTextLabel";
				this.textEdit.SetBounds(0, 16, 182, 24);
				this.textEdit.TabIndex = 2;
				this.textEdit.TextChanged += this.OnColumnChanged;
				this.textEdit.Name = "HyperlinkColumnTextEdit";
				label2.SetBounds(0, 40, 160, 14);
				label2.Text = SR.GetString("DGCol_HC_DataTextField");
				label2.TabStop = false;
				label2.TabIndex = 3;
				label2.Name = "HyperlinkColumnDataTextFieldLabel";
				this.dataTextFieldCombo.SetBounds(0, 56, 182, 21);
				this.dataTextFieldCombo.DropDownStyle = ComboBoxStyle.DropDownList;
				this.dataTextFieldCombo.TabIndex = 4;
				this.dataTextFieldCombo.SelectedIndexChanged += this.OnColumnChanged;
				this.dataTextFieldCombo.Name = "HyperlinkColumnDataTextFieldCombo";
				this.dataTextFieldEdit.SetBounds(0, 56, 182, 14);
				this.dataTextFieldEdit.TabIndex = 4;
				this.dataTextFieldEdit.TextChanged += this.OnColumnChanged;
				this.dataTextFieldEdit.Name = "HyperlinkColumnDataTextFieldEdit";
				label3.SetBounds(0, 82, 160, 14);
				label3.Text = SR.GetString("DGCol_HC_DataTextFormat");
				label3.TabStop = false;
				label3.TabIndex = 5;
				label3.Name = "HyperlinkColumnDataTextFormatStringLabel";
				this.dataTextFormatStringEdit.SetBounds(0, 98, 182, 21);
				this.dataTextFormatStringEdit.TabIndex = 6;
				this.dataTextFormatStringEdit.TextChanged += this.OnColumnChanged;
				this.dataTextFormatStringEdit.Name = "HyperlinkColumnDataTextFormatStringEdit";
				label4.SetBounds(0, 123, 160, 14);
				label4.Text = SR.GetString("DGCol_HC_Target");
				label4.TabStop = false;
				label4.TabIndex = 7;
				label4.Name = "HyperlinkColumnTargetLabel";
				this.targetCombo.SetBounds(0, 139, 182, 21);
				this.targetCombo.TabIndex = 8;
				this.targetCombo.Items.AddRange(new object[]
				{
					"_blank",
					"_parent",
					"_search",
					"_self",
					"_top"
				});
				this.targetCombo.SelectedIndexChanged += this.OnColumnChanged;
				this.targetCombo.TextChanged += this.OnColumnChanged;
				this.targetCombo.Name = "HyperlinkColumnTargetCombo";
				label5.SetBounds(200, 0, 160, 14);
				label5.Text = SR.GetString("DGCol_HC_URL");
				label5.TabStop = false;
				label5.TabIndex = 10;
				label5.Name = "HyperlinkColumnUrlLabel";
				this.urlEdit.SetBounds(200, 16, 182, 24);
				this.urlEdit.TabIndex = 11;
				this.urlEdit.TextChanged += this.OnColumnChanged;
				this.urlEdit.Name = "HyperlinkColumnUrlEdit";
				label6.SetBounds(200, 40, 160, 14);
				label6.Text = SR.GetString("DGCol_HC_DataURLField");
				label6.TabStop = false;
				label6.TabIndex = 12;
				label6.Name = "HyperlinkColumnDataUrlFieldLabel";
				this.dataUrlFieldCombo.SetBounds(200, 56, 182, 21);
				this.dataUrlFieldCombo.DropDownStyle = ComboBoxStyle.DropDownList;
				this.dataUrlFieldCombo.TabIndex = 13;
				this.dataUrlFieldCombo.SelectedIndexChanged += this.OnColumnChanged;
				this.dataUrlFieldCombo.Name = "HyperlinkColumnDataUrlFieldCombo";
				this.dataUrlFieldEdit.SetBounds(200, 56, 182, 14);
				this.dataUrlFieldEdit.TabIndex = 13;
				this.dataUrlFieldEdit.TextChanged += this.OnColumnChanged;
				this.dataUrlFieldEdit.Name = "HyperlinkColumnDataUrlFieldEdit";
				label7.SetBounds(200, 82, 160, 14);
				label7.Text = SR.GetString("DGCol_HC_DataURLFormat");
				label7.TabStop = false;
				label7.TabIndex = 14;
				label7.Name = "HyperlinkColumnDataUrlFormatStringLabel";
				this.dataUrlFormatStringEdit.SetBounds(200, 98, 182, 21);
				this.dataUrlFormatStringEdit.TabIndex = 15;
				this.dataUrlFormatStringEdit.TextChanged += this.OnColumnChanged;
				this.dataUrlFormatStringEdit.Name = "HyperlinkColumnDataUrlFormatStringEdit";
				base.Controls.Clear();
				base.Controls.AddRange(new Control[]
				{
					this.dataUrlFormatStringEdit,
					label7,
					this.dataUrlFieldEdit,
					this.dataUrlFieldCombo,
					label6,
					this.urlEdit,
					label5,
					this.targetCombo,
					label4,
					this.dataTextFormatStringEdit,
					label3,
					this.dataTextFieldEdit,
					this.dataTextFieldCombo,
					label2,
					this.textEdit,
					label
				});
			}

			// Token: 0x06002EC4 RID: 11972 RVA: 0x00109634 File Offset: 0x00108634
			public override void LoadColumn(DataGridColumnsPage.ColumnItem columnItem)
			{
				base.LoadColumn(columnItem);
				DataGridColumnsPage.HyperLinkColumnItem hyperLinkColumnItem = (DataGridColumnsPage.HyperLinkColumnItem)this.columnItem;
				this.textEdit.Text = hyperLinkColumnItem.AnchorText;
				if (this.dataFieldsAvailable)
				{
					if (hyperLinkColumnItem.AnchorDataTextField != null)
					{
						int selectedIndex = this.dataTextFieldCombo.FindStringExact(hyperLinkColumnItem.AnchorDataTextField);
						this.dataTextFieldCombo.SelectedIndex = selectedIndex;
					}
					this.dataTextFieldCombo.Visible = true;
					this.dataTextFieldEdit.Visible = false;
				}
				else
				{
					this.dataTextFieldEdit.Text = hyperLinkColumnItem.AnchorDataTextField;
					this.dataTextFieldEdit.Visible = true;
					this.dataTextFieldCombo.Visible = false;
				}
				this.dataTextFormatStringEdit.Text = hyperLinkColumnItem.AnchorDataTextFormatString;
				this.urlEdit.Text = hyperLinkColumnItem.Url;
				if (this.dataFieldsAvailable)
				{
					if (hyperLinkColumnItem.DataUrlField != null)
					{
						int selectedIndex2 = this.dataTextFieldCombo.FindStringExact(hyperLinkColumnItem.DataUrlField);
						this.dataUrlFieldCombo.SelectedIndex = selectedIndex2;
					}
					this.dataUrlFieldCombo.Visible = true;
					this.dataUrlFieldEdit.Visible = false;
				}
				else
				{
					this.dataUrlFieldEdit.Text = hyperLinkColumnItem.DataUrlField;
					this.dataUrlFieldEdit.Visible = true;
					this.dataUrlFieldCombo.Visible = false;
				}
				this.dataUrlFormatStringEdit.Text = hyperLinkColumnItem.DataUrlFormatString;
				this.targetCombo.Text = hyperLinkColumnItem.Target;
				this.UpdateEnabledState();
			}

			// Token: 0x06002EC5 RID: 11973 RVA: 0x00109791 File Offset: 0x00108791
			protected void OnColumnChanged(object source, EventArgs e)
			{
				this.OnChanged(EventArgs.Empty);
				if (source == this.dataTextFieldCombo || source == this.dataUrlFieldCombo || source == this.dataTextFieldEdit || source == this.dataUrlFieldEdit)
				{
					this.UpdateEnabledState();
				}
			}

			// Token: 0x06002EC6 RID: 11974 RVA: 0x001097C8 File Offset: 0x001087C8
			public override void SaveColumn()
			{
				base.SaveColumn();
				DataGridColumnsPage.HyperLinkColumnItem hyperLinkColumnItem = (DataGridColumnsPage.HyperLinkColumnItem)this.columnItem;
				hyperLinkColumnItem.AnchorText = this.textEdit.Text;
				if (this.dataFieldsAvailable)
				{
					if (this.dataTextFieldCombo.IsSet())
					{
						hyperLinkColumnItem.AnchorDataTextField = this.dataTextFieldCombo.Text;
					}
					else
					{
						hyperLinkColumnItem.AnchorDataTextField = string.Empty;
					}
				}
				else
				{
					hyperLinkColumnItem.AnchorDataTextField = this.dataTextFieldEdit.Text.Trim();
				}
				hyperLinkColumnItem.AnchorDataTextFormatString = this.dataTextFormatStringEdit.Text;
				hyperLinkColumnItem.Url = this.urlEdit.Text.Trim();
				if (this.dataFieldsAvailable)
				{
					if (this.dataUrlFieldCombo.IsSet())
					{
						hyperLinkColumnItem.DataUrlField = this.dataUrlFieldCombo.Text;
					}
					else
					{
						hyperLinkColumnItem.DataUrlField = string.Empty;
					}
				}
				else
				{
					hyperLinkColumnItem.DataUrlField = this.dataUrlFieldEdit.Text.Trim();
				}
				hyperLinkColumnItem.DataUrlFormatString = this.dataUrlFormatStringEdit.Text;
				hyperLinkColumnItem.Target = this.targetCombo.Text.Trim();
			}

			// Token: 0x06002EC7 RID: 11975 RVA: 0x001098DC File Offset: 0x001088DC
			private void UpdateEnabledState()
			{
				if (this.dataFieldsAvailable)
				{
					this.dataTextFormatStringEdit.Enabled = this.dataTextFieldCombo.IsSet();
					this.dataUrlFormatStringEdit.Enabled = this.dataUrlFieldCombo.IsSet();
					return;
				}
				this.dataTextFormatStringEdit.Enabled = (this.dataTextFieldEdit.Text.Trim().Length != 0);
				this.dataUrlFormatStringEdit.Enabled = (this.dataUrlFieldEdit.Text.Trim().Length != 0);
			}

			// Token: 0x04001FB2 RID: 8114
			private System.Windows.Forms.TextBox textEdit;

			// Token: 0x04001FB3 RID: 8115
			private UnsettableComboBox dataTextFieldCombo;

			// Token: 0x04001FB4 RID: 8116
			private System.Windows.Forms.TextBox dataTextFieldEdit;

			// Token: 0x04001FB5 RID: 8117
			private System.Windows.Forms.TextBox dataTextFormatStringEdit;

			// Token: 0x04001FB6 RID: 8118
			private System.Windows.Forms.TextBox urlEdit;

			// Token: 0x04001FB7 RID: 8119
			private UnsettableComboBox dataUrlFieldCombo;

			// Token: 0x04001FB8 RID: 8120
			private System.Windows.Forms.TextBox dataUrlFieldEdit;

			// Token: 0x04001FB9 RID: 8121
			private System.Windows.Forms.TextBox dataUrlFormatStringEdit;

			// Token: 0x04001FBA RID: 8122
			private ComboBox targetCombo;
		}

		// Token: 0x02000521 RID: 1313
		private class EditCommandColumnEditor : DataGridColumnsPage.ColumnItemEditor
		{
			// Token: 0x06002EC9 RID: 11977 RVA: 0x00109974 File Offset: 0x00108974
			protected override void InitPanel()
			{
				System.Windows.Forms.Label label = new System.Windows.Forms.Label();
				this.editTextEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
				this.updateTextEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label3 = new System.Windows.Forms.Label();
				this.cancelTextEdit = new System.Windows.Forms.TextBox();
				System.Windows.Forms.Label label4 = new System.Windows.Forms.Label();
				this.buttonTypeCombo = new ComboBox();
				label.SetBounds(0, 0, 160, 14);
				label.Text = SR.GetString("DGCol_EC_Edit");
				label.TabStop = false;
				label.TabIndex = 1;
				label.Name = "EditColumnEditTextLabel";
				this.editTextEdit.SetBounds(0, 16, 182, 24);
				this.editTextEdit.TabIndex = 2;
				this.editTextEdit.TextChanged += this.OnColumnChanged;
				this.editTextEdit.Name = "EditColumnEditTextEdit";
				label2.SetBounds(0, 40, 160, 14);
				label2.Text = SR.GetString("DGCol_EC_Update");
				label2.TabStop = false;
				label2.TabIndex = 3;
				label2.Name = "EditColumnUpdateTextLabel";
				this.updateTextEdit.SetBounds(0, 56, 182, 24);
				this.updateTextEdit.TabIndex = 4;
				this.updateTextEdit.TextChanged += this.OnColumnChanged;
				this.updateTextEdit.Name = "EditColumnUpdateTextEdit";
				label3.SetBounds(200, 0, 160, 14);
				label3.Text = SR.GetString("DGCol_EC_Cancel");
				label3.TabStop = false;
				label3.TabIndex = 5;
				label3.Name = "EditColumnCancelTextLabel";
				this.cancelTextEdit.SetBounds(200, 16, 182, 24);
				this.cancelTextEdit.TabIndex = 6;
				this.cancelTextEdit.TextChanged += this.OnColumnChanged;
				this.cancelTextEdit.Name = "EditColumnCancelTextEdit";
				label4.SetBounds(200, 40, 160, 14);
				label4.Text = SR.GetString("DGCol_EC_ButtonType");
				label4.TabStop = false;
				label4.TabIndex = 7;
				label4.Name = "EditColumnButtonTypeLabel";
				this.buttonTypeCombo.SetBounds(200, 56, 182, 21);
				this.buttonTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
				this.buttonTypeCombo.Items.AddRange(new object[]
				{
					SR.GetString("DGCol_EC_BT_Link"),
					SR.GetString("DGCol_EC_BT_Push")
				});
				this.buttonTypeCombo.TabIndex = 8;
				this.buttonTypeCombo.SelectedIndexChanged += this.OnColumnChanged;
				this.buttonTypeCombo.Name = "EditColumnButtonTypeCombo";
				base.Controls.Clear();
				base.Controls.AddRange(new Control[]
				{
					this.buttonTypeCombo,
					label4,
					this.cancelTextEdit,
					label3,
					this.updateTextEdit,
					label2,
					this.editTextEdit,
					label
				});
			}

			// Token: 0x06002ECA RID: 11978 RVA: 0x00109C78 File Offset: 0x00108C78
			public override void LoadColumn(DataGridColumnsPage.ColumnItem columnItem)
			{
				base.LoadColumn(columnItem);
				DataGridColumnsPage.EditCommandColumnItem editCommandColumnItem = (DataGridColumnsPage.EditCommandColumnItem)this.columnItem;
				this.editTextEdit.Text = editCommandColumnItem.EditText;
				this.updateTextEdit.Text = editCommandColumnItem.UpdateText;
				this.cancelTextEdit.Text = editCommandColumnItem.CancelText;
				switch (editCommandColumnItem.ButtonType)
				{
				case ButtonColumnType.LinkButton:
					this.buttonTypeCombo.SelectedIndex = 0;
					return;
				case ButtonColumnType.PushButton:
					this.buttonTypeCombo.SelectedIndex = 1;
					return;
				default:
					return;
				}
			}

			// Token: 0x06002ECB RID: 11979 RVA: 0x00109CFA File Offset: 0x00108CFA
			private void OnColumnChanged(object source, EventArgs e)
			{
				this.OnChanged(EventArgs.Empty);
			}

			// Token: 0x06002ECC RID: 11980 RVA: 0x00109D08 File Offset: 0x00108D08
			public override void SaveColumn()
			{
				base.SaveColumn();
				DataGridColumnsPage.EditCommandColumnItem editCommandColumnItem = (DataGridColumnsPage.EditCommandColumnItem)this.columnItem;
				editCommandColumnItem.EditText = this.editTextEdit.Text;
				editCommandColumnItem.UpdateText = this.updateTextEdit.Text;
				editCommandColumnItem.CancelText = this.cancelTextEdit.Text;
				switch (this.buttonTypeCombo.SelectedIndex)
				{
				case 0:
					editCommandColumnItem.ButtonType = ButtonColumnType.LinkButton;
					return;
				case 1:
					editCommandColumnItem.ButtonType = ButtonColumnType.PushButton;
					return;
				default:
					return;
				}
			}

			// Token: 0x04001FBB RID: 8123
			private const int IDX_TYPE_LINKBUTTON = 0;

			// Token: 0x04001FBC RID: 8124
			private const int IDX_TYPE_PUSHBUTTON = 1;

			// Token: 0x04001FBD RID: 8125
			private System.Windows.Forms.TextBox editTextEdit;

			// Token: 0x04001FBE RID: 8126
			private System.Windows.Forms.TextBox updateTextEdit;

			// Token: 0x04001FBF RID: 8127
			private System.Windows.Forms.TextBox cancelTextEdit;

			// Token: 0x04001FC0 RID: 8128
			private ComboBox buttonTypeCombo;
		}
	}
}
