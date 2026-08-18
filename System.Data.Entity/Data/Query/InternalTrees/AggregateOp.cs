using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000FB RID: 251
	internal sealed class AggregateOp : ScalarOp
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x0003CC6F File Offset: 0x0003AE6F
		internal AggregateOp(EdmFunction aggFunc, bool distinctAgg) : base(OpType.Aggregate, aggFunc.ReturnParameter.TypeUsage)
		{
			this.m_aggFunc = aggFunc;
			this.m_distinctAgg = distinctAgg;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003CC92 File Offset: 0x0003AE92
		private AggregateOp() : base(OpType.Aggregate)
		{
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		internal EdmFunction AggFunc
		{
			get
			{
				return this.m_aggFunc;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0003CCA4 File Offset: 0x0003AEA4
		internal bool IsDistinctAggregate
		{
			get
			{
				return this.m_distinctAgg;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00017938 File Offset: 0x00015B38
		internal override bool IsAggregateOp
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003CCAC File Offset: 0x0003AEAC
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0003CCB6 File Offset: 0x0003AEB6
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009B3 RID: 2483
		private EdmFunction m_aggFunc;

		// Token: 0x040009B4 RID: 2484
		private bool m_distinctAgg;

		// Token: 0x040009B5 RID: 2485
		internal static readonly AggregateOp Pattern = new AggregateOp();
	}
}
