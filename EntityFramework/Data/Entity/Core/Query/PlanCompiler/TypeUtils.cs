using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006A9 RID: 1705
	internal static class TypeUtils
	{
		// Token: 0x0600438B RID: 17291 RVA: 0x00140867 File Offset: 0x0013EA67
		internal static bool IsStructuredType(TypeUsage type)
		{
			return TypeSemantics.IsReferenceType(type) || TypeSemantics.IsRowType(type) || TypeSemantics.IsEntityType(type) || TypeSemantics.IsRelationshipType(type) || TypeSemantics.IsComplexType(type);
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x00140891 File Offset: 0x0013EA91
		internal static bool IsCollectionType(TypeUsage type)
		{
			return TypeSemantics.IsCollectionType(type);
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x00140899 File Offset: 0x0013EA99
		internal static bool IsEnumerationType(TypeUsage type)
		{
			return TypeSemantics.IsEnumerationType(type);
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x001408A1 File Offset: 0x0013EAA1
		internal static TypeUsage CreateCollectionType(TypeUsage elementType)
		{
			return TypeHelpers.CreateCollectionTypeUsage(elementType);
		}
	}
}
