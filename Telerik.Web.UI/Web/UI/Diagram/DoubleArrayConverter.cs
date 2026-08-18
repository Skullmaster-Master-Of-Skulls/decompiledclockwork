using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000259 RID: 601
	public class DoubleArrayConverter : TypeConverter
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x0004A7D0 File Offset: 0x000489D0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = (string)value;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			string[] array = text.Split(DoubleArrayConverter.splitChars, StringSplitOptions.RemoveEmptyEntries);
			double[] array2 = new double[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				double.TryParse(array[i], out array2[i]);
			}
			return array2;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0004A828 File Offset: 0x00048A28
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			IEnumerable<double> enumerable = (IEnumerable<double>)value;
			if (enumerable == null)
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			IEnumerable<string> source = enumerable.Cast<string>();
			return string.Join(";", source.ToArray<string>());
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0004A862 File Offset: 0x00048A62
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0004A880 File Offset: 0x00048A80
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x040005C3 RID: 1475
		private static readonly char[] splitChars = new char[]
		{
			',',
			';',
			' '
		};
	}
}
