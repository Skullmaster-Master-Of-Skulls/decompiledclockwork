using System;

namespace System.Windows.Forms
{
	// Token: 0x020001B1 RID: 433
	public class DataGridViewCellParsingEventArgs : ConvertEventArgs
	{
		// Token: 0x06001E6B RID: 7787 RVA: 0x0008F8E9 File Offset: 0x0008DAE9
		public DataGridViewCellParsingEventArgs(int rowIndex, int columnIndex, object value, Type desiredType, DataGridViewCellStyle inheritedCellStyle) : base(value, desiredType)
		{
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
			this.inheritedCellStyle = inheritedCellStyle;
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x0008F90A File Offset: 0x0008DB0A
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x0008F912 File Offset: 0x0008DB12
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x0008F91A File Offset: 0x0008DB1A
		// (set) Token: 0x06001E6F RID: 7791 RVA: 0x0008F922 File Offset: 0x0008DB22
		public DataGridViewCellStyle InheritedCellStyle
		{
			get
			{
				return this.inheritedCellStyle;
			}
			set
			{
				this.inheritedCellStyle = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x0008F92B File Offset: 0x0008DB2B
		// (set) Token: 0x06001E71 RID: 7793 RVA: 0x0008F933 File Offset: 0x0008DB33
		public bool ParsingApplied
		{
			get
			{
				return this.parsingApplied;
			}
			set
			{
				this.parsingApplied = value;
			}
		}

		// Token: 0x04000CE4 RID: 3300
		private int rowIndex;

		// Token: 0x04000CE5 RID: 3301
		private int columnIndex;

		// Token: 0x04000CE6 RID: 3302
		private DataGridViewCellStyle inheritedCellStyle;

		// Token: 0x04000CE7 RID: 3303
		private bool parsingApplied;
	}
}
