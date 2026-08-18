using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000605 RID: 1541
	internal sealed class NewEntityOp : NewEntityBaseOp
	{
		// Token: 0x06003CB8 RID: 15544 RVA: 0x00119653 File Offset: 0x00117853
		private NewEntityOp() : base(OpType.NewEntity)
		{
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x0011965D File Offset: 0x0011785D
		internal NewEntityOp(TypeUsage type, List<RelProperty> relProperties, bool scoped, EntitySet entitySet) : base(OpType.NewEntity, type, scoped, entitySet, relProperties)
		{
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x0011966C File Offset: 0x0011786C
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x00119676 File Offset: 0x00117876
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016BA RID: 5818
		internal static readonly NewEntityOp Pattern = new NewEntityOp();
	}
}
