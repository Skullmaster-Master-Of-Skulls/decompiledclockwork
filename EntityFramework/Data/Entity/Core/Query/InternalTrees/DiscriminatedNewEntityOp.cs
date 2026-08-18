using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005EA RID: 1514
	internal sealed class DiscriminatedNewEntityOp : NewEntityBaseOp
	{
		// Token: 0x06003C15 RID: 15381 RVA: 0x00118B98 File Offset: 0x00116D98
		internal DiscriminatedNewEntityOp(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap, EntitySet entitySet, List<RelProperty> relProperties) : base(OpType.DiscriminatedNewEntity, type, true, entitySet, relProperties)
		{
			this.m_discriminatorMap = discriminatorMap;
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x00118BAE File Offset: 0x00116DAE
		private DiscriminatedNewEntityOp() : base(OpType.DiscriminatedNewEntity)
		{
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x00118BB8 File Offset: 0x00116DB8
		internal ExplicitDiscriminatorMap DiscriminatorMap
		{
			get
			{
				return this.m_discriminatorMap;
			}
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x00118BC0 File Offset: 0x00116DC0
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x00118BCA File Offset: 0x00116DCA
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001688 RID: 5768
		private readonly ExplicitDiscriminatorMap m_discriminatorMap;

		// Token: 0x04001689 RID: 5769
		internal static readonly DiscriminatedNewEntityOp Pattern = new DiscriminatedNewEntityOp();
	}
}
