using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02001097 RID: 4247
	internal class CharTypeConverter : TypeConverter
	{
		// Token: 0x0600ACB1 RID: 44209 RVA: 0x00251F07 File Offset: 0x00250107
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600ACB2 RID: 44210 RVA: 0x00251F28 File Offset: 0x00250128
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				return ToolsFileLoader.ParseSymbol(text, ' ');
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
