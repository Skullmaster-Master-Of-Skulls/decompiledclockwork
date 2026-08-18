using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000127 RID: 295
	public class TreeNodeBindingDepthConverter : Int32Converter
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x000434F4 File Offset: 0x000416F4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null && text.Length == 0)
			{
				return -1;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00043523 File Offset: 0x00041723
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is int && (int)value == -1)
			{
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
