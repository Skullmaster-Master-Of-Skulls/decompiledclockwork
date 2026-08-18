using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000CE RID: 206
	internal sealed class FullOuterJoinOp : JoinBaseOp
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x0003C02B File Offset: 0x0003A22B
		private FullOuterJoinOp() : base(OpType.FullOuterJoin)
		{
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0003C035 File Offset: 0x0003A235
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0003C03F File Offset: 0x0003A23F
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400096B RID: 2411
		internal static readonly FullOuterJoinOp Instance = new FullOuterJoinOp();

		// Token: 0x0400096C RID: 2412
		internal static readonly FullOuterJoinOp Pattern = FullOuterJoinOp.Instance;
	}
}
