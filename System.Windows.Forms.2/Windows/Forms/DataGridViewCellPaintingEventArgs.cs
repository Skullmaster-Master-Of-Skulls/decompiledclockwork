using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020001B0 RID: 432
	public class DataGridViewCellPaintingEventArgs : HandledEventArgs
	{
		// Token: 0x06001E59 RID: 7769 RVA: 0x0008F4F0 File Offset: 0x0008D6F0
		public DataGridViewCellPaintingEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, int columnIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (dataGridView == null)
			{
				throw new ArgumentNullException("dataGridView");
			}
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if ((paintParts & ~DataGridViewPaintParts.All) != DataGridViewPaintParts.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewPaintPartsCombination", new object[]
				{
					"paintParts"
				}));
			}
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.cellBounds = cellBounds;
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
			this.cellState = cellState;
			this.value = value;
			this.formattedValue = formattedValue;
			this.errorText = errorText;
			this.cellStyle = cellStyle;
			this.advancedBorderStyle = advancedBorderStyle;
			this.paintParts = paintParts;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0008F5B1 File Offset: 0x0008D7B1
		internal DataGridViewCellPaintingEventArgs(DataGridView dataGridView)
		{
			this.dataGridView = dataGridView;
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0008F5C0 File Offset: 0x0008D7C0
		public DataGridViewAdvancedBorderStyle AdvancedBorderStyle
		{
			get
			{
				return this.advancedBorderStyle;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0008F5C8 File Offset: 0x0008D7C8
		public Rectangle CellBounds
		{
			get
			{
				return this.cellBounds;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x0008F5D0 File Offset: 0x0008D7D0
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0008F5D8 File Offset: 0x0008D7D8
		public Rectangle ClipBounds
		{
			get
			{
				return this.clipBounds;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x0008F5E0 File Offset: 0x0008D7E0
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0008F5E8 File Offset: 0x0008D7E8
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x0008F5F0 File Offset: 0x0008D7F0
		public object FormattedValue
		{
			get
			{
				return this.formattedValue;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0008F5F8 File Offset: 0x0008D7F8
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x0008F600 File Offset: 0x0008D800
		public DataGridViewPaintParts PaintParts
		{
			get
			{
				return this.paintParts;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x0008F608 File Offset: 0x0008D808
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x0008F610 File Offset: 0x0008D810
		public DataGridViewElementStates State
		{
			get
			{
				return this.cellState;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x0008F618 File Offset: 0x0008D818
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0008F620 File Offset: 0x0008D820
		public void Paint(Rectangle clipBounds, DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < -1 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			if (this.columnIndex < -1 || this.columnIndex >= this.dataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_ColumnIndexOutOfRange"));
			}
			this.dataGridView.GetCellInternal(this.columnIndex, this.rowIndex).PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, paintParts);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0008F6E4 File Offset: 0x0008D8E4
		public void PaintBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
		{
			if (this.rowIndex < -1 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			if (this.columnIndex < -1 || this.columnIndex >= this.dataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_ColumnIndexOutOfRange"));
			}
			DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.Border;
			if (cellsPaintSelectionBackground)
			{
				dataGridViewPaintParts |= DataGridViewPaintParts.SelectionBackground;
			}
			this.dataGridView.GetCellInternal(this.columnIndex, this.rowIndex).PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, dataGridViewPaintParts);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0008F7B4 File Offset: 0x0008D9B4
		public void PaintContent(Rectangle clipBounds)
		{
			if (this.rowIndex < -1 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			if (this.columnIndex < -1 || this.columnIndex >= this.dataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_ColumnIndexOutOfRange"));
			}
			this.dataGridView.GetCellInternal(this.columnIndex, this.rowIndex).PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0008F878 File Offset: 0x0008DA78
		internal void SetProperties(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, int columnIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.cellBounds = cellBounds;
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
			this.cellState = cellState;
			this.value = value;
			this.formattedValue = formattedValue;
			this.errorText = errorText;
			this.cellStyle = cellStyle;
			this.advancedBorderStyle = advancedBorderStyle;
			this.paintParts = paintParts;
			base.Handled = false;
		}

		// Token: 0x04000CD7 RID: 3287
		private DataGridView dataGridView;

		// Token: 0x04000CD8 RID: 3288
		private Graphics graphics;

		// Token: 0x04000CD9 RID: 3289
		private Rectangle clipBounds;

		// Token: 0x04000CDA RID: 3290
		private Rectangle cellBounds;

		// Token: 0x04000CDB RID: 3291
		private int rowIndex;

		// Token: 0x04000CDC RID: 3292
		private int columnIndex;

		// Token: 0x04000CDD RID: 3293
		private DataGridViewElementStates cellState;

		// Token: 0x04000CDE RID: 3294
		private object value;

		// Token: 0x04000CDF RID: 3295
		private object formattedValue;

		// Token: 0x04000CE0 RID: 3296
		private string errorText;

		// Token: 0x04000CE1 RID: 3297
		private DataGridViewCellStyle cellStyle;

		// Token: 0x04000CE2 RID: 3298
		private DataGridViewAdvancedBorderStyle advancedBorderStyle;

		// Token: 0x04000CE3 RID: 3299
		private DataGridViewPaintParts paintParts;
	}
}
