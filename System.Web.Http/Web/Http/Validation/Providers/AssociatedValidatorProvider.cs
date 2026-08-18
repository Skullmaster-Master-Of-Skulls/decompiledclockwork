using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x0200018B RID: 395
	public abstract class AssociatedValidatorProvider : ModelValidatorProvider
	{
		// Token: 0x06000A2C RID: 2604 RVA: 0x00021DA7 File Offset: 0x0001FFA7
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return TypeDescriptorHelper.Get(type);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00021DB0 File Offset: 0x0001FFB0
		public sealed override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			if (metadata == null)
			{
				throw Error.ArgumentNull("metadata");
			}
			if (validatorProviders == null)
			{
				throw Error.ArgumentNull("validatorProviders");
			}
			if (metadata.ContainerType != null && !string.IsNullOrEmpty(metadata.PropertyName))
			{
				return this.GetValidatorsForProperty(metadata, validatorProviders);
			}
			return this.GetValidatorsForType(metadata, validatorProviders);
		}

		// Token: 0x06000A2E RID: 2606
		protected abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, IEnumerable<Attribute> attributes);

		// Token: 0x06000A2F RID: 2607 RVA: 0x00021E08 File Offset: 0x00020008
		private IEnumerable<ModelValidator> GetValidatorsForProperty(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			ICustomTypeDescriptor typeDescriptor = this.GetTypeDescriptor(metadata.ContainerType);
			PropertyDescriptor propertyDescriptor = typeDescriptor.GetProperties().Find(metadata.PropertyName, true);
			if (propertyDescriptor == null)
			{
				throw Error.Argument("metadata", SRResources.Common_PropertyNotFound, new object[]
				{
					metadata.ContainerType,
					metadata.PropertyName
				});
			}
			return this.GetValidators(metadata, validatorProviders, propertyDescriptor.Attributes.OfType<Attribute>());
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00021E75 File Offset: 0x00020075
		private IEnumerable<ModelValidator> GetValidatorsForType(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			return this.GetValidators(metadata, validatorProviders, this.GetTypeDescriptor(metadata.ModelType).GetAttributes().Cast<Attribute>());
		}
	}
}
