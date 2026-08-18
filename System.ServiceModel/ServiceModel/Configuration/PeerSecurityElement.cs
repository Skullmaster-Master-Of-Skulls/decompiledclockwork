using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200066B RID: 1643
	public sealed class PeerSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x000EFF17 File Offset: 0x000EE117
		// (set) Token: 0x06003F27 RID: 16167 RVA: 0x000EFF29 File Offset: 0x000EE129
		[ConfigurationProperty("mode", DefaultValue = SecurityMode.Transport)]
		[ServiceModelEnumValidator(typeof(SecurityModeHelper))]
		public SecurityMode Mode
		{
			get
			{
				return (SecurityMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x000EFF3C File Offset: 0x000EE13C
		[ConfigurationProperty("transport")]
		public PeerTransportSecurityElement Transport
		{
			get
			{
				return (PeerTransportSecurityElement)base["transport"];
			}
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x000EFF4E File Offset: 0x000EE14E
		internal void ApplyConfiguration(PeerSecuritySettings security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			if (security.Mode != SecurityMode.None)
			{
				this.Transport.ApplyConfiguration(security.Transport);
			}
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x000EFF88 File Offset: 0x000EE188
		internal void InitializeFrom(PeerSecuritySettings security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<SecurityMode>("mode", security.Mode);
			if (security.Mode != SecurityMode.None)
			{
				this.Transport.InitializeFrom(security.Transport);
			}
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x000EFFC7 File Offset: 0x000EE1C7
		internal void CopyFrom(PeerSecurityElement source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			this.Mode = source.Mode;
			if (source.Mode != SecurityMode.None)
			{
				this.Transport.CopyFrom(source.Transport);
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x000F0004 File Offset: 0x000EE204
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(SecurityMode), SecurityMode.Transport, null, new ServiceModelEnumValidator(typeof(SecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("transport", typeof(PeerTransportSecurityElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CB2 RID: 11442
		private ConfigurationPropertyCollection properties;
	}
}
