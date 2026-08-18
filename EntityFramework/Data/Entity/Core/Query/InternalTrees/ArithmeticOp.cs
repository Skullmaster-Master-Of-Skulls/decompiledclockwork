using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005CC RID: 1484
	internal sealed class ArithmeticOp : ScalarOp
	{
		// Token: 0x06003B3B RID: 15163 RVA: 0x00117FD4 File Offset: 0x001161D4
		internal ArithmeticOp(OpType opType, TypeUsage type) : base(opType, type)
		{
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x00117FDE File Offset: 0x001161DE
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x00117FE8 File Offset: 0x001161E8
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}
	}
}
