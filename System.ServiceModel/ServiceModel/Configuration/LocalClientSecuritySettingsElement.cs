using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000638 RID: 1592
	public sealed class LocalClientSecuritySettingsElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003D20 RID: 15648 RVA: 0x000E9710 File Offset: 0x000E7910
		// (set) Token: 0x06003D21 RID: 15649 RVA: 0x000E9722 File Offset: 0x000E7922
		[ConfigurationProperty("cacheCookies", DefaultValue = true)]
		public bool CacheCookies
		{
			get
			{
				return (bool)base["cacheCookies"];
			}
			set
			{
				base["cacheCookies"] = value;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000E9735 File Offset: 0x000E7935
		// (set) Token: 0x06003D23 RID: 15651 RVA: 0x000E9747 File Offset: 0x000E7947
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

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x000E975A File Offset: 0x000E795A
		// (set) Token: 0x06003D25 RID: 15653 RVA: 0x000E976C File Offset: 0x000E796C
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

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x000E977F File Offset: 0x000E797F
		// (set) Token: 0x06003D27 RID: 15655 RVA: 0x000E9791 File Offset: 0x000E7991
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

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x000E97A4 File Offset: 0x000E79A4
		// (set) Token: 0x06003D29 RID: 15657 RVA: 0x000E97B6 File Offset: 0x000E79B6
		[ConfigurationProperty("maxCookieCachingTime", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan MaxCookieCachingTime
		{
			get
			{
				return (TimeSpan)base["maxCookieCachingTime"];
			}
			set
			{
				base["maxCookieCachingTime"] = value;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000E97C9 File Offset: 0x000E79C9
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x000E97DB File Offset: 0x000E79DB
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

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x000E97EE File Offset: 0x000E79EE
		// (set) Token: 0x06003D2D RID: 15661 RVA: 0x000E9800 File Offset: 0x000E7A00
		[ConfigurationProperty("sessionKeyRenewalInterval", DefaultValue = "10:00:00")]
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

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x000E9813 File Offset: 0x000E7A13
		// (set) Token: 0x06003D2F RID: 15663 RVA: 0x000E9825 File Offset: 0x000E7A25
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

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000E9838 File Offset: 0x000E7A38
		// (set) Token: 0x06003D31 RID: 15665 RVA: 0x000E984A File Offset: 0x000E7A4A
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

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x000E985D File Offset: 0x000E7A5D
		// (set) Token: 0x06003D33 RID: 15667 RVA: 0x000E986F File Offset: 0x000E7A6F
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

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003D34 RID: 15668 RVA: 0x000E9882 File Offset: 0x000E7A82
		// (set) Token: 0x06003D35 RID: 15669 RVA: 0x000E9894 File Offset: 0x000E7A94
		[ConfigurationProperty("cookieRenewalThresholdPercentage", DefaultValue = 60)]
		[IntegerValidator(MinValue = 0, MaxValue = 100)]
		public int CookieRenewalThresholdPercentage
		{
			get
			{
				return (int)base["cookieRenewalThresholdPercentage"];
			}
			set
			{
				base["cookieRenewalThresholdPercentage"] = value;
			}
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000E98A8 File Offset: 0x000E7AA8
		internal void ApplyConfiguration(LocalClientSecuritySettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			settings.CacheCookies = this.CacheCookies;
			if (base.ElementInformation.Properties["detectReplays"].ValueOrigin != PropertyValueOrigin.Default)
			{
				settings.DetectReplays = this.DetectReplays;
			}
			settings.MaxClockSkew = this.MaxClockSkew;
			settings.MaxCookieCachingTime = this.MaxCookieCachingTime;
			settings.ReconnectTransportOnFailure = this.ReconnectTransportOnFailure;
			settings.ReplayCacheSize = this.ReplayCacheSize;
			settings.ReplayWindow = this.ReplayWindow;
			settings.SessionKeyRenewalInterval = this.SessionKeyRenewalInterval;
			settings.SessionKeyRolloverInterval = this.SessionKeyRolloverInterval;
			settings.TimestampValidityDuration = this.TimestampValidityDuration;
			settings.CookieRenewalThresholdPercentage = this.CookieRenewalThresholdPercentage;
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000E9968 File Offset: 0x000E7B68
		internal void InitializeFrom(LocalClientSecuritySettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			base.SetPropertyValueIfNotDefaultValue<bool>("cacheCookies", settings.CacheCookies);
			this.DetectReplays = settings.DetectReplays;
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("maxClockSkew", settings.MaxClockSkew);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("maxCookieCachingTime", settings.MaxCookieCachingTime);
			base.SetPropertyValueIfNotDefaultValue<bool>("reconnectTransportOnFailure", settings.ReconnectTransportOnFailure);
			base.SetPropertyValueIfNotDefaultValue<int>("replayCacheSize", settings.ReplayCacheSize);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("replayWindow", settings.ReplayWindow);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("sessionKeyRenewalInterval", settings.SessionKeyRenewalInterval);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("sessionKeyRolloverInterval", settings.SessionKeyRolloverInterval);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("timestampValidityDuration", settings.TimestampValidityDuration);
			base.SetPropertyValueIfNotDefaultValue<int>("cookieRenewalThresholdPercentage", settings.CookieRenewalThresholdPercentage);
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000E9A40 File Offset: 0x000E7C40
		internal void CopyFrom(LocalClientSecuritySettingsElement source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.CacheCookies = source.CacheCookies;
			if (source.ElementInformation.Properties["detectReplays"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.DetectReplays = source.DetectReplays;
			}
			this.MaxClockSkew = source.MaxClockSkew;
			this.MaxCookieCachingTime = source.MaxCookieCachingTime;
			this.ReconnectTransportOnFailure = source.ReconnectTransportOnFailure;
			this.ReplayCacheSize = source.ReplayCacheSize;
			this.ReplayWindow = source.ReplayWindow;
			this.SessionKeyRenewalInterval = source.SessionKeyRenewalInterval;
			this.SessionKeyRolloverInterval = source.SessionKeyRolloverInterval;
			this.TimestampValidityDuration = source.TimestampValidityDuration;
			this.CookieRenewalThresholdPercentage = source.CookieRenewalThresholdPercentage;
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x000E9B00 File Offset: 0x000E7D00
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("cacheCookies", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("detectReplays", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("replayCacheSize", typeof(int), 900000, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxClockSkew", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxCookieCachingTime", typeof(TimeSpan), TimeSpan.Parse("10675199.02:48:05.4775807", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("replayWindow", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sessionKeyRenewalInterval", typeof(TimeSpan), TimeSpan.Parse("10:00:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sessionKeyRolloverInterval", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("reconnectTransportOnFailure", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("timestampValidityDuration", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("cookieRenewalThresholdPercentage", typeof(int), 60, null, new IntegerValidator(0, 100, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C8D RID: 11405
		private ConfigurationPropertyCollection properties;
	}
}
