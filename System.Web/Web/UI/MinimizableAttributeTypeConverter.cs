using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI
{
	// Token: 0x0200042E RID: 1070
	internal class MinimizableAttributeTypeConverter : BooleanConverter
	{
		// Token: 0x0600335A RID: 13146 RVA: 0x000DEF06 File Offset: 0x000DDF06
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000DEF20 File Offset: 0x000DDF20
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
