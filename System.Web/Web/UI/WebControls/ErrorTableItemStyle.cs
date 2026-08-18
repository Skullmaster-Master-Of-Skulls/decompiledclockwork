using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000577 RID: 1399
	internal sealed class ErrorTableItemStyle : TableItemStyle, ICustomTypeDescriptor
	{
		// Token: 0x060044A7 RID: 17575 RVA: 0x0011A2E4 File Offset: 0x001192E4
		public ErrorTableItemStyle()
		{
			base.ForeColor = Color.Red;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x0011A2F7 File Offset: 0x001192F7
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x0011A300 File Offset: 0x00119300
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x0011A309 File Offset: 0x00119309
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x0011A312 File Offset: 0x00119312
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x0011A31B File Offset: 0x0011931B
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x0011A324 File Offset: 0x00119324
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x0011A32D File Offset: 0x0011932D
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x0011A337 File Offset: 0x00119337
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x0011A340 File Offset: 0x00119340
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x0011A34A File Offset: 0x0011934A
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x0011A354 File Offset: 0x00119354
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.GetType(), attributes);
			PropertyDescriptor[] array = new PropertyDescriptor[properties.Count];
			PropertyDescriptor propertyDescriptor = properties["ForeColor"];
			for (int i = 0; i < properties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor2 = properties[i];
				if (propertyDescriptor2 == propertyDescriptor)
				{
					array[i] = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor2, new Attribute[]
					{
						new DefaultValueAttribute(typeof(Color), "Red")
					});
				}
				else
				{
					array[i] = propertyDescriptor2;
				}
			}
			return new PropertyDescriptorCollection(array, true);
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x0011A3E5 File Offset: 0x001193E5
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}
