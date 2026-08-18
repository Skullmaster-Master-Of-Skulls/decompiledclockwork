using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F4 RID: 1524
	internal sealed class FullOuterJoinOp : JoinBaseOp
	{
		// Token: 0x06003C57 RID: 15447 RVA: 0x00119057 File Offset: 0x00117257
		private FullOuterJoinOp() : base(OpType.FullOuterJoin)
		{
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x00119061 File Offset: 0x00117261
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x0011906B File Offset: 0x0011726B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400169D RID: 5789
		internal static readonly FullOuterJoinOp Instance = new FullOuterJoinOp();

		// Token: 0x0400169E RID: 5790
		internal static readonly FullOuterJoinOp Pattern = FullOuterJoinOp.Instance;
	}
}
