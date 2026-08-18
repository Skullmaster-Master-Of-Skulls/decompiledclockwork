using System;

namespace Microsoft.Practices.Unity.Utility
{
	// Token: 0x020000AA RID: 170
	public class Pair<TFirst, TSecond>
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000AEB4 File Offset: 0x000090B4
		public Pair(TFirst first, TSecond second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000AECA File Offset: 0x000090CA
		public TFirst First
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000AED2 File Offset: 0x000090D2
		public TSecond Second
		{
			get
			{
				return this.second;
			}
		}

		// Token: 0x040000C1 RID: 193
		private TFirst first;

		// Token: 0x040000C2 RID: 194
		private TSecond second;
	}
}
