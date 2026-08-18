using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000526 RID: 1318
	internal static class TypeSemantics
	{
		// Token: 0x060031D4 RID: 12756 RVA: 0x000EE42D File Offset: 0x000EC62D
		internal static bool IsEqual(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.CompareTypes(type1, type2, false);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000EE437 File Offset: 0x000EC637
		internal static bool IsStructurallyEqual(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.CompareTypes(fromType, toType, true);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000EE441 File Offset: 0x000EC641
		internal static bool IsStructurallyEqualOrPromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsStructurallyEqual(fromType, toType) || TypeSemantics.IsPromotableTo(fromType, toType);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000EE455 File Offset: 0x000EC655
		internal static bool IsStructurallyEqualOrPromotableTo(EdmType fromType, EdmType toType)
		{
			return TypeSemantics.IsStructurallyEqualOrPromotableTo(TypeUsage.Create(fromType), TypeUsage.Create(toType));
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000EE468 File Offset: 0x000EC668
		internal static bool IsSubTypeOf(TypeUsage subType, TypeUsage superType)
		{
			if (subType.EdmEquals(superType))
			{
				return true;
			}
			if (Helper.IsPrimitiveType(subType.EdmType) && Helper.IsPrimitiveType(superType.EdmType))
			{
				return TypeSemantics.IsPrimitiveTypeSubTypeOf(subType, superType);
			}
			return subType.IsSubtypeOf(superType);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000EE49E File Offset: 0x000EC69E
		internal static bool IsSubTypeOf(EdmType subEdmType, EdmType superEdmType)
		{
			return subEdmType.IsSubtypeOf(superEdmType);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000EE4A8 File Offset: 0x000EC6A8
		internal static bool IsPromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			if (toType.EdmType.EdmEquals(fromType.EdmType))
			{
				return true;
			}
			if (Helper.IsPrimitiveType(fromType.EdmType) && Helper.IsPrimitiveType(toType.EdmType))
			{
				return TypeSemantics.IsPrimitiveTypePromotableTo(fromType, toType);
			}
			if (Helper.IsCollectionType(fromType.EdmType) && Helper.IsCollectionType(toType.EdmType))
			{
				return TypeSemantics.IsPromotableTo(TypeHelpers.GetElementTypeUsage(fromType), TypeHelpers.GetElementTypeUsage(toType));
			}
			if (Helper.IsEntityTypeBase(fromType.EdmType) && Helper.IsEntityTypeBase(toType.EdmType))
			{
				return fromType.EdmType.IsSubtypeOf(toType.EdmType);
			}
			if (Helper.IsRefType(fromType.EdmType) && Helper.IsRefType(toType.EdmType))
			{
				return TypeSemantics.IsPromotableTo(TypeHelpers.GetElementTypeUsage(fromType), TypeHelpers.GetElementTypeUsage(toType));
			}
			return Helper.IsRowType(fromType.EdmType) && Helper.IsRowType(toType.EdmType) && TypeSemantics.IsPromotableTo((RowType)fromType.EdmType, (RowType)toType.EdmType);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000EE640 File Offset: 0x000EC840
		internal static IEnumerable<TypeUsage> FlattenType(TypeUsage type)
		{
			Func<TypeUsage, bool> isLeaf = (TypeUsage t) => !Helper.IsTransientType(t.EdmType);
			Func<TypeUsage, IEnumerable<TypeUsage>> getImmediateSubNodes = delegate(TypeUsage t)
			{
				if (Helper.IsCollectionType(t.EdmType) || Helper.IsRefType(t.EdmType))
				{
					return new TypeUsage[]
					{
						TypeHelpers.GetElementTypeUsage(t)
					};
				}
				if (Helper.IsRowType(t.EdmType))
				{
					return from p in ((RowType)t.EdmType).Properties
					select p.TypeUsage;
				}
				return new TypeUsage[0];
			};
			return Helpers.GetLeafNodes<TypeUsage>(type, isLeaf, getImmediateSubNodes);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000EE694 File Offset: 0x000EC894
		internal static bool IsCastAllowed(TypeUsage fromType, TypeUsage toType)
		{
			return (Helper.IsPrimitiveType(fromType.EdmType) && Helper.IsPrimitiveType(toType.EdmType)) || (Helper.IsPrimitiveType(fromType.EdmType) && Helper.IsEnumType(toType.EdmType)) || (Helper.IsEnumType(fromType.EdmType) && Helper.IsPrimitiveType(toType.EdmType)) || (Helper.IsEnumType(fromType.EdmType) && Helper.IsEnumType(toType.EdmType) && fromType.EdmType.Equals(toType.EdmType));
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000EE720 File Offset: 0x000EC920
		internal static bool TryGetCommonType(TypeUsage type1, TypeUsage type2, out TypeUsage commonType)
		{
			commonType = null;
			if (type1.EdmEquals(type2))
			{
				commonType = TypeSemantics.ForgetConstraints(type2);
				return true;
			}
			if (Helper.IsPrimitiveType(type1.EdmType) && Helper.IsPrimitiveType(type2.EdmType))
			{
				return TypeSemantics.TryGetCommonPrimitiveType(type1, type2, out commonType);
			}
			EdmType edmType;
			if (TypeSemantics.TryGetCommonType(type1.EdmType, type2.EdmType, out edmType))
			{
				commonType = TypeSemantics.ForgetConstraints(TypeUsage.Create(edmType));
				return true;
			}
			commonType = null;
			return false;
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000EE790 File Offset: 0x000EC990
		internal static TypeUsage GetCommonType(TypeUsage type1, TypeUsage type2)
		{
			TypeUsage result = null;
			if (TypeSemantics.TryGetCommonType(type1, type2, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000EE7AD File Offset: 0x000EC9AD
		internal static bool IsAggregateFunction(EdmFunction function)
		{
			return function.AggregateAttribute;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000EE7B5 File Offset: 0x000EC9B5
		internal static bool IsValidPolymorphicCast(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsPolymorphicType(fromType) && TypeSemantics.IsPolymorphicType(toType) && (TypeSemantics.IsStructurallyEqual(fromType, toType) || TypeSemantics.IsSubTypeOf(fromType, toType) || TypeSemantics.IsSubTypeOf(toType, fromType));
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000EE7E4 File Offset: 0x000EC9E4
		internal static bool IsValidPolymorphicCast(EdmType fromEdmType, EdmType toEdmType)
		{
			return TypeSemantics.IsValidPolymorphicCast(TypeUsage.Create(fromEdmType), TypeUsage.Create(toEdmType));
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000EE7F7 File Offset: 0x000EC9F7
		internal static bool IsNominalType(TypeUsage type)
		{
			return TypeSemantics.IsEntityType(type) || TypeSemantics.IsComplexType(type);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000EE809 File Offset: 0x000ECA09
		internal static bool IsCollectionType(TypeUsage type)
		{
			return Helper.IsCollectionType(type.EdmType);
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000EE816 File Offset: 0x000ECA16
		internal static bool IsComplexType(TypeUsage type)
		{
			return BuiltInTypeKind.ComplexType == type.EdmType.BuiltInTypeKind;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000EE826 File Offset: 0x000ECA26
		internal static bool IsEntityType(TypeUsage type)
		{
			return Helper.IsEntityType(type.EdmType);
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000EE833 File Offset: 0x000ECA33
		internal static bool IsRelationshipType(TypeUsage type)
		{
			return BuiltInTypeKind.AssociationType == type.EdmType.BuiltInTypeKind;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000EE843 File Offset: 0x000ECA43
		internal static bool IsEnumerationType(TypeUsage type)
		{
			return Helper.IsEnumType(type.EdmType);
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000EE850 File Offset: 0x000ECA50
		internal static bool IsScalarType(TypeUsage type)
		{
			return TypeSemantics.IsScalarType(type.EdmType);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000EE85D File Offset: 0x000ECA5D
		internal static bool IsScalarType(EdmType type)
		{
			return Helper.IsPrimitiveType(type) || Helper.IsEnumType(type);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000EE86F File Offset: 0x000ECA6F
		internal static bool IsNumericType(TypeUsage type)
		{
			return TypeSemantics.IsIntegerNumericType(type) || TypeSemantics.IsFixedPointNumericType(type) || TypeSemantics.IsFloatPointNumericType(type);
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000EE88C File Offset: 0x000ECA8C
		internal static bool IsIntegerNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			if (TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind))
			{
				PrimitiveTypeKind primitiveTypeKind2 = primitiveTypeKind;
				if (primitiveTypeKind2 != PrimitiveTypeKind.Byte)
				{
					switch (primitiveTypeKind2)
					{
					case PrimitiveTypeKind.SByte:
					case PrimitiveTypeKind.Int16:
					case PrimitiveTypeKind.Int32:
					case PrimitiveTypeKind.Int64:
						break;
					default:
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000EE8C8 File Offset: 0x000ECAC8
		internal static bool IsFixedPointNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && primitiveTypeKind == PrimitiveTypeKind.Decimal;
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000EE8E8 File Offset: 0x000ECAE8
		internal static bool IsFloatPointNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && (primitiveTypeKind == PrimitiveTypeKind.Double || primitiveTypeKind == PrimitiveTypeKind.Single);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000EE90C File Offset: 0x000ECB0C
		internal static bool IsUnsignedNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			if (TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind))
			{
				PrimitiveTypeKind primitiveTypeKind2 = primitiveTypeKind;
				return primitiveTypeKind2 == PrimitiveTypeKind.Byte;
			}
			return false;
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000EE92E File Offset: 0x000ECB2E
		internal static bool IsPolymorphicType(TypeUsage type)
		{
			return TypeSemantics.IsEntityType(type) || TypeSemantics.IsComplexType(type);
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000EE940 File Offset: 0x000ECB40
		internal static bool IsBooleanType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Boolean);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000EE949 File Offset: 0x000ECB49
		internal static bool IsPrimitiveType(TypeUsage type)
		{
			return Helper.IsPrimitiveType(type.EdmType);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000EE958 File Offset: 0x000ECB58
		internal static bool IsPrimitiveType(TypeUsage type, PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveTypeKind primitiveTypeKind2;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind2) && primitiveTypeKind2 == primitiveTypeKind;
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000EE975 File Offset: 0x000ECB75
		internal static bool IsRowType(TypeUsage type)
		{
			return Helper.IsRowType(type.EdmType);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000EE982 File Offset: 0x000ECB82
		internal static bool IsReferenceType(TypeUsage type)
		{
			return Helper.IsRefType(type.EdmType);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000EE98F File Offset: 0x000ECB8F
		internal static bool IsSpatialType(TypeUsage type)
		{
			return Helper.IsSpatialType(type);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000EE997 File Offset: 0x000ECB97
		internal static bool IsStrongSpatialType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type) && Helper.IsStrongSpatialTypeKind(((PrimitiveType)type.EdmType).PrimitiveTypeKind);
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000EE9B8 File Offset: 0x000ECBB8
		internal static bool IsStructuralType(TypeUsage type)
		{
			return Helper.IsStructuralType(type.EdmType);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000EE9C8 File Offset: 0x000ECBC8
		internal static bool IsPartOfKey(EdmMember edmMember)
		{
			if (Helper.IsRelationshipEndMember(edmMember))
			{
				return ((RelationshipType)edmMember.DeclaringType).KeyMembers.Contains(edmMember);
			}
			return Helper.IsEdmProperty(edmMember) && Helper.IsEntityTypeBase(edmMember.DeclaringType) && ((EntityTypeBase)edmMember.DeclaringType).KeyMembers.Contains(edmMember);
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000EEA24 File Offset: 0x000ECC24
		internal static bool IsNullable(TypeUsage type)
		{
			Facet facet;
			return !type.Facets.TryGetValue("Nullable", false, out facet) || (bool)facet.Value;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000EEA53 File Offset: 0x000ECC53
		internal static bool IsNullable(EdmMember edmMember)
		{
			return TypeSemantics.IsNullable(edmMember.TypeUsage);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000EEA60 File Offset: 0x000ECC60
		internal static bool IsEqualComparable(TypeUsage type)
		{
			return TypeSemantics.IsEqualComparable(type.EdmType);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000EEA6D File Offset: 0x000ECC6D
		internal static bool IsEqualComparableTo(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.IsEqualComparable(type1) && TypeSemantics.IsEqualComparable(type2) && TypeSemantics.HasCommonType(type1, type2);
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000EEA88 File Offset: 0x000ECC88
		internal static bool IsOrderComparable(TypeUsage type)
		{
			return TypeSemantics.IsOrderComparable(type.EdmType);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000EEA95 File Offset: 0x000ECC95
		internal static bool IsOrderComparableTo(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.IsOrderComparable(type1) && TypeSemantics.IsOrderComparable(type2) && TypeSemantics.HasCommonType(type1, type2);
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000EEAB0 File Offset: 0x000ECCB0
		internal static TypeUsage ForgetConstraints(TypeUsage type)
		{
			if (Helper.IsPrimitiveType(type.EdmType))
			{
				return EdmProviderManifest.Instance.ForgetScalarConstraints(type);
			}
			return type;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000EEACC File Offset: 0x000ECCCC
		[Conditional("DEBUG")]
		internal static void AssertTypeInvariant(string message, Func<bool> assertPredicate)
		{
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000EEACE File Offset: 0x000ECCCE
		private static bool IsPrimitiveTypeSubTypeOf(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsSubTypeOf((PrimitiveType)fromType.EdmType, (PrimitiveType)toType.EdmType);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000EEAF0 File Offset: 0x000ECCF0
		private static bool IsSubTypeOf(PrimitiveType subPrimitiveType, PrimitiveType superPrimitiveType)
		{
			if (object.ReferenceEquals(subPrimitiveType, superPrimitiveType))
			{
				return true;
			}
			if (Helper.AreSameSpatialUnionType(subPrimitiveType, superPrimitiveType))
			{
				return true;
			}
			ReadOnlyCollection<PrimitiveType> promotionTypes = EdmProviderManifest.Instance.GetPromotionTypes(subPrimitiveType);
			return -1 != promotionTypes.IndexOf(superPrimitiveType);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x000EEB2C File Offset: 0x000ECD2C
		private static bool IsPromotableTo(RowType fromRowType, RowType toRowType)
		{
			if (fromRowType.Properties.Count != toRowType.Properties.Count)
			{
				return false;
			}
			for (int i = 0; i < fromRowType.Properties.Count; i++)
			{
				if (!TypeSemantics.IsPromotableTo(fromRowType.Properties[i].TypeUsage, toRowType.Properties[i].TypeUsage))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000EEB95 File Offset: 0x000ECD95
		private static bool IsPrimitiveTypePromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsSubTypeOf((PrimitiveType)fromType.EdmType, (PrimitiveType)toType.EdmType);
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000EEBB8 File Offset: 0x000ECDB8
		private static bool TryGetCommonType(EdmType edmType1, EdmType edmType2, out EdmType commonEdmType)
		{
			if (edmType2 == edmType1)
			{
				commonEdmType = edmType1;
				return true;
			}
			if (Helper.IsPrimitiveType(edmType1) && Helper.IsPrimitiveType(edmType2))
			{
				return TypeSemantics.TryGetCommonType((PrimitiveType)edmType1, (PrimitiveType)edmType2, out commonEdmType);
			}
			if (Helper.IsCollectionType(edmType1) && Helper.IsCollectionType(edmType2))
			{
				return TypeSemantics.TryGetCommonType((CollectionType)edmType1, (CollectionType)edmType2, out commonEdmType);
			}
			if (Helper.IsEntityTypeBase(edmType1) && Helper.IsEntityTypeBase(edmType2))
			{
				return TypeSemantics.TryGetCommonBaseType(edmType1, edmType2, out commonEdmType);
			}
			if (Helper.IsRefType(edmType1) && Helper.IsRefType(edmType2))
			{
				return TypeSemantics.TryGetCommonType((RefType)edmType1, (RefType)edmType2, out commonEdmType);
			}
			if (Helper.IsRowType(edmType1) && Helper.IsRowType(edmType2))
			{
				return TypeSemantics.TryGetCommonType((RowType)edmType1, (RowType)edmType2, out commonEdmType);
			}
			commonEdmType = null;
			return false;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000EEC78 File Offset: 0x000ECE78
		private static bool TryGetCommonPrimitiveType(TypeUsage type1, TypeUsage type2, out TypeUsage commonType)
		{
			commonType = null;
			if (TypeSemantics.IsPromotableTo(type1, type2))
			{
				commonType = TypeSemantics.ForgetConstraints(type2);
				return true;
			}
			if (TypeSemantics.IsPromotableTo(type2, type1))
			{
				commonType = TypeSemantics.ForgetConstraints(type1);
				return true;
			}
			ReadOnlyCollection<PrimitiveType> primitiveCommonSuperTypes = TypeSemantics.GetPrimitiveCommonSuperTypes((PrimitiveType)type1.EdmType, (PrimitiveType)type2.EdmType);
			if (primitiveCommonSuperTypes.Count == 0)
			{
				return false;
			}
			commonType = TypeUsage.CreateDefaultTypeUsage(primitiveCommonSuperTypes[0]);
			return null != commonType;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000EECEC File Offset: 0x000ECEEC
		private static bool TryGetCommonType(PrimitiveType primitiveType1, PrimitiveType primitiveType2, out EdmType commonType)
		{
			commonType = null;
			if (TypeSemantics.IsSubTypeOf(primitiveType1, primitiveType2))
			{
				commonType = primitiveType2;
				return true;
			}
			if (TypeSemantics.IsSubTypeOf(primitiveType2, primitiveType1))
			{
				commonType = primitiveType1;
				return true;
			}
			ReadOnlyCollection<PrimitiveType> primitiveCommonSuperTypes = TypeSemantics.GetPrimitiveCommonSuperTypes(primitiveType1, primitiveType2);
			if (primitiveCommonSuperTypes.Count > 0)
			{
				commonType = primitiveCommonSuperTypes[0];
				return true;
			}
			return false;
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000EED38 File Offset: 0x000ECF38
		private static bool TryGetCommonType(CollectionType collectionType1, CollectionType collectionType2, out EdmType commonType)
		{
			TypeUsage elementType = null;
			if (!TypeSemantics.TryGetCommonType(collectionType1.TypeUsage, collectionType2.TypeUsage, out elementType))
			{
				commonType = null;
				return false;
			}
			commonType = new CollectionType(elementType);
			return true;
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000EED6A File Offset: 0x000ECF6A
		private static bool TryGetCommonType(RefType refType1, RefType reftype2, out EdmType commonType)
		{
			if (!TypeSemantics.TryGetCommonType(refType1.ElementType, reftype2.ElementType, out commonType))
			{
				return false;
			}
			commonType = new RefType((EntityType)commonType);
			return true;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000EED94 File Offset: 0x000ECF94
		private static bool TryGetCommonType(RowType rowType1, RowType rowType2, out EdmType commonRowType)
		{
			if (rowType1.Properties.Count != rowType2.Properties.Count || rowType1.InitializerMetadata != rowType2.InitializerMetadata)
			{
				commonRowType = null;
				return false;
			}
			List<EdmProperty> list = new List<EdmProperty>();
			for (int i = 0; i < rowType1.Properties.Count; i++)
			{
				TypeUsage typeUsage;
				if (!TypeSemantics.TryGetCommonType(rowType1.Properties[i].TypeUsage, rowType2.Properties[i].TypeUsage, out typeUsage))
				{
					commonRowType = null;
					return false;
				}
				list.Add(new EdmProperty(rowType1.Properties[i].Name, typeUsage));
			}
			commonRowType = new RowType(list, rowType1.InitializerMetadata);
			return true;
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000EEE44 File Offset: 0x000ED044
		internal static bool TryGetCommonBaseType(EdmType type1, EdmType type2, out EdmType commonBaseType)
		{
			Dictionary<EdmType, byte> dictionary = new Dictionary<EdmType, byte>();
			for (EdmType edmType = type2; edmType != null; edmType = edmType.BaseType)
			{
				dictionary.Add(edmType, 0);
			}
			for (EdmType edmType2 = type1; edmType2 != null; edmType2 = edmType2.BaseType)
			{
				if (dictionary.ContainsKey(edmType2))
				{
					commonBaseType = edmType2;
					return true;
				}
			}
			commonBaseType = null;
			return false;
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000EEE8D File Offset: 0x000ED08D
		private static bool HasCommonType(TypeUsage type1, TypeUsage type2)
		{
			return null != TypeHelpers.GetCommonTypeUsage(type1, type2);
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000EEE9C File Offset: 0x000ED09C
		private static bool IsEqualComparable(EdmType edmType)
		{
			if (Helper.IsPrimitiveType(edmType) || Helper.IsRefType(edmType) || Helper.IsEntityType(edmType) || Helper.IsEnumType(edmType))
			{
				return true;
			}
			if (Helper.IsRowType(edmType))
			{
				RowType rowType = (RowType)edmType;
				foreach (EdmProperty edmProperty in rowType.Properties)
				{
					if (!TypeSemantics.IsEqualComparable(edmProperty.TypeUsage))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000EEF30 File Offset: 0x000ED130
		private static bool IsOrderComparable(EdmType edmType)
		{
			return Helper.IsScalarType(edmType);
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000EEF38 File Offset: 0x000ED138
		private static bool CompareTypes(TypeUsage fromType, TypeUsage toType, bool equivalenceOnly)
		{
			if (object.ReferenceEquals(fromType, toType))
			{
				return true;
			}
			if (fromType.EdmType.BuiltInTypeKind != toType.EdmType.BuiltInTypeKind)
			{
				return false;
			}
			if (fromType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
			{
				return TypeSemantics.CompareTypes(((CollectionType)fromType.EdmType).TypeUsage, ((CollectionType)toType.EdmType).TypeUsage, equivalenceOnly);
			}
			if (fromType.EdmType.BuiltInTypeKind == BuiltInTypeKind.RefType)
			{
				return ((RefType)fromType.EdmType).ElementType.EdmEquals(((RefType)toType.EdmType).ElementType);
			}
			if (fromType.EdmType.BuiltInTypeKind != BuiltInTypeKind.RowType)
			{
				return fromType.EdmType.EdmEquals(toType.EdmType);
			}
			RowType rowType = (RowType)fromType.EdmType;
			RowType rowType2 = (RowType)toType.EdmType;
			if (rowType.Properties.Count != rowType2.Properties.Count)
			{
				return false;
			}
			for (int i = 0; i < rowType.Properties.Count; i++)
			{
				EdmProperty edmProperty = rowType.Properties[i];
				EdmProperty edmProperty2 = rowType2.Properties[i];
				if (!equivalenceOnly && edmProperty.Name != edmProperty2.Name)
				{
					return false;
				}
				if (!TypeSemantics.CompareTypes(edmProperty.TypeUsage, edmProperty2.TypeUsage, equivalenceOnly))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000EF08C File Offset: 0x000ED28C
		[SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Body")]
		private static void ComputeCommonTypeClosure()
		{
			if (TypeSemantics._commonTypeClosure != null)
			{
				return;
			}
			ReadOnlyCollection<PrimitiveType>[,] array = new ReadOnlyCollection<PrimitiveType>[31, 31];
			for (int i = 0; i < 31; i++)
			{
				array[i, i] = Helper.EmptyPrimitiveTypeReadOnlyCollection;
			}
			ReadOnlyCollection<PrimitiveType> storeTypes = EdmProviderManifest.Instance.GetStoreTypes();
			for (int j = 0; j < 31; j++)
			{
				for (int k = 0; k < j; k++)
				{
					array[j, k] = TypeSemantics.Intersect(EdmProviderManifest.Instance.GetPromotionTypes(storeTypes[j]), EdmProviderManifest.Instance.GetPromotionTypes(storeTypes[k]));
					array[k, j] = array[j, k];
				}
			}
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>[,]>(ref TypeSemantics._commonTypeClosure, array, null);
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000EF140 File Offset: 0x000ED340
		private static ReadOnlyCollection<PrimitiveType> Intersect(IList<PrimitiveType> types1, IList<PrimitiveType> types2)
		{
			List<PrimitiveType> list = new List<PrimitiveType>();
			for (int i = 0; i < types1.Count; i++)
			{
				if (types2.Contains(types1[i]))
				{
					list.Add(types1[i]);
				}
			}
			if (list.Count == 0)
			{
				return Helper.EmptyPrimitiveTypeReadOnlyCollection;
			}
			return new ReadOnlyCollection<PrimitiveType>(list);
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000EF194 File Offset: 0x000ED394
		private static ReadOnlyCollection<PrimitiveType> GetPrimitiveCommonSuperTypes(PrimitiveType primitiveType1, PrimitiveType primitiveType2)
		{
			TypeSemantics.ComputeCommonTypeClosure();
			return TypeSemantics._commonTypeClosure[(int)primitiveType1.PrimitiveTypeKind, (int)primitiveType2.PrimitiveTypeKind];
		}

		// Token: 0x040012C4 RID: 4804
		[SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Member")]
		private static ReadOnlyCollection<PrimitiveType>[,] _commonTypeClosure;
	}
}
