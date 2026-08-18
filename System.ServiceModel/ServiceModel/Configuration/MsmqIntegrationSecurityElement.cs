using System;
using System.Configuration;
using System.ServiceModel.MsmqIntegration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000645 RID: 1605
	public sealed class MsmqIntegrationSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x000EBE60 File Offset: 0x000EA060
		// (set) Token: 0x06003DCD RID: 15821 RVA: 0x000EBE72 File Offset: 0x000EA072
		[ConfigurationProperty("mode", DefaultValue = MsmqIntegrationSecurityMode.Transport)]
		[ServiceModelEnumValidator(typeof(MsmqIntegrationSecurityModeHelper))]
		public MsmqIntegrationSecurityMode Mode
		{
			get
			{
				return (MsmqIntegrationSecurityMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x000EBE85 File Offset: 0x000EA085
		[ConfigurationProperty("transport")]
		public MsmqTransportSecurityElement Transport
		{
			get
			{
				return (MsmqTransportSecurityElement)base["transport"];
			}
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000EBE97 File Offset: 0x000EA097
		internal void ApplyConfiguration(MsmqIntegrationSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			this.Transport.ApplyConfiguration(security.Transport);
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000EBEC9 File Offset: 0x000EA0C9
		internal void InitializeFrom(MsmqIntegrationSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<MsmqIntegrationSecurityMode>("mode", security.Mode);
			this.Transport.InitializeFrom(security.Transport);
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06003DD1 RID: 15825 RVA: 0x000EBF00 File Offset: 0x000EA100
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(MsmqIntegrationSecurityMode), MsmqIntegrationSecurityMode.Transport, null, new ServiceModelEnumValidator(typeof(MsmqIntegrationSecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("transport", typeof(MsmqTransportSecurityElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C97 RID: 11415
		private ConfigurationPropertyCollection properties;
	}
}
