using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E4 RID: 1508
	internal sealed class CrossJoinOp : JoinBaseOp
	{
		// Token: 0x06003BF9 RID: 15353 RVA: 0x0011899B File Offset: 0x00116B9B
		private CrossJoinOp() : base(OpType.CrossJoin)
		{
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x001189A5 File Offset: 0x00116BA5
		internal override int Arity
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x001189A8 File Offset: 0x00116BA8
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x001189B2 File Offset: 0x00116BB2
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400167D RID: 5757
		internal static readonly CrossJoinOp Instance = new CrossJoinOp();

		// Token: 0x0400167E RID: 5758
		internal static readonly CrossJoinOp Pattern = CrossJoinOp.Instance;
	}
}
