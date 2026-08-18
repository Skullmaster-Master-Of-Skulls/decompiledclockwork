using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E2 RID: 1506
	internal sealed class CrossApplyOp : ApplyBaseOp
	{
		// Token: 0x06003BF3 RID: 15347 RVA: 0x0011895B File Offset: 0x00116B5B
		private CrossApplyOp() : base(OpType.CrossApply)
		{
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x00118965 File Offset: 0x00116B65
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x0011896F File Offset: 0x00116B6F
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400167B RID: 5755
		internal static readonly CrossApplyOp Instance = new CrossApplyOp();

		// Token: 0x0400167C RID: 5756
		internal static readonly CrossApplyOp Pattern = CrossApplyOp.Instance;
	}
}
