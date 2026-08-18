using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000159 RID: 345
	public class ColumnReorderedEventArgs : CancelEventArgs
	{
		// Token: 0x06000DC9 RID: 3529 RVA: 0x00027C28 File Offset: 0x00025E28
		public ColumnReorderedEventArgs(int oldDisplayIndex, int newDisplayIndex, ColumnHeader header)
		{
			this.oldDisplayIndex = oldDisplayIndex;
			this.newDisplayIndex = newDisplayIndex;
			this.header = header;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00027C45 File Offset: 0x00025E45
		public int OldDisplayIndex
		{
			get
			{
				return this.oldDisplayIndex;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00027C4D File Offset: 0x00025E4D
		public int NewDisplayIndex
		{
			get
			{
				return this.newDisplayIndex;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00027C55 File Offset: 0x00025E55
		public ColumnHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x040007A9 RID: 1961
		private int oldDisplayIndex;

		// Token: 0x040007AA RID: 1962
		private int newDisplayIndex;

		// Token: 0x040007AB RID: 1963
		private ColumnHeader header;
	}
}
