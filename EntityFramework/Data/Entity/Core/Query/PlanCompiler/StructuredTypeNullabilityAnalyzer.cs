using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006A2 RID: 1698
	internal class StructuredTypeNullabilityAnalyzer : ColumnMapVisitor<HashSet<string>>
	{
		// Token: 0x06004350 RID: 17232 RVA: 0x0013F894 File Offset: 0x0013DA94
		internal override void Visit(VarRefColumnMap columnMap, HashSet<string> typesNeedingNullSentinel)
		{
			StructuredTypeNullabilityAnalyzer.AddTypeNeedingNullSentinel(typesNeedingNullSentinel, columnMap.Type);
			base.Visit(columnMap, typesNeedingNullSentinel);
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x0013F8AC File Offset: 0x0013DAAC
		private static void AddTypeNeedingNullSentinel(HashSet<string> typesNeedingNullSentinel, TypeUsage typeUsage)
		{
			if (TypeSemantics.IsCollectionType(typeUsage))
			{
				StructuredTypeNullabilityAnalyzer.AddTypeNeedingNullSentinel(typesNeedingNullSentinel, TypeHelpers.GetElementTypeUsage(typeUsage));
				return;
			}
			if (TypeSemantics.IsRowType(typeUsage) || TypeSemantics.IsComplexType(typeUsage))
			{
				StructuredTypeNullabilityAnalyzer.MarkAsNeedingNullSentinel(typesNeedingNullSentinel, typeUsage);
			}
			foreach (object obj in TypeHelpers.GetAllStructuralMembers(typeUsage))
			{
				EdmMember edmMember = (EdmMember)obj;
				StructuredTypeNullabilityAnalyzer.AddTypeNeedingNullSentinel(typesNeedingNullSentinel, edmMember.TypeUsage);
			}
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x0013F938 File Offset: 0x0013DB38
		internal static void MarkAsNeedingNullSentinel(HashSet<string> typesNeedingNullSentinel, TypeUsage typeUsage)
		{
			typesNeedingNullSentinel.Add(typeUsage.EdmType.Identity);
		}

		// Token: 0x040018EA RID: 6378
		internal static StructuredTypeNullabilityAnalyzer Instance = new StructuredTypeNullabilityAnalyzer();
	}
}
