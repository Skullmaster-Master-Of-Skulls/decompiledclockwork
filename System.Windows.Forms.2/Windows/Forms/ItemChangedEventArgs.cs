using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A8 RID: 680
	public class ItemChangedEventArgs : EventArgs
	{
		// Token: 0x06002A3F RID: 10815 RVA: 0x000BFB62 File Offset: 0x000BDD62
		internal ItemChangedEventArgs(int index)
		{
			this.index = index;
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000BFB71 File Offset: 0x000BDD71
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x0400112E RID: 4398
		private int index;
	}
}
