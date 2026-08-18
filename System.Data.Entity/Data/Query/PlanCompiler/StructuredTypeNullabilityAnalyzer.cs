using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000060 RID: 96
	internal class StructuredTypeNullabilityAnalyzer : ColumnMapVisitor<HashSet<string>>
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0002B8E6 File Offset: 0x00029AE6
		internal override void Visit(VarRefColumnMap columnMap, HashSet<string> typesNeedingNullSentinel)
		{
			StructuredTypeNullabilityAnalyzer.AddTypeNeedingNullSentinel(typesNeedingNullSentinel, columnMap.Type);
			base.Visit(columnMap, typesNeedingNullSentinel);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002B8FC File Offset: 0x00029AFC
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

		// Token: 0x0600082C RID: 2092 RVA: 0x0002B988 File Offset: 0x00029B88
		internal static void MarkAsNeedingNullSentinel(HashSet<string> typesNeedingNullSentinel, TypeUsage typeUsage)
		{
			typesNeedingNullSentinel.Add(typeUsage.EdmType.Identity);
		}

		// Token: 0x040007F1 RID: 2033
		internal static StructuredTypeNullabilityAnalyzer Instance = new StructuredTypeNullabilityAnalyzer();
	}
}
