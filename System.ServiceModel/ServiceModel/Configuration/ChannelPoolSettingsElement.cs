using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005FD RID: 1533
	public sealed class ChannelPoolSettingsElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000E2A22 File Offset: 0x000E0C22
		// (set) Token: 0x06003B1E RID: 15134 RVA: 0x000E2A34 File Offset: 0x000E0C34
		[ConfigurationProperty("idleTimeout", DefaultValue = "00:02:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan IdleTimeout
		{
			get
			{
				return (TimeSpan)base["idleTimeout"];
			}
			set
			{
				base["idleTimeout"] = value;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000E2A47 File Offset: 0x000E0C47
		// (set) Token: 0x06003B20 RID: 15136 RVA: 0x000E2A59 File Offset: 0x000E0C59
		[ConfigurationProperty("leaseTimeout", DefaultValue = "00:10:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan LeaseTimeout
		{
			get
			{
				return (TimeSpan)base["leaseTimeout"];
			}
			set
			{
				base["leaseTimeout"] = value;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000E2A6C File Offset: 0x000E0C6C
		// (set) Token: 0x06003B22 RID: 15138 RVA: 0x000E2A7E File Offset: 0x000E0C7E
		[ConfigurationProperty("maxOutboundChannelsPerEndpoint", DefaultValue = 10)]
		[IntegerValidator(MinValue = 1)]
		public int MaxOutboundChannelsPerEndpoint
		{
			get
			{
				return (int)base["maxOutboundChannelsPerEndpoint"];
			}
			set
			{
				base["maxOutboundChannelsPerEndpoint"] = value;
			}
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000E2A91 File Offset: 0x000E0C91
		internal void ApplyConfiguration(ChannelPoolSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			settings.IdleTimeout = this.IdleTimeout;
			settings.LeaseTimeout = this.LeaseTimeout;
			settings.MaxOutboundChannelsPerEndpoint = this.MaxOutboundChannelsPerEndpoint;
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000E2ACC File Offset: 0x000E0CCC
		internal void InitializeFrom(ChannelPoolSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("idleTimeout", settings.IdleTimeout);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("leaseTimeout", settings.LeaseTimeout);
			base.SetPropertyValueIfNotDefaultValue<int>("maxOutboundChannelsPerEndpoint", settings.MaxOutboundChannelsPerEndpoint);
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x000E2B1F File Offset: 0x000E0D1F
		internal void CopyFrom(ChannelPoolSettingsElement source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.IdleTimeout = source.IdleTimeout;
			this.LeaseTimeout = source.LeaseTimeout;
			this.MaxOutboundChannelsPerEndpoint = source.MaxOutboundChannelsPerEndpoint;
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000E2B58 File Offset: 0x000E0D58
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.Parse("00:02:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("leaseTimeout", typeof(TimeSpan), TimeSpan.Parse("00:10:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxOutboundChannelsPerEndpoint", typeof(int), 10, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A7E RID: 10878
		private ConfigurationPropertyCollection properties;
	}
}
