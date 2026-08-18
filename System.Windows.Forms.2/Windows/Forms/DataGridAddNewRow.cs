using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200017C RID: 380
	internal class DataGridAddNewRow : DataGridRow
	{
		// Token: 0x060015D7 RID: 5591 RVA: 0x0004EFEC File Offset: 0x0004D1EC
		public DataGridAddNewRow(DataGrid dGrid, DataGridTableStyle gridTable, int rowNum) : base(dGrid, gridTable, rowNum)
		{
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0004EFF7 File Offset: 0x0004D1F7
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x0004EFFF File Offset: 0x0004D1FF
		public bool DataBound
		{
			get
			{
				return this.dataBound;
			}
			set
			{
				this.dataBound = value;
			}
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0004F008 File Offset: 0x0004D208
		public override void OnEdit()
		{
			if (!this.DataBound)
			{
				base.DataGrid.AddNewRow();
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0004F01D File Offset: 0x0004D21D
		public override void OnRowLeave()
		{
			if (this.DataBound)
			{
				this.DataBound = false;
			}
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000072B6 File Offset: 0x000054B6
		internal override void LoseChildFocus(Rectangle rowHeader, bool alignToRight)
		{
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal override bool ProcessTabKey(Keys keyData, Rectangle rowHeaders, bool alignToRight)
		{
			return false;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0004F02E File Offset: 0x0004D22E
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int columnCount)
		{
			return this.Paint(g, bounds, trueRowBounds, firstVisibleColumn, columnCount, false);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0004F040 File Offset: 0x0004D240
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int columnCount, bool alignToRight)
		{
			Rectangle bounds2 = bounds;
			DataGridLineStyle gridLineStyle;
			if (this.dgTable.IsDefault)
			{
				gridLineStyle = base.DataGrid.GridLineStyle;
			}
			else
			{
				gridLineStyle = this.dgTable.GridLineStyle;
			}
			int num = (base.DataGrid == null) ? 0 : ((gridLineStyle == DataGridLineStyle.Solid) ? 1 : 0);
			bounds2.Height -= num;
			int num2 = base.PaintData(g, bounds2, firstVisibleColumn, columnCount, alignToRight);
			if (num > 0)
			{
				this.PaintBottomBorder(g, bounds, num2, num, alignToRight);
			}
			return num2;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0004F0BC File Offset: 0x0004D2BC
		protected override void PaintCellContents(Graphics g, Rectangle cellBounds, DataGridColumnStyle column, Brush backBr, Brush foreBrush, bool alignToRight)
		{
			if (this.DataBound)
			{
				CurrencyManager listManager = base.DataGrid.ListManager;
				column.Paint(g, cellBounds, listManager, base.RowNumber, alignToRight);
				return;
			}
			base.PaintCellContents(g, cellBounds, column, backBr, foreBrush, alignToRight);
		}

		// Token: 0x04000A13 RID: 2579
		private bool dataBound;
	}
}
