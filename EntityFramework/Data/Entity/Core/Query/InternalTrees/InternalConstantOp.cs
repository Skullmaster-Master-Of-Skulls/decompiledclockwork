using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005FC RID: 1532
	internal sealed class InternalConstantOp : ConstantBaseOp
	{
		// Token: 0x06003C85 RID: 15493 RVA: 0x0011926F File Offset: 0x0011746F
		internal InternalConstantOp(TypeUsage type, object value) : base(OpType.InternalConstant, type, value)
		{
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x0011927A File Offset: 0x0011747A
		private InternalConstantOp() : base(OpType.InternalConstant)
		{
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x00119283 File Offset: 0x00117483
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x0011928D File Offset: 0x0011748D
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016AA RID: 5802
		internal static readonly InternalConstantOp Pattern = new InternalConstantOp();
	}
}
