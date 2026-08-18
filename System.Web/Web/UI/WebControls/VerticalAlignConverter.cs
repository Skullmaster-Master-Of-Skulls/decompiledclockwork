using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000683 RID: 1667
	internal class VerticalAlignConverter : EnumConverter
	{
		// Token: 0x060051D9 RID: 20953 RVA: 0x0014B0D2 File Offset: 0x0014A0D2
		static VerticalAlignConverter()
		{
			VerticalAlignConverter.stringValues[0] = "NotSet";
			VerticalAlignConverter.stringValues[1] = "Top";
			VerticalAlignConverter.stringValues[2] = "Middle";
			VerticalAlignConverter.stringValues[3] = "Bottom";
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x0014B10F File Offset: 0x0014A10F
		public VerticalAlignConverter() : base(typeof(VerticalAlign))
		{
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x0014B121 File Offset: 0x0014A121
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0014B13C File Offset: 0x0014A13C
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
				string a;
				if ((a = text) != null)
				{
					if (a == "NotSet")
					{
						return VerticalAlign.NotSet;
					}
					if (a == "Top")
					{
						return VerticalAlign.Top;
					}
					if (a == "Middle")
					{
						return VerticalAlign.Middle;
					}
					if (a == "Bottom")
					{
						return VerticalAlign.Bottom;
					}
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x0014B1D1 File Offset: 0x0014A1D1
		public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x0014B1EA File Offset: 0x0014A1EA
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && (int)value <= 3)
			{
				return VerticalAlignConverter.stringValues[(int)value];
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x04002DD1 RID: 11729
		private static string[] stringValues = new string[4];
	}
}
