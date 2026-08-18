using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000592 RID: 1426
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class MultilineStringConverter : TypeConverter
	{
		// Token: 0x06003505 RID: 13573 RVA: 0x000E77A0 File Offset: 0x000E59A0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is string)
			{
				return SR.GetString("MultilineStringConverterText");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000E77F3 File Offset: 0x000E59F3
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000E77F6 File Offset: 0x000E59F6
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
