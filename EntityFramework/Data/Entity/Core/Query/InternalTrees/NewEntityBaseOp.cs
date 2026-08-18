using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E9 RID: 1513
	internal abstract class NewEntityBaseOp : ScalarOp
	{
		// Token: 0x06003C10 RID: 15376 RVA: 0x00118B56 File Offset: 0x00116D56
		internal NewEntityBaseOp(OpType opType, TypeUsage type, bool scoped, EntitySet entitySet, List<RelProperty> relProperties) : base(opType, type)
		{
			this.m_scoped = scoped;
			this.m_entitySet = entitySet;
			this.m_relProperties = relProperties;
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00118B77 File Offset: 0x00116D77
		protected NewEntityBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003C12 RID: 15378 RVA: 0x00118B80 File Offset: 0x00116D80
		internal bool Scoped
		{
			get
			{
				return this.m_scoped;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003C13 RID: 15379 RVA: 0x00118B88 File Offset: 0x00116D88
		internal EntitySet EntitySet
		{
			get
			{
				return this.m_entitySet;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003C14 RID: 15380 RVA: 0x00118B90 File Offset: 0x00116D90
		internal List<RelProperty> RelationshipProperties
		{
			get
			{
				return this.m_relProperties;
			}
		}

		// Token: 0x04001685 RID: 5765
		private readonly bool m_scoped;

		// Token: 0x04001686 RID: 5766
		private readonly EntitySet m_entitySet;

		// Token: 0x04001687 RID: 5767
		private readonly List<RelProperty> m_relProperties;
	}
}
