using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000503 RID: 1283
	internal static class Helper
	{
		// Token: 0x06002FC3 RID: 12227 RVA: 0x000E565C File Offset: 0x000E385C
		internal static string GetAttributeValue(XPathNavigator nav, string attributeName)
		{
			nav = nav.Clone();
			string result = null;
			if (nav.MoveToAttribute(attributeName, string.Empty))
			{
				result = nav.Value;
			}
			return result;
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000E568C File Offset: 0x000E388C
		internal static object GetTypedAttributeValue(XPathNavigator nav, string attributeName, Type clrType)
		{
			nav = nav.Clone();
			object result = null;
			if (nav.MoveToAttribute(attributeName, string.Empty))
			{
				result = nav.ValueAs(clrType);
			}
			return result;
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000E56BC File Offset: 0x000E38BC
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

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000E5714 File Offset: 0x000E3914
		internal static bool IsAssignableFrom(EdmType firstType, EdmType secondType)
		{
			return secondType != null && (firstType.Equals(secondType) || Helper.IsSubtypeOf(secondType, firstType));
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000E5730 File Offset: 0x000E3930
		internal static bool IsSubtypeOf(EdmType firstType, EdmType secondType)
		{
			if (secondType == null)
			{
				return false;
			}
			for (EdmType baseType = firstType.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				if (baseType == secondType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000E575C File Offset: 0x000E395C
		internal static IList GetAllStructuralMembers(EdmType edmType)
		{
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
			{
				if (builtInTypeKind == BuiltInTypeKind.AssociationType)
				{
					return ((AssociationType)edmType).AssociationEndMembers;
				}
				if (builtInTypeKind == BuiltInTypeKind.ComplexType)
				{
					return ((ComplexType)edmType).Properties;
				}
			}
			else
			{
				if (builtInTypeKind == BuiltInTypeKind.EntityType)
				{
					return ((EntityType)edmType).Properties;
				}
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					return ((RowType)edmType).Properties;
				}
			}
			return Helper.EmptyArrayEdmProperty;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000E583C File Offset: 0x000E3A3C
		internal static AssociationEndMember GetEndThatShouldBeMappedToKey(AssociationType associationType)
		{
			if (associationType.AssociationEndMembers.Any((AssociationEndMember it) => it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.One)))
			{
				return associationType.AssociationEndMembers.SingleOrDefault((AssociationEndMember it) => it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.Many) || it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.ZeroOrOne));
			}
			if (associationType.AssociationEndMembers.Any((AssociationEndMember it) => it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.ZeroOrOne)))
			{
				return associationType.AssociationEndMembers.SingleOrDefault((AssociationEndMember it) => it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.Many));
			}
			return null;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000E58F0 File Offset: 0x000E3AF0
		internal static string GetCommaDelimitedString(IEnumerable<string> stringList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string value in stringList)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				else
				{
					flag = false;
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000E5B6C File Offset: 0x000E3D6C
		internal static IEnumerable<T> Concat<T>(params IEnumerable<T>[] sources)
		{
			foreach (IEnumerable<T> source in sources)
			{
				if (source != null)
				{
					foreach (T element in source)
					{
						yield return element;
					}
				}
			}
			yield break;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000E5B8C File Offset: 0x000E3D8C
		internal static void DisposeXmlReaders(IEnumerable<XmlReader> xmlReaders)
		{
			foreach (XmlReader xmlReader in xmlReaders)
			{
				((IDisposable)xmlReader).Dispose();
			}
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000E5BD4 File Offset: 0x000E3DD4
		internal static bool IsStructuralType(EdmType type)
		{
			return Helper.IsComplexType(type) || Helper.IsEntityType(type) || Helper.IsRelationshipType(type) || Helper.IsRowType(type);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000E5BF6 File Offset: 0x000E3DF6
		internal static bool IsCollectionType(GlobalItem item)
		{
			return BuiltInTypeKind.CollectionType == item.BuiltInTypeKind;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000E5C01 File Offset: 0x000E3E01
		internal static bool IsEntityType(EdmType type)
		{
			return BuiltInTypeKind.EntityType == type.BuiltInTypeKind;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000E5C0D File Offset: 0x000E3E0D
		internal static bool IsComplexType(EdmType type)
		{
			return BuiltInTypeKind.ComplexType == type.BuiltInTypeKind;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000E5C18 File Offset: 0x000E3E18
		internal static bool IsPrimitiveType(EdmType type)
		{
			return BuiltInTypeKind.PrimitiveType == type.BuiltInTypeKind;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000E5C24 File Offset: 0x000E3E24
		internal static bool IsRefType(GlobalItem item)
		{
			return BuiltInTypeKind.RefType == item.BuiltInTypeKind;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000E5C30 File Offset: 0x000E3E30
		internal static bool IsRowType(GlobalItem item)
		{
			return BuiltInTypeKind.RowType == item.BuiltInTypeKind;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000E5C3C File Offset: 0x000E3E3C
		internal static bool IsAssociationType(EdmType type)
		{
			return BuiltInTypeKind.AssociationType == type.BuiltInTypeKind;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000E5C47 File Offset: 0x000E3E47
		internal static bool IsRelationshipType(EdmType type)
		{
			return BuiltInTypeKind.AssociationType == type.BuiltInTypeKind;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000E5C52 File Offset: 0x000E3E52
		internal static bool IsEdmProperty(EdmMember member)
		{
			return BuiltInTypeKind.EdmProperty == member.BuiltInTypeKind;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000E5C5E File Offset: 0x000E3E5E
		internal static bool IsRelationshipEndMember(EdmMember member)
		{
			return BuiltInTypeKind.AssociationEndMember == member.BuiltInTypeKind;
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000E5C69 File Offset: 0x000E3E69
		internal static bool IsAssociationEndMember(EdmMember member)
		{
			return BuiltInTypeKind.AssociationEndMember == member.BuiltInTypeKind;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000E5C74 File Offset: 0x000E3E74
		internal static bool IsNavigationProperty(EdmMember member)
		{
			return BuiltInTypeKind.NavigationProperty == member.BuiltInTypeKind;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000E5C80 File Offset: 0x000E3E80
		internal static bool IsEntityTypeBase(EdmType edmType)
		{
			return Helper.IsEntityType(edmType) || Helper.IsRelationshipType(edmType);
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000E5C92 File Offset: 0x000E3E92
		internal static bool IsTransientType(EdmType edmType)
		{
			return Helper.IsCollectionType(edmType) || Helper.IsRefType(edmType) || Helper.IsRowType(edmType);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000E5CAC File Offset: 0x000E3EAC
		internal static bool IsAssociationSet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.AssociationSet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000E5CB7 File Offset: 0x000E3EB7
		internal static bool IsEntitySet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.EntitySet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000E5CC3 File Offset: 0x000E3EC3
		internal static bool IsRelationshipSet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.AssociationSet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000E5CCE File Offset: 0x000E3ECE
		internal static bool IsEntityContainer(GlobalItem item)
		{
			return BuiltInTypeKind.EntityContainer == item.BuiltInTypeKind;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000E5CDA File Offset: 0x000E3EDA
		internal static bool IsEdmFunction(GlobalItem item)
		{
			return BuiltInTypeKind.EdmFunction == item.BuiltInTypeKind;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000E5CE6 File Offset: 0x000E3EE6
		internal static string GetFileNameFromUri(Uri uri)
		{
			Check.NotNull<Uri>(uri, "uri");
			if (uri.IsFile)
			{
				return uri.LocalPath;
			}
			if (uri.IsAbsoluteUri)
			{
				return uri.AbsolutePath;
			}
			throw new ArgumentException(Strings.UnacceptableUri(uri), "uri");
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000E5D22 File Offset: 0x000E3F22
		internal static bool IsEnumType(EdmType edmType)
		{
			return BuiltInTypeKind.EnumType == edmType.BuiltInTypeKind;
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000E5D2E File Offset: 0x000E3F2E
		internal static bool IsUnboundedFacetValue(Facet facet)
		{
			return object.ReferenceEquals(facet.Value, EdmConstants.UnboundedValue);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000E5D40 File Offset: 0x000E3F40
		internal static bool IsVariableFacetValue(Facet facet)
		{
			return object.ReferenceEquals(facet.Value, EdmConstants.VariableValue);
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000E5D52 File Offset: 0x000E3F52
		internal static bool IsScalarType(EdmType edmType)
		{
			return Helper.IsEnumType(edmType) || Helper.IsPrimitiveType(edmType);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000E5D64 File Offset: 0x000E3F64
		internal static bool IsSpatialType(PrimitiveType type)
		{
			return Helper.IsGeographicType(type) || Helper.IsGeometricType(type);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000E5D78 File Offset: 0x000E3F78
		internal static bool IsSpatialType(EdmType type, out bool isGeographic)
		{
			PrimitiveType primitiveType = type as PrimitiveType;
			if (primitiveType == null)
			{
				isGeographic = false;
				return false;
			}
			isGeographic = Helper.IsGeographicType(primitiveType);
			return isGeographic || Helper.IsGeometricType(primitiveType);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000E5DA8 File Offset: 0x000E3FA8
		internal static bool IsGeographicType(PrimitiveType type)
		{
			return Helper.IsGeographicTypeKind(type.PrimitiveTypeKind);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000E5DB5 File Offset: 0x000E3FB5
		internal static bool AreSameSpatialUnionType(PrimitiveType firstType, PrimitiveType secondType)
		{
			return (Helper.IsGeographicTypeKind(firstType.PrimitiveTypeKind) && Helper.IsGeographicTypeKind(secondType.PrimitiveTypeKind)) || (Helper.IsGeometricTypeKind(firstType.PrimitiveTypeKind) && Helper.IsGeometricTypeKind(secondType.PrimitiveTypeKind));
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000E5DF0 File Offset: 0x000E3FF0
		internal static bool IsGeographicTypeKind(PrimitiveTypeKind kind)
		{
			return kind == PrimitiveTypeKind.Geography || Helper.IsStrongGeographicTypeKind(kind);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000E5DFF File Offset: 0x000E3FFF
		internal static bool IsGeometricType(PrimitiveType type)
		{
			return Helper.IsGeometricTypeKind(type.PrimitiveTypeKind);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000E5E0C File Offset: 0x000E400C
		internal static bool IsGeometricTypeKind(PrimitiveTypeKind kind)
		{
			return kind == PrimitiveTypeKind.Geometry || Helper.IsStrongGeometricTypeKind(kind);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000E5E1B File Offset: 0x000E401B
		internal static bool IsStrongSpatialTypeKind(PrimitiveTypeKind kind)
		{
			return Helper.IsStrongGeometricTypeKind(kind) || Helper.IsStrongGeographicTypeKind(kind);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000E5E2D File Offset: 0x000E402D
		private static bool IsStrongGeometricTypeKind(PrimitiveTypeKind kind)
		{
			return kind >= PrimitiveTypeKind.GeometryPoint && kind <= PrimitiveTypeKind.GeometryCollection;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000E5E3E File Offset: 0x000E403E
		private static bool IsStrongGeographicTypeKind(PrimitiveTypeKind kind)
		{
			return kind >= PrimitiveTypeKind.GeographyPoint && kind <= PrimitiveTypeKind.GeographyCollection;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000E5E4F File Offset: 0x000E404F
		internal static bool IsSpatialType(TypeUsage type)
		{
			return type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && Helper.IsSpatialType((PrimitiveType)type.EdmType);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000E5E74 File Offset: 0x000E4074
		internal static bool IsSpatialType(TypeUsage type, out PrimitiveTypeKind spatialType)
		{
			if (type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				PrimitiveType primitiveType = (PrimitiveType)type.EdmType;
				if (Helper.IsGeographicTypeKind(primitiveType.PrimitiveTypeKind) || Helper.IsGeometricTypeKind(primitiveType.PrimitiveTypeKind))
				{
					spatialType = primitiveType.PrimitiveTypeKind;
					return true;
				}
			}
			spatialType = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000E5EC4 File Offset: 0x000E40C4
		internal static string ToString(ParameterDirection value)
		{
			switch (value)
			{
			case ParameterDirection.Input:
				return "Input";
			case ParameterDirection.Output:
				return "Output";
			case ParameterDirection.InputOutput:
				return "InputOutput";
			case ParameterDirection.ReturnValue:
				return "ReturnValue";
			}
			return value.ToString();
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000E5F18 File Offset: 0x000E4118
		internal static string ToString(ParameterMode value)
		{
			switch (value)
			{
			case ParameterMode.In:
				return "In";
			case ParameterMode.Out:
				return "Out";
			case ParameterMode.InOut:
				return "InOut";
			case ParameterMode.ReturnValue:
				return "ReturnValue";
			default:
				return value.ToString();
			}
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000E5F62 File Offset: 0x000E4162
		internal static bool IsSupportedEnumUnderlyingType(PrimitiveTypeKind typeKind)
		{
			return typeKind == PrimitiveTypeKind.Byte || typeKind == PrimitiveTypeKind.SByte || typeKind == PrimitiveTypeKind.Int16 || typeKind == PrimitiveTypeKind.Int32 || typeKind == PrimitiveTypeKind.Int64;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000E5F7D File Offset: 0x000E417D
		internal static bool IsEnumMemberValueInRange(PrimitiveTypeKind underlyingTypeKind, long value)
		{
			return value >= Helper._enumUnderlyingTypeRanges[underlyingTypeKind][0] && value <= Helper._enumUnderlyingTypeRanges[underlyingTypeKind][1];
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000E5FA4 File Offset: 0x000E41A4
		internal static PrimitiveType AsPrimitive(EdmType type)
		{
			if (!Helper.IsEnumType(type))
			{
				return (PrimitiveType)type;
			}
			return Helper.GetUnderlyingEdmTypeForEnumType(type);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000E5FBB File Offset: 0x000E41BB
		internal static PrimitiveType GetUnderlyingEdmTypeForEnumType(EdmType type)
		{
			return ((EnumType)type).UnderlyingType;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000E5FC8 File Offset: 0x000E41C8
		internal static PrimitiveType GetSpatialNormalizedPrimitiveType(EdmType type)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			if (Helper.IsGeographicType(primitiveType) && primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Geography)
			{
				return PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Geography);
			}
			if (Helper.IsGeometricType(primitiveType) && primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Geometry)
			{
				return PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Geometry);
			}
			return primitiveType;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000E6014 File Offset: 0x000E4214
		internal static string CombineErrorMessage(IEnumerable<EdmSchemaError> errors)
		{
			StringBuilder stringBuilder = new StringBuilder(Environment.NewLine);
			int num = 0;
			foreach (EdmSchemaError value in errors)
			{
				if (num++ != 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000E6084 File Offset: 0x000E4284
		internal static string CombineErrorMessage(IEnumerable<EdmItemError> errors)
		{
			StringBuilder stringBuilder = new StringBuilder(Environment.NewLine);
			int num = 0;
			foreach (EdmItemError edmItemError in errors)
			{
				if (num++ != 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(edmItemError.Message);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000E6228 File Offset: 0x000E4428
		internal static IEnumerable<KeyValuePair<T, S>> PairEnumerations<T, S>(IBaseList<T> left, IEnumerable<S> right)
		{
			IEnumerator leftEnumerator = left.GetEnumerator();
			IEnumerator<S> rightEnumerator = right.GetEnumerator();
			while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
			{
				yield return new KeyValuePair<T, S>((T)((object)leftEnumerator.Current), rightEnumerator.Current);
			}
			yield break;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000E624C File Offset: 0x000E444C
		internal static TypeUsage GetModelTypeUsage(TypeUsage typeUsage)
		{
			return typeUsage.ModelTypeUsage;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000E6254 File Offset: 0x000E4454
		internal static TypeUsage GetModelTypeUsage(EdmMember member)
		{
			return Helper.GetModelTypeUsage(member.TypeUsage);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000E6264 File Offset: 0x000E4464
		internal static TypeUsage ValidateAndConvertTypeUsage(EdmProperty edmProperty, EdmProperty columnProperty)
		{
			return Helper.ValidateAndConvertTypeUsage(edmProperty.TypeUsage, columnProperty.TypeUsage);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000E6284 File Offset: 0x000E4484
		internal static TypeUsage ValidateAndConvertTypeUsage(TypeUsage cspaceType, TypeUsage sspaceType)
		{
			TypeUsage typeUsage = sspaceType;
			if (sspaceType.EdmType.DataSpace == DataSpace.SSpace)
			{
				typeUsage = sspaceType.ModelTypeUsage;
			}
			if (Helper.ValidateScalarTypesAreCompatible(cspaceType, typeUsage))
			{
				return typeUsage;
			}
			return null;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000E62B4 File Offset: 0x000E44B4
		private static bool ValidateScalarTypesAreCompatible(TypeUsage cspaceType, TypeUsage storeType)
		{
			if (Helper.IsEnumType(cspaceType.EdmType))
			{
				return TypeSemantics.IsSubTypeOf(TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(cspaceType.EdmType)), storeType);
			}
			return TypeSemantics.IsSubTypeOf(cspaceType, storeType);
		}

		// Token: 0x0400123E RID: 4670
		internal const char PeriodSymbol = '.';

		// Token: 0x0400123F RID: 4671
		internal const char CommaSymbol = ',';

		// Token: 0x04001240 RID: 4672
		internal static readonly EdmMember[] EmptyArrayEdmProperty = new EdmMember[0];

		// Token: 0x04001241 RID: 4673
		private static readonly Dictionary<PrimitiveTypeKind, long[]> _enumUnderlyingTypeRanges = new Dictionary<PrimitiveTypeKind, long[]>
		{
			{
				PrimitiveTypeKind.Byte,
				new long[]
				{
					0L,
					255L
				}
			},
			{
				PrimitiveTypeKind.SByte,
				new long[]
				{
					-128L,
					127L
				}
			},
			{
				PrimitiveTypeKind.Int16,
				new long[]
				{
					-32768L,
					32767L
				}
			},
			{
				PrimitiveTypeKind.Int32,
				new long[]
				{
					-2147483648L,
					2147483647L
				}
			},
			{
				PrimitiveTypeKind.Int64,
				new long[]
				{
					long.MinValue,
					long.MaxValue
				}
			}
		};

		// Token: 0x04001242 RID: 4674
		internal static readonly ReadOnlyCollection<KeyValuePair<string, object>> EmptyKeyValueStringObjectList = new ReadOnlyCollection<KeyValuePair<string, object>>(new KeyValuePair<string, object>[0]);

		// Token: 0x04001243 RID: 4675
		internal static readonly ReadOnlyCollection<string> EmptyStringList = new ReadOnlyCollection<string>(new string[0]);

		// Token: 0x04001244 RID: 4676
		internal static readonly ReadOnlyCollection<FacetDescription> EmptyFacetDescriptionEnumerable = new ReadOnlyCollection<FacetDescription>(new FacetDescription[0]);

		// Token: 0x04001245 RID: 4677
		internal static readonly ReadOnlyCollection<EdmFunction> EmptyEdmFunctionReadOnlyCollection = new ReadOnlyCollection<EdmFunction>(new EdmFunction[0]);

		// Token: 0x04001246 RID: 4678
		internal static readonly ReadOnlyCollection<PrimitiveType> EmptyPrimitiveTypeReadOnlyCollection = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[0]);

		// Token: 0x04001247 RID: 4679
		internal static readonly KeyValuePair<string, object>[] EmptyKeyValueStringObjectArray = new KeyValuePair<string, object>[0];
	}
}
