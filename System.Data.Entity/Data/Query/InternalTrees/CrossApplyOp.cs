using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D0 RID: 208
	internal sealed class CrossApplyOp : ApplyBaseOp
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x0003C05F File Offset: 0x0003A25F
		private CrossApplyOp() : base(OpType.CrossApply)
		{
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0003C069 File Offset: 0x0003A269
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0003C073 File Offset: 0x0003A273
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400096D RID: 2413
		internal static readonly CrossApplyOp Instance = new CrossApplyOp();

		// Token: 0x0400096E RID: 2414
		internal static readonly CrossApplyOp Pattern = CrossApplyOp.Instance;
	}
}
