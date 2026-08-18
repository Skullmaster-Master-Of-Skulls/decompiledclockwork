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
	// Token: 0x020001E3 RID: 483
	internal partial class DataGridViewColumnCollectionDialog : Form
	{
		// Token: 0x06001287 RID: 4743 RVA: 0x0005D87C File Offset: 0x0005C87C
		internal DataGridViewColumnCollectionDialog()
		{
			this.InitializeComponent();
			this.dataGridViewPrivateCopy = new DataGridView();
			this.columnsPrivateCopy = this.dataGridViewPrivateCopy.Columns;
			this.columnsPrivateCopy.CollectionChanged += this.columnsPrivateCopy_CollectionChanged;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0005D8C8 File Offset: 0x0005C8C8
		private Bitmap SelectedColumnsItemBitmap
		{
			get
			{
				if (DataGridViewColumnCollectionDialog.selectedColumnsItemBitmap == null)
				{
					DataGridViewColumnCollectionDialog.selectedColumnsItemBitmap = new Bitmap(typeof(DataGridViewColumnCollectionDialog), "DataGridViewColumnsDialog.selectedColumns.bmp");
					DataGridViewColumnCollectionDialog.selectedColumnsItemBitmap.MakeTransparent(Color.Red);
				}
				return DataGridViewColumnCollectionDialog.selectedColumnsItemBitmap;
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0005D900 File Offset: 0x0005C900
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

		// Token: 0x0600128A RID: 4746 RVA: 0x0005D994 File Offset: 0x0005C994
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

		// Token: 0x0600128B RID: 4747 RVA: 0x0005DB8C File Offset: 0x0005CB8C
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

		// Token: 0x0600128C RID: 4748 RVA: 0x0005DE9C File Offset: 0x0005CE9C
		private void componentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (e.Component is DataGridViewColumnCollectionDialog.ListBoxItem && this.selectedColumns.Items.Contains(e.Component))
			{
				this.formIsDirty = true;
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0005DECC File Offset: 0x0005CECC
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

		// Token: 0x0600128E RID: 4750 RVA: 0x0005DF78 File Offset: 0x0005CF78
		private static void CopyDataGridViewColumnState(DataGridViewColumn srcColumn, DataGridViewColumn destColumn)
		{
			destColumn.Frozen = srcColumn.Frozen;
			destColumn.Visible = srcColumn.Visible;
			destColumn.ReadOnly = srcColumn.ReadOnly;
			destColumn.Resizable = srcColumn.Resizable;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0005DFAC File Offset: 0x0005CFAC
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
			catch
			{
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

		// Token: 0x06001291 RID: 4753 RVA: 0x0005E2B8 File Offset: 0x0005D2B8
		private void FixColumnCollectionDisplayIndices()
		{
			for (int i = 0; i < this.columnsPrivateCopy.Count; i++)
			{
				this.columnsPrivateCopy[i].DisplayIndex = i;
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0005E2ED File Offset: 0x0005D2ED
		private void HookComponentChangedEventHandler(IComponentChangeService componentChangeService)
		{
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanged += this.componentChanged;
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0005EB94 File Offset: 0x0005DB94
		private static bool IsColumnAddedByUser(DataGridViewColumn col)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(col)["UserAddedColumn"];
			return propertyDescriptor != null && (bool)propertyDescriptor.GetValue(col);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0005EBC3 File Offset: 0x0005DBC3
		private void okButton_Click(object sender, EventArgs e)
		{
			this.CommitChanges();
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0005EBCC File Offset: 0x0005DBCC
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

		// Token: 0x06001297 RID: 4759 RVA: 0x0005ECE8 File Offset: 0x0005DCE8
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

		// Token: 0x06001298 RID: 4760 RVA: 0x0005EE54 File Offset: 0x0005DE54
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

		// Token: 0x06001299 RID: 4761 RVA: 0x0005EEBA File Offset: 0x0005DEBA
		private void DataGridViewColumnCollectionDialog_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.DataGridViewColumnCollectionDialog_HelpRequestHandled();
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0005EEC9 File Offset: 0x0005DEC9
		private void DataGridViewColumnCollectionDialog_HelpRequested(object sender, HelpEventArgs e)
		{
			this.DataGridViewColumnCollectionDialog_HelpRequestHandled();
			e.Handled = true;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0005EED8 File Offset: 0x0005DED8
		private void DataGridViewColumnCollectionDialog_HelpRequestHandled()
		{
			IHelpService helpService = this.liveDataGridView.Site.GetService(DataGridViewColumnCollectionDialog.iHelpServiceType) as IHelpService;
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword("vs.DataGridViewColumnCollectionDialog");
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0005EF10 File Offset: 0x0005DF10
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

		// Token: 0x0600129D RID: 4765 RVA: 0x0005F04C File Offset: 0x0005E04C
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

		// Token: 0x0600129E RID: 4766 RVA: 0x0005F150 File Offset: 0x0005E150
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
				this.addColumnDialog = new DataGridViewAddColumnDialog(this.columnsPrivateCopy, this.liveDataGridView);
				this.addColumnDialog.StartPosition = FormStartPosition.CenterParent;
			}
			this.addColumnDialog.Start(insertAtPosition, false);
			this.addColumnDialog.ShowDialog(this);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0005F1D4 File Offset: 0x0005E1D4
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

		// Token: 0x060012A0 RID: 4768 RVA: 0x0005F310 File Offset: 0x0005E310
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

		// Token: 0x060012A1 RID: 4769 RVA: 0x0005F488 File Offset: 0x0005E488
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

		// Token: 0x060012A2 RID: 4770 RVA: 0x0005F755 File Offset: 0x0005E755
		private void selectedColumns_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Modifiers == Keys.None && e.KeyCode == Keys.F4)
			{
				this.propertyGrid1.Focus();
				e.Handled = true;
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0005F77C File Offset: 0x0005E77C
		private void selectedColumns_KeyPress(object sender, KeyPressEventArgs e)
		{
			Keys modifierKeys = Control.ModifierKeys;
			if ((modifierKeys & Keys.Control) != Keys.None)
			{
				e.Handled = true;
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0005F7A0 File Offset: 0x0005E7A0
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

		// Token: 0x060012A5 RID: 4773 RVA: 0x0005F8E4 File Offset: 0x0005E8E4
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

		// Token: 0x060012A6 RID: 4774 RVA: 0x0005FAD4 File Offset: 0x0005EAD4
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

		// Token: 0x060012A7 RID: 4775 RVA: 0x0005FB51 File Offset: 0x0005EB51
		private void UnhookComponentChangedEventHandler(IComponentChangeService componentChangeService)
		{
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanged -= this.componentChanged;
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0005FB68 File Offset: 0x0005EB68
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

		// Token: 0x04001152 RID: 4434
		private const int LISTBOXITEMHEIGHT = 17;

		// Token: 0x04001153 RID: 4435
		private const int OWNERDRAWHORIZONTALBUFFER = 3;

		// Token: 0x04001154 RID: 4436
		private const int OWNERDRAWVERTICALBUFFER = 4;

		// Token: 0x04001155 RID: 4437
		private const int OWNERDRAWITEMIMAGEBUFFER = 2;

		// Token: 0x04001161 RID: 4449
		private DataGridView liveDataGridView;

		// Token: 0x04001162 RID: 4450
		private IComponentChangeService compChangeService;

		// Token: 0x04001163 RID: 4451
		private DataGridView dataGridViewPrivateCopy;

		// Token: 0x04001164 RID: 4452
		private DataGridViewColumnCollection columnsPrivateCopy;

		// Token: 0x04001165 RID: 4453
		private Hashtable columnsNames;

		// Token: 0x04001166 RID: 4454
		private DataGridViewAddColumnDialog addColumnDialog;

		// Token: 0x04001167 RID: 4455
		private static Bitmap selectedColumnsItemBitmap;

		// Token: 0x04001168 RID: 4456
		private static ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x04001169 RID: 4457
		private static Type iTypeResolutionServiceType = typeof(ITypeResolutionService);

		// Token: 0x0400116A RID: 4458
		private static Type iTypeDiscoveryServiceType = typeof(ITypeDiscoveryService);

		// Token: 0x0400116B RID: 4459
		private static Type iComponentChangeServiceType = typeof(IComponentChangeService);

		// Token: 0x0400116C RID: 4460
		private static Type iHelpServiceType = typeof(IHelpService);

		// Token: 0x0400116D RID: 4461
		private static Type iUIServiceType = typeof(IUIService);

		// Token: 0x0400116E RID: 4462
		private static Type toolboxBitmapAttributeType = typeof(ToolboxBitmapAttribute);

		// Token: 0x0400116F RID: 4463
		private bool columnCollectionChanging;

		// Token: 0x04001170 RID: 4464
		private bool formIsDirty;

		// Token: 0x04001173 RID: 4467
		private Hashtable userAddedColumns;

		// Token: 0x020001E4 RID: 484
		internal class ListBoxItem : ICustomTypeDescriptor, IComponent, IDisposable
		{
			// Token: 0x060012AA RID: 4778 RVA: 0x0005FC50 File Offset: 0x0005EC50
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

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x060012AB RID: 4779 RVA: 0x0005FCF2 File Offset: 0x0005ECF2
			public DataGridViewColumn DataGridViewColumn
			{
				get
				{
					return this.column;
				}
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x060012AC RID: 4780 RVA: 0x0005FCFA File Offset: 0x0005ECFA
			public ComponentDesigner DataGridViewColumnDesigner
			{
				get
				{
					return this.compDesigner;
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x060012AD RID: 4781 RVA: 0x0005FD02 File Offset: 0x0005ED02
			public DataGridViewColumnCollectionDialog Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x060012AE RID: 4782 RVA: 0x0005FD0A File Offset: 0x0005ED0A
			public Image ToolboxBitmap
			{
				get
				{
					return this.toolboxBitmap;
				}
			}

			// Token: 0x060012AF RID: 4783 RVA: 0x0005FD12 File Offset: 0x0005ED12
			public override string ToString()
			{
				return this.column.HeaderText;
			}

			// Token: 0x060012B0 RID: 4784 RVA: 0x0005FD1F File Offset: 0x0005ED1F
			AttributeCollection ICustomTypeDescriptor.GetAttributes()
			{
				return TypeDescriptor.GetAttributes(this.column);
			}

			// Token: 0x060012B1 RID: 4785 RVA: 0x0005FD2C File Offset: 0x0005ED2C
			string ICustomTypeDescriptor.GetClassName()
			{
				return TypeDescriptor.GetClassName(this.column);
			}

			// Token: 0x060012B2 RID: 4786 RVA: 0x0005FD39 File Offset: 0x0005ED39
			string ICustomTypeDescriptor.GetComponentName()
			{
				return TypeDescriptor.GetComponentName(this.column);
			}

			// Token: 0x060012B3 RID: 4787 RVA: 0x0005FD46 File Offset: 0x0005ED46
			TypeConverter ICustomTypeDescriptor.GetConverter()
			{
				return TypeDescriptor.GetConverter(this.column);
			}

			// Token: 0x060012B4 RID: 4788 RVA: 0x0005FD53 File Offset: 0x0005ED53
			EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
			{
				return TypeDescriptor.GetDefaultEvent(this.column);
			}

			// Token: 0x060012B5 RID: 4789 RVA: 0x0005FD60 File Offset: 0x0005ED60
			PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
			{
				return TypeDescriptor.GetDefaultProperty(this.column);
			}

			// Token: 0x060012B6 RID: 4790 RVA: 0x0005FD6D File Offset: 0x0005ED6D
			object ICustomTypeDescriptor.GetEditor(Type type)
			{
				return TypeDescriptor.GetEditor(this.column, type);
			}

			// Token: 0x060012B7 RID: 4791 RVA: 0x0005FD7B File Offset: 0x0005ED7B
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
			{
				return TypeDescriptor.GetEvents(this.column);
			}

			// Token: 0x060012B8 RID: 4792 RVA: 0x0005FD88 File Offset: 0x0005ED88
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
			{
				return TypeDescriptor.GetEvents(this.column, attrs);
			}

			// Token: 0x060012B9 RID: 4793 RVA: 0x0005FD96 File Offset: 0x0005ED96
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
			{
				return ((ICustomTypeDescriptor)this).GetProperties(null);
			}

			// Token: 0x060012BA RID: 4794 RVA: 0x0005FDA0 File Offset: 0x0005EDA0
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

			// Token: 0x060012BB RID: 4795 RVA: 0x0005FE44 File Offset: 0x0005EE44
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

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x060012BC RID: 4796 RVA: 0x0005FE60 File Offset: 0x0005EE60
			// (set) Token: 0x060012BD RID: 4797 RVA: 0x0005FE72 File Offset: 0x0005EE72
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

			// Token: 0x14000013 RID: 19
			// (add) Token: 0x060012BE RID: 4798 RVA: 0x0005FE74 File Offset: 0x0005EE74
			// (remove) Token: 0x060012BF RID: 4799 RVA: 0x0005FE76 File Offset: 0x0005EE76
			event EventHandler IComponent.Disposed
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x060012C0 RID: 4800 RVA: 0x0005FE78 File Offset: 0x0005EE78
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04001175 RID: 4469
			private DataGridViewColumn column;

			// Token: 0x04001176 RID: 4470
			private DataGridViewColumnCollectionDialog owner;

			// Token: 0x04001177 RID: 4471
			private ComponentDesigner compDesigner;

			// Token: 0x04001178 RID: 4472
			private Image toolboxBitmap;
		}

		// Token: 0x020001E5 RID: 485
		private class ColumnTypePropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x060012C1 RID: 4801 RVA: 0x0005FE7A File Offset: 0x0005EE7A
			public ColumnTypePropertyDescriptor() : base("ColumnType", null)
			{
			}

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0005FE88 File Offset: 0x0005EE88
			public override AttributeCollection Attributes
			{
				get
				{
					EditorAttribute editorAttribute = new EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnTypeEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor));
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

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0005FEE0 File Offset: 0x0005EEE0
			public override Type ComponentType
			{
				get
				{
					return typeof(DataGridViewColumnCollectionDialog.ListBoxItem);
				}
			}

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0005FEEC File Offset: 0x0005EEEC
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170002F8 RID: 760
			// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0005FEEF File Offset: 0x0005EEEF
			public override Type PropertyType
			{
				get
				{
					return typeof(Type);
				}
			}

			// Token: 0x060012C6 RID: 4806 RVA: 0x0005FEFB File Offset: 0x0005EEFB
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x060012C7 RID: 4807 RVA: 0x0005FF00 File Offset: 0x0005EF00
			public override object GetValue(object component)
			{
				if (component == null)
				{
					return null;
				}
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = (DataGridViewColumnCollectionDialog.ListBoxItem)component;
				return listBoxItem.DataGridViewColumn.GetType().Name;
			}

			// Token: 0x060012C8 RID: 4808 RVA: 0x0005FF29 File Offset: 0x0005EF29
			public override void ResetValue(object component)
			{
			}

			// Token: 0x060012C9 RID: 4809 RVA: 0x0005FF2C File Offset: 0x0005EF2C
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

			// Token: 0x060012CA RID: 4810 RVA: 0x0005FF6E File Offset: 0x0005EF6E
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}
		}
	}
}
