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
	// Token: 0x0200018E RID: 398
	public class DataAnnotationsModelValidatorProvider : AssociatedValidatorProvider
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00021EA0 File Offset: 0x000200A0
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, IEnumerable<Attribute> attributes)
		{
			List<ModelValidator> list = new List<ModelValidator>();
			foreach (ValidationAttribute validationAttribute in attributes.OfType<ValidationAttribute>())
			{
				DataAnnotationsModelValidationFactory defaultAttributeFactory;
				if (!this.AttributeFactories.TryGetValue(validationAttribute.GetType(), out defaultAttributeFactory))
				{
					defaultAttributeFactory = this.DefaultAttributeFactory;
				}
				list.Add(defaultAttributeFactory(validatorProviders, validationAttribute));
			}
			if (typeof(IValidatableObject).IsAssignableFrom(metadata.ModelType))
			{
				DataAnnotationsValidatableObjectAdapterFactory defaultValidatableFactory;
				if (!this.ValidatableFactories.TryGetValue(metadata.ModelType, out defaultValidatableFactory))
				{
					defaultValidatableFactory = this.DefaultValidatableFactory;
				}
				list.Add(defaultValidatableFactory(validatorProviders));
			}
			return list;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00021F94 File Offset: 0x00020194
		public void RegisterAdapter(Type attributeType, Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeType(attributeType);
			DataAnnotationsModelValidatorProvider.ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetAttributeAdapterConstructor(attributeType, adapterType);
			this.AttributeFactories[attributeType] = ((IEnumerable<ModelValidatorProvider> context, ValidationAttribute attribute) => (ModelValidator)constructor.Invoke(new object[]
			{
				context,
				attribute
			}));
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00021FD8 File Offset: 0x000201D8
		public void RegisterAdapterFactory(Type attributeType, DataAnnotationsModelValidationFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeType(attributeType);
			DataAnnotationsModelValidatorProvider.ValidateAttributeFactory(factory);
			this.AttributeFactories[attributeType] = factory;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002202C File Offset: 0x0002022C
		public void RegisterDefaultAdapter(Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetAttributeAdapterConstructor(typeof(ValidationAttribute), adapterType);
			this.DefaultAttributeFactory = ((IEnumerable<ModelValidatorProvider> context, ValidationAttribute attribute) => (ModelValidator)constructor.Invoke(new object[]
			{
				context,
				attribute
			}));
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002206D File Offset: 0x0002026D
		public void RegisterDefaultAdapterFactory(DataAnnotationsModelValidationFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeFactory(factory);
			this.DefaultAttributeFactory = factory;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002207C File Offset: 0x0002027C
		private static ConstructorInfo GetAttributeAdapterConstructor(Type attributeType, Type adapterType)
		{
			ConstructorInfo constructor = adapterType.GetConstructor(new Type[]
			{
				typeof(IEnumerable<ModelValidatorProvider>),
				attributeType
			});
			if (constructor == null)
			{
				throw Error.Argument("adapterType", SRResources.DataAnnotationsModelValidatorProvider_ConstructorRequirements, new object[]
				{
					adapterType.Name,
					typeof(ModelMetadata).Name,
					"IEnumerable<" + typeof(ModelValidatorProvider).Name + ">",
					attributeType.Name
				});
			}
			return constructor;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00022110 File Offset: 0x00020310
		private static void ValidateAttributeAdapterType(Type adapterType)
		{
			if (adapterType == null)
			{
				throw Error.ArgumentNull("adapterType");
			}
			if (!typeof(ModelValidator).IsAssignableFrom(adapterType))
			{
				throw Error.Argument("adapterType", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					adapterType.Name,
					typeof(ModelValidator).Name
				});
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00022178 File Offset: 0x00020378
		private static void ValidateAttributeType(Type attributeType)
		{
			if (attributeType == null)
			{
				throw Error.ArgumentNull("attributeType");
			}
			if (!typeof(ValidationAttribute).IsAssignableFrom(attributeType))
			{
				throw Error.Argument("attributeType", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					attributeType.Name,
					typeof(ValidationAttribute).Name
				});
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000221DE File Offset: 0x000203DE
		private static void ValidateAttributeFactory(DataAnnotationsModelValidationFactory factory)
		{
			if (factory == null)
			{
				throw Error.ArgumentNull("factory");
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00022224 File Offset: 0x00020424
		public void RegisterValidatableObjectAdapter(Type modelType, Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableModelType(modelType);
			DataAnnotationsModelValidatorProvider.ValidateValidatableAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetValidatableAdapterConstructor(adapterType);
			this.ValidatableFactories[modelType] = ((IEnumerable<ModelValidatorProvider> context) => (ModelValidator)constructor.Invoke(new object[]
			{
				context
			}));
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00022267 File Offset: 0x00020467
		public void RegisterValidatableObjectAdapterFactory(Type modelType, DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableModelType(modelType);
			DataAnnotationsModelValidatorProvider.ValidateValidatableFactory(factory);
			this.ValidatableFactories[modelType] = factory;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000222B8 File Offset: 0x000204B8
		public void RegisterDefaultValidatableObjectAdapter(Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetValidatableAdapterConstructor(adapterType);
			this.DefaultValidatableFactory = ((IEnumerable<ModelValidatorProvider> context) => (ModelValidator)constructor.Invoke(new object[]
			{
				context
			}));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000222EF File Offset: 0x000204EF
		public void RegisterDefaultValidatableObjectAdapterFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableFactory(factory);
			this.DefaultValidatableFactory = factory;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00022300 File Offset: 0x00020500
		private static ConstructorInfo GetValidatableAdapterConstructor(Type adapterType)
		{
			ConstructorInfo constructor = adapterType.GetConstructor(new Type[]
			{
				typeof(IEnumerable<ModelValidatorProvider>)
			});
			if (constructor == null)
			{
				throw Error.Argument("adapterType", SRResources.DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements, new object[]
				{
					adapterType.Name,
					typeof(ModelMetadata).Name,
					"IEnumerable<" + typeof(ModelValidatorProvider).Name + ">"
				});
			}
			return constructor;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00022388 File Offset: 0x00020588
		private static void ValidateValidatableAdapterType(Type adapterType)
		{
			if (adapterType == null)
			{
				throw Error.ArgumentNull("adapterType");
			}
			if (!typeof(ModelValidator).IsAssignableFrom(adapterType))
			{
				throw Error.Argument("adapterType", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					adapterType.Name,
					typeof(ModelValidator).Name
				});
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000223F0 File Offset: 0x000205F0
		private static void ValidateValidatableModelType(Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (!typeof(IValidatableObject).IsAssignableFrom(modelType))
			{
				throw Error.Argument("modelType", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					modelType.Name,
					typeof(IValidatableObject).Name
				});
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00022456 File Offset: 0x00020656
		private static void ValidateValidatableFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			if (factory == null)
			{
				throw Error.ArgumentNull("factory");
			}
		}

		// Token: 0x04000305 RID: 773
		internal DataAnnotationsModelValidationFactory DefaultAttributeFactory = (IEnumerable<ModelValidatorProvider> validationProviders, ValidationAttribute attribute) => new DataAnnotationsModelValidator(validationProviders, attribute);

		// Token: 0x04000306 RID: 774
		internal Dictionary<Type, DataAnnotationsModelValidationFactory> AttributeFactories = new Dictionary<Type, DataAnnotationsModelValidationFactory>();

		// Token: 0x04000307 RID: 775
		internal DataAnnotationsValidatableObjectAdapterFactory DefaultValidatableFactory = (IEnumerable<ModelValidatorProvider> validationProviders) => new ValidatableObjectAdapter(validationProviders);

		// Token: 0x04000308 RID: 776
		internal Dictionary<Type, DataAnnotationsValidatableObjectAdapterFactory> ValidatableFactories = new Dictionary<Type, DataAnnotationsValidatableObjectAdapterFactory>();
	}
}
