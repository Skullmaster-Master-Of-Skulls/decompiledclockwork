using System;
using System.Configuration;
using System.Net.Security;

namespace System.Net.Configuration
{
	// Token: 0x02000341 RID: 833
	public sealed class ServicePointManagerElement : ConfigurationElement
	{
		// Token: 0x06001DE7 RID: 7655 RVA: 0x0008CEC8 File Offset: 0x0008B0C8
		public ServicePointManagerElement()
		{
			this.properties.Add(this.checkCertificateName);
			this.properties.Add(this.checkCertificateRevocationList);
			this.properties.Add(this.dnsRefreshTimeout);
			this.properties.Add(this.enableDnsRoundRobin);
			this.properties.Add(this.encryptionPolicy);
			this.properties.Add(this.expect100Continue);
			this.properties.Add(this.useNagleAlgorithm);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0008D050 File Offset: 0x0008B250
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			PropertyInformation[] array = new PropertyInformation[]
			{
				base.ElementInformation.Properties["checkCertificateName"],
				base.ElementInformation.Properties["checkCertificateRevocationList"]
			};
			foreach (PropertyInformation propertyInformation in array)
			{
				if (propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere)
				{
					try
					{
						ExceptionHelper.UnmanagedPermission.Demand();
					}
					catch (Exception inner)
					{
						throw new ConfigurationErrorsException(SR.GetString("net_config_property_permission", new object[]
						{
							propertyInformation.Name
						}), inner);
					}
				}
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0008D100 File Offset: 0x0008B300
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x0008D113 File Offset: 0x0008B313
		[ConfigurationProperty("checkCertificateName", DefaultValue = true)]
		public bool CheckCertificateName
		{
			get
			{
				return (bool)base[this.checkCertificateName];
			}
			set
			{
				base[this.checkCertificateName] = value;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x0008D127 File Offset: 0x0008B327
		// (set) Token: 0x06001DEC RID: 7660 RVA: 0x0008D13A File Offset: 0x0008B33A
		[ConfigurationProperty("checkCertificateRevocationList", DefaultValue = false)]
		public bool CheckCertificateRevocationList
		{
			get
			{
				return (bool)base[this.checkCertificateRevocationList];
			}
			set
			{
				base[this.checkCertificateRevocationList] = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x0008D14E File Offset: 0x0008B34E
		// (set) Token: 0x06001DEE RID: 7662 RVA: 0x0008D161 File Offset: 0x0008B361
		[ConfigurationProperty("dnsRefreshTimeout", DefaultValue = 120000)]
		public int DnsRefreshTimeout
		{
			get
			{
				return (int)base[this.dnsRefreshTimeout];
			}
			set
			{
				base[this.dnsRefreshTimeout] = value;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0008D175 File Offset: 0x0008B375
		// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x0008D188 File Offset: 0x0008B388
		[ConfigurationProperty("enableDnsRoundRobin", DefaultValue = false)]
		public bool EnableDnsRoundRobin
		{
			get
			{
				return (bool)base[this.enableDnsRoundRobin];
			}
			set
			{
				base[this.enableDnsRoundRobin] = value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0008D19C File Offset: 0x0008B39C
		// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x0008D1AF File Offset: 0x0008B3AF
		[ConfigurationProperty("encryptionPolicy", DefaultValue = EncryptionPolicy.RequireEncryption)]
		public EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return (EncryptionPolicy)base[this.encryptionPolicy];
			}
			set
			{
				base[this.encryptionPolicy] = value;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0008D1C3 File Offset: 0x0008B3C3
		// (set) Token: 0x06001DF4 RID: 7668 RVA: 0x0008D1D6 File Offset: 0x0008B3D6
		[ConfigurationProperty("expect100Continue", DefaultValue = true)]
		public bool Expect100Continue
		{
			get
			{
				return (bool)base[this.expect100Continue];
			}
			set
			{
				base[this.expect100Continue] = value;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x0008D1EA File Offset: 0x0008B3EA
		// (set) Token: 0x06001DF6 RID: 7670 RVA: 0x0008D1FD File Offset: 0x0008B3FD
		[ConfigurationProperty("useNagleAlgorithm", DefaultValue = true)]
		public bool UseNagleAlgorithm
		{
			get
			{
				return (bool)base[this.useNagleAlgorithm];
			}
			set
			{
				base[this.useNagleAlgorithm] = value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x0008D211 File Offset: 0x0008B411
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C90 RID: 7312
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C91 RID: 7313
		private readonly ConfigurationProperty checkCertificateName = new ConfigurationProperty("checkCertificateName", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C92 RID: 7314
		private readonly ConfigurationProperty checkCertificateRevocationList = new ConfigurationProperty("checkCertificateRevocationList", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001C93 RID: 7315
		private readonly ConfigurationProperty dnsRefreshTimeout = new ConfigurationProperty("dnsRefreshTimeout", typeof(int), 120000, null, new TimeoutValidator(true), ConfigurationPropertyOptions.None);

		// Token: 0x04001C94 RID: 7316
		private readonly ConfigurationProperty enableDnsRoundRobin = new ConfigurationProperty("enableDnsRoundRobin", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001C95 RID: 7317
		private readonly ConfigurationProperty encryptionPolicy = new ConfigurationProperty("encryptionPolicy", typeof(EncryptionPolicy), EncryptionPolicy.RequireEncryption, ConfigurationPropertyOptions.None);

		// Token: 0x04001C96 RID: 7318
		private readonly ConfigurationProperty expect100Continue = new ConfigurationProperty("expect100Continue", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C97 RID: 7319
		private readonly ConfigurationProperty useNagleAlgorithm = new ConfigurationProperty("useNagleAlgorithm", typeof(bool), true, ConfigurationPropertyOptions.None);
	}
}
