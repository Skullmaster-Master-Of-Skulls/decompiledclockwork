using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x0200002A RID: 42
	public partial class DataTableViewProperties : Form
	{
		// Token: 0x06000127 RID: 295 RVA: 0x0000C64D File Offset: 0x0000B64D
		public DataTableViewProperties(DataGrid _dataGrid)
		{
			this.InitializeComponent();
			this.dataGrid = _dataGrid;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000CD60 File Offset: 0x0000BD60
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
					DataTable dataTable2 = new DataTable();
					dataTable2.Columns.Add("colname");
					dataTable2.Columns.Add("dbtype");
					for (int i = 0; i < dataTable.Columns.Count; i++)
					{
						DataColumn dataColumn = dataTable.Columns[i];
						dataTable2.Rows.Add(new object[]
						{
							dataColumn.ColumnName,
							dataColumn.DataType.ToString()
						});
					}
					this.dataGrid1.DataSource = dataTable2;
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000CF02 File Offset: 0x0000BF02
		private void label1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0400016F RID: 367
		private DataGrid dataGrid;
	}
}
