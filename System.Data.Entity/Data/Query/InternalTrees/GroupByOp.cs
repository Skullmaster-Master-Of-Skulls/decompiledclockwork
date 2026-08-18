using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D9 RID: 217
	internal sealed class GroupByOp : GroupByBaseOp
	{
		// Token: 0x06000C7B RID: 3195 RVA: 0x0003C245 File Offset: 0x0003A445
		private GroupByOp() : base(OpType.GroupBy)
		{
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0003C24F File Offset: 0x0003A44F
		internal GroupByOp(VarVec keys, VarVec outputs) : base(OpType.GroupBy, keys, outputs)
		{
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0003BF8C File Offset: 0x0003A18C
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0003C25B File Offset: 0x0003A45B
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0003C265 File Offset: 0x0003A465
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400097E RID: 2430
		internal static readonly GroupByOp Pattern = new GroupByOp();
	}
}
