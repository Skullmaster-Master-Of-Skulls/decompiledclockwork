using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000021 RID: 33
	internal static class EdmModelSyntacticValidationRules
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00007510 File Offset: 0x00005710
		private static bool IsEdmTypeUsageValid(TypeUsage typeUsage)
		{
			HashSet<TypeUsage> visitedValidTypeUsages = new HashSet<TypeUsage>();
			return EdmModelSyntacticValidationRules.IsEdmTypeUsageValid(typeUsage, visitedValidTypeUsages);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000752A File Offset: 0x0000572A
		private static bool IsEdmTypeUsageValid(TypeUsage typeUsage, HashSet<TypeUsage> visitedValidTypeUsages)
		{
			if (visitedValidTypeUsages.Contains(typeUsage))
			{
				return false;
			}
			visitedValidTypeUsages.Add(typeUsage);
			return true;
		}

		// Token: 0x0400008B RID: 139
		internal static readonly EdmModelValidationRule<INamedDataModelItem> EdmModel_NameMustNotBeEmptyOrWhiteSpace = new EdmModelValidationRule<INamedDataModelItem>(delegate(EdmModelValidationContext context, INamedDataModelItem item)
		{
			if (string.IsNullOrWhiteSpace(item.Name))
			{
				context.AddError((MetadataItem)item, "Name", Strings.EdmModel_Validator_Syntactic_MissingName);
			}
		});

		// Token: 0x0400008C RID: 140
		internal static readonly EdmModelValidationRule<INamedDataModelItem> EdmModel_NameIsTooLong = new EdmModelValidationRule<INamedDataModelItem>(delegate(EdmModelValidationContext context, INamedDataModelItem item)
		{
			if (!string.IsNullOrWhiteSpace(item.Name) && item.Name.Length > 480 && !(item is RowType) && !(item is CollectionType))
			{
				context.AddError((MetadataItem)item, "Name", Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(item.Name));
			}
		});

		// Token: 0x0400008D RID: 141
		internal static readonly EdmModelValidationRule<INamedDataModelItem> EdmModel_NameIsNotAllowed = new EdmModelValidationRule<INamedDataModelItem>(delegate(EdmModelValidationContext context, INamedDataModelItem item)
		{
			if (string.IsNullOrWhiteSpace(item.Name) || item is RowType || item is CollectionType || (!context.IsCSpace && item is EdmProperty))
			{
				return;
			}
			if (item.Name.Contains(".") || (context.IsCSpace && !item.Name.IsValidUndottedName()))
			{
				context.AddError((MetadataItem)item, "Name", Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(item.Name));
			}
		});

		// Token: 0x0400008E RID: 142
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_AssocationEndMustNotBeNull = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.SourceEnd == null || edmAssociationType.TargetEnd == null)
			{
				context.AddError(edmAssociationType, "End", Strings.EdmModel_Validator_Syntactic_EdmAssociationType_AssocationEndMustNotBeNull);
			}
		});

		// Token: 0x0400008F RID: 143
		internal static readonly EdmModelValidationRule<ReferentialConstraint> EdmAssociationConstraint_DependentEndMustNotBeNull = new EdmModelValidationRule<ReferentialConstraint>(delegate(EdmModelValidationContext context, ReferentialConstraint edmAssociationConstraint)
		{
			if (edmAssociationConstraint.ToRole == null)
			{
				context.AddError(edmAssociationConstraint, "Dependent", Strings.EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentEndMustNotBeNull);
			}
		});

		// Token: 0x04000090 RID: 144
		internal static readonly EdmModelValidationRule<ReferentialConstraint> EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty = new EdmModelValidationRule<ReferentialConstraint>(delegate(EdmModelValidationContext context, ReferentialConstraint edmAssociationConstraint)
		{
			if (edmAssociationConstraint.ToProperties == null || !edmAssociationConstraint.ToProperties.Any<EdmProperty>())
			{
				context.AddError(edmAssociationConstraint, "Dependent", Strings.EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty);
			}
		});

		// Token: 0x04000091 RID: 145
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_AssocationMustNotBeNull = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			if (edmNavigationProperty.Association == null)
			{
				context.AddError(edmNavigationProperty, "Relationship", Strings.EdmModel_Validator_Syntactic_EdmNavigationProperty_AssocationMustNotBeNull);
			}
		});

		// Token: 0x04000092 RID: 146
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_ResultEndMustNotBeNull = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			if (edmNavigationProperty.ToEndMember == null)
			{
				context.AddError(edmNavigationProperty, "ToRole", Strings.EdmModel_Validator_Syntactic_EdmNavigationProperty_ResultEndMustNotBeNull);
			}
		});

		// Token: 0x04000093 RID: 147
		internal static readonly EdmModelValidationRule<AssociationEndMember> EdmAssociationEnd_EntityTypeMustNotBeNull = new EdmModelValidationRule<AssociationEndMember>(delegate(EdmModelValidationContext context, AssociationEndMember edmAssociationEnd)
		{
			if (edmAssociationEnd.GetEntityType() == null)
			{
				context.AddError(edmAssociationEnd, "Type", Strings.EdmModel_Validator_Syntactic_EdmAssociationEnd_EntityTypeMustNotBeNull);
			}
		});

		// Token: 0x04000094 RID: 148
		internal static readonly EdmModelValidationRule<EntitySet> EdmEntitySet_ElementTypeMustNotBeNull = new EdmModelValidationRule<EntitySet>(delegate(EdmModelValidationContext context, EntitySet edmEntitySet)
		{
			if (edmEntitySet.ElementType == null)
			{
				context.AddError(edmEntitySet, "ElementType", Strings.EdmModel_Validator_Syntactic_EdmEntitySet_ElementTypeMustNotBeNull);
			}
		});

		// Token: 0x04000095 RID: 149
		internal static readonly EdmModelValidationRule<AssociationSet> EdmAssociationSet_ElementTypeMustNotBeNull = new EdmModelValidationRule<AssociationSet>(delegate(EdmModelValidationContext context, AssociationSet edmAssociationSet)
		{
			if (edmAssociationSet.ElementType == null)
			{
				context.AddError(edmAssociationSet, "ElementType", Strings.EdmModel_Validator_Syntactic_EdmAssociationSet_ElementTypeMustNotBeNull);
			}
		});

		// Token: 0x04000096 RID: 150
		internal static readonly EdmModelValidationRule<AssociationSet> EdmAssociationSet_SourceSetMustNotBeNull = new EdmModelValidationRule<AssociationSet>(delegate(EdmModelValidationContext context, AssociationSet edmAssociationSet)
		{
			if (context.IsCSpace && edmAssociationSet.SourceSet == null)
			{
				context.AddError(edmAssociationSet, "FromRole", Strings.EdmModel_Validator_Syntactic_EdmAssociationSet_SourceSetMustNotBeNull);
			}
		});

		// Token: 0x04000097 RID: 151
		internal static readonly EdmModelValidationRule<AssociationSet> EdmAssociationSet_TargetSetMustNotBeNull = new EdmModelValidationRule<AssociationSet>(delegate(EdmModelValidationContext context, AssociationSet edmAssociationSet)
		{
			if (context.IsCSpace && edmAssociationSet.TargetSet == null)
			{
				context.AddError(edmAssociationSet, "ToRole", Strings.EdmModel_Validator_Syntactic_EdmAssociationSet_TargetSetMustNotBeNull);
			}
		});

		// Token: 0x04000098 RID: 152
		internal static readonly EdmModelValidationRule<TypeUsage> EdmTypeReference_TypeNotValid = new EdmModelValidationRule<TypeUsage>(delegate(EdmModelValidationContext context, TypeUsage edmTypeReference)
		{
			if (!EdmModelSyntacticValidationRules.IsEdmTypeUsageValid(edmTypeReference))
			{
				context.AddError(edmTypeReference, null, Strings.EdmModel_Validator_Syntactic_EdmTypeReferenceNotValid);
			}
		});
	}
}
