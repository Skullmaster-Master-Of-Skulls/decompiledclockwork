using System;

namespace TechnoPro.Common.DataStructure
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class Pair<S, T>
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002050 File Offset: 0x00000250
		public Pair()
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025E2 File Offset: 0x000007E2
		public Pair(S item1, T item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000025F8 File Offset: 0x000007F8
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002600 File Offset: 0x00000800
		public S Item1 { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002609 File Offset: 0x00000809
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002611 File Offset: 0x00000811
		public T Item2 { get; set; }
	}
}
