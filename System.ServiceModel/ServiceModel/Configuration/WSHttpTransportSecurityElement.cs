using System;
using System.Configuration;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006BD RID: 1725
	public sealed class WSHttpTransportSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x060042F5 RID: 17141 RVA: 0x000FCFC4 File Offset: 0x000FB1C4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("clientCredentialType", typeof(HttpClientCredentialType), HttpClientCredentialType.Windows, null, new ServiceModelEnumValidator(typeof(HttpClientCredentialTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("proxyCredentialType", typeof(HttpProxyCredentialType), HttpProxyCredentialType.None, null, new ServiceModelEnumValidator(typeof(HttpProxyCredentialTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("extendedProtectionPolicy", typeof(ExtendedProtectionPolicyElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("realm", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x060042F6 RID: 17142 RVA: 0x000FD09C File Offset: 0x000FB29C
		// (set) Token: 0x060042F7 RID: 17143 RVA: 0x000FD0AE File Offset: 0x000FB2AE
		[ConfigurationProperty("clientCredentialType", DefaultValue = HttpClientCredentialType.Windows)]
		[ServiceModelEnumValidator(typeof(HttpClientCredentialTypeHelper))]
		public HttpClientCredentialType ClientCredentialType
		{
			get
			{
				return (HttpClientCredentialType)base["clientCredentialType"];
			}
			set
			{
				base["clientCredentialType"] = value;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060042F8 RID: 17144 RVA: 0x000FD0C1 File Offset: 0x000FB2C1
		// (set) Token: 0x060042F9 RID: 17145 RVA: 0x000FD0D3 File Offset: 0x000FB2D3
		[ConfigurationProperty("proxyCredentialType", DefaultValue = HttpProxyCredentialType.None)]
		[ServiceModelEnumValidator(typeof(HttpProxyCredentialTypeHelper))]
		public HttpProxyCredentialType ProxyCredentialType
		{
			get
			{
				return (HttpProxyCredentialType)base["proxyCredentialType"];
			}
			set
			{
				base["proxyCredentialType"] = value;
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060042FA RID: 17146 RVA: 0x000FD0E6 File Offset: 0x000FB2E6
		// (set) Token: 0x060042FB RID: 17147 RVA: 0x000FD0F8 File Offset: 0x000FB2F8
		[ConfigurationProperty("extendedProtectionPolicy")]
		public ExtendedProtectionPolicyElement ExtendedProtectionPolicy
		{
			get
			{
				return (ExtendedProtectionPolicyElement)base["extendedProtectionPolicy"];
			}
			private set
			{
				base["extendedProtectionPolicy"] = value;
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060042FC RID: 17148 RVA: 0x000FD106 File Offset: 0x000FB306
		// (set) Token: 0x060042FD RID: 17149 RVA: 0x000FD118 File Offset: 0x000FB318
		[ConfigurationProperty("realm", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string Realm
		{
			get
			{
				return (string)base["realm"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["realm"] = value;
			}
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000FD138 File Offset: 0x000FB338
		internal void ApplyConfiguration(HttpTransportSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.ClientCredentialType = this.ClientCredentialType;
			security.ProxyCredentialType = this.ProxyCredentialType;
			security.Realm = this.Realm;
			security.ExtendedProtectionPolicy = ChannelBindingUtility.BuildPolicy(this.ExtendedProtectionPolicy);
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x000FD190 File Offset: 0x000FB390
		internal void InitializeFrom(HttpTransportSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<HttpClientCredentialType>("clientCredentialType", security.ClientCredentialType);
			base.SetPropertyValueIfNotDefaultValue<HttpProxyCredentialType>("proxyCredentialType", security.ProxyCredentialType);
			base.SetPropertyValueIfNotDefaultValue<string>("realm", security.Realm);
			ChannelBindingUtility.InitializeFrom(security.ExtendedProtectionPolicy, this.ExtendedProtectionPolicy);
		}

		// Token: 0x04002D0D RID: 11533
		private ConfigurationPropertyCollection properties;
	}
}
