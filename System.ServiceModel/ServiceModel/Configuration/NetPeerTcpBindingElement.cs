using System;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000656 RID: 1622
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	public class NetPeerTcpBindingElement : StandardBindingElement
	{
		// Token: 0x06003E7D RID: 15997 RVA: 0x000EDFCC File Offset: 0x000EC1CC
		public NetPeerTcpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x000EDFD5 File Offset: 0x000EC1D5
		public NetPeerTcpBindingElement() : this(null)
		{
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06003E7F RID: 15999 RVA: 0x000EDFDE File Offset: 0x000EC1DE
		protected override Type BindingElementType
		{
			get
			{
				return typeof(NetPeerTcpBinding);
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06003E80 RID: 16000 RVA: 0x000EDFEA File Offset: 0x000EC1EA
		// (set) Token: 0x06003E81 RID: 16001 RVA: 0x000EDFFC File Offset: 0x000EC1FC
		[ConfigurationProperty("listenIPAddress", DefaultValue = null)]
		[TypeConverter(typeof(PeerTransportListenAddressConverter))]
		[PeerTransportListenAddressValidator]
		public IPAddress ListenIPAddress
		{
			get
			{
				return (IPAddress)base["listenIPAddress"];
			}
			set
			{
				base["listenIPAddress"] = value;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06003E82 RID: 16002 RVA: 0x000EE00A File Offset: 0x000EC20A
		// (set) Token: 0x06003E83 RID: 16003 RVA: 0x000EE01C File Offset: 0x000EC21C
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

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06003E84 RID: 16004 RVA: 0x000EE02F File Offset: 0x000EC22F
		// (set) Token: 0x06003E85 RID: 16005 RVA: 0x000EE041 File Offset: 0x000EC241
		[ConfigurationProperty("maxReceivedMessageSize", DefaultValue = 65536L)]
		[LongValidator(MinValue = 16384L)]
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

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x000EE054 File Offset: 0x000EC254
		// (set) Token: 0x06003E87 RID: 16007 RVA: 0x000EE066 File Offset: 0x000EC266
		[ConfigurationProperty("port", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 65535)]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x000EE079 File Offset: 0x000EC279
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003E89 RID: 16009 RVA: 0x000EE08B File Offset: 0x000EC28B
		[ConfigurationProperty("resolver", DefaultValue = null)]
		public PeerResolverElement Resolver
		{
			get
			{
				return (PeerResolverElement)base["resolver"];
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000EE09D File Offset: 0x000EC29D
		[ConfigurationProperty("security")]
		public PeerSecurityElement Security
		{
			get
			{
				return (PeerSecurityElement)base["security"];
			}
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x000EE0B0 File Offset: 0x000EC2B0
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			NetPeerTcpBinding netPeerTcpBinding = (NetPeerTcpBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<IPAddress>("listenIPAddress", netPeerTcpBinding.ListenIPAddress);
			base.SetPropertyValueIfNotDefaultValue<long>("maxBufferPoolSize", netPeerTcpBinding.MaxBufferPoolSize);
			base.SetPropertyValueIfNotDefaultValue<long>("maxReceivedMessageSize", netPeerTcpBinding.MaxReceivedMessageSize);
			base.SetPropertyValueIfNotDefaultValue<int>("port", netPeerTcpBinding.Port);
			this.Security.InitializeFrom(netPeerTcpBinding.Security);
			this.Resolver.InitializeFrom(netPeerTcpBinding.Resolver);
			this.ReaderQuotas.InitializeFrom(netPeerTcpBinding.ReaderQuotas);
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x000EE144 File Offset: 0x000EC344
		protected override void OnApplyConfiguration(Binding binding)
		{
			NetPeerTcpBinding netPeerTcpBinding = (NetPeerTcpBinding)binding;
			netPeerTcpBinding.ListenIPAddress = this.ListenIPAddress;
			netPeerTcpBinding.MaxBufferPoolSize = this.MaxBufferPoolSize;
			netPeerTcpBinding.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
			netPeerTcpBinding.Port = this.Port;
			netPeerTcpBinding.Security = new PeerSecuritySettings();
			this.ReaderQuotas.ApplyConfiguration(netPeerTcpBinding.ReaderQuotas);
			this.Resolver.ApplyConfiguration(netPeerTcpBinding.Resolver);
			this.Security.ApplyConfiguration(netPeerTcpBinding.Security);
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000EE1C8 File Offset: 0x000EC3C8
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
							configurationPropertyCollection.Add(new ConfigurationProperty("listenIPAddress", typeof(IPAddress), null, new PeerTransportListenAddressConverter(), new PeerTransportListenAddressValidator(), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(0L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(16384L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("port", typeof(int), 0, null, new IntegerValidator(0, 65535, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("readerQuotas", typeof(XmlDictionaryReaderQuotasElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("resolver", typeof(PeerResolverElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(PeerSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CA7 RID: 11431
		private ConfigurationPropertyCollection properties;
	}
}
