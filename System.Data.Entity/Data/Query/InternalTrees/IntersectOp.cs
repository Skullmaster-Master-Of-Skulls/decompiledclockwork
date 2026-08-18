using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000DD RID: 221
	internal sealed class IntersectOp : SetOp
	{
		// Token: 0x06000C93 RID: 3219 RVA: 0x0003C349 File Offset: 0x0003A549
		private IntersectOp() : base(OpType.Intersect)
		{
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003C353 File Offset: 0x0003A553
		internal IntersectOp(VarVec outputs, VarMap left, VarMap right) : base(OpType.Intersect, outputs, left, right)
		{
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0003C360 File Offset: 0x0003A560
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003C36A File Offset: 0x0003A56A
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000985 RID: 2437
		internal static readonly IntersectOp Pattern = new IntersectOp();
	}
}
