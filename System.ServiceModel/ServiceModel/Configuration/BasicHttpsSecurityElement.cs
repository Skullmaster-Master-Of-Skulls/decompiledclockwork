using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F2 RID: 1522
	public sealed class BasicHttpsSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003A9F RID: 15007 RVA: 0x000E1449 File Offset: 0x000DF649
		// (set) Token: 0x06003AA0 RID: 15008 RVA: 0x000E145B File Offset: 0x000DF65B
		[ConfigurationProperty("mode", DefaultValue = BasicHttpsSecurityMode.Transport)]
		[ServiceModelEnumValidator(typeof(BasicHttpsSecurityModeHelper))]
		public BasicHttpsSecurityMode Mode
		{
			get
			{
				return (BasicHttpsSecurityMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x000E146E File Offset: 0x000DF66E
		[ConfigurationProperty("transport")]
		public HttpTransportSecurityElement Transport
		{
			get
			{
				return (HttpTransportSecurityElement)base["transport"];
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x000E1480 File Offset: 0x000DF680
		[ConfigurationProperty("message")]
		public BasicHttpMessageSecurityElement Message
		{
			get
			{
				return (BasicHttpMessageSecurityElement)base["message"];
			}
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000E1494 File Offset: 0x000DF694
		internal void ApplyConfiguration(BasicHttpsSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			this.Transport.ApplyConfiguration(security.Transport);
			this.Message.ApplyConfiguration(security.Message);
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000E14E4 File Offset: 0x000DF6E4
		internal void InitializeFrom(BasicHttpsSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<BasicHttpsSecurityMode>("mode", security.Mode);
			this.Transport.InitializeFrom(security.Transport);
			this.Message.InitializeFrom(security.Message);
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003AA5 RID: 15013 RVA: 0x000E1538 File Offset: 0x000DF738
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(BasicHttpsSecurityMode), BasicHttpsSecurityMode.Transport, null, new ServiceModelEnumValidator(typeof(BasicHttpsSecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("transport", typeof(HttpTransportSecurityElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("message", typeof(BasicHttpMessageSecurityElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A74 RID: 10868
		private ConfigurationPropertyCollection properties;
	}
}
