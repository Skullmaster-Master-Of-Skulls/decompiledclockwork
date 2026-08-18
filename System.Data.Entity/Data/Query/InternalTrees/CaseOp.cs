using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000FA RID: 250
	internal sealed class CaseOp : ScalarOp
	{
		// Token: 0x06000D2F RID: 3375 RVA: 0x0003CC3A File Offset: 0x0003AE3A
		internal CaseOp(TypeUsage type) : base(OpType.Case, type)
		{
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003CC45 File Offset: 0x0003AE45
		private CaseOp() : base(OpType.Case)
		{
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003CC4F File Offset: 0x0003AE4F
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0003CC59 File Offset: 0x0003AE59
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009B2 RID: 2482
		internal static readonly CaseOp Pattern = new CaseOp();
	}
}
