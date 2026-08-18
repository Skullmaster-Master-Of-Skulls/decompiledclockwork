using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200067F RID: 1663
	public sealed class ReliableSessionElement : BindingElementExtensionElement
	{
		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06003FDC RID: 16348 RVA: 0x000F1ED8 File Offset: 0x000F00D8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("acknowledgementInterval", typeof(TimeSpan), TimeSpan.Parse("00:00:00.2", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00.0000001", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("flowControlEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("inactivityTimeout", typeof(TimeSpan), TimeSpan.Parse("00:10:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00.0000001", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxPendingChannels", typeof(int), 4, null, new IntegerValidator(1, 16384, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxRetryCount", typeof(int), 8, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxTransferWindowSize", typeof(int), 8, null, new IntegerValidator(1, 4096, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("ordered", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("reliableMessagingVersion", typeof(ReliableMessagingVersion), "WSReliableMessagingFebruary2005", new ReliableMessagingVersionConverter(), null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x000F20AF File Offset: 0x000F02AF
		// (set) Token: 0x06003FDF RID: 16351 RVA: 0x000F20C1 File Offset: 0x000F02C1
		[ConfigurationProperty("acknowledgementInterval", DefaultValue = "00:00:00.2")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00.0000001")]
		public TimeSpan AcknowledgementInterval
		{
			get
			{
				return (TimeSpan)base["acknowledgementInterval"];
			}
			set
			{
				base["acknowledgementInterval"] = value;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x000F20D4 File Offset: 0x000F02D4
		public override Type BindingElementType
		{
			get
			{
				return typeof(ReliableSessionBindingElement);
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x000F20E0 File Offset: 0x000F02E0
		// (set) Token: 0x06003FE2 RID: 16354 RVA: 0x000F20F2 File Offset: 0x000F02F2
		[ConfigurationProperty("flowControlEnabled", DefaultValue = true)]
		public bool FlowControlEnabled
		{
			get
			{
				return (bool)base["flowControlEnabled"];
			}
			set
			{
				base["flowControlEnabled"] = value;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x000F2105 File Offset: 0x000F0305
		// (set) Token: 0x06003FE4 RID: 16356 RVA: 0x000F2117 File Offset: 0x000F0317
		[ConfigurationProperty("inactivityTimeout", DefaultValue = "00:10:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00.0000001")]
		public TimeSpan InactivityTimeout
		{
			get
			{
				return (TimeSpan)base["inactivityTimeout"];
			}
			set
			{
				base["inactivityTimeout"] = value;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x000F212A File Offset: 0x000F032A
		// (set) Token: 0x06003FE6 RID: 16358 RVA: 0x000F213C File Offset: 0x000F033C
		[ConfigurationProperty("maxPendingChannels", DefaultValue = 4)]
		[IntegerValidator(MinValue = 1, MaxValue = 16384)]
		public int MaxPendingChannels
		{
			get
			{
				return (int)base["maxPendingChannels"];
			}
			set
			{
				base["maxPendingChannels"] = value;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x000F214F File Offset: 0x000F034F
		// (set) Token: 0x06003FE8 RID: 16360 RVA: 0x000F2161 File Offset: 0x000F0361
		[ConfigurationProperty("maxRetryCount", DefaultValue = 8)]
		[IntegerValidator(MinValue = 1)]
		public int MaxRetryCount
		{
			get
			{
				return (int)base["maxRetryCount"];
			}
			set
			{
				base["maxRetryCount"] = value;
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06003FE9 RID: 16361 RVA: 0x000F2174 File Offset: 0x000F0374
		// (set) Token: 0x06003FEA RID: 16362 RVA: 0x000F2186 File Offset: 0x000F0386
		[ConfigurationProperty("maxTransferWindowSize", DefaultValue = 8)]
		[IntegerValidator(MinValue = 1, MaxValue = 4096)]
		public int MaxTransferWindowSize
		{
			get
			{
				return (int)base["maxTransferWindowSize"];
			}
			set
			{
				base["maxTransferWindowSize"] = value;
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06003FEB RID: 16363 RVA: 0x000F2199 File Offset: 0x000F0399
		// (set) Token: 0x06003FEC RID: 16364 RVA: 0x000F21AB File Offset: 0x000F03AB
		[ConfigurationProperty("ordered", DefaultValue = true)]
		public bool Ordered
		{
			get
			{
				return (bool)base["ordered"];
			}
			set
			{
				base["ordered"] = value;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06003FED RID: 16365 RVA: 0x000F21BE File Offset: 0x000F03BE
		// (set) Token: 0x06003FEE RID: 16366 RVA: 0x000F21D0 File Offset: 0x000F03D0
		[ConfigurationProperty("reliableMessagingVersion", DefaultValue = "WSReliableMessagingFebruary2005")]
		[TypeConverter(typeof(ReliableMessagingVersionConverter))]
		public ReliableMessagingVersion ReliableMessagingVersion
		{
			get
			{
				return (ReliableMessagingVersion)base["reliableMessagingVersion"];
			}
			set
			{
				base["reliableMessagingVersion"] = value;
			}
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000F21E0 File Offset: 0x000F03E0
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			ReliableSessionBindingElement reliableSessionBindingElement = (ReliableSessionBindingElement)bindingElement;
			reliableSessionBindingElement.AcknowledgementInterval = this.AcknowledgementInterval;
			reliableSessionBindingElement.FlowControlEnabled = this.FlowControlEnabled;
			reliableSessionBindingElement.InactivityTimeout = this.InactivityTimeout;
			reliableSessionBindingElement.MaxPendingChannels = this.MaxPendingChannels;
			reliableSessionBindingElement.MaxRetryCount = this.MaxRetryCount;
			reliableSessionBindingElement.MaxTransferWindowSize = this.MaxTransferWindowSize;
			reliableSessionBindingElement.Ordered = this.Ordered;
			reliableSessionBindingElement.ReliableMessagingVersion = this.ReliableMessagingVersion;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000F225C File Offset: 0x000F045C
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ReliableSessionElement reliableSessionElement = (ReliableSessionElement)from;
			this.AcknowledgementInterval = reliableSessionElement.AcknowledgementInterval;
			this.FlowControlEnabled = reliableSessionElement.FlowControlEnabled;
			this.InactivityTimeout = reliableSessionElement.InactivityTimeout;
			this.MaxPendingChannels = reliableSessionElement.MaxPendingChannels;
			this.MaxRetryCount = reliableSessionElement.MaxRetryCount;
			this.MaxTransferWindowSize = reliableSessionElement.MaxTransferWindowSize;
			this.Ordered = reliableSessionElement.Ordered;
			this.ReliableMessagingVersion = reliableSessionElement.ReliableMessagingVersion;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000F22D8 File Offset: 0x000F04D8
		protected internal override BindingElement CreateBindingElement()
		{
			ReliableSessionBindingElement reliableSessionBindingElement = new ReliableSessionBindingElement();
			this.ApplyConfiguration(reliableSessionBindingElement);
			return reliableSessionBindingElement;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000F22F4 File Offset: 0x000F04F4
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			ReliableSessionBindingElement reliableSessionBindingElement = (ReliableSessionBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("acknowledgementInterval", reliableSessionBindingElement.AcknowledgementInterval);
			base.SetPropertyValueIfNotDefaultValue<bool>("flowControlEnabled", reliableSessionBindingElement.FlowControlEnabled);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("inactivityTimeout", reliableSessionBindingElement.InactivityTimeout);
			base.SetPropertyValueIfNotDefaultValue<int>("maxPendingChannels", reliableSessionBindingElement.MaxPendingChannels);
			base.SetPropertyValueIfNotDefaultValue<int>("maxRetryCount", reliableSessionBindingElement.MaxRetryCount);
			base.SetPropertyValueIfNotDefaultValue<int>("maxTransferWindowSize", reliableSessionBindingElement.MaxTransferWindowSize);
			base.SetPropertyValueIfNotDefaultValue<bool>("ordered", reliableSessionBindingElement.Ordered);
			base.SetPropertyValueIfNotDefaultValue<ReliableMessagingVersion>("reliableMessagingVersion", reliableSessionBindingElement.ReliableMessagingVersion);
		}

		// Token: 0x04002CC3 RID: 11459
		private ConfigurationPropertyCollection properties;
	}
}
