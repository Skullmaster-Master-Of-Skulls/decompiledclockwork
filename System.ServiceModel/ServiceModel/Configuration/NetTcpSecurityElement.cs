using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000666 RID: 1638
	public sealed class NetTcpSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x000EF598 File Offset: 0x000ED798
		// (set) Token: 0x06003EFB RID: 16123 RVA: 0x000EF5AA File Offset: 0x000ED7AA
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

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x000EF5BD File Offset: 0x000ED7BD
		[ConfigurationProperty("transport")]
		public TcpTransportSecurityElement Transport
		{
			get
			{
				return (TcpTransportSecurityElement)base["transport"];
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06003EFD RID: 16125 RVA: 0x000EF5CF File Offset: 0x000ED7CF
		[ConfigurationProperty("message")]
		public MessageSecurityOverTcpElement Message
		{
			get
			{
				return (MessageSecurityOverTcpElement)base["message"];
			}
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x000EF5E4 File Offset: 0x000ED7E4
		internal void ApplyConfiguration(NetTcpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			this.Transport.ApplyConfiguration(security.Transport);
			this.Message.ApplyConfiguration(security.Message);
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x000EF634 File Offset: 0x000ED834
		internal void InitializeFrom(NetTcpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<SecurityMode>("mode", security.Mode);
			this.Transport.InitializeFrom(security.Transport);
			this.Message.InitializeFrom(security.Message);
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06003F00 RID: 16128 RVA: 0x000EF688 File Offset: 0x000ED888
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(SecurityMode), SecurityMode.Transport, null, new ServiceModelEnumValidator(typeof(SecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("transport", typeof(TcpTransportSecurityElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("message", typeof(MessageSecurityOverTcpElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CAD RID: 11437
		private ConfigurationPropertyCollection properties;
	}
}
