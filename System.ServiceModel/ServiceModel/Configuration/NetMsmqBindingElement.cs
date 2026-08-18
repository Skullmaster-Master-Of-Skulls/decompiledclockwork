using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200065F RID: 1631
	public class NetMsmqBindingElement : MsmqBindingElementBase
	{
		// Token: 0x06003EBE RID: 16062 RVA: 0x000EEA87 File Offset: 0x000ECC87
		public NetMsmqBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x000EEA90 File Offset: 0x000ECC90
		public NetMsmqBindingElement() : this(null)
		{
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x000EEA99 File Offset: 0x000ECC99
		protected override Type BindingElementType
		{
			get
			{
				return typeof(NetMsmqBinding);
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06003EC1 RID: 16065 RVA: 0x000EEAA5 File Offset: 0x000ECCA5
		// (set) Token: 0x06003EC2 RID: 16066 RVA: 0x000EEAB7 File Offset: 0x000ECCB7
		[ConfigurationProperty("queueTransferProtocol", DefaultValue = QueueTransferProtocol.Native)]
		[ServiceModelEnumValidator(typeof(QueueTransferProtocolHelper))]
		public QueueTransferProtocol QueueTransferProtocol
		{
			get
			{
				return (QueueTransferProtocol)base["queueTransferProtocol"];
			}
			set
			{
				base["queueTransferProtocol"] = value;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06003EC3 RID: 16067 RVA: 0x000EEACA File Offset: 0x000ECCCA
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x000EEADC File Offset: 0x000ECCDC
		// (set) Token: 0x06003EC5 RID: 16069 RVA: 0x000EEAEE File Offset: 0x000ECCEE
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

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000EEB01 File Offset: 0x000ECD01
		[ConfigurationProperty("security")]
		public NetMsmqSecurityElement Security
		{
			get
			{
				return (NetMsmqSecurityElement)base["security"];
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06003EC7 RID: 16071 RVA: 0x000EEB13 File Offset: 0x000ECD13
		// (set) Token: 0x06003EC8 RID: 16072 RVA: 0x000EEB25 File Offset: 0x000ECD25
		[ConfigurationProperty("useActiveDirectory", DefaultValue = false)]
		public bool UseActiveDirectory
		{
			get
			{
				return (bool)base["useActiveDirectory"];
			}
			set
			{
				base["useActiveDirectory"] = value;
			}
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000EEB38 File Offset: 0x000ECD38
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			NetMsmqBinding netMsmqBinding = (NetMsmqBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<long>("maxBufferPoolSize", netMsmqBinding.MaxBufferPoolSize);
			base.SetPropertyValueIfNotDefaultValue<QueueTransferProtocol>("queueTransferProtocol", netMsmqBinding.QueueTransferProtocol);
			base.SetPropertyValueIfNotDefaultValue<bool>("useActiveDirectory", netMsmqBinding.UseActiveDirectory);
			this.Security.InitializeFrom(netMsmqBinding.Security);
			this.ReaderQuotas.InitializeFrom(netMsmqBinding.ReaderQuotas);
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x000EEBA8 File Offset: 0x000ECDA8
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			NetMsmqBinding netMsmqBinding = (NetMsmqBinding)binding;
			netMsmqBinding.MaxBufferPoolSize = this.MaxBufferPoolSize;
			netMsmqBinding.QueueTransferProtocol = this.QueueTransferProtocol;
			netMsmqBinding.UseActiveDirectory = this.UseActiveDirectory;
			this.Security.ApplyConfiguration(netMsmqBinding.Security);
			this.ReaderQuotas.ApplyConfiguration(netMsmqBinding.ReaderQuotas);
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000EEC0C File Offset: 0x000ECE0C
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
							configurationPropertyCollection.Add(new ConfigurationProperty("queueTransferProtocol", typeof(QueueTransferProtocol), QueueTransferProtocol.Native, null, new ServiceModelEnumValidator(typeof(QueueTransferProtocolHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("readerQuotas", typeof(XmlDictionaryReaderQuotasElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(0L, long.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(NetMsmqSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("useActiveDirectory", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CAA RID: 11434
		private ConfigurationPropertyCollection properties;
	}
}
