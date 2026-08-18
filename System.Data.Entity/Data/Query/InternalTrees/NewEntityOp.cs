using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000FF RID: 255
	internal sealed class NewEntityOp : NewEntityBaseOp
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x0003CD7E File Offset: 0x0003AF7E
		private NewEntityOp() : base(OpType.NewEntity)
		{
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003CD88 File Offset: 0x0003AF88
		internal NewEntityOp(TypeUsage type, List<RelProperty> relProperties, bool scoped, EntitySet entitySet) : base(OpType.NewEntity, type, scoped, entitySet, relProperties)
		{
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003CD97 File Offset: 0x0003AF97
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003CDA1 File Offset: 0x0003AFA1
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009BC RID: 2492
		internal static readonly NewEntityOp Pattern = new NewEntityOp();
	}
}
