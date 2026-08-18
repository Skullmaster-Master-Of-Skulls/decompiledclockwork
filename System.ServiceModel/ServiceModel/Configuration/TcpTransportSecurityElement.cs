using System;
using System.Configuration;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000694 RID: 1684
	public sealed class TcpTransportSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x000F7800 File Offset: 0x000F5A00
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("clientCredentialType", typeof(TcpClientCredentialType), TcpClientCredentialType.Windows, null, new ServiceModelEnumValidator(typeof(TcpClientCredentialTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("protectionLevel", typeof(ProtectionLevel), ProtectionLevel.EncryptAndSign, null, new ServiceModelEnumValidator(typeof(ProtectionLevelHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("extendedProtectionPolicy", typeof(ExtendedProtectionPolicyElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sslProtocols", typeof(SslProtocols), SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, null, new ServiceModelEnumValidator(typeof(SslProtocolsHelper)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x000F78E0 File Offset: 0x000F5AE0
		// (set) Token: 0x0600412E RID: 16686 RVA: 0x000F78F2 File Offset: 0x000F5AF2
		[ConfigurationProperty("clientCredentialType", DefaultValue = TcpClientCredentialType.Windows)]
		[ServiceModelEnumValidator(typeof(TcpClientCredentialTypeHelper))]
		public TcpClientCredentialType ClientCredentialType
		{
			get
			{
				return (TcpClientCredentialType)base["clientCredentialType"];
			}
			set
			{
				base["clientCredentialType"] = value;
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x000F7905 File Offset: 0x000F5B05
		// (set) Token: 0x06004130 RID: 16688 RVA: 0x000F7917 File Offset: 0x000F5B17
		[ConfigurationProperty("protectionLevel", DefaultValue = ProtectionLevel.EncryptAndSign)]
		[ServiceModelEnumValidator(typeof(ProtectionLevelHelper))]
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return (ProtectionLevel)base["protectionLevel"];
			}
			set
			{
				base["protectionLevel"] = value;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06004131 RID: 16689 RVA: 0x000F792A File Offset: 0x000F5B2A
		// (set) Token: 0x06004132 RID: 16690 RVA: 0x000F793C File Offset: 0x000F5B3C
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

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x000F794A File Offset: 0x000F5B4A
		// (set) Token: 0x06004134 RID: 16692 RVA: 0x000F795C File Offset: 0x000F5B5C
		[ConfigurationProperty("sslProtocols", DefaultValue = (SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12))]
		[ServiceModelEnumValidator(typeof(SslProtocolsHelper))]
		public SslProtocols SslProtocols
		{
			get
			{
				return (SslProtocols)base["sslProtocols"];
			}
			private set
			{
				base["sslProtocols"] = value;
			}
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x000F7970 File Offset: 0x000F5B70
		internal void ApplyConfiguration(TcpTransportSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.ClientCredentialType = this.ClientCredentialType;
			security.ProtectionLevel = this.ProtectionLevel;
			security.ExtendedProtectionPolicy = ChannelBindingUtility.BuildPolicy(this.ExtendedProtectionPolicy);
			security.SslProtocols = this.SslProtocols;
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000F79C8 File Offset: 0x000F5BC8
		internal void InitializeFrom(TcpTransportSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<TcpClientCredentialType>("clientCredentialType", security.ClientCredentialType);
			base.SetPropertyValueIfNotDefaultValue<ProtectionLevel>("protectionLevel", security.ProtectionLevel);
			ChannelBindingUtility.InitializeFrom(security.ExtendedProtectionPolicy, this.ExtendedProtectionPolicy);
			base.SetPropertyValueIfNotDefaultValue<SslProtocols>("sslProtocols", security.SslProtocols);
		}

		// Token: 0x04002CE2 RID: 11490
		private ConfigurationPropertyCollection properties;
	}
}
