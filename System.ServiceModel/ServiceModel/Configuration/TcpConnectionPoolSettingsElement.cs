using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006BC RID: 1724
	public sealed class TcpConnectionPoolSettingsElement : ServiceModelConfigurationElement
	{
		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060042E8 RID: 17128 RVA: 0x000FCCE4 File Offset: 0x000FAEE4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("groupName", typeof(string), "default", null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("leaseTimeout", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.Parse("00:02:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxOutboundConnectionsPerEndpoint", typeof(int), 10, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x000FCE21 File Offset: 0x000FB021
		// (set) Token: 0x060042EB RID: 17131 RVA: 0x000FCE33 File Offset: 0x000FB033
		[ConfigurationProperty("groupName", DefaultValue = "default")]
		[StringValidator(MinLength = 0)]
		public string GroupName
		{
			get
			{
				return (string)base["groupName"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["groupName"] = value;
			}
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x000FCE50 File Offset: 0x000FB050
		// (set) Token: 0x060042ED RID: 17133 RVA: 0x000FCE62 File Offset: 0x000FB062
		[ConfigurationProperty("leaseTimeout", DefaultValue = "00:05:00")]
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

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x060042EE RID: 17134 RVA: 0x000FCE75 File Offset: 0x000FB075
		// (set) Token: 0x060042EF RID: 17135 RVA: 0x000FCE87 File Offset: 0x000FB087
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

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x060042F0 RID: 17136 RVA: 0x000FCE9A File Offset: 0x000FB09A
		// (set) Token: 0x060042F1 RID: 17137 RVA: 0x000FCEAC File Offset: 0x000FB0AC
		[ConfigurationProperty("maxOutboundConnectionsPerEndpoint", DefaultValue = 10)]
		[IntegerValidator(MinValue = 0)]
		public int MaxOutboundConnectionsPerEndpoint
		{
			get
			{
				return (int)base["maxOutboundConnectionsPerEndpoint"];
			}
			set
			{
				base["maxOutboundConnectionsPerEndpoint"] = value;
			}
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x000FCEC0 File Offset: 0x000FB0C0
		internal void ApplyConfiguration(TcpConnectionPoolSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			settings.GroupName = this.GroupName;
			settings.IdleTimeout = this.IdleTimeout;
			settings.LeaseTimeout = this.LeaseTimeout;
			settings.MaxOutboundConnectionsPerEndpoint = this.MaxOutboundConnectionsPerEndpoint;
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x000FCF10 File Offset: 0x000FB110
		internal void InitializeFrom(TcpConnectionPoolSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			base.SetPropertyValueIfNotDefaultValue<string>("groupName", settings.GroupName);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("idleTimeout", settings.IdleTimeout);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("leaseTimeout", settings.LeaseTimeout);
			base.SetPropertyValueIfNotDefaultValue<int>("maxOutboundConnectionsPerEndpoint", settings.MaxOutboundConnectionsPerEndpoint);
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000FCF74 File Offset: 0x000FB174
		internal void CopyFrom(TcpConnectionPoolSettingsElement source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.GroupName = source.GroupName;
			this.IdleTimeout = source.IdleTimeout;
			this.LeaseTimeout = source.LeaseTimeout;
			this.MaxOutboundConnectionsPerEndpoint = source.MaxOutboundConnectionsPerEndpoint;
		}

		// Token: 0x04002D0C RID: 11532
		private ConfigurationPropertyCollection properties;
	}
}
