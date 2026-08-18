using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006DC RID: 1756
	internal class TimeSpanOrInfiniteConverter : TimeSpanConverter
	{
		// Token: 0x060043DF RID: 17375 RVA: 0x001005E0 File Offset: 0x000FE7E0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type type)
		{
			if (value == null)
			{
				throw FxTrace.Exception.ArgumentNull("value");
			}
			if (!(value is TimeSpan))
			{
				throw FxTrace.Exception.Argument("value", InternalSR.IncompatibleArgumentType(typeof(TimeSpan), value.GetType()));
			}
			if ((TimeSpan)value == TimeSpan.MaxValue)
			{
				return "Infinite";
			}
			return base.ConvertTo(context, cultureInfo, value, type);
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x00100650 File Offset: 0x000FE850
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
