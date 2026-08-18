using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000661 RID: 1633
	public sealed class NetMsmqSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x000EED51 File Offset: 0x000ECF51
		// (set) Token: 0x06003ECF RID: 16079 RVA: 0x000EED63 File Offset: 0x000ECF63
		[ConfigurationProperty("mode", DefaultValue = NetMsmqSecurityMode.Transport)]
		[ServiceModelEnumValidator(typeof(SecurityModeHelper))]
		public NetMsmqSecurityMode Mode
		{
			get
			{
				return (NetMsmqSecurityMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x000EED76 File Offset: 0x000ECF76
		[ConfigurationProperty("transport")]
		public MsmqTransportSecurityElement Transport
		{
			get
			{
				return (MsmqTransportSecurityElement)base["transport"];
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x000EED88 File Offset: 0x000ECF88
		[ConfigurationProperty("message")]
		public MessageSecurityOverMsmqElement Message
		{
			get
			{
				return (MessageSecurityOverMsmqElement)base["message"];
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000EED9C File Offset: 0x000ECF9C
		internal void ApplyConfiguration(NetMsmqSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			this.Transport.ApplyConfiguration(security.Transport);
			this.Message.ApplyConfiguration(security.Message);
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000EEDEC File Offset: 0x000ECFEC
		internal void InitializeFrom(NetMsmqSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<NetMsmqSecurityMode>("mode", security.Mode);
			this.Transport.InitializeFrom(security.Transport);
			this.Message.InitializeFrom(security.Message);
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x000EEE40 File Offset: 0x000ED040
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(NetMsmqSecurityMode), NetMsmqSecurityMode.Transport, null, new ServiceModelEnumValidator(typeof(SecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("transport", typeof(MsmqTransportSecurityElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("message", typeof(MessageSecurityOverMsmqElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CAB RID: 11435
		private ConfigurationPropertyCollection properties;
	}
}
