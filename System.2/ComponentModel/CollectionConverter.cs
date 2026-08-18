using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000528 RID: 1320
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CollectionConverter : TypeConverter
	{
		// Token: 0x06003201 RID: 12801 RVA: 0x000E090C File Offset: 0x000DEB0C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is ICollection)
			{
				return SR.GetString("CollectionConverterText");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000E095F File Offset: 0x000DEB5F
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x000E0962 File Offset: 0x000DEB62
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
