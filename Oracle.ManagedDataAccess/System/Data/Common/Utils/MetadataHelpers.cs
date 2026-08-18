using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Common.Utils
{
	// Token: 0x020000EA RID: 234
	internal static class MetadataHelpers
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x0006CA58 File Offset: 0x0006AC58
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

		// Token: 0x0600092C RID: 2348 RVA: 0x0006CAB0 File Offset: 0x0006ACB0
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

		// Token: 0x0600092D RID: 2349 RVA: 0x0006CAEC File Offset: 0x0006ACEC
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

		// Token: 0x0600092E RID: 2350 RVA: 0x0006CB30 File Offset: 0x0006AD30
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

		// Token: 0x0600092F RID: 2351 RVA: 0x0006CB74 File Offset: 0x0006AD74
		internal static bool TryGetIsFixedLength(TypeUsage type, out bool isFixedLength)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.String) && !MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				isFixedLength = false;
				return false;
			}
			return MetadataHelpers.TryGetBooleanFacetValue(type, "FixedLength", out isFixedLength);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0006CB9C File Offset: 0x0006AD9C
		internal static bool TryGetIsUnicode(TypeUsage type, out bool isUnicode)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.String))
			{
				isUnicode = false;
				return false;
			}
			return MetadataHelpers.TryGetBooleanFacetValue(type, "Unicode", out isUnicode);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0006CBBC File Offset: 0x0006ADBC
		internal static bool IsFacetValueConstant(TypeUsage type, string facetName)
		{
			return MetadataHelpers.GetFacet(((PrimitiveType)type.EdmType).FacetDescriptions, facetName).IsConstant;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0006CBDC File Offset: 0x0006ADDC
		internal static bool TryGetMaxLength(TypeUsage type, out int maxLength)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.String) && !MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				maxLength = 0;
				return false;
			}
			return MetadataHelpers.TryGetIntFacetValue(type, "MaxLength", out maxLength);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0006CC04 File Offset: 0x0006AE04
		internal static bool TryGetPrecision(TypeUsage type, out byte precision)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				precision = 0;
				return false;
			}
			return MetadataHelpers.TryGetByteFacetValue(type, "Precision", out precision);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0006CC20 File Offset: 0x0006AE20
		internal static bool TryGetScale(TypeUsage type, out byte scale)
		{
			if (!MetadataHelpers.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				scale = 0;
				return false;
			}
			return MetadataHelpers.TryGetByteFacetValue(type, "Scale", out scale);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0006CC3C File Offset: 0x0006AE3C
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

		// Token: 0x06000936 RID: 2358 RVA: 0x0006CC70 File Offset: 0x0006AE70
		internal static PrimitiveTypeKind GetPrimitiveTypeKind(TypeUsage typeUsage)
		{
			PrimitiveType primitiveType = (PrimitiveType)typeUsage.EdmType;
			return primitiveType.PrimitiveTypeKind;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0006CC90 File Offset: 0x0006AE90
		internal static bool IsPrimitiveType(EdmType type)
		{
			return BuiltInTypeKind.PrimitiveType == type.BuiltInTypeKind;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0006CC9C File Offset: 0x0006AE9C
		internal static bool IsPrimitiveType(TypeUsage type, PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveTypeKind primitiveTypeKind2;
			return MetadataHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind2) && primitiveTypeKind2 == primitiveTypeKind;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0006CCBC File Offset: 0x0006AEBC
		internal static bool IsNullable(TypeUsage type)
		{
			Facet facet;
			return !type.Facets.TryGetValue("Nullable", false, out facet) || (bool)facet.Value;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0006CCEC File Offset: 0x0006AEEC
		internal static bool IsReferenceType(GlobalItem item)
		{
			return BuiltInTypeKind.RefType == item.BuiltInTypeKind;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0006CCF8 File Offset: 0x0006AEF8
		internal static bool IsRowType(GlobalItem item)
		{
			return BuiltInTypeKind.RowType == item.BuiltInTypeKind;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0006CD04 File Offset: 0x0006AF04
		internal static bool IsCollectionType(GlobalItem item)
		{
			return BuiltInTypeKind.CollectionType == item.BuiltInTypeKind;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0006CD10 File Offset: 0x0006AF10
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

		// Token: 0x0600093E RID: 2366 RVA: 0x0006CD60 File Offset: 0x0006AF60
		internal static TEdmType GetEdmType<TEdmType>(TypeUsage typeUsage) where TEdmType : EdmType
		{
			return (TEdmType)((object)typeUsage.EdmType);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0006CD70 File Offset: 0x0006AF70
		internal static bool IsCanonicalFunction(EdmFunction function)
		{
			return function.NamespaceName.Equals("Edm", StringComparison.InvariantCulture);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0006CD84 File Offset: 0x0006AF84
		internal static bool IsProviderSpecificFunction(EdmFunction function)
		{
			return function.NamespaceName.Equals("OracleEFProvider", StringComparison.InvariantCulture);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0006CD98 File Offset: 0x0006AF98
		internal static IList<EdmProperty> GetProperties(TypeUsage typeUsage)
		{
			return MetadataHelpers.GetProperties(typeUsage.EdmType);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0006CDA8 File Offset: 0x0006AFA8
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

		// Token: 0x06000943 RID: 2371 RVA: 0x0006CDF8 File Offset: 0x0006AFF8
		internal static TypeUsage CopyTypeUsageAndSetUnicodeFacetToFalse(TypeUsage typeUsage)
		{
			bool isFixedLength = false;
			int num = 0;
			MetadataHelpers.TryGetIsFixedLength(typeUsage, out isFixedLength);
			MetadataHelpers.TryGetMaxLength(typeUsage, out num);
			if (num > 0)
			{
				return TypeUsage.CreateStringTypeUsage((PrimitiveType)typeUsage.EdmType, false, isFixedLength, num);
			}
			return TypeUsage.CreateStringTypeUsage((PrimitiveType)typeUsage.EdmType, false, isFixedLength);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0006CE48 File Offset: 0x0006B048
		internal static T GetMetadataProperty<T>(MetadataItem item, string propertyName)
		{
			MetadataProperty metadataProperty;
			if (!item.MetadataProperties.TryGetValue(propertyName, true, out metadataProperty) || !(metadataProperty.Value is T))
			{
				return default(T);
			}
			return (T)((object)metadataProperty.Value);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0006CE88 File Offset: 0x0006B088
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

		// Token: 0x04000C3D RID: 3133
		internal const string MaxLengthFacetName = "MaxLength";

		// Token: 0x04000C3E RID: 3134
		internal const string UnicodeFacetName = "Unicode";

		// Token: 0x04000C3F RID: 3135
		internal const string FixedLengthFacetName = "FixedLength";

		// Token: 0x04000C40 RID: 3136
		internal const string PrecisionFacetName = "Precision";

		// Token: 0x04000C41 RID: 3137
		internal const string ScaleFacetName = "Scale";

		// Token: 0x04000C42 RID: 3138
		internal const string NullableFacetName = "Nullable";

		// Token: 0x04000C43 RID: 3139
		internal const string DefaultValueFacetName = "DefaultValue";

		// Token: 0x04000C44 RID: 3140
		internal const string TableMetadata = "Table";

		// Token: 0x04000C45 RID: 3141
		internal const string SchemaMetadata = "Schema";

		// Token: 0x04000C46 RID: 3142
		internal const string DefiningQueryMetadata = "DefiningQuery";

		// Token: 0x04000C47 RID: 3143
		internal const string CommandTextMetadata = "CommandTextAttribute";

		// Token: 0x04000C48 RID: 3144
		internal const string StoreFunctionNameMetadata = "StoreFunctionNameAttribute";

		// Token: 0x04000C49 RID: 3145
		internal const string BuiltInMetadata = "BuiltInAttribute";

		// Token: 0x04000C4A RID: 3146
		internal const string NiladicFunctionMetadata = "NiladicFunctionAttribute";

		// Token: 0x04000C4B RID: 3147
		internal const string OracleCursorParameterNameMetadata = "EFOracleProviderExtensions:CursorParameterName";

		// Token: 0x04000C4C RID: 3148
		internal const string EdmNamespaceName = "Edm";

		// Token: 0x04000C4D RID: 3149
		internal const string OracleEFProviderNamespaceName = "OracleEFProvider";
	}
}
