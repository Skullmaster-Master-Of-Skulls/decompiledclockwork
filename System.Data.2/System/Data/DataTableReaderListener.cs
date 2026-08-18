using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000D6 RID: 214
	internal sealed class DataTableReaderListener
	{
		// Token: 0x06000E10 RID: 3600 RVA: 0x00075528 File Offset: 0x00074928
		internal DataTableReaderListener(DataTableReader reader)
		{
			if (reader == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTableReader");
			}
			if (this.currentDataTable != null)
			{
				this.UnSubscribeEvents();
			}
			this.readerWeak = new WeakReference(reader);
			this.currentDataTable = reader.CurrentDataTable;
			if (this.currentDataTable != null)
			{
				this.SubscribeEvents();
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00075580 File Offset: 0x00074980
		internal void CleanUp()
		{
			this.UnSubscribeEvents();
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00075594 File Offset: 0x00074994
		internal void UpdataTable(DataTable datatable)
		{
			if (datatable == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			this.UnSubscribeEvents();
			this.currentDataTable = datatable;
			this.SubscribeEvents();
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x000755C4 File Offset: 0x000749C4
		private void SubscribeEvents()
		{
			if (this.currentDataTable == null)
			{
				return;
			}
			if (this.isSubscribed)
			{
				return;
			}
			this.currentDataTable.Columns.ColumnPropertyChanged += this.SchemaChanged;
			this.currentDataTable.Columns.CollectionChanged += this.SchemaChanged;
			this.currentDataTable.RowChanged += this.DataChanged;
			this.currentDataTable.RowDeleted += this.DataChanged;
			this.currentDataTable.TableCleared += this.DataTableCleared;
			this.isSubscribed = true;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00075668 File Offset: 0x00074A68
		private void UnSubscribeEvents()
		{
			if (this.currentDataTable == null)
			{
				return;
			}
			if (!this.isSubscribed)
			{
				return;
			}
			this.currentDataTable.Columns.ColumnPropertyChanged -= this.SchemaChanged;
			this.currentDataTable.Columns.CollectionChanged -= this.SchemaChanged;
			this.currentDataTable.RowChanged -= this.DataChanged;
			this.currentDataTable.RowDeleted -= this.DataChanged;
			this.currentDataTable.TableCleared -= this.DataTableCleared;
			this.isSubscribed = false;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0007570C File Offset: 0x00074B0C
		private void DataTableCleared(object sender, DataTableClearEventArgs e)
		{
			DataTableReader dataTableReader = (DataTableReader)this.readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.DataTableCleared();
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0007573C File Offset: 0x00074B3C
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			DataTableReader dataTableReader = (DataTableReader)this.readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.SchemaChanged();
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0007576C File Offset: 0x00074B6C
		private void DataChanged(object sender, DataRowChangeEventArgs args)
		{
			DataTableReader dataTableReader = (DataTableReader)this.readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.DataChanged(args);
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x04000415 RID: 1045
		private DataTable currentDataTable;

		// Token: 0x04000416 RID: 1046
		private bool isSubscribed;

		// Token: 0x04000417 RID: 1047
		private WeakReference readerWeak;
	}
}
