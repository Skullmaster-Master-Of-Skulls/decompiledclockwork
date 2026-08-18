using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000DC RID: 220
	internal sealed class UnionAllOp : SetOp
	{
		// Token: 0x06000C8D RID: 3213 RVA: 0x0003C302 File Offset: 0x0003A502
		private UnionAllOp() : base(OpType.UnionAll)
		{
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0003C30C File Offset: 0x0003A50C
		internal UnionAllOp(VarVec outputs, VarMap left, VarMap right, Var branchDiscriminator) : base(OpType.UnionAll, outputs, left, right)
		{
			this.m_branchDiscriminator = branchDiscriminator;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0003C321 File Offset: 0x0003A521
		internal Var BranchDiscriminator
		{
			get
			{
				return this.m_branchDiscriminator;
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003C329 File Offset: 0x0003A529
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003C333 File Offset: 0x0003A533
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000983 RID: 2435
		private Var m_branchDiscriminator;

		// Token: 0x04000984 RID: 2436
		internal static readonly UnionAllOp Pattern = new UnionAllOp();
	}
}
