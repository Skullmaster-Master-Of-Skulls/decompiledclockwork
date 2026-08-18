using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Telerik.Web
{
	// Token: 0x02000AF8 RID: 2808
	internal class IntegerArrayConverter : TypeConverter
	{
		// Token: 0x06006980 RID: 27008 RVA: 0x0018CF38 File Offset: 0x0018B138
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			int[] array = value as int[];
			if (array == null || array.Length == 0)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(array[0]);
			for (int i = 1; i < array.Length; i++)
			{
				stringBuilder.Append(";");
				stringBuilder.Append(array[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006981 RID: 27009 RVA: 0x0018CFB3 File Offset: 0x0018B1B3
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x0018CFB6 File Offset: 0x0018B1B6
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return null;
		}

		// Token: 0x06006983 RID: 27011 RVA: 0x0018CFB9 File Offset: 0x0018B1B9
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06006984 RID: 27012 RVA: 0x0018CFE0 File Offset: 0x0018B1E0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value.GetType() == typeof(string))
			{
				string[] array = value.ToString().Split(new char[]
				{
					',',
					';',
					' '
				}, StringSplitOptions.RemoveEmptyEntries);
				List<int> list = new List<int>();
				for (int i = 0; i < array.Length; i++)
				{
					int item;
					if (int.TryParse(array[i], out item))
					{
						list.Add(item);
					}
				}
				return list.ToArray();
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
