using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005EC RID: 1516
	internal sealed class ElementOp : ScalarOp
	{
		// Token: 0x06003C22 RID: 15394 RVA: 0x00118C24 File Offset: 0x00116E24
		internal ElementOp(TypeUsage type) : base(OpType.Element, type)
		{
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x00118C2F File Offset: 0x00116E2F
		private ElementOp() : base(OpType.Element)
		{
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x00118C39 File Offset: 0x00116E39
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x00118C3C File Offset: 0x00116E3C
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x00118C46 File Offset: 0x00116E46
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400168C RID: 5772
		internal static readonly ElementOp Pattern = new ElementOp();
	}
}
