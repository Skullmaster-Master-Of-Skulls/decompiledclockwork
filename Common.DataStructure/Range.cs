using System;

namespace TechnoPro.Common.DataStructure
{
	// Token: 0x02000008 RID: 8
	public class Range<T>
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002050 File Offset: 0x00000250
		public Range()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000261A File Offset: 0x0000081A
		public Range(T start, T end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002630 File Offset: 0x00000830
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002638 File Offset: 0x00000838
		public T Start { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002641 File Offset: 0x00000841
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002649 File Offset: 0x00000849
		public T End { get; set; }
	}
}
