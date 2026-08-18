using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200060A RID: 1546
	internal sealed class NullOp : ConstantBaseOp
	{
		// Token: 0x06003CE9 RID: 15593 RVA: 0x0011AAD0 File Offset: 0x00118CD0
		internal NullOp(TypeUsage type) : base(OpType.Null, type, null)
		{
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x0011AADB File Offset: 0x00118CDB
		private NullOp() : base(OpType.Null)
		{
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x0011AAE4 File Offset: 0x00118CE4
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x0011AAEE File Offset: 0x00118CEE
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016C0 RID: 5824
		internal static readonly NullOp Pattern = new NullOp();
	}
}
