using System;

namespace System.Windows.Forms
{
	// Token: 0x0200015C RID: 348
	public class ColumnWidthChangedEventArgs : EventArgs
	{
		// Token: 0x06000DD1 RID: 3537 RVA: 0x00027C5D File Offset: 0x00025E5D
		public ColumnWidthChangedEventArgs(int columnIndex)
		{
			this.columnIndex = columnIndex;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00027C6C File Offset: 0x00025E6C
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x040007B0 RID: 1968
		private readonly int columnIndex;
	}
}
