using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000506 RID: 1286
	internal struct QueryRange
	{
		// Token: 0x060030A0 RID: 12448 RVA: 0x000BA7F6 File Offset: 0x000B89F6
		internal QueryRange(int start, int end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000BA806 File Offset: 0x000B8A06
		internal int Count
		{
			get
			{
				return this.end - this.start + 1;
			}
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000BA817 File Offset: 0x000B8A17
		internal bool IsInRange(int point)
		{
			return this.start <= point && point <= this.end;
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000BA830 File Offset: 0x000B8A30
		internal void Shift(int offset)
		{
			this.start += offset;
			this.end += offset;
		}

		// Token: 0x0400260E RID: 9742
		internal int end;

		// Token: 0x0400260F RID: 9743
		internal int start;
	}
}
