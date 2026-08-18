using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200051E RID: 1310
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class BooleanConverter : TypeConverter
	{
		// Token: 0x060031C7 RID: 12743 RVA: 0x000E0378 File Offset: 0x000DE578
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000E0398 File Offset: 0x000DE598
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string value2 = ((string)value).Trim();
				try
				{
					return bool.Parse(value2);
				}
				catch (FormatException innerException)
				{
					throw new FormatException(SR.GetString("ConvertInvalidPrimitive", new object[]
					{
						(string)value,
						"Boolean"
					}), innerException);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000E040C File Offset: 0x000DE60C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (BooleanConverter.values == null)
			{
				BooleanConverter.values = new TypeConverter.StandardValuesCollection(new object[]
				{
					true,
					false
				});
			}
			return BooleanConverter.values;
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000E0442 File Offset: 0x000DE642
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000E0445 File Offset: 0x000DE645
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04002940 RID: 10560
		private static volatile TypeConverter.StandardValuesCollection values;
	}
}
