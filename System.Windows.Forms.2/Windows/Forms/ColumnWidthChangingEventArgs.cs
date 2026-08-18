using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200015E RID: 350
	public class ColumnWidthChangingEventArgs : CancelEventArgs
	{
		// Token: 0x06000DD7 RID: 3543 RVA: 0x00027C74 File Offset: 0x00025E74
		public ColumnWidthChangingEventArgs(int columnIndex, int newWidth, bool cancel) : base(cancel)
		{
			this.columnIndex = columnIndex;
			this.newWidth = newWidth;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00027C8B File Offset: 0x00025E8B
		public ColumnWidthChangingEventArgs(int columnIndex, int newWidth)
		{
			this.columnIndex = columnIndex;
			this.newWidth = newWidth;
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00027CA1 File Offset: 0x00025EA1
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00027CA9 File Offset: 0x00025EA9
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00027CB1 File Offset: 0x00025EB1
		public int NewWidth
		{
			get
			{
				return this.newWidth;
			}
			set
			{
				this.newWidth = value;
			}
		}

		// Token: 0x040007B1 RID: 1969
		private int columnIndex;

		// Token: 0x040007B2 RID: 1970
		private int newWidth;
	}
}
