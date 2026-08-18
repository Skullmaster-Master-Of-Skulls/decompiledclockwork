using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000B3 RID: 179
	internal sealed class DefaultValueTypeConverter : StringConverter
	{
		// Token: 0x06000C12 RID: 3090 RVA: 0x0020F858 File Offset: 0x0020EC58
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return DefaultValueTypeConverter.nullString;
				}
				if (value == DBNull.Value)
				{
					return DefaultValueTypeConverter.dbNullString;
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0020F8A8 File Offset: 0x0020ECA8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value.GetType() == typeof(string))
			{
				string strA = (string)value;
				if (string.Compare(strA, DefaultValueTypeConverter.nullString, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return null;
				}
				if (string.Compare(strA, DefaultValueTypeConverter.dbNullString, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return DBNull.Value;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x04000898 RID: 2200
		private static string nullString = "<null>";

		// Token: 0x04000899 RID: 2201
		private static string dbNullString = "<DBNull>";
	}
}
