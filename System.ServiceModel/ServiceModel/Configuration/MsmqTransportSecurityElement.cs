using System;
using System.Configuration;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000648 RID: 1608
	public sealed class MsmqTransportSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06003E00 RID: 15872 RVA: 0x000EC9F0 File Offset: 0x000EABF0
		// (set) Token: 0x06003E01 RID: 15873 RVA: 0x000ECA02 File Offset: 0x000EAC02
		[ConfigurationProperty("msmqAuthenticationMode", DefaultValue = MsmqAuthenticationMode.WindowsDomain)]
		[ServiceModelEnumValidator(typeof(MsmqAuthenticationModeHelper))]
		public MsmqAuthenticationMode MsmqAuthenticationMode
		{
			get
			{
				return (MsmqAuthenticationMode)base["msmqAuthenticationMode"];
			}
			set
			{
				base["msmqAuthenticationMode"] = value;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000ECA15 File Offset: 0x000EAC15
		// (set) Token: 0x06003E03 RID: 15875 RVA: 0x000ECA27 File Offset: 0x000EAC27
		[ConfigurationProperty("msmqEncryptionAlgorithm", DefaultValue = MsmqEncryptionAlgorithm.RC4Stream)]
		[ServiceModelEnumValidator(typeof(MsmqEncryptionAlgorithmHelper))]
		public MsmqEncryptionAlgorithm MsmqEncryptionAlgorithm
		{
			get
			{
				return (MsmqEncryptionAlgorithm)base["msmqEncryptionAlgorithm"];
			}
			set
			{
				base["msmqEncryptionAlgorithm"] = value;
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x000ECA3A File Offset: 0x000EAC3A
		// (set) Token: 0x06003E05 RID: 15877 RVA: 0x000ECA4C File Offset: 0x000EAC4C
		[ConfigurationProperty("msmqProtectionLevel", DefaultValue = ProtectionLevel.Sign)]
		[ServiceModelEnumValidator(typeof(ProtectionLevelHelper))]
		public ProtectionLevel MsmqProtectionLevel
		{
			get
			{
				return (ProtectionLevel)base["msmqProtectionLevel"];
			}
			set
			{
				base["msmqProtectionLevel"] = value;
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000ECA5F File Offset: 0x000EAC5F
		// (set) Token: 0x06003E07 RID: 15879 RVA: 0x000ECA7F File Offset: 0x000EAC7F
		[ConfigurationProperty("msmqSecureHashAlgorithm")]
		[ServiceModelEnumValidator(typeof(MsmqSecureHashAlgorithmHelper))]
		public MsmqSecureHashAlgorithm MsmqSecureHashAlgorithm
		{
			get
			{
				return (MsmqSecureHashAlgorithm)(base["msmqSecureHashAlgorithm"] ?? MsmqDefaults.MsmqSecureHashAlgorithm);
			}
			set
			{
				base["msmqSecureHashAlgorithm"] = value;
			}
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000ECA94 File Offset: 0x000EAC94
		internal void ApplyConfiguration(MsmqTransportSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.MsmqAuthenticationMode = this.MsmqAuthenticationMode;
			security.MsmqEncryptionAlgorithm = this.MsmqEncryptionAlgorithm;
			security.MsmqProtectionLevel = this.MsmqProtectionLevel;
			security.MsmqSecureHashAlgorithm = this.MsmqSecureHashAlgorithm;
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000ECAE4 File Offset: 0x000EACE4
		internal void InitializeFrom(MsmqTransportSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<MsmqAuthenticationMode>("msmqAuthenticationMode", security.MsmqAuthenticationMode);
			base.SetPropertyValueIfNotDefaultValue<MsmqEncryptionAlgorithm>("msmqEncryptionAlgorithm", security.MsmqEncryptionAlgorithm);
			base.SetPropertyValueIfNotDefaultValue<ProtectionLevel>("msmqProtectionLevel", security.MsmqProtectionLevel);
			base.SetPropertyValueIfNotDefaultValue<MsmqSecureHashAlgorithm>("msmqSecureHashAlgorithm", security.MsmqSecureHashAlgorithm);
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x000ECB48 File Offset: 0x000EAD48
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("msmqAuthenticationMode", typeof(MsmqAuthenticationMode), MsmqAuthenticationMode.WindowsDomain, null, new ServiceModelEnumValidator(typeof(MsmqAuthenticationModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("msmqEncryptionAlgorithm", typeof(MsmqEncryptionAlgorithm), MsmqEncryptionAlgorithm.RC4Stream, null, new ServiceModelEnumValidator(typeof(MsmqEncryptionAlgorithmHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("msmqProtectionLevel", typeof(ProtectionLevel), ProtectionLevel.Sign, null, new ServiceModelEnumValidator(typeof(ProtectionLevelHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("msmqSecureHashAlgorithm", typeof(MsmqSecureHashAlgorithm), MsmqSecureHashAlgorithm.Sha1, null, new ServiceModelEnumValidator(typeof(MsmqSecureHashAlgorithmHelper)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C9A RID: 11418
		private ConfigurationPropertyCollection properties;
	}
}
