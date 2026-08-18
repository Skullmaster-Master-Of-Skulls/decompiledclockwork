using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x020007A1 RID: 1953
	internal class EntityValidatorBuilder
	{
		// Token: 0x0600583C RID: 22588 RVA: 0x0017B654 File Offset: 0x00179854
		public EntityValidatorBuilder(AttributeProvider attributeProvider)
		{
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x0017B66C File Offset: 0x0017986C
		public virtual EntityValidator BuildEntityValidator(InternalEntityEntry entityEntry)
		{
			return this.BuildTypeValidator<EntityValidator>(entityEntry.EntityType, entityEntry.EdmEntityType.Properties, entityEntry.EdmEntityType.NavigationProperties, (IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators) => new EntityValidator(propertyValidators, typeLevelValidators));
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x0017B6C1 File Offset: 0x001798C1
		protected virtual ComplexTypeValidator BuildComplexTypeValidator(Type clrType, ComplexType complexType)
		{
			return this.BuildTypeValidator<ComplexTypeValidator>(clrType, complexType.Properties, Enumerable.Empty<NavigationProperty>(), (IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators) => new ComplexTypeValidator(propertyValidators, typeLevelValidators));
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x0017B6F4 File Offset: 0x001798F4
		private T BuildTypeValidator<T>(Type clrType, IEnumerable<EdmProperty> edmProperties, IEnumerable<NavigationProperty> navigationProperties, Func<IEnumerable<PropertyValidator>, IEnumerable<IValidator>, T> validatorFactoryFunc) where T : TypeValidator
		{
			IList<PropertyValidator> list = this.BuildValidatorsForProperties(this.GetPublicInstanceProperties(clrType), edmProperties, navigationProperties);
			IEnumerable<Attribute> attributes = this._attributeProvider.GetAttributes(clrType);
			IList<IValidator> list2 = this.BuildValidationAttributeValidators(attributes);
			if (typeof(IValidatableObject).IsAssignableFrom(clrType))
			{
				list2.Add(new ValidatableObjectValidator(attributes.OfType<DisplayAttribute>().SingleOrDefault<DisplayAttribute>()));
			}
			if (!list.Any<PropertyValidator>() && !list2.Any<IValidator>())
			{
				return default(T);
			}
			return validatorFactoryFunc(list, list2);
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x0017BA24 File Offset: 0x00179C24
		protected virtual IList<PropertyValidator> BuildValidatorsForProperties(IEnumerable<PropertyInfo> clrProperties, IEnumerable<EdmProperty> edmProperties, IEnumerable<NavigationProperty> navigationProperties)
		{
			List<PropertyValidator> list = new List<PropertyValidator>();
			using (IEnumerator<PropertyInfo> enumerator = clrProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntityValidatorBuilder.<>c__DisplayClass13 CS$<>8__locals1 = new EntityValidatorBuilder.<>c__DisplayClass13();
					CS$<>8__locals1.property = enumerator.Current;
					EdmProperty edmProperty = (from p in edmProperties
					where p.Name == CS$<>8__locals1.property.Name
					select p).SingleOrDefault<EdmProperty>();
					PropertyValidator propertyValidator;
					if (edmProperty != null)
					{
						IEnumerable<ReferentialConstraint> source = from navigationProperty in navigationProperties
						let associationType = navigationProperty.RelationshipType as AssociationType
						where associationType != null
						from constraint in associationType.ReferentialConstraints
						where constraint.ToProperties.Contains(edmProperty)
						select constraint;
						propertyValidator = this.BuildPropertyValidator(CS$<>8__locals1.property, edmProperty, !source.Any<ReferentialConstraint>());
					}
					else
					{
						propertyValidator = this.BuildPropertyValidator(CS$<>8__locals1.property);
					}
					if (propertyValidator != null)
					{
						list.Add(propertyValidator);
					}
				}
			}
			return list;
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x0017BBD0 File Offset: 0x00179DD0
		protected virtual PropertyValidator BuildPropertyValidator(PropertyInfo clrProperty, EdmProperty edmProperty, bool buildFacetValidators)
		{
			List<IValidator> list = new List<IValidator>();
			IEnumerable<Attribute> attributes = this._attributeProvider.GetAttributes(clrProperty);
			list.AddRange(this.BuildValidationAttributeValidators(attributes));
			if (edmProperty.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType)
			{
				ComplexType complexType = (ComplexType)edmProperty.TypeUsage.EdmType;
				ComplexTypeValidator complexTypeValidator = this.BuildComplexTypeValidator(clrProperty.PropertyType, complexType);
				if (!list.Any<IValidator>() && complexTypeValidator == null)
				{
					return null;
				}
				return new ComplexPropertyValidator(clrProperty.Name, list, complexTypeValidator);
			}
			else
			{
				if (buildFacetValidators)
				{
					list.AddRange(this.BuildFacetValidators(clrProperty, edmProperty, attributes));
				}
				if (!list.Any<IValidator>())
				{
					return null;
				}
				return new PropertyValidator(clrProperty.Name, list);
			}
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x0017BC74 File Offset: 0x00179E74
		protected virtual PropertyValidator BuildPropertyValidator(PropertyInfo clrProperty)
		{
			IList<IValidator> list = this.BuildValidationAttributeValidators(this._attributeProvider.GetAttributes(clrProperty));
			if (list.Count <= 0)
			{
				return null;
			}
			return new PropertyValidator(clrProperty.Name, list);
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x0017BCDC File Offset: 0x00179EDC
		protected virtual IList<IValidator> BuildValidationAttributeValidators(IEnumerable<Attribute> attributes)
		{
			return (from validationAttribute in attributes
			where validationAttribute is ValidationAttribute
			select new ValidationAttributeValidator((ValidationAttribute)validationAttribute, attributes.OfType<DisplayAttribute>().SingleOrDefault<DisplayAttribute>())).ToList<IValidator>();
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x0017BD56 File Offset: 0x00179F56
		protected virtual IEnumerable<PropertyInfo> GetPublicInstanceProperties(Type type)
		{
			return from p in type.GetInstanceProperties()
			where p.IsPublic() && p.GetIndexParameters().Length == 0 && p.Getter() != null
			select p;
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x0017BDC4 File Offset: 0x00179FC4
		protected virtual IEnumerable<IValidator> BuildFacetValidators(PropertyInfo clrProperty, EdmMember edmProperty, IEnumerable<Attribute> existingAttributes)
		{
			List<ValidationAttribute> list = new List<ValidationAttribute>();
			MetadataProperty metadataProperty;
			edmProperty.MetadataProperties.TryGetValue("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", false, out metadataProperty);
			bool flag = metadataProperty != null && metadataProperty.Value != null;
			Facet facet;
			edmProperty.TypeUsage.Facets.TryGetValue("Nullable", false, out facet);
			bool flag2 = facet != null && facet.Value != null && !(bool)facet.Value;
			if (flag2 && !flag && clrProperty.PropertyType.IsNullable())
			{
				if (!existingAttributes.Any((Attribute a) => a is RequiredAttribute))
				{
					list.Add(new RequiredAttribute
					{
						AllowEmptyStrings = true
					});
				}
			}
			Facet facet2;
			edmProperty.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet2);
			if (facet2 != null && facet2.Value != null && facet2.Value is int)
			{
				if (!existingAttributes.Any((Attribute a) => a is MaxLengthAttribute))
				{
					if (!existingAttributes.Any((Attribute a) => a is StringLengthAttribute))
					{
						list.Add(new MaxLengthAttribute((int)facet2.Value));
					}
				}
			}
			return from attribute in list
			select new ValidationAttributeValidator(attribute, existingAttributes.OfType<DisplayAttribute>().SingleOrDefault<DisplayAttribute>());
		}

		// Token: 0x0400236A RID: 9066
		private readonly AttributeProvider _attributeProvider;
	}
}
