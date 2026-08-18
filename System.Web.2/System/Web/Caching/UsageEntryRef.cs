using System;

namespace System.Web.Caching
{
	// Token: 0x020008A0 RID: 2208
	internal struct UsageEntryRef
	{
		// Token: 0x06006767 RID: 26471 RVA: 0x0016E7C9 File Offset: 0x0016C9C9
		internal UsageEntryRef(int pageIndex, int entryIndex)
		{
			this._ref = (uint)(pageIndex << 8 | (entryIndex & 255));
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x0016E7DC File Offset: 0x0016C9DC
		public override bool Equals(object value)
		{
			return value is UsageEntryRef && this._ref == ((UsageEntryRef)value)._ref;
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x0016E7FB File Offset: 0x0016C9FB
		public static bool operator ==(UsageEntryRef r1, UsageEntryRef r2)
		{
			return r1._ref == r2._ref;
		}

		// Token: 0x0600676A RID: 26474 RVA: 0x0016E80B File Offset: 0x0016CA0B
		public static bool operator !=(UsageEntryRef r1, UsageEntryRef r2)
		{
			return r1._ref != r2._ref;
		}

		// Token: 0x0600676B RID: 26475 RVA: 0x0016E81E File Offset: 0x0016CA1E
		public override int GetHashCode()
		{
			return (int)this._ref;
		}

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x0600676C RID: 26476 RVA: 0x0016E828 File Offset: 0x0016CA28
		internal int PageIndex
		{
			get
			{
				return (int)(this._ref >> 8);
			}
		}

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x0600676D RID: 26477 RVA: 0x0016E840 File Offset: 0x0016CA40
		internal int Ref1Index
		{
			get
			{
				return (int)((sbyte)(this._ref & 255U));
			}
		}

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x0600676E RID: 26478 RVA: 0x0016E85C File Offset: 0x0016CA5C
		internal int Ref2Index
		{
			get
			{
				int num = (int)((sbyte)(this._ref & 255U));
				return -num;
			}
		}

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x0600676F RID: 26479 RVA: 0x0016E879 File Offset: 0x0016CA79
		internal bool IsRef1
		{
			get
			{
				return (sbyte)(this._ref & 255U) > 0;
			}
		}

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x06006770 RID: 26480 RVA: 0x0016E88B File Offset: 0x0016CA8B
		internal bool IsRef2
		{
			get
			{
				return (sbyte)(this._ref & 255U) < 0;
			}
		}

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x06006771 RID: 26481 RVA: 0x0016E89D File Offset: 0x0016CA9D
		internal bool IsInvalid
		{
			get
			{
				return this._ref == 0U;
			}
		}

		// Token: 0x04003588 RID: 13704
		internal static readonly UsageEntryRef INVALID = new UsageEntryRef(0, 0);

		// Token: 0x04003589 RID: 13705
		private const uint ENTRY_MASK = 255U;

		// Token: 0x0400358A RID: 13706
		private const uint PAGE_MASK = 4294967040U;

		// Token: 0x0400358B RID: 13707
		private const int PAGE_SHIFT = 8;

		// Token: 0x0400358C RID: 13708
		private uint _ref;
	}
}
