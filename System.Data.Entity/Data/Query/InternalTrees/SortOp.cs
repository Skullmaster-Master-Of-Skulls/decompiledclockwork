using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D6 RID: 214
	internal sealed class SortOp : SortBaseOp
	{
		// Token: 0x06000C67 RID: 3175 RVA: 0x0003C192 File Offset: 0x0003A392
		private SortOp() : base(OpType.Sort)
		{
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0003C19C File Offset: 0x0003A39C
		internal SortOp(List<SortKey> sortKeys) : base(OpType.Sort, sortKeys)
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0003C1A7 File Offset: 0x0003A3A7
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0003C1B1 File Offset: 0x0003A3B1
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000979 RID: 2425
		internal static readonly SortOp Pattern = new SortOp();
	}
}
