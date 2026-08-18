using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Net.Configuration
{
	// Token: 0x0200034E RID: 846
	public sealed class WebUtilityElement : ConfigurationElement
	{
		// Token: 0x06001E59 RID: 7769 RVA: 0x0008E0E8 File Offset: 0x0008C2E8
		public WebUtilityElement()
		{
			this.properties.Add(this.unicodeDecodingConformance);
			this.properties.Add(this.unicodeEncodingConformance);
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0008E176 File Offset: 0x0008C376
		// (set) Token: 0x06001E5B RID: 7771 RVA: 0x0008E189 File Offset: 0x0008C389
		[ConfigurationProperty("unicodeDecodingConformance", DefaultValue = UnicodeDecodingConformance.Auto)]
		public UnicodeDecodingConformance UnicodeDecodingConformance
		{
			get
			{
				return (UnicodeDecodingConformance)base[this.unicodeDecodingConformance];
			}
			set
			{
				base[this.unicodeDecodingConformance] = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0008E19D File Offset: 0x0008C39D
		// (set) Token: 0x06001E5D RID: 7773 RVA: 0x0008E1B0 File Offset: 0x0008C3B0
		[ConfigurationProperty("unicodeEncodingConformance", DefaultValue = UnicodeEncodingConformance.Auto)]
		public UnicodeEncodingConformance UnicodeEncodingConformance
		{
			get
			{
				return (UnicodeEncodingConformance)base[this.unicodeEncodingConformance];
			}
			set
			{
				base[this.unicodeEncodingConformance] = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0008E1C4 File Offset: 0x0008C3C4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001CC4 RID: 7364
		private readonly ConfigurationProperty unicodeDecodingConformance = new ConfigurationProperty("unicodeDecodingConformance", typeof(UnicodeDecodingConformance), UnicodeDecodingConformance.Auto, new WebUtilityElement.EnumTypeConverter<UnicodeDecodingConformance>(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001CC5 RID: 7365
		private readonly ConfigurationProperty unicodeEncodingConformance = new ConfigurationProperty("unicodeEncodingConformance", typeof(UnicodeEncodingConformance), UnicodeEncodingConformance.Auto, new WebUtilityElement.EnumTypeConverter<UnicodeEncodingConformance>(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001CC6 RID: 7366
		private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x020007CA RID: 1994
		private class EnumTypeConverter<TEnum> : TypeConverter where TEnum : struct
		{
			// Token: 0x060043A1 RID: 17313 RVA: 0x0011D2E5 File Offset: 0x0011B4E5
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x060043A2 RID: 17314 RVA: 0x0011D304 File Offset: 0x0011B504
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				TEnum tenum;
				if (text != null && Enum.TryParse<TEnum>(text, true, out tenum))
				{
					return tenum;
				}
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
}
