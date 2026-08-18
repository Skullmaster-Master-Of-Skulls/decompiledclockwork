using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200063B RID: 1595
	public sealed class MessageLoggingElement : ConfigurationElement
	{
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06003D63 RID: 15715 RVA: 0x000EA7A8 File Offset: 0x000E89A8
		// (set) Token: 0x06003D64 RID: 15716 RVA: 0x000EA7BA File Offset: 0x000E89BA
		[ConfigurationProperty("logEntireMessage", DefaultValue = false)]
		public bool LogEntireMessage
		{
			get
			{
				return (bool)base["logEntireMessage"];
			}
			set
			{
				base["logEntireMessage"] = value;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06003D65 RID: 15717 RVA: 0x000EA7CD File Offset: 0x000E89CD
		// (set) Token: 0x06003D66 RID: 15718 RVA: 0x000EA7DF File Offset: 0x000E89DF
		[ConfigurationProperty("logKnownPii", DefaultValue = false)]
		public bool LogKnownPii
		{
			get
			{
				return (bool)base["logKnownPii"];
			}
			set
			{
				base["logKnownPii"] = value;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06003D67 RID: 15719 RVA: 0x000EA7F2 File Offset: 0x000E89F2
		// (set) Token: 0x06003D68 RID: 15720 RVA: 0x000EA804 File Offset: 0x000E8A04
		[ConfigurationProperty("logMalformedMessages", DefaultValue = false)]
		public bool LogMalformedMessages
		{
			get
			{
				return (bool)base["logMalformedMessages"];
			}
			set
			{
				base["logMalformedMessages"] = value;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x000EA817 File Offset: 0x000E8A17
		// (set) Token: 0x06003D6A RID: 15722 RVA: 0x000EA829 File Offset: 0x000E8A29
		[ConfigurationProperty("logMessagesAtServiceLevel", DefaultValue = false)]
		public bool LogMessagesAtServiceLevel
		{
			get
			{
				return (bool)base["logMessagesAtServiceLevel"];
			}
			set
			{
				base["logMessagesAtServiceLevel"] = value;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06003D6B RID: 15723 RVA: 0x000EA83C File Offset: 0x000E8A3C
		// (set) Token: 0x06003D6C RID: 15724 RVA: 0x000EA84E File Offset: 0x000E8A4E
		[ConfigurationProperty("logMessagesAtTransportLevel", DefaultValue = false)]
		public bool LogMessagesAtTransportLevel
		{
			get
			{
				return (bool)base["logMessagesAtTransportLevel"];
			}
			set
			{
				base["logMessagesAtTransportLevel"] = value;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x000EA861 File Offset: 0x000E8A61
		// (set) Token: 0x06003D6E RID: 15726 RVA: 0x000EA873 File Offset: 0x000E8A73
		[ConfigurationProperty("maxMessagesToLog", DefaultValue = 10000)]
		[IntegerValidator(MinValue = -1)]
		public int MaxMessagesToLog
		{
			get
			{
				return (int)base["maxMessagesToLog"];
			}
			set
			{
				base["maxMessagesToLog"] = value;
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x000EA886 File Offset: 0x000E8A86
		// (set) Token: 0x06003D70 RID: 15728 RVA: 0x000EA898 File Offset: 0x000E8A98
		[ConfigurationProperty("maxSizeOfMessageToLog", DefaultValue = 262144)]
		[IntegerValidator(MinValue = -1)]
		public int MaxSizeOfMessageToLog
		{
			get
			{
				return (int)base["maxSizeOfMessageToLog"];
			}
			set
			{
				base["maxSizeOfMessageToLog"] = value;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x000EA8AB File Offset: 0x000E8AAB
		[ConfigurationProperty("filters", DefaultValue = null)]
		public XPathMessageFilterElementCollection Filters
		{
			get
			{
				return (XPathMessageFilterElementCollection)base["filters"];
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000EA8C0 File Offset: 0x000E8AC0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("logEntireMessage", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("logKnownPii", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("logMalformedMessages", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("logMessagesAtServiceLevel", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("logMessagesAtTransportLevel", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxMessagesToLog", typeof(int), 10000, null, new IntegerValidator(-1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxSizeOfMessageToLog", typeof(int), 262144, null, new IntegerValidator(-1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("filters", typeof(XPathMessageFilterElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C90 RID: 11408
		private ConfigurationPropertyCollection properties;
	}
}
