using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.ELinq;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x0200032E RID: 814
	internal static class TypeHelpers
	{
		// Token: 0x06002FC7 RID: 12231 RVA: 0x000B48C0 File Offset: 0x000B2AC0
		[Conditional("DEBUG")]
		internal static void AssertEdmType(TypeUsage typeUsage)
		{
			EdmType edmType = typeUsage.EdmType;
			if (!TypeSemantics.IsCollectionType(typeUsage))
			{
				if (TypeSemantics.IsStructuralType(typeUsage) && !Helper.IsComplexType(typeUsage.EdmType) && !Helper.IsEntityType(typeUsage.EdmType))
				{
					using (IEnumerator enumerator = TypeHelpers.GetDeclaredStructuralMembers(typeUsage).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							EdmMember edmMember = (EdmMember)obj;
						}
						return;
					}
				}
				if (TypeSemantics.IsPrimitiveType(typeUsage))
				{
					PrimitiveType primitiveType = edmType as PrimitiveType;
					if (primitiveType != null && primitiveType.DataSpace != DataSpace.CSpace)
					{
						throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "PrimitiveType must be CSpace '{0}'", new object[]
						{
							typeUsage.ToString()
						}));
					}
				}
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000B4988 File Offset: 0x000B2B88
		[Conditional("DEBUG")]
		internal static void AssertEdmType(DbCommandTree commandTree)
		{
			DbQueryCommandTree dbQueryCommandTree = commandTree as DbQueryCommandTree;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000B49A0 File Offset: 0x000B2BA0
		internal static bool IsValidSortOpKeyType(TypeUsage typeUsage)
		{
			if (TypeSemantics.IsRowType(typeUsage))
			{
				RowType rowType = (RowType)typeUsage.EdmType;
				foreach (EdmProperty edmProperty in rowType.Properties)
				{
					if (!TypeHelpers.IsValidSortOpKeyType(edmProperty.TypeUsage))
					{
						return false;
					}
				}
				return true;
			}
			return TypeSemantics.IsOrderComparable(typeUsage);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000B4A1C File Offset: 0x000B2C1C
		internal static bool IsValidGroupKeyType(TypeUsage typeUsage)
		{
			return TypeHelpers.IsSetComparableOpType(typeUsage);
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000B4A1C File Offset: 0x000B2C1C
		internal static bool IsValidDistinctOpType(TypeUsage typeUsage)
		{
			return TypeHelpers.IsSetComparableOpType(typeUsage);
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000B4A24 File Offset: 0x000B2C24
		internal static bool IsSetComparableOpType(TypeUsage typeUsage)
		{
			if (Helper.IsEntityType(typeUsage.EdmType) || Helper.IsPrimitiveType(typeUsage.EdmType) || Helper.IsEnumType(typeUsage.EdmType) || Helper.IsRefType(typeUsage.EdmType))
			{
				return true;
			}
			if (TypeSemantics.IsRowType(typeUsage))
			{
				RowType rowType = (RowType)typeUsage.EdmType;
				foreach (EdmProperty edmProperty in rowType.Properties)
				{
					if (!TypeHelpers.IsSetComparableOpType(edmProperty.TypeUsage))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000B4AD0 File Offset: 0x000B2CD0
		internal static bool IsValidIsNullOpType(TypeUsage typeUsage)
		{
			return TypeSemantics.IsReferenceType(typeUsage) || TypeSemantics.IsEntityType(typeUsage) || TypeSemantics.IsScalarType(typeUsage);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000B4AD0 File Offset: 0x000B2CD0
		internal static bool IsValidInOpType(TypeUsage typeUsage)
		{
			return TypeSemantics.IsReferenceType(typeUsage) || TypeSemantics.IsEntityType(typeUsage) || TypeSemantics.IsScalarType(typeUsage);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000B4AEA File Offset: 0x000B2CEA
		internal static TypeUsage GetCommonTypeUsage(TypeUsage typeUsage1, TypeUsage typeUsage2)
		{
			return TypeSemantics.GetCommonType(typeUsage1, typeUsage2);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000B4AF4 File Offset: 0x000B2CF4
		internal static TypeUsage GetCommonTypeUsage(IEnumerable<TypeUsage> types)
		{
			TypeUsage typeUsage = null;
			foreach (TypeUsage typeUsage2 in types)
			{
				if (typeUsage2 == null)
				{
					return null;
				}
				if (typeUsage == null)
				{
					typeUsage = typeUsage2;
				}
				else
				{
					typeUsage = TypeSemantics.GetCommonType(typeUsage, typeUsage2);
					if (typeUsage == null)
					{
						break;
					}
				}
			}
			return typeUsage;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000B4B58 File Offset: 0x000B2D58
		internal static bool TryGetClosestPromotableType(TypeUsage fromType, out TypeUsage promotableType)
		{
			promotableType = null;
			if (Helper.IsPrimitiveType(fromType.EdmType))
			{
				PrimitiveType primitiveType = (PrimitiveType)fromType.EdmType;
				IList<PrimitiveType> promotionTypes = EdmProviderManifest.Instance.GetPromotionTypes(primitiveType);
				int num = promotionTypes.IndexOf(primitiveType);
				if (-1 != num && num + 1 < promotionTypes.Count)
				{
					promotableType = TypeUsage.Create(promotionTypes[num + 1]);
				}
			}
			return promotableType != null;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000B4BBC File Offset: 0x000B2DBC
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

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000B4BF8 File Offset: 0x000B2DF8
		internal static bool TryGetByteFacetValue(TypeUsage type, string facetName, out byte byteValue)
		{
			byteValue = 0;
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value != null && !Helper.IsUnboundedFacetValue(facet))
			{
				byteValue = (byte)facet.Value;
				return true;
			}
			return false;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000B4C3C File Offset: 0x000B2E3C
		internal static bool TryGetIntFacetValue(TypeUsage type, string facetName, out int intValue)
		{
			intValue = 0;
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value != null && !Helper.IsUnboundedFacetValue(facet) && !Helper.IsVariableFacetValue(facet))
			{
				intValue = (int)facet.Value;
				return true;
			}
			return false;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000B4C85 File Offset: 0x000B2E85
		internal static bool TryGetIsFixedLength(TypeUsage type, out bool isFixedLength)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String) && !TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				isFixedLength = false;
				return false;
			}
			return TypeHelpers.TryGetBooleanFacetValue(type, "FixedLength", out isFixedLength);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000B4CAB File Offset: 0x000B2EAB
		internal static bool TryGetIsUnicode(TypeUsage type, out bool isUnicode)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String))
			{
				isUnicode = false;
				return false;
			}
			return TypeHelpers.TryGetBooleanFacetValue(type, "Unicode", out isUnicode);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000B4CC8 File Offset: 0x000B2EC8
		internal static bool IsFacetValueConstant(TypeUsage type, string facetName)
		{
			return Helper.GetFacet(((PrimitiveType)type.EdmType).FacetDescriptions, facetName).IsConstant;
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000B4CE5 File Offset: 0x000B2EE5
		internal static bool TryGetMaxLength(TypeUsage type, out int maxLength)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String) && !TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				maxLength = 0;
				return false;
			}
			return TypeHelpers.TryGetIntFacetValue(type, "MaxLength", out maxLength);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000B4D0B File Offset: 0x000B2F0B
		internal static bool TryGetPrecision(TypeUsage type, out byte precision)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				precision = 0;
				return false;
			}
			return TypeHelpers.TryGetByteFacetValue(type, "Precision", out precision);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000B4D27 File Offset: 0x000B2F27
		internal static bool TryGetScale(TypeUsage type, out byte scale)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				scale = 0;
				return false;
			}
			return TypeHelpers.TryGetByteFacetValue(type, "Scale", out scale);
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000B4D43 File Offset: 0x000B2F43
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

		// Token: 0x06002FDC RID: 12252 RVA: 0x000B4D77 File Offset: 0x000B2F77
		internal static CollectionType CreateCollectionType(TypeUsage elementType)
		{
			return new CollectionType(elementType);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000B4D7F File Offset: 0x000B2F7F
		internal static TypeUsage CreateCollectionTypeUsage(TypeUsage elementType)
		{
			return TypeHelpers.CreateCollectionTypeUsage(elementType, false);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000B4D88 File Offset: 0x000B2F88
		internal static TypeUsage CreateCollectionTypeUsage(TypeUsage elementType, bool readOnly)
		{
			return TypeUsage.Create(new CollectionType(elementType));
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000B4D95 File Offset: 0x000B2F95
		internal static RowType CreateRowType(IEnumerable<KeyValuePair<string, TypeUsage>> columns)
		{
			return TypeHelpers.CreateRowType(columns, null);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000B4DA0 File Offset: 0x000B2FA0
		internal static RowType CreateRowType(IEnumerable<KeyValuePair<string, TypeUsage>> columns, InitializerMetadata initializerMetadata)
		{
			List<EdmProperty> list = new List<EdmProperty>();
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in columns)
			{
				list.Add(new EdmProperty(keyValuePair.Key, keyValuePair.Value));
			}
			return new RowType(list, initializerMetadata);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000B4E08 File Offset: 0x000B3008
		internal static TypeUsage CreateRowTypeUsage(IEnumerable<KeyValuePair<string, TypeUsage>> columns, bool readOnly)
		{
			return TypeUsage.Create(TypeHelpers.CreateRowType(columns));
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000B4E15 File Offset: 0x000B3015
		internal static RefType CreateReferenceType(EntityTypeBase entityType)
		{
			return new RefType((EntityType)entityType);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000B4E22 File Offset: 0x000B3022
		internal static TypeUsage CreateReferenceTypeUsage(EntityType entityType)
		{
			return TypeUsage.Create(TypeHelpers.CreateReferenceType(entityType));
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000B4E30 File Offset: 0x000B3030
		internal static RowType CreateKeyRowType(EntityTypeBase entityType)
		{
			IEnumerable<EdmMember> keyMembers = entityType.KeyMembers;
			if (keyMembers == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_EntityTypeNullKeyMembersInvalid, "entityType");
			}
			List<KeyValuePair<string, TypeUsage>> list = new List<KeyValuePair<string, TypeUsage>>();
			foreach (EdmMember edmMember in keyMembers)
			{
				EdmProperty edmProperty = (EdmProperty)edmMember;
				list.Add(new KeyValuePair<string, TypeUsage>(edmProperty.Name, Helper.GetModelTypeUsage(edmProperty)));
			}
			if (list.Count < 1)
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid, "entityType");
			}
			return TypeHelpers.CreateRowType(list);
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000B4ECC File Offset: 0x000B30CC
		internal static TypeUsage GetPrimitiveTypeUsageForScalar(TypeUsage scalarType)
		{
			if (!TypeSemantics.IsEnumerationType(scalarType))
			{
				return scalarType;
			}
			return TypeHelpers.CreateEnumUnderlyingTypeUsage(scalarType);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000B4EDE File Offset: 0x000B30DE
		internal static TypeUsage CreateEnumUnderlyingTypeUsage(TypeUsage enumTypeUsage)
		{
			return TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(enumTypeUsage.EdmType), enumTypeUsage.Facets);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000B4EF6 File Offset: 0x000B30F6
		internal static TypeUsage CreateSpatialUnionTypeUsage(TypeUsage spatialTypeUsage)
		{
			return TypeUsage.Create(Helper.GetSpatialNormalizedPrimitiveType(spatialTypeUsage.EdmType), spatialTypeUsage.Facets);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000B4F0E File Offset: 0x000B310E
		internal static IBaseList<EdmMember> GetAllStructuralMembers(TypeUsage type)
		{
			return TypeHelpers.GetAllStructuralMembers(type.EdmType);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000B4F1C File Offset: 0x000B311C
		internal static IBaseList<EdmMember> GetAllStructuralMembers(EdmType edmType)
		{
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
			{
				if (builtInTypeKind == BuiltInTypeKind.AssociationType)
				{
					return (IBaseList<EdmMember>)((AssociationType)edmType).AssociationEndMembers;
				}
				if (builtInTypeKind == BuiltInTypeKind.ComplexType)
				{
					return (IBaseList<EdmMember>)((ComplexType)edmType).Properties;
				}
			}
			else
			{
				if (builtInTypeKind == BuiltInTypeKind.EntityType)
				{
					return (IBaseList<EdmMember>)((EntityType)edmType).Properties;
				}
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					return (IBaseList<EdmMember>)((RowType)edmType).Properties;
				}
			}
			return TypeHelpers.EmptyArrayEdmProperty;
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000B4F93 File Offset: 0x000B3193
		internal static IEnumerable GetDeclaredStructuralMembers(TypeUsage type)
		{
			return TypeHelpers.GetDeclaredStructuralMembers(type.EdmType);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000B4FA0 File Offset: 0x000B31A0
		internal static IEnumerable GetDeclaredStructuralMembers(EdmType edmType)
		{
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
			{
				if (builtInTypeKind == BuiltInTypeKind.AssociationType)
				{
					return ((AssociationType)edmType).GetDeclaredOnlyMembers<AssociationEndMember>();
				}
				if (builtInTypeKind == BuiltInTypeKind.ComplexType)
				{
					return ((ComplexType)edmType).GetDeclaredOnlyMembers<EdmProperty>();
				}
			}
			else
			{
				if (builtInTypeKind == BuiltInTypeKind.EntityType)
				{
					return ((EntityType)edmType).GetDeclaredOnlyMembers<EdmProperty>();
				}
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					return ((RowType)edmType).GetDeclaredOnlyMembers<EdmProperty>();
				}
			}
			return TypeHelpers.EmptyArrayEdmProperty;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000B5003 File Offset: 0x000B3203
		internal static ReadOnlyMetadataCollection<EdmProperty> GetProperties(TypeUsage typeUsage)
		{
			return TypeHelpers.GetProperties(typeUsage.EdmType);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000B5010 File Offset: 0x000B3210
		internal static ReadOnlyMetadataCollection<EdmProperty> GetProperties(EdmType edmType)
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
				return TypeHelpers.EmptyArrayEdmProperty;
			}
			return ((RowType)edmType).Properties;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000B505D File Offset: 0x000B325D
		internal static TypeUsage GetElementTypeUsage(TypeUsage type)
		{
			if (TypeSemantics.IsCollectionType(type))
			{
				return ((CollectionType)type.EdmType).TypeUsage;
			}
			if (TypeSemantics.IsReferenceType(type))
			{
				return TypeUsage.Create(((RefType)type.EdmType).ElementType);
			}
			return null;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000B5098 File Offset: 0x000B3298
		internal static RowType GetTvfReturnType(EdmFunction tvf)
		{
			if (tvf.ReturnParameter != null && TypeSemantics.IsCollectionType(tvf.ReturnParameter.TypeUsage))
			{
				TypeUsage typeUsage = ((CollectionType)tvf.ReturnParameter.TypeUsage.EdmType).TypeUsage;
				if (TypeSemantics.IsRowType(typeUsage))
				{
					return (RowType)typeUsage.EdmType;
				}
			}
			return null;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000B50F0 File Offset: 0x000B32F0
		internal static bool TryGetCollectionElementType(TypeUsage type, out TypeUsage elementType)
		{
			CollectionType collectionType;
			if (TypeHelpers.TryGetEdmType<CollectionType>(type, out collectionType))
			{
				elementType = collectionType.TypeUsage;
				return elementType != null;
			}
			elementType = null;
			return false;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000B511C File Offset: 0x000B331C
		internal static bool TryGetRefEntityType(TypeUsage type, out EntityType referencedEntityType)
		{
			RefType refType;
			if (TypeHelpers.TryGetEdmType<RefType>(type, out refType) && Helper.IsEntityType(refType.ElementType))
			{
				referencedEntityType = (EntityType)refType.ElementType;
				return true;
			}
			referencedEntityType = null;
			return false;
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000B5153 File Offset: 0x000B3353
		internal static TEdmType GetEdmType<TEdmType>(TypeUsage typeUsage) where TEdmType : EdmType
		{
			return (TEdmType)((object)typeUsage.EdmType);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000B5160 File Offset: 0x000B3360
		internal static bool TryGetEdmType<TEdmType>(TypeUsage typeUsage, out TEdmType type) where TEdmType : EdmType
		{
			type = (typeUsage.EdmType as TEdmType);
			return type != null;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000B5186 File Offset: 0x000B3386
		internal static TypeUsage GetReadOnlyType(TypeUsage type)
		{
			if (!type.IsReadOnly)
			{
				type.SetReadOnly();
			}
			return type;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000B5197 File Offset: 0x000B3397
		internal static string GetFullName(TypeUsage type)
		{
			return type.ToString();
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000B519F File Offset: 0x000B339F
		internal static string GetFullName(EdmType type)
		{
			return TypeHelpers.GetFullName(type.NamespaceName, type.Name);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000B51B2 File Offset: 0x000B33B2
		internal static string GetFullName(EntitySetBase entitySet)
		{
			return TypeHelpers.GetFullName(entitySet.EntityContainer.Name, entitySet.Name);
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000B51CC File Offset: 0x000B33CC
		internal static string GetFullName(string qualifier, string name)
		{
			if (string.IsNullOrEmpty(qualifier))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					name
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				qualifier,
				name
			});
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000B5218 File Offset: 0x000B3418
		internal static DbType ConvertClrTypeToDbType(Type clrType)
		{
			switch (Type.GetTypeCode(clrType))
			{
			case TypeCode.Empty:
				throw EntityUtil.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (clrType == typeof(byte[]))
				{
					return DbType.Binary;
				}
				if (clrType == typeof(char[]))
				{
					return DbType.String;
				}
				if (clrType == typeof(Guid))
				{
					return DbType.Guid;
				}
				if (clrType == typeof(TimeSpan))
				{
					return DbType.Time;
				}
				if (clrType == typeof(DateTimeOffset))
				{
					return DbType.DateTimeOffset;
				}
				return DbType.Object;
			case TypeCode.DBNull:
				return DbType.Object;
			case TypeCode.Boolean:
				return DbType.Boolean;
			case TypeCode.Char:
				return DbType.String;
			case TypeCode.SByte:
				return DbType.SByte;
			case TypeCode.Byte:
				return DbType.Byte;
			case TypeCode.Int16:
				return DbType.Int16;
			case TypeCode.UInt16:
				return DbType.UInt16;
			case TypeCode.Int32:
				return DbType.Int32;
			case TypeCode.UInt32:
				return DbType.UInt32;
			case TypeCode.Int64:
				return DbType.Int64;
			case TypeCode.UInt64:
				return DbType.UInt64;
			case TypeCode.Single:
				return DbType.Single;
			case TypeCode.Double:
				return DbType.Double;
			case TypeCode.Decimal:
				return DbType.Decimal;
			case TypeCode.DateTime:
				return DbType.DateTime;
			case TypeCode.String:
				return DbType.String;
			}
			throw EntityUtil.UnknownDataTypeCode(clrType, Type.GetTypeCode(clrType));
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000B532C File Offset: 0x000B352C
		internal static bool IsIntegerConstant(TypeUsage valueType, object value, long expectedValue)
		{
			if (!TypeSemantics.IsIntegerNumericType(valueType))
			{
				return false;
			}
			if (value == null)
			{
				return false;
			}
			PrimitiveType primitiveType = (PrimitiveType)valueType.EdmType;
			PrimitiveTypeKind primitiveTypeKind = primitiveType.PrimitiveTypeKind;
			if (primitiveTypeKind == PrimitiveTypeKind.Byte)
			{
				return expectedValue == (long)((ulong)((byte)value));
			}
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.SByte:
				return expectedValue == (long)((sbyte)value);
			case PrimitiveTypeKind.Int16:
				return expectedValue == (long)((short)value);
			case PrimitiveTypeKind.Int32:
				return expectedValue == (long)((int)value);
			case PrimitiveTypeKind.Int64:
				return expectedValue == (long)value;
			default:
				return false;
			}
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000B53B0 File Offset: 0x000B35B0
		internal static TypeUsage GetLiteralTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			return TypeHelpers.GetLiteralTypeUsage(primitiveTypeKind, true);
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000B53BC File Offset: 0x000B35BC
		internal static TypeUsage GetLiteralTypeUsage(PrimitiveTypeKind primitiveTypeKind, bool isUnicode)
		{
			PrimitiveType primitiveType = EdmProviderManifest.Instance.GetPrimitiveType(primitiveTypeKind);
			TypeUsage result;
			if (primitiveTypeKind == PrimitiveTypeKind.String)
			{
				result = TypeUsage.Create(primitiveType, new FacetValues
				{
					Unicode = new bool?(isUnicode),
					MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
					FixedLength = new bool?(false),
					Nullable = new bool?(false)
				});
			}
			else
			{
				result = TypeUsage.Create(primitiveType, new FacetValues
				{
					Nullable = new bool?(false)
				});
			}
			return result;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000B544C File Offset: 0x000B364C
		internal static bool IsCanonicalFunction(EdmFunction function)
		{
			return function.DataSpace == DataSpace.CSpace && function.NamespaceName == "Edm";
		}

		// Token: 0x04001482 RID: 5250
		internal static readonly ReadOnlyMetadataCollection<EdmMember> EmptyArrayEdmMember = new ReadOnlyMetadataCollection<EdmMember>(new MetadataCollection<EdmMember>().SetReadOnly());

		// Token: 0x04001483 RID: 5251
		internal static readonly FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember> EmptyArrayEdmProperty = new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(TypeHelpers.EmptyArrayEdmMember, null);
	}
}
