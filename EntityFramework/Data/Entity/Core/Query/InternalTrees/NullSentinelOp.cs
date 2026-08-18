using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200060B RID: 1547
	internal sealed class NullSentinelOp : ConstantBaseOp
	{
		// Token: 0x06003CEE RID: 15598 RVA: 0x0011AB04 File Offset: 0x00118D04
		internal NullSentinelOp(TypeUsage type, object value) : base(OpType.NullSentinel, type, value)
		{
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x0011AB0F File Offset: 0x00118D0F
		private NullSentinelOp() : base(OpType.NullSentinel)
		{
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x0011AB18 File Offset: 0x00118D18
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x0011AB22 File Offset: 0x00118D22
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016C1 RID: 5825
		internal static readonly NullSentinelOp Pattern = new NullSentinelOp();
	}
}
