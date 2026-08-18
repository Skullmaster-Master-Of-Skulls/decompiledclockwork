using System;

namespace System.Windows.Forms
{
	// Token: 0x02000215 RID: 533
	public class DataGridViewRowsAddedEventArgs : EventArgs
	{
		// Token: 0x060022D2 RID: 8914 RVA: 0x000A712D File Offset: 0x000A532D
		public DataGridViewRowsAddedEventArgs(int rowIndex, int rowCount)
		{
			this.rowIndex = rowIndex;
			this.rowCount = rowCount;
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x000A7143 File Offset: 0x000A5343
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x000A714B File Offset: 0x000A534B
		public int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x04000E66 RID: 3686
		private int rowIndex;

		// Token: 0x04000E67 RID: 3687
		private int rowCount;
	}
}
