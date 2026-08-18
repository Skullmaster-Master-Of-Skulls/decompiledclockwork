using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A0 RID: 1696
	public sealed class WSHttpSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x000F8FC0 File Offset: 0x000F71C0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(SecurityMode), SecurityMode.Message, null, new ServiceModelEnumValidator(typeof(SecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("transport", typeof(WSHttpTransportSecurityElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("message", typeof(NonDualMessageSecurityOverHttpElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x000F9055 File Offset: 0x000F7255
		// (set) Token: 0x060041B2 RID: 16818 RVA: 0x000F9067 File Offset: 0x000F7267
		[ConfigurationProperty("mode", DefaultValue = SecurityMode.Message)]
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

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x000F907A File Offset: 0x000F727A
		[ConfigurationProperty("transport")]
		public WSHttpTransportSecurityElement Transport
		{
			get
			{
				return (WSHttpTransportSecurityElement)base["transport"];
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x000F908C File Offset: 0x000F728C
		[ConfigurationProperty("message")]
		public NonDualMessageSecurityOverHttpElement Message
		{
			get
			{
				return (NonDualMessageSecurityOverHttpElement)base["message"];
			}
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x000F90A0 File Offset: 0x000F72A0
		internal void ApplyConfiguration(WSHttpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			this.Transport.ApplyConfiguration(security.Transport);
			this.Message.ApplyConfiguration(security.Message);
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x000F90F0 File Offset: 0x000F72F0
		internal void InitializeFrom(WSHttpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<SecurityMode>("mode", security.Mode);
			this.Transport.InitializeFrom(security.Transport);
			this.Message.InitializeFrom(security.Message);
		}

		// Token: 0x04002CEE RID: 11502
		private ConfigurationPropertyCollection properties;
	}
}
