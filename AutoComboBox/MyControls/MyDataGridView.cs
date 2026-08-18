using System;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x020000AF RID: 175
	public class MyDataGridView : DataGridView
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x00034F2A File Offset: 0x00033F2A
		public MyDataGridView()
		{
			base.RowHeadersVisible = false;
			base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			base.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			base.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
		}
	}
}
