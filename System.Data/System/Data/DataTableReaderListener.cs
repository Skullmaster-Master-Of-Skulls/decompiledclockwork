using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000A4 RID: 164
	internal sealed class DataTableReaderListener
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x0020B5D8 File Offset: 0x0020A9D8
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

		// Token: 0x06000B03 RID: 2819 RVA: 0x0020B638 File Offset: 0x0020AA38
		internal void CleanUp()
		{
			this.UnSubscribeEvents();
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0020B658 File Offset: 0x0020AA58
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

		// Token: 0x06000B05 RID: 2821 RVA: 0x0020B688 File Offset: 0x0020AA88
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

		// Token: 0x06000B06 RID: 2822 RVA: 0x0020B738 File Offset: 0x0020AB38
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

		// Token: 0x06000B07 RID: 2823 RVA: 0x0020B7E8 File Offset: 0x0020ABE8
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

		// Token: 0x06000B08 RID: 2824 RVA: 0x0020B818 File Offset: 0x0020AC18
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

		// Token: 0x06000B09 RID: 2825 RVA: 0x0020B848 File Offset: 0x0020AC48
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

		// Token: 0x04000837 RID: 2103
		private DataTable currentDataTable;

		// Token: 0x04000838 RID: 2104
		private bool isSubscribed;

		// Token: 0x04000839 RID: 2105
		private WeakReference readerWeak;
	}
}
