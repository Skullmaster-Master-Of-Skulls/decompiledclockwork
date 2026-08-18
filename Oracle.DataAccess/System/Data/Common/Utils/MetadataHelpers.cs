using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Common.Utils
{
	// Token: 0x02000087 RID: 135
	internal static class MetadataHelpers
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x0003F424 File Offset: 0x0003E424
		internal static FacetDescription GetFacet(IEnumerable<FacetDescription> facetCollection, string facetName)
		{
			foreach (FacetDescription facetDescription in facetCollection)
			{
				if (facetDescription.FacetName == facetName)
				{
					return facetDescription;
				}
			}
			return null;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0003F47C File Offset: 0x0003E47C
		internal static bool TryGetBooleanFacetValue(TypeUsage type, string facetName, out bool boolValue)
		{
			boolValue = false;
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value != null)
			{
				boolValue = (bool)facet.Value;
				return true;
			}
			return false;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0003F4B8 File Offset: 0x0003E4B8
		internal static bool TryGetByteFacetValue(TypeUsage type, string facetName, out byte byteValue)
		{
			byteValue = 0;
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value != null && !facet.IsUnbounded)
			{
				byteValue = (byte)facet.Value;
				return true;
			}
			return false;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0003F4FC File Offset: 0x0003E4FC
		internal static bool TryGetIntFacetValue(TypeUsage type, string facetName, out int intValue)
		{
			intValue = 0;
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value != null && !facet.IsUnbounded)
			{
				intValue = (int)facet.Value;
				return true;
			}
			return false;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0003F53D File Offset: 0x0003E53D
		internal static bool TryGetIsFixedLength(TypeUsage type, out bool isFixedLength)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.String) && !MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				isFixedLength = false;
				return false;
			}
			return MetadataHelpers.TryGetBooleanFacetValue(type, "FixedLength", out isFixedLength);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0003F563 File Offset: 0x0003E563
		internal static bool TryGetIsUnicode(TypeUsage type, out bool isUnicode)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.String))
			{
				isUnicode = false;
				return false;
			}
			return MetadataHelpers.TryGetBooleanFacetValue(type, "Unicode", out isUnicode);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0003F580 File Offset: 0x0003E580
		internal static bool IsFacetValueConstant(TypeUsage type, string facetName)
		{
			return MetadataHelpers.GetFacet(((PrimitiveType)type.EdmType).FacetDescriptions, facetName).IsConstant;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0003F59D File Offset: 0x0003E59D
		internal static bool TryGetMaxLength(TypeUsage type, out int maxLength)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.String) && !MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				maxLength = 0;
				return false;
			}
			return MetadataHelpers.TryGetIntFacetValue(type, "MaxLength", out maxLength);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0003F5C3 File Offset: 0x0003E5C3
		internal static bool TryGetPrecision(TypeUsage type, out byte precision)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				precision = 0;
				return false;
			}
			return MetadataHelpers.TryGetByteFacetValue(type, "Precision", out precision);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0003F5DF File Offset: 0x0003E5DF
		internal static bool TryGetScale(TypeUsage type, out byte scale)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				scale = 0;
				return false;
			}
			return MetadataHelpers.TryGetByteFacetValue(type, "Scale", out scale);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0003F5FB File Offset: 0x0003E5FB
		internal static bool TryGetPrimitiveTypeKind(TypeUsage type, out PrimitiveTypeKind typeKind)
		{
			if (type != null && type.EdmType != null && type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				typeKind = ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
				return true;
			}
			typeKind = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0003F630 File Offset: 0x0003E630
		internal static PrimitiveTypeKind GetPrimitiveTypeKind(TypeUsage typeUsage)
		{
			PrimitiveType primitiveType = (PrimitiveType)typeUsage.EdmType;
			return primitiveType.PrimitiveTypeKind;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0003F64F File Offset: 0x0003E64F
		internal static bool IsPrimitiveType(EdmType type)
		{
			return BuiltInTypeKind.PrimitiveType == type.BuiltInTypeKind;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0003F65C File Offset: 0x0003E65C
		internal static bool IsPrimitiveType(TypeUsage type, PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveTypeKind primitiveTypeKind2;
			return MetadataHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind2) && primitiveTypeKind2 == primitiveTypeKind;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0003F67C File Offset: 0x0003E67C
		internal static bool IsNullable(TypeUsage type)
		{
			Facet facet;
			return !type.Facets.TryGetValue("Nullable", false, out facet) || (bool)facet.Value;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0003F6AB File Offset: 0x0003E6AB
		internal static bool IsReferenceType(GlobalItem item)
		{
			return BuiltInTypeKind.RefType == item.BuiltInTypeKind;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0003F6B7 File Offset: 0x0003E6B7
		internal static bool IsRowType(GlobalItem item)
		{
			return BuiltInTypeKind.RowType == item.BuiltInTypeKind;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0003F6C3 File Offset: 0x0003E6C3
		internal static bool IsCollectionType(GlobalItem item)
		{
			return BuiltInTypeKind.CollectionType == item.BuiltInTypeKind;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0003F6D0 File Offset: 0x0003E6D0
		internal static TypeUsage GetElementTypeUsage(TypeUsage type)
		{
			if (MetadataHelpers.IsCollectionType(type.EdmType))
			{
				return ((CollectionType)type.EdmType).TypeUsage;
			}
			if (MetadataHelpers.IsReferenceType(type.EdmType))
			{
				return TypeUsage.CreateDefaultTypeUsage(((RefType)type.EdmType).ElementType);
			}
			return null;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0003F71F File Offset: 0x0003E71F
		internal static TEdmType GetEdmType<TEdmType>(TypeUsage typeUsage) where TEdmType : EdmType
		{
			return (TEdmType)((object)typeUsage.EdmType);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0003F72C File Offset: 0x0003E72C
		internal static bool IsCanonicalFunction(EdmFunction function)
		{
			return function.NamespaceName.Equals("Edm", StringComparison.InvariantCulture);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0003F73F File Offset: 0x0003E73F
		internal static IList<EdmProperty> GetProperties(TypeUsage typeUsage)
		{
			return MetadataHelpers.GetProperties(typeUsage.EdmType);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0003F74C File Offset: 0x0003E74C
		internal static IList<EdmProperty> GetProperties(EdmType edmType)
		{
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
				return new List<EdmProperty>();
			}
			return ((RowType)edmType).Properties;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0003F79C File Offset: 0x0003E79C
		internal static T GetMetadataProperty<T>(MetadataItem item, string propertyName)
		{
			MetadataProperty metadataProperty;
			if (!item.MetadataProperties.TryGetValue(propertyName, true, out metadataProperty) || !(metadataProperty.Value is T))
			{
				return default(T);
			}
			return (T)((object)metadataProperty.Value);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0003F7DC File Offset: 0x0003E7DC
		internal static ParameterDirection ParameterModeToParameterDirection(ParameterMode mode)
		{
			switch (mode)
			{
			case ParameterMode.In:
				return ParameterDirection.Input;
			case ParameterMode.Out:
				return ParameterDirection.Output;
			case ParameterMode.InOut:
				return ParameterDirection.InputOutput;
			case ParameterMode.ReturnValue:
				return ParameterDirection.ReturnValue;
			default:
				return (ParameterDirection)0;
			}
		}

		// Token: 0x040003C8 RID: 968
		internal const string MaxLengthFacetName = "MaxLength";

		// Token: 0x040003C9 RID: 969
		internal const string UnicodeFacetName = "Unicode";

		// Token: 0x040003CA RID: 970
		internal const string FixedLengthFacetName = "FixedLength";

		// Token: 0x040003CB RID: 971
		internal const string PrecisionFacetName = "Precision";

		// Token: 0x040003CC RID: 972
		internal const string ScaleFacetName = "Scale";

		// Token: 0x040003CD RID: 973
		internal const string NullableFacetName = "Nullable";

		// Token: 0x040003CE RID: 974
		internal const string DefaultValueFacetName = "DefaultValue";

		// Token: 0x040003CF RID: 975
		internal const string TableMetadata = "Table";

		// Token: 0x040003D0 RID: 976
		internal const string SchemaMetadata = "Schema";

		// Token: 0x040003D1 RID: 977
		internal const string DefiningQueryMetadata = "DefiningQuery";

		// Token: 0x040003D2 RID: 978
		internal const string CommandTextMetadata = "CommandTextAttribute";

		// Token: 0x040003D3 RID: 979
		internal const string StoreFunctionNameMetadata = "StoreFunctionNameAttribute";

		// Token: 0x040003D4 RID: 980
		internal const string BuiltInMetadata = "BuiltInAttribute";

		// Token: 0x040003D5 RID: 981
		internal const string NiladicFunctionMetadata = "NiladicFunctionAttribute";

		// Token: 0x040003D6 RID: 982
		internal const string OracleCursorParameterNameMetadata = "EFOracleProviderExtensions:CursorParameterName";

		// Token: 0x040003D7 RID: 983
		internal const string EdmNamespaceName = "Edm";
	}
}
