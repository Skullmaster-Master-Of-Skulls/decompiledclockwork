using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003F3 RID: 1011
	internal sealed class ErrorStyle : Style, ICustomTypeDescriptor
	{
		// Token: 0x060030BF RID: 12479 RVA: 0x0009ED0F File Offset: 0x0009CF0F
		public ErrorStyle()
		{
			base.ForeColor = Color.Red;
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x0009ED22 File Offset: 0x0009CF22
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x0009ED2B File Offset: 0x0009CF2B
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x0009ED34 File Offset: 0x0009CF34
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x0009ED3D File Offset: 0x0009CF3D
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x0009ED46 File Offset: 0x0009CF46
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0009ED4F File Offset: 0x0009CF4F
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0009ED58 File Offset: 0x0009CF58
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0009ED62 File Offset: 0x0009CF62
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0009ED6B File Offset: 0x0009CF6B
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0009ED75 File Offset: 0x0009CF75
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0009ED80 File Offset: 0x0009CF80
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

		// Token: 0x060030CB RID: 12491 RVA: 0x00004335 File Offset: 0x00002535
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}
