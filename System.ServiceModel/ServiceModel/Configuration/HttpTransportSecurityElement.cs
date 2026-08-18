using System;
using System.Configuration;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200062F RID: 1583
	public sealed class HttpTransportSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x000E78E4 File Offset: 0x000E5AE4
		// (set) Token: 0x06003CB7 RID: 15543 RVA: 0x000E78F6 File Offset: 0x000E5AF6
		[ConfigurationProperty("clientCredentialType", DefaultValue = HttpClientCredentialType.None)]
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

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x000E7909 File Offset: 0x000E5B09
		// (set) Token: 0x06003CB9 RID: 15545 RVA: 0x000E791B File Offset: 0x000E5B1B
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

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x000E792E File Offset: 0x000E5B2E
		// (set) Token: 0x06003CBB RID: 15547 RVA: 0x000E7940 File Offset: 0x000E5B40
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

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x000E794E File Offset: 0x000E5B4E
		// (set) Token: 0x06003CBD RID: 15549 RVA: 0x000E7960 File Offset: 0x000E5B60
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

		// Token: 0x06003CBE RID: 15550 RVA: 0x000E7980 File Offset: 0x000E5B80
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

		// Token: 0x06003CBF RID: 15551 RVA: 0x000E79D8 File Offset: 0x000E5BD8
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

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x000E7A3C File Offset: 0x000E5C3C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("clientCredentialType", typeof(HttpClientCredentialType), HttpClientCredentialType.None, null, new ServiceModelEnumValidator(typeof(HttpClientCredentialTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("proxyCredentialType", typeof(HttpProxyCredentialType), HttpProxyCredentialType.None, null, new ServiceModelEnumValidator(typeof(HttpProxyCredentialTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("extendedProtectionPolicy", typeof(ExtendedProtectionPolicyElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("realm", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C85 RID: 11397
		private ConfigurationPropertyCollection properties;
	}
}
