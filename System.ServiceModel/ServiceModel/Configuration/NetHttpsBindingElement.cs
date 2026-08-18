using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000653 RID: 1619
	public sealed class NetHttpsBindingElement : HttpBindingBaseElement
	{
		// Token: 0x06003E5B RID: 15963 RVA: 0x000ED959 File Offset: 0x000EBB59
		public NetHttpsBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x000ED962 File Offset: 0x000EBB62
		public NetHttpsBindingElement() : this(null)
		{
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003E5D RID: 15965 RVA: 0x000ED96B File Offset: 0x000EBB6B
		// (set) Token: 0x06003E5E RID: 15966 RVA: 0x000ED97D File Offset: 0x000EBB7D
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

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003E5F RID: 15967 RVA: 0x000ED990 File Offset: 0x000EBB90
		[ConfigurationProperty("reliableSession")]
		public StandardBindingOptionalReliableSessionElement ReliableSession
		{
			get
			{
				return (StandardBindingOptionalReliableSessionElement)base["reliableSession"];
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x000ED9A2 File Offset: 0x000EBBA2
		[ConfigurationProperty("security")]
		public BasicHttpsSecurityElement Security
		{
			get
			{
				return (BasicHttpsSecurityElement)base["security"];
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003E61 RID: 15969 RVA: 0x000ED9B4 File Offset: 0x000EBBB4
		// (set) Token: 0x06003E62 RID: 15970 RVA: 0x000ED9C6 File Offset: 0x000EBBC6
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

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003E63 RID: 15971 RVA: 0x000ED9D4 File Offset: 0x000EBBD4
		protected override Type BindingElementType
		{
			get
			{
				return typeof(NetHttpsBinding);
			}
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x000ED9E0 File Offset: 0x000EBBE0
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			NetHttpsBinding netHttpsBinding = (NetHttpsBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<NetHttpMessageEncoding>("messageEncoding", netHttpsBinding.MessageEncoding);
			this.WebSocketSettings.InitializeFrom(netHttpsBinding.WebSocketSettings);
			this.ReliableSession.InitializeFrom(netHttpsBinding.ReliableSession);
			this.Security.InitializeFrom(netHttpsBinding.Security);
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x000EDA40 File Offset: 0x000EBC40
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			NetHttpsBinding netHttpsBinding = (NetHttpsBinding)binding;
			netHttpsBinding.MessageEncoding = this.MessageEncoding;
			this.WebSocketSettings.ApplyConfiguration(netHttpsBinding.WebSocketSettings);
			this.ReliableSession.ApplyConfiguration(netHttpsBinding.ReliableSession);
			this.Security.ApplyConfiguration(netHttpsBinding.Security);
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000EDA9C File Offset: 0x000EBC9C
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
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(BasicHttpsSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("webSocketSettings", typeof(NetHttpWebSocketTransportSettingsElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CA4 RID: 11428
		private ConfigurationPropertyCollection properties;
	}
}
