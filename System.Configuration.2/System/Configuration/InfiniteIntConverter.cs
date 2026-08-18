using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000063 RID: 99
	public sealed class InfiniteIntConverter : ConfigurationConverterBase
	{
		// Token: 0x060003DA RID: 986 RVA: 0x00013F84 File Offset: 0x00012184
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			base.ValidateType(value, typeof(int));
			if ((int)value == 2147483647)
			{
				return "Infinite";
			}
			return ((int)value).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00013FC8 File Offset: 0x000121C8
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if ((string)data == "Infinite")
			{
				return int.MaxValue;
			}
			return Convert.ToInt32((string)data, 10);
		}
	}
}
