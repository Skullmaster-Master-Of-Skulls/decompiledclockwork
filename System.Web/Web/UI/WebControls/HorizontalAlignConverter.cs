using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005B5 RID: 1461
	internal class HorizontalAlignConverter : EnumConverter
	{
		// Token: 0x0600477F RID: 18303 RVA: 0x00124538 File Offset: 0x00123538
		static HorizontalAlignConverter()
		{
			HorizontalAlignConverter.stringValues[0] = "NotSet";
			HorizontalAlignConverter.stringValues[1] = "Left";
			HorizontalAlignConverter.stringValues[2] = "Center";
			HorizontalAlignConverter.stringValues[3] = "Right";
			HorizontalAlignConverter.stringValues[4] = "Justify";
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x0012458C File Offset: 0x0012358C
		public HorizontalAlignConverter() : base(typeof(HorizontalAlign))
		{
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x0012459E File Offset: 0x0012359E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x001245B8 File Offset: 0x001235B8
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
				string a;
				if ((a = text) != null)
				{
					if (a == "NotSet")
					{
						return HorizontalAlign.NotSet;
					}
					if (a == "Left")
					{
						return HorizontalAlign.Left;
					}
					if (a == "Center")
					{
						return HorizontalAlign.Center;
					}
					if (a == "Right")
					{
						return HorizontalAlign.Right;
					}
					if (a == "Justify")
					{
						return HorizontalAlign.Justify;
					}
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x00124664 File Offset: 0x00123664
		public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x0012467D File Offset: 0x0012367D
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && (int)value <= 4)
			{
				return HorizontalAlignConverter.stringValues[(int)value];
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x04002AA2 RID: 10914
		private static string[] stringValues = new string[5];
	}
}
