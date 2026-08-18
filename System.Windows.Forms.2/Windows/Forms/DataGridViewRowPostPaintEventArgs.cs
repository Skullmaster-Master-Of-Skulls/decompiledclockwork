using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000213 RID: 531
	public class DataGridViewRowPostPaintEventArgs : EventArgs
	{
		// Token: 0x060022AA RID: 8874 RVA: 0x000A690C File Offset: 0x000A4B0C
		public DataGridViewRowPostPaintEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, string errorText, DataGridViewCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			if (dataGridView == null)
			{
				throw new ArgumentNullException("dataGridView");
			}
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (inheritedRowStyle == null)
			{
				throw new ArgumentNullException("inheritedRowStyle");
			}
			this.dataGridView = dataGridView;
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.rowBounds = rowBounds;
			this.rowIndex = rowIndex;
			this.rowState = rowState;
			this.errorText = errorText;
			this.inheritedRowStyle = inheritedRowStyle;
			this.isFirstDisplayedRow = isFirstDisplayedRow;
			this.isLastVisibleRow = isLastVisibleRow;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000A6997 File Offset: 0x000A4B97
		internal DataGridViewRowPostPaintEventArgs(DataGridView dataGridView)
		{
			this.dataGridView = dataGridView;
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x000A69A6 File Offset: 0x000A4BA6
		// (set) Token: 0x060022AD RID: 8877 RVA: 0x000A69AE File Offset: 0x000A4BAE
		public Rectangle ClipBounds
		{
			get
			{
				return this.clipBounds;
			}
			set
			{
				this.clipBounds = value;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x000A69B7 File Offset: 0x000A4BB7
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x000A69BF File Offset: 0x000A4BBF
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x000A69C7 File Offset: 0x000A4BC7
		public DataGridViewCellStyle InheritedRowStyle
		{
			get
			{
				return this.inheritedRowStyle;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x000A69CF File Offset: 0x000A4BCF
		public bool IsFirstDisplayedRow
		{
			get
			{
				return this.isFirstDisplayedRow;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x000A69D7 File Offset: 0x000A4BD7
		public bool IsLastVisibleRow
		{
			get
			{
				return this.isLastVisibleRow;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x000A69DF File Offset: 0x000A4BDF
		public Rectangle RowBounds
		{
			get
			{
				return this.rowBounds;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x000A69E7 File Offset: 0x000A4BE7
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000A69EF File Offset: 0x000A4BEF
		public DataGridViewElementStates State
		{
			get
			{
				return this.rowState;
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x000A69F8 File Offset: 0x000A4BF8
		public void DrawFocus(Rectangle bounds, bool cellsPaintSelectionBackground)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).DrawFocus(this.graphics, this.clipBounds, bounds, this.rowIndex, this.rowState, this.inheritedRowStyle, cellsPaintSelectionBackground);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000A6A74 File Offset: 0x000A4C74
		public void PaintCells(Rectangle clipBounds, DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000A6AF4 File Offset: 0x000A4CF4
		public void PaintCellsBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.Border;
			if (cellsPaintSelectionBackground)
			{
				dataGridViewPaintParts |= DataGridViewPaintParts.SelectionBackground;
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, dataGridViewPaintParts);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000A6B80 File Offset: 0x000A4D80
		public void PaintCellsContent(Rectangle clipBounds)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000A6C00 File Offset: 0x000A4E00
		public void PaintHeader(bool paintSelectionBackground)
		{
			DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon;
			if (paintSelectionBackground)
			{
				dataGridViewPaintParts |= DataGridViewPaintParts.SelectionBackground;
			}
			this.PaintHeader(dataGridViewPaintParts);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000A6C20 File Offset: 0x000A4E20
		public void PaintHeader(DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintHeader(this.graphics, this.clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000A6CA4 File Offset: 0x000A4EA4
		internal void SetProperties(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, string errorText, DataGridViewCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.rowBounds = rowBounds;
			this.rowIndex = rowIndex;
			this.rowState = rowState;
			this.errorText = errorText;
			this.inheritedRowStyle = inheritedRowStyle;
			this.isFirstDisplayedRow = isFirstDisplayedRow;
			this.isLastVisibleRow = isLastVisibleRow;
		}

		// Token: 0x04000E51 RID: 3665
		private DataGridView dataGridView;

		// Token: 0x04000E52 RID: 3666
		private Graphics graphics;

		// Token: 0x04000E53 RID: 3667
		private Rectangle clipBounds;

		// Token: 0x04000E54 RID: 3668
		private Rectangle rowBounds;

		// Token: 0x04000E55 RID: 3669
		private DataGridViewCellStyle inheritedRowStyle;

		// Token: 0x04000E56 RID: 3670
		private int rowIndex;

		// Token: 0x04000E57 RID: 3671
		private DataGridViewElementStates rowState;

		// Token: 0x04000E58 RID: 3672
		private string errorText;

		// Token: 0x04000E59 RID: 3673
		private bool isFirstDisplayedRow;

		// Token: 0x04000E5A RID: 3674
		private bool isLastVisibleRow;
	}
}
