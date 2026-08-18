using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000488 RID: 1160
	internal class QueryBranchResultSet
	{
		// Token: 0x06002CE5 RID: 11493 RVA: 0x000AF12B File Offset: 0x000AD32B
		internal QueryBranchResultSet() : this(2)
		{
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000AF134 File Offset: 0x000AD334
		internal QueryBranchResultSet(int capacity)
		{
			this.results = new QueryBuffer<QueryBranchResult>(capacity);
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000AF148 File Offset: 0x000AD348
		internal int Count
		{
			get
			{
				return this.results.count;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		internal QueryBranchResult this[int index]
		{
			get
			{
				return this.results[index];
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x000AF163 File Offset: 0x000AD363
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x000AF16B File Offset: 0x000AD36B
		internal QueryBranchResultSet Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000AF174 File Offset: 0x000AD374
		internal void Add(QueryBranch branch, int valIndex)
		{
			this.results.Add(new QueryBranchResult(branch, valIndex));
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000AF188 File Offset: 0x000AD388
		internal void Clear()
		{
			this.results.count = 0;
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000AF196 File Offset: 0x000AD396
		internal void Sort()
		{
			this.results.Sort(QueryBranchResultSet.comparer);
		}

		// Token: 0x04002457 RID: 9303
		private QueryBuffer<QueryBranchResult> results;

		// Token: 0x04002458 RID: 9304
		private QueryBranchResultSet next;

		// Token: 0x04002459 RID: 9305
		internal static QueryBranchResultSet.SortComparer comparer = new QueryBranchResultSet.SortComparer();

		// Token: 0x02000C47 RID: 3143
		internal class SortComparer : IComparer<QueryBranchResult>
		{
			// Token: 0x0600776C RID: 30572 RVA: 0x001BDF18 File Offset: 0x001BC118
			public bool Equals(QueryBranchResult x, QueryBranchResult y)
			{
				return x.branch.id == y.branch.id;
			}

			// Token: 0x0600776D RID: 30573 RVA: 0x001BDF32 File Offset: 0x001BC132
			public int Compare(QueryBranchResult x, QueryBranchResult y)
			{
				return x.branch.id - y.branch.id;
			}

			// Token: 0x0600776E RID: 30574 RVA: 0x001BDF4B File Offset: 0x001BC14B
			public int GetHashCode(QueryBranchResult obj)
			{
				return obj.branch.id;
			}
		}
	}
}
