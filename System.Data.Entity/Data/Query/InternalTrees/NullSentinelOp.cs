using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000EE RID: 238
	internal sealed class NullSentinelOp : ConstantBaseOp
	{
		// Token: 0x06000CE0 RID: 3296 RVA: 0x0003C8D5 File Offset: 0x0003AAD5
		internal NullSentinelOp(TypeUsage type, object value) : base(OpType.NullSentinel, type, value)
		{
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003C8E0 File Offset: 0x0003AAE0
		private NullSentinelOp() : base(OpType.NullSentinel)
		{
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0003C8E9 File Offset: 0x0003AAE9
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003C8F3 File Offset: 0x0003AAF3
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400099D RID: 2461
		internal static readonly NullSentinelOp Pattern = new NullSentinelOp();
	}
}
