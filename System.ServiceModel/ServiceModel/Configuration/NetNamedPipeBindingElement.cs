using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000658 RID: 1624
	public class NetNamedPipeBindingElement : StandardBindingElement
	{
		// Token: 0x06003E90 RID: 16016 RVA: 0x000EE375 File Offset: 0x000EC575
		public NetNamedPipeBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x000EE37E File Offset: 0x000EC57E
		public NetNamedPipeBindingElement() : this(null)
		{
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000EE387 File Offset: 0x000EC587
		protected override Type BindingElementType
		{
			get
			{
				return typeof(NetNamedPipeBinding);
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x000EE393 File Offset: 0x000EC593
		// (set) Token: 0x06003E94 RID: 16020 RVA: 0x000EE3A5 File Offset: 0x000EC5A5
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

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x000EE3B8 File Offset: 0x000EC5B8
		// (set) Token: 0x06003E96 RID: 16022 RVA: 0x000EE3CA File Offset: 0x000EC5CA
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

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06003E97 RID: 16023 RVA: 0x000EE3DD File Offset: 0x000EC5DD
		// (set) Token: 0x06003E98 RID: 16024 RVA: 0x000EE3EF File Offset: 0x000EC5EF
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

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06003E99 RID: 16025 RVA: 0x000EE3FD File Offset: 0x000EC5FD
		// (set) Token: 0x06003E9A RID: 16026 RVA: 0x000EE40F File Offset: 0x000EC60F
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

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06003E9B RID: 16027 RVA: 0x000EE422 File Offset: 0x000EC622
		// (set) Token: 0x06003E9C RID: 16028 RVA: 0x000EE434 File Offset: 0x000EC634
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

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06003E9D RID: 16029 RVA: 0x000EE447 File Offset: 0x000EC647
		// (set) Token: 0x06003E9E RID: 16030 RVA: 0x000EE459 File Offset: 0x000EC659
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

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x000EE46C File Offset: 0x000EC66C
		// (set) Token: 0x06003EA0 RID: 16032 RVA: 0x000EE47E File Offset: 0x000EC67E
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

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06003EA1 RID: 16033 RVA: 0x000EE491 File Offset: 0x000EC691
		// (set) Token: 0x06003EA2 RID: 16034 RVA: 0x000EE4A3 File Offset: 0x000EC6A3
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

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x000EE4B6 File Offset: 0x000EC6B6
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x000EE4C8 File Offset: 0x000EC6C8
		[ConfigurationProperty("security")]
		public NetNamedPipeSecurityElement Security
		{
			get
			{
				return (NetNamedPipeSecurityElement)base["security"];
			}
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x000EE4DC File Offset: 0x000EC6DC
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			NetNamedPipeBinding netNamedPipeBinding = (NetNamedPipeBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<bool>("transactionFlow", netNamedPipeBinding.TransactionFlow);
			base.SetPropertyValueIfNotDefaultValue<TransferMode>("transferMode", netNamedPipeBinding.TransferMode);
			base.SetPropertyValueIfNotDefaultValue<TransactionProtocol>("transactionProtocol", netNamedPipeBinding.TransactionProtocol);
			base.SetPropertyValueIfNotDefaultValue<HostNameComparisonMode>("hostNameComparisonMode", netNamedPipeBinding.HostNameComparisonMode);
			base.SetPropertyValueIfNotDefaultValue<long>("maxBufferPoolSize", netNamedPipeBinding.MaxBufferPoolSize);
			base.SetPropertyValueIfNotDefaultValue<int>("maxBufferSize", netNamedPipeBinding.MaxBufferSize);
			if (netNamedPipeBinding.IsMaxConnectionsSet)
			{
				ConfigurationProperty prop = this.Properties["maxConnections"];
				base.SetPropertyValue(prop, netNamedPipeBinding.MaxConnections, false);
			}
			base.SetPropertyValueIfNotDefaultValue<long>("maxReceivedMessageSize", netNamedPipeBinding.MaxReceivedMessageSize);
			this.Security.InitializeFrom(netNamedPipeBinding.Security);
			this.ReaderQuotas.InitializeFrom(netNamedPipeBinding.ReaderQuotas);
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x000EE5BC File Offset: 0x000EC7BC
		protected override void OnApplyConfiguration(Binding binding)
		{
			NetNamedPipeBinding netNamedPipeBinding = (NetNamedPipeBinding)binding;
			netNamedPipeBinding.TransactionFlow = this.TransactionFlow;
			netNamedPipeBinding.TransferMode = this.TransferMode;
			netNamedPipeBinding.TransactionProtocol = this.TransactionProtocol;
			netNamedPipeBinding.HostNameComparisonMode = this.HostNameComparisonMode;
			netNamedPipeBinding.MaxBufferPoolSize = this.MaxBufferPoolSize;
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["maxBufferSize"].ValueOrigin != PropertyValueOrigin.Default)
			{
				netNamedPipeBinding.MaxBufferSize = this.MaxBufferSize;
			}
			if (this.MaxConnections != 0)
			{
				netNamedPipeBinding.MaxConnections = this.MaxConnections;
			}
			netNamedPipeBinding.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
			this.Security.ApplyConfiguration(netNamedPipeBinding.Security);
			this.ReaderQuotas.ApplyConfiguration(netNamedPipeBinding.ReaderQuotas);
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000EE678 File Offset: 0x000EC878
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
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(0L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferSize", typeof(int), 65536, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxConnections", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(1L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("readerQuotas", typeof(XmlDictionaryReaderQuotasElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(NetNamedPipeSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CA8 RID: 11432
		private ConfigurationPropertyCollection properties;
	}
}
