using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200069E RID: 1694
	public class WSDualHttpBindingElement : StandardBindingElement
	{
		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x0600418C RID: 16780 RVA: 0x000F88AC File Offset: 0x000F6AAC
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
							configurationPropertyCollection.Add(new ConfigurationProperty("clientBaseAddress", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("transactionFlow", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("hostNameComparisonMode", typeof(HostNameComparisonMode), HostNameComparisonMode.StrongWildcard, null, new ServiceModelEnumValidator(typeof(HostNameComparisonModeHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(0L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(1L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("messageEncoding", typeof(WSMessageEncoding), WSMessageEncoding.Text, null, new ServiceModelEnumValidator(typeof(WSMessageEncodingHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("proxyAddress", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("readerQuotas", typeof(XmlDictionaryReaderQuotasElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("reliableSession", typeof(StandardBindingReliableSessionElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(WSDualHttpSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("textEncoding", typeof(Encoding), "utf-8", new EncodingConverter(), null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("useDefaultWebProxy", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x000F8B14 File Offset: 0x000F6D14
		public WSDualHttpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000F8B1D File Offset: 0x000F6D1D
		public WSDualHttpBindingElement() : this(null)
		{
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x000F8B26 File Offset: 0x000F6D26
		protected override Type BindingElementType
		{
			get
			{
				return typeof(WSDualHttpBinding);
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x000F8B32 File Offset: 0x000F6D32
		// (set) Token: 0x06004191 RID: 16785 RVA: 0x000F8B44 File Offset: 0x000F6D44
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

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x000F8B57 File Offset: 0x000F6D57
		// (set) Token: 0x06004193 RID: 16787 RVA: 0x000F8B69 File Offset: 0x000F6D69
		[ConfigurationProperty("clientBaseAddress", DefaultValue = null)]
		public Uri ClientBaseAddress
		{
			get
			{
				return (Uri)base["clientBaseAddress"];
			}
			set
			{
				base["clientBaseAddress"] = value;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x000F8B77 File Offset: 0x000F6D77
		// (set) Token: 0x06004195 RID: 16789 RVA: 0x000F8B89 File Offset: 0x000F6D89
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

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x000F8B9C File Offset: 0x000F6D9C
		// (set) Token: 0x06004197 RID: 16791 RVA: 0x000F8BAE File Offset: 0x000F6DAE
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

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06004198 RID: 16792 RVA: 0x000F8BC1 File Offset: 0x000F6DC1
		// (set) Token: 0x06004199 RID: 16793 RVA: 0x000F8BD3 File Offset: 0x000F6DD3
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

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x0600419A RID: 16794 RVA: 0x000F8BE6 File Offset: 0x000F6DE6
		// (set) Token: 0x0600419B RID: 16795 RVA: 0x000F8BF8 File Offset: 0x000F6DF8
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

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x000F8C0B File Offset: 0x000F6E0B
		// (set) Token: 0x0600419D RID: 16797 RVA: 0x000F8C1D File Offset: 0x000F6E1D
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

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x000F8C30 File Offset: 0x000F6E30
		// (set) Token: 0x0600419F RID: 16799 RVA: 0x000F8C42 File Offset: 0x000F6E42
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

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x000F8C50 File Offset: 0x000F6E50
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x000F8C62 File Offset: 0x000F6E62
		[ConfigurationProperty("reliableSession")]
		public StandardBindingReliableSessionElement ReliableSession
		{
			get
			{
				return (StandardBindingReliableSessionElement)base["reliableSession"];
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x000F8C74 File Offset: 0x000F6E74
		[ConfigurationProperty("security")]
		public WSDualHttpSecurityElement Security
		{
			get
			{
				return (WSDualHttpSecurityElement)base["security"];
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x000F8C86 File Offset: 0x000F6E86
		// (set) Token: 0x060041A4 RID: 16804 RVA: 0x000F8C98 File Offset: 0x000F6E98
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

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x000F8CA6 File Offset: 0x000F6EA6
		// (set) Token: 0x060041A6 RID: 16806 RVA: 0x000F8CB8 File Offset: 0x000F6EB8
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

		// Token: 0x060041A7 RID: 16807 RVA: 0x000F8CCC File Offset: 0x000F6ECC
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			WSDualHttpBinding wsdualHttpBinding = (WSDualHttpBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<bool>("bypassProxyOnLocal", wsdualHttpBinding.BypassProxyOnLocal);
			base.SetPropertyValueIfNotDefaultValue<Uri>("clientBaseAddress", wsdualHttpBinding.ClientBaseAddress);
			base.SetPropertyValueIfNotDefaultValue<bool>("transactionFlow", wsdualHttpBinding.TransactionFlow);
			base.SetPropertyValueIfNotDefaultValue<HostNameComparisonMode>("hostNameComparisonMode", wsdualHttpBinding.HostNameComparisonMode);
			base.SetPropertyValueIfNotDefaultValue<long>("maxBufferPoolSize", wsdualHttpBinding.MaxBufferPoolSize);
			base.SetPropertyValueIfNotDefaultValue<long>("maxReceivedMessageSize", wsdualHttpBinding.MaxReceivedMessageSize);
			base.SetPropertyValueIfNotDefaultValue<WSMessageEncoding>("messageEncoding", wsdualHttpBinding.MessageEncoding);
			base.SetPropertyValueIfNotDefaultValue<Uri>("proxyAddress", wsdualHttpBinding.ProxyAddress);
			this.ReliableSession.InitializeFrom(wsdualHttpBinding.ReliableSession);
			base.SetPropertyValueIfNotDefaultValue<Encoding>("textEncoding", wsdualHttpBinding.TextEncoding);
			base.SetPropertyValueIfNotDefaultValue<bool>("useDefaultWebProxy", wsdualHttpBinding.UseDefaultWebProxy);
			this.Security.InitializeFrom(wsdualHttpBinding.Security);
			this.ReaderQuotas.InitializeFrom(wsdualHttpBinding.ReaderQuotas);
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000F8DC4 File Offset: 0x000F6FC4
		protected override void OnApplyConfiguration(Binding binding)
		{
			WSDualHttpBinding wsdualHttpBinding = (WSDualHttpBinding)binding;
			wsdualHttpBinding.BypassProxyOnLocal = this.BypassProxyOnLocal;
			if (this.ClientBaseAddress != null)
			{
				wsdualHttpBinding.ClientBaseAddress = this.ClientBaseAddress;
			}
			wsdualHttpBinding.TransactionFlow = this.TransactionFlow;
			wsdualHttpBinding.HostNameComparisonMode = this.HostNameComparisonMode;
			wsdualHttpBinding.MaxBufferPoolSize = this.MaxBufferPoolSize;
			wsdualHttpBinding.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
			wsdualHttpBinding.MessageEncoding = this.MessageEncoding;
			if (this.ProxyAddress != null)
			{
				wsdualHttpBinding.ProxyAddress = this.ProxyAddress;
			}
			this.ReliableSession.ApplyConfiguration(wsdualHttpBinding.ReliableSession);
			wsdualHttpBinding.TextEncoding = this.TextEncoding;
			wsdualHttpBinding.UseDefaultWebProxy = this.UseDefaultWebProxy;
			this.Security.ApplyConfiguration(wsdualHttpBinding.Security);
			this.ReaderQuotas.ApplyConfiguration(wsdualHttpBinding.ReaderQuotas);
		}

		// Token: 0x04002CEC RID: 11500
		private ConfigurationPropertyCollection properties;
	}
}
