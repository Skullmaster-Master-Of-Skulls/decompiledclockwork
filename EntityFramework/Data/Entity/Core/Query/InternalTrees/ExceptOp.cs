using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005EF RID: 1519
	internal sealed class ExceptOp : SetOp
	{
		// Token: 0x06003C32 RID: 15410 RVA: 0x00118D06 File Offset: 0x00116F06
		private ExceptOp() : base(OpType.Except)
		{
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x00118D10 File Offset: 0x00116F10
		internal ExceptOp(VarVec outputs, VarMap left, VarMap right) : base(OpType.Except, outputs, left, right)
		{
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x00118D1D File Offset: 0x00116F1D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x00118D27 File Offset: 0x00116F27
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001690 RID: 5776
		internal static readonly ExceptOp Pattern = new ExceptOp();
	}
}
