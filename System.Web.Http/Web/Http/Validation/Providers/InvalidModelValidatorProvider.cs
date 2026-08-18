using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;
using System.Web.Http.Validation.Validators;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x02000190 RID: 400
	public class InvalidModelValidatorProvider : AssociatedValidatorProvider
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x00022994 File Offset: 0x00020B94
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, IEnumerable<Attribute> attributes)
		{
			if (metadata.ContainerType == null || string.IsNullOrEmpty(metadata.PropertyName))
			{
				Type type = metadata.ModelType;
				PropertyInfo[] nonPublicProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
				foreach (PropertyInfo nonPublicProperty in nonPublicProperties)
				{
					if (nonPublicProperty.GetCustomAttributes(typeof(ValidationAttribute), true).Length > 0)
					{
						yield return new ErrorModelValidator(validatorProviders, Error.Format(SRResources.ValidationAttributeOnNonPublicProperty, new object[]
						{
							nonPublicProperty.Name,
							type
						}));
					}
				}
				FieldInfo[] allFields = metadata.ModelType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (FieldInfo field in allFields)
				{
					if (field.GetCustomAttributes(typeof(ValidationAttribute), true).Length > 0)
					{
						yield return new ErrorModelValidator(validatorProviders, Error.Format(SRResources.ValidationAttributeOnField, new object[]
						{
							field.Name,
							type
						}));
					}
				}
			}
			else if (metadata.ModelType.IsValueType)
			{
				if (attributes.Any((Attribute attribute) => attribute is RequiredAttribute) && !DataMemberModelValidatorProvider.IsRequiredDataMember(metadata.ContainerType, attributes))
				{
					yield return new ErrorModelValidator(validatorProviders, Error.Format(SRResources.MissingDataMemberIsRequired, new object[]
					{
						metadata.PropertyName,
						metadata.ContainerType
					}));
				}
			}
			yield break;
		}
	}
}
