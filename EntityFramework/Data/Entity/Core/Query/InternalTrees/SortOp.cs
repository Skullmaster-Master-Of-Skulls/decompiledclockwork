using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200062B RID: 1579
	internal sealed class SortOp : SortBaseOp
	{
		// Token: 0x06003D7F RID: 15743 RVA: 0x0011B3AC File Offset: 0x001195AC
		private SortOp() : base(OpType.Sort)
		{
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x0011B3B6 File Offset: 0x001195B6
		internal SortOp(List<SortKey> sortKeys) : base(OpType.Sort, sortKeys)
		{
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x0011B3C1 File Offset: 0x001195C1
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x0011B3C4 File Offset: 0x001195C4
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x0011B3CE File Offset: 0x001195CE
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400173A RID: 5946
		internal static readonly SortOp Pattern = new SortOp();
	}
}
