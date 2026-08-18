using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace System.Web.ModelBinding
{
	// Token: 0x02000649 RID: 1609
	public class DataAnnotationsModelValidatorProvider : AssociatedValidatorProvider
	{
		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x00113D28 File Offset: 0x00111F28
		// (set) Token: 0x06004F79 RID: 20345 RVA: 0x00113D2F File Offset: 0x00111F2F
		public static bool AddImplicitRequiredAttributeForValueTypes
		{
			get
			{
				return DataAnnotationsModelValidatorProvider._addImplicitRequiredAttributeForValueTypes;
			}
			set
			{
				DataAnnotationsModelValidatorProvider._addImplicitRequiredAttributeForValueTypes = value;
			}
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00113D38 File Offset: 0x00111F38
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context, IEnumerable<Attribute> attributes)
		{
			DataAnnotationsModelValidatorProvider._adaptersLock.EnterReadLock();
			IEnumerable<ModelValidator> result;
			try
			{
				List<ModelValidator> list = new List<ModelValidator>();
				if (DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes && metadata.IsRequired)
				{
					if (!attributes.Any((Attribute a) => a is RequiredAttribute))
					{
						attributes = attributes.Concat(new RequiredAttribute[]
						{
							new RequiredAttribute()
						});
					}
				}
				foreach (ValidationAttribute validationAttribute in attributes.OfType<ValidationAttribute>())
				{
					DataAnnotationsModelValidationFactory defaultAttributeFactory;
					if (!DataAnnotationsModelValidatorProvider.AttributeFactories.TryGetValue(validationAttribute.GetType(), out defaultAttributeFactory))
					{
						defaultAttributeFactory = DataAnnotationsModelValidatorProvider.DefaultAttributeFactory;
					}
					list.Add(defaultAttributeFactory(metadata, context, validationAttribute));
				}
				if (typeof(IValidatableObject).IsAssignableFrom(metadata.ModelType))
				{
					DataAnnotationsValidatableObjectAdapterFactory defaultValidatableFactory;
					if (!DataAnnotationsModelValidatorProvider.ValidatableFactories.TryGetValue(metadata.ModelType, out defaultValidatableFactory))
					{
						defaultValidatableFactory = DataAnnotationsModelValidatorProvider.DefaultValidatableFactory;
					}
					list.Add(defaultValidatableFactory(metadata, context));
				}
				result = list;
			}
			finally
			{
				DataAnnotationsModelValidatorProvider._adaptersLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x00113E64 File Offset: 0x00112064
		public static void RegisterAdapter(Type attributeType, Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeType(attributeType);
			DataAnnotationsModelValidatorProvider.ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetAttributeAdapterConstructor(attributeType, adapterType);
			DataAnnotationsModelValidatorProvider._adaptersLock.EnterWriteLock();
			try
			{
				DataAnnotationsModelValidatorProvider.AttributeFactories[attributeType] = ((ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) => (ModelValidator)constructor.Invoke(new object[]
				{
					metadata,
					context,
					attribute
				}));
			}
			finally
			{
				DataAnnotationsModelValidatorProvider._adaptersLock.ExitWriteLock();
			}
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x00113ED0 File Offset: 0x001120D0
		public static void RegisterAdapterFactory(Type attributeType, DataAnnotationsModelValidationFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeType(attributeType);
			DataAnnotationsModelValidatorProvider.ValidateAttributeFactory(factory);
			DataAnnotationsModelValidatorProvider._adaptersLock.EnterWriteLock();
			try
			{
				DataAnnotationsModelValidatorProvider.AttributeFactories[attributeType] = factory;
			}
			finally
			{
				DataAnnotationsModelValidatorProvider._adaptersLock.ExitWriteLock();
			}
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x00113F1C File Offset: 0x0011211C
		public static void RegisterDefaultAdapter(Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetAttributeAdapterConstructor(typeof(ValidationAttribute), adapterType);
			DataAnnotationsModelValidatorProvider.DefaultAttributeFactory = ((ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) => (ModelValidator)constructor.Invoke(new object[]
			{
				metadata,
				context,
				attribute
			}));
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00113F5C File Offset: 0x0011215C
		public static void RegisterDefaultAdapterFactory(DataAnnotationsModelValidationFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeFactory(factory);
			DataAnnotationsModelValidatorProvider.DefaultAttributeFactory = factory;
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x00113F6C File Offset: 0x0011216C
		private static ConstructorInfo GetAttributeAdapterConstructor(Type attributeType, Type adapterType)
		{
			ConstructorInfo constructor = adapterType.GetConstructor(new Type[]
			{
				typeof(ModelMetadata),
				typeof(ModelBindingExecutionContext),
				attributeType
			});
			if (constructor == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("DataAnnotationsModelValidatorProvider_ConstructorRequirements"), new object[]
				{
					adapterType.FullName,
					typeof(ModelMetadata).FullName,
					typeof(ModelBindingExecutionContext).FullName,
					attributeType.FullName
				}), "adapterType");
			}
			return constructor;
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x0011400C File Offset: 0x0011220C
		private static void ValidateAttributeAdapterType(Type adapterType)
		{
			if (adapterType == null)
			{
				throw new ArgumentNullException("adapterType");
			}
			if (!typeof(ModelValidator).IsAssignableFrom(adapterType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Common_TypeMustDriveFromType"), new object[]
				{
					adapterType.FullName,
					typeof(ModelValidator).FullName
				}), "adapterType");
			}
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x00114080 File Offset: 0x00112280
		private static void ValidateAttributeType(Type attributeType)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!typeof(ValidationAttribute).IsAssignableFrom(attributeType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Common_TypeMustDriveFromType"), new object[]
				{
					attributeType.FullName,
					typeof(ValidationAttribute).FullName
				}), "attributeType");
			}
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x001140F3 File Offset: 0x001122F3
		private static void ValidateAttributeFactory(DataAnnotationsModelValidationFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x00114104 File Offset: 0x00112304
		public static void RegisterValidatableObjectAdapter(Type modelType, Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableModelType(modelType);
			DataAnnotationsModelValidatorProvider.ValidateValidatableAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetValidatableAdapterConstructor(adapterType);
			DataAnnotationsModelValidatorProvider._adaptersLock.EnterWriteLock();
			try
			{
				DataAnnotationsModelValidatorProvider.ValidatableFactories[modelType] = ((ModelMetadata metadata, ModelBindingExecutionContext context) => (ModelValidator)constructor.Invoke(new object[]
				{
					metadata,
					context
				}));
			}
			finally
			{
				DataAnnotationsModelValidatorProvider._adaptersLock.ExitWriteLock();
			}
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x00114170 File Offset: 0x00112370
		public static void RegisterValidatableObjectAdapterFactory(Type modelType, DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableModelType(modelType);
			DataAnnotationsModelValidatorProvider.ValidateValidatableFactory(factory);
			DataAnnotationsModelValidatorProvider._adaptersLock.EnterWriteLock();
			try
			{
				DataAnnotationsModelValidatorProvider.ValidatableFactories[modelType] = factory;
			}
			finally
			{
				DataAnnotationsModelValidatorProvider._adaptersLock.ExitWriteLock();
			}
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x001141BC File Offset: 0x001123BC
		public static void RegisterDefaultValidatableObjectAdapter(Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetValidatableAdapterConstructor(adapterType);
			DataAnnotationsModelValidatorProvider.DefaultValidatableFactory = ((ModelMetadata metadata, ModelBindingExecutionContext context) => (ModelValidator)constructor.Invoke(new object[]
			{
				metadata,
				context
			}));
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x001141F2 File Offset: 0x001123F2
		public static void RegisterDefaultValidatableObjectAdapterFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableFactory(factory);
			DataAnnotationsModelValidatorProvider.DefaultValidatableFactory = factory;
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x00114200 File Offset: 0x00112400
		private static ConstructorInfo GetValidatableAdapterConstructor(Type adapterType)
		{
			ConstructorInfo constructor = adapterType.GetConstructor(new Type[]
			{
				typeof(ModelMetadata),
				typeof(ModelBindingExecutionContext)
			});
			if (constructor == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements"), new object[]
				{
					adapterType.FullName,
					typeof(ModelMetadata).FullName,
					typeof(ModelBindingExecutionContext).FullName
				}), "adapterType");
			}
			return constructor;
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x00114290 File Offset: 0x00112490
		private static void ValidateValidatableAdapterType(Type adapterType)
		{
			if (adapterType == null)
			{
				throw new ArgumentNullException("adapterType");
			}
			if (!typeof(ModelValidator).IsAssignableFrom(adapterType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Common_TypeMustDriveFromType"), new object[]
				{
					adapterType.FullName,
					typeof(ModelValidator).FullName
				}), "adapterType");
			}
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x00114304 File Offset: 0x00112504
		private static void ValidateValidatableModelType(Type modelType)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			if (!typeof(IValidatableObject).IsAssignableFrom(modelType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Common_TypeMustDriveFromType"), new object[]
				{
					modelType.FullName,
					typeof(IValidatableObject).FullName
				}), "modelType");
			}
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x001140F3 File Offset: 0x001122F3
		private static void ValidateValidatableFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
		}

		// Token: 0x04002A74 RID: 10868
		private static bool _addImplicitRequiredAttributeForValueTypes = true;

		// Token: 0x04002A75 RID: 10869
		private static ReaderWriterLockSlim _adaptersLock = new ReaderWriterLockSlim();

		// Token: 0x04002A76 RID: 10870
		internal static DataAnnotationsModelValidationFactory DefaultAttributeFactory = (ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) => new DataAnnotationsModelValidator(metadata, context, attribute);

		// Token: 0x04002A77 RID: 10871
		internal static Dictionary<Type, DataAnnotationsModelValidationFactory> AttributeFactories = new Dictionary<Type, DataAnnotationsModelValidationFactory>
		{
			{
				typeof(RangeAttribute),
				(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) => new RangeAttributeAdapter(metadata, context, (RangeAttribute)attribute)
			},
			{
				typeof(RegularExpressionAttribute),
				(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) => new RegularExpressionAttributeAdapter(metadata, context, (RegularExpressionAttribute)attribute)
			},
			{
				typeof(RequiredAttribute),
				(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) => new RequiredAttributeAdapter(metadata, context, (RequiredAttribute)attribute)
			},
			{
				typeof(StringLengthAttribute),
				(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) => new StringLengthAttributeAdapter(metadata, context, (StringLengthAttribute)attribute)
			},
			{
				typeof(MinLengthAttribute),
				(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) => new MinLengthAttributeAdapter(metadata, context, (MinLengthAttribute)attribute)
			},
			{
				typeof(MaxLengthAttribute),
				(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) => new MaxLengthAttributeAdapter(metadata, context, (MaxLengthAttribute)attribute)
			}
		};

		// Token: 0x04002A78 RID: 10872
		internal static DataAnnotationsValidatableObjectAdapterFactory DefaultValidatableFactory = (ModelMetadata metadata, ModelBindingExecutionContext context) => new ValidatableObjectAdapter(metadata, context);

		// Token: 0x04002A79 RID: 10873
		internal static Dictionary<Type, DataAnnotationsValidatableObjectAdapterFactory> ValidatableFactories = new Dictionary<Type, DataAnnotationsValidatableObjectAdapterFactory>();
	}
}
