using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000104 RID: 260
	internal sealed class ArithmeticOp : ScalarOp
	{
		// Token: 0x06000D6B RID: 3435 RVA: 0x0003CB93 File Offset: 0x0003AD93
		internal ArithmeticOp(OpType opType, TypeUsage type) : base(opType, type)
		{
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0003CF0C File Offset: 0x0003B10C
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0003CF16 File Offset: 0x0003B116
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}
	}
}
