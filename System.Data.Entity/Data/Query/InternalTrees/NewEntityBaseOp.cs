using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000FE RID: 254
	internal abstract class NewEntityBaseOp : ScalarOp
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x0003CD45 File Offset: 0x0003AF45
		internal NewEntityBaseOp(OpType opType, TypeUsage type, bool scoped, EntitySet entitySet, List<RelProperty> relProperties) : base(opType, type)
		{
			this.m_scoped = scoped;
			this.m_entitySet = entitySet;
			this.m_relProperties = relProperties;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003C7C9 File Offset: 0x0003A9C9
		protected NewEntityBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0003CD66 File Offset: 0x0003AF66
		internal bool Scoped
		{
			get
			{
				return this.m_scoped;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0003CD6E File Offset: 0x0003AF6E
		internal EntitySet EntitySet
		{
			get
			{
				return this.m_entitySet;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0003CD76 File Offset: 0x0003AF76
		internal List<RelProperty> RelationshipProperties
		{
			get
			{
				return this.m_relProperties;
			}
		}

		// Token: 0x040009B9 RID: 2489
		private readonly bool m_scoped;

		// Token: 0x040009BA RID: 2490
		private readonly EntitySet m_entitySet;

		// Token: 0x040009BB RID: 2491
		private readonly List<RelProperty> m_relProperties;
	}
}
