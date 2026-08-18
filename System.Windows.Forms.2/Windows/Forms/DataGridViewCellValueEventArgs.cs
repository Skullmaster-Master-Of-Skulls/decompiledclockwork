using System;

namespace System.Windows.Forms
{
	// Token: 0x020001BB RID: 443
	public class DataGridViewCellValueEventArgs : EventArgs
	{
		// Token: 0x06001EBD RID: 7869 RVA: 0x00090B38 File Offset: 0x0008ED38
		internal DataGridViewCellValueEventArgs()
		{
			this.columnIndex = (this.rowIndex = -1);
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00090B5B File Offset: 0x0008ED5B
		public DataGridViewCellValueEventArgs(int columnIndex, int rowIndex)
		{
			if (columnIndex < 0)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			if (rowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x00090B8F File Offset: 0x0008ED8F
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00090B97 File Offset: 0x0008ED97
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x00090B9F File Offset: 0x0008ED9F
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x00090BA7 File Offset: 0x0008EDA7
		public object Value
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00090BB0 File Offset: 0x0008EDB0
		internal void SetProperties(int columnIndex, int rowIndex, object value)
		{
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
			this.val = value;
		}

		// Token: 0x04000D10 RID: 3344
		private int rowIndex;

		// Token: 0x04000D11 RID: 3345
		private int columnIndex;

		// Token: 0x04000D12 RID: 3346
		private object val;
	}
}
