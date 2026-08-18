using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Net.Mail;

namespace System.Net.Configuration
{
	// Token: 0x02000661 RID: 1633
	public sealed class SmtpSection : ConfigurationSection
	{
		// Token: 0x06003292 RID: 12946 RVA: 0x000D6D30 File Offset: 0x000D5D30
		public SmtpSection()
		{
			this.properties.Add(this.deliveryMethod);
			this.properties.Add(this.from);
			this.properties.Add(this.network);
			this.properties.Add(this.specifiedPickupDirectory);
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x000D6E0D File Offset: 0x000D5E0D
		// (set) Token: 0x06003294 RID: 12948 RVA: 0x000D6E20 File Offset: 0x000D5E20
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

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x000D6E34 File Offset: 0x000D5E34
		// (set) Token: 0x06003296 RID: 12950 RVA: 0x000D6E47 File Offset: 0x000D5E47
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

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x000D6E56 File Offset: 0x000D5E56
		[ConfigurationProperty("network")]
		public SmtpNetworkElement Network
		{
			get
			{
				return (SmtpNetworkElement)base[this.network];
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x000D6E69 File Offset: 0x000D5E69
		[ConfigurationProperty("specifiedPickupDirectory")]
		public SmtpSpecifiedPickupDirectoryElement SpecifiedPickupDirectory
		{
			get
			{
				return (SmtpSpecifiedPickupDirectoryElement)base[this.specifiedPickupDirectory];
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x000D6E7C File Offset: 0x000D5E7C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002F57 RID: 12119
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F58 RID: 12120
		private readonly ConfigurationProperty from = new ConfigurationProperty("from", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F59 RID: 12121
		private readonly ConfigurationProperty network = new ConfigurationProperty("network", typeof(SmtpNetworkElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F5A RID: 12122
		private readonly ConfigurationProperty specifiedPickupDirectory = new ConfigurationProperty("specifiedPickupDirectory", typeof(SmtpSpecifiedPickupDirectoryElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F5B RID: 12123
		private readonly ConfigurationProperty deliveryMethod = new ConfigurationProperty("deliveryMethod", typeof(SmtpDeliveryMethod), SmtpDeliveryMethod.Network, new SmtpSection.SmtpDeliveryMethodTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x02000662 RID: 1634
		private class SmtpDeliveryMethodTypeConverter : TypeConverter
		{
			// Token: 0x0600329A RID: 12954 RVA: 0x000D6E84 File Offset: 0x000D5E84
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x0600329B RID: 12955 RVA: 0x000D6EA0 File Offset: 0x000D5EA0
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text != null)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					string a;
					if ((a = text) != null)
					{
						if (a == "network")
						{
							return SmtpDeliveryMethod.Network;
						}
						if (a == "specifiedpickupdirectory")
						{
							return SmtpDeliveryMethod.SpecifiedPickupDirectory;
						}
						if (a == "pickupdirectoryfromiis")
						{
							return SmtpDeliveryMethod.PickupDirectoryFromIis;
						}
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
}
