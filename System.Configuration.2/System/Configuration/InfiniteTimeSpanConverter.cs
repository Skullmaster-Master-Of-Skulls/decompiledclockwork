using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000064 RID: 100
	public sealed class InfiniteTimeSpanConverter : ConfigurationConverterBase
	{
		// Token: 0x060003DD RID: 989 RVA: 0x00013FF9 File Offset: 0x000121F9
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			base.ValidateType(value, typeof(TimeSpan));
			if ((TimeSpan)value == TimeSpan.MaxValue)
			{
				return "Infinite";
			}
			return InfiniteTimeSpanConverter.s_TimeSpanConverter.ConvertToInvariantString(value);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001402F File Offset: 0x0001222F
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if ((string)data == "Infinite")
			{
				return TimeSpan.MaxValue;
			}
			return InfiniteTimeSpanConverter.s_TimeSpanConverter.ConvertFromInvariantString((string)data);
		}

		// Token: 0x04000285 RID: 645
		private static readonly TypeConverter s_TimeSpanConverter = TypeDescriptor.GetConverter(typeof(TimeSpan));
	}
}
