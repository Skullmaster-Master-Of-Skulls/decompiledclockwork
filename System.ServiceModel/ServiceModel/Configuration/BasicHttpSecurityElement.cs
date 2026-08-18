using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005EF RID: 1519
	public sealed class BasicHttpSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000E1127 File Offset: 0x000DF327
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000E1139 File Offset: 0x000DF339
		[ConfigurationProperty("mode", DefaultValue = BasicHttpSecurityMode.None)]
		[ServiceModelEnumValidator(typeof(BasicHttpSecurityModeHelper))]
		public BasicHttpSecurityMode Mode
		{
			get
			{
				return (BasicHttpSecurityMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000E114C File Offset: 0x000DF34C
		[ConfigurationProperty("transport")]
		public HttpTransportSecurityElement Transport
		{
			get
			{
				return (HttpTransportSecurityElement)base["transport"];
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x000E115E File Offset: 0x000DF35E
		[ConfigurationProperty("message")]
		public BasicHttpMessageSecurityElement Message
		{
			get
			{
				return (BasicHttpMessageSecurityElement)base["message"];
			}
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x000E1170 File Offset: 0x000DF370
		internal void ApplyConfiguration(BasicHttpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			this.Transport.ApplyConfiguration(security.Transport);
			this.Message.ApplyConfiguration(security.Message);
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000E11C0 File Offset: 0x000DF3C0
		internal void InitializeFrom(BasicHttpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<BasicHttpSecurityMode>("mode", security.Mode);
			this.Transport.InitializeFrom(security.Transport);
			this.Message.InitializeFrom(security.Message);
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x000E1214 File Offset: 0x000DF414
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(BasicHttpSecurityMode), BasicHttpSecurityMode.None, null, new ServiceModelEnumValidator(typeof(BasicHttpSecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("transport", typeof(HttpTransportSecurityElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("message", typeof(BasicHttpMessageSecurityElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A72 RID: 10866
		private ConfigurationPropertyCollection properties;
	}
}
