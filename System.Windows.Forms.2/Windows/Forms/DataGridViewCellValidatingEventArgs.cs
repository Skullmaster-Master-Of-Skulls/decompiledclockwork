using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020001BA RID: 442
	public class DataGridViewCellValidatingEventArgs : CancelEventArgs
	{
		// Token: 0x06001EB9 RID: 7865 RVA: 0x00090B02 File Offset: 0x0008ED02
		internal DataGridViewCellValidatingEventArgs(int columnIndex, int rowIndex, object formattedValue)
		{
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
			this.formattedValue = formattedValue;
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00090B1F File Offset: 0x0008ED1F
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00090B27 File Offset: 0x0008ED27
		public object FormattedValue
		{
			get
			{
				return this.formattedValue;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x00090B2F File Offset: 0x0008ED2F
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000D0D RID: 3341
		private int rowIndex;

		// Token: 0x04000D0E RID: 3342
		private int columnIndex;

		// Token: 0x04000D0F RID: 3343
		private object formattedValue;
	}
}
