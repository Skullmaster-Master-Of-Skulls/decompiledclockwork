using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006EE RID: 1774
	public sealed class HealthMonitoringSection : ConfigurationSection
	{
		// Token: 0x06005528 RID: 21800 RVA: 0x00129AF0 File Offset: 0x00127CF0
		static HealthMonitoringSection()
		{
			HealthMonitoringSection._properties = new ConfigurationPropertyCollection();
			HealthMonitoringSection._properties.Add(HealthMonitoringSection._propHeartbeatInterval);
			HealthMonitoringSection._properties.Add(HealthMonitoringSection._propEnabled);
			HealthMonitoringSection._properties.Add(HealthMonitoringSection._propBufferModes);
			HealthMonitoringSection._properties.Add(HealthMonitoringSection._propProviders);
			HealthMonitoringSection._properties.Add(HealthMonitoringSection._propProfileSettingsCollection);
			HealthMonitoringSection._properties.Add(HealthMonitoringSection._propRuleSettingsCollection);
			HealthMonitoringSection._properties.Add(HealthMonitoringSection._propEventMappingSettingsCollection);
		}

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x0600552A RID: 21802 RVA: 0x00129C61 File Offset: 0x00127E61
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HealthMonitoringSection._properties;
			}
		}

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x00129C68 File Offset: 0x00127E68
		// (set) Token: 0x0600552C RID: 21804 RVA: 0x00129C7A File Offset: 0x00127E7A
		[ConfigurationProperty("heartbeatInterval", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "24.20:31:23")]
		public TimeSpan HeartbeatInterval
		{
			get
			{
				return (TimeSpan)base[HealthMonitoringSection._propHeartbeatInterval];
			}
			set
			{
				base[HealthMonitoringSection._propHeartbeatInterval] = value;
			}
		}

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x00129C8D File Offset: 0x00127E8D
		// (set) Token: 0x0600552E RID: 21806 RVA: 0x00129C9F File Offset: 0x00127E9F
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base[HealthMonitoringSection._propEnabled];
			}
			set
			{
				base[HealthMonitoringSection._propEnabled] = value;
			}
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x0600552F RID: 21807 RVA: 0x00129CB2 File Offset: 0x00127EB2
		[ConfigurationProperty("bufferModes")]
		public BufferModesCollection BufferModes
		{
			get
			{
				return (BufferModesCollection)base[HealthMonitoringSection._propBufferModes];
			}
		}

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x06005530 RID: 21808 RVA: 0x00129CC4 File Offset: 0x00127EC4
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[HealthMonitoringSection._propProviders];
			}
		}

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x06005531 RID: 21809 RVA: 0x00129CD6 File Offset: 0x00127ED6
		[ConfigurationProperty("profiles")]
		public ProfileSettingsCollection Profiles
		{
			get
			{
				return (ProfileSettingsCollection)base[HealthMonitoringSection._propProfileSettingsCollection];
			}
		}

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06005532 RID: 21810 RVA: 0x00129CE8 File Offset: 0x00127EE8
		[ConfigurationProperty("rules")]
		public RuleSettingsCollection Rules
		{
			get
			{
				return (RuleSettingsCollection)base[HealthMonitoringSection._propRuleSettingsCollection];
			}
		}

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x06005533 RID: 21811 RVA: 0x00129CFA File Offset: 0x00127EFA
		[ConfigurationProperty("eventMappings")]
		public EventMappingSettingsCollection EventMappings
		{
			get
			{
				return (EventMappingSettingsCollection)base[HealthMonitoringSection._propEventMappingSettingsCollection];
			}
		}

		// Token: 0x04002C9D RID: 11421
		private const int MAX_HEARTBEAT_VALUE = 2147483;

		// Token: 0x04002C9E RID: 11422
		private const bool DEFAULT_HEALTH_MONITORING_ENABLED = true;

		// Token: 0x04002C9F RID: 11423
		private const int DEFAULT_HEARTBEATINTERVAL = 0;

		// Token: 0x04002CA0 RID: 11424
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002CA1 RID: 11425
		private static readonly ConfigurationProperty _propHeartbeatInterval = new ConfigurationProperty("heartbeatInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), StdValidatorsAndConverters.TimeSpanSecondsConverter, new TimeSpanValidator(TimeSpan.Zero, TimeSpan.FromSeconds(2147483.0)), ConfigurationPropertyOptions.None);

		// Token: 0x04002CA2 RID: 11426
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002CA3 RID: 11427
		private static readonly ConfigurationProperty _propBufferModes = new ConfigurationProperty("bufferModes", typeof(BufferModesCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002CA4 RID: 11428
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002CA5 RID: 11429
		private static readonly ConfigurationProperty _propProfileSettingsCollection = new ConfigurationProperty("profiles", typeof(ProfileSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002CA6 RID: 11430
		private static readonly ConfigurationProperty _propRuleSettingsCollection = new ConfigurationProperty("rules", typeof(RuleSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002CA7 RID: 11431
		private static readonly ConfigurationProperty _propEventMappingSettingsCollection = new ConfigurationProperty("eventMappings", typeof(EventMappingSettingsCollection), null, ConfigurationPropertyOptions.None);
	}
}
