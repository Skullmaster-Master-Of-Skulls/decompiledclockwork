using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000189 RID: 393
	internal sealed class DataGridState : ICloneable
	{
		// Token: 0x06001768 RID: 5992 RVA: 0x00054C41 File Offset: 0x00052E41
		public DataGridState()
		{
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00054C55 File Offset: 0x00052E55
		public DataGridState(DataGrid dataGrid)
		{
			this.PushState(dataGrid);
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00054C70 File Offset: 0x00052E70
		internal AccessibleObject ParentRowAccessibleObject
		{
			get
			{
				if (this.parentRowAccessibleObject == null)
				{
					this.parentRowAccessibleObject = new DataGridState.DataGridStateParentRowAccessibleObject(this);
				}
				return this.parentRowAccessibleObject;
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00054C8C File Offset: 0x00052E8C
		public object Clone()
		{
			return new DataGridState
			{
				DataGridRows = this.DataGridRows,
				DataSource = this.DataSource,
				DataMember = this.DataMember,
				FirstVisibleRow = this.FirstVisibleRow,
				FirstVisibleCol = this.FirstVisibleCol,
				CurrentRow = this.CurrentRow,
				CurrentCol = this.CurrentCol,
				GridColumnStyles = this.GridColumnStyles,
				ListManager = this.ListManager,
				DataGrid = this.DataGrid
			};
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00054D18 File Offset: 0x00052F18
		public void PushState(DataGrid dataGrid)
		{
			this.DataSource = dataGrid.DataSource;
			this.DataMember = dataGrid.DataMember;
			this.DataGrid = dataGrid;
			this.DataGridRows = dataGrid.DataGridRows;
			this.DataGridRowsLength = dataGrid.DataGridRowsLength;
			this.FirstVisibleRow = dataGrid.firstVisibleRow;
			this.FirstVisibleCol = dataGrid.firstVisibleCol;
			this.CurrentRow = dataGrid.currentRow;
			this.GridColumnStyles = new GridColumnStylesCollection(dataGrid.myGridTable);
			this.GridColumnStyles.Clear();
			foreach (object obj in dataGrid.myGridTable.GridColumnStyles)
			{
				DataGridColumnStyle column = (DataGridColumnStyle)obj;
				this.GridColumnStyles.Add(column);
			}
			this.ListManager = dataGrid.ListManager;
			this.ListManager.ItemChanged += this.DataSource_Changed;
			this.ListManager.MetaDataChanged += this.DataSource_MetaDataChanged;
			this.CurrentCol = dataGrid.currentCol;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00054E3C File Offset: 0x0005303C
		public void RemoveChangeNotification()
		{
			this.ListManager.ItemChanged -= this.DataSource_Changed;
			this.ListManager.MetaDataChanged -= this.DataSource_MetaDataChanged;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00054E6C File Offset: 0x0005306C
		public void PullState(DataGrid dataGrid, bool createColumn)
		{
			dataGrid.Set_ListManager(this.DataSource, this.DataMember, true, createColumn);
			dataGrid.firstVisibleRow = this.FirstVisibleRow;
			dataGrid.firstVisibleCol = this.FirstVisibleCol;
			dataGrid.currentRow = this.CurrentRow;
			dataGrid.currentCol = this.CurrentCol;
			dataGrid.SetDataGridRows(this.DataGridRows, this.DataGridRowsLength);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00054ECF File Offset: 0x000530CF
		private void DataSource_Changed(object sender, ItemChangedEventArgs e)
		{
			if (this.DataGrid != null && this.ListManager.Position == e.Index)
			{
				this.DataGrid.InvalidateParentRows();
				return;
			}
			if (this.DataGrid != null)
			{
				this.DataGrid.ParentRowsDataChanged();
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00054F0B File Offset: 0x0005310B
		private void DataSource_MetaDataChanged(object sender, EventArgs e)
		{
			if (this.DataGrid != null)
			{
				this.DataGrid.ParentRowsDataChanged();
			}
		}

		// Token: 0x04000A8B RID: 2699
		public object DataSource;

		// Token: 0x04000A8C RID: 2700
		public string DataMember;

		// Token: 0x04000A8D RID: 2701
		public CurrencyManager ListManager;

		// Token: 0x04000A8E RID: 2702
		public DataGridRow[] DataGridRows = new DataGridRow[0];

		// Token: 0x04000A8F RID: 2703
		public DataGrid DataGrid;

		// Token: 0x04000A90 RID: 2704
		public int DataGridRowsLength;

		// Token: 0x04000A91 RID: 2705
		public GridColumnStylesCollection GridColumnStyles;

		// Token: 0x04000A92 RID: 2706
		public int FirstVisibleRow;

		// Token: 0x04000A93 RID: 2707
		public int FirstVisibleCol;

		// Token: 0x04000A94 RID: 2708
		public int CurrentRow;

		// Token: 0x04000A95 RID: 2709
		public int CurrentCol;

		// Token: 0x04000A96 RID: 2710
		public DataGridRow LinkingRow;

		// Token: 0x04000A97 RID: 2711
		private AccessibleObject parentRowAccessibleObject;

		// Token: 0x02000655 RID: 1621
		[ComVisible(true)]
		internal class DataGridStateParentRowAccessibleObject : AccessibleObject
		{
			// Token: 0x06006539 RID: 25913 RVA: 0x00178C28 File Offset: 0x00176E28
			public DataGridStateParentRowAccessibleObject(DataGridState owner)
			{
				this.owner = owner;
			}

			// Token: 0x170015CD RID: 5581
			// (get) Token: 0x0600653A RID: 25914 RVA: 0x00178C38 File Offset: 0x00176E38
			public override Rectangle Bounds
			{
				get
				{
					DataGridParentRows dataGridParentRows = ((DataGridParentRows.DataGridParentRowsAccessibleObject)this.Parent).Owner;
					DataGrid dataGrid = this.owner.LinkingRow.DataGrid;
					Rectangle boundsForDataGridStateAccesibility = dataGridParentRows.GetBoundsForDataGridStateAccesibility(this.owner);
					boundsForDataGridStateAccesibility.Y += dataGrid.ParentRowsBounds.Y;
					return dataGrid.RectangleToScreen(boundsForDataGridStateAccesibility);
				}
			}

			// Token: 0x170015CE RID: 5582
			// (get) Token: 0x0600653B RID: 25915 RVA: 0x00178C97 File Offset: 0x00176E97
			public override string Name
			{
				get
				{
					return SR.GetString("AccDGParentRow");
				}
			}

			// Token: 0x170015CF RID: 5583
			// (get) Token: 0x0600653C RID: 25916 RVA: 0x00178CA3 File Offset: 0x00176EA3
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.owner.LinkingRow.DataGrid.ParentRowsAccessibleObject;
				}
			}

			// Token: 0x170015D0 RID: 5584
			// (get) Token: 0x0600653D RID: 25917 RVA: 0x0001612D File Offset: 0x0001432D
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ListItem;
				}
			}

			// Token: 0x170015D1 RID: 5585
			// (get) Token: 0x0600653E RID: 25918 RVA: 0x00178CBC File Offset: 0x00176EBC
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					CurrencyManager currencyManager = (CurrencyManager)this.owner.LinkingRow.DataGrid.BindingContext[this.owner.DataSource, this.owner.DataMember];
					stringBuilder.Append(this.owner.ListManager.GetListName());
					stringBuilder.Append(": ");
					bool flag = false;
					foreach (object obj in this.owner.GridColumnStyles)
					{
						DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)obj;
						if (flag)
						{
							stringBuilder.Append(", ");
						}
						string headerText = dataGridColumnStyle.HeaderText;
						string value = dataGridColumnStyle.PropertyDescriptor.Converter.ConvertToString(dataGridColumnStyle.PropertyDescriptor.GetValue(currencyManager.Current));
						stringBuilder.Append(headerText);
						stringBuilder.Append(": ");
						stringBuilder.Append(value);
						flag = true;
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x0600653F RID: 25919 RVA: 0x00178DDC File Offset: 0x00176FDC
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				DataGridParentRows.DataGridParentRowsAccessibleObject dataGridParentRowsAccessibleObject = (DataGridParentRows.DataGridParentRowsAccessibleObject)this.Parent;
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					return dataGridParentRowsAccessibleObject.GetPrev(this);
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					return dataGridParentRowsAccessibleObject.GetNext(this);
				default:
					return null;
				}
			}

			// Token: 0x040039E7 RID: 14823
			private DataGridState owner;
		}
	}
}
