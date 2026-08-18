using System;

namespace System.Windows.Forms
{
	// Token: 0x02000154 RID: 340
	public class ColumnClickEventArgs : EventArgs
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x0002736B File Offset: 0x0002556B
		public ColumnClickEventArgs(int column)
		{
			this.column = column;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0002737A File Offset: 0x0002557A
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x0400079A RID: 1946
		private readonly int column;
	}
}
