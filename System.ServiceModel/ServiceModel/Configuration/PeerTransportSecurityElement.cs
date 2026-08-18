using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000670 RID: 1648
	public sealed class PeerTransportSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x000F0459 File Offset: 0x000EE659
		// (set) Token: 0x06003F49 RID: 16201 RVA: 0x000F046B File Offset: 0x000EE66B
		[ConfigurationProperty("credentialType", DefaultValue = PeerTransportCredentialType.Password)]
		[ServiceModelEnumValidator(typeof(PeerTransportCredentialTypeHelper))]
		public PeerTransportCredentialType CredentialType
		{
			get
			{
				return (PeerTransportCredentialType)base["credentialType"];
			}
			set
			{
				base["credentialType"] = value;
			}
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000F047E File Offset: 0x000EE67E
		internal void ApplyConfiguration(PeerTransportSecuritySettings security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.CredentialType = this.CredentialType;
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x000F049F File Offset: 0x000EE69F
		internal void InitializeFrom(PeerTransportSecuritySettings security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<PeerTransportCredentialType>("credentialType", security.CredentialType);
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x000F04C5 File Offset: 0x000EE6C5
		internal void CopyFrom(PeerTransportSecurityElement security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			this.CredentialType = security.CredentialType;
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x000F04E8 File Offset: 0x000EE6E8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("credentialType", typeof(PeerTransportCredentialType), PeerTransportCredentialType.Password, null, new ServiceModelEnumValidator(typeof(PeerTransportCredentialTypeHelper)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CB4 RID: 11444
		private ConfigurationPropertyCollection properties;
	}
}
