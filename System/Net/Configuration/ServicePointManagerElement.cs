using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000660 RID: 1632
	public sealed class ServicePointManagerElement : ConfigurationElement
	{
		// Token: 0x06003283 RID: 12931 RVA: 0x000D6A28 File Offset: 0x000D5A28
		public ServicePointManagerElement()
		{
			this.properties.Add(this.checkCertificateName);
			this.properties.Add(this.checkCertificateRevocationList);
			this.properties.Add(this.dnsRefreshTimeout);
			this.properties.Add(this.enableDnsRoundRobin);
			this.properties.Add(this.expect100Continue);
			this.properties.Add(this.useNagleAlgorithm);
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000D6B80 File Offset: 0x000D5B80
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

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000D6C3C File Offset: 0x000D5C3C
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000D6C4F File Offset: 0x000D5C4F
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

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000D6C63 File Offset: 0x000D5C63
		// (set) Token: 0x06003288 RID: 12936 RVA: 0x000D6C76 File Offset: 0x000D5C76
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

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000D6C8A File Offset: 0x000D5C8A
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x000D6C9D File Offset: 0x000D5C9D
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

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000D6CB1 File Offset: 0x000D5CB1
		// (set) Token: 0x0600328C RID: 12940 RVA: 0x000D6CC4 File Offset: 0x000D5CC4
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

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x000D6CD8 File Offset: 0x000D5CD8
		// (set) Token: 0x0600328E RID: 12942 RVA: 0x000D6CEB File Offset: 0x000D5CEB
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

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x000D6CFF File Offset: 0x000D5CFF
		// (set) Token: 0x06003290 RID: 12944 RVA: 0x000D6D12 File Offset: 0x000D5D12
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

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x000D6D26 File Offset: 0x000D5D26
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002F50 RID: 12112
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F51 RID: 12113
		private readonly ConfigurationProperty checkCertificateName = new ConfigurationProperty("checkCertificateName", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002F52 RID: 12114
		private readonly ConfigurationProperty checkCertificateRevocationList = new ConfigurationProperty("checkCertificateRevocationList", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F53 RID: 12115
		private readonly ConfigurationProperty dnsRefreshTimeout = new ConfigurationProperty("dnsRefreshTimeout", typeof(int), 120000, null, new TimeoutValidator(true), ConfigurationPropertyOptions.None);

		// Token: 0x04002F54 RID: 12116
		private readonly ConfigurationProperty enableDnsRoundRobin = new ConfigurationProperty("enableDnsRoundRobin", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F55 RID: 12117
		private readonly ConfigurationProperty expect100Continue = new ConfigurationProperty("expect100Continue", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002F56 RID: 12118
		private readonly ConfigurationProperty useNagleAlgorithm = new ConfigurationProperty("useNagleAlgorithm", typeof(bool), true, ConfigurationPropertyOptions.None);
	}
}
