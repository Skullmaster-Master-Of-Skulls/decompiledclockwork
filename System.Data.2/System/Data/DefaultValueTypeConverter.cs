using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000E1 RID: 225
	internal sealed class DefaultValueTypeConverter : StringConverter
	{
		// Token: 0x06000F15 RID: 3861 RVA: 0x00078F40 File Offset: 0x00078340
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

		// Token: 0x06000F16 RID: 3862 RVA: 0x00078F98 File Offset: 0x00078398
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

		// Token: 0x04000472 RID: 1138
		private static string nullString = "<null>";

		// Token: 0x04000473 RID: 1139
		private static string dbNullString = "<DBNull>";
	}
}
