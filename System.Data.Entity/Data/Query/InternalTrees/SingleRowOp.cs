using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000E0 RID: 224
	internal sealed class SingleRowOp : RelOp
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003C3F8 File Offset: 0x0003A5F8
		private SingleRowOp() : base(OpType.SingleRow)
		{
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0003C402 File Offset: 0x0003A602
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0003C40C File Offset: 0x0003A60C
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000989 RID: 2441
		internal static readonly SingleRowOp Instance = new SingleRowOp();

		// Token: 0x0400098A RID: 2442
		internal static readonly SingleRowOp Pattern = SingleRowOp.Instance;
	}
}
