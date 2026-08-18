using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002BB RID: 699
	internal partial class DataGridViewColumnCollectionDialog : Form
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x000A5F34 File Offset: 0x000A4134
		internal DataGridViewColumnCollectionDialog(IServiceProvider provider)
		{
			this.serviceProvider = provider;
			this.InitializeComponent();
			if (DpiHelper.IsScalingRequired)
			{
				DpiHelper.ScaleButtonImageLogicalToDevice(this.moveUp);
				DpiHelper.ScaleButtonImageLogicalToDevice(this.moveDown);
			}
			this.dataGridViewPrivateCopy = new DataGridView();
			this.columnsPrivateCopy = this.dataGridViewPrivateCopy.Columns;
			this.columnsPrivateCopy.CollectionChanged += this.columnsPrivateCopy_CollectionChanged;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x000A5FA4 File Offset: 0x000A41A4
		private Bitmap SelectedColumnsItemBitmap
		{
			get
			{
				if (DataGridViewColumnCollectionDialog.selectedColumnsItemBitmap == null)
				{
					DataGridViewColumnCollectionDialog.selectedColumnsItemBitmap = new Bitmap(BitmapSelector.GetResourceStream(typeof(DataGridViewColumnCollectionDialog), "DataGridViewColumnsDialog.selectedColumns.bmp"));
					DataGridViewColumnCollectionDialog.selectedColumnsItemBitmap.MakeTransparent(Color.Red);
				}
				return DataGridViewColumnCollectionDialog.selectedColumnsItemBitmap;
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000A5FE0 File Offset: 0x000A41E0
		private void columnsPrivateCopy_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (this.columnCollectionChanging)
			{
				return;
			}
			this.PopulateSelectedColumns();
			if (e.Action == CollectionChangeAction.Add)
			{
				this.selectedColumns.SelectedIndex = this.columnsPrivateCopy.IndexOf((DataGridViewColumn)e.Element);
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = this.selectedColumns.SelectedItem as DataGridViewColumnCollectionDialog.ListBoxItem;
				this.userAddedColumns[listBoxItem.DataGridViewColumn] = true;
				this.columnsNames[listBoxItem.DataGridViewColumn] = listBoxItem.DataGridViewColumn.Name;
			}
			this.formIsDirty = true;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000A6074 File Offset: 0x000A4274
		private void ColumnTypeChanged(DataGridViewColumnCollectionDialog.ListBoxItem item, Type newType)
		{
			DataGridViewColumn dataGridViewColumn = item.DataGridViewColumn;
			DataGridViewColumn dataGridViewColumn2 = Activator.CreateInstance(newType) as DataGridViewColumn;
			ITypeResolutionService tr = this.liveDataGridView.Site.GetService(DataGridViewColumnCollectionDialog.iTypeResolutionServiceType) as ITypeResolutionService;
			ComponentDesigner componentDesignerForType = DataGridViewAddColumnDialog.GetComponentDesignerForType(tr, newType);
			DataGridViewColumnCollectionDialog.CopyDataGridViewColumnProperties(dataGridViewColumn, dataGridViewColumn2);
			DataGridViewColumnCollectionDialog.CopyDataGridViewColumnState(dataGridViewColumn, dataGridViewColumn2);
			this.columnCollectionChanging = true;
			int selectedIndex = this.selectedColumns.SelectedIndex;
			this.selectedColumns.Focus();
			base.ActiveControl = this.selectedColumns;
			try
			{
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = (DataGridViewColumnCollectionDialog.ListBoxItem)this.selectedColumns.SelectedItem;
				bool flag = (bool)this.userAddedColumns[listBoxItem.DataGridViewColumn];
				string value = string.Empty;
				if (this.columnsNames.Contains(listBoxItem.DataGridViewColumn))
				{
					value = (string)this.columnsNames[listBoxItem.DataGridViewColumn];
					this.columnsNames.Remove(listBoxItem.DataGridViewColumn);
				}
				if (this.userAddedColumns.Contains(listBoxItem.DataGridViewColumn))
				{
					this.userAddedColumns.Remove(listBoxItem.DataGridViewColumn);
				}
				if (listBoxItem.DataGridViewColumnDesigner != null)
				{
					TypeDescriptor.RemoveAssociation(listBoxItem.DataGridViewColumn, listBoxItem.DataGridViewColumnDesigner);
				}
				this.selectedColumns.Items.RemoveAt(selectedIndex);
				this.selectedColumns.Items.Insert(selectedIndex, new DataGridViewColumnCollectionDialog.ListBoxItem(dataGridViewColumn2, this, componentDesignerForType));
				this.columnsPrivateCopy.RemoveAt(selectedIndex);
				dataGridViewColumn2.DisplayIndex = -1;
				this.columnsPrivateCopy.Insert(selectedIndex, dataGridViewColumn2);
				if (!string.IsNullOrEmpty(value))
				{
					this.columnsNames[dataGridViewColumn2] = value;
				}
				this.userAddedColumns[dataGridViewColumn2] = flag;
				this.FixColumnCollectionDisplayIndices();
				this.selectedColumns.SelectedIndex = selectedIndex;
				this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
			}
			finally
			{
				this.columnCollectionChanging = false;
			}
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000A626C File Offset: 0x000A446C
		private void CommitChanges()
		{
			if (this.formIsDirty)
			{
				try
				{
					IComponentChangeService componentChangeService = (IComponentChangeService)this.liveDataGridView.Site.GetService(DataGridViewColumnCollectionDialog.iComponentChangeServiceType);
					PropertyDescriptor member = TypeDescriptor.GetProperties(this.liveDataGridView)["Columns"];
					IContainer container = (this.liveDataGridView.Site != null) ? this.liveDataGridView.Site.Container : null;
					DataGridViewColumn[] array = new DataGridViewColumn[this.liveDataGridView.Columns.Count];
					this.liveDataGridView.Columns.CopyTo(array, 0);
					componentChangeService.OnComponentChanging(this.liveDataGridView, member);
					this.liveDataGridView.Columns.Clear();
					componentChangeService.OnComponentChanged(this.liveDataGridView, member, null, null);
					if (container != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							container.Remove(array[i]);
						}
					}
					DataGridViewColumn[] array2 = new DataGridViewColumn[this.columnsPrivateCopy.Count];
					bool[] array3 = new bool[this.columnsPrivateCopy.Count];
					string[] array4 = new string[this.columnsPrivateCopy.Count];
					for (int j = 0; j < this.columnsPrivateCopy.Count; j++)
					{
						DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.columnsPrivateCopy[j].Clone();
						dataGridViewColumn.ContextMenuStrip = this.columnsPrivateCopy[j].ContextMenuStrip;
						array2[j] = dataGridViewColumn;
						array3[j] = (bool)this.userAddedColumns[this.columnsPrivateCopy[j]];
						array4[j] = (string)this.columnsNames[this.columnsPrivateCopy[j]];
					}
					if (container != null)
					{
						for (int k = 0; k < array2.Length; k++)
						{
							if (!string.IsNullOrEmpty(array4[k]) && DataGridViewColumnCollectionDialog.ValidateName(container, array4[k], array2[k]))
							{
								container.Add(array2[k], array4[k]);
							}
							else
							{
								container.Add(array2[k]);
							}
						}
					}
					componentChangeService.OnComponentChanging(this.liveDataGridView, member);
					for (int l = 0; l < array2.Length; l++)
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(array2[l])["DisplayIndex"];
						if (propertyDescriptor != null)
						{
							propertyDescriptor.SetValue(array2[l], -1);
						}
						this.liveDataGridView.Columns.Add(array2[l]);
					}
					componentChangeService.OnComponentChanged(this.liveDataGridView, member, null, null);
					for (int m = 0; m < array3.Length; m++)
					{
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(array2[m])["UserAddedColumn"];
						if (propertyDescriptor2 != null)
						{
							propertyDescriptor2.SetValue(array2[m], array3[m]);
						}
					}
				}
				catch (InvalidOperationException ex)
				{
					IUIService uiService = (IUIService)this.liveDataGridView.Site.GetService(typeof(IUIService));
					DataGridViewDesigner.ShowErrorDialog(uiService, ex, this.liveDataGridView);
					base.DialogResult = DialogResult.Cancel;
				}
			}
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000A657C File Offset: 0x000A477C
		private void componentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (e.Component is DataGridViewColumnCollectionDialog.ListBoxItem && this.selectedColumns.Items.Contains(e.Component))
			{
				this.formIsDirty = true;
			}
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x000A65AC File Offset: 0x000A47AC
		private static void CopyDataGridViewColumnProperties(DataGridViewColumn srcColumn, DataGridViewColumn destColumn)
		{
			destColumn.AutoSizeMode = srcColumn.AutoSizeMode;
			destColumn.ContextMenuStrip = srcColumn.ContextMenuStrip;
			destColumn.DataPropertyName = srcColumn.DataPropertyName;
			if (srcColumn.HasDefaultCellStyle)
			{
				DataGridViewColumnCollectionDialog.CopyDefaultCellStyle(srcColumn, destColumn);
			}
			destColumn.DividerWidth = srcColumn.DividerWidth;
			destColumn.HeaderText = srcColumn.HeaderText;
			destColumn.MinimumWidth = srcColumn.MinimumWidth;
			destColumn.Name = srcColumn.Name;
			destColumn.SortMode = srcColumn.SortMode;
			destColumn.Tag = srcColumn.Tag;
			destColumn.ToolTipText = srcColumn.ToolTipText;
			destColumn.Width = srcColumn.Width;
			destColumn.FillWeight = srcColumn.FillWeight;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000A6658 File Offset: 0x000A4858
		private static void CopyDataGridViewColumnState(DataGridViewColumn srcColumn, DataGridViewColumn destColumn)
		{
			destColumn.Frozen = srcColumn.Frozen;
			destColumn.Visible = srcColumn.Visible;
			destColumn.ReadOnly = srcColumn.ReadOnly;
			destColumn.Resizable = srcColumn.Resizable;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x000A668C File Offset: 0x000A488C
		private static void CopyDefaultCellStyle(DataGridViewColumn srcColumn, DataGridViewColumn destColumn)
		{
			Type type = srcColumn.GetType();
			Type type2 = destColumn.GetType();
			if (type.IsAssignableFrom(type2) || type2.IsAssignableFrom(type))
			{
				destColumn.DefaultCellStyle = srcColumn.DefaultCellStyle;
				return;
			}
			DataGridViewColumn dataGridViewColumn = null;
			try
			{
				dataGridViewColumn = (Activator.CreateInstance(type) as DataGridViewColumn);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
				dataGridViewColumn = null;
			}
			if (dataGridViewColumn == null || dataGridViewColumn.DefaultCellStyle.Alignment != srcColumn.DefaultCellStyle.Alignment)
			{
				destColumn.DefaultCellStyle.Alignment = srcColumn.DefaultCellStyle.Alignment;
			}
			if (dataGridViewColumn == null || !dataGridViewColumn.DefaultCellStyle.BackColor.Equals(srcColumn.DefaultCellStyle.BackColor))
			{
				destColumn.DefaultCellStyle.BackColor = srcColumn.DefaultCellStyle.BackColor;
			}
			if (dataGridViewColumn != null && srcColumn.DefaultCellStyle.Font != null && !srcColumn.DefaultCellStyle.Font.Equals(dataGridViewColumn.DefaultCellStyle.Font))
			{
				destColumn.DefaultCellStyle.Font = srcColumn.DefaultCellStyle.Font;
			}
			if (dataGridViewColumn == null || !dataGridViewColumn.DefaultCellStyle.ForeColor.Equals(srcColumn.DefaultCellStyle.ForeColor))
			{
				destColumn.DefaultCellStyle.ForeColor = srcColumn.DefaultCellStyle.ForeColor;
			}
			if (dataGridViewColumn == null || !dataGridViewColumn.DefaultCellStyle.Format.Equals(srcColumn.DefaultCellStyle.Format))
			{
				destColumn.DefaultCellStyle.Format = srcColumn.DefaultCellStyle.Format;
			}
			if (dataGridViewColumn == null || dataGridViewColumn.DefaultCellStyle.Padding != srcColumn.DefaultCellStyle.Padding)
			{
				destColumn.DefaultCellStyle.Padding = srcColumn.DefaultCellStyle.Padding;
			}
			if (dataGridViewColumn == null || !dataGridViewColumn.DefaultCellStyle.SelectionBackColor.Equals(srcColumn.DefaultCellStyle.SelectionBackColor))
			{
				destColumn.DefaultCellStyle.SelectionBackColor = srcColumn.DefaultCellStyle.SelectionBackColor;
			}
			if (dataGridViewColumn == null || !dataGridViewColumn.DefaultCellStyle.SelectionForeColor.Equals(srcColumn.DefaultCellStyle.SelectionForeColor))
			{
				destColumn.DefaultCellStyle.SelectionForeColor = srcColumn.DefaultCellStyle.SelectionForeColor;
			}
			if (dataGridViewColumn == null || dataGridViewColumn.DefaultCellStyle.WrapMode != srcColumn.DefaultCellStyle.WrapMode)
			{
				destColumn.DefaultCellStyle.WrapMode = srcColumn.DefaultCellStyle.WrapMode;
			}
			if (!srcColumn.DefaultCellStyle.IsNullValueDefault)
			{
				object nullValue = srcColumn.DefaultCellStyle.NullValue;
				object nullValue2 = destColumn.DefaultCellStyle.NullValue;
				if (nullValue != null && nullValue2 != null && nullValue.GetType() == nullValue2.GetType())
				{
					destColumn.DefaultCellStyle.NullValue = nullValue;
				}
			}
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000A698C File Offset: 0x000A4B8C
		private void FixColumnCollectionDisplayIndices()
		{
			for (int i = 0; i < this.columnsPrivateCopy.Count; i++)
			{
				this.columnsPrivateCopy[i].DisplayIndex = i;
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000A69C1 File Offset: 0x000A4BC1
		private void HookComponentChangedEventHandler(IComponentChangeService componentChangeService)
		{
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanged += this.componentChanged;
			}
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000A726C File Offset: 0x000A546C
		private static bool IsColumnAddedByUser(DataGridViewColumn col)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(col)["UserAddedColumn"];
			return propertyDescriptor != null && (bool)propertyDescriptor.GetValue(col);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000A729B File Offset: 0x000A549B
		private void okButton_Click(object sender, EventArgs e)
		{
			this.CommitChanges();
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000A72A4 File Offset: 0x000A54A4
		private void moveDown_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.selectedColumns.SelectedIndex;
			this.columnCollectionChanging = true;
			try
			{
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = (DataGridViewColumnCollectionDialog.ListBoxItem)this.selectedColumns.SelectedItem;
				this.selectedColumns.Items.RemoveAt(selectedIndex);
				this.selectedColumns.Items.Insert(selectedIndex + 1, listBoxItem);
				this.columnsPrivateCopy.RemoveAt(selectedIndex);
				if (listBoxItem.DataGridViewColumn.Frozen)
				{
					this.columnsPrivateCopy[selectedIndex].Frozen = true;
				}
				listBoxItem.DataGridViewColumn.DisplayIndex = -1;
				this.columnsPrivateCopy.Insert(selectedIndex + 1, listBoxItem.DataGridViewColumn);
				this.FixColumnCollectionDisplayIndices();
			}
			finally
			{
				this.columnCollectionChanging = false;
			}
			this.formIsDirty = true;
			this.selectedColumns.SelectedIndex = selectedIndex + 1;
			this.moveUp.Enabled = (this.selectedColumns.SelectedIndex > 0);
			this.moveDown.Enabled = (this.selectedColumns.SelectedIndex < this.selectedColumns.Items.Count - 1);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000A73C0 File Offset: 0x000A55C0
		private void moveUp_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.selectedColumns.SelectedIndex;
			this.columnCollectionChanging = true;
			try
			{
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = (DataGridViewColumnCollectionDialog.ListBoxItem)this.selectedColumns.Items[selectedIndex - 1];
				this.selectedColumns.Items.RemoveAt(selectedIndex - 1);
				this.selectedColumns.Items.Insert(selectedIndex, listBoxItem);
				this.columnsPrivateCopy.RemoveAt(selectedIndex - 1);
				if (listBoxItem.DataGridViewColumn.Frozen && !this.columnsPrivateCopy[selectedIndex - 1].Frozen)
				{
					listBoxItem.DataGridViewColumn.Frozen = false;
				}
				listBoxItem.DataGridViewColumn.DisplayIndex = -1;
				this.columnsPrivateCopy.Insert(selectedIndex, listBoxItem.DataGridViewColumn);
				this.FixColumnCollectionDisplayIndices();
			}
			finally
			{
				this.columnCollectionChanging = false;
			}
			this.formIsDirty = true;
			this.selectedColumns.SelectedIndex = selectedIndex - 1;
			this.moveUp.Enabled = (this.selectedColumns.SelectedIndex > 0);
			this.moveDown.Enabled = (this.selectedColumns.SelectedIndex < this.selectedColumns.Items.Count - 1);
			if (this.selectedColumns.SelectedIndex != -1 && this.selectedColumns.TopIndex > this.selectedColumns.SelectedIndex)
			{
				this.selectedColumns.TopIndex = this.selectedColumns.SelectedIndex;
			}
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000A752C File Offset: 0x000A572C
		private void DataGridViewColumnCollectionDialog_Closed(object sender, EventArgs e)
		{
			for (int i = 0; i < this.selectedColumns.Items.Count; i++)
			{
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = this.selectedColumns.Items[i] as DataGridViewColumnCollectionDialog.ListBoxItem;
				if (listBoxItem.DataGridViewColumnDesigner != null)
				{
					TypeDescriptor.RemoveAssociation(listBoxItem.DataGridViewColumn, listBoxItem.DataGridViewColumnDesigner);
				}
			}
			this.columnsNames = null;
			this.userAddedColumns = null;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000A7592 File Offset: 0x000A5792
		private void DataGridViewColumnCollectionDialog_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.DataGridViewColumnCollectionDialog_HelpRequestHandled();
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000A75A1 File Offset: 0x000A57A1
		private void DataGridViewColumnCollectionDialog_HelpRequested(object sender, HelpEventArgs e)
		{
			this.DataGridViewColumnCollectionDialog_HelpRequestHandled();
			e.Handled = true;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x000A75B0 File Offset: 0x000A57B0
		private void DataGridViewColumnCollectionDialog_HelpRequestHandled()
		{
			IHelpService helpService = this.liveDataGridView.Site.GetService(DataGridViewColumnCollectionDialog.iHelpServiceType) as IHelpService;
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword("vs.DataGridViewColumnCollectionDialog");
			}
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x000A75E8 File Offset: 0x000A57E8
		private void DataGridViewColumnCollectionDialog_Load(object sender, EventArgs e)
		{
			Font font = Control.DefaultFont;
			IUIService iuiservice = (IUIService)this.liveDataGridView.Site.GetService(DataGridViewColumnCollectionDialog.iUIServiceType);
			if (iuiservice != null)
			{
				font = (Font)iuiservice.Styles["DialogFont"];
			}
			this.Font = font;
			this.selectedColumns.SelectedIndex = Math.Min(0, this.selectedColumns.Items.Count - 1);
			this.moveUp.Enabled = (this.selectedColumns.SelectedIndex > 0);
			this.moveDown.Enabled = (this.selectedColumns.SelectedIndex < this.selectedColumns.Items.Count - 1);
			this.deleteButton.Enabled = (this.selectedColumns.Items.Count > 0 && this.selectedColumns.SelectedIndex != -1);
			this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
			this.selectedColumns.ItemHeight = this.Font.Height + 4;
			base.ActiveControl = this.selectedColumns;
			this.SetSelectedColumnsHorizontalExtent();
			this.selectedColumns.Focus();
			this.formIsDirty = false;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000A7724 File Offset: 0x000A5924
		private void deleteButton_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.selectedColumns.SelectedIndex;
			this.columnsNames.Remove(this.columnsPrivateCopy[selectedIndex]);
			this.userAddedColumns.Remove(this.columnsPrivateCopy[selectedIndex]);
			this.columnsPrivateCopy.RemoveAt(selectedIndex);
			this.selectedColumns.SelectedIndex = Math.Min(this.selectedColumns.Items.Count - 1, selectedIndex);
			this.moveUp.Enabled = (this.selectedColumns.SelectedIndex > 0);
			this.moveDown.Enabled = (this.selectedColumns.SelectedIndex < this.selectedColumns.Items.Count - 1);
			this.deleteButton.Enabled = (this.selectedColumns.Items.Count > 0 && this.selectedColumns.SelectedIndex != -1);
			this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x000A7828 File Offset: 0x000A5A28
		private void addButton_Click(object sender, EventArgs e)
		{
			int insertAtPosition;
			if (this.selectedColumns.SelectedIndex == -1)
			{
				insertAtPosition = this.selectedColumns.Items.Count;
			}
			else
			{
				insertAtPosition = this.selectedColumns.SelectedIndex + 1;
			}
			if (this.addColumnDialog == null)
			{
				this.addColumnDialog = DpiHelper.CreateInstanceInSystemAwareContext<DataGridViewAddColumnDialog>(() => new DataGridViewAddColumnDialog(this.columnsPrivateCopy, this.liveDataGridView));
				this.addColumnDialog.StartPosition = FormStartPosition.CenterParent;
			}
			this.addColumnDialog.Start(insertAtPosition, false);
			this.addColumnDialog.ShowDialog(this);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x000A78AC File Offset: 0x000A5AAC
		private void PopulateSelectedColumns()
		{
			int selectedIndex = this.selectedColumns.SelectedIndex;
			for (int i = 0; i < this.selectedColumns.Items.Count; i++)
			{
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = this.selectedColumns.Items[i] as DataGridViewColumnCollectionDialog.ListBoxItem;
				if (listBoxItem.DataGridViewColumnDesigner != null)
				{
					TypeDescriptor.RemoveAssociation(listBoxItem.DataGridViewColumn, listBoxItem.DataGridViewColumnDesigner);
				}
			}
			this.selectedColumns.Items.Clear();
			ITypeResolutionService tr = (ITypeResolutionService)this.liveDataGridView.Site.GetService(DataGridViewColumnCollectionDialog.iTypeResolutionServiceType);
			for (int j = 0; j < this.columnsPrivateCopy.Count; j++)
			{
				ComponentDesigner componentDesignerForType = DataGridViewAddColumnDialog.GetComponentDesignerForType(tr, this.columnsPrivateCopy[j].GetType());
				this.selectedColumns.Items.Add(new DataGridViewColumnCollectionDialog.ListBoxItem(this.columnsPrivateCopy[j], this, componentDesignerForType));
			}
			this.selectedColumns.SelectedIndex = Math.Min(selectedIndex, this.selectedColumns.Items.Count - 1);
			this.SetSelectedColumnsHorizontalExtent();
			if (this.selectedColumns.Items.Count == 0)
			{
				this.propertyGridLabel.Text = SR.GetString("DataGridViewProperties");
			}
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x000A79E8 File Offset: 0x000A5BE8
		private void propertyGrid1_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			if (!this.columnCollectionChanging)
			{
				this.formIsDirty = true;
				if (e.ChangedItem.PropertyDescriptor.Name.Equals("HeaderText"))
				{
					int selectedIndex = this.selectedColumns.SelectedIndex;
					Rectangle rc = new Rectangle(0, selectedIndex * this.selectedColumns.ItemHeight, this.selectedColumns.Width, this.selectedColumns.ItemHeight);
					this.columnCollectionChanging = true;
					try
					{
						this.selectedColumns.Items[selectedIndex] = this.selectedColumns.Items[selectedIndex];
					}
					finally
					{
						this.columnCollectionChanging = false;
					}
					this.selectedColumns.Invalidate(rc);
					this.SetSelectedColumnsHorizontalExtent();
					return;
				}
				if (e.ChangedItem.PropertyDescriptor.Name.Equals("DataPropertyName"))
				{
					DataGridViewColumn dataGridViewColumn = ((DataGridViewColumnCollectionDialog.ListBoxItem)this.selectedColumns.SelectedItem).DataGridViewColumn;
					if (string.IsNullOrEmpty(dataGridViewColumn.DataPropertyName))
					{
						this.propertyGridLabel.Text = SR.GetString("DataGridViewUnboundColumnProperties");
						return;
					}
					this.propertyGridLabel.Text = SR.GetString("DataGridViewBoundColumnProperties");
					return;
				}
				else if (e.ChangedItem.PropertyDescriptor.Name.Equals("Name"))
				{
					DataGridViewColumn dataGridViewColumn2 = ((DataGridViewColumnCollectionDialog.ListBoxItem)this.selectedColumns.SelectedItem).DataGridViewColumn;
					this.columnsNames[dataGridViewColumn2] = dataGridViewColumn2.Name;
				}
			}
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x000A7B60 File Offset: 0x000A5D60
		private void selectedColumns_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0)
			{
				return;
			}
			DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = this.selectedColumns.Items[e.Index] as DataGridViewColumnCollectionDialog.ListBoxItem;
			e.Graphics.DrawImage(listBoxItem.ToolboxBitmap, e.Bounds.X + 2, e.Bounds.Y + 2, listBoxItem.ToolboxBitmap.Width, listBoxItem.ToolboxBitmap.Height);
			Rectangle bounds = e.Bounds;
			bounds.Width -= listBoxItem.ToolboxBitmap.Width + 4;
			bounds.X += listBoxItem.ToolboxBitmap.Width + 4;
			bounds.Y += 2;
			bounds.Height -= 4;
			Brush brush = new SolidBrush(e.BackColor);
			Brush brush2 = new SolidBrush(e.ForeColor);
			Brush brush3 = new SolidBrush(this.selectedColumns.BackColor);
			string text = ((DataGridViewColumnCollectionDialog.ListBoxItem)this.selectedColumns.Items[e.Index]).ToString();
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				int width = Size.Ceiling(e.Graphics.MeasureString(text, e.Font, new SizeF((float)bounds.Width, (float)bounds.Height))).Width;
				Rectangle rectangle = new Rectangle(bounds.X, e.Bounds.Y + 1, width + 3, e.Bounds.Height - 2);
				e.Graphics.FillRectangle(brush, rectangle);
				rectangle.Inflate(-1, -1);
				e.Graphics.DrawString(text, e.Font, brush2, rectangle);
				rectangle.Inflate(1, 1);
				if (this.selectedColumns.Focused)
				{
					ControlPaint.DrawFocusRectangle(e.Graphics, rectangle, e.ForeColor, e.BackColor);
				}
				e.Graphics.FillRectangle(brush3, new Rectangle(rectangle.Right + 1, e.Bounds.Y, e.Bounds.Width - rectangle.Right - 1, e.Bounds.Height));
			}
			else
			{
				e.Graphics.FillRectangle(brush3, new Rectangle(bounds.X, e.Bounds.Y, e.Bounds.Width - bounds.X, e.Bounds.Height));
				e.Graphics.DrawString(text, e.Font, brush2, bounds);
			}
			brush.Dispose();
			brush3.Dispose();
			brush2.Dispose();
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x000A7E2D File Offset: 0x000A602D
		private void selectedColumns_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Modifiers == Keys.None && e.KeyCode == Keys.F4)
			{
				this.propertyGrid1.Focus();
				e.Handled = true;
			}
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x000A7E54 File Offset: 0x000A6054
		private void selectedColumns_KeyPress(object sender, KeyPressEventArgs e)
		{
			Keys modifierKeys = Control.ModifierKeys;
			if ((modifierKeys & Keys.Control) != Keys.None)
			{
				e.Handled = true;
			}
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x000A7E78 File Offset: 0x000A6078
		private void selectedColumns_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.columnCollectionChanging)
			{
				return;
			}
			this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
			this.moveDown.Enabled = (this.selectedColumns.Items.Count > 0 && this.selectedColumns.SelectedIndex != this.selectedColumns.Items.Count - 1);
			this.moveUp.Enabled = (this.selectedColumns.Items.Count > 0 && this.selectedColumns.SelectedIndex > 0);
			this.deleteButton.Enabled = (this.selectedColumns.Items.Count > 0 && this.selectedColumns.SelectedIndex != -1);
			if (this.selectedColumns.SelectedItem == null)
			{
				this.propertyGridLabel.Text = SR.GetString("DataGridViewProperties");
				return;
			}
			DataGridViewColumn dataGridViewColumn = ((DataGridViewColumnCollectionDialog.ListBoxItem)this.selectedColumns.SelectedItem).DataGridViewColumn;
			if (string.IsNullOrEmpty(dataGridViewColumn.DataPropertyName))
			{
				this.propertyGridLabel.Text = SR.GetString("DataGridViewUnboundColumnProperties");
				return;
			}
			this.propertyGridLabel.Text = SR.GetString("DataGridViewBoundColumnProperties");
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000A7FBC File Offset: 0x000A61BC
		internal void SetLiveDataGridView(DataGridView dataGridView)
		{
			IComponentChangeService componentChangeService = null;
			if (dataGridView.Site != null)
			{
				componentChangeService = (IComponentChangeService)dataGridView.Site.GetService(DataGridViewColumnCollectionDialog.iComponentChangeServiceType);
			}
			if (componentChangeService != this.compChangeService)
			{
				this.UnhookComponentChangedEventHandler(this.compChangeService);
				this.compChangeService = componentChangeService;
				this.HookComponentChangedEventHandler(this.compChangeService);
			}
			this.liveDataGridView = dataGridView;
			this.dataGridViewPrivateCopy.Site = dataGridView.Site;
			this.dataGridViewPrivateCopy.AutoSizeColumnsMode = dataGridView.AutoSizeColumnsMode;
			this.dataGridViewPrivateCopy.DataSource = dataGridView.DataSource;
			this.dataGridViewPrivateCopy.DataMember = dataGridView.DataMember;
			this.columnsNames = new Hashtable(this.columnsPrivateCopy.Count);
			this.columnsPrivateCopy.Clear();
			this.userAddedColumns = new Hashtable(this.liveDataGridView.Columns.Count);
			this.columnCollectionChanging = true;
			try
			{
				for (int i = 0; i < this.liveDataGridView.Columns.Count; i++)
				{
					DataGridViewColumn dataGridViewColumn = this.liveDataGridView.Columns[i];
					DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)dataGridViewColumn.Clone();
					dataGridViewColumn2.ContextMenuStrip = this.liveDataGridView.Columns[i].ContextMenuStrip;
					dataGridViewColumn2.DisplayIndex = -1;
					this.columnsPrivateCopy.Add(dataGridViewColumn2);
					if (dataGridViewColumn.Site != null)
					{
						this.columnsNames[dataGridViewColumn2] = dataGridViewColumn.Site.Name;
					}
					this.userAddedColumns[dataGridViewColumn2] = DataGridViewColumnCollectionDialog.IsColumnAddedByUser(this.liveDataGridView.Columns[i]);
				}
			}
			finally
			{
				this.columnCollectionChanging = false;
			}
			this.PopulateSelectedColumns();
			this.propertyGrid1.Site = new DataGridViewComponentPropertyGridSite(this.liveDataGridView.Site, this.liveDataGridView);
			this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000A81AC File Offset: 0x000A63AC
		private void SetSelectedColumnsHorizontalExtent()
		{
			int num = 0;
			for (int i = 0; i < this.selectedColumns.Items.Count; i++)
			{
				int width = TextRenderer.MeasureText(this.selectedColumns.Items[i].ToString(), this.selectedColumns.Font).Width;
				num = Math.Max(num, width);
			}
			this.selectedColumns.HorizontalExtent = this.SelectedColumnsItemBitmap.Width + 4 + num + 3;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x000A8229 File Offset: 0x000A6429
		private void UnhookComponentChangedEventHandler(IComponentChangeService componentChangeService)
		{
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanged -= this.componentChanged;
			}
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000A8240 File Offset: 0x000A6440
		private static bool ValidateName(IContainer container, string siteName, IComponent component)
		{
			ComponentCollection componentCollection = container.Components;
			if (componentCollection == null)
			{
				return true;
			}
			for (int i = 0; i < componentCollection.Count; i++)
			{
				IComponent component2 = componentCollection[i];
				if (component2 != null && component2.Site != null)
				{
					ISite site = component2.Site;
					if (site != null && site.Name != null && string.Equals(site.Name, siteName, StringComparison.OrdinalIgnoreCase) && site.Component != component)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04001696 RID: 5782
		private DataGridView liveDataGridView;

		// Token: 0x04001697 RID: 5783
		private IComponentChangeService compChangeService;

		// Token: 0x04001698 RID: 5784
		private DataGridView dataGridViewPrivateCopy;

		// Token: 0x04001699 RID: 5785
		private DataGridViewColumnCollection columnsPrivateCopy;

		// Token: 0x0400169A RID: 5786
		private Hashtable columnsNames;

		// Token: 0x0400169B RID: 5787
		private DataGridViewAddColumnDialog addColumnDialog;

		// Token: 0x0400169C RID: 5788
		private const int LISTBOXITEMHEIGHT = 17;

		// Token: 0x0400169D RID: 5789
		private const int OWNERDRAWHORIZONTALBUFFER = 3;

		// Token: 0x0400169E RID: 5790
		private const int OWNERDRAWVERTICALBUFFER = 4;

		// Token: 0x0400169F RID: 5791
		private const int OWNERDRAWITEMIMAGEBUFFER = 2;

		// Token: 0x040016A0 RID: 5792
		private static Bitmap selectedColumnsItemBitmap;

		// Token: 0x040016A1 RID: 5793
		private static ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x040016A2 RID: 5794
		private static Type iTypeResolutionServiceType = typeof(ITypeResolutionService);

		// Token: 0x040016A3 RID: 5795
		private static Type iTypeDiscoveryServiceType = typeof(ITypeDiscoveryService);

		// Token: 0x040016A4 RID: 5796
		private static Type iComponentChangeServiceType = typeof(IComponentChangeService);

		// Token: 0x040016A5 RID: 5797
		private static Type iHelpServiceType = typeof(IHelpService);

		// Token: 0x040016A6 RID: 5798
		private static Type iUIServiceType = typeof(IUIService);

		// Token: 0x040016A7 RID: 5799
		private static Type toolboxBitmapAttributeType = typeof(ToolboxBitmapAttribute);

		// Token: 0x040016A8 RID: 5800
		private bool columnCollectionChanging;

		// Token: 0x040016A9 RID: 5801
		private bool formIsDirty;

		// Token: 0x040016AC RID: 5804
		private Hashtable userAddedColumns;

		// Token: 0x02000551 RID: 1361
		internal class ListBoxItem : ICustomTypeDescriptor, IComponent, IDisposable
		{
			// Token: 0x06003138 RID: 12600 RVA: 0x0010CC60 File Offset: 0x0010AE60
			public ListBoxItem(DataGridViewColumn column, DataGridViewColumnCollectionDialog owner, ComponentDesigner compDesigner)
			{
				this.column = column;
				this.owner = owner;
				this.compDesigner = compDesigner;
				if (this.compDesigner != null)
				{
					this.compDesigner.Initialize(column);
					TypeDescriptor.CreateAssociation(this.column, this.compDesigner);
				}
				ToolboxBitmapAttribute toolboxBitmapAttribute = TypeDescriptor.GetAttributes(column)[DataGridViewColumnCollectionDialog.toolboxBitmapAttributeType] as ToolboxBitmapAttribute;
				if (toolboxBitmapAttribute != null)
				{
					this.toolboxBitmap = toolboxBitmapAttribute.GetImage(column, false);
				}
				else
				{
					this.toolboxBitmap = this.owner.SelectedColumnsItemBitmap;
				}
				DataGridViewColumnDesigner dataGridViewColumnDesigner = compDesigner as DataGridViewColumnDesigner;
				if (dataGridViewColumnDesigner != null)
				{
					dataGridViewColumnDesigner.LiveDataGridView = this.owner.liveDataGridView;
				}
			}

			// Token: 0x17000986 RID: 2438
			// (get) Token: 0x06003139 RID: 12601 RVA: 0x0010CD02 File Offset: 0x0010AF02
			public DataGridViewColumn DataGridViewColumn
			{
				get
				{
					return this.column;
				}
			}

			// Token: 0x17000987 RID: 2439
			// (get) Token: 0x0600313A RID: 12602 RVA: 0x0010CD0A File Offset: 0x0010AF0A
			public ComponentDesigner DataGridViewColumnDesigner
			{
				get
				{
					return this.compDesigner;
				}
			}

			// Token: 0x17000988 RID: 2440
			// (get) Token: 0x0600313B RID: 12603 RVA: 0x0010CD12 File Offset: 0x0010AF12
			public DataGridViewColumnCollectionDialog Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x17000989 RID: 2441
			// (get) Token: 0x0600313C RID: 12604 RVA: 0x0010CD1A File Offset: 0x0010AF1A
			public Image ToolboxBitmap
			{
				get
				{
					return this.toolboxBitmap;
				}
			}

			// Token: 0x0600313D RID: 12605 RVA: 0x0010CD22 File Offset: 0x0010AF22
			public override string ToString()
			{
				return this.column.HeaderText;
			}

			// Token: 0x0600313E RID: 12606 RVA: 0x0010CD2F File Offset: 0x0010AF2F
			AttributeCollection ICustomTypeDescriptor.GetAttributes()
			{
				return TypeDescriptor.GetAttributes(this.column);
			}

			// Token: 0x0600313F RID: 12607 RVA: 0x0010CD3C File Offset: 0x0010AF3C
			string ICustomTypeDescriptor.GetClassName()
			{
				return TypeDescriptor.GetClassName(this.column);
			}

			// Token: 0x06003140 RID: 12608 RVA: 0x0010CD49 File Offset: 0x0010AF49
			string ICustomTypeDescriptor.GetComponentName()
			{
				return TypeDescriptor.GetComponentName(this.column);
			}

			// Token: 0x06003141 RID: 12609 RVA: 0x0010CD56 File Offset: 0x0010AF56
			TypeConverter ICustomTypeDescriptor.GetConverter()
			{
				return TypeDescriptor.GetConverter(this.column);
			}

			// Token: 0x06003142 RID: 12610 RVA: 0x0010CD63 File Offset: 0x0010AF63
			EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
			{
				return TypeDescriptor.GetDefaultEvent(this.column);
			}

			// Token: 0x06003143 RID: 12611 RVA: 0x0010CD70 File Offset: 0x0010AF70
			PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
			{
				return TypeDescriptor.GetDefaultProperty(this.column);
			}

			// Token: 0x06003144 RID: 12612 RVA: 0x0010CD7D File Offset: 0x0010AF7D
			object ICustomTypeDescriptor.GetEditor(Type type)
			{
				return TypeDescriptor.GetEditor(this.column, type);
			}

			// Token: 0x06003145 RID: 12613 RVA: 0x0010CD8B File Offset: 0x0010AF8B
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
			{
				return TypeDescriptor.GetEvents(this.column);
			}

			// Token: 0x06003146 RID: 12614 RVA: 0x0010CD98 File Offset: 0x0010AF98
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
			{
				return TypeDescriptor.GetEvents(this.column, attrs);
			}

			// Token: 0x06003147 RID: 12615 RVA: 0x0010CDA6 File Offset: 0x0010AFA6
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
			{
				return ((ICustomTypeDescriptor)this).GetProperties(null);
			}

			// Token: 0x06003148 RID: 12616 RVA: 0x0010CDB0 File Offset: 0x0010AFB0
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attrs)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.column);
				PropertyDescriptor[] array;
				if (this.compDesigner != null)
				{
					Hashtable hashtable = new Hashtable();
					for (int i = 0; i < properties.Count; i++)
					{
						hashtable.Add(properties[i].Name, properties[i]);
					}
					((IDesignerFilter)this.compDesigner).PreFilterProperties(hashtable);
					array = new PropertyDescriptor[hashtable.Count + 1];
					hashtable.Values.CopyTo(array, 0);
				}
				else
				{
					array = new PropertyDescriptor[properties.Count + 1];
					properties.CopyTo(array, 0);
				}
				array[array.Length - 1] = new DataGridViewColumnCollectionDialog.ColumnTypePropertyDescriptor();
				return new PropertyDescriptorCollection(array);
			}

			// Token: 0x06003149 RID: 12617 RVA: 0x0010CE54 File Offset: 0x0010B054
			object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
			{
				if (pd == null)
				{
					return this.column;
				}
				if (pd is DataGridViewColumnCollectionDialog.ColumnTypePropertyDescriptor)
				{
					return this;
				}
				return this.column;
			}

			// Token: 0x1700098A RID: 2442
			// (get) Token: 0x0600314A RID: 12618 RVA: 0x0010CE70 File Offset: 0x0010B070
			// (set) Token: 0x0600314B RID: 12619 RVA: 0x00003937 File Offset: 0x00001B37
			ISite IComponent.Site
			{
				get
				{
					return this.owner.liveDataGridView.Site;
				}
				set
				{
				}
			}

			// Token: 0x1400006B RID: 107
			// (add) Token: 0x0600314C RID: 12620 RVA: 0x00003937 File Offset: 0x00001B37
			// (remove) Token: 0x0600314D RID: 12621 RVA: 0x00003937 File Offset: 0x00001B37
			event EventHandler IComponent.Disposed
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x0600314E RID: 12622 RVA: 0x00003937 File Offset: 0x00001B37
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04002129 RID: 8489
			private DataGridViewColumn column;

			// Token: 0x0400212A RID: 8490
			private DataGridViewColumnCollectionDialog owner;

			// Token: 0x0400212B RID: 8491
			private ComponentDesigner compDesigner;

			// Token: 0x0400212C RID: 8492
			private Image toolboxBitmap;
		}

		// Token: 0x02000552 RID: 1362
		private class ColumnTypePropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x0600314F RID: 12623 RVA: 0x0010CE82 File Offset: 0x0010B082
			public ColumnTypePropertyDescriptor() : base("ColumnType", null)
			{
			}

			// Token: 0x1700098B RID: 2443
			// (get) Token: 0x06003150 RID: 12624 RVA: 0x0010CE90 File Offset: 0x0010B090
			public override AttributeCollection Attributes
			{
				get
				{
					EditorAttribute editorAttribute = new EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnTypeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor));
					DescriptionAttribute descriptionAttribute = new DescriptionAttribute(SR.GetString("DataGridViewColumnTypePropertyDescription"));
					CategoryAttribute design = CategoryAttribute.Design;
					Attribute[] attributes = new Attribute[]
					{
						editorAttribute,
						descriptionAttribute,
						design
					};
					return new AttributeCollection(attributes);
				}
			}

			// Token: 0x1700098C RID: 2444
			// (get) Token: 0x06003151 RID: 12625 RVA: 0x0010CEE1 File Offset: 0x0010B0E1
			public override Type ComponentType
			{
				get
				{
					return typeof(DataGridViewColumnCollectionDialog.ListBoxItem);
				}
			}

			// Token: 0x1700098D RID: 2445
			// (get) Token: 0x06003152 RID: 12626 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700098E RID: 2446
			// (get) Token: 0x06003153 RID: 12627 RVA: 0x0010CEED File Offset: 0x0010B0ED
			public override Type PropertyType
			{
				get
				{
					return typeof(Type);
				}
			}

			// Token: 0x06003154 RID: 12628 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x06003155 RID: 12629 RVA: 0x0010CEFC File Offset: 0x0010B0FC
			public override object GetValue(object component)
			{
				if (component == null)
				{
					return null;
				}
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = (DataGridViewColumnCollectionDialog.ListBoxItem)component;
				return listBoxItem.DataGridViewColumn.GetType().Name;
			}

			// Token: 0x06003156 RID: 12630 RVA: 0x00003937 File Offset: 0x00001B37
			public override void ResetValue(object component)
			{
			}

			// Token: 0x06003157 RID: 12631 RVA: 0x0010CF28 File Offset: 0x0010B128
			public override void SetValue(object component, object value)
			{
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = (DataGridViewColumnCollectionDialog.ListBoxItem)component;
				Type type = value as Type;
				if (listBoxItem.DataGridViewColumn.GetType() != type)
				{
					listBoxItem.Owner.ColumnTypeChanged(listBoxItem, type);
					this.OnValueChanged(component, EventArgs.Empty);
				}
			}

			// Token: 0x06003158 RID: 12632 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}
		}
	}
}
