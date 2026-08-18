using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005C8 RID: 1480
	internal sealed class AggregateOp : ScalarOp
	{
		// Token: 0x06003B2D RID: 15149 RVA: 0x00117F50 File Offset: 0x00116150
		internal AggregateOp(EdmFunction aggFunc, bool distinctAgg) : base(OpType.Aggregate, aggFunc.ReturnParameter.TypeUsage)
		{
			this.m_aggFunc = aggFunc;
			this.m_distinctAgg = distinctAgg;
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x00117F73 File Offset: 0x00116173
		private AggregateOp() : base(OpType.Aggregate)
		{
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x00117F7D File Offset: 0x0011617D
		internal EdmFunction AggFunc
		{
			get
			{
				return this.m_aggFunc;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x00117F85 File Offset: 0x00116185
		internal bool IsDistinctAggregate
		{
			get
			{
				return this.m_distinctAgg;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x00117F8D File Offset: 0x0011618D
		internal override bool IsAggregateOp
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x00117F90 File Offset: 0x00116190
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x00117F9A File Offset: 0x0011619A
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001652 RID: 5714
		private readonly EdmFunction m_aggFunc;

		// Token: 0x04001653 RID: 5715
		private readonly bool m_distinctAgg;

		// Token: 0x04001654 RID: 5716
		internal static readonly AggregateOp Pattern = new AggregateOp();
	}
}
