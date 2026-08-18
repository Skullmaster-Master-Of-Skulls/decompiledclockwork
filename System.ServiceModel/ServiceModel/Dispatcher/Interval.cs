using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004A6 RID: 1190
	internal class Interval
	{
		// Token: 0x06002D81 RID: 11649 RVA: 0x000B1EDC File Offset: 0x000B00DC
		internal Interval(double literal, RelationOperator op)
		{
			this.lowerBound = double.MinValue;
			this.upperBound = double.MaxValue;
			this.lowerOp = IntervalOp.LessThanEquals;
			this.upperOp = IntervalOp.LessThanEquals;
			switch (op)
			{
			case RelationOperator.Gt:
				this.lowerBound = literal;
				this.lowerOp = IntervalOp.LessThan;
				return;
			case RelationOperator.Ge:
				this.lowerBound = literal;
				return;
			case RelationOperator.Lt:
				this.upperBound = literal;
				this.upperOp = IntervalOp.LessThan;
				return;
			case RelationOperator.Le:
				this.upperBound = literal;
				return;
			default:
				return;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000B1F61 File Offset: 0x000B0161
		// (set) Token: 0x06002D83 RID: 11651 RVA: 0x000B1F69 File Offset: 0x000B0169
		internal QueryBranch Branch
		{
			get
			{
				return this.branch;
			}
			set
			{
				this.branch = value;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x000B1F72 File Offset: 0x000B0172
		internal double LowerBound
		{
			get
			{
				return this.lowerBound;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x000B1F7A File Offset: 0x000B017A
		internal IntervalOp LowerOp
		{
			get
			{
				return this.lowerOp;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000B1F82 File Offset: 0x000B0182
		internal double UpperBound
		{
			get
			{
				return this.upperBound;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x000B1F8A File Offset: 0x000B018A
		internal IntervalOp UpperOp
		{
			get
			{
				return this.upperOp;
			}
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000B1F92 File Offset: 0x000B0192
		internal bool Equals(double lowerBound, IntervalOp lowerOp, double upperBound, IntervalOp upperOp)
		{
			return this.lowerBound == lowerBound && this.lowerOp == lowerOp && this.upperBound == upperBound && this.upperOp == upperOp;
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000B1FBB File Offset: 0x000B01BB
		internal bool HasMatchingEndPoint(double endpoint)
		{
			return this.lowerBound == endpoint || this.upperBound == endpoint;
		}

		// Token: 0x040024D1 RID: 9425
		private QueryBranch branch;

		// Token: 0x040024D2 RID: 9426
		private double lowerBound;

		// Token: 0x040024D3 RID: 9427
		private IntervalOp lowerOp;

		// Token: 0x040024D4 RID: 9428
		private double upperBound;

		// Token: 0x040024D5 RID: 9429
		private IntervalOp upperOp;
	}
}
