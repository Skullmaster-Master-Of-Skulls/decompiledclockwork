using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000639 RID: 1593
	public sealed class LocalServiceSecuritySettingsElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000E9E04 File Offset: 0x000E8004
		// (set) Token: 0x06003D3C RID: 15676 RVA: 0x000E9E16 File Offset: 0x000E8016
		[ConfigurationProperty("detectReplays", DefaultValue = true)]
		public bool DetectReplays
		{
			get
			{
				return (bool)base["detectReplays"];
			}
			set
			{
				base["detectReplays"] = value;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x000E9E29 File Offset: 0x000E8029
		// (set) Token: 0x06003D3E RID: 15678 RVA: 0x000E9E3B File Offset: 0x000E803B
		[ConfigurationProperty("issuedCookieLifetime", DefaultValue = "10:00:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan IssuedCookieLifetime
		{
			get
			{
				return (TimeSpan)base["issuedCookieLifetime"];
			}
			set
			{
				base["issuedCookieLifetime"] = value;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x000E9E4E File Offset: 0x000E804E
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x000E9E60 File Offset: 0x000E8060
		[ConfigurationProperty("maxStatefulNegotiations", DefaultValue = 128)]
		[IntegerValidator(MinValue = 0)]
		public int MaxStatefulNegotiations
		{
			get
			{
				return (int)base["maxStatefulNegotiations"];
			}
			set
			{
				base["maxStatefulNegotiations"] = value;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000E9E73 File Offset: 0x000E8073
		// (set) Token: 0x06003D42 RID: 15682 RVA: 0x000E9E85 File Offset: 0x000E8085
		[ConfigurationProperty("replayCacheSize", DefaultValue = 900000)]
		[IntegerValidator(MinValue = 1)]
		public int ReplayCacheSize
		{
			get
			{
				return (int)base["replayCacheSize"];
			}
			set
			{
				base["replayCacheSize"] = value;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003D43 RID: 15683 RVA: 0x000E9E98 File Offset: 0x000E8098
		// (set) Token: 0x06003D44 RID: 15684 RVA: 0x000E9EAA File Offset: 0x000E80AA
		[ConfigurationProperty("maxClockSkew", DefaultValue = "00:05:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan MaxClockSkew
		{
			get
			{
				return (TimeSpan)base["maxClockSkew"];
			}
			set
			{
				base["maxClockSkew"] = value;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003D45 RID: 15685 RVA: 0x000E9EBD File Offset: 0x000E80BD
		// (set) Token: 0x06003D46 RID: 15686 RVA: 0x000E9ECF File Offset: 0x000E80CF
		[ConfigurationProperty("negotiationTimeout", DefaultValue = "00:01:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan NegotiationTimeout
		{
			get
			{
				return (TimeSpan)base["negotiationTimeout"];
			}
			set
			{
				base["negotiationTimeout"] = value;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000E9EE2 File Offset: 0x000E80E2
		// (set) Token: 0x06003D48 RID: 15688 RVA: 0x000E9EF4 File Offset: 0x000E80F4
		[ConfigurationProperty("replayWindow", DefaultValue = "00:05:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan ReplayWindow
		{
			get
			{
				return (TimeSpan)base["replayWindow"];
			}
			set
			{
				base["replayWindow"] = value;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06003D49 RID: 15689 RVA: 0x000E9F07 File Offset: 0x000E8107
		// (set) Token: 0x06003D4A RID: 15690 RVA: 0x000E9F19 File Offset: 0x000E8119
		[ConfigurationProperty("inactivityTimeout", DefaultValue = "00:02:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
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

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06003D4B RID: 15691 RVA: 0x000E9F2C File Offset: 0x000E812C
		// (set) Token: 0x06003D4C RID: 15692 RVA: 0x000E9F3E File Offset: 0x000E813E
		[ConfigurationProperty("sessionKeyRenewalInterval", DefaultValue = "15:00:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan SessionKeyRenewalInterval
		{
			get
			{
				return (TimeSpan)base["sessionKeyRenewalInterval"];
			}
			set
			{
				base["sessionKeyRenewalInterval"] = value;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06003D4D RID: 15693 RVA: 0x000E9F51 File Offset: 0x000E8151
		// (set) Token: 0x06003D4E RID: 15694 RVA: 0x000E9F63 File Offset: 0x000E8163
		[ConfigurationProperty("sessionKeyRolloverInterval", DefaultValue = "00:05:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan SessionKeyRolloverInterval
		{
			get
			{
				return (TimeSpan)base["sessionKeyRolloverInterval"];
			}
			set
			{
				base["sessionKeyRolloverInterval"] = value;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06003D4F RID: 15695 RVA: 0x000E9F76 File Offset: 0x000E8176
		// (set) Token: 0x06003D50 RID: 15696 RVA: 0x000E9F88 File Offset: 0x000E8188
		[ConfigurationProperty("reconnectTransportOnFailure", DefaultValue = true)]
		public bool ReconnectTransportOnFailure
		{
			get
			{
				return (bool)base["reconnectTransportOnFailure"];
			}
			set
			{
				base["reconnectTransportOnFailure"] = value;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06003D51 RID: 15697 RVA: 0x000E9F9B File Offset: 0x000E819B
		// (set) Token: 0x06003D52 RID: 15698 RVA: 0x000E9FAD File Offset: 0x000E81AD
		[ConfigurationProperty("maxPendingSessions", DefaultValue = 128)]
		[IntegerValidator(MinValue = 1)]
		public int MaxPendingSessions
		{
			get
			{
				return (int)base["maxPendingSessions"];
			}
			set
			{
				base["maxPendingSessions"] = value;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06003D53 RID: 15699 RVA: 0x000E9FC0 File Offset: 0x000E81C0
		// (set) Token: 0x06003D54 RID: 15700 RVA: 0x000E9FD2 File Offset: 0x000E81D2
		[ConfigurationProperty("maxCachedCookies", DefaultValue = 1000)]
		[IntegerValidator(MinValue = 0)]
		public int MaxCachedCookies
		{
			get
			{
				return (int)base["maxCachedCookies"];
			}
			set
			{
				base["maxCachedCookies"] = value;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x000E9FE5 File Offset: 0x000E81E5
		// (set) Token: 0x06003D56 RID: 15702 RVA: 0x000E9FF7 File Offset: 0x000E81F7
		[ConfigurationProperty("timestampValidityDuration", DefaultValue = "00:05:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan TimestampValidityDuration
		{
			get
			{
				return (TimeSpan)base["timestampValidityDuration"];
			}
			set
			{
				base["timestampValidityDuration"] = value;
			}
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000EA00C File Offset: 0x000E820C
		internal void ApplyConfiguration(LocalServiceSecuritySettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			if (base.ElementInformation.Properties["detectReplays"].ValueOrigin != PropertyValueOrigin.Default)
			{
				settings.DetectReplays = this.DetectReplays;
			}
			settings.IssuedCookieLifetime = this.IssuedCookieLifetime;
			settings.MaxClockSkew = this.MaxClockSkew;
			settings.MaxPendingSessions = this.MaxPendingSessions;
			settings.MaxStatefulNegotiations = this.MaxStatefulNegotiations;
			settings.NegotiationTimeout = this.NegotiationTimeout;
			settings.ReconnectTransportOnFailure = this.ReconnectTransportOnFailure;
			settings.ReplayCacheSize = this.ReplayCacheSize;
			settings.ReplayWindow = this.ReplayWindow;
			settings.SessionKeyRenewalInterval = this.SessionKeyRenewalInterval;
			settings.SessionKeyRolloverInterval = this.SessionKeyRolloverInterval;
			settings.InactivityTimeout = this.InactivityTimeout;
			settings.TimestampValidityDuration = this.TimestampValidityDuration;
			settings.MaxCachedCookies = this.MaxCachedCookies;
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000EA0F0 File Offset: 0x000E82F0
		internal void InitializeFrom(LocalServiceSecuritySettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			this.DetectReplays = settings.DetectReplays;
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("issuedCookieLifetime", settings.IssuedCookieLifetime);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("maxClockSkew", settings.MaxClockSkew);
			base.SetPropertyValueIfNotDefaultValue<int>("maxPendingSessions", settings.MaxPendingSessions);
			base.SetPropertyValueIfNotDefaultValue<int>("maxStatefulNegotiations", settings.MaxStatefulNegotiations);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("negotiationTimeout", settings.NegotiationTimeout);
			base.SetPropertyValueIfNotDefaultValue<bool>("reconnectTransportOnFailure", settings.ReconnectTransportOnFailure);
			base.SetPropertyValueIfNotDefaultValue<int>("replayCacheSize", settings.ReplayCacheSize);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("replayWindow", settings.ReplayWindow);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("sessionKeyRenewalInterval", settings.SessionKeyRenewalInterval);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("sessionKeyRolloverInterval", settings.SessionKeyRolloverInterval);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("inactivityTimeout", settings.InactivityTimeout);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("timestampValidityDuration", settings.TimestampValidityDuration);
			base.SetPropertyValueIfNotDefaultValue<int>("maxCachedCookies", settings.MaxCachedCookies);
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000EA1FC File Offset: 0x000E83FC
		internal void CopyFrom(LocalServiceSecuritySettingsElement source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			if (source.ElementInformation.Properties["detectReplays"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.DetectReplays = source.DetectReplays;
			}
			this.IssuedCookieLifetime = source.IssuedCookieLifetime;
			this.MaxClockSkew = source.MaxClockSkew;
			this.MaxPendingSessions = source.MaxPendingSessions;
			this.MaxStatefulNegotiations = source.MaxStatefulNegotiations;
			this.NegotiationTimeout = source.NegotiationTimeout;
			this.ReconnectTransportOnFailure = source.ReconnectTransportOnFailure;
			this.ReplayCacheSize = source.ReplayCacheSize;
			this.ReplayWindow = source.ReplayWindow;
			this.SessionKeyRenewalInterval = source.SessionKeyRenewalInterval;
			this.SessionKeyRolloverInterval = source.SessionKeyRolloverInterval;
			this.InactivityTimeout = source.InactivityTimeout;
			this.TimestampValidityDuration = source.TimestampValidityDuration;
			this.MaxCachedCookies = source.MaxCachedCookies;
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06003D5A RID: 15706 RVA: 0x000EA2E0 File Offset: 0x000E84E0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("detectReplays", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuedCookieLifetime", typeof(TimeSpan), TimeSpan.Parse("10:00:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxStatefulNegotiations", typeof(int), 128, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("replayCacheSize", typeof(int), 900000, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxClockSkew", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("negotiationTimeout", typeof(TimeSpan), TimeSpan.Parse("00:01:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("replayWindow", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("inactivityTimeout", typeof(TimeSpan), TimeSpan.Parse("00:02:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sessionKeyRenewalInterval", typeof(TimeSpan), TimeSpan.Parse("15:00:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sessionKeyRolloverInterval", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("reconnectTransportOnFailure", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxPendingSessions", typeof(int), 128, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxCachedCookies", typeof(int), 1000, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("timestampValidityDuration", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C8E RID: 11406
		private ConfigurationPropertyCollection properties;
	}
}
