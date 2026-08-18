using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E5 RID: 1509
	internal sealed class DerefOp : ScalarOp
	{
		// Token: 0x06003BFE RID: 15358 RVA: 0x001189D2 File Offset: 0x00116BD2
		internal DerefOp(TypeUsage type) : base(OpType.Deref, type)
		{
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x001189DD File Offset: 0x00116BDD
		private DerefOp() : base(OpType.Deref)
		{
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x001189E7 File Offset: 0x00116BE7
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x001189EA File Offset: 0x00116BEA
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x001189F4 File Offset: 0x00116BF4
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400167F RID: 5759
		internal static readonly DerefOp Pattern = new DerefOp();
	}
}
