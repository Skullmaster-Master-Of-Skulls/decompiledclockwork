using System;
using System.Configuration;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000C1 RID: 193
	public sealed class UdpTransportSettingsElement : ConfigurationElement
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00013E59 File Offset: 0x00012059
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00013E6B File Offset: 0x0001206B
		[ConfigurationProperty("duplicateMessageHistoryLength", DefaultValue = 4112)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int DuplicateMessageHistoryLength
		{
			get
			{
				return (int)base["duplicateMessageHistoryLength"];
			}
			set
			{
				base["duplicateMessageHistoryLength"] = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00013E7E File Offset: 0x0001207E
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00013E90 File Offset: 0x00012090
		[ConfigurationProperty("maxPendingMessageCount", DefaultValue = 32)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		public int MaxPendingMessageCount
		{
			get
			{
				return (int)base["maxPendingMessageCount"];
			}
			set
			{
				base["maxPendingMessageCount"] = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00013EA3 File Offset: 0x000120A3
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00013EB5 File Offset: 0x000120B5
		[ConfigurationProperty("maxMulticastRetransmitCount", DefaultValue = 2)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int MaxMulticastRetransmitCount
		{
			get
			{
				return (int)base["maxMulticastRetransmitCount"];
			}
			set
			{
				base["maxMulticastRetransmitCount"] = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00013EC8 File Offset: 0x000120C8
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00013EDA File Offset: 0x000120DA
		[ConfigurationProperty("maxUnicastRetransmitCount", DefaultValue = 1)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int MaxUnicastRetransmitCount
		{
			get
			{
				return (int)base["maxUnicastRetransmitCount"];
			}
			set
			{
				base["maxUnicastRetransmitCount"] = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00013EED File Offset: 0x000120ED
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x00013EFF File Offset: 0x000120FF
		[ConfigurationProperty("multicastInterfaceId")]
		public string MulticastInterfaceId
		{
			get
			{
				return (string)base["multicastInterfaceId"];
			}
			set
			{
				base["multicastInterfaceId"] = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00013F0D File Offset: 0x0001210D
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00013F1F File Offset: 0x0001211F
		[ConfigurationProperty("socketReceiveBufferSize", DefaultValue = 65536)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		public int SocketReceiveBufferSize
		{
			get
			{
				return (int)base["socketReceiveBufferSize"];
			}
			set
			{
				base["socketReceiveBufferSize"] = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00013F32 File Offset: 0x00012132
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00013F44 File Offset: 0x00012144
		[ConfigurationProperty("timeToLive", DefaultValue = 1)]
		[IntegerValidator(MinValue = 0, MaxValue = 255)]
		public int TimeToLive
		{
			get
			{
				return (int)base["timeToLive"];
			}
			set
			{
				base["timeToLive"] = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00013F57 File Offset: 0x00012157
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00013F69 File Offset: 0x00012169
		[ConfigurationProperty("maxReceivedMessageSize", DefaultValue = 65536L)]
		[LongValidator(MinValue = 1L, MaxValue = 65536L)]
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00013F7C File Offset: 0x0001217C
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00013F8E File Offset: 0x0001218E
		[ConfigurationProperty("maxBufferPoolSize", DefaultValue = 524288L)]
		[LongValidator(MinValue = 1L, MaxValue = 9223372036854775807L)]
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00013FA4 File Offset: 0x000121A4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("duplicateMessageHistoryLength", typeof(int), 4112, null, new IntegerValidator(0, int.MaxValue), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxPendingMessageCount", typeof(int), 32, null, new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxMulticastRetransmitCount", typeof(int), 2, null, new IntegerValidator(0, int.MaxValue), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxUnicastRetransmitCount", typeof(int), 1, null, new IntegerValidator(0, int.MaxValue), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("multicastInterfaceId", typeof(string), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("socketReceiveBufferSize", typeof(int), 65536, null, new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("timeToLive", typeof(int), 1, null, new IntegerValidator(0, 255), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(1L, 65536L), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(1L, long.MaxValue), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00014170 File Offset: 0x00012370
		internal void ApplyConfiguration(UdpTransportSettings target)
		{
			target.DuplicateMessageHistoryLength = this.DuplicateMessageHistoryLength;
			target.MaxPendingMessageCount = this.MaxPendingMessageCount;
			target.MaxMulticastRetransmitCount = this.MaxMulticastRetransmitCount;
			target.MaxUnicastRetransmitCount = this.MaxUnicastRetransmitCount;
			target.MulticastInterfaceId = this.MulticastInterfaceId;
			target.SocketReceiveBufferSize = this.SocketReceiveBufferSize;
			target.TimeToLive = this.TimeToLive;
			target.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
			target.MaxBufferPoolSize = this.MaxBufferPoolSize;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000141EC File Offset: 0x000123EC
		internal void InitializeFrom(UdpTransportSettings source)
		{
			this.DuplicateMessageHistoryLength = source.DuplicateMessageHistoryLength;
			this.MaxPendingMessageCount = source.MaxPendingMessageCount;
			this.MaxMulticastRetransmitCount = source.MaxMulticastRetransmitCount;
			this.MaxUnicastRetransmitCount = source.MaxUnicastRetransmitCount;
			this.MulticastInterfaceId = source.MulticastInterfaceId;
			this.SocketReceiveBufferSize = source.SocketReceiveBufferSize;
			this.TimeToLive = source.TimeToLive;
			this.MaxReceivedMessageSize = source.MaxReceivedMessageSize;
			this.MaxBufferPoolSize = source.MaxBufferPoolSize;
		}

		// Token: 0x040001D2 RID: 466
		private ConfigurationPropertyCollection properties;
	}
}
