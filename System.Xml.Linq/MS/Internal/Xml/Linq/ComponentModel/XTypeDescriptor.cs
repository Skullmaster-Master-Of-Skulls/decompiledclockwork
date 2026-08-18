using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x02000034 RID: 52
	internal class XTypeDescriptor<T> : CustomTypeDescriptor
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000B8AB File Offset: 0x00009AAB
		public XTypeDescriptor(ICustomTypeDescriptor parent) : base(parent)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		public override PropertyDescriptorCollection GetProperties()
		{
			return this.GetProperties(null);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			if (attributes == null)
			{
				if (typeof(T) == typeof(XElement))
				{
					propertyDescriptorCollection.Add(new XElementAttributePropertyDescriptor());
					propertyDescriptorCollection.Add(new XElementDescendantsPropertyDescriptor());
					propertyDescriptorCollection.Add(new XElementElementPropertyDescriptor());
					propertyDescriptorCollection.Add(new XElementElementsPropertyDescriptor());
					propertyDescriptorCollection.Add(new XElementValuePropertyDescriptor());
					propertyDescriptorCollection.Add(new XElementXmlPropertyDescriptor());
				}
				else if (typeof(T) == typeof(XAttribute))
				{
					propertyDescriptorCollection.Add(new XAttributeValuePropertyDescriptor());
				}
			}
			foreach (object obj in base.GetProperties(attributes))
			{
				PropertyDescriptor value = (PropertyDescriptor)obj;
				propertyDescriptorCollection.Add(value);
			}
			return propertyDescriptorCollection;
		}
	}
}
