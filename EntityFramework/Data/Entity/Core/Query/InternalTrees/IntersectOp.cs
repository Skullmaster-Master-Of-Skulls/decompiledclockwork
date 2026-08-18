using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005FD RID: 1533
	internal sealed class IntersectOp : SetOp
	{
		// Token: 0x06003C8A RID: 15498 RVA: 0x001192A3 File Offset: 0x001174A3
		private IntersectOp() : base(OpType.Intersect)
		{
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x001192AD File Offset: 0x001174AD
		internal IntersectOp(VarVec outputs, VarMap left, VarMap right) : base(OpType.Intersect, outputs, left, right)
		{
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x001192BA File Offset: 0x001174BA
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x001192C4 File Offset: 0x001174C4
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016AB RID: 5803
		internal static readonly IntersectOp Pattern = new IntersectOp();
	}
}
