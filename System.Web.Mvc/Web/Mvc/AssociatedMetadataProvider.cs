using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200006C RID: 108
	public abstract class AssociatedMetadataProvider : ModelMetadataProvider
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x00009738 File Offset: 0x00007938
		private static void ApplyMetadataAwareAttributes(IEnumerable<Attribute> attributes, ModelMetadata result)
		{
			foreach (IMetadataAware metadataAware in attributes.OfType<IMetadataAware>())
			{
				metadataAware.OnMetadataCreated(result);
			}
		}

		// Token: 0x060002E6 RID: 742
		protected abstract ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName);

		// Token: 0x060002E7 RID: 743 RVA: 0x00009798 File Offset: 0x00007998
		protected virtual IEnumerable<Attribute> FilterAttributes(Type containerType, PropertyDescriptor propertyDescriptor, IEnumerable<Attribute> attributes)
		{
			if (typeof(ViewPage).IsAssignableFrom(containerType) || typeof(ViewUserControl).IsAssignableFrom(containerType))
			{
				return from a in attributes
				where !(a is ReadOnlyAttribute)
				select a;
			}
			return attributes;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000097F0 File Offset: 0x000079F0
		public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
		{
			if (containerType == null)
			{
				throw new ArgumentNullException("containerType");
			}
			PropertyDescriptorCollection properties = this.GetTypeDescriptor(containerType).GetProperties();
			ModelMetadata[] array = new ModelMetadata[properties.Count];
			for (int i = 0; i < properties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = properties[i];
				Func<object> modelAccessor = (container == null) ? null : AssociatedMetadataProvider.GetPropertyValueAccessor(container, propertyDescriptor);
				ModelMetadata metadataForProperty = this.GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);
				if (metadataForProperty != null)
				{
					metadataForProperty.Container = container;
				}
				array[i] = metadataForProperty;
			}
			return array;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00009870 File Offset: 0x00007A70
		public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
		{
			if (containerType == null)
			{
				throw new ArgumentNullException("containerType");
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "propertyName");
			}
			ICustomTypeDescriptor typeDescriptor = this.GetTypeDescriptor(containerType);
			PropertyDescriptor propertyDescriptor = typeDescriptor.GetProperties().Find(propertyName, true);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_PropertyNotFound, new object[]
				{
					containerType.FullName,
					propertyName
				}));
			}
			return this.GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000098F8 File Offset: 0x00007AF8
		protected virtual ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
		{
			IEnumerable<Attribute> attributes = this.FilterAttributes(containerType, propertyDescriptor, new AttributeList(propertyDescriptor.Attributes));
			ModelMetadata result = this.CreateMetadata(attributes, containerType, modelAccessor, propertyDescriptor.PropertyType, propertyDescriptor.Name);
			AssociatedMetadataProvider.ApplyMetadataAwareAttributes(attributes, result);
			return result;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009938 File Offset: 0x00007B38
		public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			AttributeList attributes = new AttributeList(this.GetTypeDescriptor(modelType).GetAttributes());
			ModelMetadata result = this.CreateMetadata(attributes, null, modelAccessor, modelType, null);
			AssociatedMetadataProvider.ApplyMetadataAwareAttributes(attributes, result);
			return result;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000999C File Offset: 0x00007B9C
		private static Func<object> GetPropertyValueAccessor(object container, PropertyDescriptor property)
		{
			return () => property.GetValue(container);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000099C9 File Offset: 0x00007BC9
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return TypeDescriptorHelper.Get(type);
		}
	}
}
