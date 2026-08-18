using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000DE RID: 222
	internal sealed class ExceptOp : SetOp
	{
		// Token: 0x06000C98 RID: 3224 RVA: 0x0003C380 File Offset: 0x0003A580
		private ExceptOp() : base(OpType.Except)
		{
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0003C38A File Offset: 0x0003A58A
		internal ExceptOp(VarVec outputs, VarMap left, VarMap right) : base(OpType.Except, outputs, left, right)
		{
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0003C397 File Offset: 0x0003A597
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0003C3A1 File Offset: 0x0003A5A1
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000986 RID: 2438
		internal static readonly ExceptOp Pattern = new ExceptOp();
	}
}
