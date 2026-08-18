using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000622 RID: 1570
	public abstract class HttpBindingBaseElement : StandardBindingElement
	{
		// Token: 0x06003C48 RID: 15432 RVA: 0x000E65C5 File Offset: 0x000E47C5
		protected HttpBindingBaseElement(string name) : base(name)
		{
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000E65CE File Offset: 0x000E47CE
		// (set) Token: 0x06003C4A RID: 15434 RVA: 0x000E65E0 File Offset: 0x000E47E0
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

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000E65F3 File Offset: 0x000E47F3
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x000E6605 File Offset: 0x000E4805
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

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000E6618 File Offset: 0x000E4818
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x000E662A File Offset: 0x000E482A
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

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000E663D File Offset: 0x000E483D
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000E664F File Offset: 0x000E484F
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

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000E6662 File Offset: 0x000E4862
		// (set) Token: 0x06003C52 RID: 15442 RVA: 0x000E6674 File Offset: 0x000E4874
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

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000E6687 File Offset: 0x000E4887
		// (set) Token: 0x06003C54 RID: 15444 RVA: 0x000E6699 File Offset: 0x000E4899
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

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000E66AC File Offset: 0x000E48AC
		// (set) Token: 0x06003C56 RID: 15446 RVA: 0x000E66BE File Offset: 0x000E48BE
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

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x000E66CC File Offset: 0x000E48CC
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x000E66DE File Offset: 0x000E48DE
		// (set) Token: 0x06003C59 RID: 15449 RVA: 0x000E66F0 File Offset: 0x000E48F0
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

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000E66FE File Offset: 0x000E48FE
		// (set) Token: 0x06003C5B RID: 15451 RVA: 0x000E6710 File Offset: 0x000E4910
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

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x000E6723 File Offset: 0x000E4923
		// (set) Token: 0x06003C5D RID: 15453 RVA: 0x000E6735 File Offset: 0x000E4935
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

		// Token: 0x06003C5E RID: 15454 RVA: 0x000E6748 File Offset: 0x000E4948
		internal virtual void InitializeAllowCookies(HttpBindingBase binding)
		{
			base.SetPropertyValueIfNotDefaultValue<bool>("allowCookies", binding.AllowCookies);
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x000E675C File Offset: 0x000E495C
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			HttpBindingBase httpBindingBase = (HttpBindingBase)binding;
			this.InitializeAllowCookies(httpBindingBase);
			base.SetPropertyValueIfNotDefaultValue<bool>("bypassProxyOnLocal", httpBindingBase.BypassProxyOnLocal);
			base.SetPropertyValueIfNotDefaultValue<HostNameComparisonMode>("hostNameComparisonMode", httpBindingBase.HostNameComparisonMode);
			base.SetPropertyValueIfNotDefaultValue<int>("maxBufferSize", httpBindingBase.MaxBufferSize);
			base.SetPropertyValueIfNotDefaultValue<long>("maxBufferPoolSize", httpBindingBase.MaxBufferPoolSize);
			base.SetPropertyValueIfNotDefaultValue<long>("maxReceivedMessageSize", httpBindingBase.MaxReceivedMessageSize);
			base.SetPropertyValueIfNotDefaultValue<Uri>("proxyAddress", httpBindingBase.ProxyAddress);
			base.SetPropertyValueIfNotDefaultValue<Encoding>("textEncoding", httpBindingBase.TextEncoding);
			base.SetPropertyValueIfNotDefaultValue<TransferMode>("transferMode", httpBindingBase.TransferMode);
			base.SetPropertyValueIfNotDefaultValue<bool>("useDefaultWebProxy", httpBindingBase.UseDefaultWebProxy);
			this.ReaderQuotas.InitializeFrom(httpBindingBase.ReaderQuotas);
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x000E6828 File Offset: 0x000E4A28
		protected override void OnApplyConfiguration(Binding binding)
		{
			HttpBindingBase httpBindingBase = (HttpBindingBase)binding;
			httpBindingBase.BypassProxyOnLocal = this.BypassProxyOnLocal;
			httpBindingBase.HostNameComparisonMode = this.HostNameComparisonMode;
			httpBindingBase.MaxBufferPoolSize = this.MaxBufferPoolSize;
			httpBindingBase.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
			httpBindingBase.TextEncoding = this.TextEncoding;
			httpBindingBase.TransferMode = this.TransferMode;
			httpBindingBase.UseDefaultWebProxy = this.UseDefaultWebProxy;
			httpBindingBase.AllowCookies = this.AllowCookies;
			if (this.ProxyAddress != null)
			{
				httpBindingBase.ProxyAddress = this.ProxyAddress;
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["maxBufferSize"].ValueOrigin != PropertyValueOrigin.Default)
			{
				httpBindingBase.MaxBufferSize = this.MaxBufferSize;
			}
			this.ReaderQuotas.ApplyConfiguration(httpBindingBase.ReaderQuotas);
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x000E68F4 File Offset: 0x000E4AF4
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
							configurationPropertyCollection.Add(new ConfigurationProperty("bypassProxyOnLocal", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("hostNameComparisonMode", typeof(HostNameComparisonMode), HostNameComparisonMode.StrongWildcard, null, new ServiceModelEnumValidator(typeof(HostNameComparisonModeHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(0L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferSize", typeof(int), 65536, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(1L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("proxyAddress", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("readerQuotas", typeof(XmlDictionaryReaderQuotasElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("textEncoding", typeof(Encoding), "utf-8", new EncodingConverter(), null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("transferMode", typeof(TransferMode), TransferMode.Buffered, null, new ServiceModelEnumValidator(typeof(TransferModeHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("useDefaultWebProxy", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C80 RID: 11392
		private ConfigurationPropertyCollection properties;
	}
}
