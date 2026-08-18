using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000204 RID: 516
	internal static class Helper
	{
		// Token: 0x06002209 RID: 8713 RVA: 0x00077F60 File Offset: 0x00076160
		internal static string CombineErrorMessage(IEnumerable<EdmSchemaError> errors)
		{
			StringBuilder stringBuilder = new StringBuilder(Environment.NewLine);
			int num = 0;
			foreach (EdmSchemaError edmSchemaError in errors)
			{
				if (num++ != 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(edmSchemaError.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00077FD4 File Offset: 0x000761D4
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

		// Token: 0x0600220B RID: 8715 RVA: 0x00078048 File Offset: 0x00076248
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

		// Token: 0x0600220C RID: 8716 RVA: 0x0007805F File Offset: 0x0007625F
		internal static TypeUsage GetModelTypeUsage(TypeUsage typeUsage)
		{
			return typeUsage.GetModelTypeUsage();
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00078067 File Offset: 0x00076267
		internal static TypeUsage GetModelTypeUsage(EdmMember member)
		{
			return Helper.GetModelTypeUsage(member.TypeUsage);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00078074 File Offset: 0x00076274
		internal static TypeUsage ValidateAndConvertTypeUsage(EdmProperty edmProperty, EdmProperty columnProperty, IXmlLineInfo lineInfo, string sourceLocation, List<EdmSchemaError> parsingErrors, StoreItemCollection storeItemCollection)
		{
			return Helper.ValidateAndConvertTypeUsage(edmProperty, lineInfo, sourceLocation, edmProperty.TypeUsage, columnProperty.TypeUsage, parsingErrors, storeItemCollection);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0007809C File Offset: 0x0007629C
		internal static TypeUsage ValidateAndConvertTypeUsage(EdmMember edmMember, IXmlLineInfo lineInfo, string sourceLocation, TypeUsage cspaceType, TypeUsage sspaceType, List<EdmSchemaError> parsingErrors, StoreItemCollection storeItemCollection)
		{
			TypeUsage typeUsage = sspaceType;
			if (sspaceType.EdmType.DataSpace == DataSpace.SSpace)
			{
				typeUsage = sspaceType.GetModelTypeUsage();
			}
			if (Helper.ValidateScalarTypesAreCompatible(cspaceType, typeUsage))
			{
				return typeUsage;
			}
			return null;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000780CF File Offset: 0x000762CF
		private static bool ValidateScalarTypesAreCompatible(TypeUsage cspaceType, TypeUsage storeType)
		{
			if (Helper.IsEnumType(cspaceType.EdmType))
			{
				return TypeSemantics.IsSubTypeOf(TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(cspaceType.EdmType)), storeType);
			}
			return TypeSemantics.IsSubTypeOf(cspaceType, storeType);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000780FC File Offset: 0x000762FC
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

		// Token: 0x06002212 RID: 8722 RVA: 0x0007812C File Offset: 0x0007632C
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

		// Token: 0x06002213 RID: 8723 RVA: 0x0007815C File Offset: 0x0007635C
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

		// Token: 0x06002214 RID: 8724 RVA: 0x000781B4 File Offset: 0x000763B4
		internal static bool IsAssignableFrom(EdmType firstType, EdmType secondType)
		{
			return secondType != null && (firstType.Equals(secondType) || Helper.IsSubtypeOf(secondType, firstType));
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000781D0 File Offset: 0x000763D0
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

		// Token: 0x06002216 RID: 8726 RVA: 0x000781FC File Offset: 0x000763FC
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

		// Token: 0x06002217 RID: 8727 RVA: 0x00078260 File Offset: 0x00076460
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

		// Token: 0x06002218 RID: 8728 RVA: 0x0007831C File Offset: 0x0007651C
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

		// Token: 0x06002219 RID: 8729 RVA: 0x00078388 File Offset: 0x00076588
		internal static IEnumerable<T> Concat<T>(params IEnumerable<T>[] sources)
		{
			foreach (IEnumerable<T> enumerable in sources)
			{
				if (enumerable != null)
				{
					foreach (T t in enumerable)
					{
						yield return t;
					}
					IEnumerator<T> enumerator = null;
				}
			}
			IEnumerable<T>[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00078398 File Offset: 0x00076598
		internal static void DisposeXmlReaders(IEnumerable<XmlReader> xmlReaders)
		{
			foreach (XmlReader xmlReader in xmlReaders)
			{
				((IDisposable)xmlReader).Dispose();
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000783E0 File Offset: 0x000765E0
		internal static bool IsStructuralType(EdmType type)
		{
			return Helper.IsComplexType(type) || Helper.IsEntityType(type) || Helper.IsRelationshipType(type) || Helper.IsRowType(type);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00078402 File Offset: 0x00076602
		internal static bool IsCollectionType(GlobalItem item)
		{
			return BuiltInTypeKind.CollectionType == item.BuiltInTypeKind;
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x0007840D File Offset: 0x0007660D
		internal static bool IsEntityType(EdmType type)
		{
			return BuiltInTypeKind.EntityType == type.BuiltInTypeKind;
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00078419 File Offset: 0x00076619
		internal static bool IsComplexType(EdmType type)
		{
			return BuiltInTypeKind.ComplexType == type.BuiltInTypeKind;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00078424 File Offset: 0x00076624
		internal static bool IsPrimitiveType(EdmType type)
		{
			return BuiltInTypeKind.PrimitiveType == type.BuiltInTypeKind;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x00078430 File Offset: 0x00076630
		internal static bool IsRefType(GlobalItem item)
		{
			return BuiltInTypeKind.RefType == item.BuiltInTypeKind;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0007843C File Offset: 0x0007663C
		internal static bool IsRowType(GlobalItem item)
		{
			return BuiltInTypeKind.RowType == item.BuiltInTypeKind;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00078448 File Offset: 0x00076648
		internal static bool IsAssociationType(EdmType type)
		{
			return BuiltInTypeKind.AssociationType == type.BuiltInTypeKind;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x00078448 File Offset: 0x00076648
		internal static bool IsRelationshipType(EdmType type)
		{
			return BuiltInTypeKind.AssociationType == type.BuiltInTypeKind;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00078453 File Offset: 0x00076653
		internal static bool IsEdmProperty(EdmMember member)
		{
			return BuiltInTypeKind.EdmProperty == member.BuiltInTypeKind;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0007845F File Offset: 0x0007665F
		internal static bool IsRelationshipEndMember(EdmMember member)
		{
			return member.BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0007845F File Offset: 0x0007665F
		internal static bool IsAssociationEndMember(EdmMember member)
		{
			return member.BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0007846A File Offset: 0x0007666A
		internal static bool IsNavigationProperty(EdmMember member)
		{
			return BuiltInTypeKind.NavigationProperty == member.BuiltInTypeKind;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00078476 File Offset: 0x00076676
		internal static bool IsEntityTypeBase(EdmType edmType)
		{
			return Helper.IsEntityType(edmType) || Helper.IsRelationshipType(edmType);
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00078488 File Offset: 0x00076688
		internal static bool IsTransientType(EdmType edmType)
		{
			return Helper.IsCollectionType(edmType) || Helper.IsRefType(edmType) || Helper.IsRowType(edmType);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000784A2 File Offset: 0x000766A2
		internal static bool IsEntitySet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.EntitySet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000784AE File Offset: 0x000766AE
		internal static bool IsRelationshipSet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.AssociationSet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000784B9 File Offset: 0x000766B9
		internal static bool IsEntityContainer(GlobalItem item)
		{
			return BuiltInTypeKind.EntityContainer == item.BuiltInTypeKind;
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000784C5 File Offset: 0x000766C5
		internal static bool IsEdmFunction(GlobalItem item)
		{
			return BuiltInTypeKind.EdmFunction == item.BuiltInTypeKind;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000784D4 File Offset: 0x000766D4
		internal static string GetFileNameFromUri(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
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

		// Token: 0x0600222F RID: 8751 RVA: 0x00078523 File Offset: 0x00076723
		internal static bool IsEnumType(EdmType edmType)
		{
			return BuiltInTypeKind.EnumType == edmType.BuiltInTypeKind;
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0007852F File Offset: 0x0007672F
		internal static bool IsUnboundedFacetValue(Facet facet)
		{
			return facet.Value == EdmConstants.UnboundedValue;
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0007853E File Offset: 0x0007673E
		internal static bool IsVariableFacetValue(Facet facet)
		{
			return facet.Value == EdmConstants.VariableValue;
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0007854D File Offset: 0x0007674D
		internal static bool IsScalarType(EdmType edmType)
		{
			return Helper.IsEnumType(edmType) || Helper.IsPrimitiveType(edmType);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0007855F File Offset: 0x0007675F
		internal static bool IsSpatialType(PrimitiveType type)
		{
			return Helper.IsGeographicType(type) || Helper.IsGeometricType(type);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x00078574 File Offset: 0x00076774
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

		// Token: 0x06002235 RID: 8757 RVA: 0x000785A4 File Offset: 0x000767A4
		internal static bool IsGeographicType(PrimitiveType type)
		{
			return Helper.IsGeographicTypeKind(type.PrimitiveTypeKind);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000785B1 File Offset: 0x000767B1
		internal static bool AreSameSpatialUnionType(PrimitiveType firstType, PrimitiveType secondType)
		{
			return (Helper.IsGeographicTypeKind(firstType.PrimitiveTypeKind) && Helper.IsGeographicTypeKind(secondType.PrimitiveTypeKind)) || (Helper.IsGeometricTypeKind(firstType.PrimitiveTypeKind) && Helper.IsGeometricTypeKind(secondType.PrimitiveTypeKind));
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x000785EC File Offset: 0x000767EC
		internal static bool IsGeographicTypeKind(PrimitiveTypeKind kind)
		{
			return kind == PrimitiveTypeKind.Geography || Helper.IsStrongGeographicTypeKind(kind);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000785FB File Offset: 0x000767FB
		internal static bool IsGeometricType(PrimitiveType type)
		{
			return Helper.IsGeometricTypeKind(type.PrimitiveTypeKind);
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x00078608 File Offset: 0x00076808
		internal static bool IsGeometricTypeKind(PrimitiveTypeKind kind)
		{
			return kind == PrimitiveTypeKind.Geometry || Helper.IsStrongGeometricTypeKind(kind);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00078617 File Offset: 0x00076817
		internal static bool IsStrongSpatialTypeKind(PrimitiveTypeKind kind)
		{
			return Helper.IsStrongGeometricTypeKind(kind) || Helper.IsStrongGeographicTypeKind(kind);
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00078629 File Offset: 0x00076829
		private static bool IsStrongGeometricTypeKind(PrimitiveTypeKind kind)
		{
			return kind >= PrimitiveTypeKind.GeometryPoint && kind <= PrimitiveTypeKind.GeometryCollection;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0007863A File Offset: 0x0007683A
		private static bool IsStrongGeographicTypeKind(PrimitiveTypeKind kind)
		{
			return kind >= PrimitiveTypeKind.GeographyPoint && kind <= PrimitiveTypeKind.GeographyCollection;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0007864B File Offset: 0x0007684B
		internal static bool IsSpatialType(TypeUsage type)
		{
			return type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && Helper.IsSpatialType((PrimitiveType)type.EdmType);
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00078670 File Offset: 0x00076870
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

		// Token: 0x0600223F RID: 8767 RVA: 0x000786C0 File Offset: 0x000768C0
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

		// Token: 0x06002240 RID: 8768 RVA: 0x00078714 File Offset: 0x00076914
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

		// Token: 0x06002241 RID: 8769 RVA: 0x00078753 File Offset: 0x00076953
		internal static bool IsSupportedEnumUnderlyingType(PrimitiveTypeKind typeKind)
		{
			return typeKind == PrimitiveTypeKind.Byte || typeKind == PrimitiveTypeKind.SByte || typeKind == PrimitiveTypeKind.Int16 || typeKind == PrimitiveTypeKind.Int32 || typeKind == PrimitiveTypeKind.Int64;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0007876E File Offset: 0x0007696E
		internal static bool IsEnumMemberValueInRange(PrimitiveTypeKind underlyingTypeKind, long value)
		{
			return value >= Helper._enumUnderlyingTypeRanges[underlyingTypeKind][0] && value <= Helper._enumUnderlyingTypeRanges[underlyingTypeKind][1];
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x00078795 File Offset: 0x00076995
		internal static PrimitiveType AsPrimitive(EdmType type)
		{
			if (!Helper.IsEnumType(type))
			{
				return (PrimitiveType)type;
			}
			return Helper.GetUnderlyingEdmTypeForEnumType(type);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000787AC File Offset: 0x000769AC
		internal static PrimitiveType GetUnderlyingEdmTypeForEnumType(EdmType type)
		{
			return ((EnumType)type).UnderlyingType;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000787BC File Offset: 0x000769BC
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

		// Token: 0x04000EDD RID: 3805
		internal static readonly ReadOnlyCollection<KeyValuePair<string, object>> EmptyKeyValueStringObjectList = new ReadOnlyCollection<KeyValuePair<string, object>>(new KeyValuePair<string, object>[0]);

		// Token: 0x04000EDE RID: 3806
		internal static readonly ReadOnlyCollection<string> EmptyStringList = new ReadOnlyCollection<string>(new string[0]);

		// Token: 0x04000EDF RID: 3807
		internal static readonly ReadOnlyCollection<FacetDescription> EmptyFacetDescriptionEnumerable = new ReadOnlyCollection<FacetDescription>(new FacetDescription[0]);

		// Token: 0x04000EE0 RID: 3808
		internal static readonly ReadOnlyCollection<EdmFunction> EmptyEdmFunctionReadOnlyCollection = new ReadOnlyCollection<EdmFunction>(new EdmFunction[0]);

		// Token: 0x04000EE1 RID: 3809
		internal static readonly ReadOnlyCollection<PrimitiveType> EmptyPrimitiveTypeReadOnlyCollection = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[0]);

		// Token: 0x04000EE2 RID: 3810
		internal static readonly KeyValuePair<string, object>[] EmptyKeyValueStringObjectArray = new KeyValuePair<string, object>[0];

		// Token: 0x04000EE3 RID: 3811
		internal const char PeriodSymbol = '.';

		// Token: 0x04000EE4 RID: 3812
		internal const char CommaSymbol = ',';

		// Token: 0x04000EE5 RID: 3813
		internal static readonly EdmMember[] EmptyArrayEdmProperty = new EdmMember[0];

		// Token: 0x04000EE6 RID: 3814
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
	}
}
