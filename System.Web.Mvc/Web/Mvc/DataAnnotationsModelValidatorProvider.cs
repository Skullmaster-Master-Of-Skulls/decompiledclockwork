using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Mvc.Properties;
using System.Web.Security;

namespace System.Web.Mvc
{
	// Token: 0x02000144 RID: 324
	public class DataAnnotationsModelValidatorProvider : AssociatedValidatorProvider
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x000169B7 File Offset: 0x00014BB7
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x000169BE File Offset: 0x00014BBE
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

		// Token: 0x06000849 RID: 2121 RVA: 0x000169D4 File Offset: 0x00014BD4
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
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

		// Token: 0x0600084A RID: 2122 RVA: 0x00016B40 File Offset: 0x00014D40
		public static void RegisterAdapter(Type attributeType, Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeType(attributeType);
			DataAnnotationsModelValidatorProvider.ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetAttributeAdapterConstructor(attributeType, adapterType);
			DataAnnotationsModelValidatorProvider._adaptersLock.EnterWriteLock();
			try
			{
				DataAnnotationsModelValidatorProvider.AttributeFactories[attributeType] = ((ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => (ModelValidator)constructor.Invoke(new object[]
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

		// Token: 0x0600084B RID: 2123 RVA: 0x00016BB4 File Offset: 0x00014DB4
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

		// Token: 0x0600084C RID: 2124 RVA: 0x00016C3C File Offset: 0x00014E3C
		public static void RegisterDefaultAdapter(Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetAttributeAdapterConstructor(typeof(ValidationAttribute), adapterType);
			DataAnnotationsModelValidatorProvider.DefaultAttributeFactory = ((ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => (ModelValidator)constructor.Invoke(new object[]
			{
				metadata,
				context,
				attribute
			}));
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00016C7C File Offset: 0x00014E7C
		public static void RegisterDefaultAdapterFactory(DataAnnotationsModelValidationFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeFactory(factory);
			DataAnnotationsModelValidatorProvider.DefaultAttributeFactory = factory;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00016C8C File Offset: 0x00014E8C
		private static ConstructorInfo GetAttributeAdapterConstructor(Type attributeType, Type adapterType)
		{
			ConstructorInfo constructor = adapterType.GetConstructor(new Type[]
			{
				typeof(ModelMetadata),
				typeof(ControllerContext),
				attributeType
			});
			if (constructor == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.DataAnnotationsModelValidatorProvider_ConstructorRequirements, new object[]
				{
					adapterType.FullName,
					typeof(ModelMetadata).FullName,
					typeof(ControllerContext).FullName,
					attributeType.FullName
				}), "adapterType");
			}
			return constructor;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00016D28 File Offset: 0x00014F28
		private static void ValidateAttributeAdapterType(Type adapterType)
		{
			if (adapterType == null)
			{
				throw new ArgumentNullException("adapterType");
			}
			if (!typeof(ModelValidator).IsAssignableFrom(adapterType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_TypeMustDriveFromType, new object[]
				{
					adapterType.FullName,
					typeof(ModelValidator).FullName
				}), "adapterType");
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00016D98 File Offset: 0x00014F98
		private static void ValidateAttributeType(Type attributeType)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!typeof(ValidationAttribute).IsAssignableFrom(attributeType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_TypeMustDriveFromType, new object[]
				{
					attributeType.FullName,
					typeof(ValidationAttribute).FullName
				}), "attributeType");
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00016E08 File Offset: 0x00015008
		private static void ValidateAttributeFactory(DataAnnotationsModelValidationFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00016E50 File Offset: 0x00015050
		public static void RegisterValidatableObjectAdapter(Type modelType, Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableModelType(modelType);
			DataAnnotationsModelValidatorProvider.ValidateValidatableAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetValidatableAdapterConstructor(adapterType);
			DataAnnotationsModelValidatorProvider._adaptersLock.EnterWriteLock();
			try
			{
				DataAnnotationsModelValidatorProvider.ValidatableFactories[modelType] = ((ModelMetadata metadata, ControllerContext context) => (ModelValidator)constructor.Invoke(new object[]
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

		// Token: 0x06000853 RID: 2131 RVA: 0x00016EC0 File Offset: 0x000150C0
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

		// Token: 0x06000854 RID: 2132 RVA: 0x00016F44 File Offset: 0x00015144
		public static void RegisterDefaultValidatableObjectAdapter(Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetValidatableAdapterConstructor(adapterType);
			DataAnnotationsModelValidatorProvider.DefaultValidatableFactory = ((ModelMetadata metadata, ControllerContext context) => (ModelValidator)constructor.Invoke(new object[]
			{
				metadata,
				context
			}));
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00016F7A File Offset: 0x0001517A
		public static void RegisterDefaultValidatableObjectAdapterFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableFactory(factory);
			DataAnnotationsModelValidatorProvider.DefaultValidatableFactory = factory;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00016F88 File Offset: 0x00015188
		private static ConstructorInfo GetValidatableAdapterConstructor(Type adapterType)
		{
			ConstructorInfo constructor = adapterType.GetConstructor(new Type[]
			{
				typeof(ModelMetadata),
				typeof(ControllerContext)
			});
			if (constructor == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements, new object[]
				{
					adapterType.FullName,
					typeof(ModelMetadata).FullName,
					typeof(ControllerContext).FullName
				}), "adapterType");
			}
			return constructor;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00017018 File Offset: 0x00015218
		private static void ValidateValidatableAdapterType(Type adapterType)
		{
			if (adapterType == null)
			{
				throw new ArgumentNullException("adapterType");
			}
			if (!typeof(ModelValidator).IsAssignableFrom(adapterType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_TypeMustDriveFromType, new object[]
				{
					adapterType.FullName,
					typeof(ModelValidator).FullName
				}), "adapterType");
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00017088 File Offset: 0x00015288
		private static void ValidateValidatableModelType(Type modelType)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			if (!typeof(IValidatableObject).IsAssignableFrom(modelType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_TypeMustDriveFromType, new object[]
				{
					modelType.FullName,
					typeof(IValidatableObject).FullName
				}), "modelType");
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000170F8 File Offset: 0x000152F8
		private static void ValidateValidatableFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00017190 File Offset: 0x00015390
		private static Dictionary<Type, DataAnnotationsModelValidationFactory> BuildAttributeFactoriesDictionary()
		{
			Dictionary<Type, DataAnnotationsModelValidationFactory> dictionary = new Dictionary<Type, DataAnnotationsModelValidationFactory>();
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, typeof(RangeAttribute), (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new RangeAttributeAdapter(metadata, context, (RangeAttribute)attribute));
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, typeof(RegularExpressionAttribute), (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new RegularExpressionAttributeAdapter(metadata, context, (RegularExpressionAttribute)attribute));
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, typeof(RequiredAttribute), (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new RequiredAttributeAdapter(metadata, context, (RequiredAttribute)attribute));
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, typeof(StringLengthAttribute), (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new StringLengthAttributeAdapter(metadata, context, (StringLengthAttribute)attribute));
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, typeof(MaxLengthAttribute), (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new MaxLengthAttributeAdapter(metadata, context, (MaxLengthAttribute)attribute));
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, typeof(MinLengthAttribute), (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new MinLengthAttributeAdapter(metadata, context, (MinLengthAttribute)attribute));
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, typeof(MembershipPasswordAttribute), (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new MembershipPasswordAttributeAdapter(metadata, context, (MembershipPasswordAttribute)attribute));
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, typeof(CompareAttribute), (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new CompareAttributeAdapter(metadata, context, (CompareAttribute)attribute));
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, typeof(FileExtensionsAttribute), (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new FileExtensionsAttributeAdapter(metadata, context, (FileExtensionsAttribute)attribute));
			DataAnnotationsModelValidatorProvider.AddDataTypeAttributeAdapter(dictionary, typeof(CreditCardAttribute), "creditcard");
			DataAnnotationsModelValidatorProvider.AddDataTypeAttributeAdapter(dictionary, typeof(EmailAddressAttribute), "email");
			DataAnnotationsModelValidatorProvider.AddDataTypeAttributeAdapter(dictionary, typeof(PhoneAttribute), "phone");
			DataAnnotationsModelValidatorProvider.AddDataTypeAttributeAdapter(dictionary, typeof(UrlAttribute), "url");
			return dictionary;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001738D File Offset: 0x0001558D
		private static void AddValidationAttributeAdapter(Dictionary<Type, DataAnnotationsModelValidationFactory> dictionary, Type validataionAttributeType, DataAnnotationsModelValidationFactory factory)
		{
			if (validataionAttributeType != null)
			{
				dictionary.Add(validataionAttributeType, factory);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000173C0 File Offset: 0x000155C0
		private static void AddDataTypeAttributeAdapter(Dictionary<Type, DataAnnotationsModelValidationFactory> dictionary, Type attributeType, string ruleName)
		{
			DataAnnotationsModelValidatorProvider.AddValidationAttributeAdapter(dictionary, attributeType, (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new DataTypeAttributeAdapter(metadata, context, (DataTypeAttribute)attribute, ruleName));
		}

		// Token: 0x0400024A RID: 586
		private static bool _addImplicitRequiredAttributeForValueTypes = true;

		// Token: 0x0400024B RID: 587
		private static ReaderWriterLockSlim _adaptersLock = new ReaderWriterLockSlim();

		// Token: 0x0400024C RID: 588
		internal static DataAnnotationsModelValidationFactory DefaultAttributeFactory = (ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) => new DataAnnotationsModelValidator(metadata, context, attribute);

		// Token: 0x0400024D RID: 589
		internal static Dictionary<Type, DataAnnotationsModelValidationFactory> AttributeFactories = DataAnnotationsModelValidatorProvider.BuildAttributeFactoriesDictionary();

		// Token: 0x0400024E RID: 590
		internal static DataAnnotationsValidatableObjectAdapterFactory DefaultValidatableFactory = (ModelMetadata metadata, ControllerContext context) => new ValidatableObjectAdapter(metadata, context);

		// Token: 0x0400024F RID: 591
		internal static Dictionary<Type, DataAnnotationsValidatableObjectAdapterFactory> ValidatableFactories = new Dictionary<Type, DataAnnotationsValidatableObjectAdapterFactory>();
	}
}
