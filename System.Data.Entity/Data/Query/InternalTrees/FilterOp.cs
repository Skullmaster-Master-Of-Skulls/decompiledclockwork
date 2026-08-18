using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D2 RID: 210
	internal sealed class FilterOp : RelOp
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x0003C0C7 File Offset: 0x0003A2C7
		private FilterOp() : base(OpType.Filter)
		{
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00033532 File Offset: 0x00031732
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0003C0D1 File Offset: 0x0003A2D1
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0003C0DB File Offset: 0x0003A2DB
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000971 RID: 2417
		internal static readonly FilterOp Instance = new FilterOp();

		// Token: 0x04000972 RID: 2418
		internal static readonly FilterOp Pattern = FilterOp.Instance;
	}
}
