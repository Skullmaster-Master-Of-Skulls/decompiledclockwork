using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003FC RID: 1020
	public class FontUnitConverter : TypeConverter
	{
		// Token: 0x06003128 RID: 12584 RVA: 0x000A017D File Offset: 0x0009E37D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x000A019C File Offset: 0x0009E39C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return null;
			}
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text2 = text.Trim();
			if (text2.Length == 0)
			{
				return FontUnit.Empty;
			}
			return FontUnit.Parse(text2, culture);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x000A01E8 File Offset: 0x0009E3E8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000A0218 File Offset: 0x0009E418
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				if (value == null || ((FontUnit)value).Type == FontSize.NotSet)
				{
					return string.Empty;
				}
				return ((FontUnit)value).ToString(culture);
			}
			else
			{
				if (!(destinationType == typeof(InstanceDescriptor)) || value == null)
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				FontUnit fontUnit = (FontUnit)value;
				MemberInfo memberInfo = null;
				object[] arguments = null;
				if (fontUnit.IsEmpty)
				{
					memberInfo = typeof(FontUnit).GetField("Empty");
				}
				else if (fontUnit.Type != FontSize.AsUnit)
				{
					string text = null;
					switch (fontUnit.Type)
					{
					case FontSize.Smaller:
						text = "Smaller";
						break;
					case FontSize.Larger:
						text = "Larger";
						break;
					case FontSize.XXSmall:
						text = "XXSmall";
						break;
					case FontSize.XSmall:
						text = "XSmall";
						break;
					case FontSize.Small:
						text = "Small";
						break;
					case FontSize.Medium:
						text = "Medium";
						break;
					case FontSize.Large:
						text = "Large";
						break;
					case FontSize.XLarge:
						text = "XLarge";
						break;
					case FontSize.XXLarge:
						text = "XXLarge";
						break;
					}
					if (text != null)
					{
						memberInfo = typeof(FontUnit).GetField(text);
					}
				}
				else
				{
					memberInfo = typeof(FontUnit).GetConstructor(new Type[]
					{
						typeof(Unit)
					});
					arguments = new object[]
					{
						fontUnit.Unit
					};
				}
				if (memberInfo != null)
				{
					return new InstanceDescriptor(memberInfo, arguments);
				}
				return null;
			}
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000A03B4 File Offset: 0x0009E5B4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				object[] array = new object[]
				{
					FontUnit.Smaller,
					FontUnit.Larger,
					FontUnit.XXSmall,
					FontUnit.XSmall,
					FontUnit.Small,
					FontUnit.Medium,
					FontUnit.Large,
					FontUnit.XLarge,
					FontUnit.XXLarge
				};
				this.values = new TypeConverter.StandardValuesCollection(array);
			}
			return this.values;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x00007722 File Offset: 0x00005922
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040020BC RID: 8380
		private TypeConverter.StandardValuesCollection values;
	}
}
