using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI
{
	// Token: 0x02000277 RID: 631
	public class DataSourceCacheDurationConverter : Int32Converter
	{
		// Token: 0x06001DF9 RID: 7673 RVA: 0x00060FD1 File Offset: 0x0005F1D1
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00060FF0 File Offset: 0x0005F1F0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return null;
			}
			string text = value as string;
			if (text != null)
			{
				string text2 = text.Trim();
				if (text2.Length == 0)
				{
					return 0;
				}
				if (string.Equals(text2, "infinite", StringComparison.OrdinalIgnoreCase))
				{
					return 0;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00061040 File Offset: 0x0005F240
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0006105E File Offset: 0x0005F25E
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value != null && destinationType == typeof(string) && (int)value == 0)
			{
				return "Infinite";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00061090 File Offset: 0x0005F290
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this._values == null)
			{
				object[] values = new object[]
				{
					0
				};
				this._values = new TypeConverter.StandardValuesCollection(values);
			}
			return this._values;
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x00007722 File Offset: 0x00005922
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04001973 RID: 6515
		private TypeConverter.StandardValuesCollection _values;
	}
}
