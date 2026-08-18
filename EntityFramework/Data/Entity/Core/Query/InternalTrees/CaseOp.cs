using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005CE RID: 1486
	internal sealed class CaseOp : ScalarOp
	{
		// Token: 0x06003B8A RID: 15242 RVA: 0x0011836E File Offset: 0x0011656E
		internal CaseOp(TypeUsage type) : base(OpType.Case, type)
		{
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x00118379 File Offset: 0x00116579
		private CaseOp() : base(OpType.Case)
		{
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x00118383 File Offset: 0x00116583
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x0011838D File Offset: 0x0011658D
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001655 RID: 5717
		internal static readonly CaseOp Pattern = new CaseOp();
	}
}
