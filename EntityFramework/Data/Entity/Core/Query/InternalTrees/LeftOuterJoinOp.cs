using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000600 RID: 1536
	internal sealed class LeftOuterJoinOp : JoinBaseOp
	{
		// Token: 0x06003CA1 RID: 15521 RVA: 0x00119485 File Offset: 0x00117685
		private LeftOuterJoinOp() : base(OpType.LeftOuterJoin)
		{
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x0011948F File Offset: 0x0011768F
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x00119499 File Offset: 0x00117699
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016B1 RID: 5809
		internal static readonly LeftOuterJoinOp Instance = new LeftOuterJoinOp();

		// Token: 0x040016B2 RID: 5810
		internal static readonly LeftOuterJoinOp Pattern = LeftOuterJoinOp.Instance;
	}
}
