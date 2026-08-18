using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000431 RID: 1073
	internal class HorizontalAlignConverter : EnumConverter
	{
		// Token: 0x06003400 RID: 13312 RVA: 0x000A9870 File Offset: 0x000A7A70
		static HorizontalAlignConverter()
		{
			HorizontalAlignConverter.stringValues[0] = "NotSet";
			HorizontalAlignConverter.stringValues[1] = "Left";
			HorizontalAlignConverter.stringValues[2] = "Center";
			HorizontalAlignConverter.stringValues[3] = "Right";
			HorizontalAlignConverter.stringValues[4] = "Justify";
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000A98C4 File Offset: 0x000A7AC4
		public HorizontalAlignConverter() : base(typeof(HorizontalAlign))
		{
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000A98D6 File Offset: 0x000A7AD6
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000A98F4 File Offset: 0x000A7AF4
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
					return HorizontalAlign.NotSet;
				}
				if (text == "NotSet")
				{
					return HorizontalAlign.NotSet;
				}
				if (text == "Left")
				{
					return HorizontalAlign.Left;
				}
				if (text == "Center")
				{
					return HorizontalAlign.Center;
				}
				if (text == "Right")
				{
					return HorizontalAlign.Right;
				}
				if (text == "Justify")
				{
					return HorizontalAlign.Justify;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000A999B File Offset: 0x000A7B9B
		public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000A99B9 File Offset: 0x000A7BB9
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && (int)value <= 4)
			{
				return HorizontalAlignConverter.stringValues[(int)value];
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0400218F RID: 8591
		private static string[] stringValues = new string[5];
	}
}
