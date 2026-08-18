using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200063F RID: 1599
	internal sealed class SingleRowTableOp : RelOp
	{
		// Token: 0x06003EC8 RID: 16072 RVA: 0x0011FB07 File Offset: 0x0011DD07
		private SingleRowTableOp() : base(OpType.SingleRowTable)
		{
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x0011FB11 File Offset: 0x0011DD11
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x0011FB14 File Offset: 0x0011DD14
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x0011FB1E File Offset: 0x0011DD1E
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400177A RID: 6010
		internal static readonly SingleRowTableOp Instance = new SingleRowTableOp();

		// Token: 0x0400177B RID: 6011
		internal static readonly SingleRowTableOp Pattern = SingleRowTableOp.Instance;
	}
}
