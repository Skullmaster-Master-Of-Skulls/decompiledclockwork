using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI
{
	// Token: 0x020002C7 RID: 711
	internal class MinimizableAttributeTypeConverter : BooleanConverter
	{
		// Token: 0x06002011 RID: 8209 RVA: 0x0006604B File Offset: 0x0006424B
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0006606C File Offset: 0x0006426C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (text.Length > 0 && !string.Equals(text, "false", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			return false;
		}
	}
}
