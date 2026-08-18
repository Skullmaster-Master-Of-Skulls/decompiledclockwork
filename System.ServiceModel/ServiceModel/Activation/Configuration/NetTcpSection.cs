using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Activation.Configuration
{
	// Token: 0x020005D2 RID: 1490
	public sealed class NetTcpSection : ConfigurationSection
	{
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000DF9C4 File Offset: 0x000DDBC4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("allowAccounts", typeof(SecurityIdentifierElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("listenBacklog", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxPendingConnections", typeof(int), 100, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxPendingAccepts", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("receiveTimeout", typeof(TimeSpan), TimeSpan.Parse("00:00:30", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("teredoEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000DFB12 File Offset: 0x000DDD12
		public NetTcpSection()
		{
			this.propertyInfo = base.ElementInformation.Properties;
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000DFB2B File Offset: 0x000DDD2B
		[ConfigurationProperty("allowAccounts")]
		public SecurityIdentifierElementCollection AllowAccounts
		{
			get
			{
				return (SecurityIdentifierElementCollection)base["allowAccounts"];
			}
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000DFB40 File Offset: 0x000DDD40
		internal static NetTcpSection GetSection()
		{
			NetTcpSection netTcpSection = (NetTcpSection)ConfigurationManager.GetSection(ConfigurationStrings.NetTcpSectionPath);
			if (netTcpSection == null)
			{
				netTcpSection = new NetTcpSection();
			}
			return netTcpSection;
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000DFB67 File Offset: 0x000DDD67
		protected override void InitializeDefault()
		{
			this.AllowAccounts.SetDefaultIdentifiers();
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060039EA RID: 14826 RVA: 0x000DFB74 File Offset: 0x000DDD74
		// (set) Token: 0x060039EB RID: 14827 RVA: 0x000DFB9C File Offset: 0x000DDD9C
		[ConfigurationProperty("listenBacklog", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int ListenBacklog
		{
			get
			{
				int num = (int)base["listenBacklog"];
				if (num != 0)
				{
					return num;
				}
				return TcpTransportDefaults.GetListenBacklog();
			}
			set
			{
				base["listenBacklog"] = value;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000DFBAF File Offset: 0x000DDDAF
		// (set) Token: 0x060039ED RID: 14829 RVA: 0x000DFBC1 File Offset: 0x000DDDC1
		[ConfigurationProperty("maxPendingConnections", DefaultValue = 100)]
		[IntegerValidator(MinValue = 0)]
		public int MaxPendingConnections
		{
			get
			{
				return (int)base["maxPendingConnections"];
			}
			set
			{
				base["maxPendingConnections"] = value;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000DFBD4 File Offset: 0x000DDDD4
		// (set) Token: 0x060039EF RID: 14831 RVA: 0x000DFBFE File Offset: 0x000DDDFE
		[ConfigurationProperty("maxPendingAccepts", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxPendingAccepts
		{
			get
			{
				int num = (int)base["maxPendingAccepts"];
				if (num != 0)
				{
					return num;
				}
				return 2 * ConnectionOrientedTransportDefaults.GetMaxPendingAccepts();
			}
			set
			{
				base["maxPendingAccepts"] = value;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000DFC11 File Offset: 0x000DDE11
		// (set) Token: 0x060039F1 RID: 14833 RVA: 0x000DFC23 File Offset: 0x000DDE23
		[ConfigurationProperty("receiveTimeout", DefaultValue = "00:00:30")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan ReceiveTimeout
		{
			get
			{
				return (TimeSpan)base["receiveTimeout"];
			}
			set
			{
				base["receiveTimeout"] = value;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x000DFC36 File Offset: 0x000DDE36
		// (set) Token: 0x060039F3 RID: 14835 RVA: 0x000DFC48 File Offset: 0x000DDE48
		[ConfigurationProperty("teredoEnabled", DefaultValue = false)]
		public bool TeredoEnabled
		{
			get
			{
				return (bool)base["teredoEnabled"];
			}
			set
			{
				base["teredoEnabled"] = value;
			}
		}

		// Token: 0x04002A34 RID: 10804
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002A35 RID: 10805
		private PropertyInformationCollection propertyInfo;
	}
}
