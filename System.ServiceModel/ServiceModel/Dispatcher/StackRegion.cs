using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004FE RID: 1278
	internal struct StackRegion
	{
		// Token: 0x0600304E RID: 12366 RVA: 0x000B8D5A File Offset: 0x000B6F5A
		internal StackRegion(QueryRange bounds)
		{
			this.bounds = bounds;
			this.stackPtr = bounds.start - 1;
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x000B8D71 File Offset: 0x000B6F71
		internal int Count
		{
			get
			{
				return this.stackPtr - this.bounds.start + 1;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000B8D87 File Offset: 0x000B6F87
		internal bool NeedsGrowth
		{
			get
			{
				return this.stackPtr > this.bounds.end;
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000B8D9C File Offset: 0x000B6F9C
		internal void Clear()
		{
			this.stackPtr = this.bounds.start - 1;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000B8DB1 File Offset: 0x000B6FB1
		internal void Grow(int growBy)
		{
			this.bounds.end = this.bounds.end + growBy;
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000B8DC3 File Offset: 0x000B6FC3
		internal bool IsValidStackPtr()
		{
			return this.bounds.IsInRange(this.stackPtr);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000B8DD6 File Offset: 0x000B6FD6
		internal bool IsValidStackPtr(int stackPtr)
		{
			return this.bounds.IsInRange(stackPtr);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000B8DE4 File Offset: 0x000B6FE4
		internal void Shift(int shiftBy)
		{
			this.bounds.Shift(shiftBy);
			this.stackPtr += shiftBy;
		}

		// Token: 0x040025F9 RID: 9721
		internal QueryRange bounds;

		// Token: 0x040025FA RID: 9722
		internal int stackPtr;
	}
}
