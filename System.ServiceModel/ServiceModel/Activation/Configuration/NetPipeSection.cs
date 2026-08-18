using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Activation.Configuration
{
	// Token: 0x020005D1 RID: 1489
	public sealed class NetPipeSection : ConfigurationSection
	{
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x000DF7DC File Offset: 0x000DD9DC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("allowAccounts", typeof(SecurityIdentifierElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxPendingConnections", typeof(int), 100, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxPendingAccepts", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("receiveTimeout", typeof(TimeSpan), TimeSpan.Parse("00:00:30", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000DF8D9 File Offset: 0x000DDAD9
		public NetPipeSection()
		{
			this.propertyInfo = base.ElementInformation.Properties;
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000DF8F2 File Offset: 0x000DDAF2
		[ConfigurationProperty("allowAccounts")]
		public SecurityIdentifierElementCollection AllowAccounts
		{
			get
			{
				return (SecurityIdentifierElementCollection)base["allowAccounts"];
			}
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000DF904 File Offset: 0x000DDB04
		internal static NetPipeSection GetSection()
		{
			NetPipeSection netPipeSection = (NetPipeSection)ConfigurationManager.GetSection(ConfigurationStrings.NetPipeSectionPath);
			if (netPipeSection == null)
			{
				netPipeSection = new NetPipeSection();
			}
			return netPipeSection;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000DF92B File Offset: 0x000DDB2B
		protected override void InitializeDefault()
		{
			this.AllowAccounts.SetDefaultIdentifiers();
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x000DF938 File Offset: 0x000DDB38
		// (set) Token: 0x060039E0 RID: 14816 RVA: 0x000DF94A File Offset: 0x000DDB4A
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

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000DF960 File Offset: 0x000DDB60
		// (set) Token: 0x060039E2 RID: 14818 RVA: 0x000DF98A File Offset: 0x000DDB8A
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

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000DF99D File Offset: 0x000DDB9D
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x000DF9AF File Offset: 0x000DDBAF
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

		// Token: 0x04002A32 RID: 10802
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002A33 RID: 10803
		private PropertyInformationCollection propertyInfo;
	}
}
