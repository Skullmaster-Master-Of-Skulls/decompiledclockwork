using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ImportExportClassLibrary
{
	// Token: 0x02000045 RID: 69
	public partial class DataTableViewProperties : Form
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0001C731 File Offset: 0x0001B731
		public DataTableViewProperties(DataGrid _dataGrid)
		{
			this.InitializeComponent();
			this.dataGrid = _dataGrid;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001CA80 File Offset: 0x0001BA80
		private void DataTableViewProperties_Load(object sender, EventArgs e)
		{
			if (this.dataGrid != null && this.dataGrid.DataSource != null)
			{
				DataView dataView;
				DataTable dataTable;
				if (this.dataGrid.DataSource is DataView)
				{
					dataView = (DataView)this.dataGrid.DataSource;
					dataTable = dataView.Table;
				}
				else if (this.dataGrid.DataSource is DataTable)
				{
					dataView = null;
					dataTable = (DataTable)this.dataGrid.DataSource;
				}
				else
				{
					dataView = null;
					dataTable = null;
				}
				if (dataTable != null)
				{
					this.lbl_numRows.Text = dataTable.Rows.Count.ToString();
					this.lbl_numCols.Text = dataTable.Columns.Count.ToString();
					if (dataView != null)
					{
						this.lbl_sort.Text = dataView.Sort;
					}
				}
			}
		}

		// Token: 0x04000186 RID: 390
		private DataGrid dataGrid;
	}
}
