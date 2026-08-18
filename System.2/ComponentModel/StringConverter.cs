using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005AD RID: 1453
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class StringConverter : TypeConverter
	{
		// Token: 0x0600362E RID: 13870 RVA: 0x000EC861 File Offset: 0x000EAA61
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000EC87F File Offset: 0x000EAA7F
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return (string)value;
			}
			if (value == null)
			{
				return "";
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
