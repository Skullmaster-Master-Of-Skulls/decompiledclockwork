using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000097 RID: 151
	public class TimeSpanSecondsConverter : ConfigurationConverterBase
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x0001CD20 File Offset: 0x0001AF20
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			base.ValidateType(value, typeof(TimeSpan));
			return ((long)((TimeSpan)value).TotalSeconds).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001CD5C File Offset: 0x0001AF5C
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			long num = 0L;
			try
			{
				num = long.Parse((string)data, CultureInfo.InvariantCulture);
			}
			catch
			{
				throw new ArgumentException(SR.GetString("Converter_timespan_not_in_second"));
			}
			return TimeSpan.FromSeconds((double)num);
		}
	}
}
