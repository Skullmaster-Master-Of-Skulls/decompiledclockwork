using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000505 RID: 1285
	internal class GridEntryRecreateChildrenEventArgs : EventArgs
	{
		// Token: 0x06005486 RID: 21638 RVA: 0x00161CC0 File Offset: 0x0015FEC0
		public GridEntryRecreateChildrenEventArgs(int oldCount, int newCount)
		{
			this.OldChildCount = oldCount;
			this.NewChildCount = newCount;
		}

		// Token: 0x0400370B RID: 14091
		public readonly int OldChildCount;

		// Token: 0x0400370C RID: 14092
		public readonly int NewChildCount;
	}
}
