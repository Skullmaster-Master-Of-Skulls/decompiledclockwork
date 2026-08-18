using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200020D RID: 525
	internal static class TypeSemantics
	{
		// Token: 0x060022A7 RID: 8871 RVA: 0x0007B307 File Offset: 0x00079507
		internal static bool IsEqual(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.CompareTypes(type1, type2, false);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0007B311 File Offset: 0x00079511
		internal static bool IsStructurallyEqual(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.CompareTypes(fromType, toType, true);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0007B31B File Offset: 0x0007951B
		internal static bool IsStructurallyEqualOrPromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsStructurallyEqual(fromType, toType) || TypeSemantics.IsPromotableTo(fromType, toType);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0007B32F File Offset: 0x0007952F
		internal static bool IsStructurallyEqualOrPromotableTo(EdmType fromType, EdmType toType)
		{
			return TypeSemantics.IsStructurallyEqualOrPromotableTo(TypeUsage.Create(fromType), TypeUsage.Create(toType));
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0007B342 File Offset: 0x00079542
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

		// Token: 0x060022AC RID: 8876 RVA: 0x0007B378 File Offset: 0x00079578
		internal static bool IsSubTypeOf(EdmType subEdmType, EdmType superEdmType)
		{
			return subEdmType.IsSubtypeOf(superEdmType);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0007B384 File Offset: 0x00079584
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

		// Token: 0x060022AE RID: 8878 RVA: 0x0007B484 File Offset: 0x00079684
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

		// Token: 0x060022AF RID: 8879 RVA: 0x0007B4DC File Offset: 0x000796DC
		internal static bool IsCastAllowed(TypeUsage fromType, TypeUsage toType)
		{
			return (Helper.IsPrimitiveType(fromType.EdmType) && Helper.IsPrimitiveType(toType.EdmType)) || (Helper.IsPrimitiveType(fromType.EdmType) && Helper.IsEnumType(toType.EdmType)) || (Helper.IsEnumType(fromType.EdmType) && Helper.IsPrimitiveType(toType.EdmType)) || (Helper.IsEnumType(fromType.EdmType) && Helper.IsEnumType(toType.EdmType) && fromType.EdmType.Equals(toType.EdmType));
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0007B568 File Offset: 0x00079768
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

		// Token: 0x060022B1 RID: 8881 RVA: 0x0007B5D8 File Offset: 0x000797D8
		internal static TypeUsage GetCommonType(TypeUsage type1, TypeUsage type2)
		{
			TypeUsage result = null;
			if (TypeSemantics.TryGetCommonType(type1, type2, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0007B5F5 File Offset: 0x000797F5
		internal static bool IsAggregateFunction(EdmFunction function)
		{
			return function.AggregateAttribute;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0007B5FD File Offset: 0x000797FD
		internal static bool IsValidPolymorphicCast(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsPolymorphicType(fromType) && TypeSemantics.IsPolymorphicType(toType) && (TypeSemantics.IsStructurallyEqual(fromType, toType) || TypeSemantics.IsSubTypeOf(fromType, toType) || TypeSemantics.IsSubTypeOf(toType, fromType));
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0007B62C File Offset: 0x0007982C
		internal static bool IsValidPolymorphicCast(EdmType fromEdmType, EdmType toEdmType)
		{
			return TypeSemantics.IsValidPolymorphicCast(TypeUsage.Create(fromEdmType), TypeUsage.Create(toEdmType));
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0007B63F File Offset: 0x0007983F
		internal static bool IsNominalType(TypeUsage type)
		{
			return TypeSemantics.IsEntityType(type) || TypeSemantics.IsComplexType(type);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0007B651 File Offset: 0x00079851
		internal static bool IsCollectionType(TypeUsage type)
		{
			return Helper.IsCollectionType(type.EdmType);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0007B65E File Offset: 0x0007985E
		internal static bool IsComplexType(TypeUsage type)
		{
			return BuiltInTypeKind.ComplexType == type.EdmType.BuiltInTypeKind;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0007B66E File Offset: 0x0007986E
		internal static bool IsEntityType(TypeUsage type)
		{
			return Helper.IsEntityType(type.EdmType);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0007B67B File Offset: 0x0007987B
		internal static bool IsRelationshipType(TypeUsage type)
		{
			return BuiltInTypeKind.AssociationType == type.EdmType.BuiltInTypeKind;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0007B68B File Offset: 0x0007988B
		internal static bool IsEnumerationType(TypeUsage type)
		{
			return Helper.IsEnumType(type.EdmType);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0007B698 File Offset: 0x00079898
		internal static bool IsScalarType(TypeUsage type)
		{
			return TypeSemantics.IsScalarType(type.EdmType);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0007B6A5 File Offset: 0x000798A5
		internal static bool IsScalarType(EdmType type)
		{
			return Helper.IsPrimitiveType(type) || Helper.IsEnumType(type);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0007B6B7 File Offset: 0x000798B7
		internal static bool IsNumericType(TypeUsage type)
		{
			return TypeSemantics.IsIntegerNumericType(type) || TypeSemantics.IsFixedPointNumericType(type) || TypeSemantics.IsFloatPointNumericType(type);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0007B6D4 File Offset: 0x000798D4
		internal static bool IsIntegerNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && (primitiveTypeKind == PrimitiveTypeKind.Byte || primitiveTypeKind - PrimitiveTypeKind.SByte <= 3);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0007B6FC File Offset: 0x000798FC
		internal static bool IsFixedPointNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && primitiveTypeKind == PrimitiveTypeKind.Decimal;
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0007B71C File Offset: 0x0007991C
		internal static bool IsFloatPointNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && (primitiveTypeKind == PrimitiveTypeKind.Double || primitiveTypeKind == PrimitiveTypeKind.Single);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0007B740 File Offset: 0x00079940
		internal static bool IsUnsignedNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && primitiveTypeKind == PrimitiveTypeKind.Byte;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0007B63F File Offset: 0x0007983F
		internal static bool IsPolymorphicType(TypeUsage type)
		{
			return TypeSemantics.IsEntityType(type) || TypeSemantics.IsComplexType(type);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0007B760 File Offset: 0x00079960
		internal static bool IsBooleanType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Boolean);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0007B769 File Offset: 0x00079969
		internal static bool IsPrimitiveType(TypeUsage type)
		{
			return Helper.IsPrimitiveType(type.EdmType);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0007B778 File Offset: 0x00079978
		internal static bool IsPrimitiveType(TypeUsage type, PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveTypeKind primitiveTypeKind2;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind2) && primitiveTypeKind2 == primitiveTypeKind;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0007B795 File Offset: 0x00079995
		internal static bool IsRowType(TypeUsage type)
		{
			return Helper.IsRowType(type.EdmType);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0007B7A2 File Offset: 0x000799A2
		internal static bool IsReferenceType(TypeUsage type)
		{
			return Helper.IsRefType(type.EdmType);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0007B7AF File Offset: 0x000799AF
		internal static bool IsSpatialType(TypeUsage type)
		{
			return Helper.IsSpatialType(type);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0007B7B7 File Offset: 0x000799B7
		internal static bool IsStrongSpatialType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type) && Helper.IsStrongSpatialTypeKind(((PrimitiveType)type.EdmType).PrimitiveTypeKind);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0007B7D8 File Offset: 0x000799D8
		internal static bool IsStructuralType(TypeUsage type)
		{
			return Helper.IsStructuralType(type.EdmType);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0007B7E8 File Offset: 0x000799E8
		internal static bool IsPartOfKey(EdmMember edmMember)
		{
			if (Helper.IsRelationshipEndMember(edmMember))
			{
				return ((RelationshipType)edmMember.DeclaringType).KeyMembers.Contains(edmMember);
			}
			return Helper.IsEdmProperty(edmMember) && Helper.IsEntityTypeBase(edmMember.DeclaringType) && ((EntityTypeBase)edmMember.DeclaringType).KeyMembers.Contains(edmMember);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0007B844 File Offset: 0x00079A44
		internal static bool IsNullable(TypeUsage type)
		{
			Facet facet;
			return !type.Facets.TryGetValue("Nullable", false, out facet) || (bool)facet.Value;
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0007B873 File Offset: 0x00079A73
		internal static bool IsNullable(EdmMember edmMember)
		{
			return TypeSemantics.IsNullable(edmMember.TypeUsage);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0007B880 File Offset: 0x00079A80
		internal static bool IsEqualComparable(TypeUsage type)
		{
			return TypeSemantics.IsEqualComparable(type.EdmType);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0007B88D File Offset: 0x00079A8D
		internal static bool IsEqualComparableTo(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.IsEqualComparable(type1) && TypeSemantics.IsEqualComparable(type2) && TypeSemantics.HasCommonType(type1, type2);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x0007B8A8 File Offset: 0x00079AA8
		internal static bool IsOrderComparable(TypeUsage type)
		{
			return TypeSemantics.IsOrderComparable(type.EdmType);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x0007B8B5 File Offset: 0x00079AB5
		internal static bool IsOrderComparableTo(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.IsOrderComparable(type1) && TypeSemantics.IsOrderComparable(type2) && TypeSemantics.HasCommonType(type1, type2);
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x0007B8D0 File Offset: 0x00079AD0
		internal static TypeUsage ForgetConstraints(TypeUsage type)
		{
			if (Helper.IsPrimitiveType(type.EdmType))
			{
				return EdmProviderManifest.Instance.ForgetScalarConstraints(type);
			}
			return type;
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000089D0 File Offset: 0x00006BD0
		[Conditional("DEBUG")]
		internal static void AssertTypeInvariant(string message, Func<bool> assertPredicate)
		{
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0007B8EC File Offset: 0x00079AEC
		private static bool IsPrimitiveTypeSubTypeOf(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsSubTypeOf((PrimitiveType)fromType.EdmType, (PrimitiveType)toType.EdmType);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0007B910 File Offset: 0x00079B10
		private static bool IsSubTypeOf(PrimitiveType subPrimitiveType, PrimitiveType superPrimitiveType)
		{
			if (subPrimitiveType == superPrimitiveType)
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

		// Token: 0x060022D6 RID: 8918 RVA: 0x0007B948 File Offset: 0x00079B48
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

		// Token: 0x060022D7 RID: 8919 RVA: 0x0007B8EC File Offset: 0x00079AEC
		private static bool IsPrimitiveTypePromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsSubTypeOf((PrimitiveType)fromType.EdmType, (PrimitiveType)toType.EdmType);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0007B9B4 File Offset: 0x00079BB4
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

		// Token: 0x060022D9 RID: 8921 RVA: 0x0007BA74 File Offset: 0x00079C74
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
			return commonType != null;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0007BAE4 File Offset: 0x00079CE4
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

		// Token: 0x060022DB RID: 8923 RVA: 0x0007BB30 File Offset: 0x00079D30
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

		// Token: 0x060022DC RID: 8924 RVA: 0x0007BB62 File Offset: 0x00079D62
		private static bool TryGetCommonType(RefType refType1, RefType reftype2, out EdmType commonType)
		{
			if (!TypeSemantics.TryGetCommonType(refType1.ElementType, reftype2.ElementType, out commonType))
			{
				return false;
			}
			commonType = new RefType((EntityType)commonType);
			return true;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0007BB8C File Offset: 0x00079D8C
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

		// Token: 0x060022DE RID: 8926 RVA: 0x0007BC3C File Offset: 0x00079E3C
		private static bool TryGetCommonBaseType(EdmType type1, EdmType type2, out EdmType commonBaseType)
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

		// Token: 0x060022DF RID: 8927 RVA: 0x0007BC85 File Offset: 0x00079E85
		private static bool HasCommonType(TypeUsage type1, TypeUsage type2)
		{
			return TypeHelpers.GetCommonTypeUsage(type1, type2) != null;
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x0007BC94 File Offset: 0x00079E94
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

		// Token: 0x060022E1 RID: 8929 RVA: 0x0007BD28 File Offset: 0x00079F28
		private static bool IsOrderComparable(EdmType edmType)
		{
			return Helper.IsScalarType(edmType);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0007BD30 File Offset: 0x00079F30
		private static bool CompareTypes(TypeUsage fromType, TypeUsage toType, bool equivalenceOnly)
		{
			if (fromType == toType)
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

		// Token: 0x060022E3 RID: 8931 RVA: 0x0007BE80 File Offset: 0x0007A080
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

		// Token: 0x060022E4 RID: 8932 RVA: 0x0007BF34 File Offset: 0x0007A134
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

		// Token: 0x060022E5 RID: 8933 RVA: 0x0007BF88 File Offset: 0x0007A188
		private static ReadOnlyCollection<PrimitiveType> GetPrimitiveCommonSuperTypes(PrimitiveType primitiveType1, PrimitiveType primitiveType2)
		{
			TypeSemantics.ComputeCommonTypeClosure();
			return TypeSemantics._commonTypeClosure[(int)primitiveType1.PrimitiveTypeKind, (int)primitiveType2.PrimitiveTypeKind];
		}

		// Token: 0x04000F89 RID: 3977
		private static ReadOnlyCollection<PrimitiveType>[,] _commonTypeClosure;
	}
}
