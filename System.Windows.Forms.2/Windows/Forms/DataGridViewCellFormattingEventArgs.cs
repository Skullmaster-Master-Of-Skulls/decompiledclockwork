using System;

namespace System.Windows.Forms
{
	// Token: 0x020001AB RID: 427
	public class DataGridViewCellFormattingEventArgs : ConvertEventArgs
	{
		// Token: 0x06001E3D RID: 7741 RVA: 0x0008F129 File Offset: 0x0008D329
		public DataGridViewCellFormattingEventArgs(int columnIndex, int rowIndex, object value, Type desiredType, DataGridViewCellStyle cellStyle) : base(value, desiredType)
		{
			if (columnIndex < -1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			if (rowIndex < -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
			this.cellStyle = cellStyle;
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x0008F168 File Offset: 0x0008D368
		// (set) Token: 0x06001E3F RID: 7743 RVA: 0x0008F170 File Offset: 0x0008D370
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
			set
			{
				this.cellStyle = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0008F179 File Offset: 0x0008D379
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x0008F181 File Offset: 0x0008D381
		// (set) Token: 0x06001E42 RID: 7746 RVA: 0x0008F189 File Offset: 0x0008D389
		public bool FormattingApplied
		{
			get
			{
				return this.formattingApplied;
			}
			set
			{
				this.formattingApplied = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x0008F192 File Offset: 0x0008D392
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000CC8 RID: 3272
		private int columnIndex;

		// Token: 0x04000CC9 RID: 3273
		private int rowIndex;

		// Token: 0x04000CCA RID: 3274
		private DataGridViewCellStyle cellStyle;

		// Token: 0x04000CCB RID: 3275
		private bool formattingApplied;
	}
}
