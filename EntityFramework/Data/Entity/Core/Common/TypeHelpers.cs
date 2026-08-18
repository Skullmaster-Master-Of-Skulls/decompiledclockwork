using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x02000525 RID: 1317
	internal static class TypeHelpers
	{
		// Token: 0x060031A0 RID: 12704 RVA: 0x000ED830 File Offset: 0x000EBA30
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CSpace")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "PrimitiveType")]
		[Conditional("DEBUG")]
		internal static void AssertEdmType(TypeUsage typeUsage)
		{
			EdmType edmType = typeUsage.EdmType;
			if (TypeSemantics.IsCollectionType(typeUsage))
			{
				return;
			}
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
						typeUsage
					}));
				}
			}
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000ED8F4 File Offset: 0x000EBAF4
		[Conditional("DEBUG")]
		internal static void AssertEdmType(DbCommandTree commandTree)
		{
			DbQueryCommandTree dbQueryCommandTree = commandTree as DbQueryCommandTree;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000ED90C File Offset: 0x000EBB0C
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

		// Token: 0x060031A3 RID: 12707 RVA: 0x000ED988 File Offset: 0x000EBB88
		internal static bool IsValidGroupKeyType(TypeUsage typeUsage)
		{
			return TypeHelpers.IsSetComparableOpType(typeUsage);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000ED990 File Offset: 0x000EBB90
		internal static bool IsValidDistinctOpType(TypeUsage typeUsage)
		{
			return TypeHelpers.IsSetComparableOpType(typeUsage);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000ED998 File Offset: 0x000EBB98
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

		// Token: 0x060031A6 RID: 12710 RVA: 0x000EDA44 File Offset: 0x000EBC44
		internal static bool IsValidIsNullOpType(TypeUsage typeUsage)
		{
			return TypeSemantics.IsReferenceType(typeUsage) || TypeSemantics.IsEntityType(typeUsage) || TypeSemantics.IsScalarType(typeUsage) || TypeSemantics.IsRowType(typeUsage);
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000EDA66 File Offset: 0x000EBC66
		internal static bool IsValidInOpType(TypeUsage typeUsage)
		{
			return TypeSemantics.IsReferenceType(typeUsage) || TypeSemantics.IsEntityType(typeUsage) || TypeSemantics.IsScalarType(typeUsage);
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000EDA80 File Offset: 0x000EBC80
		internal static TypeUsage GetCommonTypeUsage(TypeUsage typeUsage1, TypeUsage typeUsage2)
		{
			return TypeSemantics.GetCommonType(typeUsage1, typeUsage2);
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000EDA8C File Offset: 0x000EBC8C
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

		// Token: 0x060031AA RID: 12714 RVA: 0x000EDAEC File Offset: 0x000EBCEC
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
			return null != promotableType;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000EDB50 File Offset: 0x000EBD50
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

		// Token: 0x060031AC RID: 12716 RVA: 0x000EDB8C File Offset: 0x000EBD8C
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

		// Token: 0x060031AD RID: 12717 RVA: 0x000EDBD0 File Offset: 0x000EBDD0
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

		// Token: 0x060031AE RID: 12718 RVA: 0x000EDC19 File Offset: 0x000EBE19
		internal static bool TryGetIsFixedLength(TypeUsage type, out bool isFixedLength)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String) && !TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				isFixedLength = false;
				return false;
			}
			return TypeHelpers.TryGetBooleanFacetValue(type, "FixedLength", out isFixedLength);
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000EDC3F File Offset: 0x000EBE3F
		internal static bool TryGetIsUnicode(TypeUsage type, out bool isUnicode)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String))
			{
				isUnicode = false;
				return false;
			}
			return TypeHelpers.TryGetBooleanFacetValue(type, "Unicode", out isUnicode);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000EDC5C File Offset: 0x000EBE5C
		internal static bool IsFacetValueConstant(TypeUsage type, string facetName)
		{
			return Helper.GetFacet(((PrimitiveType)type.EdmType).FacetDescriptions, facetName).IsConstant;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000EDC79 File Offset: 0x000EBE79
		internal static bool TryGetMaxLength(TypeUsage type, out int maxLength)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String) && !TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				maxLength = 0;
				return false;
			}
			return TypeHelpers.TryGetIntFacetValue(type, "MaxLength", out maxLength);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000EDC9F File Offset: 0x000EBE9F
		internal static bool TryGetPrecision(TypeUsage type, out byte precision)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				precision = 0;
				return false;
			}
			return TypeHelpers.TryGetByteFacetValue(type, "Precision", out precision);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000EDCBB File Offset: 0x000EBEBB
		internal static bool TryGetScale(TypeUsage type, out byte scale)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				scale = 0;
				return false;
			}
			return TypeHelpers.TryGetByteFacetValue(type, "Scale", out scale);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000EDCD7 File Offset: 0x000EBED7
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

		// Token: 0x060031B5 RID: 12725 RVA: 0x000EDD0B File Offset: 0x000EBF0B
		internal static CollectionType CreateCollectionType(TypeUsage elementType)
		{
			return new CollectionType(elementType);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000EDD13 File Offset: 0x000EBF13
		internal static TypeUsage CreateCollectionTypeUsage(TypeUsage elementType)
		{
			return TypeUsage.Create(new CollectionType(elementType));
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000EDD20 File Offset: 0x000EBF20
		internal static RowType CreateRowType(IEnumerable<KeyValuePair<string, TypeUsage>> columns)
		{
			return TypeHelpers.CreateRowType(columns, null);
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000EDD2C File Offset: 0x000EBF2C
		internal static RowType CreateRowType(IEnumerable<KeyValuePair<string, TypeUsage>> columns, InitializerMetadata initializerMetadata)
		{
			List<EdmProperty> list = new List<EdmProperty>();
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in columns)
			{
				list.Add(new EdmProperty(keyValuePair.Key, keyValuePair.Value));
			}
			return new RowType(list, initializerMetadata);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000EDD94 File Offset: 0x000EBF94
		internal static TypeUsage CreateRowTypeUsage(IEnumerable<KeyValuePair<string, TypeUsage>> columns)
		{
			return TypeUsage.Create(TypeHelpers.CreateRowType(columns));
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000EDDA1 File Offset: 0x000EBFA1
		internal static RefType CreateReferenceType(EntityTypeBase entityType)
		{
			return new RefType((EntityType)entityType);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000EDDAE File Offset: 0x000EBFAE
		internal static TypeUsage CreateReferenceTypeUsage(EntityType entityType)
		{
			return TypeUsage.Create(TypeHelpers.CreateReferenceType(entityType));
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000EDDBC File Offset: 0x000EBFBC
		internal static RowType CreateKeyRowType(EntityTypeBase entityType)
		{
			IEnumerable<EdmMember> keyMembers = entityType.KeyMembers;
			if (keyMembers == null)
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntityTypeNullKeyMembersInvalid, "entityType");
			}
			List<KeyValuePair<string, TypeUsage>> list = new List<KeyValuePair<string, TypeUsage>>();
			foreach (EdmMember edmMember in keyMembers)
			{
				EdmProperty edmProperty = (EdmProperty)edmMember;
				list.Add(new KeyValuePair<string, TypeUsage>(edmProperty.Name, Helper.GetModelTypeUsage(edmProperty)));
			}
			if (list.Count < 1)
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid, "entityType");
			}
			return TypeHelpers.CreateRowType(list);
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000EDE58 File Offset: 0x000EC058
		internal static TypeUsage GetPrimitiveTypeUsageForScalar(TypeUsage scalarType)
		{
			if (!TypeSemantics.IsEnumerationType(scalarType))
			{
				return scalarType;
			}
			return TypeHelpers.CreateEnumUnderlyingTypeUsage(scalarType);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000EDE6A File Offset: 0x000EC06A
		internal static TypeUsage CreateEnumUnderlyingTypeUsage(TypeUsage enumTypeUsage)
		{
			return TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(enumTypeUsage.EdmType), enumTypeUsage.Facets);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000EDE82 File Offset: 0x000EC082
		internal static TypeUsage CreateSpatialUnionTypeUsage(TypeUsage spatialTypeUsage)
		{
			return TypeUsage.Create(Helper.GetSpatialNormalizedPrimitiveType(spatialTypeUsage.EdmType), spatialTypeUsage.Facets);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000EDE9A File Offset: 0x000EC09A
		internal static IBaseList<EdmMember> GetAllStructuralMembers(TypeUsage type)
		{
			return TypeHelpers.GetAllStructuralMembers(type.EdmType);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000EDEA8 File Offset: 0x000EC0A8
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

		// Token: 0x060031C2 RID: 12738 RVA: 0x000EDF1F File Offset: 0x000EC11F
		internal static IEnumerable GetDeclaredStructuralMembers(TypeUsage type)
		{
			return TypeHelpers.GetDeclaredStructuralMembers(type.EdmType);
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000EDF2C File Offset: 0x000EC12C
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

		// Token: 0x060031C4 RID: 12740 RVA: 0x000EDF8F File Offset: 0x000EC18F
		internal static ReadOnlyMetadataCollection<EdmProperty> GetProperties(TypeUsage typeUsage)
		{
			return TypeHelpers.GetProperties(typeUsage.EdmType);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000EDF9C File Offset: 0x000EC19C
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

		// Token: 0x060031C6 RID: 12742 RVA: 0x000EDFE9 File Offset: 0x000EC1E9
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

		// Token: 0x060031C7 RID: 12743 RVA: 0x000EE024 File Offset: 0x000EC224
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

		// Token: 0x060031C8 RID: 12744 RVA: 0x000EE07C File Offset: 0x000EC27C
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

		// Token: 0x060031C9 RID: 12745 RVA: 0x000EE0A8 File Offset: 0x000EC2A8
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

		// Token: 0x060031CA RID: 12746 RVA: 0x000EE0DF File Offset: 0x000EC2DF
		internal static TEdmType GetEdmType<TEdmType>(TypeUsage typeUsage) where TEdmType : EdmType
		{
			return (TEdmType)((object)typeUsage.EdmType);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000EE0EC File Offset: 0x000EC2EC
		internal static bool TryGetEdmType<TEdmType>(TypeUsage typeUsage, out TEdmType type) where TEdmType : EdmType
		{
			type = (typeUsage.EdmType as TEdmType);
			return type != null;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000EE115 File Offset: 0x000EC315
		internal static TypeUsage GetReadOnlyType(TypeUsage type)
		{
			if (!type.IsReadOnly)
			{
				type.SetReadOnly();
			}
			return type;
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000EE128 File Offset: 0x000EC328
		internal static string GetFullName(string qualifier, string name)
		{
			if (!string.IsNullOrEmpty(qualifier))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
				{
					qualifier,
					name
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				name
			});
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000EE178 File Offset: 0x000EC378
		internal static DbType ConvertClrTypeToDbType(Type clrType)
		{
			switch (Type.GetTypeCode(clrType))
			{
			case TypeCode.Empty:
				throw new ArgumentException(Strings.ADP_InvalidDataType(TypeCode.Empty.ToString()));
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
			throw new ArgumentException(Strings.ADP_UnknownDataTypeCode(((int)Type.GetTypeCode(clrType)).ToString(CultureInfo.InvariantCulture), clrType.FullName));
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000EE2B4 File Offset: 0x000EC4B4
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

		// Token: 0x060031D0 RID: 12752 RVA: 0x000EE338 File Offset: 0x000EC538
		internal static TypeUsage GetLiteralTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			return TypeHelpers.GetLiteralTypeUsage(primitiveTypeKind, true);
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000EE344 File Offset: 0x000EC544
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

		// Token: 0x060031D2 RID: 12754 RVA: 0x000EE3DC File Offset: 0x000EC5DC
		internal static bool IsCanonicalFunction(EdmFunction function)
		{
			return function.DataSpace == DataSpace.CSpace && function.NamespaceName == "Edm";
		}

		// Token: 0x040012C2 RID: 4802
		internal static readonly ReadOnlyMetadataCollection<EdmMember> EmptyArrayEdmMember = new ReadOnlyMetadataCollection<EdmMember>(new MetadataCollection<EdmMember>().SetReadOnly());

		// Token: 0x040012C3 RID: 4803
		internal static readonly FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember> EmptyArrayEdmProperty = new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(TypeHelpers.EmptyArrayEdmMember, null);
	}
}
