using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000CD RID: 205
	internal sealed class LeftOuterJoinOp : JoinBaseOp
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x0003BFF7 File Offset: 0x0003A1F7
		private LeftOuterJoinOp() : base(OpType.LeftOuterJoin)
		{
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0003C001 File Offset: 0x0003A201
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0003C00B File Offset: 0x0003A20B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000969 RID: 2409
		internal static readonly LeftOuterJoinOp Instance = new LeftOuterJoinOp();

		// Token: 0x0400096A RID: 2410
		internal static readonly LeftOuterJoinOp Pattern = LeftOuterJoinOp.Instance;
	}
}
