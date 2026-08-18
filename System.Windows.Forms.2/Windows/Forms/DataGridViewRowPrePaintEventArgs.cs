using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000214 RID: 532
	public class DataGridViewRowPrePaintEventArgs : HandledEventArgs
	{
		// Token: 0x060022BD RID: 8893 RVA: 0x000A6CF8 File Offset: 0x000A4EF8
		public DataGridViewRowPrePaintEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, string errorText, DataGridViewCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
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
			this.paintParts = DataGridViewPaintParts.All;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000A6D8B File Offset: 0x000A4F8B
		internal DataGridViewRowPrePaintEventArgs(DataGridView dataGridView)
		{
			this.dataGridView = dataGridView;
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x000A6D9A File Offset: 0x000A4F9A
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x000A6DA2 File Offset: 0x000A4FA2
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

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x000A6DAB File Offset: 0x000A4FAB
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x000A6DB3 File Offset: 0x000A4FB3
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000A6DBB File Offset: 0x000A4FBB
		public DataGridViewCellStyle InheritedRowStyle
		{
			get
			{
				return this.inheritedRowStyle;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x000A6DC3 File Offset: 0x000A4FC3
		public bool IsFirstDisplayedRow
		{
			get
			{
				return this.isFirstDisplayedRow;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x000A6DCB File Offset: 0x000A4FCB
		public bool IsLastVisibleRow
		{
			get
			{
				return this.isLastVisibleRow;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x000A6DD3 File Offset: 0x000A4FD3
		// (set) Token: 0x060022C7 RID: 8903 RVA: 0x000A6DDB File Offset: 0x000A4FDB
		public DataGridViewPaintParts PaintParts
		{
			get
			{
				return this.paintParts;
			}
			set
			{
				if ((value & ~DataGridViewPaintParts.All) != DataGridViewPaintParts.None)
				{
					throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewPaintPartsCombination", new object[]
					{
						"value"
					}));
				}
				this.paintParts = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x000A6E08 File Offset: 0x000A5008
		public Rectangle RowBounds
		{
			get
			{
				return this.rowBounds;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x000A6E10 File Offset: 0x000A5010
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x000A6E18 File Offset: 0x000A5018
		public DataGridViewElementStates State
		{
			get
			{
				return this.rowState;
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000A6E20 File Offset: 0x000A5020
		public void DrawFocus(Rectangle bounds, bool cellsPaintSelectionBackground)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).DrawFocus(this.graphics, this.clipBounds, bounds, this.rowIndex, this.rowState, this.inheritedRowStyle, cellsPaintSelectionBackground);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000A6E9C File Offset: 0x000A509C
		public void PaintCells(Rectangle clipBounds, DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000A6F1C File Offset: 0x000A511C
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

		// Token: 0x060022CE RID: 8910 RVA: 0x000A6FA8 File Offset: 0x000A51A8
		public void PaintCellsContent(Rectangle clipBounds)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000A7028 File Offset: 0x000A5228
		public void PaintHeader(bool paintSelectionBackground)
		{
			DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon;
			if (paintSelectionBackground)
			{
				dataGridViewPaintParts |= DataGridViewPaintParts.SelectionBackground;
			}
			this.PaintHeader(dataGridViewPaintParts);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000A7048 File Offset: 0x000A5248
		public void PaintHeader(DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintHeader(this.graphics, this.clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000A70CC File Offset: 0x000A52CC
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
			this.paintParts = DataGridViewPaintParts.All;
			base.Handled = false;
		}

		// Token: 0x04000E5B RID: 3675
		private DataGridView dataGridView;

		// Token: 0x04000E5C RID: 3676
		private Graphics graphics;

		// Token: 0x04000E5D RID: 3677
		private Rectangle clipBounds;

		// Token: 0x04000E5E RID: 3678
		private Rectangle rowBounds;

		// Token: 0x04000E5F RID: 3679
		private DataGridViewCellStyle inheritedRowStyle;

		// Token: 0x04000E60 RID: 3680
		private int rowIndex;

		// Token: 0x04000E61 RID: 3681
		private DataGridViewElementStates rowState;

		// Token: 0x04000E62 RID: 3682
		private string errorText;

		// Token: 0x04000E63 RID: 3683
		private bool isFirstDisplayedRow;

		// Token: 0x04000E64 RID: 3684
		private bool isLastVisibleRow;

		// Token: 0x04000E65 RID: 3685
		private DataGridViewPaintParts paintParts;
	}
}
