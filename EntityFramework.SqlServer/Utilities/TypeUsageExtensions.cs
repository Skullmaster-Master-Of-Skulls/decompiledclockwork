using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200004A RID: 74
	internal static class TypeUsageExtensions
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0001D2E7 File Offset: 0x0001B4E7
		internal static byte GetPrecision(this TypeUsage type)
		{
			return type.GetFacetValue("Precision");
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
		internal static byte GetScale(this TypeUsage type)
		{
			return type.GetFacetValue("Scale");
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001D301 File Offset: 0x0001B501
		internal static int GetMaxLength(this TypeUsage type)
		{
			return type.GetFacetValue("MaxLength");
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001D30E File Offset: 0x0001B50E
		internal static T GetFacetValue<T>(this TypeUsage type, string facetName)
		{
			return (T)((object)type.Facets[facetName].Value);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001D338 File Offset: 0x0001B538
		internal static bool IsFixedLength(this TypeUsage type)
		{
			Facet facet = type.Facets.SingleOrDefault((Facet f) => f.Name == "FixedLength");
			return facet != null && facet.Value != null && (bool)facet.Value;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001D386 File Offset: 0x0001B586
		internal static bool TryGetPrecision(this TypeUsage type, out byte precision)
		{
			if (!type.IsPrimitiveType(PrimitiveTypeKind.Decimal))
			{
				precision = 0;
				return false;
			}
			return type.TryGetFacetValue("Precision", out precision);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001D3A2 File Offset: 0x0001B5A2
		internal static bool TryGetScale(this TypeUsage type, out byte scale)
		{
			if (!type.IsPrimitiveType(PrimitiveTypeKind.Decimal))
			{
				scale = 0;
				return false;
			}
			return type.TryGetFacetValue("Scale", out scale);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
		internal static bool TryGetFacetValue<T>(this TypeUsage type, string facetName, out T value)
		{
			value = default(T);
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value is T)
			{
				value = (T)((object)facet.Value);
				return true;
			}
			return false;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001D406 File Offset: 0x0001B606
		internal static bool IsPrimitiveType(this TypeUsage type, PrimitiveTypeKind primitiveTypeKind)
		{
			return type.IsPrimitiveType() && ((PrimitiveType)type.EdmType).PrimitiveTypeKind == primitiveTypeKind;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001D425 File Offset: 0x0001B625
		internal static bool IsPrimitiveType(this TypeUsage type)
		{
			return type != null && type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001D450 File Offset: 0x0001B650
		internal static bool IsNullable(this TypeUsage type)
		{
			Facet facet = type.Facets.SingleOrDefault((Facet f) => f.Name == "Nullable");
			return facet != null && facet.Value != null && (bool)facet.Value;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001D49E File Offset: 0x0001B69E
		internal static PrimitiveTypeKind GetPrimitiveTypeKind(this TypeUsage type)
		{
			return ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001D4B0 File Offset: 0x0001B6B0
		internal static bool TryGetIsUnicode(this TypeUsage type, out bool isUnicode)
		{
			if (!type.IsPrimitiveType(PrimitiveTypeKind.String))
			{
				isUnicode = false;
				return false;
			}
			return type.TryGetFacetValue("Unicode", out isUnicode);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001D4CD File Offset: 0x0001B6CD
		internal static bool TryGetMaxLength(this TypeUsage type, out int maxLength)
		{
			if (!type.IsPrimitiveType(PrimitiveTypeKind.String) && !type.IsPrimitiveType(PrimitiveTypeKind.Binary))
			{
				maxLength = 0;
				return false;
			}
			return type.TryGetFacetValue("MaxLength", out maxLength);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		internal static IEnumerable<EdmProperty> GetProperties(this TypeUsage type)
		{
			EdmType edmType = type.EdmType;
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.ComplexType)
			{
				return ((ComplexType)edmType).Properties;
			}
			if (builtInTypeKind == BuiltInTypeKind.EntityType)
			{
				return ((EntityType)edmType).Properties;
			}
			if (builtInTypeKind != BuiltInTypeKind.RowType)
			{
				return Enumerable.Empty<EdmProperty>();
			}
			return ((RowType)edmType).Properties;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001D548 File Offset: 0x0001B748
		internal static TypeUsage GetElementTypeUsage(this TypeUsage type)
		{
			EdmType edmType = type.EdmType;
			if (BuiltInTypeKind.CollectionType == edmType.BuiltInTypeKind)
			{
				return ((CollectionType)edmType).TypeUsage;
			}
			if (BuiltInTypeKind.RefType == edmType.BuiltInTypeKind)
			{
				return TypeUsage.CreateDefaultTypeUsage(((RefType)edmType).ElementType);
			}
			return null;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		internal static bool MustFacetBeConstant(this TypeUsage type, string facetName)
		{
			return ((PrimitiveType)type.EdmType).FacetDescriptions.Single((FacetDescription f) => f.FacetName == facetName).IsConstant;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001D5E8 File Offset: 0x0001B7E8
		internal static bool IsSpatialType(this TypeUsage type)
		{
			return type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && ((PrimitiveType)type.EdmType).IsSpatialType();
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001D60B File Offset: 0x0001B80B
		internal static bool IsSpatialType(this TypeUsage type, out PrimitiveTypeKind spatialType)
		{
			if (type.IsSpatialType())
			{
				spatialType = ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
				return true;
			}
			spatialType = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001D654 File Offset: 0x0001B854
		internal static TypeUsage ForceNonUnicode(this TypeUsage typeUsage)
		{
			TypeUsage typeUsage2 = TypeUsage.CreateStringTypeUsage((PrimitiveType)typeUsage.EdmType, false, false);
			return TypeUsage.Create(typeUsage.EdmType, (from f in typeUsage.Facets
			where f.Name != "Unicode"
			select f).Union(from f in typeUsage2.Facets
			where f.Name == "Unicode"
			select f));
		}
	}
}
