using System;
using System.Data.Common;
using System.Data.Metadata.Edm;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000080 RID: 128
	internal static class TypeUtils
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x000334A9 File Offset: 0x000316A9
		internal static bool IsUdt(TypeUsage type)
		{
			return TypeUtils.IsUdt(type.EdmType);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000173E2 File Offset: 0x000155E2
		internal static bool IsUdt(EdmType type)
		{
			return false;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000334B6 File Offset: 0x000316B6
		internal static bool IsStructuredType(TypeUsage type)
		{
			return TypeSemantics.IsReferenceType(type) || TypeSemantics.IsRowType(type) || TypeSemantics.IsEntityType(type) || TypeSemantics.IsRelationshipType(type) || (TypeSemantics.IsComplexType(type) && !TypeUtils.IsUdt(type));
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000334ED File Offset: 0x000316ED
		internal static bool IsCollectionType(TypeUsage type)
		{
			return TypeSemantics.IsCollectionType(type);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000334F5 File Offset: 0x000316F5
		internal static bool IsEnumerationType(TypeUsage type)
		{
			return TypeSemantics.IsEnumerationType(type);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000334FD File Offset: 0x000316FD
		internal static TypeUsage CreateCollectionType(TypeUsage elementType)
		{
			return TypeHelpers.CreateCollectionTypeUsage(elementType);
		}
	}
}
