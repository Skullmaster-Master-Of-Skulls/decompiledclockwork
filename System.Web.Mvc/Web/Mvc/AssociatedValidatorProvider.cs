using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020000E6 RID: 230
	public abstract class AssociatedValidatorProvider : ModelValidatorProvider
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x00010072 File Offset: 0x0000E272
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return TypeDescriptorHelper.Get(type);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001007C File Offset: 0x0000E27C
		public sealed override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
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

		// Token: 0x060005E7 RID: 1511
		protected abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes);

		// Token: 0x060005E8 RID: 1512 RVA: 0x000100D4 File Offset: 0x0000E2D4
		private IEnumerable<ModelValidator> GetValidatorsForProperty(ModelMetadata metadata, ControllerContext context)
		{
			ICustomTypeDescriptor typeDescriptor = this.GetTypeDescriptor(metadata.ContainerType);
			PropertyDescriptor propertyDescriptor = typeDescriptor.GetProperties().Find(metadata.PropertyName, true);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_PropertyNotFound, new object[]
				{
					metadata.ContainerType.FullName,
					metadata.PropertyName
				}), "metadata");
			}
			return this.GetValidators(metadata, context, new AttributeList(propertyDescriptor.Attributes));
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00010150 File Offset: 0x0000E350
		private IEnumerable<ModelValidator> GetValidatorsForType(ModelMetadata metadata, ControllerContext context)
		{
			return this.GetValidators(metadata, context, new AttributeList(this.GetTypeDescriptor(metadata.ModelType).GetAttributes()));
		}
	}
}
