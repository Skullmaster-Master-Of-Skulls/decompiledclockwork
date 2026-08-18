using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005FB RID: 1531
	internal sealed class InnerJoinOp : JoinBaseOp
	{
		// Token: 0x06003C81 RID: 15489 RVA: 0x0011923B File Offset: 0x0011743B
		private InnerJoinOp() : base(OpType.InnerJoin)
		{
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x00119245 File Offset: 0x00117445
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x0011924F File Offset: 0x0011744F
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016A8 RID: 5800
		internal static readonly InnerJoinOp Instance = new InnerJoinOp();

		// Token: 0x040016A9 RID: 5801
		internal static readonly InnerJoinOp Pattern = InnerJoinOp.Instance;
	}
}
