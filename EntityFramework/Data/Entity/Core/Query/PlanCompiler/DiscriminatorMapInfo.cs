using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000669 RID: 1641
	internal class DiscriminatorMapInfo
	{
		// Token: 0x06004030 RID: 16432 RVA: 0x00125D3C File Offset: 0x00123F3C
		internal DiscriminatorMapInfo(EntityTypeBase rootEntityType, bool includesSubTypes, ExplicitDiscriminatorMap discriminatorMap)
		{
			this.RootEntityType = rootEntityType;
			this.IncludesSubTypes = includesSubTypes;
			this.DiscriminatorMap = discriminatorMap;
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x00125D5C File Offset: 0x00123F5C
		internal void Merge(EntityTypeBase neededRootEntityType, bool includesSubtypes, ExplicitDiscriminatorMap discriminatorMap)
		{
			if (this.RootEntityType != neededRootEntityType || this.IncludesSubTypes != includesSubtypes)
			{
				if (!this.IncludesSubTypes || !includesSubtypes)
				{
					this.DiscriminatorMap = null;
				}
				if (TypeSemantics.IsSubTypeOf(this.RootEntityType, neededRootEntityType))
				{
					this.RootEntityType = neededRootEntityType;
					this.DiscriminatorMap = discriminatorMap;
				}
				if (!TypeSemantics.IsSubTypeOf(neededRootEntityType, this.RootEntityType))
				{
					this.DiscriminatorMap = null;
				}
			}
		}

		// Token: 0x040017DF RID: 6111
		internal EntityTypeBase RootEntityType;

		// Token: 0x040017E0 RID: 6112
		internal bool IncludesSubTypes;

		// Token: 0x040017E1 RID: 6113
		internal ExplicitDiscriminatorMap DiscriminatorMap;
	}
}
