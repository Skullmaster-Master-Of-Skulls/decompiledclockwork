using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000BB RID: 187
	public class DataGridNoActiveCellColumn : DataGridTextBoxColumn
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x00039FC8 File Offset: 0x00038FC8
		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			if (this.SelectedRow > -1 && this.SelectedRow < source.List.Count + 1)
			{
				this.DataGridTableStyle.DataGrid.UnSelect(this.SelectedRow);
			}
			this.SelectedRow = rowNum;
			this.DataGridTableStyle.DataGrid.Select(this.SelectedRow);
		}

		// Token: 0x04000588 RID: 1416
		private int SelectedRow = -1;
	}
}
