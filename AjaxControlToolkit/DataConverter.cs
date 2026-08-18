using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace AjaxControlToolkit
{
	// Token: 0x0200004E RID: 78
	public class DataConverter<T> : TypeConverter
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000962E File Offset: 0x0000782E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00009640 File Offset: 0x00007840
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (string.IsNullOrEmpty(text))
			{
				return new T[0];
			}
			string[] array = text.Split(new char[]
			{
				','
			});
			List<T> list = new List<T>();
			Type typeFromHandle = typeof(T);
			foreach (string value2 in array)
			{
				T item = (T)((object)Convert.ChangeType(value2, typeFromHandle));
				list.Add(item);
			}
			return list.ToArray();
		}
	}
}
