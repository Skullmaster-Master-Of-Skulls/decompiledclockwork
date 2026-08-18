using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200065E RID: 1630
	public sealed class NetNamedPipeSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06003EB7 RID: 16055 RVA: 0x000EE968 File Offset: 0x000ECB68
		// (set) Token: 0x06003EB8 RID: 16056 RVA: 0x000EE97A File Offset: 0x000ECB7A
		[ConfigurationProperty("mode", DefaultValue = NetNamedPipeSecurityMode.Transport)]
		[ServiceModelEnumValidator(typeof(NetNamedPipeSecurityModeHelper))]
		public NetNamedPipeSecurityMode Mode
		{
			get
			{
				return (NetNamedPipeSecurityMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06003EB9 RID: 16057 RVA: 0x000EE98D File Offset: 0x000ECB8D
		[ConfigurationProperty("transport")]
		public NamedPipeTransportSecurityElement Transport
		{
			get
			{
				return (NamedPipeTransportSecurityElement)base["transport"];
			}
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x000EE99F File Offset: 0x000ECB9F
		internal void ApplyConfiguration(NetNamedPipeSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			this.Transport.ApplyConfiguration(security.Transport);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x000EE9D1 File Offset: 0x000ECBD1
		internal void InitializeFrom(NetNamedPipeSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<NetNamedPipeSecurityMode>("mode", security.Mode);
			this.Transport.InitializeFrom(security.Transport);
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000EEA08 File Offset: 0x000ECC08
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(NetNamedPipeSecurityMode), NetNamedPipeSecurityMode.Transport, null, new ServiceModelEnumValidator(typeof(NetNamedPipeSecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("transport", typeof(NamedPipeTransportSecurityElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CA9 RID: 11433
		private ConfigurationPropertyCollection properties;
	}
}
