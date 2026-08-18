using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000CC RID: 204
	internal sealed class InnerJoinOp : JoinBaseOp
	{
		// Token: 0x06000C3D RID: 3133 RVA: 0x0003BFC3 File Offset: 0x0003A1C3
		private InnerJoinOp() : base(OpType.InnerJoin)
		{
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0003BFCD File Offset: 0x0003A1CD
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0003BFD7 File Offset: 0x0003A1D7
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000967 RID: 2407
		internal static readonly InnerJoinOp Instance = new InnerJoinOp();

		// Token: 0x04000968 RID: 2408
		internal static readonly InnerJoinOp Pattern = InnerJoinOp.Instance;
	}
}
