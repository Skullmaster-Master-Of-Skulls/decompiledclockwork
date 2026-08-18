using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000EC RID: 236
	internal sealed class NullOp : ConstantBaseOp
	{
		// Token: 0x06000CD6 RID: 3286 RVA: 0x0003C86D File Offset: 0x0003AA6D
		internal NullOp(TypeUsage type) : base(OpType.Null, type, null)
		{
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003C878 File Offset: 0x0003AA78
		private NullOp() : base(OpType.Null)
		{
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0003C881 File Offset: 0x0003AA81
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0003C88B File Offset: 0x0003AA8B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400099B RID: 2459
		internal static readonly NullOp Pattern = new NullOp();
	}
}
