using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200044F RID: 1103
	[SupportsEventValidation]
	internal sealed class LayoutTable : Table
	{
		// Token: 0x0600353B RID: 13627 RVA: 0x000AC9A0 File Offset: 0x000AABA0
		public LayoutTable(int rows, int columns, Page page)
		{
			if (rows <= 0)
			{
				throw new ArgumentOutOfRangeException("rows");
			}
			if (columns <= 0)
			{
				throw new ArgumentOutOfRangeException("columns");
			}
			if (page != null)
			{
				this.Page = page;
			}
			for (int i = 0; i < rows; i++)
			{
				TableRow tableRow = new TableRow();
				this.Rows.Add(tableRow);
				for (int j = 0; j < columns; j++)
				{
					TableCell cell = new LayoutTableCell();
					tableRow.Cells.Add(cell);
				}
			}
		}

		// Token: 0x17000F7A RID: 3962
		public TableCell this[int row, int column]
		{
			get
			{
				return this.Rows[row].Cells[column];
			}
		}
	}
}
