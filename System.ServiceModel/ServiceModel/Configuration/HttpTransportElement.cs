using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200062E RID: 1582
	public class HttpTransportElement : TransportElement
	{
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x000E6F64 File Offset: 0x000E5164
		// (set) Token: 0x06003C8D RID: 15501 RVA: 0x000E6F76 File Offset: 0x000E5176
		[ConfigurationProperty("allowCookies", DefaultValue = false)]
		public bool AllowCookies
		{
			get
			{
				return (bool)base["allowCookies"];
			}
			set
			{
				base["allowCookies"] = value;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003C8E RID: 15502 RVA: 0x000E6F89 File Offset: 0x000E5189
		// (set) Token: 0x06003C8F RID: 15503 RVA: 0x000E6F9B File Offset: 0x000E519B
		[ConfigurationProperty("requestInitializationTimeout", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan RequestInitializationTimeout
		{
			get
			{
				return (TimeSpan)base["requestInitializationTimeout"];
			}
			set
			{
				base["requestInitializationTimeout"] = value;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003C90 RID: 15504 RVA: 0x000E6FAE File Offset: 0x000E51AE
		// (set) Token: 0x06003C91 RID: 15505 RVA: 0x000E6FC0 File Offset: 0x000E51C0
		[ConfigurationProperty("authenticationScheme", DefaultValue = AuthenticationSchemes.Anonymous)]
		[StandardRuntimeFlagEnumValidator(typeof(AuthenticationSchemes))]
		public AuthenticationSchemes AuthenticationScheme
		{
			get
			{
				return (AuthenticationSchemes)base["authenticationScheme"];
			}
			set
			{
				base["authenticationScheme"] = value;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x000E6FD3 File Offset: 0x000E51D3
		public override Type BindingElementType
		{
			get
			{
				return typeof(HttpTransportBindingElement);
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003C93 RID: 15507 RVA: 0x000E6FDF File Offset: 0x000E51DF
		// (set) Token: 0x06003C94 RID: 15508 RVA: 0x000E6FF1 File Offset: 0x000E51F1
		[ConfigurationProperty("bypassProxyOnLocal", DefaultValue = false)]
		public bool BypassProxyOnLocal
		{
			get
			{
				return (bool)base["bypassProxyOnLocal"];
			}
			set
			{
				base["bypassProxyOnLocal"] = value;
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003C95 RID: 15509 RVA: 0x000E7004 File Offset: 0x000E5204
		// (set) Token: 0x06003C96 RID: 15510 RVA: 0x000E7016 File Offset: 0x000E5216
		[ConfigurationProperty("decompressionEnabled", DefaultValue = true)]
		public bool DecompressionEnabled
		{
			get
			{
				return (bool)base["decompressionEnabled"];
			}
			set
			{
				base["decompressionEnabled"] = value;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003C97 RID: 15511 RVA: 0x000E7029 File Offset: 0x000E5229
		// (set) Token: 0x06003C98 RID: 15512 RVA: 0x000E703B File Offset: 0x000E523B
		[ConfigurationProperty("hostNameComparisonMode", DefaultValue = HostNameComparisonMode.StrongWildcard)]
		[ServiceModelEnumValidator(typeof(HostNameComparisonModeHelper))]
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return (HostNameComparisonMode)base["hostNameComparisonMode"];
			}
			set
			{
				base["hostNameComparisonMode"] = value;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003C99 RID: 15513 RVA: 0x000E704E File Offset: 0x000E524E
		// (set) Token: 0x06003C9A RID: 15514 RVA: 0x000E7060 File Offset: 0x000E5260
		[ConfigurationProperty("keepAliveEnabled", DefaultValue = true)]
		public bool KeepAliveEnabled
		{
			get
			{
				return (bool)base["keepAliveEnabled"];
			}
			set
			{
				base["keepAliveEnabled"] = value;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x000E7073 File Offset: 0x000E5273
		// (set) Token: 0x06003C9C RID: 15516 RVA: 0x000E7085 File Offset: 0x000E5285
		[ConfigurationProperty("maxBufferSize", DefaultValue = 65536)]
		[IntegerValidator(MinValue = 1)]
		public int MaxBufferSize
		{
			get
			{
				return (int)base["maxBufferSize"];
			}
			set
			{
				base["maxBufferSize"] = value;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003C9D RID: 15517 RVA: 0x000E7098 File Offset: 0x000E5298
		// (set) Token: 0x06003C9E RID: 15518 RVA: 0x000E70AA File Offset: 0x000E52AA
		[ConfigurationProperty("maxPendingAccepts", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 100000)]
		public int MaxPendingAccepts
		{
			get
			{
				return (int)base["maxPendingAccepts"];
			}
			set
			{
				base["maxPendingAccepts"] = value;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x000E70BD File Offset: 0x000E52BD
		// (set) Token: 0x06003CA0 RID: 15520 RVA: 0x000E70CF File Offset: 0x000E52CF
		[ConfigurationProperty("messageHandlerFactory", DefaultValue = null)]
		[HttpMessageHandlerFactoryValidator]
		public HttpMessageHandlerFactoryElement MessageHandlerFactory
		{
			get
			{
				return (HttpMessageHandlerFactoryElement)base["messageHandlerFactory"];
			}
			set
			{
				base["messageHandlerFactory"] = value;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x000E70DD File Offset: 0x000E52DD
		// (set) Token: 0x06003CA2 RID: 15522 RVA: 0x000E70EF File Offset: 0x000E52EF
		[ConfigurationProperty("proxyAddress", DefaultValue = null)]
		public Uri ProxyAddress
		{
			get
			{
				return (Uri)base["proxyAddress"];
			}
			set
			{
				base["proxyAddress"] = value;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x000E70FD File Offset: 0x000E52FD
		// (set) Token: 0x06003CA4 RID: 15524 RVA: 0x000E710F File Offset: 0x000E530F
		[ConfigurationProperty("proxyAuthenticationScheme", DefaultValue = AuthenticationSchemes.Anonymous)]
		[StandardRuntimeEnumValidator(typeof(AuthenticationSchemes))]
		public AuthenticationSchemes ProxyAuthenticationScheme
		{
			get
			{
				return (AuthenticationSchemes)base["proxyAuthenticationScheme"];
			}
			set
			{
				base["proxyAuthenticationScheme"] = value;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x000E7122 File Offset: 0x000E5322
		// (set) Token: 0x06003CA6 RID: 15526 RVA: 0x000E7134 File Offset: 0x000E5334
		[ConfigurationProperty("realm", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string Realm
		{
			get
			{
				return (string)base["realm"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["realm"] = value;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x000E7151 File Offset: 0x000E5351
		// (set) Token: 0x06003CA8 RID: 15528 RVA: 0x000E7163 File Offset: 0x000E5363
		[ConfigurationProperty("transferMode", DefaultValue = TransferMode.Buffered)]
		[ServiceModelEnumValidator(typeof(TransferModeHelper))]
		public TransferMode TransferMode
		{
			get
			{
				return (TransferMode)base["transferMode"];
			}
			set
			{
				base["transferMode"] = value;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x000E7176 File Offset: 0x000E5376
		// (set) Token: 0x06003CAA RID: 15530 RVA: 0x000E7188 File Offset: 0x000E5388
		[ConfigurationProperty("unsafeConnectionNtlmAuthentication", DefaultValue = false)]
		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				return (bool)base["unsafeConnectionNtlmAuthentication"];
			}
			set
			{
				base["unsafeConnectionNtlmAuthentication"] = value;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x000E719B File Offset: 0x000E539B
		// (set) Token: 0x06003CAC RID: 15532 RVA: 0x000E71AD File Offset: 0x000E53AD
		[ConfigurationProperty("useDefaultWebProxy", DefaultValue = true)]
		public bool UseDefaultWebProxy
		{
			get
			{
				return (bool)base["useDefaultWebProxy"];
			}
			set
			{
				base["useDefaultWebProxy"] = value;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000E71C0 File Offset: 0x000E53C0
		// (set) Token: 0x06003CAE RID: 15534 RVA: 0x000E71D2 File Offset: 0x000E53D2
		[ConfigurationProperty("extendedProtectionPolicy")]
		public ExtendedProtectionPolicyElement ExtendedProtectionPolicy
		{
			get
			{
				return (ExtendedProtectionPolicyElement)base["extendedProtectionPolicy"];
			}
			private set
			{
				base["extendedProtectionPolicy"] = value;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003CAF RID: 15535 RVA: 0x000E71E0 File Offset: 0x000E53E0
		// (set) Token: 0x06003CB0 RID: 15536 RVA: 0x000E71F2 File Offset: 0x000E53F2
		[ConfigurationProperty("webSocketSettings")]
		public WebSocketTransportSettingsElement WebSocketSettings
		{
			get
			{
				return (WebSocketTransportSettingsElement)base["webSocketSettings"];
			}
			set
			{
				base["webSocketSettings"] = value;
			}
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000E7200 File Offset: 0x000E5400
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			HttpTransportBindingElement httpTransportBindingElement = (HttpTransportBindingElement)bindingElement;
			httpTransportBindingElement.AllowCookies = this.AllowCookies;
			httpTransportBindingElement.AuthenticationScheme = this.AuthenticationScheme;
			httpTransportBindingElement.BypassProxyOnLocal = this.BypassProxyOnLocal;
			httpTransportBindingElement.DecompressionEnabled = this.DecompressionEnabled;
			httpTransportBindingElement.KeepAliveEnabled = this.KeepAliveEnabled;
			httpTransportBindingElement.HostNameComparisonMode = this.HostNameComparisonMode;
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["maxBufferSize"].ValueOrigin != PropertyValueOrigin.Default)
			{
				httpTransportBindingElement.MaxBufferSize = this.MaxBufferSize;
			}
			httpTransportBindingElement.MaxPendingAccepts = this.MaxPendingAccepts;
			httpTransportBindingElement.ProxyAddress = this.ProxyAddress;
			httpTransportBindingElement.ProxyAuthenticationScheme = this.ProxyAuthenticationScheme;
			httpTransportBindingElement.Realm = this.Realm;
			httpTransportBindingElement.RequestInitializationTimeout = this.RequestInitializationTimeout;
			httpTransportBindingElement.TransferMode = this.TransferMode;
			httpTransportBindingElement.UnsafeConnectionNtlmAuthentication = this.UnsafeConnectionNtlmAuthentication;
			httpTransportBindingElement.UseDefaultWebProxy = this.UseDefaultWebProxy;
			httpTransportBindingElement.ExtendedProtectionPolicy = ChannelBindingUtility.BuildPolicy(this.ExtendedProtectionPolicy);
			this.WebSocketSettings.ApplyConfiguration(httpTransportBindingElement.WebSocketSettings);
			if (this.MessageHandlerFactory != null)
			{
				httpTransportBindingElement.MessageHandlerFactory = HttpMessageHandlerFactory.CreateFromConfigurationElement(this.MessageHandlerFactory);
			}
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x000E7328 File Offset: 0x000E5528
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			HttpTransportElement httpTransportElement = (HttpTransportElement)from;
			this.AllowCookies = httpTransportElement.AllowCookies;
			this.RequestInitializationTimeout = httpTransportElement.RequestInitializationTimeout;
			this.AuthenticationScheme = httpTransportElement.AuthenticationScheme;
			this.BypassProxyOnLocal = httpTransportElement.BypassProxyOnLocal;
			this.DecompressionEnabled = httpTransportElement.DecompressionEnabled;
			this.KeepAliveEnabled = httpTransportElement.KeepAliveEnabled;
			this.HostNameComparisonMode = httpTransportElement.HostNameComparisonMode;
			this.MaxBufferSize = httpTransportElement.MaxBufferSize;
			this.MaxPendingAccepts = httpTransportElement.MaxPendingAccepts;
			this.ProxyAddress = httpTransportElement.ProxyAddress;
			this.ProxyAuthenticationScheme = httpTransportElement.ProxyAuthenticationScheme;
			this.Realm = httpTransportElement.Realm;
			this.TransferMode = httpTransportElement.TransferMode;
			this.UnsafeConnectionNtlmAuthentication = httpTransportElement.UnsafeConnectionNtlmAuthentication;
			this.UseDefaultWebProxy = httpTransportElement.UseDefaultWebProxy;
			this.WebSocketSettings = httpTransportElement.WebSocketSettings;
			this.MessageHandlerFactory = httpTransportElement.MessageHandlerFactory;
			ChannelBindingUtility.CopyFrom(httpTransportElement.ExtendedProtectionPolicy, this.ExtendedProtectionPolicy);
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x000E7420 File Offset: 0x000E5620
		protected override TransportBindingElement CreateDefaultBindingElement()
		{
			return new HttpTransportBindingElement();
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000E7428 File Offset: 0x000E5628
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			HttpTransportBindingElement httpTransportBindingElement = (HttpTransportBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<bool>("allowCookies", httpTransportBindingElement.AllowCookies);
			base.SetPropertyValueIfNotDefaultValue<AuthenticationSchemes>("authenticationScheme", httpTransportBindingElement.AuthenticationScheme);
			base.SetPropertyValueIfNotDefaultValue<bool>("decompressionEnabled", httpTransportBindingElement.DecompressionEnabled);
			base.SetPropertyValueIfNotDefaultValue<bool>("bypassProxyOnLocal", httpTransportBindingElement.BypassProxyOnLocal);
			base.SetPropertyValueIfNotDefaultValue<bool>("keepAliveEnabled", httpTransportBindingElement.KeepAliveEnabled);
			base.SetPropertyValueIfNotDefaultValue<HostNameComparisonMode>("hostNameComparisonMode", httpTransportBindingElement.HostNameComparisonMode);
			base.SetPropertyValueIfNotDefaultValue<int>("maxBufferSize", httpTransportBindingElement.MaxBufferSize);
			base.SetPropertyValueIfNotDefaultValue<int>("maxPendingAccepts", httpTransportBindingElement.MaxPendingAccepts);
			base.SetPropertyValueIfNotDefaultValue<Uri>("proxyAddress", httpTransportBindingElement.ProxyAddress);
			base.SetPropertyValueIfNotDefaultValue<AuthenticationSchemes>("proxyAuthenticationScheme", httpTransportBindingElement.ProxyAuthenticationScheme);
			base.SetPropertyValueIfNotDefaultValue<string>("realm", httpTransportBindingElement.Realm);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("requestInitializationTimeout", httpTransportBindingElement.RequestInitializationTimeout);
			base.SetPropertyValueIfNotDefaultValue<TransferMode>("transferMode", httpTransportBindingElement.TransferMode);
			base.SetPropertyValueIfNotDefaultValue<bool>("unsafeConnectionNtlmAuthentication", httpTransportBindingElement.UnsafeConnectionNtlmAuthentication);
			base.SetPropertyValueIfNotDefaultValue<bool>("useDefaultWebProxy", httpTransportBindingElement.UseDefaultWebProxy);
			this.WebSocketSettings.InitializeFrom(httpTransportBindingElement.WebSocketSettings);
			if (httpTransportBindingElement.MessageHandlerFactory != null)
			{
				this.MessageHandlerFactory = httpTransportBindingElement.MessageHandlerFactory.GenerateConfigurationElement();
			}
			ChannelBindingUtility.InitializeFrom(httpTransportBindingElement.ExtendedProtectionPolicy, this.ExtendedProtectionPolicy);
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000E7580 File Offset: 0x000E5780
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
							configurationPropertyCollection.Add(new ConfigurationProperty("allowCookies", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("requestInitializationTimeout", typeof(TimeSpan), TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("authenticationScheme", typeof(AuthenticationSchemes), AuthenticationSchemes.Anonymous, null, new StandardRuntimeFlagEnumValidator<AuthenticationSchemes>(), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("bypassProxyOnLocal", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("decompressionEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("hostNameComparisonMode", typeof(HostNameComparisonMode), HostNameComparisonMode.StrongWildcard, null, new ServiceModelEnumValidator(typeof(HostNameComparisonModeHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("keepAliveEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferSize", typeof(int), 65536, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxPendingAccepts", typeof(int), 0, null, new IntegerValidator(0, 100000, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("messageHandlerFactory", typeof(HttpMessageHandlerFactoryElement), null, null, new HttpMessageHandlerFactoryValidator(), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("proxyAddress", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("proxyAuthenticationScheme", typeof(AuthenticationSchemes), AuthenticationSchemes.Anonymous, null, new StandardRuntimeEnumValidator(typeof(AuthenticationSchemes)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("realm", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("transferMode", typeof(TransferMode), TransferMode.Buffered, null, new ServiceModelEnumValidator(typeof(TransferModeHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("unsafeConnectionNtlmAuthentication", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("useDefaultWebProxy", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("extendedProtectionPolicy", typeof(ExtendedProtectionPolicyElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("webSocketSettings", typeof(WebSocketTransportSettingsElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C84 RID: 11396
		private ConfigurationPropertyCollection properties;
	}
}
