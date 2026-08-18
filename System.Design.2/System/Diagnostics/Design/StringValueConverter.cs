using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Diagnostics.Design
{
	// Token: 0x0200020E RID: 526
	internal class StringValueConverter : TypeConverter
	{
		// Token: 0x06001380 RID: 4992 RVA: 0x00010631 File Offset: 0x0000E831
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0006FA78 File Offset: 0x0006DC78
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text == string.Empty)
				{
					text = null;
				}
				return text;
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
