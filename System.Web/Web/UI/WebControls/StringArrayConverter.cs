using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000651 RID: 1617
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class StringArrayConverter : TypeConverter
	{
		// Token: 0x06004F4E RID: 20302 RVA: 0x0013F551 File Offset: 0x0013E551
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x0013F564 File Offset: 0x0013E564
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				throw base.GetConvertFromException(value);
			}
			if (((string)value).Length == 0)
			{
				return new string[0];
			}
			string[] array = ((string)value).Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
			}
			return array;
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x0013F5C8 File Offset: 0x0013E5C8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string))
			{
				throw base.GetConvertToException(value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			return string.Join(",", (string[])value);
		}
	}
}
