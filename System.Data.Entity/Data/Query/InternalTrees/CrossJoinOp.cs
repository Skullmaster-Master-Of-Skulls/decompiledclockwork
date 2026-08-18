using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000CB RID: 203
	internal sealed class CrossJoinOp : JoinBaseOp
	{
		// Token: 0x06000C38 RID: 3128 RVA: 0x0003BF8F File Offset: 0x0003A18F
		private CrossJoinOp() : base(OpType.CrossJoin)
		{
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0003BCE8 File Offset: 0x00039EE8
		internal override int Arity
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0003BF99 File Offset: 0x0003A199
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0003BFA3 File Offset: 0x0003A1A3
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000965 RID: 2405
		internal static readonly CrossJoinOp Instance = new CrossJoinOp();

		// Token: 0x04000966 RID: 2406
		internal static readonly CrossJoinOp Pattern = CrossJoinOp.Instance;
	}
}
