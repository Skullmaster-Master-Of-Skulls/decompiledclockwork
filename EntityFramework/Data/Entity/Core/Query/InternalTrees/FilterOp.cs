using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F3 RID: 1523
	internal sealed class FilterOp : RelOp
	{
		// Token: 0x06003C52 RID: 15442 RVA: 0x00119020 File Offset: 0x00117220
		private FilterOp() : base(OpType.Filter)
		{
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x0011902A File Offset: 0x0011722A
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x0011902D File Offset: 0x0011722D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x00119037 File Offset: 0x00117237
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400169B RID: 5787
		internal static readonly FilterOp Instance = new FilterOp();

		// Token: 0x0400169C RID: 5788
		internal static readonly FilterOp Pattern = FilterOp.Instance;
	}
}
