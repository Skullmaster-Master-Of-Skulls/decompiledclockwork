using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000103 RID: 259
	internal sealed class NewMultisetOp : ScalarOp
	{
		// Token: 0x06000D66 RID: 3430 RVA: 0x0003CED7 File Offset: 0x0003B0D7
		internal NewMultisetOp(TypeUsage type) : base(OpType.NewMultiset, type)
		{
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003CEE2 File Offset: 0x0003B0E2
		private NewMultisetOp() : base(OpType.NewMultiset)
		{
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0003CEEC File Offset: 0x0003B0EC
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0003CEF6 File Offset: 0x0003B0F6
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009C2 RID: 2498
		internal static readonly NewMultisetOp Pattern = new NewMultisetOp();
	}
}
