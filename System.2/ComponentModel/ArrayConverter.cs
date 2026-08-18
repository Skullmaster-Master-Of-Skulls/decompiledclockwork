using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200050F RID: 1295
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ArrayConverter : CollectionConverter
	{
		// Token: 0x06003122 RID: 12578 RVA: 0x000DEE18 File Offset: 0x000DD018
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Array)
			{
				return SR.GetString("ArrayConverterText", new object[]
				{
					value.GetType().Name
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000DEE80 File Offset: 0x000DD080
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptor[] array = null;
			if (value.GetType().IsArray)
			{
				Array array2 = (Array)value;
				int length = array2.GetLength(0);
				array = new PropertyDescriptor[length];
				Type type = value.GetType();
				Type elementType = type.GetElementType();
				for (int i = 0; i < length; i++)
				{
					array[i] = new ArrayConverter.ArrayPropertyDescriptor(type, elementType, i);
				}
			}
			return new PropertyDescriptorCollection(array);
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000DEEE5 File Offset: 0x000DD0E5
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0200088E RID: 2190
		private class ArrayPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
		{
			// Token: 0x0600458C RID: 17804 RVA: 0x001220EC File Offset: 0x001202EC
			public ArrayPropertyDescriptor(Type arrayType, Type elementType, int index) : base(arrayType, "[" + index.ToString() + "]", elementType, null)
			{
				this.index = index;
			}

			// Token: 0x0600458D RID: 17805 RVA: 0x00122114 File Offset: 0x00120314
			public override object GetValue(object instance)
			{
				if (instance is Array)
				{
					Array array = (Array)instance;
					if (array.GetLength(0) > this.index)
					{
						return array.GetValue(this.index);
					}
				}
				return null;
			}

			// Token: 0x0600458E RID: 17806 RVA: 0x00122150 File Offset: 0x00120350
			public override void SetValue(object instance, object value)
			{
				if (instance is Array)
				{
					Array array = (Array)instance;
					if (array.GetLength(0) > this.index)
					{
						array.SetValue(value, this.index);
					}
					this.OnValueChanged(instance, EventArgs.Empty);
				}
			}

			// Token: 0x040037C5 RID: 14277
			private int index;
		}
	}
}
