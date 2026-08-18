using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000EB RID: 235
	internal sealed class ConstantOp : ConstantBaseOp
	{
		// Token: 0x06000CD1 RID: 3281 RVA: 0x0003C839 File Offset: 0x0003AA39
		internal ConstantOp(TypeUsage type, object value) : base(OpType.Constant, type, value)
		{
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0003C844 File Offset: 0x0003AA44
		private ConstantOp() : base(OpType.Constant)
		{
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003C84D File Offset: 0x0003AA4D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0003C857 File Offset: 0x0003AA57
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400099A RID: 2458
		internal static readonly ConstantOp Pattern = new ConstantOp();
	}
}
