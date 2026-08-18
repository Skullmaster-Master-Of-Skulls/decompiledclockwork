using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x0200007A RID: 122
	public class DropListStyleEnumConverter : TypeConverter
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00048344 File Offset: 0x00047344
		public DropListStyleEnumConverter()
		{
			this.values = new ArrayList();
			Type typeFromHandle = typeof(DropListBehaviour);
			FieldInfo[] fields = typeFromHandle.GetFields();
			foreach (FieldInfo fieldInfo in fields)
			{
				DescriptionAttribute[] array2 = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (array2.Length > 0)
				{
					this.values.Add(fieldInfo.GetValue(fieldInfo.Name));
				}
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000483DC File Offset: 0x000473DC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000483FC File Offset: 0x000473FC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			object result;
			if (value is string)
			{
				string text = (string)value;
				text = text.Replace(' ', '_');
				result = Enum.Parse(typeof(DropListBehaviour), text);
			}
			else
			{
				result = base.ConvertFrom(value);
			}
			return result;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0004844C File Offset: 0x0004744C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0004846C File Offset: 0x0004746C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			object result;
			if (destinationType == typeof(string))
			{
				if (value is DropListBehaviour)
				{
					DropListBehaviour dropListBehaviour = (DropListBehaviour)value;
					result = Enum.GetName(typeof(DropListBehaviour), dropListBehaviour).Replace('_', ' ');
				}
				else
				{
					result = "?";
				}
			}
			else
			{
				result = base.ConvertTo(value, destinationType);
			}
			return result;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000484E0 File Offset: 0x000474E0
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000484F4 File Offset: 0x000474F4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.values);
		}

		// Token: 0x04000399 RID: 921
		private ArrayList values;
	}
}
