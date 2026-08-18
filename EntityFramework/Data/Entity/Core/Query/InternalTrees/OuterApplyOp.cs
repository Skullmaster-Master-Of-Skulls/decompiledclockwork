using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200060E RID: 1550
	internal sealed class OuterApplyOp : ApplyBaseOp
	{
		// Token: 0x06003CF7 RID: 15607 RVA: 0x0011AB38 File Offset: 0x00118D38
		private OuterApplyOp() : base(OpType.OuterApply)
		{
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x0011AB42 File Offset: 0x00118D42
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x0011AB4C File Offset: 0x00118D4C
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400170E RID: 5902
		internal static readonly OuterApplyOp Instance = new OuterApplyOp();

		// Token: 0x0400170F RID: 5903
		internal static readonly OuterApplyOp Pattern = OuterApplyOp.Instance;
	}
}
