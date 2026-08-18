using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000B6 RID: 182
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed partial class DataControlFieldsEditor : DesignerForm
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x0001CC60 File Offset: 0x0001AE60
		public DataControlFieldsEditor(DataBoundControlDesigner controlDesigner) : base(controlDesigner.Component.Site)
		{
			this._controlDesigner = controlDesigner;
			this.InitializeComponent();
			this.InitForm();
			this._initialActivate = true;
			this.IgnoreRefreshSchemaEvents();
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001CC93 File Offset: 0x0001AE93
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0001CCD2 File Offset: 0x0001AED2
		private bool AutoGenerateFields
		{
			get
			{
				if (this.Control is GridView)
				{
					return ((GridView)this.Control).AutoGenerateColumns;
				}
				return this.Control is DetailsView && ((DetailsView)this.Control).AutoGenerateRows;
			}
			set
			{
				if (this.Control is GridView)
				{
					((GridView)this.Control).AutoGenerateColumns = value;
					return;
				}
				if (this.Control is DetailsView)
				{
					((DetailsView)this.Control).AutoGenerateRows = value;
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001CD11 File Offset: 0x0001AF11
		private DataBoundControl Control
		{
			get
			{
				return this._controlDesigner.Component as DataBoundControl;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001CD24 File Offset: 0x0001AF24
		private DataControlFieldCollection FieldCollection
		{
			get
			{
				if (this._clonedFieldCollection == null)
				{
					if (this.Control is GridView)
					{
						DataControlFieldCollection columns = ((GridView)this.Control).Columns;
						this._clonedFieldCollection = columns.CloneFields();
						for (int i = 0; i < columns.Count; i++)
						{
							this._controlDesigner.RegisterClone(columns[i], this._clonedFieldCollection[i]);
						}
					}
					else if (this.Control is DetailsView)
					{
						DataControlFieldCollection fields = ((DetailsView)this.Control).Fields;
						this._clonedFieldCollection = fields.CloneFields();
						for (int j = 0; j < fields.Count; j++)
						{
							this._controlDesigner.RegisterClone(fields[j], this._clonedFieldCollection[j]);
						}
					}
				}
				return this._clonedFieldCollection;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001CDF6 File Offset: 0x0001AFF6
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.DataControlField.DataControlFieldEditor";
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001CDFD File Offset: 0x0001AFFD
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001CE3C File Offset: 0x0001B03C
		private bool IgnoreRefreshSchema
		{
			get
			{
				if (this._controlDesigner is GridViewDesigner)
				{
					return ((GridViewDesigner)this._controlDesigner)._ignoreSchemaRefreshedEvent;
				}
				return this._controlDesigner is DetailsViewDesigner && ((DetailsViewDesigner)this._controlDesigner)._ignoreSchemaRefreshedEvent;
			}
			set
			{
				if (this._controlDesigner is GridViewDesigner)
				{
					((GridViewDesigner)this._controlDesigner)._ignoreSchemaRefreshedEvent = value;
				}
				if (this._controlDesigner is DetailsViewDesigner)
				{
					((DetailsViewDesigner)this._controlDesigner)._ignoreSchemaRefreshedEvent = value;
				}
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001CE7A File Offset: 0x0001B07A
		private void EnterLoadingMode()
		{
			this._isLoading = true;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001CE83 File Offset: 0x0001B083
		private void ExitLoadingMode()
		{
			this._isLoading = false;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001CE8C File Offset: 0x0001B08C
		internal string GetNewDataSourceName(Type controlType, DataBoundControlMode mode)
		{
			if (mode == DataBoundControlMode.Edit)
			{
				return this.GetNewDataSourceName(controlType, 1);
			}
			if (mode == DataBoundControlMode.Insert)
			{
				return this.GetNewDataSourceName(controlType, 2);
			}
			return this.GetNewDataSourceName(controlType, 0);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001CEB0 File Offset: 0x0001B0B0
		private string GetNewDataSourceName(Type controlType, int editMode)
		{
			int num = 1;
			return this.GetNewDataSourceName(controlType, editMode, ref num);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001CECC File Offset: 0x0001B0CC
		private string GetNewDataSourceName(Type controlType, int editMode, ref int startIndex)
		{
			int num = startIndex;
			DataControlFieldCollection dataControlFieldCollection = new DataControlFieldCollection();
			int count = this._selFieldsList.Items.Count;
			for (int i = 0; i < count; i++)
			{
				DataControlFieldsEditor.FieldItem fieldItem = (DataControlFieldsEditor.FieldItem)this._selFieldsList.Items[i];
				dataControlFieldCollection.Add(fieldItem.RuntimeField);
			}
			if (dataControlFieldCollection != null && dataControlFieldCollection.Count > 0)
			{
				bool flag = false;
				while (!flag)
				{
					for (int j = 0; j < dataControlFieldCollection.Count; j++)
					{
						DataControlField dataControlField = dataControlFieldCollection[j];
						if (dataControlField is TemplateField)
						{
							ITemplate template = null;
							switch (editMode)
							{
							case 0:
								template = ((TemplateField)dataControlField).ItemTemplate;
								break;
							case 1:
								template = ((TemplateField)dataControlField).EditItemTemplate;
								break;
							case 2:
								template = ((TemplateField)dataControlField).InsertItemTemplate;
								break;
							}
							if (template != null)
							{
								IDesignerHost host = (IDesignerHost)this.Control.Site.GetService(typeof(IDesignerHost));
								string text = ControlSerializer.SerializeTemplate(template, host);
								if (text.Contains(controlType.Name + num.ToString(NumberFormatInfo.InvariantInfo)))
								{
									num++;
									break;
								}
							}
						}
						if (j == dataControlFieldCollection.Count - 1)
						{
							flag = true;
						}
					}
				}
			}
			startIndex = num;
			return controlType.Name + num.ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001D03C File Offset: 0x0001B23C
		private IDataSourceViewSchema GetViewSchema()
		{
			if (this._viewSchema == null && this._controlDesigner != null)
			{
				DesignerDataSourceView designerView = this._controlDesigner.DesignerView;
				if (designerView != null)
				{
					try
					{
						this._viewSchema = designerView.Schema;
					}
					catch (Exception ex)
					{
						IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)base.ServiceProvider.GetService(typeof(IComponentDesignerDebugService));
						if (componentDesignerDebugService != null)
						{
							componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
							{
								"DesignerDataSourceView.Schema",
								ex.Message
							}));
						}
					}
				}
			}
			return this._viewSchema;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		private IDataSourceFieldSchema[] GetFieldSchemas()
		{
			if (this._fieldSchemas == null)
			{
				IDataSourceViewSchema viewSchema = this.GetViewSchema();
				if (viewSchema != null)
				{
					this._fieldSchemas = viewSchema.GetFields();
				}
			}
			return this._fieldSchemas;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001D108 File Offset: 0x0001B308
		private void IgnoreRefreshSchemaEvents()
		{
			this._initialIgnoreRefreshSchemaValue = this.IgnoreRefreshSchema;
			this.IgnoreRefreshSchema = true;
			IDataSourceDesigner dataSourceDesigner = this._controlDesigner.DataSourceDesigner;
			if (dataSourceDesigner != null)
			{
				dataSourceDesigner.SuppressDataSourceEvents();
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001DAAC File Offset: 0x0001BCAC
		private void InitForm()
		{
			System.Drawing.Image value = BitmapSelector.CreateBitmap(base.GetType(), "FieldNodes.bmp");
			ImageList imageList = new ImageList();
			imageList.TransparentColor = Color.Magenta;
			imageList.Images.AddStrip(value);
			this._autoFieldCheck.Text = SR.GetString("DCFEditor_AutoGen");
			this._availableFieldsTree.ImageList = imageList;
			this._addFieldButton.Text = SR.GetString("DCFEditor_Add");
			ColumnHeader columnHeader = new ColumnHeader();
			columnHeader.Width = this._selFieldsList.Width - 4;
			this._selFieldsList.Columns.Add(columnHeader);
			this._selFieldsList.SmallImageList = imageList;
			Icon icon = BitmapSelector.CreateIcon(base.GetType(), "SortUp.ico");
			Bitmap bitmap = icon.ToBitmap();
			bitmap.MakeTransparent();
			this._moveFieldUpButton.Image = bitmap;
			this._moveFieldUpButton.AccessibleDescription = SR.GetString("DCFEditor_MoveFieldUpDesc");
			this._moveFieldUpButton.AccessibleName = SR.GetString("DCFEditor_MoveFieldUpName");
			Icon icon2 = BitmapSelector.CreateIcon(base.GetType(), "SortDown.ico");
			Bitmap bitmap2 = icon2.ToBitmap();
			bitmap2.MakeTransparent();
			this._moveFieldDownButton.Image = bitmap2;
			this._moveFieldDownButton.AccessibleDescription = SR.GetString("DCFEditor_MoveFieldDownDesc");
			this._moveFieldDownButton.AccessibleName = SR.GetString("DCFEditor_MoveFieldDownName");
			Icon icon3 = BitmapSelector.CreateIcon(base.GetType(), "Delete.ico");
			Bitmap bitmap3 = icon3.ToBitmap();
			bitmap3.MakeTransparent();
			this._deleteFieldButton.Image = bitmap3;
			this._deleteFieldButton.AccessibleDescription = SR.GetString("DCFEditor_DeleteFieldDesc");
			this._deleteFieldButton.AccessibleName = SR.GetString("DCFEditor_DeleteFieldName");
			this._templatizeLink.Text = SR.GetString("DCFEditor_Templatize");
			this._refreshSchemaLink.Text = SR.GetString("DataSourceDesigner_RefreshSchemaNoHotkey");
			this._refreshSchemaLink.Visible = (this._controlDesigner.DataSourceDesigner != null && this._controlDesigner.DataSourceDesigner.CanRefreshSchema);
			this._okButton.Text = SR.GetString("OKCaption");
			this._cancelButton.Text = SR.GetString("CancelCaption");
			this._selFieldLabel.Text = SR.GetString("DCFEditor_FieldProps");
			this._availableFieldsLabel.Text = SR.GetString("DCFEditor_AvailableFields");
			this._selFieldsLabel.Text = SR.GetString("DCFEditor_SelectedFields");
			this._currentFieldProps.Site = this._controlDesigner.Component.Site;
			this.Text = SR.GetString("DCFEditor_Text");
			base.Icon = BitmapSelector.CreateIcon(base.GetType(), "DataControlFieldsEditor.ico");
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001DD60 File Offset: 0x0001BF60
		private void InitPage()
		{
			this._autoFieldCheck.Checked = false;
			this._selectedDataSourceNode = null;
			this._selectedCheckBoxDataSourceNode = null;
			this._availableFieldsTree.Nodes.Clear();
			this._selFieldsList.Items.Clear();
			this._currentFieldItem = null;
			this._propChangesPending = false;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		private void LoadFields()
		{
			DataControlFieldCollection fieldCollection = this.FieldCollection;
			if (fieldCollection != null)
			{
				int count = fieldCollection.Count;
				IDataSourceViewSchema viewSchema = this.GetViewSchema();
				for (int i = 0; i < count; i++)
				{
					DataControlField dataControlField = fieldCollection[i];
					Type type = dataControlField.GetType();
					DataControlFieldsEditor.FieldItem fieldItem;
					if (type == typeof(CheckBoxField))
					{
						fieldItem = new DataControlFieldsEditor.CheckBoxFieldItem(this, (CheckBoxField)dataControlField);
					}
					else if (type == typeof(BoundField))
					{
						fieldItem = new DataControlFieldsEditor.BoundFieldItem(this, (BoundField)dataControlField);
					}
					else if (type == typeof(ButtonField))
					{
						fieldItem = new DataControlFieldsEditor.ButtonFieldItem(this, (ButtonField)dataControlField);
					}
					else if (type == typeof(HyperLinkField))
					{
						fieldItem = new DataControlFieldsEditor.HyperLinkFieldItem(this, (HyperLinkField)dataControlField);
					}
					else if (type == typeof(TemplateField))
					{
						fieldItem = new DataControlFieldsEditor.TemplateFieldItem(this, (TemplateField)dataControlField);
					}
					else if (type == typeof(CommandField))
					{
						fieldItem = new DataControlFieldsEditor.CommandFieldItem(this, (CommandField)dataControlField);
					}
					else if (type == typeof(ImageField))
					{
						fieldItem = new DataControlFieldsEditor.ImageFieldItem(this, (ImageField)dataControlField);
					}
					else if (this._customFieldDesigners.ContainsKey(type))
					{
						fieldItem = new DataControlFieldsEditor.DataControlFieldDesignerItem(this._customFieldDesigners[type], dataControlField);
					}
					else
					{
						fieldItem = new DataControlFieldsEditor.CustomFieldItem(this, dataControlField);
					}
					fieldItem.LoadFieldInfo();
					IDataSourceViewSchemaAccessor runtimeField = fieldItem.RuntimeField;
					if (runtimeField != null)
					{
						runtimeField.DataSourceViewSchema = viewSchema;
					}
					this._selFieldsList.Items.Add(fieldItem);
				}
				if (this._selFieldsList.Items.Count != 0)
				{
					this._currentFieldItem = (DataControlFieldsEditor.FieldItem)this._selFieldsList.Items[0];
					this._currentFieldItem.Selected = true;
				}
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001DFA9 File Offset: 0x0001C1A9
		private void LoadComponent()
		{
			this.InitPage();
			this.LoadAvailableFieldsTree();
			this.LoadDataSourceFields();
			this.LoadCustomFields();
			this._autoFieldCheck.Checked = this.AutoGenerateFields;
			this.LoadFields();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001DFE0 File Offset: 0x0001C1E0
		private void LoadCustomFields()
		{
			if (this._customFieldDesigners == null)
			{
				this._customFieldDesigners = DataControlFieldHelper.GetCustomFieldDesigners(this, this.Control);
			}
			IDataSourceFieldSchema[] fieldSchemas = this.GetFieldSchemas();
			bool flag = fieldSchemas != null && fieldSchemas.Length != 0;
			foreach (KeyValuePair<Type, DataControlFieldDesigner> keyValuePair in this._customFieldDesigners)
			{
				DataControlFieldDesigner value = keyValuePair.Value;
				if (value.UsesSchema && flag)
				{
					DataControlFieldsEditor.DataSourceNode dataSourceNode = new DataControlFieldsEditor.DataSourceNode(keyValuePair.Key.Name);
					this._availableFieldsTree.Nodes.Add(dataSourceNode);
					foreach (IDataSourceFieldSchema fieldSchema in fieldSchemas)
					{
						dataSourceNode.Nodes.Add(new DataControlFieldsEditor.DataControlFieldDesignerNode(value, fieldSchema));
					}
					dataSourceNode.Expand();
				}
				else
				{
					this._availableFieldsTree.Nodes.Add(new DataControlFieldsEditor.DataControlFieldDesignerNode(value));
				}
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		private void LoadDataSourceFields()
		{
			this.EnterLoadingMode();
			IDataSourceFieldSchema[] fieldSchemas = this.GetFieldSchemas();
			if (fieldSchemas != null && fieldSchemas.Length != 0)
			{
				DataControlFieldsEditor.DataFieldNode dataFieldNode = new DataControlFieldsEditor.DataFieldNode(this);
				this._availableFieldsTree.Nodes.Insert(0, dataFieldNode);
				foreach (IDataSourceFieldSchema fieldSchema in fieldSchemas)
				{
					DataControlFieldsEditor.BoundNode node = new DataControlFieldsEditor.BoundNode(this, fieldSchema);
					this._selectedDataSourceNode.Nodes.Add(node);
				}
				this._selectedDataSourceNode.Expand();
				foreach (IDataSourceFieldSchema dataSourceFieldSchema in fieldSchemas)
				{
					if (dataSourceFieldSchema.DataType == typeof(bool) || dataSourceFieldSchema.DataType == typeof(bool?))
					{
						DataControlFieldsEditor.CheckBoxNode node2 = new DataControlFieldsEditor.CheckBoxNode(this, dataSourceFieldSchema);
						this._selectedCheckBoxDataSourceNode.Nodes.Add(node2);
					}
				}
				this._selectedCheckBoxDataSourceNode.Expand();
				this._availableFieldsTree.SelectedNode = dataFieldNode;
				dataFieldNode.EnsureVisible();
			}
			else
			{
				DataControlFieldsEditor.BoundNode boundNode = new DataControlFieldsEditor.BoundNode(this, null);
				this._availableFieldsTree.Nodes.Insert(0, boundNode);
				boundNode.EnsureVisible();
				DataControlFieldsEditor.CheckBoxNode checkBoxNode = new DataControlFieldsEditor.CheckBoxNode(this, null);
				this._availableFieldsTree.Nodes.Insert(1, checkBoxNode);
				checkBoxNode.EnsureVisible();
				this._availableFieldsTree.SelectedNode = boundNode;
			}
			this.ExitLoadingMode();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001E248 File Offset: 0x0001C448
		private void LoadAvailableFieldsTree()
		{
			IDataSourceFieldSchema[] fieldSchemas = this.GetFieldSchemas();
			if (fieldSchemas != null && fieldSchemas.Length != 0)
			{
				this._selectedDataSourceNode = new DataControlFieldsEditor.DataSourceNode();
				this._availableFieldsTree.Nodes.Add(this._selectedDataSourceNode);
				this._selectedCheckBoxDataSourceNode = new DataControlFieldsEditor.BoolDataSourceNode();
				this._availableFieldsTree.Nodes.Add(this._selectedCheckBoxDataSourceNode);
			}
			DataControlFieldsEditor.HyperLinkNode node = new DataControlFieldsEditor.HyperLinkNode(this);
			this._availableFieldsTree.Nodes.Add(node);
			DataControlFieldsEditor.ImageNode node2 = new DataControlFieldsEditor.ImageNode(this);
			this._availableFieldsTree.Nodes.Add(node2);
			DataControlFieldsEditor.ButtonNode node3 = new DataControlFieldsEditor.ButtonNode(this);
			this._availableFieldsTree.Nodes.Add(node3);
			DataControlFieldsEditor.CommandNode commandNode = new DataControlFieldsEditor.CommandNode(this);
			this._availableFieldsTree.Nodes.Add(commandNode);
			DataControlFieldsEditor.CommandNode node4 = new DataControlFieldsEditor.CommandNode(this, 0, SR.GetString("DCFEditor_Node_Edit"), 6);
			commandNode.Nodes.Add(node4);
			if (this.Control is GridView)
			{
				DataControlFieldsEditor.CommandNode node5 = new DataControlFieldsEditor.CommandNode(this, 2, SR.GetString("DCFEditor_Node_Select"), 5);
				commandNode.Nodes.Add(node5);
			}
			DataControlFieldsEditor.CommandNode node6 = new DataControlFieldsEditor.CommandNode(this, 3, SR.GetString("DCFEditor_Node_Delete"), 7);
			commandNode.Nodes.Add(node6);
			if (this.Control is DetailsView)
			{
				DataControlFieldsEditor.CommandNode node7 = new DataControlFieldsEditor.CommandNode(this, 1, SR.GetString("DCFEditor_Node_Insert"), 11);
				commandNode.Nodes.Add(node7);
			}
			DataControlFieldsEditor.TemplateNode node8 = new DataControlFieldsEditor.TemplateNode(this);
			this._availableFieldsTree.Nodes.Add(node8);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001E3CF File Offset: 0x0001C5CF
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			if (this._initialActivate)
			{
				this.LoadComponent();
				this._initialActivate = false;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001E3ED File Offset: 0x0001C5ED
		private void OnAvailableFieldsDoubleClick(object source, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.OnClickAddField(source, e);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001E404 File Offset: 0x0001C604
		private void OnAvailableFieldsGotFocus(object source, EventArgs e)
		{
			this._currentFieldProps.SelectedObject = null;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001E412 File Offset: 0x0001C612
		private void OnAvailableFieldsKeyPress(object source, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.OnClickAddField(source, e);
				e.Handled = true;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001E430 File Offset: 0x0001C630
		private void OnChangedPropertyValues(object source, PropertyValueChangedEventArgs e)
		{
			if (this._isLoading)
			{
				return;
			}
			if (e.ChangedItem.Label == "HeaderText" || e.ChangedItem.PropertyDescriptor.ComponentType == typeof(CommandField))
			{
				this._propChangesPending = true;
				this.SaveFieldProperties();
				if (this._selFieldsList.SelectedItems.Count == 0)
				{
					this._currentFieldItem = null;
					return;
				}
				this._currentFieldItem = (DataControlFieldsEditor.FieldItem)this._selFieldsList.SelectedItems[0];
				DataControlFieldsEditor.CommandFieldItem commandFieldItem = this._currentFieldItem as DataControlFieldsEditor.CommandFieldItem;
				if (commandFieldItem != null)
				{
					commandFieldItem.UpdateImageIndex();
				}
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001E4D6 File Offset: 0x0001C6D6
		private void OnCheckChangedAutoField(object source, EventArgs e)
		{
			if (this._isLoading)
			{
				return;
			}
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		private void OnClickAddField(object source, EventArgs e)
		{
			DataControlFieldsEditor.AvailableFieldNode availableFieldNode = (DataControlFieldsEditor.AvailableFieldNode)this._availableFieldsTree.SelectedNode;
			if (!this._addFieldButton.Enabled)
			{
				return;
			}
			if (this._propChangesPending)
			{
				this.SaveFieldProperties();
			}
			if (!availableFieldNode.CreatesMultipleFields)
			{
				DataControlFieldsEditor.FieldItem fieldItem = availableFieldNode.CreateField();
				this._selFieldsList.Items.Add(fieldItem);
				this._currentFieldItem = fieldItem;
				this._currentFieldItem.Selected = true;
				this._currentFieldItem.EnsureVisible();
			}
			else
			{
				IDataSourceFieldSchema[] fieldSchemas = this.GetFieldSchemas();
				DataControlFieldsEditor.FieldItem[] array = availableFieldNode.CreateFields(this.Control, fieldSchemas);
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					this._selFieldsList.Items.Add(array[i]);
				}
				this._currentFieldItem = array[num - 1];
				this._currentFieldItem.Selected = true;
				this._currentFieldItem.EnsureVisible();
			}
			IDataSourceViewSchemaAccessor runtimeField = this._currentFieldItem.RuntimeField;
			if (runtimeField != null)
			{
				runtimeField.DataSourceViewSchema = this.GetViewSchema();
			}
			this._selFieldsList.Focus();
			this._selFieldsList.FocusedItem = this._currentFieldItem;
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001E608 File Offset: 0x0001C808
		private void OnClickDeleteField(object source, EventArgs e)
		{
			int index = this._currentFieldItem.Index;
			int num = -1;
			int count = this._selFieldsList.Items.Count;
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
			this._propChangesPending = false;
			this._currentFieldItem.Remove();
			this._currentFieldItem = null;
			if (num != -1)
			{
				this._currentFieldItem = (DataControlFieldsEditor.FieldItem)this._selFieldsList.Items[num];
				this._currentFieldItem.Selected = true;
				this._currentFieldItem.EnsureVisible();
				this._deleteFieldButton.Focus();
			}
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
		private void OnClickMoveFieldDown(object source, EventArgs e)
		{
			this._fieldMovePending = true;
			int index = this._currentFieldItem.Index;
			ListViewItem item = this._selFieldsList.Items[index];
			this._selFieldsList.Items.RemoveAt(index);
			this._selFieldsList.Items.Insert(index + 1, item);
			this._currentFieldItem = (DataControlFieldsEditor.FieldItem)this._selFieldsList.Items[index + 1];
			this._currentFieldItem.Selected = true;
			this._currentFieldItem.EnsureVisible();
			this.UpdateFieldPositionButtonsState();
			if (this._moveFieldUpButton.Enabled && !this._moveFieldDownButton.Enabled)
			{
				this._moveFieldUpButton.Focus();
			}
			this._fieldMovePending = false;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001E768 File Offset: 0x0001C968
		private void OnClickMoveFieldUp(object source, EventArgs e)
		{
			this._fieldMovePending = true;
			int index = this._currentFieldItem.Index;
			ListViewItem item = this._selFieldsList.Items[index];
			this._selFieldsList.Items.RemoveAt(index);
			this._selFieldsList.Items.Insert(index - 1, item);
			this._currentFieldItem = (DataControlFieldsEditor.FieldItem)this._selFieldsList.Items[index - 1];
			this._currentFieldItem.Selected = true;
			this._currentFieldItem.EnsureVisible();
			this.UpdateFieldPositionButtonsState();
			this._fieldMovePending = false;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001E802 File Offset: 0x0001CA02
		private void OnClickOK(object source, EventArgs e)
		{
			this.SaveComponent();
			this.PersistClonedFieldsToControl();
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001E810 File Offset: 0x0001CA10
		private void OnClickRefreshSchema(object source, LinkLabelLinkClickedEventArgs e)
		{
			this._fieldSchemas = null;
			this._viewSchema = null;
			IDataSourceDesigner dataSourceDesigner = this._controlDesigner.DataSourceDesigner;
			if (dataSourceDesigner != null && dataSourceDesigner.CanRefreshSchema)
			{
				dataSourceDesigner.RefreshSchema(false);
			}
			IDataSourceViewSchema viewSchema = this.GetViewSchema();
			foreach (object obj in this._selFieldsList.Items)
			{
				DataControlFieldsEditor.FieldItem fieldItem = (DataControlFieldsEditor.FieldItem)obj;
				IDataSourceViewSchemaAccessor runtimeField = fieldItem.RuntimeField;
				if (runtimeField != null)
				{
					runtimeField.DataSourceViewSchema = viewSchema;
				}
			}
			this._availableFieldsTree.Nodes.Clear();
			this.LoadAvailableFieldsTree();
			this.LoadDataSourceFields();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
		private void OnClickTemplatize(object source, LinkLabelLinkClickedEventArgs e)
		{
			if (this._propChangesPending)
			{
				this.SaveFieldProperties();
			}
			TemplateField templateField = this._currentFieldItem.GetTemplateField(this.Control);
			DataControlFieldsEditor.TemplateFieldItem templateFieldItem = new DataControlFieldsEditor.TemplateFieldItem(this, templateField);
			templateFieldItem.LoadFieldInfo();
			this._selFieldsList.Items[this._currentFieldItem.Index] = templateFieldItem;
			this._currentFieldItem = templateFieldItem;
			this._currentFieldItem.Selected = true;
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001E940 File Offset: 0x0001CB40
		protected override void OnClosed(EventArgs e)
		{
			IDataSourceDesigner dataSourceDesigner = this._controlDesigner.DataSourceDesigner;
			if (dataSourceDesigner != null)
			{
				dataSourceDesigner.ResumeDataSourceEvents();
			}
			this.IgnoreRefreshSchema = this._initialIgnoreRefreshSchemaValue;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001E96E File Offset: 0x0001CB6E
		private void OnSelChangedAvailableFields(object source, TreeViewEventArgs e)
		{
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001E96E File Offset: 0x0001CB6E
		private void OnSelFieldsListGotFocus(object source, EventArgs e)
		{
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001E976 File Offset: 0x0001CB76
		private void OnSelFieldsListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				if (this._currentFieldItem != null)
				{
					this.OnClickDeleteField(sender, e);
				}
				e.Handled = true;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001E99C File Offset: 0x0001CB9C
		private void OnSelIndexChangedSelFieldsList(object source, EventArgs e)
		{
			if (this._fieldMovePending)
			{
				return;
			}
			if (this._propChangesPending)
			{
				this.SaveFieldProperties();
			}
			if (this._selFieldsList.SelectedItems.Count == 0)
			{
				this._currentFieldItem = null;
			}
			else
			{
				this._currentFieldItem = (DataControlFieldsEditor.FieldItem)this._selFieldsList.SelectedItems[0];
			}
			this.SetFieldPropertyHeader();
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001EA04 File Offset: 0x0001CC04
		private void PersistClonedFieldsToControl()
		{
			DataControlFieldCollection dataControlFieldCollection = null;
			if (this.Control is GridView)
			{
				dataControlFieldCollection = ((GridView)this.Control).Columns;
			}
			else if (this.Control is DetailsView)
			{
				dataControlFieldCollection = ((DetailsView)this.Control).Fields;
			}
			if (dataControlFieldCollection != null)
			{
				dataControlFieldCollection.Clear();
				foreach (object obj in this.FieldCollection)
				{
					DataControlField field = (DataControlField)obj;
					dataControlFieldCollection.Add(field);
				}
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001EAA8 File Offset: 0x0001CCA8
		private void SaveFieldProperties()
		{
			if (this._currentFieldItem != null)
			{
				this._currentFieldItem.HeaderText = this._currentFieldItem.RuntimeField.HeaderText;
				if (this._currentFieldProps.Visible)
				{
					this._currentFieldProps.Refresh();
				}
			}
			this._propChangesPending = false;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
		private void SaveComponent()
		{
			if (this._propChangesPending)
			{
				this.SaveFieldProperties();
			}
			this.AutoGenerateFields = this._autoFieldCheck.Checked;
			DataControlFieldCollection fieldCollection = this.FieldCollection;
			if (fieldCollection != null)
			{
				fieldCollection.Clear();
				int count = this._selFieldsList.Items.Count;
				for (int i = 0; i < count; i++)
				{
					DataControlFieldsEditor.FieldItem fieldItem = (DataControlFieldsEditor.FieldItem)this._selFieldsList.Items[i];
					fieldCollection.Add(fieldItem.RuntimeField);
				}
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001EB74 File Offset: 0x0001CD74
		private void SetFieldPropertyHeader()
		{
			string @string = SR.GetString("DCFEditor_FieldProps");
			if (this._currentFieldItem != null)
			{
				this.EnterLoadingMode();
				Type type = this._currentFieldItem.GetType();
				if (type == typeof(DataControlFieldsEditor.CheckBoxFieldItem))
				{
					@string = SR.GetString("DCFEditor_FieldPropsFormat", new object[]
					{
						SR.GetString("DCFEditor_Node_CheckBox")
					});
				}
				else if (type == typeof(DataControlFieldsEditor.BoundFieldItem))
				{
					@string = SR.GetString("DCFEditor_FieldPropsFormat", new object[]
					{
						SR.GetString("DCFEditor_Node_Bound")
					});
				}
				else if (type == typeof(DataControlFieldsEditor.ButtonFieldItem))
				{
					@string = SR.GetString("DCFEditor_FieldPropsFormat", new object[]
					{
						SR.GetString("DCFEditor_Node_Button")
					});
				}
				else if (type == typeof(DataControlFieldsEditor.HyperLinkFieldItem))
				{
					@string = SR.GetString("DCFEditor_FieldPropsFormat", new object[]
					{
						SR.GetString("DCFEditor_Node_HyperLink")
					});
				}
				else if (type == typeof(DataControlFieldsEditor.CommandFieldItem))
				{
					@string = SR.GetString("DCFEditor_FieldPropsFormat", new object[]
					{
						SR.GetString("DCFEditor_Node_Command")
					});
				}
				else if (type == typeof(DataControlFieldsEditor.TemplateFieldItem))
				{
					@string = SR.GetString("DCFEditor_FieldPropsFormat", new object[]
					{
						SR.GetString("DCFEditor_Node_Template")
					});
				}
				else if (type == typeof(DataControlFieldsEditor.ImageFieldItem))
				{
					@string = SR.GetString("DCFEditor_FieldPropsFormat", new object[]
					{
						SR.GetString("DCFEditor_Node_Image")
					});
				}
				this.ExitLoadingMode();
			}
			this._selFieldLabel.Text = @string;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001ED24 File Offset: 0x0001CF24
		private void UpdateEnabledVisibleState()
		{
			DataControlFieldsEditor.AvailableFieldNode availableFieldNode = (DataControlFieldsEditor.AvailableFieldNode)this._availableFieldsTree.SelectedNode;
			int count = this._selFieldsList.Items.Count;
			int count2 = this._selFieldsList.SelectedItems.Count;
			DataControlFieldsEditor.FieldItem fieldItem = null;
			int num = -1;
			if (count2 != 0)
			{
				fieldItem = (DataControlFieldsEditor.FieldItem)this._selFieldsList.SelectedItems[0];
			}
			if (fieldItem != null)
			{
				num = fieldItem.Index;
			}
			bool enabled = num != -1;
			this._addFieldButton.Enabled = (availableFieldNode != null && availableFieldNode.IsFieldCreator);
			this._deleteFieldButton.Enabled = enabled;
			this.UpdateFieldPositionButtonsState();
			this._currentFieldProps.Enabled = (fieldItem != null);
			this._currentFieldProps.SelectedObject = ((fieldItem != null && this._selFieldsList.Focused) ? fieldItem.RuntimeField : null);
			Type type = (fieldItem == null) ? null : fieldItem.RuntimeField.GetType();
			this._templatizeLink.Visible = (count != 0 && fieldItem != null && (type == typeof(BoundField) || type == typeof(CheckBoxField) || type == typeof(ButtonField) || type == typeof(HyperLinkField) || type == typeof(CommandField) || type == typeof(ImageField) || this._customFieldDesigners.ContainsKey(type)));
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		private void UpdateFieldPositionButtonsState()
		{
			int num = -1;
			int count = this._selFieldsList.SelectedItems.Count;
			DataControlFieldsEditor.FieldItem fieldItem = null;
			if (count > 0)
			{
				fieldItem = (this._selFieldsList.SelectedItems[0] as DataControlFieldsEditor.FieldItem);
			}
			if (fieldItem != null)
			{
				num = fieldItem.Index;
			}
			this._moveFieldUpButton.Enabled = (num > 0);
			this._moveFieldDownButton.Enabled = (num >= 0 && num < this._selFieldsList.Items.Count - 1);
		}

		// Token: 0x040002F2 RID: 754
		private const int ILI_DATASOURCE = 0;

		// Token: 0x040002F3 RID: 755
		private const int ILI_BOUND = 1;

		// Token: 0x040002F4 RID: 756
		private const int ILI_ALL = 2;

		// Token: 0x040002F5 RID: 757
		private const int ILI_CUSTOM = 3;

		// Token: 0x040002F6 RID: 758
		private const int ILI_BUTTON = 4;

		// Token: 0x040002F7 RID: 759
		private const int ILI_SELECTBUTTON = 5;

		// Token: 0x040002F8 RID: 760
		private const int ILI_EDITBUTTON = 6;

		// Token: 0x040002F9 RID: 761
		private const int ILI_DELETEBUTTON = 7;

		// Token: 0x040002FA RID: 762
		private const int ILI_HYPERLINK = 8;

		// Token: 0x040002FB RID: 763
		private const int ILI_TEMPLATE = 9;

		// Token: 0x040002FC RID: 764
		private const int ILI_CHECKBOX = 10;

		// Token: 0x040002FD RID: 765
		private const int ILI_INSERTBUTTON = 11;

		// Token: 0x040002FE RID: 766
		private const int ILI_COMMAND = 12;

		// Token: 0x040002FF RID: 767
		private const int ILI_BOOLDATASOURCE = 13;

		// Token: 0x04000300 RID: 768
		private const int ILI_IMAGE = 14;

		// Token: 0x04000301 RID: 769
		private const int ILI_FIELDDESIGNER = 15;

		// Token: 0x04000302 RID: 770
		private const int CF_EDIT = 0;

		// Token: 0x04000303 RID: 771
		private const int CF_INSERT = 1;

		// Token: 0x04000304 RID: 772
		private const int CF_SELECT = 2;

		// Token: 0x04000305 RID: 773
		private const int CF_DELETE = 3;

		// Token: 0x04000306 RID: 774
		private const int MODE_READONLY = 0;

		// Token: 0x04000307 RID: 775
		private const int MODE_EDIT = 1;

		// Token: 0x04000308 RID: 776
		private const int MODE_INSERT = 2;

		// Token: 0x04000318 RID: 792
		private DataControlFieldsEditor.DataSourceNode _selectedDataSourceNode;

		// Token: 0x04000319 RID: 793
		private DataControlFieldsEditor.BoolDataSourceNode _selectedCheckBoxDataSourceNode;

		// Token: 0x0400031A RID: 794
		private DataControlFieldsEditor.FieldItem _currentFieldItem;

		// Token: 0x0400031B RID: 795
		private bool _propChangesPending;

		// Token: 0x0400031C RID: 796
		private bool _fieldMovePending;

		// Token: 0x0400031D RID: 797
		private DataControlFieldCollection _clonedFieldCollection;

		// Token: 0x0400031E RID: 798
		private DataBoundControlDesigner _controlDesigner;

		// Token: 0x0400031F RID: 799
		private bool _isLoading;

		// Token: 0x04000320 RID: 800
		private IDataSourceFieldSchema[] _fieldSchemas;

		// Token: 0x04000321 RID: 801
		private IDataSourceViewSchema _viewSchema;

		// Token: 0x04000322 RID: 802
		private bool _initialActivate;

		// Token: 0x04000323 RID: 803
		private bool _initialIgnoreRefreshSchemaValue;

		// Token: 0x04000324 RID: 804
		private IDictionary<Type, DataControlFieldDesigner> _customFieldDesigners;

		// Token: 0x020003E1 RID: 993
		private abstract class AvailableFieldNode : System.Windows.Forms.TreeNode
		{
			// Token: 0x06002733 RID: 10035 RVA: 0x000F162E File Offset: 0x000EF82E
			public AvailableFieldNode(string text, int icon) : base(text, icon, icon)
			{
			}

			// Token: 0x1700083D RID: 2109
			// (get) Token: 0x06002734 RID: 10036 RVA: 0x0000445B File Offset: 0x0000265B
			public virtual bool CreatesMultipleFields
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700083E RID: 2110
			// (get) Token: 0x06002735 RID: 10037 RVA: 0x00003B0F File Offset: 0x00001D0F
			public virtual bool IsFieldCreator
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002736 RID: 10038 RVA: 0x00003598 File Offset: 0x00001798
			public virtual DataControlFieldsEditor.FieldItem CreateField()
			{
				return null;
			}

			// Token: 0x06002737 RID: 10039 RVA: 0x00003598 File Offset: 0x00001798
			public virtual DataControlFieldsEditor.FieldItem[] CreateFields(DataBoundControl control, IDataSourceFieldSchema[] fieldSchemas)
			{
				return null;
			}
		}

		// Token: 0x020003E2 RID: 994
		private class DataSourceNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x06002738 RID: 10040 RVA: 0x000F1639 File Offset: 0x000EF839
			public DataSourceNode() : base(SR.GetString("DCFEditor_Node_Bound"), 0)
			{
			}

			// Token: 0x06002739 RID: 10041 RVA: 0x000F164C File Offset: 0x000EF84C
			public DataSourceNode(string nodeText) : base(nodeText, 0)
			{
			}

			// Token: 0x1700083F RID: 2111
			// (get) Token: 0x0600273A RID: 10042 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool IsFieldCreator
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020003E3 RID: 995
		private class BoolDataSourceNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x0600273B RID: 10043 RVA: 0x000F1656 File Offset: 0x000EF856
			public BoolDataSourceNode() : base(SR.GetString("DCFEditor_Node_CheckBox"), 13)
			{
			}

			// Token: 0x17000840 RID: 2112
			// (get) Token: 0x0600273C RID: 10044 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool IsFieldCreator
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020003E4 RID: 996
		private class DataFieldNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x0600273D RID: 10045 RVA: 0x000F166A File Offset: 0x000EF86A
			public DataFieldNode(DataControlFieldsEditor fieldsEditor) : base(SR.GetString("DCFEditor_Node_AllFields"), 2)
			{
				this._fieldsEditor = fieldsEditor;
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x0600273E RID: 10046 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool CreatesMultipleFields
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600273F RID: 10047 RVA: 0x0000C5AC File Offset: 0x0000A7AC
			public override DataControlFieldsEditor.FieldItem CreateField()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002740 RID: 10048 RVA: 0x000F1684 File Offset: 0x000EF884
			public override DataControlFieldsEditor.FieldItem[] CreateFields(DataBoundControl control, IDataSourceFieldSchema[] fieldSchemas)
			{
				if (fieldSchemas == null)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				foreach (IDataSourceFieldSchema dataSourceFieldSchema in fieldSchemas)
				{
					if (DataBinder.IsBindableType(dataSourceFieldSchema.DataType))
					{
						string name = dataSourceFieldSchema.Name;
						BoundField boundField;
						DataControlFieldsEditor.FieldItem fieldItem;
						if (dataSourceFieldSchema.DataType == typeof(bool) || dataSourceFieldSchema.DataType == typeof(bool?))
						{
							boundField = new CheckBoxField();
							boundField.HeaderText = name;
							boundField.DataField = name;
							boundField.SortExpression = name;
							fieldItem = new DataControlFieldsEditor.CheckBoxFieldItem(this._fieldsEditor, (CheckBoxField)boundField);
						}
						else
						{
							boundField = new BoundField();
							boundField.HeaderText = name;
							boundField.DataField = name;
							boundField.SortExpression = name;
							fieldItem = new DataControlFieldsEditor.BoundFieldItem(this._fieldsEditor, boundField);
						}
						if (dataSourceFieldSchema.PrimaryKey)
						{
							boundField.ReadOnly = true;
						}
						if (dataSourceFieldSchema.Identity)
						{
							boundField.InsertVisible = false;
						}
						fieldItem.LoadFieldInfo();
						arrayList.Add(fieldItem);
					}
				}
				return (DataControlFieldsEditor.FieldItem[])arrayList.ToArray(typeof(DataControlFieldsEditor.FieldItem));
			}

			// Token: 0x04001C2B RID: 7211
			private DataControlFieldsEditor _fieldsEditor;
		}

		// Token: 0x020003E5 RID: 997
		private class BoundNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x06002741 RID: 10049 RVA: 0x000F17B0 File Offset: 0x000EF9B0
			public BoundNode(DataControlFieldsEditor fieldsEditor, IDataSourceFieldSchema fieldSchema) : base((fieldSchema == null) ? string.Empty : fieldSchema.Name, 1)
			{
				this._fieldSchema = fieldSchema;
				this._fieldsEditor = fieldsEditor;
				if (fieldSchema == null)
				{
					this._genericBoundField = true;
					base.Text = SR.GetString("DCFEditor_Node_Bound");
				}
			}

			// Token: 0x06002742 RID: 10050 RVA: 0x000F17FC File Offset: 0x000EF9FC
			public override DataControlFieldsEditor.FieldItem CreateField()
			{
				BoundField boundField = new BoundField();
				string text = string.Empty;
				if (this._fieldSchema != null)
				{
					text = this._fieldSchema.Name;
				}
				if (!this._genericBoundField)
				{
					boundField.HeaderText = text;
					boundField.DataField = text;
					boundField.SortExpression = text;
				}
				if (this._fieldSchema != null)
				{
					if (this._fieldSchema.PrimaryKey)
					{
						boundField.ReadOnly = true;
					}
					if (this._fieldSchema.Identity)
					{
						boundField.InsertVisible = false;
					}
				}
				DataControlFieldsEditor.FieldItem fieldItem = new DataControlFieldsEditor.BoundFieldItem(this._fieldsEditor, boundField);
				fieldItem.LoadFieldInfo();
				return fieldItem;
			}

			// Token: 0x04001C2C RID: 7212
			protected IDataSourceFieldSchema _fieldSchema;

			// Token: 0x04001C2D RID: 7213
			private bool _genericBoundField;

			// Token: 0x04001C2E RID: 7214
			private DataControlFieldsEditor _fieldsEditor;
		}

		// Token: 0x020003E6 RID: 998
		private class ButtonNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x06002743 RID: 10051 RVA: 0x000F188A File Offset: 0x000EFA8A
			public ButtonNode(DataControlFieldsEditor fieldsEditor) : this(fieldsEditor, string.Empty, SR.GetString("DCFEditor_Button"), SR.GetString("DCFEditor_Node_Button"))
			{
			}

			// Token: 0x06002744 RID: 10052 RVA: 0x000F18AC File Offset: 0x000EFAAC
			public ButtonNode(DataControlFieldsEditor fieldsEditor, string command, string buttonText, string text) : base(text, 4)
			{
				this._fieldsEditor = fieldsEditor;
				this.command = command;
				this.buttonText = buttonText;
			}

			// Token: 0x06002745 RID: 10053 RVA: 0x000F18CC File Offset: 0x000EFACC
			public override DataControlFieldsEditor.FieldItem CreateField()
			{
				ButtonField buttonField = new ButtonField();
				buttonField.Text = this.buttonText;
				buttonField.CommandName = this.command;
				DataControlFieldsEditor.FieldItem fieldItem = new DataControlFieldsEditor.ButtonFieldItem(this._fieldsEditor, buttonField);
				fieldItem.LoadFieldInfo();
				return fieldItem;
			}

			// Token: 0x04001C2F RID: 7215
			private string command;

			// Token: 0x04001C30 RID: 7216
			private string buttonText;

			// Token: 0x04001C31 RID: 7217
			private DataControlFieldsEditor _fieldsEditor;
		}

		// Token: 0x020003E7 RID: 999
		private class CheckBoxNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x06002746 RID: 10054 RVA: 0x000F190C File Offset: 0x000EFB0C
			public CheckBoxNode(DataControlFieldsEditor fieldsEditor, IDataSourceFieldSchema fieldSchema) : base((fieldSchema == null) ? string.Empty : fieldSchema.Name, 10)
			{
				this._fieldsEditor = fieldsEditor;
				this._fieldSchema = fieldSchema;
				if (fieldSchema == null)
				{
					this._genericCheckBoxField = true;
					base.Text = SR.GetString("DCFEditor_Node_CheckBox");
				}
			}

			// Token: 0x06002747 RID: 10055 RVA: 0x000F195C File Offset: 0x000EFB5C
			public override DataControlFieldsEditor.FieldItem CreateField()
			{
				CheckBoxField checkBoxField = new CheckBoxField();
				string text = string.Empty;
				if (this._fieldSchema != null)
				{
					text = this._fieldSchema.Name;
				}
				if (!this._genericCheckBoxField)
				{
					checkBoxField.HeaderText = text;
					checkBoxField.DataField = text;
					checkBoxField.SortExpression = text;
				}
				if (this._fieldSchema != null)
				{
					if (this._fieldSchema.PrimaryKey)
					{
						checkBoxField.ReadOnly = true;
					}
					if (this._fieldSchema.Identity)
					{
						checkBoxField.InsertVisible = false;
					}
				}
				DataControlFieldsEditor.FieldItem fieldItem = new DataControlFieldsEditor.CheckBoxFieldItem(this._fieldsEditor, checkBoxField);
				fieldItem.LoadFieldInfo();
				return fieldItem;
			}

			// Token: 0x04001C32 RID: 7218
			protected IDataSourceFieldSchema _fieldSchema;

			// Token: 0x04001C33 RID: 7219
			private bool _genericCheckBoxField;

			// Token: 0x04001C34 RID: 7220
			private DataControlFieldsEditor _fieldsEditor;
		}

		// Token: 0x020003E8 RID: 1000
		private class ImageNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x06002748 RID: 10056 RVA: 0x000F19EA File Offset: 0x000EFBEA
			public ImageNode(DataControlFieldsEditor fieldsEditor) : base(string.Empty, 14)
			{
				this._fieldsEditor = fieldsEditor;
				base.Text = SR.GetString("DCFEditor_Node_Image");
			}

			// Token: 0x06002749 RID: 10057 RVA: 0x000F1A10 File Offset: 0x000EFC10
			public override DataControlFieldsEditor.FieldItem CreateField()
			{
				ImageField runtimeField = new ImageField();
				DataControlFieldsEditor.FieldItem fieldItem = new DataControlFieldsEditor.ImageFieldItem(this._fieldsEditor, runtimeField);
				fieldItem.LoadFieldInfo();
				return fieldItem;
			}

			// Token: 0x04001C35 RID: 7221
			private DataControlFieldsEditor _fieldsEditor;
		}

		// Token: 0x020003E9 RID: 1001
		private class CommandNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x0600274A RID: 10058 RVA: 0x000F1A37 File Offset: 0x000EFC37
			public CommandNode(DataControlFieldsEditor fieldsEditor) : this(fieldsEditor, -1, SR.GetString("DCFEditor_Node_Command"), 12)
			{
			}

			// Token: 0x0600274B RID: 10059 RVA: 0x000F1A4D File Offset: 0x000EFC4D
			public CommandNode(DataControlFieldsEditor fieldsEditor, int commandType, string text, int icon) : base(text, icon)
			{
				this.commandType = commandType;
				this._fieldsEditor = fieldsEditor;
			}

			// Token: 0x0600274C RID: 10060 RVA: 0x000F1A68 File Offset: 0x000EFC68
			public override DataControlFieldsEditor.FieldItem CreateField()
			{
				CommandField commandField = new CommandField();
				switch (this.commandType)
				{
				case 0:
					commandField.ShowEditButton = true;
					break;
				case 1:
					commandField.ShowInsertButton = true;
					break;
				case 2:
					commandField.ShowSelectButton = true;
					break;
				case 3:
					commandField.ShowDeleteButton = true;
					break;
				}
				DataControlFieldsEditor.FieldItem fieldItem = new DataControlFieldsEditor.CommandFieldItem(this._fieldsEditor, commandField);
				fieldItem.LoadFieldInfo();
				return fieldItem;
			}

			// Token: 0x04001C36 RID: 7222
			private int commandType;

			// Token: 0x04001C37 RID: 7223
			private DataControlFieldsEditor _fieldsEditor;
		}

		// Token: 0x020003EA RID: 1002
		private class HyperLinkNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x0600274D RID: 10061 RVA: 0x000F1AD0 File Offset: 0x000EFCD0
			public HyperLinkNode(DataControlFieldsEditor fieldsEditor) : this(fieldsEditor, SR.GetString("DCFEditor_HyperLink"))
			{
			}

			// Token: 0x0600274E RID: 10062 RVA: 0x000F1AE3 File Offset: 0x000EFCE3
			public HyperLinkNode(DataControlFieldsEditor fieldsEditor, string hyperLinkText) : base(SR.GetString("DCFEditor_Node_HyperLink"), 8)
			{
				this._fieldsEditor = fieldsEditor;
				this.hyperLinkText = hyperLinkText;
			}

			// Token: 0x0600274F RID: 10063 RVA: 0x000F1B04 File Offset: 0x000EFD04
			public override DataControlFieldsEditor.FieldItem CreateField()
			{
				HyperLinkField runtimeField = new HyperLinkField();
				DataControlFieldsEditor.FieldItem fieldItem = new DataControlFieldsEditor.HyperLinkFieldItem(this._fieldsEditor, runtimeField);
				fieldItem.Text = this.hyperLinkText;
				fieldItem.LoadFieldInfo();
				return fieldItem;
			}

			// Token: 0x04001C38 RID: 7224
			private string hyperLinkText;

			// Token: 0x04001C39 RID: 7225
			private DataControlFieldsEditor _fieldsEditor;
		}

		// Token: 0x020003EB RID: 1003
		private class TemplateNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x06002750 RID: 10064 RVA: 0x000F1B37 File Offset: 0x000EFD37
			public TemplateNode(DataControlFieldsEditor fieldsEditor) : base(SR.GetString("DCFEditor_Node_Template"), 9)
			{
				this._fieldsEditor = fieldsEditor;
			}

			// Token: 0x06002751 RID: 10065 RVA: 0x000F1B54 File Offset: 0x000EFD54
			public override DataControlFieldsEditor.FieldItem CreateField()
			{
				TemplateField runtimeField = new TemplateField();
				DataControlFieldsEditor.FieldItem fieldItem = new DataControlFieldsEditor.TemplateFieldItem(this._fieldsEditor, runtimeField);
				fieldItem.LoadFieldInfo();
				return fieldItem;
			}

			// Token: 0x04001C3A RID: 7226
			private DataControlFieldsEditor _fieldsEditor;
		}

		// Token: 0x020003EC RID: 1004
		private abstract class FieldItem : ListViewItem
		{
			// Token: 0x06002752 RID: 10066 RVA: 0x000F1B7B File Offset: 0x000EFD7B
			public FieldItem(DataControlFieldsEditor fieldsEditor, DataControlField runtimeField, int image) : base(string.Empty, image)
			{
				this.fieldsEditor = fieldsEditor;
				this.runtimeField = runtimeField;
				base.Text = this.GetNodeText(null);
			}

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06002753 RID: 10067 RVA: 0x000F1BA4 File Offset: 0x000EFDA4
			// (set) Token: 0x06002754 RID: 10068 RVA: 0x000F1BB1 File Offset: 0x000EFDB1
			public string HeaderText
			{
				get
				{
					return this.runtimeField.HeaderText;
				}
				set
				{
					this.runtimeField.HeaderText = value;
					this.UpdateDisplayText();
				}
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06002755 RID: 10069 RVA: 0x000F1BC5 File Offset: 0x000EFDC5
			public DataControlField RuntimeField
			{
				get
				{
					return this.runtimeField;
				}
			}

			// Token: 0x06002756 RID: 10070 RVA: 0x000F1BCD File Offset: 0x000EFDCD
			protected virtual string GetDefaultNodeText()
			{
				return this.runtimeField.GetType().Name;
			}

			// Token: 0x06002757 RID: 10071 RVA: 0x000F1BDF File Offset: 0x000EFDDF
			public virtual string GetNodeText(string headerText)
			{
				if (headerText == null || headerText.Length == 0)
				{
					return this.GetDefaultNodeText();
				}
				return headerText;
			}

			// Token: 0x06002758 RID: 10072 RVA: 0x0001CA22 File Offset: 0x0001AC22
			protected ITemplate GetTemplate(DataBoundControl control, string templateContent)
			{
				return DataControlFieldHelper.GetTemplate(control, templateContent);
			}

			// Token: 0x06002759 RID: 10073 RVA: 0x000F1BF4 File Offset: 0x000EFDF4
			public virtual TemplateField GetTemplateField(DataBoundControl dataBoundControl)
			{
				return DataControlFieldHelper.GetTemplateField(this.runtimeField, dataBoundControl);
			}

			// Token: 0x0600275A RID: 10074 RVA: 0x000F1C02 File Offset: 0x000EFE02
			public virtual void LoadFieldInfo()
			{
				this.UpdateDisplayText();
			}

			// Token: 0x0600275B RID: 10075 RVA: 0x000F1C0A File Offset: 0x000EFE0A
			protected string PrepareFormatString(string formatString)
			{
				return formatString.Replace("'", "&#039;");
			}

			// Token: 0x0600275C RID: 10076 RVA: 0x000F1C1C File Offset: 0x000EFE1C
			protected void UpdateDisplayText()
			{
				base.Text = this.GetNodeText(this.HeaderText);
			}

			// Token: 0x04001C3B RID: 7227
			protected DataControlField runtimeField;

			// Token: 0x04001C3C RID: 7228
			protected DataControlFieldsEditor fieldsEditor;
		}

		// Token: 0x020003ED RID: 1005
		private class BoundFieldItem : DataControlFieldsEditor.FieldItem
		{
			// Token: 0x0600275D RID: 10077 RVA: 0x000F1C30 File Offset: 0x000EFE30
			public BoundFieldItem(DataControlFieldsEditor fieldsEditor, BoundField runtimeField) : base(fieldsEditor, runtimeField, 1)
			{
			}

			// Token: 0x0600275E RID: 10078 RVA: 0x000F1C3C File Offset: 0x000EFE3C
			protected override string GetDefaultNodeText()
			{
				string dataField = ((BoundField)base.RuntimeField).DataField;
				if (dataField != null && dataField.Length != 0)
				{
					return dataField;
				}
				return SR.GetString("DCFEditor_Node_Bound");
			}

			// Token: 0x0600275F RID: 10079 RVA: 0x000F1C74 File Offset: 0x000EFE74
			public override TemplateField GetTemplateField(DataBoundControl dataBoundControl)
			{
				TemplateField templateField = base.GetTemplateField(dataBoundControl);
				templateField.SortExpression = base.RuntimeField.SortExpression;
				templateField.ItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(0, false));
				templateField.ConvertEmptyStringToNull = ((BoundField)base.RuntimeField).ConvertEmptyStringToNull;
				templateField.EditItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(1, ((BoundField)base.RuntimeField).ReadOnly));
				if (dataBoundControl is DetailsView && ((BoundField)base.RuntimeField).InsertVisible)
				{
					templateField.InsertItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(2, false));
				}
				return templateField;
			}

			// Token: 0x06002760 RID: 10080 RVA: 0x000F1D1C File Offset: 0x000EFF1C
			private string GetTemplateContent(int editMode, bool readOnly)
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = editMode == 1 && readOnly;
				Type type = (editMode == 0 || flag) ? typeof(System.Web.UI.WebControls.Label) : typeof(System.Web.UI.WebControls.TextBox);
				string dataFormatString = ((BoundField)base.RuntimeField).DataFormatString;
				string dataField = ((BoundField)base.RuntimeField).DataField;
				string format = string.Empty;
				if (editMode != 1 || ((BoundField)base.RuntimeField).ApplyFormatInEditMode || flag)
				{
					format = base.PrepareFormatString(dataFormatString);
				}
				string value = flag ? DesignTimeDataBinding.CreateEvalExpression(dataField, format) : DesignTimeDataBinding.CreateBindExpression(dataField, format);
				if (editMode == 2 && !((BoundField)base.RuntimeField).InsertVisible)
				{
					return string.Empty;
				}
				stringBuilder.Append("<asp:");
				stringBuilder.Append(type.Name);
				stringBuilder.Append(" runat=\"server\"");
				if (dataField.Length != 0)
				{
					stringBuilder.Append(" Text='<%# ");
					stringBuilder.Append(value);
					stringBuilder.Append(" %>'");
				}
				stringBuilder.Append(" id=\"");
				stringBuilder.Append(this.fieldsEditor.GetNewDataSourceName(type, editMode));
				stringBuilder.Append("\"></asp:");
				stringBuilder.Append(type.Name);
				stringBuilder.Append(">");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x020003EE RID: 1006
		private class ButtonFieldItem : DataControlFieldsEditor.FieldItem
		{
			// Token: 0x06002761 RID: 10081 RVA: 0x000F1E72 File Offset: 0x000F0072
			public ButtonFieldItem(DataControlFieldsEditor fieldsEditor, ButtonField runtimeField) : base(fieldsEditor, runtimeField, 4)
			{
			}

			// Token: 0x06002762 RID: 10082 RVA: 0x000F1E80 File Offset: 0x000F0080
			protected override string GetDefaultNodeText()
			{
				string text = ((ButtonField)this.runtimeField).Text;
				if (text != null && text.Length != 0)
				{
					return text;
				}
				return SR.GetString("DCFEditor_Node_Button");
			}

			// Token: 0x06002763 RID: 10083 RVA: 0x000F1EB8 File Offset: 0x000F00B8
			public override TemplateField GetTemplateField(DataBoundControl dataBoundControl)
			{
				TemplateField templateField = base.GetTemplateField(dataBoundControl);
				ButtonField buttonField = (ButtonField)base.RuntimeField;
				StringBuilder stringBuilder = new StringBuilder();
				Type typeFromHandle = typeof(System.Web.UI.WebControls.Button);
				if (buttonField.ButtonType == ButtonType.Link)
				{
					typeFromHandle = typeof(LinkButton);
				}
				else if (buttonField.ButtonType == ButtonType.Image)
				{
					typeFromHandle = typeof(ImageButton);
				}
				stringBuilder.Append("<asp:");
				stringBuilder.Append(typeFromHandle.Name);
				stringBuilder.Append(" runat=\"server\"");
				if (buttonField.DataTextField.Length != 0)
				{
					stringBuilder.Append(" Text='<%# ");
					stringBuilder.Append(DesignTimeDataBinding.CreateEvalExpression(buttonField.DataTextField, base.PrepareFormatString(buttonField.DataTextFormatString)));
					stringBuilder.Append(" %>'");
				}
				else
				{
					stringBuilder.Append(" Text=\"");
					stringBuilder.Append(buttonField.Text);
					stringBuilder.Append("\"");
				}
				stringBuilder.Append(" CommandName=\"");
				stringBuilder.Append(buttonField.CommandName);
				stringBuilder.Append("\"");
				if (buttonField.ButtonType == ButtonType.Image && buttonField.ImageUrl.Length > 0)
				{
					stringBuilder.Append(" ImageUrl=\"");
					stringBuilder.Append(buttonField.ImageUrl);
					stringBuilder.Append("\"");
				}
				stringBuilder.Append(" CausesValidation=\"false\" id=\"");
				stringBuilder.Append(this.fieldsEditor.GetNewDataSourceName(typeFromHandle, 0));
				stringBuilder.Append("\"></asp:");
				stringBuilder.Append(typeFromHandle.Name);
				stringBuilder.Append(">");
				templateField.ItemTemplate = base.GetTemplate(dataBoundControl, stringBuilder.ToString());
				return templateField;
			}
		}

		// Token: 0x020003EF RID: 1007
		private class CheckBoxFieldItem : DataControlFieldsEditor.FieldItem
		{
			// Token: 0x06002764 RID: 10084 RVA: 0x000F205D File Offset: 0x000F025D
			public CheckBoxFieldItem(DataControlFieldsEditor fieldsEditor, CheckBoxField runtimeField) : base(fieldsEditor, runtimeField, 10)
			{
			}

			// Token: 0x06002765 RID: 10085 RVA: 0x000F206C File Offset: 0x000F026C
			protected override string GetDefaultNodeText()
			{
				string dataField = ((CheckBoxField)base.RuntimeField).DataField;
				if (dataField != null && dataField.Length != 0)
				{
					return dataField;
				}
				return SR.GetString("DCFEditor_Node_CheckBox");
			}

			// Token: 0x06002766 RID: 10086 RVA: 0x000F20A4 File Offset: 0x000F02A4
			public override TemplateField GetTemplateField(DataBoundControl dataBoundControl)
			{
				TemplateField templateField = base.GetTemplateField(dataBoundControl);
				CheckBoxField checkBoxField = (CheckBoxField)base.RuntimeField;
				templateField.SortExpression = checkBoxField.SortExpression;
				templateField.ItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(0));
				if (!checkBoxField.ReadOnly)
				{
					templateField.EditItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(1));
				}
				if (dataBoundControl is DetailsView && ((CheckBoxField)base.RuntimeField).InsertVisible)
				{
					templateField.InsertItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(2));
				}
				return templateField;
			}

			// Token: 0x06002767 RID: 10087 RVA: 0x000F2130 File Offset: 0x000F0330
			private string GetTemplateContent(int editMode)
			{
				StringBuilder stringBuilder = new StringBuilder();
				Type typeFromHandle = typeof(System.Web.UI.WebControls.CheckBox);
				if (editMode == 2 && !((CheckBoxField)base.RuntimeField).InsertVisible)
				{
					return string.Empty;
				}
				stringBuilder.Append("<asp:");
				stringBuilder.Append(typeFromHandle.Name);
				stringBuilder.Append(" runat=\"server\"");
				string dataField = ((CheckBoxField)base.RuntimeField).DataField;
				if (dataField.Length != 0)
				{
					stringBuilder.Append(" Checked='<%# ");
					stringBuilder.Append(DesignTimeDataBinding.CreateBindExpression(dataField, string.Empty));
					stringBuilder.Append(" %>'");
					if (editMode == 0)
					{
						stringBuilder.Append(" Enabled=\"false\"");
					}
				}
				stringBuilder.Append(" id=\"");
				stringBuilder.Append(this.fieldsEditor.GetNewDataSourceName(typeFromHandle, editMode));
				stringBuilder.Append("\"></asp:");
				stringBuilder.Append(typeFromHandle.Name);
				stringBuilder.Append(">");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x020003F0 RID: 1008
		private class ImageFieldItem : DataControlFieldsEditor.FieldItem
		{
			// Token: 0x06002768 RID: 10088 RVA: 0x000F222C File Offset: 0x000F042C
			public ImageFieldItem(DataControlFieldsEditor fieldsEditor, ImageField runtimeField) : base(fieldsEditor, runtimeField, 14)
			{
			}

			// Token: 0x06002769 RID: 10089 RVA: 0x000F2238 File Offset: 0x000F0438
			protected override string GetDefaultNodeText()
			{
				string dataImageUrlField = ((ImageField)base.RuntimeField).DataImageUrlField;
				if (dataImageUrlField != null && dataImageUrlField.Length != 0)
				{
					return dataImageUrlField;
				}
				return SR.GetString("DCFEditor_Node_Image");
			}

			// Token: 0x0600276A RID: 10090 RVA: 0x000F2270 File Offset: 0x000F0470
			public override TemplateField GetTemplateField(DataBoundControl dataBoundControl)
			{
				TemplateField templateField = base.GetTemplateField(dataBoundControl);
				ImageField imageField = (ImageField)base.RuntimeField;
				templateField.SortExpression = imageField.SortExpression;
				templateField.ItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(0));
				templateField.ConvertEmptyStringToNull = imageField.ConvertEmptyStringToNull;
				if (!imageField.ReadOnly)
				{
					templateField.EditItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(1));
					if (dataBoundControl is DetailsView)
					{
						templateField.InsertItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(2));
					}
				}
				return templateField;
			}

			// Token: 0x0600276B RID: 10091 RVA: 0x000F22F8 File Offset: 0x000F04F8
			private string GetTemplateContent(int editMode)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string dataImageUrlField = ((ImageField)base.RuntimeField).DataImageUrlField;
				string dataAlternateTextField = ((ImageField)this.runtimeField).DataAlternateTextField;
				string text;
				if (dataAlternateTextField.Length > 0)
				{
					string dataAlternateTextFormatString = ((ImageField)this.runtimeField).DataAlternateTextFormatString;
					text = "'<%# " + DesignTimeDataBinding.CreateEvalExpression(dataAlternateTextField, base.PrepareFormatString(dataAlternateTextFormatString)) + " %>'";
				}
				else
				{
					text = ((ImageField)this.runtimeField).AlternateText;
				}
				Type typeFromHandle;
				if (editMode == 0)
				{
					typeFromHandle = typeof(System.Web.UI.WebControls.Image);
				}
				else
				{
					typeFromHandle = typeof(System.Web.UI.WebControls.TextBox);
				}
				stringBuilder.Append("<asp:");
				stringBuilder.Append(typeFromHandle.Name);
				stringBuilder.Append(" runat=\"server\"");
				if (dataImageUrlField.Length > 0)
				{
					if (typeFromHandle == typeof(System.Web.UI.WebControls.Image))
					{
						stringBuilder.Append(" ImageUrl='<%# ");
						stringBuilder.Append(DesignTimeDataBinding.CreateEvalExpression(dataImageUrlField, base.PrepareFormatString(((ImageField)this.runtimeField).DataImageUrlFormatString)));
					}
					else if (typeFromHandle == typeof(System.Web.UI.WebControls.TextBox))
					{
						stringBuilder.Append(" Text='<%# ");
						stringBuilder.Append(DesignTimeDataBinding.CreateEvalExpression(dataImageUrlField, string.Empty));
					}
					stringBuilder.Append(" %>' ");
				}
				if (text.Length > 0)
				{
					if (typeFromHandle == typeof(System.Web.UI.WebControls.TextBox))
					{
						stringBuilder.Append(" Tooltip=");
					}
					else
					{
						stringBuilder.Append(" AlternateText=");
					}
					stringBuilder.Append(text);
				}
				stringBuilder.Append(" id=\"");
				stringBuilder.Append(this.fieldsEditor.GetNewDataSourceName(typeFromHandle, editMode));
				stringBuilder.Append("\"></asp:");
				stringBuilder.Append(typeFromHandle.Name);
				stringBuilder.Append(">");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x020003F1 RID: 1009
		private class HyperLinkFieldItem : DataControlFieldsEditor.FieldItem
		{
			// Token: 0x0600276C RID: 10092 RVA: 0x000F24CC File Offset: 0x000F06CC
			public HyperLinkFieldItem(DataControlFieldsEditor fieldsEditor, HyperLinkField runtimeField) : base(fieldsEditor, runtimeField, 8)
			{
			}

			// Token: 0x0600276D RID: 10093 RVA: 0x000F24D8 File Offset: 0x000F06D8
			protected override string GetDefaultNodeText()
			{
				string text = ((HyperLinkField)base.RuntimeField).Text;
				if (text != null && text.Length != 0)
				{
					return text;
				}
				return SR.GetString("DCFEditor_Node_HyperLink");
			}

			// Token: 0x0600276E RID: 10094 RVA: 0x000F2510 File Offset: 0x000F0710
			public override TemplateField GetTemplateField(DataBoundControl dataBoundControl)
			{
				TemplateField templateField = base.GetTemplateField(dataBoundControl);
				HyperLinkField hyperLinkField = (HyperLinkField)base.RuntimeField;
				Type typeFromHandle = typeof(HyperLink);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<asp:");
				stringBuilder.Append(typeFromHandle.Name);
				stringBuilder.Append(" runat=\"server\"");
				if (hyperLinkField.DataTextField.Length != 0)
				{
					stringBuilder.Append(" Text='<%# ");
					stringBuilder.Append(DesignTimeDataBinding.CreateEvalExpression(hyperLinkField.DataTextField, base.PrepareFormatString(hyperLinkField.DataTextFormatString)));
					stringBuilder.Append(" %>'");
				}
				else
				{
					stringBuilder.Append(" Text=\"");
					stringBuilder.Append(hyperLinkField.Text);
					stringBuilder.Append("\"");
				}
				if (hyperLinkField.DataNavigateUrlFields.Length != 0 && hyperLinkField.DataNavigateUrlFields[0].Length > 0)
				{
					stringBuilder.Append(" NavigateUrl='<%# ");
					stringBuilder.Append(DesignTimeDataBinding.CreateEvalExpression(hyperLinkField.DataNavigateUrlFields[0], base.PrepareFormatString(hyperLinkField.DataNavigateUrlFormatString)));
					stringBuilder.Append(" %>'");
				}
				else
				{
					stringBuilder.Append(" NavigateUrl=\"");
					stringBuilder.Append(hyperLinkField.NavigateUrl);
					stringBuilder.Append("\"");
				}
				if (hyperLinkField.Target.Length != 0)
				{
					stringBuilder.Append(" Target=\"");
					stringBuilder.Append(hyperLinkField.Target);
					stringBuilder.Append("\"");
				}
				stringBuilder.Append(" id=\"");
				stringBuilder.Append(this.fieldsEditor.GetNewDataSourceName(typeFromHandle, 0));
				stringBuilder.Append("\"></asp:");
				stringBuilder.Append(typeFromHandle.Name);
				stringBuilder.Append(">");
				templateField.ItemTemplate = base.GetTemplate(dataBoundControl, stringBuilder.ToString());
				return templateField;
			}
		}

		// Token: 0x020003F2 RID: 1010
		private class TemplateFieldItem : DataControlFieldsEditor.FieldItem
		{
			// Token: 0x0600276F RID: 10095 RVA: 0x000F26D4 File Offset: 0x000F08D4
			public TemplateFieldItem(DataControlFieldsEditor fieldsEditor, TemplateField runtimeField) : base(fieldsEditor, runtimeField, 9)
			{
			}

			// Token: 0x06002770 RID: 10096 RVA: 0x000F26E0 File Offset: 0x000F08E0
			protected override string GetDefaultNodeText()
			{
				return SR.GetString("DCFEditor_Node_Template");
			}
		}

		// Token: 0x020003F3 RID: 1011
		private class CommandFieldItem : DataControlFieldsEditor.FieldItem
		{
			// Token: 0x06002771 RID: 10097 RVA: 0x000F26EC File Offset: 0x000F08EC
			public CommandFieldItem(DataControlFieldsEditor fieldsEditor, CommandField runtimeField) : base(fieldsEditor, runtimeField, 12)
			{
				this.UpdateImageIndex();
			}

			// Token: 0x06002772 RID: 10098 RVA: 0x000F2700 File Offset: 0x000F0900
			private string BuildButtonString(Type controlType, string buttonText, string commandName, string imageUrl, bool causesValidation, int mode, ref int buttonNameStartIndex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<asp:");
				stringBuilder.Append(controlType.Name);
				stringBuilder.Append(" runat=\"server\"");
				stringBuilder.Append(" Text=\"");
				stringBuilder.Append(buttonText);
				stringBuilder.Append("\"");
				stringBuilder.Append(" CommandName=\"");
				stringBuilder.Append(commandName);
				if (imageUrl != null && imageUrl.Length > 0)
				{
					stringBuilder.Append("\" ImageUrl=\"");
					stringBuilder.Append(imageUrl);
				}
				stringBuilder.Append("\" CausesValidation=\"");
				stringBuilder.Append(causesValidation.ToString());
				stringBuilder.Append("\" id=\"");
				stringBuilder.Append(this.fieldsEditor.GetNewDataSourceName(controlType, mode, ref buttonNameStartIndex));
				stringBuilder.Append("\"></asp:");
				stringBuilder.Append(controlType.Name);
				stringBuilder.Append(">");
				return stringBuilder.ToString();
			}

			// Token: 0x06002773 RID: 10099 RVA: 0x000F27F8 File Offset: 0x000F09F8
			protected override string GetDefaultNodeText()
			{
				CommandField commandField = (CommandField)base.RuntimeField;
				if (commandField.ShowEditButton && !commandField.ShowDeleteButton && !commandField.ShowSelectButton && !commandField.ShowInsertButton)
				{
					return SR.GetString("DCFEditor_Node_Edit");
				}
				if (commandField.ShowDeleteButton && !commandField.ShowEditButton && !commandField.ShowSelectButton && !commandField.ShowInsertButton)
				{
					return SR.GetString("DCFEditor_Node_Delete");
				}
				if (commandField.ShowSelectButton && !commandField.ShowDeleteButton && !commandField.ShowEditButton && !commandField.ShowInsertButton)
				{
					return SR.GetString("DCFEditor_Node_Select");
				}
				if (commandField.ShowInsertButton && !commandField.ShowDeleteButton && !commandField.ShowSelectButton && !commandField.ShowEditButton)
				{
					return SR.GetString("DCFEditor_Node_Insert");
				}
				return SR.GetString("DCFEditor_Node_Command");
			}

			// Token: 0x06002774 RID: 10100 RVA: 0x000F28C8 File Offset: 0x000F0AC8
			public override TemplateField GetTemplateField(DataBoundControl dataBoundControl)
			{
				TemplateField templateField = base.GetTemplateField(dataBoundControl);
				templateField.ItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(0));
				templateField.EditItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(1));
				if (dataBoundControl is DetailsView)
				{
					templateField.InsertItemTemplate = base.GetTemplate(dataBoundControl, this.GetTemplateContent(2));
				}
				return templateField;
			}

			// Token: 0x06002775 RID: 10101 RVA: 0x000F2924 File Offset: 0x000F0B24
			private string GetTemplateContent(int editMode)
			{
				StringBuilder stringBuilder = new StringBuilder();
				CommandField commandField = (CommandField)base.RuntimeField;
				Type typeFromHandle = typeof(System.Web.UI.WebControls.Button);
				int num = 1;
				if (commandField.ButtonType == ButtonType.Link)
				{
					typeFromHandle = typeof(LinkButton);
				}
				else if (commandField.ButtonType == ButtonType.Image)
				{
					typeFromHandle = typeof(ImageButton);
				}
				switch (editMode)
				{
				case 0:
				{
					bool flag = true;
					if (commandField.ShowEditButton)
					{
						string imageUrl = (commandField.ButtonType == ButtonType.Image) ? commandField.EditImageUrl : null;
						stringBuilder.Append(this.BuildButtonString(typeFromHandle, commandField.EditText, "Edit", imageUrl, false, 0, ref num));
						num++;
						flag = false;
					}
					if (commandField.ShowInsertButton)
					{
						if (!flag)
						{
							stringBuilder.Append("&nbsp;");
						}
						string imageUrl2 = (commandField.ButtonType == ButtonType.Image) ? commandField.NewImageUrl : null;
						stringBuilder.Append(this.BuildButtonString(typeFromHandle, commandField.NewText, "New", imageUrl2, false, 0, ref num));
						num++;
					}
					if (commandField.ShowSelectButton)
					{
						if (!flag)
						{
							stringBuilder.Append("&nbsp;");
						}
						string imageUrl3 = (commandField.ButtonType == ButtonType.Image) ? commandField.SelectImageUrl : null;
						stringBuilder.Append(this.BuildButtonString(typeFromHandle, commandField.SelectText, "Select", imageUrl3, false, 0, ref num));
						num++;
					}
					if (commandField.ShowDeleteButton)
					{
						if (!flag)
						{
							stringBuilder.Append("&nbsp;");
						}
						string imageUrl4 = (commandField.ButtonType == ButtonType.Image) ? commandField.DeleteImageUrl : null;
						stringBuilder.Append(this.BuildButtonString(typeFromHandle, commandField.DeleteText, "Delete", imageUrl4, false, 0, ref num));
						num++;
					}
					break;
				}
				case 1:
					if (commandField.ShowEditButton)
					{
						string imageUrl5 = (commandField.ButtonType == ButtonType.Image) ? commandField.UpdateImageUrl : null;
						stringBuilder.Append(this.BuildButtonString(typeFromHandle, commandField.UpdateText, "Update", imageUrl5, true, 1, ref num));
						num++;
						if (commandField.ShowCancelButton)
						{
							stringBuilder.Append("&nbsp;");
							string imageUrl6 = (commandField.ButtonType == ButtonType.Image) ? commandField.CancelImageUrl : null;
							stringBuilder.Append(this.BuildButtonString(typeFromHandle, commandField.CancelText, "Cancel", imageUrl6, false, 1, ref num));
							num++;
						}
					}
					break;
				case 2:
					if (commandField.ShowInsertButton)
					{
						string imageUrl7 = (commandField.ButtonType == ButtonType.Image) ? commandField.InsertImageUrl : null;
						stringBuilder.Append(this.BuildButtonString(typeFromHandle, commandField.InsertText, "Insert", imageUrl7, true, 2, ref num));
						num++;
						if (commandField.ShowCancelButton)
						{
							stringBuilder.Append("&nbsp;");
							string imageUrl8 = (commandField.ButtonType == ButtonType.Image) ? commandField.CancelImageUrl : null;
							stringBuilder.Append(this.BuildButtonString(typeFromHandle, commandField.CancelText, "Cancel", imageUrl8, false, 2, ref num));
							num++;
						}
					}
					break;
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06002776 RID: 10102 RVA: 0x000F2BF4 File Offset: 0x000F0DF4
			public void UpdateImageIndex()
			{
				CommandField commandField = (CommandField)base.RuntimeField;
				if (commandField.ShowEditButton && !commandField.ShowDeleteButton && !commandField.ShowSelectButton && !commandField.ShowInsertButton)
				{
					base.ImageIndex = 6;
					return;
				}
				if (commandField.ShowDeleteButton && !commandField.ShowEditButton && !commandField.ShowSelectButton && !commandField.ShowInsertButton)
				{
					base.ImageIndex = 7;
					return;
				}
				if (commandField.ShowSelectButton && !commandField.ShowDeleteButton && !commandField.ShowEditButton && !commandField.ShowInsertButton)
				{
					base.ImageIndex = 5;
					return;
				}
				if (commandField.ShowInsertButton && !commandField.ShowDeleteButton && !commandField.ShowSelectButton && !commandField.ShowEditButton)
				{
					base.ImageIndex = 11;
					return;
				}
				base.ImageIndex = 12;
			}
		}

		// Token: 0x020003F4 RID: 1012
		private class TreeViewWithEnter : System.Windows.Forms.TreeView
		{
			// Token: 0x06002777 RID: 10103 RVA: 0x000F2CB6 File Offset: 0x000F0EB6
			protected override bool IsInputKey(Keys keyCode)
			{
				return keyCode == Keys.Return || base.IsInputKey(keyCode);
			}
		}

		// Token: 0x020003F5 RID: 1013
		private class ListViewWithEnter : ListView
		{
			// Token: 0x06002779 RID: 10105 RVA: 0x000F2CCE File Offset: 0x000F0ECE
			protected override bool IsInputKey(Keys keyCode)
			{
				return keyCode == Keys.Return || base.IsInputKey(keyCode);
			}
		}

		// Token: 0x020003F6 RID: 1014
		private class CustomFieldItem : DataControlFieldsEditor.FieldItem
		{
			// Token: 0x0600277B RID: 10107 RVA: 0x000F2CE6 File Offset: 0x000F0EE6
			public CustomFieldItem(DataControlFieldsEditor fieldsEditor, DataControlField runtimeField) : base(fieldsEditor, runtimeField, 3)
			{
			}
		}

		// Token: 0x020003F7 RID: 1015
		private sealed class DataControlFieldDesignerItem : DataControlFieldsEditor.FieldItem
		{
			// Token: 0x0600277C RID: 10108 RVA: 0x000F2CF1 File Offset: 0x000F0EF1
			public DataControlFieldDesignerItem(DataControlFieldDesigner fieldDesigner, DataControlField runtimeField) : base(null, runtimeField, 15)
			{
				this._fieldDesigner = fieldDesigner;
				base.Text = this.GetDefaultNodeText();
			}

			// Token: 0x0600277D RID: 10109 RVA: 0x000F2D10 File Offset: 0x000F0F10
			protected override string GetDefaultNodeText()
			{
				if (this._fieldDesigner != null)
				{
					return this._fieldDesigner.GetNodeText(base.RuntimeField);
				}
				return base.GetDefaultNodeText();
			}

			// Token: 0x0600277E RID: 10110 RVA: 0x000F2D32 File Offset: 0x000F0F32
			public override TemplateField GetTemplateField(DataBoundControl dataBoundControl)
			{
				if (this._fieldDesigner != null)
				{
					return this._fieldDesigner.CreateTemplateField(base.RuntimeField, dataBoundControl);
				}
				return base.GetTemplateField(dataBoundControl);
			}

			// Token: 0x04001C3D RID: 7229
			private DataControlFieldDesigner _fieldDesigner;
		}

		// Token: 0x020003F8 RID: 1016
		private sealed class DataControlFieldDesignerNode : DataControlFieldsEditor.AvailableFieldNode
		{
			// Token: 0x0600277F RID: 10111 RVA: 0x000F2D56 File Offset: 0x000F0F56
			public DataControlFieldDesignerNode(DataControlFieldDesigner fieldDesigner) : base(fieldDesigner.DefaultNodeText, 15)
			{
				this._fieldDesigner = fieldDesigner;
			}

			// Token: 0x06002780 RID: 10112 RVA: 0x000F2D6D File Offset: 0x000F0F6D
			public DataControlFieldDesignerNode(DataControlFieldDesigner fieldDesigner, IDataSourceFieldSchema fieldSchema) : base((fieldSchema == null) ? fieldDesigner.DefaultNodeText : fieldSchema.Name, 15)
			{
				this._fieldSchema = fieldSchema;
				this._fieldDesigner = fieldDesigner;
			}

			// Token: 0x06002781 RID: 10113 RVA: 0x000F2D98 File Offset: 0x000F0F98
			public override DataControlFieldsEditor.FieldItem CreateField()
			{
				DataControlField runtimeField = (this._fieldSchema == null) ? this._fieldDesigner.CreateField() : this._fieldDesigner.CreateField(this._fieldSchema);
				DataControlFieldsEditor.FieldItem fieldItem = new DataControlFieldsEditor.DataControlFieldDesignerItem(this._fieldDesigner, runtimeField);
				fieldItem.LoadFieldInfo();
				return fieldItem;
			}

			// Token: 0x04001C3E RID: 7230
			private IDataSourceFieldSchema _fieldSchema;

			// Token: 0x04001C3F RID: 7231
			private DataControlFieldDesigner _fieldDesigner;
		}
	}
}
