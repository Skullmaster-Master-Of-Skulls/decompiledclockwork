using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F9 RID: 249
	internal sealed class ConditionalOp : ScalarOp
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x0003CB93 File Offset: 0x0003AD93
		internal ConditionalOp(OpType optype, TypeUsage type) : base(optype, type)
		{
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003C7C9 File Offset: 0x0003A9C9
		private ConditionalOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003CBF4 File Offset: 0x0003ADF4
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003CBFE File Offset: 0x0003ADFE
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009AE RID: 2478
		internal static readonly ConditionalOp PatternAnd = new ConditionalOp(OpType.And);

		// Token: 0x040009AF RID: 2479
		internal static readonly ConditionalOp PatternOr = new ConditionalOp(OpType.Or);

		// Token: 0x040009B0 RID: 2480
		internal static readonly ConditionalOp PatternNot = new ConditionalOp(OpType.Not);

		// Token: 0x040009B1 RID: 2481
		internal static readonly ConditionalOp PatternIsNull = new ConditionalOp(OpType.IsNull);
	}
}
