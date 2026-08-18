using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200069F RID: 1695
	public sealed class WSDualHttpSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x000F8EA0 File Offset: 0x000F70A0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(WSDualHttpSecurityMode), WSDualHttpSecurityMode.Message, null, new ServiceModelEnumValidator(typeof(WSDualHttpSecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("message", typeof(MessageSecurityOverHttpElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x000F8F17 File Offset: 0x000F7117
		// (set) Token: 0x060041AB RID: 16811 RVA: 0x000F8F29 File Offset: 0x000F7129
		[ConfigurationProperty("mode", DefaultValue = WSDualHttpSecurityMode.Message)]
		[ServiceModelEnumValidator(typeof(WSDualHttpSecurityModeHelper))]
		public WSDualHttpSecurityMode Mode
		{
			get
			{
				return (WSDualHttpSecurityMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x000F8F3C File Offset: 0x000F713C
		[ConfigurationProperty("message")]
		public MessageSecurityOverHttpElement Message
		{
			get
			{
				return (MessageSecurityOverHttpElement)base["message"];
			}
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x000F8F4E File Offset: 0x000F714E
		internal void ApplyConfiguration(WSDualHttpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			this.Message.ApplyConfiguration(security.Message);
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000F8F80 File Offset: 0x000F7180
		internal void InitializeFrom(WSDualHttpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<WSDualHttpSecurityMode>("mode", security.Mode);
			this.Message.InitializeFrom(security.Message);
		}

		// Token: 0x04002CED RID: 11501
		private ConfigurationPropertyCollection properties;
	}
}
