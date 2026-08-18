using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000662 RID: 1634
	public class NetTcpBindingElement : StandardBindingElement
	{
		// Token: 0x06003ED6 RID: 16086 RVA: 0x000EEEDD File Offset: 0x000ED0DD
		public NetTcpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000EEEE6 File Offset: 0x000ED0E6
		public NetTcpBindingElement() : this(null)
		{
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06003ED8 RID: 16088 RVA: 0x000EEEEF File Offset: 0x000ED0EF
		protected override Type BindingElementType
		{
			get
			{
				return typeof(NetTcpBinding);
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x000EEEFB File Offset: 0x000ED0FB
		// (set) Token: 0x06003EDA RID: 16090 RVA: 0x000EEF0D File Offset: 0x000ED10D
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

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000EEF20 File Offset: 0x000ED120
		// (set) Token: 0x06003EDC RID: 16092 RVA: 0x000EEF32 File Offset: 0x000ED132
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

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06003EDD RID: 16093 RVA: 0x000EEF45 File Offset: 0x000ED145
		// (set) Token: 0x06003EDE RID: 16094 RVA: 0x000EEF57 File Offset: 0x000ED157
		[ConfigurationProperty("transactionProtocol", DefaultValue = "OleTransactions")]
		[TypeConverter(typeof(TransactionProtocolConverter))]
		public TransactionProtocol TransactionProtocol
		{
			get
			{
				return (TransactionProtocol)base["transactionProtocol"];
			}
			set
			{
				base["transactionProtocol"] = value;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000EEF65 File Offset: 0x000ED165
		// (set) Token: 0x06003EE0 RID: 16096 RVA: 0x000EEF77 File Offset: 0x000ED177
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

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x000EEF8A File Offset: 0x000ED18A
		// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x000EEF9C File Offset: 0x000ED19C
		[ConfigurationProperty("listenBacklog", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int ListenBacklog
		{
			get
			{
				return (int)base["listenBacklog"];
			}
			set
			{
				base["listenBacklog"] = value;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x000EEFAF File Offset: 0x000ED1AF
		// (set) Token: 0x06003EE4 RID: 16100 RVA: 0x000EEFC1 File Offset: 0x000ED1C1
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

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x000EEFD4 File Offset: 0x000ED1D4
		// (set) Token: 0x06003EE6 RID: 16102 RVA: 0x000EEFE6 File Offset: 0x000ED1E6
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

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x000EEFF9 File Offset: 0x000ED1F9
		// (set) Token: 0x06003EE8 RID: 16104 RVA: 0x000EF00B File Offset: 0x000ED20B
		[ConfigurationProperty("maxConnections", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxConnections
		{
			get
			{
				return (int)base["maxConnections"];
			}
			set
			{
				base["maxConnections"] = value;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x000EF01E File Offset: 0x000ED21E
		// (set) Token: 0x06003EEA RID: 16106 RVA: 0x000EF030 File Offset: 0x000ED230
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

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06003EEB RID: 16107 RVA: 0x000EF043 File Offset: 0x000ED243
		// (set) Token: 0x06003EEC RID: 16108 RVA: 0x000EF055 File Offset: 0x000ED255
		[ConfigurationProperty("portSharingEnabled", DefaultValue = false)]
		public bool PortSharingEnabled
		{
			get
			{
				return (bool)base["portSharingEnabled"];
			}
			set
			{
				base["portSharingEnabled"] = value;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x000EF068 File Offset: 0x000ED268
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x000EF07A File Offset: 0x000ED27A
		[ConfigurationProperty("reliableSession")]
		public StandardBindingOptionalReliableSessionElement ReliableSession
		{
			get
			{
				return (StandardBindingOptionalReliableSessionElement)base["reliableSession"];
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000EF08C File Offset: 0x000ED28C
		[ConfigurationProperty("security")]
		public NetTcpSecurityElement Security
		{
			get
			{
				return (NetTcpSecurityElement)base["security"];
			}
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x000EF0A0 File Offset: 0x000ED2A0
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			NetTcpBinding netTcpBinding = (NetTcpBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<bool>("transactionFlow", netTcpBinding.TransactionFlow);
			base.SetPropertyValueIfNotDefaultValue<TransferMode>("transferMode", netTcpBinding.TransferMode);
			base.SetPropertyValueIfNotDefaultValue<TransactionProtocol>("transactionProtocol", netTcpBinding.TransactionProtocol);
			base.SetPropertyValueIfNotDefaultValue<HostNameComparisonMode>("hostNameComparisonMode", netTcpBinding.HostNameComparisonMode);
			base.SetPropertyValueIfNotDefaultValue<long>("maxBufferPoolSize", netTcpBinding.MaxBufferPoolSize);
			base.SetPropertyValueIfNotDefaultValue<int>("maxBufferSize", netTcpBinding.MaxBufferSize);
			if (netTcpBinding.IsMaxConnectionsSet)
			{
				ConfigurationProperty prop = this.Properties["maxConnections"];
				base.SetPropertyValue(prop, netTcpBinding.MaxConnections, false);
			}
			base.SetPropertyValueIfNotDefaultValue<long>("maxReceivedMessageSize", netTcpBinding.MaxReceivedMessageSize);
			if (netTcpBinding.IsListenBacklogSet)
			{
				ConfigurationProperty prop2 = this.Properties["listenBacklog"];
				base.SetPropertyValue(prop2, netTcpBinding.ListenBacklog, false);
			}
			this.ReliableSession.InitializeFrom(netTcpBinding.ReliableSession);
			this.Security.InitializeFrom(netTcpBinding.Security);
			this.ReaderQuotas.InitializeFrom(netTcpBinding.ReaderQuotas);
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x000EF1C0 File Offset: 0x000ED3C0
		protected override void OnApplyConfiguration(Binding binding)
		{
			NetTcpBinding netTcpBinding = (NetTcpBinding)binding;
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			netTcpBinding.TransactionFlow = this.TransactionFlow;
			netTcpBinding.TransferMode = this.TransferMode;
			netTcpBinding.TransactionProtocol = this.TransactionProtocol;
			netTcpBinding.HostNameComparisonMode = this.HostNameComparisonMode;
			if (this.ListenBacklog != 0)
			{
				netTcpBinding.ListenBacklog = this.ListenBacklog;
			}
			netTcpBinding.MaxBufferPoolSize = this.MaxBufferPoolSize;
			if (propertyInformationCollection["maxBufferSize"].ValueOrigin != PropertyValueOrigin.Default)
			{
				netTcpBinding.MaxBufferSize = this.MaxBufferSize;
			}
			if (this.MaxConnections != 0)
			{
				netTcpBinding.MaxConnections = this.MaxConnections;
			}
			netTcpBinding.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
			netTcpBinding.PortSharingEnabled = this.PortSharingEnabled;
			this.ReliableSession.ApplyConfiguration(netTcpBinding.ReliableSession);
			this.Security.ApplyConfiguration(netTcpBinding.Security);
			this.ReaderQuotas.ApplyConfiguration(netTcpBinding.ReaderQuotas);
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06003EF2 RID: 16114 RVA: 0x000EF2B0 File Offset: 0x000ED4B0
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
							configurationPropertyCollection.Add(new ConfigurationProperty("transactionFlow", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("transferMode", typeof(TransferMode), TransferMode.Buffered, null, new ServiceModelEnumValidator(typeof(TransferModeHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("transactionProtocol", typeof(TransactionProtocol), "OleTransactions", new TransactionProtocolConverter(), null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("hostNameComparisonMode", typeof(HostNameComparisonMode), HostNameComparisonMode.StrongWildcard, null, new ServiceModelEnumValidator(typeof(HostNameComparisonModeHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("listenBacklog", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(0L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferSize", typeof(int), 65536, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxConnections", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(1L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("portSharingEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("readerQuotas", typeof(XmlDictionaryReaderQuotasElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("reliableSession", typeof(StandardBindingOptionalReliableSessionElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(NetTcpSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CAC RID: 11436
		private ConfigurationPropertyCollection properties;
	}
}
