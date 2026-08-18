using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004AD RID: 1197
	internal class IntervalBranchIndex : QueryBranchIndex
	{
		// Token: 0x06002DCC RID: 11724 RVA: 0x000B2990 File Offset: 0x000B0B90
		internal IntervalBranchIndex()
		{
			this.intervalTree = new IntervalTree();
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x000B29A3 File Offset: 0x000B0BA3
		internal override int Count
		{
			get
			{
				return this.intervalTree.Count;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		internal override QueryBranch this[object key]
		{
			get
			{
				Interval interval = this.intervalTree.FindInterval((Interval)key);
				if (interval != null)
				{
					return interval.Branch;
				}
				return null;
			}
			set
			{
				Interval interval = (Interval)key;
				interval.Branch = value;
				this.intervalTree.Add(interval);
			}
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000B2A04 File Offset: 0x000B0C04
		internal override void CollectXPathFilters(ICollection<MessageFilter> filters)
		{
			for (int i = 0; i < this.intervalTree.Intervals.Count; i++)
			{
				this.intervalTree.Intervals[i].Branch.Branch.CollectXPathFilters(filters);
			}
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000B2A50 File Offset: 0x000B0C50
		private void Match(int valIndex, double point, QueryBranchResultSet results)
		{
			IntervalTreeTraverser intervalTreeTraverser = new IntervalTreeTraverser(point, this.intervalTree.Root);
			while (intervalTreeTraverser.MoveNext())
			{
				IntervalCollection slot = intervalTreeTraverser.Slot;
				int i = 0;
				int count = slot.Count;
				while (i < count)
				{
					QueryBranch branch = slot[i].Branch;
					if (branch != null)
					{
						results.Add(branch, valIndex);
					}
					i++;
				}
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000B2AB4 File Offset: 0x000B0CB4
		internal override void Match(int valIndex, ref Value val, QueryBranchResultSet results)
		{
			if (ValueDataType.Sequence == val.Type)
			{
				NodeSequence sequence = val.Sequence;
				for (int i = 0; i < sequence.Count; i++)
				{
					this.Match(valIndex, sequence.Items[i].NumberValue(), results);
				}
				return;
			}
			this.Match(valIndex, val.ToDouble(), results);
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000B2B0A File Offset: 0x000B0D0A
		internal override void Remove(object key)
		{
			this.intervalTree.Remove((Interval)key);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000B2B1D File Offset: 0x000B0D1D
		internal override void Trim()
		{
			this.intervalTree.Trim();
		}

		// Token: 0x040024E6 RID: 9446
		private IntervalTree intervalTree;
	}
}
