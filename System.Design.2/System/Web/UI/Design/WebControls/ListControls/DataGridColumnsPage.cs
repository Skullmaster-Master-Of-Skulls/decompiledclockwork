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
	// Token: 0x02000159 RID: 345
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class DataGridColumnsPage : BaseDataListPage
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0004C243 File Offset: 0x0004A443
		protected override string HelpKeyword
		{
			get
			{
				return "net.Asp.DataGridProperties.Columns";
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0004C24C File Offset: 0x0004A44C
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
			System.Drawing.Image value = BitmapSelector.CreateBitmap(base.GetType(), "ColumnNodes.bmp");
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
			Bitmap bitmap = BitmapSelector.CreateIcon(base.GetType(), "SortUp.ico").ToBitmap();
			bitmap.MakeTransparent();
			this.moveColumnUpButton.Image = bitmap;
			this.moveColumnUpButton.Click += this.OnClickMoveColumnUp;
			this.moveColumnUpButton.Name = "MoveColumnUpButton";
			this.moveColumnUpButton.AccessibleName = SR.GetString("DGCol_MoveColumnUpButtonDesc");
			this.moveColumnDownButton.SetBounds(406, 88, 28, 27);
			this.moveColumnDownButton.TabIndex = 8;
			Bitmap bitmap2 = BitmapSelector.CreateIcon(base.GetType(), "SortDown.ico").ToBitmap();
			bitmap2.MakeTransparent();
			this.moveColumnDownButton.Image = bitmap2;
			this.moveColumnDownButton.Click += this.OnClickMoveColumnDown;
			this.moveColumnDownButton.Name = "MoveColumnDownButton";
			this.moveColumnDownButton.AccessibleName = SR.GetString("DGCol_MoveColumnDownButtonDesc");
			this.deleteColumnButton.SetBounds(406, 118, 28, 27);
			this.deleteColumnButton.TabIndex = 9;
			Bitmap bitmap3 = BitmapSelector.CreateIcon(base.GetType(), "Delete.ico").ToBitmap();
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
			base.Icon = BitmapSelector.CreateIcon(base.GetType(), "DataGridColumnsPage.ico");
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

		// Token: 0x06000C07 RID: 3079 RVA: 0x0004CEB0 File Offset: 0x0004B0B0
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

		// Token: 0x06000C08 RID: 3080 RVA: 0x0004CF50 File Offset: 0x0004B150
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

		// Token: 0x06000C09 RID: 3081 RVA: 0x0004D118 File Offset: 0x0004B318
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

		// Token: 0x06000C0A RID: 3082 RVA: 0x0004D240 File Offset: 0x0004B440
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

		// Token: 0x06000C0B RID: 3083 RVA: 0x0004D290 File Offset: 0x0004B490
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

		// Token: 0x06000C0C RID: 3084 RVA: 0x0004D340 File Offset: 0x0004B540
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

		// Token: 0x06000C0D RID: 3085 RVA: 0x0004D480 File Offset: 0x0004B680
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

		// Token: 0x06000C0E RID: 3086 RVA: 0x0004D56E File Offset: 0x0004B76E
		private void OnChangedColumnProperties(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.propChangesPending = true;
			this.SetDirty();
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0004D586 File Offset: 0x0004B786
		private void OnCheckChangedAutoColumn(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.SetDirty();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0004D5A0 File Offset: 0x0004B7A0
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

		// Token: 0x06000C11 RID: 3089 RVA: 0x0004D684 File Offset: 0x0004B884
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

		// Token: 0x06000C12 RID: 3090 RVA: 0x0004D6E8 File Offset: 0x0004B8E8
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

		// Token: 0x06000C13 RID: 3091 RVA: 0x0004D784 File Offset: 0x0004B984
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

		// Token: 0x06000C14 RID: 3092 RVA: 0x0004D824 File Offset: 0x0004BA24
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

		// Token: 0x06000C15 RID: 3093 RVA: 0x0004D8C4 File Offset: 0x0004BAC4
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

		// Token: 0x06000C16 RID: 3094 RVA: 0x0004D952 File Offset: 0x0004BB52
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

		// Token: 0x06000C17 RID: 3095 RVA: 0x0004D981 File Offset: 0x0004BB81
		private void OnSelChangedAvailableColumns(object source, TreeViewEventArgs e)
		{
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0004D989 File Offset: 0x0004BB89
		private void OnSelColumnsListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete && this.currentColumnItem != null)
			{
				this.OnClickDeleteColumn(sender, e);
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0004D9A8 File Offset: 0x0004BBA8
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

		// Token: 0x06000C1A RID: 3098 RVA: 0x0004DA06 File Offset: 0x0004BC06
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

		// Token: 0x06000C1B RID: 3099 RVA: 0x0004DA28 File Offset: 0x0004BC28
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

		// Token: 0x06000C1C RID: 3100 RVA: 0x0004DAD4 File Offset: 0x0004BCD4
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

		// Token: 0x06000C1D RID: 3101 RVA: 0x0004DB79 File Offset: 0x0004BD79
		public override void SetComponent(IComponent component)
		{
			base.SetComponent(component);
			this.InitForm();
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0004DB88 File Offset: 0x0004BD88
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

		// Token: 0x04000724 RID: 1828
		private const int ILI_DATASOURCE = 0;

		// Token: 0x04000725 RID: 1829
		private const int ILI_BOUND = 1;

		// Token: 0x04000726 RID: 1830
		private const int ILI_ALL = 2;

		// Token: 0x04000727 RID: 1831
		private const int ILI_CUSTOM = 3;

		// Token: 0x04000728 RID: 1832
		private const int ILI_BUTTON = 4;

		// Token: 0x04000729 RID: 1833
		private const int ILI_SELECTBUTTON = 5;

		// Token: 0x0400072A RID: 1834
		private const int ILI_EDITBUTTON = 6;

		// Token: 0x0400072B RID: 1835
		private const int ILI_DELETEBUTTON = 7;

		// Token: 0x0400072C RID: 1836
		private const int ILI_HYPERLINK = 8;

		// Token: 0x0400072D RID: 1837
		private const int ILI_TEMPLATE = 9;

		// Token: 0x0400072E RID: 1838
		private System.Windows.Forms.CheckBox autoColumnCheck;

		// Token: 0x0400072F RID: 1839
		private System.Windows.Forms.TreeView availableColumnsTree;

		// Token: 0x04000730 RID: 1840
		private System.Windows.Forms.Button addColumnButton;

		// Token: 0x04000731 RID: 1841
		private ListView selColumnsList;

		// Token: 0x04000732 RID: 1842
		private System.Windows.Forms.Button moveColumnUpButton;

		// Token: 0x04000733 RID: 1843
		private System.Windows.Forms.Button moveColumnDownButton;

		// Token: 0x04000734 RID: 1844
		private System.Windows.Forms.Button deleteColumnButton;

		// Token: 0x04000735 RID: 1845
		private GroupLabel columnPropsGroup;

		// Token: 0x04000736 RID: 1846
		private System.Windows.Forms.TextBox columnHeaderTextEdit;

		// Token: 0x04000737 RID: 1847
		private System.Windows.Forms.TextBox columnHeaderImageEdit;

		// Token: 0x04000738 RID: 1848
		private System.Windows.Forms.TextBox columnFooterTextEdit;

		// Token: 0x04000739 RID: 1849
		private ComboBox columnSortExprCombo;

		// Token: 0x0400073A RID: 1850
		private System.Windows.Forms.CheckBox columnVisibleCheck;

		// Token: 0x0400073B RID: 1851
		private System.Windows.Forms.Button columnHeaderImagePickerButton;

		// Token: 0x0400073C RID: 1852
		private LinkLabel templatizeLink;

		// Token: 0x0400073D RID: 1853
		private DataGridColumnsPage.BoundColumnEditor boundColumnEditor;

		// Token: 0x0400073E RID: 1854
		private DataGridColumnsPage.ButtonColumnEditor buttonColumnEditor;

		// Token: 0x0400073F RID: 1855
		private DataGridColumnsPage.HyperLinkColumnEditor hyperLinkColumnEditor;

		// Token: 0x04000740 RID: 1856
		private DataGridColumnsPage.EditCommandColumnEditor editCommandColumnEditor;

		// Token: 0x04000741 RID: 1857
		private BaseDataListPage.DataSourceItem currentDataSource;

		// Token: 0x04000742 RID: 1858
		private DataGridColumnsPage.DataSourceNode selectedDataSourceNode;

		// Token: 0x04000743 RID: 1859
		private DataGridColumnsPage.ColumnItem currentColumnItem;

		// Token: 0x04000744 RID: 1860
		private DataGridColumnsPage.ColumnItemEditor currentColumnEditor;

		// Token: 0x04000745 RID: 1861
		private bool propChangesPending;

		// Token: 0x04000746 RID: 1862
		private bool headerTextChanged;

		// Token: 0x02000465 RID: 1125
		private abstract class AvailableColumnNode : System.Windows.Forms.TreeNode
		{
			// Token: 0x060029A3 RID: 10659 RVA: 0x000F162E File Offset: 0x000EF82E
			public AvailableColumnNode(string text, int icon) : base(text, icon, icon)
			{
			}

			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x060029A4 RID: 10660 RVA: 0x0000445B File Offset: 0x0000265B
			public virtual bool CreatesMultipleColumns
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170008D4 RID: 2260
			// (get) Token: 0x060029A5 RID: 10661 RVA: 0x00003B0F File Offset: 0x00001D0F
			public virtual bool IsColumnCreator
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060029A6 RID: 10662 RVA: 0x00003598 File Offset: 0x00001798
			public virtual DataGridColumnsPage.ColumnItem CreateColumn()
			{
				return null;
			}

			// Token: 0x060029A7 RID: 10663 RVA: 0x00003598 File Offset: 0x00001798
			public virtual DataGridColumnsPage.ColumnItem[] CreateColumns(PropertyDescriptorCollection fields)
			{
				return null;
			}
		}

		// Token: 0x02000466 RID: 1126
		private class DataSourceNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x060029A8 RID: 10664 RVA: 0x000FACF2 File Offset: 0x000F8EF2
			public DataSourceNode() : base(SR.GetString("DGCol_Node_DataFields"), 0)
			{
			}

			// Token: 0x170008D5 RID: 2261
			// (get) Token: 0x060029A9 RID: 10665 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool IsColumnCreator
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x02000467 RID: 1127
		private class DataFieldNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x060029AA RID: 10666 RVA: 0x000FAD05 File Offset: 0x000F8F05
			public DataFieldNode() : base(SR.GetString("DGCol_Node_AllFields"), 2)
			{
				this.fieldName = null;
				this.allFields = true;
			}

			// Token: 0x060029AB RID: 10667 RVA: 0x000FAD26 File Offset: 0x000F8F26
			public DataFieldNode(string fieldName) : base(fieldName, 1)
			{
				this.fieldName = fieldName;
				if (fieldName == null)
				{
					this.genericBoundColumn = true;
					base.Text = SR.GetString("DGCol_Node_Bound");
				}
			}

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x060029AC RID: 10668 RVA: 0x000FAD51 File Offset: 0x000F8F51
			public override bool CreatesMultipleColumns
			{
				get
				{
					return this.allFields;
				}
			}

			// Token: 0x060029AD RID: 10669 RVA: 0x000FAD5C File Offset: 0x000F8F5C
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

			// Token: 0x060029AE RID: 10670 RVA: 0x000FADAC File Offset: 0x000F8FAC
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

			// Token: 0x04001D59 RID: 7513
			protected string fieldName;

			// Token: 0x04001D5A RID: 7514
			private bool genericBoundColumn;

			// Token: 0x04001D5B RID: 7515
			private bool allFields;
		}

		// Token: 0x02000468 RID: 1128
		private class ButtonNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x060029AF RID: 10671 RVA: 0x000FAE34 File Offset: 0x000F9034
			public ButtonNode() : this(string.Empty, SR.GetString("DGCol_Button"), SR.GetString("DGCol_Node_Button"))
			{
			}

			// Token: 0x060029B0 RID: 10672 RVA: 0x000FAE55 File Offset: 0x000F9055
			public ButtonNode(string command, string buttonText, string text) : base(text, 4)
			{
				this.command = command;
				this.buttonText = buttonText;
			}

			// Token: 0x060029B1 RID: 10673 RVA: 0x000FAE70 File Offset: 0x000F9070
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

			// Token: 0x04001D5C RID: 7516
			private string command;

			// Token: 0x04001D5D RID: 7517
			private string buttonText;
		}

		// Token: 0x02000469 RID: 1129
		private class EditCommandNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x060029B2 RID: 10674 RVA: 0x000FAEA9 File Offset: 0x000F90A9
			public EditCommandNode() : base(SR.GetString("DGCol_Node_Edit"), 4)
			{
			}

			// Token: 0x060029B3 RID: 10675 RVA: 0x000FAEBC File Offset: 0x000F90BC
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

		// Token: 0x0200046A RID: 1130
		private class HyperLinkNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x060029B4 RID: 10676 RVA: 0x000FAF0D File Offset: 0x000F910D
			public HyperLinkNode() : this(SR.GetString("DGCol_HyperLink"))
			{
			}

			// Token: 0x060029B5 RID: 10677 RVA: 0x000FAF1F File Offset: 0x000F911F
			public HyperLinkNode(string hyperLinkText) : base(SR.GetString("DGCol_Node_HyperLink"), 8)
			{
				this.hyperLinkText = hyperLinkText;
			}

			// Token: 0x060029B6 RID: 10678 RVA: 0x000FAF3C File Offset: 0x000F913C
			public override DataGridColumnsPage.ColumnItem CreateColumn()
			{
				HyperLinkColumn runtimeColumn = new HyperLinkColumn();
				DataGridColumnsPage.ColumnItem columnItem = new DataGridColumnsPage.HyperLinkColumnItem(runtimeColumn);
				columnItem.Text = this.hyperLinkText;
				columnItem.LoadColumnInfo();
				return columnItem;
			}

			// Token: 0x04001D5E RID: 7518
			private string hyperLinkText;
		}

		// Token: 0x0200046B RID: 1131
		private class TemplateNode : DataGridColumnsPage.AvailableColumnNode
		{
			// Token: 0x060029B7 RID: 10679 RVA: 0x000FAF69 File Offset: 0x000F9169
			public TemplateNode() : base(SR.GetString("DGCol_Node_Template"), 9)
			{
			}

			// Token: 0x060029B8 RID: 10680 RVA: 0x000FAF80 File Offset: 0x000F9180
			public override DataGridColumnsPage.ColumnItem CreateColumn()
			{
				TemplateColumn runtimeColumn = new TemplateColumn();
				DataGridColumnsPage.ColumnItem columnItem = new DataGridColumnsPage.TemplateColumnItem(runtimeColumn);
				columnItem.LoadColumnInfo();
				return columnItem;
			}
		}

		// Token: 0x0200046C RID: 1132
		private abstract class ColumnItem : ListViewItem
		{
			// Token: 0x060029B9 RID: 10681 RVA: 0x000FAFA1 File Offset: 0x000F91A1
			public ColumnItem(DataGridColumn runtimeColumn, int image) : base(string.Empty, image)
			{
				this.runtimeColumn = runtimeColumn;
				this.headerText = this.GetDefaultHeaderText();
				base.Text = this.GetNodeText(null);
			}

			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x060029BA RID: 10682 RVA: 0x00003598 File Offset: 0x00001798
			public virtual DataGridColumnsPage.ColumnItemEditor ColumnEditor
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x060029BB RID: 10683 RVA: 0x000FAFCF File Offset: 0x000F91CF
			// (set) Token: 0x060029BC RID: 10684 RVA: 0x000FAFD7 File Offset: 0x000F91D7
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

			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x060029BD RID: 10685 RVA: 0x000FAFE6 File Offset: 0x000F91E6
			// (set) Token: 0x060029BE RID: 10686 RVA: 0x000FAFEE File Offset: 0x000F91EE
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

			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x060029BF RID: 10687 RVA: 0x000FAFF7 File Offset: 0x000F91F7
			// (set) Token: 0x060029C0 RID: 10688 RVA: 0x000FAFFF File Offset: 0x000F91FF
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

			// Token: 0x170008DB RID: 2267
			// (get) Token: 0x060029C1 RID: 10689 RVA: 0x000FB008 File Offset: 0x000F9208
			public DataGridColumn RuntimeColumn
			{
				get
				{
					return this.runtimeColumn;
				}
			}

			// Token: 0x170008DC RID: 2268
			// (get) Token: 0x060029C2 RID: 10690 RVA: 0x000FB010 File Offset: 0x000F9210
			// (set) Token: 0x060029C3 RID: 10691 RVA: 0x000FB018 File Offset: 0x000F9218
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

			// Token: 0x170008DD RID: 2269
			// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000FB021 File Offset: 0x000F9221
			// (set) Token: 0x060029C5 RID: 10693 RVA: 0x000FB029 File Offset: 0x000F9229
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

			// Token: 0x060029C6 RID: 10694 RVA: 0x000FB032 File Offset: 0x000F9232
			protected virtual string GetDefaultHeaderText()
			{
				return SR.GetString("DGCol_Node");
			}

			// Token: 0x060029C7 RID: 10695 RVA: 0x000FB03E File Offset: 0x000F923E
			public virtual string GetNodeText(string headerText)
			{
				if (headerText == null || headerText.Length == 0)
				{
					return this.GetDefaultHeaderText();
				}
				return headerText;
			}

			// Token: 0x060029C8 RID: 10696 RVA: 0x000FB054 File Offset: 0x000F9254
			protected ITemplate GetTemplate(System.Web.UI.WebControls.DataGrid dataGrid, string templateContent)
			{
				ITemplate result;
				try
				{
					ISite site = dataGrid.Site;
					IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
					result = ControlParser.ParseTemplate(designerHost, templateContent, null);
				}
				catch (Exception ex)
				{
					result = null;
				}
				return result;
			}

			// Token: 0x060029C9 RID: 10697 RVA: 0x000FB0A0 File Offset: 0x000F92A0
			public virtual TemplateColumn GetTemplateColumn(System.Web.UI.WebControls.DataGrid dataGrid)
			{
				return new TemplateColumn
				{
					HeaderText = this.headerText,
					HeaderImageUrl = this.headerImageUrl
				};
			}

			// Token: 0x060029CA RID: 10698 RVA: 0x000FB0CC File Offset: 0x000F92CC
			public virtual void LoadColumnInfo()
			{
				this.headerText = this.runtimeColumn.HeaderText;
				this.headerImageUrl = this.runtimeColumn.HeaderImageUrl;
				this.footerText = this.runtimeColumn.FooterText;
				this.visible = this.runtimeColumn.Visible;
				this.sortExpression = this.runtimeColumn.SortExpression;
				this.UpdateDisplayText();
			}

			// Token: 0x060029CB RID: 10699 RVA: 0x000FB134 File Offset: 0x000F9334
			public virtual void SaveColumnInfo()
			{
				this.runtimeColumn.HeaderText = this.headerText;
				this.runtimeColumn.HeaderImageUrl = this.headerImageUrl;
				this.runtimeColumn.FooterText = this.footerText;
				this.runtimeColumn.Visible = this.visible;
				this.runtimeColumn.SortExpression = this.sortExpression;
			}

			// Token: 0x060029CC RID: 10700 RVA: 0x000FB196 File Offset: 0x000F9396
			protected void UpdateDisplayText()
			{
				base.Text = this.GetNodeText(this.headerText);
			}

			// Token: 0x04001D5F RID: 7519
			protected DataGridColumn runtimeColumn;

			// Token: 0x04001D60 RID: 7520
			protected string headerText;

			// Token: 0x04001D61 RID: 7521
			protected string headerImageUrl;

			// Token: 0x04001D62 RID: 7522
			protected string footerText;

			// Token: 0x04001D63 RID: 7523
			protected bool visible;

			// Token: 0x04001D64 RID: 7524
			protected string sortExpression;
		}

		// Token: 0x0200046D RID: 1133
		private class BoundColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x060029CD RID: 10701 RVA: 0x000FB1AA File Offset: 0x000F93AA
			public BoundColumnItem(BoundColumn runtimeColumn) : base(runtimeColumn, 1)
			{
			}

			// Token: 0x170008DE RID: 2270
			// (get) Token: 0x060029CE RID: 10702 RVA: 0x000FB1B4 File Offset: 0x000F93B4
			// (set) Token: 0x060029CF RID: 10703 RVA: 0x000FB1BC File Offset: 0x000F93BC
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

			// Token: 0x170008DF RID: 2271
			// (get) Token: 0x060029D0 RID: 10704 RVA: 0x000FB1CB File Offset: 0x000F93CB
			// (set) Token: 0x060029D1 RID: 10705 RVA: 0x000FB1D3 File Offset: 0x000F93D3
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

			// Token: 0x170008E0 RID: 2272
			// (get) Token: 0x060029D2 RID: 10706 RVA: 0x000FB1DC File Offset: 0x000F93DC
			// (set) Token: 0x060029D3 RID: 10707 RVA: 0x000FB1E4 File Offset: 0x000F93E4
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

			// Token: 0x060029D4 RID: 10708 RVA: 0x000FB1ED File Offset: 0x000F93ED
			protected override string GetDefaultHeaderText()
			{
				if (this.dataField != null && this.dataField.Length != 0)
				{
					return this.dataField;
				}
				return SR.GetString("DGCol_Node_Bound");
			}

			// Token: 0x060029D5 RID: 10709 RVA: 0x000FB218 File Offset: 0x000F9418
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

			// Token: 0x060029D6 RID: 10710 RVA: 0x000FB260 File Offset: 0x000F9460
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

			// Token: 0x060029D7 RID: 10711 RVA: 0x000FB340 File Offset: 0x000F9540
			public override void LoadColumnInfo()
			{
				base.LoadColumnInfo();
				BoundColumn boundColumn = (BoundColumn)base.RuntimeColumn;
				this.dataField = boundColumn.DataField;
				this.dataFormatString = boundColumn.DataFormatString;
				this.readOnly = boundColumn.ReadOnly;
				base.UpdateDisplayText();
			}

			// Token: 0x060029D8 RID: 10712 RVA: 0x000FB38C File Offset: 0x000F958C
			public override void SaveColumnInfo()
			{
				base.SaveColumnInfo();
				BoundColumn boundColumn = (BoundColumn)base.RuntimeColumn;
				boundColumn.DataField = this.dataField;
				boundColumn.DataFormatString = this.dataFormatString;
				boundColumn.ReadOnly = this.readOnly;
			}

			// Token: 0x04001D65 RID: 7525
			protected string dataField;

			// Token: 0x04001D66 RID: 7526
			protected string dataFormatString;

			// Token: 0x04001D67 RID: 7527
			protected bool readOnly;
		}

		// Token: 0x0200046E RID: 1134
		private class ButtonColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x060029D9 RID: 10713 RVA: 0x000FB3CF File Offset: 0x000F95CF
			public ButtonColumnItem(ButtonColumn runtimeColumn) : base(runtimeColumn, 4)
			{
			}

			// Token: 0x170008E1 RID: 2273
			// (get) Token: 0x060029DA RID: 10714 RVA: 0x000FB3D9 File Offset: 0x000F95D9
			// (set) Token: 0x060029DB RID: 10715 RVA: 0x000FB3E1 File Offset: 0x000F95E1
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

			// Token: 0x170008E2 RID: 2274
			// (get) Token: 0x060029DC RID: 10716 RVA: 0x000FB3EA File Offset: 0x000F95EA
			// (set) Token: 0x060029DD RID: 10717 RVA: 0x000FB3F2 File Offset: 0x000F95F2
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

			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x060029DE RID: 10718 RVA: 0x000FB401 File Offset: 0x000F9601
			// (set) Token: 0x060029DF RID: 10719 RVA: 0x000FB409 File Offset: 0x000F9609
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

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x060029E0 RID: 10720 RVA: 0x000FB412 File Offset: 0x000F9612
			// (set) Token: 0x060029E1 RID: 10721 RVA: 0x000FB41A File Offset: 0x000F961A
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

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x060029E2 RID: 10722 RVA: 0x000FB423 File Offset: 0x000F9623
			// (set) Token: 0x060029E3 RID: 10723 RVA: 0x000FB42B File Offset: 0x000F962B
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

			// Token: 0x060029E4 RID: 10724 RVA: 0x000FB434 File Offset: 0x000F9634
			protected override string GetDefaultHeaderText()
			{
				if (this.buttonText != null && this.buttonText.Length != 0)
				{
					return this.buttonText;
				}
				return SR.GetString("DGCol_Node_Button");
			}

			// Token: 0x060029E5 RID: 10725 RVA: 0x000FB45C File Offset: 0x000F965C
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

			// Token: 0x060029E6 RID: 10726 RVA: 0x000FB59C File Offset: 0x000F979C
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

			// Token: 0x060029E7 RID: 10727 RVA: 0x000FB600 File Offset: 0x000F9800
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

			// Token: 0x04001D68 RID: 7528
			protected string command;

			// Token: 0x04001D69 RID: 7529
			protected string buttonText;

			// Token: 0x04001D6A RID: 7530
			protected string buttonDataTextField;

			// Token: 0x04001D6B RID: 7531
			protected string buttonDataTextFormatString;

			// Token: 0x04001D6C RID: 7532
			protected ButtonColumnType buttonType;
		}

		// Token: 0x0200046F RID: 1135
		private class HyperLinkColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x060029E8 RID: 10728 RVA: 0x000FB65B File Offset: 0x000F985B
			public HyperLinkColumnItem(HyperLinkColumn runtimeColumn) : base(runtimeColumn, 8)
			{
			}

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x060029E9 RID: 10729 RVA: 0x000FB665 File Offset: 0x000F9865
			// (set) Token: 0x060029EA RID: 10730 RVA: 0x000FB66D File Offset: 0x000F986D
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

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x060029EB RID: 10731 RVA: 0x000FB67C File Offset: 0x000F987C
			// (set) Token: 0x060029EC RID: 10732 RVA: 0x000FB684 File Offset: 0x000F9884
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

			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x060029ED RID: 10733 RVA: 0x000FB68D File Offset: 0x000F988D
			// (set) Token: 0x060029EE RID: 10734 RVA: 0x000FB695 File Offset: 0x000F9895
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

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x060029EF RID: 10735 RVA: 0x000FB69E File Offset: 0x000F989E
			// (set) Token: 0x060029F0 RID: 10736 RVA: 0x000FB6A6 File Offset: 0x000F98A6
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

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x060029F1 RID: 10737 RVA: 0x000FB6AF File Offset: 0x000F98AF
			// (set) Token: 0x060029F2 RID: 10738 RVA: 0x000FB6B7 File Offset: 0x000F98B7
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

			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x060029F3 RID: 10739 RVA: 0x000FB6C0 File Offset: 0x000F98C0
			// (set) Token: 0x060029F4 RID: 10740 RVA: 0x000FB6C8 File Offset: 0x000F98C8
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

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x060029F5 RID: 10741 RVA: 0x000FB6D1 File Offset: 0x000F98D1
			// (set) Token: 0x060029F6 RID: 10742 RVA: 0x000FB6D9 File Offset: 0x000F98D9
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

			// Token: 0x060029F7 RID: 10743 RVA: 0x000FB6E2 File Offset: 0x000F98E2
			protected override string GetDefaultHeaderText()
			{
				if (this.anchorText != null && this.anchorText.Length != 0)
				{
					return this.anchorText;
				}
				return SR.GetString("DGCol_Node_HyperLink");
			}

			// Token: 0x060029F8 RID: 10744 RVA: 0x000FB70C File Offset: 0x000F990C
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

			// Token: 0x060029F9 RID: 10745 RVA: 0x000FB8C0 File Offset: 0x000F9AC0
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

			// Token: 0x060029FA RID: 10746 RVA: 0x000FB93C File Offset: 0x000F9B3C
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

			// Token: 0x04001D6D RID: 7533
			protected string anchorText;

			// Token: 0x04001D6E RID: 7534
			protected string anchorDataTextField;

			// Token: 0x04001D6F RID: 7535
			protected string anchorDataTextFormatString;

			// Token: 0x04001D70 RID: 7536
			protected string url;

			// Token: 0x04001D71 RID: 7537
			protected string dataUrlField;

			// Token: 0x04001D72 RID: 7538
			protected string dataUrlFormatString;

			// Token: 0x04001D73 RID: 7539
			protected string target;
		}

		// Token: 0x02000470 RID: 1136
		private class TemplateColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x060029FB RID: 10747 RVA: 0x000FB9AF File Offset: 0x000F9BAF
			public TemplateColumnItem(TemplateColumn runtimeColumn) : base(runtimeColumn, 9)
			{
			}

			// Token: 0x060029FC RID: 10748 RVA: 0x000FB9BA File Offset: 0x000F9BBA
			protected override string GetDefaultHeaderText()
			{
				return SR.GetString("DGCol_Node_Template");
			}
		}

		// Token: 0x02000471 RID: 1137
		private class EditCommandColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x060029FD RID: 10749 RVA: 0x000FB3CF File Offset: 0x000F95CF
			public EditCommandColumnItem(EditCommandColumn runtimeColumn) : base(runtimeColumn, 4)
			{
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x060029FE RID: 10750 RVA: 0x000FB9C6 File Offset: 0x000F9BC6
			// (set) Token: 0x060029FF RID: 10751 RVA: 0x000FB9CE File Offset: 0x000F9BCE
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

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000FB9D7 File Offset: 0x000F9BD7
			// (set) Token: 0x06002A01 RID: 10753 RVA: 0x000FB9DF File Offset: 0x000F9BDF
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

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000FB9E8 File Offset: 0x000F9BE8
			// (set) Token: 0x06002A03 RID: 10755 RVA: 0x000FB9F0 File Offset: 0x000F9BF0
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

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000FB9F9 File Offset: 0x000F9BF9
			// (set) Token: 0x06002A05 RID: 10757 RVA: 0x000FBA01 File Offset: 0x000F9C01
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

			// Token: 0x06002A06 RID: 10758 RVA: 0x000FBA0A File Offset: 0x000F9C0A
			protected override string GetDefaultHeaderText()
			{
				return SR.GetString("DGCol_Node_Edit");
			}

			// Token: 0x06002A07 RID: 10759 RVA: 0x000FBA18 File Offset: 0x000F9C18
			public override TemplateColumn GetTemplateColumn(System.Web.UI.WebControls.DataGrid dataGrid)
			{
				TemplateColumn templateColumn = base.GetTemplateColumn(dataGrid);
				templateColumn.ItemTemplate = base.GetTemplate(dataGrid, this.GetTemplateContent(false));
				templateColumn.EditItemTemplate = base.GetTemplate(dataGrid, this.GetTemplateContent(true));
				return templateColumn;
			}

			// Token: 0x06002A08 RID: 10760 RVA: 0x000FBA58 File Offset: 0x000F9C58
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

			// Token: 0x06002A09 RID: 10761 RVA: 0x000FBBC4 File Offset: 0x000F9DC4
			public override void LoadColumnInfo()
			{
				base.LoadColumnInfo();
				EditCommandColumn editCommandColumn = (EditCommandColumn)base.RuntimeColumn;
				this.editText = editCommandColumn.EditText;
				this.updateText = editCommandColumn.UpdateText;
				this.cancelText = editCommandColumn.CancelText;
				this.buttonType = editCommandColumn.ButtonType;
			}

			// Token: 0x06002A0A RID: 10762 RVA: 0x000FBC14 File Offset: 0x000F9E14
			public override void SaveColumnInfo()
			{
				base.SaveColumnInfo();
				EditCommandColumn editCommandColumn = (EditCommandColumn)base.RuntimeColumn;
				editCommandColumn.EditText = this.editText;
				editCommandColumn.UpdateText = this.updateText;
				editCommandColumn.CancelText = this.cancelText;
				editCommandColumn.ButtonType = this.buttonType;
			}

			// Token: 0x04001D74 RID: 7540
			private string editText;

			// Token: 0x04001D75 RID: 7541
			private string updateText;

			// Token: 0x04001D76 RID: 7542
			private string cancelText;

			// Token: 0x04001D77 RID: 7543
			private ButtonColumnType buttonType;
		}

		// Token: 0x02000472 RID: 1138
		private class CustomColumnItem : DataGridColumnsPage.ColumnItem
		{
			// Token: 0x06002A0B RID: 10763 RVA: 0x000FBC63 File Offset: 0x000F9E63
			public CustomColumnItem(DataGridColumn runtimeColumn) : base(runtimeColumn, 3)
			{
			}
		}

		// Token: 0x02000473 RID: 1139
		private abstract class ColumnItemEditor : System.Windows.Forms.Panel
		{
			// Token: 0x06002A0C RID: 10764 RVA: 0x000FBC6D File Offset: 0x000F9E6D
			public ColumnItemEditor()
			{
				this.InitPanel();
			}

			// Token: 0x06002A0D RID: 10765 RVA: 0x000FBC7B File Offset: 0x000F9E7B
			public virtual void AddDataField(string fieldName)
			{
				this.dataFieldsAvailable = true;
			}

			// Token: 0x1400006A RID: 106
			// (add) Token: 0x06002A0E RID: 10766 RVA: 0x000FBC84 File Offset: 0x000F9E84
			// (remove) Token: 0x06002A0F RID: 10767 RVA: 0x000FBC9D File Offset: 0x000F9E9D
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

			// Token: 0x06002A10 RID: 10768 RVA: 0x000FBCB6 File Offset: 0x000F9EB6
			public virtual void ClearDataFields()
			{
				this.dataFieldsAvailable = false;
			}

			// Token: 0x06002A11 RID: 10769 RVA: 0x00003937 File Offset: 0x00001B37
			protected virtual void InitPanel()
			{
			}

			// Token: 0x06002A12 RID: 10770 RVA: 0x000FBCBF File Offset: 0x000F9EBF
			public virtual void LoadColumn(DataGridColumnsPage.ColumnItem columnItem)
			{
				this.columnItem = columnItem;
			}

			// Token: 0x06002A13 RID: 10771 RVA: 0x000FBCC8 File Offset: 0x000F9EC8
			protected virtual void OnChanged(EventArgs e)
			{
				if (this.onChangedHandler != null)
				{
					this.onChangedHandler(this, e);
				}
			}

			// Token: 0x06002A14 RID: 10772 RVA: 0x00003937 File Offset: 0x00001B37
			public virtual void SaveColumn()
			{
			}

			// Token: 0x04001D78 RID: 7544
			protected DataGridColumnsPage.ColumnItem columnItem;

			// Token: 0x04001D79 RID: 7545
			protected EventHandler onChangedHandler;

			// Token: 0x04001D7A RID: 7546
			protected bool dataFieldsAvailable;
		}

		// Token: 0x02000474 RID: 1140
		private class BoundColumnEditor : DataGridColumnsPage.ColumnItemEditor
		{
			// Token: 0x06002A16 RID: 10774 RVA: 0x000FBCE8 File Offset: 0x000F9EE8
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

			// Token: 0x06002A17 RID: 10775 RVA: 0x000FBEE4 File Offset: 0x000FA0E4
			public override void LoadColumn(DataGridColumnsPage.ColumnItem columnItem)
			{
				base.LoadColumn(columnItem);
				DataGridColumnsPage.BoundColumnItem boundColumnItem = (DataGridColumnsPage.BoundColumnItem)columnItem;
				this.dataFieldEdit.Text = boundColumnItem.DataField;
				this.dataFormatStringEdit.Text = boundColumnItem.DataFormatString;
				this.readOnlyCheck.Checked = boundColumnItem.ReadOnly;
				this.dataFieldEdit.ReadOnly = this.dataFieldsAvailable;
			}

			// Token: 0x06002A18 RID: 10776 RVA: 0x000FBF43 File Offset: 0x000FA143
			private void OnColumnChanged(object source, EventArgs e)
			{
				this.OnChanged(EventArgs.Empty);
			}

			// Token: 0x06002A19 RID: 10777 RVA: 0x000FBF50 File Offset: 0x000FA150
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

			// Token: 0x04001D7B RID: 7547
			private System.Windows.Forms.TextBox dataFieldEdit;

			// Token: 0x04001D7C RID: 7548
			private System.Windows.Forms.TextBox dataFormatStringEdit;

			// Token: 0x04001D7D RID: 7549
			private System.Windows.Forms.CheckBox readOnlyCheck;
		}

		// Token: 0x02000475 RID: 1141
		private class ButtonColumnEditor : DataGridColumnsPage.ColumnItemEditor
		{
			// Token: 0x06002A1B RID: 10779 RVA: 0x000FBFAF File Offset: 0x000FA1AF
			public override void AddDataField(string fieldName)
			{
				this.dataTextFieldCombo.AddItem(fieldName);
				base.AddDataField(fieldName);
			}

			// Token: 0x06002A1C RID: 10780 RVA: 0x000FBFC4 File Offset: 0x000FA1C4
			public override void ClearDataFields()
			{
				this.dataTextFieldCombo.Items.Clear();
				this.dataTextFieldCombo.EnsureNotSetItem();
				base.ClearDataFields();
			}

			// Token: 0x06002A1D RID: 10781 RVA: 0x000FBFE8 File Offset: 0x000FA1E8
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

			// Token: 0x06002A1E RID: 10782 RVA: 0x000FC3EC File Offset: 0x000FA5EC
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
				ButtonColumnType buttonType = buttonColumnItem.ButtonType;
				if (buttonType != ButtonColumnType.LinkButton)
				{
					if (buttonType == ButtonColumnType.PushButton)
					{
						this.buttonTypeCombo.SelectedIndex = 1;
					}
				}
				else
				{
					this.buttonTypeCombo.SelectedIndex = 0;
				}
				this.UpdateEnabledState();
			}

			// Token: 0x06002A1F RID: 10783 RVA: 0x000FC4E0 File Offset: 0x000FA6E0
			private void OnColumnChanged(object source, EventArgs e)
			{
				this.OnChanged(EventArgs.Empty);
				if (source == this.dataTextFieldCombo || source == this.dataTextFieldEdit)
				{
					this.UpdateEnabledState();
				}
			}

			// Token: 0x06002A20 RID: 10784 RVA: 0x000FC508 File Offset: 0x000FA708
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
				int selectedIndex = this.buttonTypeCombo.SelectedIndex;
				if (selectedIndex == 0)
				{
					buttonColumnItem.ButtonType = ButtonColumnType.LinkButton;
					return;
				}
				if (selectedIndex != 1)
				{
					return;
				}
				buttonColumnItem.ButtonType = ButtonColumnType.PushButton;
			}

			// Token: 0x06002A21 RID: 10785 RVA: 0x000FC5D0 File Offset: 0x000FA7D0
			private void UpdateEnabledState()
			{
				if (this.dataFieldsAvailable)
				{
					this.dataTextFormatStringEdit.Enabled = this.dataTextFieldCombo.IsSet();
					return;
				}
				this.dataTextFormatStringEdit.Enabled = (this.dataTextFieldEdit.Text.Trim().Length != 0);
			}

			// Token: 0x04001D7E RID: 7550
			private const int IDX_TYPE_LINKBUTTON = 0;

			// Token: 0x04001D7F RID: 7551
			private const int IDX_TYPE_PUSHBUTTON = 1;

			// Token: 0x04001D80 RID: 7552
			private System.Windows.Forms.TextBox commandEdit;

			// Token: 0x04001D81 RID: 7553
			private System.Windows.Forms.TextBox textEdit;

			// Token: 0x04001D82 RID: 7554
			private UnsettableComboBox dataTextFieldCombo;

			// Token: 0x04001D83 RID: 7555
			private System.Windows.Forms.TextBox dataTextFieldEdit;

			// Token: 0x04001D84 RID: 7556
			private System.Windows.Forms.TextBox dataTextFormatStringEdit;

			// Token: 0x04001D85 RID: 7557
			private ComboBox buttonTypeCombo;
		}

		// Token: 0x02000476 RID: 1142
		private class HyperLinkColumnEditor : DataGridColumnsPage.ColumnItemEditor
		{
			// Token: 0x06002A23 RID: 10787 RVA: 0x000FC61F File Offset: 0x000FA81F
			public override void AddDataField(string fieldName)
			{
				this.dataTextFieldCombo.AddItem(fieldName);
				this.dataUrlFieldCombo.AddItem(fieldName);
				base.AddDataField(fieldName);
			}

			// Token: 0x06002A24 RID: 10788 RVA: 0x000FC640 File Offset: 0x000FA840
			public override void ClearDataFields()
			{
				this.dataTextFieldCombo.Items.Clear();
				this.dataUrlFieldCombo.Items.Clear();
				this.dataTextFieldCombo.EnsureNotSetItem();
				this.dataUrlFieldCombo.EnsureNotSetItem();
				base.ClearDataFields();
			}

			// Token: 0x06002A25 RID: 10789 RVA: 0x000FC680 File Offset: 0x000FA880
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

			// Token: 0x06002A26 RID: 10790 RVA: 0x000FCC68 File Offset: 0x000FAE68
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

			// Token: 0x06002A27 RID: 10791 RVA: 0x000FCDC5 File Offset: 0x000FAFC5
			protected void OnColumnChanged(object source, EventArgs e)
			{
				this.OnChanged(EventArgs.Empty);
				if (source == this.dataTextFieldCombo || source == this.dataUrlFieldCombo || source == this.dataTextFieldEdit || source == this.dataUrlFieldEdit)
				{
					this.UpdateEnabledState();
				}
			}

			// Token: 0x06002A28 RID: 10792 RVA: 0x000FCDFC File Offset: 0x000FAFFC
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

			// Token: 0x06002A29 RID: 10793 RVA: 0x000FCF10 File Offset: 0x000FB110
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

			// Token: 0x04001D86 RID: 7558
			private System.Windows.Forms.TextBox textEdit;

			// Token: 0x04001D87 RID: 7559
			private UnsettableComboBox dataTextFieldCombo;

			// Token: 0x04001D88 RID: 7560
			private System.Windows.Forms.TextBox dataTextFieldEdit;

			// Token: 0x04001D89 RID: 7561
			private System.Windows.Forms.TextBox dataTextFormatStringEdit;

			// Token: 0x04001D8A RID: 7562
			private System.Windows.Forms.TextBox urlEdit;

			// Token: 0x04001D8B RID: 7563
			private UnsettableComboBox dataUrlFieldCombo;

			// Token: 0x04001D8C RID: 7564
			private System.Windows.Forms.TextBox dataUrlFieldEdit;

			// Token: 0x04001D8D RID: 7565
			private System.Windows.Forms.TextBox dataUrlFormatStringEdit;

			// Token: 0x04001D8E RID: 7566
			private ComboBox targetCombo;
		}

		// Token: 0x02000477 RID: 1143
		private class EditCommandColumnEditor : DataGridColumnsPage.ColumnItemEditor
		{
			// Token: 0x06002A2B RID: 10795 RVA: 0x000FCF98 File Offset: 0x000FB198
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

			// Token: 0x06002A2C RID: 10796 RVA: 0x000FD288 File Offset: 0x000FB488
			public override void LoadColumn(DataGridColumnsPage.ColumnItem columnItem)
			{
				base.LoadColumn(columnItem);
				DataGridColumnsPage.EditCommandColumnItem editCommandColumnItem = (DataGridColumnsPage.EditCommandColumnItem)this.columnItem;
				this.editTextEdit.Text = editCommandColumnItem.EditText;
				this.updateTextEdit.Text = editCommandColumnItem.UpdateText;
				this.cancelTextEdit.Text = editCommandColumnItem.CancelText;
				ButtonColumnType buttonType = editCommandColumnItem.ButtonType;
				if (buttonType == ButtonColumnType.LinkButton)
				{
					this.buttonTypeCombo.SelectedIndex = 0;
					return;
				}
				if (buttonType != ButtonColumnType.PushButton)
				{
					return;
				}
				this.buttonTypeCombo.SelectedIndex = 1;
			}

			// Token: 0x06002A2D RID: 10797 RVA: 0x000FBF43 File Offset: 0x000FA143
			private void OnColumnChanged(object source, EventArgs e)
			{
				this.OnChanged(EventArgs.Empty);
			}

			// Token: 0x06002A2E RID: 10798 RVA: 0x000FD304 File Offset: 0x000FB504
			public override void SaveColumn()
			{
				base.SaveColumn();
				DataGridColumnsPage.EditCommandColumnItem editCommandColumnItem = (DataGridColumnsPage.EditCommandColumnItem)this.columnItem;
				editCommandColumnItem.EditText = this.editTextEdit.Text;
				editCommandColumnItem.UpdateText = this.updateTextEdit.Text;
				editCommandColumnItem.CancelText = this.cancelTextEdit.Text;
				int selectedIndex = this.buttonTypeCombo.SelectedIndex;
				if (selectedIndex == 0)
				{
					editCommandColumnItem.ButtonType = ButtonColumnType.LinkButton;
					return;
				}
				if (selectedIndex != 1)
				{
					return;
				}
				editCommandColumnItem.ButtonType = ButtonColumnType.PushButton;
			}

			// Token: 0x04001D8F RID: 7567
			private const int IDX_TYPE_LINKBUTTON = 0;

			// Token: 0x04001D90 RID: 7568
			private const int IDX_TYPE_PUSHBUTTON = 1;

			// Token: 0x04001D91 RID: 7569
			private System.Windows.Forms.TextBox editTextEdit;

			// Token: 0x04001D92 RID: 7570
			private System.Windows.Forms.TextBox updateTextEdit;

			// Token: 0x04001D93 RID: 7571
			private System.Windows.Forms.TextBox cancelTextEdit;

			// Token: 0x04001D94 RID: 7572
			private ComboBox buttonTypeCombo;
		}
	}
}
