using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Net.Mail;

namespace System.Net.Configuration
{
	// Token: 0x02000342 RID: 834
	public sealed class SmtpSection : ConfigurationSection
	{
		// Token: 0x06001DF8 RID: 7672 RVA: 0x0008D21C File Offset: 0x0008B41C
		public SmtpSection()
		{
			this.properties.Add(this.deliveryMethod);
			this.properties.Add(this.deliveryFormat);
			this.properties.Add(this.from);
			this.properties.Add(this.network);
			this.properties.Add(this.specifiedPickupDirectory);
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0008D331 File Offset: 0x0008B531
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x0008D344 File Offset: 0x0008B544
		[ConfigurationProperty("deliveryMethod", DefaultValue = SmtpDeliveryMethod.Network)]
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return (SmtpDeliveryMethod)base[this.deliveryMethod];
			}
			set
			{
				base[this.deliveryMethod] = value;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0008D358 File Offset: 0x0008B558
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x0008D36B File Offset: 0x0008B56B
		[ConfigurationProperty("deliveryFormat", DefaultValue = SmtpDeliveryFormat.SevenBit)]
		public SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return (SmtpDeliveryFormat)base[this.deliveryFormat];
			}
			set
			{
				base[this.deliveryFormat] = value;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x0008D37F File Offset: 0x0008B57F
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x0008D392 File Offset: 0x0008B592
		[ConfigurationProperty("from")]
		public string From
		{
			get
			{
				return (string)base[this.from];
			}
			set
			{
				base[this.from] = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x0008D3A1 File Offset: 0x0008B5A1
		[ConfigurationProperty("network")]
		public SmtpNetworkElement Network
		{
			get
			{
				return (SmtpNetworkElement)base[this.network];
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0008D3B4 File Offset: 0x0008B5B4
		[ConfigurationProperty("specifiedPickupDirectory")]
		public SmtpSpecifiedPickupDirectoryElement SpecifiedPickupDirectory
		{
			get
			{
				return (SmtpSpecifiedPickupDirectoryElement)base[this.specifiedPickupDirectory];
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x0008D3C7 File Offset: 0x0008B5C7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C98 RID: 7320
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C99 RID: 7321
		private readonly ConfigurationProperty from = new ConfigurationProperty("from", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C9A RID: 7322
		private readonly ConfigurationProperty network = new ConfigurationProperty("network", typeof(SmtpNetworkElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C9B RID: 7323
		private readonly ConfigurationProperty specifiedPickupDirectory = new ConfigurationProperty("specifiedPickupDirectory", typeof(SmtpSpecifiedPickupDirectoryElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C9C RID: 7324
		private readonly ConfigurationProperty deliveryMethod = new ConfigurationProperty("deliveryMethod", typeof(SmtpDeliveryMethod), SmtpDeliveryMethod.Network, new SmtpSection.SmtpDeliveryMethodTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C9D RID: 7325
		private readonly ConfigurationProperty deliveryFormat = new ConfigurationProperty("deliveryFormat", typeof(SmtpDeliveryFormat), SmtpDeliveryFormat.SevenBit, new SmtpSection.SmtpDeliveryFormatTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x020007C5 RID: 1989
		private class SmtpDeliveryMethodTypeConverter : TypeConverter
		{
			// Token: 0x06004390 RID: 17296 RVA: 0x0011D089 File Offset: 0x0011B289
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06004391 RID: 17297 RVA: 0x0011D0A8 File Offset: 0x0011B2A8
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text != null)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					if (text == "network")
					{
						return SmtpDeliveryMethod.Network;
					}
					if (text == "specifiedpickupdirectory")
					{
						return SmtpDeliveryMethod.SpecifiedPickupDirectory;
					}
					if (text == "pickupdirectoryfromiis")
					{
						return SmtpDeliveryMethod.PickupDirectoryFromIis;
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
		}

		// Token: 0x020007C6 RID: 1990
		private class SmtpDeliveryFormatTypeConverter : TypeConverter
		{
			// Token: 0x06004393 RID: 17299 RVA: 0x0011D11A File Offset: 0x0011B31A
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06004394 RID: 17300 RVA: 0x0011D138 File Offset: 0x0011B338
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text != null)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					if (text == "sevenbit")
					{
						return SmtpDeliveryFormat.SevenBit;
					}
					if (text == "international")
					{
						return SmtpDeliveryFormat.International;
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
}
