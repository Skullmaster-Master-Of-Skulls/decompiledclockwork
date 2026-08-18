using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000049 RID: 73
	internal static class PrimitiveTypeExtensions
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0001D2C4 File Offset: 0x0001B4C4
		internal static bool IsSpatialType(this PrimitiveType type)
		{
			PrimitiveTypeKind primitiveTypeKind = type.PrimitiveTypeKind;
			return primitiveTypeKind >= PrimitiveTypeKind.Geometry && primitiveTypeKind <= PrimitiveTypeKind.GeographyCollection;
		}
	}
}
