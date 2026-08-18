using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200062F RID: 1583
	internal sealed class UnionAllOp : SetOp
	{
		// Token: 0x06003D97 RID: 15767 RVA: 0x0011B60E File Offset: 0x0011980E
		private UnionAllOp() : base(OpType.UnionAll)
		{
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x0011B618 File Offset: 0x00119818
		internal UnionAllOp(VarVec outputs, VarMap left, VarMap right, Var branchDiscriminator) : base(OpType.UnionAll, outputs, left, right)
		{
			this.m_branchDiscriminator = branchDiscriminator;
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x0011B62D File Offset: 0x0011982D
		internal Var BranchDiscriminator
		{
			get
			{
				return this.m_branchDiscriminator;
			}
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x0011B635 File Offset: 0x00119835
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x0011B63F File Offset: 0x0011983F
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001745 RID: 5957
		private readonly Var m_branchDiscriminator;

		// Token: 0x04001746 RID: 5958
		internal static readonly UnionAllOp Pattern = new UnionAllOp();
	}
}
