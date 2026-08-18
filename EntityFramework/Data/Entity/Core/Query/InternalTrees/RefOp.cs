using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000618 RID: 1560
	internal sealed class RefOp : ScalarOp
	{
		// Token: 0x06003D27 RID: 15655 RVA: 0x0011ADE3 File Offset: 0x00118FE3
		internal RefOp(EntitySet entitySet, TypeUsage type) : base(OpType.Ref, type)
		{
			this.m_entitySet = entitySet;
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x0011ADF5 File Offset: 0x00118FF5
		private RefOp() : base(OpType.Ref)
		{
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x0011ADFF File Offset: 0x00118FFF
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x0011AE02 File Offset: 0x00119002
		internal EntitySet EntitySet
		{
			get
			{
				return this.m_entitySet;
			}
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x0011AE0A File Offset: 0x0011900A
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x0011AE14 File Offset: 0x00119014
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400171D RID: 5917
		private readonly EntitySet m_entitySet;

		// Token: 0x0400171E RID: 5918
		internal static readonly RefOp Pattern = new RefOp();
	}
}
