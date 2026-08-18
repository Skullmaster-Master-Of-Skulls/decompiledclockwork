using System;
using System.ComponentModel;
using System.Globalization;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001D7 RID: 471
	internal class TimeSpanOrInfiniteConverter : TimeSpanConverter
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x00044250 File Offset: 0x00042450
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type type)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (!(value is TimeSpan))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID8028", new object[]
				{
					typeof(TimeSpan),
					value.GetType()
				}));
			}
			if ((TimeSpan)value == TimeSpan.MaxValue)
			{
				return "Infinite";
			}
			return base.ConvertTo(context, cultureInfo, value, type);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x000442D1 File Offset: 0x000424D1
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo cultureInfo, object data)
		{
			if (string.Equals((string)data, "infinite", StringComparison.OrdinalIgnoreCase))
			{
				return TimeSpan.MaxValue;
			}
			return base.ConvertFrom(context, cultureInfo, data);
		}
	}
}
