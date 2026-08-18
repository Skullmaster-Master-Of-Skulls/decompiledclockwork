using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000511 RID: 1297
	internal class VerticalAlignConverter : EnumConverter
	{
		// Token: 0x06004120 RID: 16672 RVA: 0x000D5274 File Offset: 0x000D3474
		static VerticalAlignConverter()
		{
			VerticalAlignConverter.stringValues[0] = "NotSet";
			VerticalAlignConverter.stringValues[1] = "Top";
			VerticalAlignConverter.stringValues[2] = "Middle";
			VerticalAlignConverter.stringValues[3] = "Bottom";
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x000D52B1 File Offset: 0x000D34B1
		public VerticalAlignConverter() : base(typeof(VerticalAlign))
		{
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x000A98D6 File Offset: 0x000A7AD6
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x000D52C4 File Offset: 0x000D34C4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return VerticalAlign.NotSet;
				}
				if (text == "NotSet")
				{
					return VerticalAlign.NotSet;
				}
				if (text == "Top")
				{
					return VerticalAlign.Top;
				}
				if (text == "Middle")
				{
					return VerticalAlign.Middle;
				}
				if (text == "Bottom")
				{
					return VerticalAlign.Bottom;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x000A999B File Offset: 0x000A7B9B
		public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x000D5354 File Offset: 0x000D3554
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && (int)value <= 3)
			{
				return VerticalAlignConverter.stringValues[(int)value];
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x04002505 RID: 9477
		private static string[] stringValues = new string[4];
	}
}
