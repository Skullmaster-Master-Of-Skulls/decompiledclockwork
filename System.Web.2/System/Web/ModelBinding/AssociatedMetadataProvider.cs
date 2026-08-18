using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000642 RID: 1602
	public abstract class AssociatedMetadataProvider : ModelMetadataProvider
	{
		// Token: 0x06004F4E RID: 20302 RVA: 0x00113518 File Offset: 0x00111718
		private static void ApplyMetadataAwareAttributes(IEnumerable<Attribute> attributes, ModelMetadata result)
		{
			foreach (IMetadataAware metadataAware in attributes.OfType<IMetadataAware>())
			{
				metadataAware.OnMetadataCreated(result);
			}
		}

		// Token: 0x06004F4F RID: 20303
		protected abstract ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName);

		// Token: 0x06004F50 RID: 20304 RVA: 0x00113568 File Offset: 0x00111768
		protected virtual IEnumerable<Attribute> FilterAttributes(Type containerType, PropertyDescriptor propertyDescriptor, IEnumerable<Attribute> attributes)
		{
			return attributes;
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x0011356B File Offset: 0x0011176B
		public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
		{
			if (containerType == null)
			{
				throw new ArgumentNullException("containerType");
			}
			return this.GetMetadataForPropertiesImpl(container, containerType);
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x00113589 File Offset: 0x00111789
		private IEnumerable<ModelMetadata> GetMetadataForPropertiesImpl(object container, Type containerType)
		{
			foreach (object obj in this.GetTypeDescriptor(containerType).GetProperties())
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				Func<object> modelAccessor = (container == null) ? null : AssociatedMetadataProvider.GetPropertyValueAccessor(container, propertyDescriptor);
				yield return this.GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x001135A8 File Offset: 0x001117A8
		public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
		{
			if (containerType == null)
			{
				throw new ArgumentNullException("containerType");
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentException(SR.GetString("Common_NullOrEmpty"), "propertyName");
			}
			ICustomTypeDescriptor typeDescriptor = this.GetTypeDescriptor(containerType);
			PropertyDescriptor propertyDescriptor = typeDescriptor.GetProperties().Find(propertyName, true);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Common_PropertyNotFound"), new object[]
				{
					containerType.FullName,
					propertyName
				}));
			}
			return this.GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x00113638 File Offset: 0x00111838
		protected virtual ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
		{
			IEnumerable<Attribute> attributes = this.FilterAttributes(containerType, propertyDescriptor, propertyDescriptor.Attributes.Cast<Attribute>());
			ModelMetadata result = this.CreateMetadata(attributes, containerType, modelAccessor, propertyDescriptor.PropertyType, propertyDescriptor.Name);
			AssociatedMetadataProvider.ApplyMetadataAwareAttributes(attributes, result);
			return result;
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x00113678 File Offset: 0x00111878
		public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			IEnumerable<Attribute> attributes = this.GetTypeDescriptor(modelType).GetAttributes().Cast<Attribute>();
			ModelMetadata result = this.CreateMetadata(attributes, null, modelAccessor, modelType, null);
			AssociatedMetadataProvider.ApplyMetadataAwareAttributes(attributes, result);
			return result;
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x001136C0 File Offset: 0x001118C0
		private static Func<object> GetPropertyValueAccessor(object container, PropertyDescriptor property)
		{
			return () => property.GetValue(container);
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x001136ED File Offset: 0x001118ED
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return TypeDescriptorHelper.Get(type);
		}
	}
}
