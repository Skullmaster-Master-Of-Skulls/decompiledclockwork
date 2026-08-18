using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C9 RID: 1737
	internal class SessionIdTypeConvertor : Int32Converter
	{
		// Token: 0x06004336 RID: 17206 RVA: 0x000FDF84 File Offset: 0x000FC184
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type type)
		{
			if (value == null)
			{
				throw FxTrace.Exception.ArgumentNull("value");
			}
			if (!(value is int))
			{
				throw FxTrace.Exception.Argument("value", InternalSR.IncompatibleArgumentType(typeof(int), value.GetType()));
			}
			if ((int)value == 0)
			{
				return "ServiceSession";
			}
			if ((int)value == -1)
			{
				return "CurrentSession";
			}
			return base.ConvertTo(context, cultureInfo, value, type);
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x000FDFF9 File Offset: 0x000FC1F9
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo cultureInfo, object data)
		{
			if (string.Equals((string)data, "CurrentSession", StringComparison.OrdinalIgnoreCase))
			{
				return -1;
			}
			if (string.Equals((string)data, "ServiceSession", StringComparison.OrdinalIgnoreCase))
			{
				return 0;
			}
			return base.ConvertFrom(context, cultureInfo, data);
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x000FE038 File Offset: 0x000FC238
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}
	}
}
