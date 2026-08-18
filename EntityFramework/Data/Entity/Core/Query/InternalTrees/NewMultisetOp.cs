using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000607 RID: 1543
	internal sealed class NewMultisetOp : ScalarOp
	{
		// Token: 0x06003CC2 RID: 15554 RVA: 0x001196C1 File Offset: 0x001178C1
		internal NewMultisetOp(TypeUsage type) : base(OpType.NewMultiset, type)
		{
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x001196CC File Offset: 0x001178CC
		private NewMultisetOp() : base(OpType.NewMultiset)
		{
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x001196D6 File Offset: 0x001178D6
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x001196E0 File Offset: 0x001178E0
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016BC RID: 5820
		internal static readonly NewMultisetOp Pattern = new NewMultisetOp();
	}
}
