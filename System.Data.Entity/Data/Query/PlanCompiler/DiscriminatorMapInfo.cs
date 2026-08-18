using System;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200005E RID: 94
	internal class DiscriminatorMapInfo
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x000290F2 File Offset: 0x000272F2
		internal DiscriminatorMapInfo(EntityTypeBase rootEntityType, bool includesSubTypes, ExplicitDiscriminatorMap discriminatorMap)
		{
			this.RootEntityType = rootEntityType;
			this.IncludesSubTypes = includesSubTypes;
			this.DiscriminatorMap = discriminatorMap;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00029110 File Offset: 0x00027310
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

		// Token: 0x040007E4 RID: 2020
		internal EntityTypeBase RootEntityType;

		// Token: 0x040007E5 RID: 2021
		internal bool IncludesSubTypes;

		// Token: 0x040007E6 RID: 2022
		internal ExplicitDiscriminatorMap DiscriminatorMap;
	}
}
