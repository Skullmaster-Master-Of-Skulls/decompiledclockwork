using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000651 RID: 1617
	public sealed class NetHttpBindingElement : HttpBindingBaseElement
	{
		// Token: 0x06003E4D RID: 15949 RVA: 0x000ED70D File Offset: 0x000EB90D
		public NetHttpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x000ED716 File Offset: 0x000EB916
		public NetHttpBindingElement() : this(null)
		{
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003E4F RID: 15951 RVA: 0x000ED71F File Offset: 0x000EB91F
		// (set) Token: 0x06003E50 RID: 15952 RVA: 0x000ED731 File Offset: 0x000EB931
		[ConfigurationProperty("messageEncoding", DefaultValue = NetHttpMessageEncoding.Binary)]
		[ServiceModelEnumValidator(typeof(NetHttpMessageEncodingHelper))]
		public NetHttpMessageEncoding MessageEncoding
		{
			get
			{
				return (NetHttpMessageEncoding)base["messageEncoding"];
			}
			set
			{
				base["messageEncoding"] = value;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x000ED744 File Offset: 0x000EB944
		[ConfigurationProperty("reliableSession")]
		public StandardBindingOptionalReliableSessionElement ReliableSession
		{
			get
			{
				return (StandardBindingOptionalReliableSessionElement)base["reliableSession"];
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x000ED756 File Offset: 0x000EB956
		[ConfigurationProperty("security")]
		public BasicHttpSecurityElement Security
		{
			get
			{
				return (BasicHttpSecurityElement)base["security"];
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003E53 RID: 15955 RVA: 0x000ED768 File Offset: 0x000EB968
		// (set) Token: 0x06003E54 RID: 15956 RVA: 0x000ED77A File Offset: 0x000EB97A
		[ConfigurationProperty("webSocketSettings")]
		public NetHttpWebSocketTransportSettingsElement WebSocketSettings
		{
			get
			{
				return (NetHttpWebSocketTransportSettingsElement)base["webSocketSettings"];
			}
			set
			{
				base["webSocketSettings"] = value;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003E55 RID: 15957 RVA: 0x000ED788 File Offset: 0x000EB988
		protected override Type BindingElementType
		{
			get
			{
				return typeof(NetHttpBinding);
			}
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000ED794 File Offset: 0x000EB994
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			NetHttpBinding netHttpBinding = (NetHttpBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<NetHttpMessageEncoding>("messageEncoding", netHttpBinding.MessageEncoding);
			this.WebSocketSettings.InitializeFrom(netHttpBinding.WebSocketSettings);
			this.ReliableSession.InitializeFrom(netHttpBinding.ReliableSession);
			this.Security.InitializeFrom(netHttpBinding.Security);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000ED7F4 File Offset: 0x000EB9F4
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			NetHttpBinding netHttpBinding = (NetHttpBinding)binding;
			netHttpBinding.MessageEncoding = this.MessageEncoding;
			this.WebSocketSettings.ApplyConfiguration(netHttpBinding.WebSocketSettings);
			this.ReliableSession.ApplyConfiguration(netHttpBinding.ReliableSession);
			this.Security.ApplyConfiguration(netHttpBinding.Security);
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003E58 RID: 15960 RVA: 0x000ED850 File Offset: 0x000EBA50
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("messageEncoding", typeof(NetHttpMessageEncoding), NetHttpMessageEncoding.Binary, null, new ServiceModelEnumValidator(typeof(NetHttpMessageEncodingHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("reliableSession", typeof(StandardBindingOptionalReliableSessionElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(BasicHttpSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("webSocketSettings", typeof(NetHttpWebSocketTransportSettingsElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CA3 RID: 11427
		private ConfigurationPropertyCollection properties;
	}
}
