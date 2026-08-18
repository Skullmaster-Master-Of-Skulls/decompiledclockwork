using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D7 RID: 215
	internal sealed class ConstrainedSortOp : SortBaseOp
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x0003C1C7 File Offset: 0x0003A3C7
		private ConstrainedSortOp() : base(OpType.ConstrainedSort)
		{
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0003C1D1 File Offset: 0x0003A3D1
		internal ConstrainedSortOp(List<SortKey> sortKeys, bool withTies) : base(OpType.ConstrainedSort, sortKeys)
		{
			this._withTies = withTies;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0003C1E3 File Offset: 0x0003A3E3
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x0003C1EB File Offset: 0x0003A3EB
		internal bool WithTies
		{
			get
			{
				return this._withTies;
			}
			set
			{
				this._withTies = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0003BF8C File Offset: 0x0003A18C
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0003C1F4 File Offset: 0x0003A3F4
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0003C1FE File Offset: 0x0003A3FE
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400097A RID: 2426
		private bool _withTies;

		// Token: 0x0400097B RID: 2427
		internal static readonly ConstrainedSortOp Pattern = new ConstrainedSortOp();
	}
}
