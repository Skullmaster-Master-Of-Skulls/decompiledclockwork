using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000643 RID: 1603
	public abstract class AssociatedValidatorProvider : ModelValidatorProvider
	{
		// Token: 0x06004F59 RID: 20313 RVA: 0x001136ED File Offset: 0x001118ED
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return TypeDescriptorHelper.Get(type);
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x00113700 File Offset: 0x00111900
		public sealed override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (metadata.ContainerType != null && !string.IsNullOrEmpty(metadata.PropertyName))
			{
				return this.GetValidatorsForProperty(metadata, context);
			}
			return this.GetValidatorsForType(metadata, context);
		}

		// Token: 0x06004F5B RID: 20315
		protected abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context, IEnumerable<Attribute> attributes);

		// Token: 0x06004F5C RID: 20316 RVA: 0x00113758 File Offset: 0x00111958
		private IEnumerable<ModelValidator> GetValidatorsForProperty(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			ICustomTypeDescriptor typeDescriptor = this.GetTypeDescriptor(metadata.ContainerType);
			PropertyDescriptor propertyDescriptor = typeDescriptor.GetProperties().Find(metadata.PropertyName, true);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Common_PropertyNotFound"), new object[]
				{
					metadata.ContainerType.FullName,
					metadata.PropertyName
				}), "metadata");
			}
			return this.GetValidators(metadata, context, propertyDescriptor.Attributes.OfType<Attribute>());
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x001137D7 File Offset: 0x001119D7
		private IEnumerable<ModelValidator> GetValidatorsForType(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			return this.GetValidators(metadata, context, this.GetTypeDescriptor(metadata.ModelType).GetAttributes().Cast<Attribute>());
		}
	}
}
