using System;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000DD RID: 221
	internal class RoundRobin
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x0005E040 File Offset: 0x0005C240
		public RoundRobin()
		{
			this.m_mod = 1;
			this.m_count = 0;
			this.m_sync = new object();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0005E064 File Offset: 0x0005C264
		public int NextValue()
		{
			int count;
			lock (this.m_sync)
			{
				this.m_count++;
				if (this.m_count >= this.m_mod)
				{
					this.m_count = 0;
				}
				count = this.m_count;
			}
			return count;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0005E0CC File Offset: 0x0005C2CC
		public int NextValue(int val)
		{
			if (val != 2147483647)
			{
				return ++val % this.m_mod;
			}
			return 0;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0005E0E8 File Offset: 0x0005C2E8
		public void SetMax(int exclusiveMax)
		{
			lock (this.m_sync)
			{
				if (this.m_mod > 0)
				{
					this.m_mod = exclusiveMax;
				}
				else
				{
					this.m_mod = 1;
				}
			}
		}

		// Token: 0x04000BA9 RID: 2985
		private object m_sync;

		// Token: 0x04000BAA RID: 2986
		private int m_mod;

		// Token: 0x04000BAB RID: 2987
		private int m_count;
	}
}
