using System;
using System.ComponentModel;
using System.Design;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C5 RID: 709
	internal class DataMemberFieldConverter : TypeConverter
	{
		// Token: 0x06001C2C RID: 7212 RVA: 0x00010631 File Offset: 0x0000E831
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000AA057 File Offset: 0x000A8257
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value.Equals(SR.GetString("None")))
			{
				return string.Empty;
			}
			return value;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000AA075 File Offset: 0x000A8275
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && (value == null || value.Equals(string.Empty)))
			{
				return SR.GetString("None_lc");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
