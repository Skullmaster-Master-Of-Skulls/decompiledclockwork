using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000101 RID: 257
	internal sealed class DiscriminatedNewEntityOp : NewEntityBaseOp
	{
		// Token: 0x06000D58 RID: 3416 RVA: 0x0003CDEC File Offset: 0x0003AFEC
		internal DiscriminatedNewEntityOp(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap, EntitySet entitySet, List<RelProperty> relProperties) : base(OpType.DiscriminatedNewEntity, type, true, entitySet, relProperties)
		{
			this.m_discriminatorMap = discriminatorMap;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003CE02 File Offset: 0x0003B002
		private DiscriminatedNewEntityOp() : base(OpType.DiscriminatedNewEntity)
		{
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0003CE0C File Offset: 0x0003B00C
		internal ExplicitDiscriminatorMap DiscriminatorMap
		{
			get
			{
				return this.m_discriminatorMap;
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003CE14 File Offset: 0x0003B014
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003CE1E File Offset: 0x0003B01E
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009BE RID: 2494
		private readonly ExplicitDiscriminatorMap m_discriminatorMap;

		// Token: 0x040009BF RID: 2495
		internal static readonly DiscriminatedNewEntityOp Pattern = new DiscriminatedNewEntityOp();
	}
}
