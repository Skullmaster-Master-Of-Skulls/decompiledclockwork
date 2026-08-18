using System;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001FF RID: 511
	public class CollectionChangeEventArgs<T> : EventArgs
	{
		// Token: 0x06001CEA RID: 7402 RVA: 0x000F9680 File Offset: 0x000F8680
		private CollectionChangeEventArgs()
		{
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000F9694 File Offset: 0x000F8694
		public CollectionChangeEventArgs(int index, T value)
		{
			this.ᜀ = index;
			this.ᜁ = value;
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x000F96B8 File Offset: 0x000F86B8
		public int Index
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x000F96FC File Offset: 0x000F86FC
		public T Value
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜁ;
			}
		}

		// Token: 0x0400109F RID: 4255
		private int \u25D9\u0083\u0082\u009E;

		// Token: 0x040010A0 RID: 4256
		private string[] \u2593\u00AD\u0093\u00AE;

		// Token: 0x040010A1 RID: 4257
		private int ᜀ;

		// Token: 0x040010A2 RID: 4258
		private byte \u2609\u009C\u0084\u0087;

		// Token: 0x040010A3 RID: 4259
		private bool[] \u2609\u0088\u00AD\u008D;

		// Token: 0x040010A4 RID: 4260
		private T ᜁ;
	}
}
