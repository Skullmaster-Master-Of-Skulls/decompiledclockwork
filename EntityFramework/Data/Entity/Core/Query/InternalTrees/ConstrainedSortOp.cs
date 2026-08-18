using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E1 RID: 1505
	internal sealed class ConstrainedSortOp : SortBaseOp
	{
		// Token: 0x06003BEB RID: 15339 RVA: 0x0011890B File Offset: 0x00116B0B
		private ConstrainedSortOp() : base(OpType.ConstrainedSort)
		{
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x00118915 File Offset: 0x00116B15
		internal ConstrainedSortOp(List<SortKey> sortKeys, bool withTies) : base(OpType.ConstrainedSort, sortKeys)
		{
			this.WithTies = withTies;
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x00118927 File Offset: 0x00116B27
		// (set) Token: 0x06003BEE RID: 15342 RVA: 0x0011892F File Offset: 0x00116B2F
		internal bool WithTies { get; set; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x00118938 File Offset: 0x00116B38
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x0011893B File Offset: 0x00116B3B
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x00118945 File Offset: 0x00116B45
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001679 RID: 5753
		internal static readonly ConstrainedSortOp Pattern = new ConstrainedSortOp();
	}
}
