using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000677 RID: 1655
	public abstract class WSHttpBindingBaseElement : StandardBindingElement
	{
		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x000F0EB4 File Offset: 0x000EF0B4
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
							configurationPropertyCollection.Add(new ConfigurationProperty("bypassProxyOnLocal", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("transactionFlow", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("hostNameComparisonMode", typeof(HostNameComparisonMode), HostNameComparisonMode.StrongWildcard, null, new ServiceModelEnumValidator(typeof(HostNameComparisonModeHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(0L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(1L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("messageEncoding", typeof(WSMessageEncoding), WSMessageEncoding.Text, null, new ServiceModelEnumValidator(typeof(WSMessageEncodingHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("proxyAddress", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("readerQuotas", typeof(XmlDictionaryReaderQuotasElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("reliableSession", typeof(StandardBindingOptionalReliableSessionElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("textEncoding", typeof(Encoding), "utf-8", new EncodingConverter(), null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("useDefaultWebProxy", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x000F10E0 File Offset: 0x000EF2E0
		protected WSHttpBindingBaseElement(string name) : base(name)
		{
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x000F10E9 File Offset: 0x000EF2E9
		protected WSHttpBindingBaseElement() : this(null)
		{
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x000F10F2 File Offset: 0x000EF2F2
		// (set) Token: 0x06003F83 RID: 16259 RVA: 0x000F1104 File Offset: 0x000EF304
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

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x000F1117 File Offset: 0x000EF317
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x000F1129 File Offset: 0x000EF329
		[ConfigurationProperty("transactionFlow", DefaultValue = false)]
		public bool TransactionFlow
		{
			get
			{
				return (bool)base["transactionFlow"];
			}
			set
			{
				base["transactionFlow"] = value;
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x000F113C File Offset: 0x000EF33C
		// (set) Token: 0x06003F87 RID: 16263 RVA: 0x000F114E File Offset: 0x000EF34E
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

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x000F1161 File Offset: 0x000EF361
		// (set) Token: 0x06003F89 RID: 16265 RVA: 0x000F1173 File Offset: 0x000EF373
		[ConfigurationProperty("maxBufferPoolSize", DefaultValue = 524288L)]
		[LongValidator(MinValue = 0L)]
		public long MaxBufferPoolSize
		{
			get
			{
				return (long)base["maxBufferPoolSize"];
			}
			set
			{
				base["maxBufferPoolSize"] = value;
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000F1186 File Offset: 0x000EF386
		// (set) Token: 0x06003F8B RID: 16267 RVA: 0x000F1198 File Offset: 0x000EF398
		[ConfigurationProperty("maxReceivedMessageSize", DefaultValue = 65536L)]
		[LongValidator(MinValue = 1L)]
		public long MaxReceivedMessageSize
		{
			get
			{
				return (long)base["maxReceivedMessageSize"];
			}
			set
			{
				base["maxReceivedMessageSize"] = value;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x000F11AB File Offset: 0x000EF3AB
		// (set) Token: 0x06003F8D RID: 16269 RVA: 0x000F11BD File Offset: 0x000EF3BD
		[ConfigurationProperty("messageEncoding", DefaultValue = WSMessageEncoding.Text)]
		[ServiceModelEnumValidator(typeof(WSMessageEncodingHelper))]
		public WSMessageEncoding MessageEncoding
		{
			get
			{
				return (WSMessageEncoding)base["messageEncoding"];
			}
			set
			{
				base["messageEncoding"] = value;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06003F8E RID: 16270 RVA: 0x000F11D0 File Offset: 0x000EF3D0
		// (set) Token: 0x06003F8F RID: 16271 RVA: 0x000F11E2 File Offset: 0x000EF3E2
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

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x000F11F0 File Offset: 0x000EF3F0
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000F1202 File Offset: 0x000EF402
		[ConfigurationProperty("reliableSession")]
		public StandardBindingOptionalReliableSessionElement ReliableSession
		{
			get
			{
				return (StandardBindingOptionalReliableSessionElement)base["reliableSession"];
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x000F1214 File Offset: 0x000EF414
		// (set) Token: 0x06003F93 RID: 16275 RVA: 0x000F1226 File Offset: 0x000EF426
		[ConfigurationProperty("textEncoding", DefaultValue = "utf-8")]
		[TypeConverter(typeof(EncodingConverter))]
		public Encoding TextEncoding
		{
			get
			{
				return (Encoding)base["textEncoding"];
			}
			set
			{
				base["textEncoding"] = value;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x000F1234 File Offset: 0x000EF434
		// (set) Token: 0x06003F95 RID: 16277 RVA: 0x000F1246 File Offset: 0x000EF446
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

		// Token: 0x06003F96 RID: 16278 RVA: 0x000F125C File Offset: 0x000EF45C
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			WSHttpBindingBase wshttpBindingBase = (WSHttpBindingBase)binding;
			base.SetPropertyValueIfNotDefaultValue<bool>("bypassProxyOnLocal", wshttpBindingBase.BypassProxyOnLocal);
			base.SetPropertyValueIfNotDefaultValue<bool>("transactionFlow", wshttpBindingBase.TransactionFlow);
			base.SetPropertyValueIfNotDefaultValue<HostNameComparisonMode>("hostNameComparisonMode", wshttpBindingBase.HostNameComparisonMode);
			base.SetPropertyValueIfNotDefaultValue<long>("maxBufferPoolSize", wshttpBindingBase.MaxBufferPoolSize);
			base.SetPropertyValueIfNotDefaultValue<long>("maxReceivedMessageSize", wshttpBindingBase.MaxReceivedMessageSize);
			base.SetPropertyValueIfNotDefaultValue<WSMessageEncoding>("messageEncoding", wshttpBindingBase.MessageEncoding);
			base.SetPropertyValueIfNotDefaultValue<Uri>("proxyAddress", wshttpBindingBase.ProxyAddress);
			base.SetPropertyValueIfNotDefaultValue<Encoding>("textEncoding", wshttpBindingBase.TextEncoding);
			base.SetPropertyValueIfNotDefaultValue<bool>("useDefaultWebProxy", wshttpBindingBase.UseDefaultWebProxy);
			this.ReaderQuotas.InitializeFrom(wshttpBindingBase.ReaderQuotas);
			this.ReliableSession.InitializeFrom(wshttpBindingBase.ReliableSession);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000F1334 File Offset: 0x000EF534
		protected override void OnApplyConfiguration(Binding binding)
		{
			WSHttpBindingBase wshttpBindingBase = (WSHttpBindingBase)binding;
			wshttpBindingBase.BypassProxyOnLocal = this.BypassProxyOnLocal;
			wshttpBindingBase.TransactionFlow = this.TransactionFlow;
			wshttpBindingBase.HostNameComparisonMode = this.HostNameComparisonMode;
			wshttpBindingBase.MaxBufferPoolSize = this.MaxBufferPoolSize;
			wshttpBindingBase.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
			wshttpBindingBase.MessageEncoding = this.MessageEncoding;
			if (this.ProxyAddress != null)
			{
				wshttpBindingBase.ProxyAddress = this.ProxyAddress;
			}
			wshttpBindingBase.TextEncoding = this.TextEncoding;
			wshttpBindingBase.UseDefaultWebProxy = this.UseDefaultWebProxy;
			this.ReaderQuotas.ApplyConfiguration(wshttpBindingBase.ReaderQuotas);
			this.ReliableSession.ApplyConfiguration(wshttpBindingBase.ReliableSession);
		}

		// Token: 0x04002CB9 RID: 11449
		private ConfigurationPropertyCollection properties;
	}
}
