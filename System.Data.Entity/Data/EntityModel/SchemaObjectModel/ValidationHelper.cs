using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002E0 RID: 736
	internal static class ValidationHelper
	{
		// Token: 0x06002C52 RID: 11346 RVA: 0x000A86C4 File Offset: 0x000A68C4
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

		// Token: 0x06002C53 RID: 11347 RVA: 0x000A8727 File Offset: 0x000A6927
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

		// Token: 0x06002C54 RID: 11348 RVA: 0x000A8757 File Offset: 0x000A6957
		internal static void ValidateRefType(SchemaElement element, SchemaType type)
		{
			if (type != null && !(type is SchemaEntityType))
			{
				element.AddError(ErrorCode.ReferenceToNonEntityType, EdmSchemaErrorSeverity.Error, Strings.ReferenceToNonEntityType(type.FQName));
			}
		}
	}
}
