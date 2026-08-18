using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000524 RID: 1316
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CharConverter : TypeConverter
	{
		// Token: 0x060031F6 RID: 12790 RVA: 0x000E0808 File Offset: 0x000DEA08
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000E0826 File Offset: 0x000DEA26
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is char && (char)value == '\0')
			{
				return "";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000E085C File Offset: 0x000DEA5C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			if (text.Length > 1)
			{
				text = text.Trim();
			}
			if (text == null || text.Length <= 0)
			{
				return '\0';
			}
			if (text.Length != 1)
			{
				throw new FormatException(SR.GetString("ConvertInvalidPrimitive", new object[]
				{
					text,
					"Char"
				}));
			}
			return text[0];
		}
	}
}
