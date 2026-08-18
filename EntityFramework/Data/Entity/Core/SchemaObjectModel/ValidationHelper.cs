using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200039B RID: 923
	internal static class ValidationHelper
	{
		// Token: 0x06002170 RID: 8560 RVA: 0x0009D5E4 File Offset: 0x0009B7E4
		internal static void ValidateFacets(SchemaElement element, SchemaType type, TypeUsageBuilder typeUsageBuilder)
		{
			if (type != null)
			{
				SchemaEnumType schemaEnumType = type as SchemaEnumType;
				if (schemaEnumType != null)
				{
					typeUsageBuilder.ValidateEnumFacets(schemaEnumType);
					return;
				}
				if (!(type is ScalarType) && typeUsageBuilder.HasUserDefinedFacets)
				{
					element.AddError(ErrorCode.FacetOnNonScalarType, EdmSchemaErrorSeverity.Error, Strings.FacetsOnNonScalarType(type.FQName));
					return;
				}
			}
			else if (typeUsageBuilder.HasUserDefinedFacets)
			{
				element.AddError(ErrorCode.IncorrectlyPlacedFacet, EdmSchemaErrorSeverity.Error, Strings.FacetDeclarationRequiresTypeAttribute);
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0009D647 File Offset: 0x0009B847
		internal static void ValidateTypeDeclaration(SchemaElement element, SchemaType type, SchemaElement typeSubElement)
		{
			if (type == null && typeSubElement == null)
			{
				element.AddError(ErrorCode.TypeNotDeclared, EdmSchemaErrorSeverity.Error, Strings.TypeMustBeDeclared);
			}
			if (type != null && typeSubElement != null)
			{
				element.AddError(ErrorCode.TypeDeclaredAsAttributeAndElement, EdmSchemaErrorSeverity.Error, Strings.TypeDeclaredAsAttributeAndElement);
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0009D677 File Offset: 0x0009B877
		internal static void ValidateRefType(SchemaElement element, SchemaType type)
		{
			if (type != null && !(type is SchemaEntityType))
			{
				element.AddError(ErrorCode.ReferenceToNonEntityType, EdmSchemaErrorSeverity.Error, Strings.ReferenceToNonEntityType(type.FQName));
			}
		}
	}
}
