using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003F4 RID: 1012
	internal sealed class ErrorTableItemStyle : TableItemStyle, ICustomTypeDescriptor
	{
		// Token: 0x060030CC RID: 12492 RVA: 0x0009EE0C File Offset: 0x0009D00C
		public ErrorTableItemStyle()
		{
			base.ForeColor = Color.Red;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0009ED22 File Offset: 0x0009CF22
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0009ED2B File Offset: 0x0009CF2B
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0009ED34 File Offset: 0x0009CF34
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0009ED3D File Offset: 0x0009CF3D
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0009ED46 File Offset: 0x0009CF46
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0009ED4F File Offset: 0x0009CF4F
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0009ED58 File Offset: 0x0009CF58
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0009ED62 File Offset: 0x0009CF62
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0009ED6B File Offset: 0x0009CF6B
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0009ED75 File Offset: 0x0009CF75
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x0009EE20 File Offset: 0x0009D020
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

		// Token: 0x060030D8 RID: 12504 RVA: 0x00004335 File Offset: 0x00002535
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}
