using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200074B RID: 1867
	public sealed class RuleSettings : ConfigurationElement
	{
		// Token: 0x060059CF RID: 22991 RVA: 0x00139AB8 File Offset: 0x00137CB8
		static RuleSettings()
		{
			RuleSettings._properties = new ConfigurationPropertyCollection();
			RuleSettings._properties.Add(RuleSettings._propName);
			RuleSettings._properties.Add(RuleSettings._propEventName);
			RuleSettings._properties.Add(RuleSettings._propProvider);
			RuleSettings._properties.Add(RuleSettings._propProfile);
			RuleSettings._properties.Add(RuleSettings._propMinInstances);
			RuleSettings._properties.Add(RuleSettings._propMaxLimit);
			RuleSettings._properties.Add(RuleSettings._propMinInterval);
			RuleSettings._properties.Add(RuleSettings._propCustom);
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x00117E9E File Offset: 0x0011609E
		internal RuleSettings()
		{
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x00139C86 File Offset: 0x00137E86
		public RuleSettings(string name, string eventName, string provider) : this()
		{
			this.Name = name;
			this.EventName = eventName;
			this.Provider = provider;
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x00139CA3 File Offset: 0x00137EA3
		public RuleSettings(string name, string eventName, string provider, string profile, int minInstances, int maxLimit, TimeSpan minInterval) : this(name, eventName, provider)
		{
			this.Profile = profile;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = minInterval;
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x00139CCE File Offset: 0x00137ECE
		public RuleSettings(string name, string eventName, string provider, string profile, int minInstances, int maxLimit, TimeSpan minInterval, string custom) : this(name, eventName, provider)
		{
			this.Profile = profile;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = minInterval;
			this.Custom = custom;
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x060059D4 RID: 22996 RVA: 0x00139D01 File Offset: 0x00137F01
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RuleSettings._properties;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x060059D5 RID: 22997 RVA: 0x00139D08 File Offset: 0x00137F08
		// (set) Token: 0x060059D6 RID: 22998 RVA: 0x00139D1A File Offset: 0x00137F1A
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[RuleSettings._propName];
			}
			set
			{
				base[RuleSettings._propName] = value;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x060059D7 RID: 22999 RVA: 0x00139D28 File Offset: 0x00137F28
		// (set) Token: 0x060059D8 RID: 23000 RVA: 0x00139D3A File Offset: 0x00137F3A
		[ConfigurationProperty("eventName", IsRequired = true, DefaultValue = "")]
		public string EventName
		{
			get
			{
				return (string)base[RuleSettings._propEventName];
			}
			set
			{
				base[RuleSettings._propEventName] = value;
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x060059D9 RID: 23001 RVA: 0x00139D48 File Offset: 0x00137F48
		// (set) Token: 0x060059DA RID: 23002 RVA: 0x00139D5A File Offset: 0x00137F5A
		[ConfigurationProperty("custom", DefaultValue = "")]
		public string Custom
		{
			get
			{
				return (string)base[RuleSettings._propCustom];
			}
			set
			{
				base[RuleSettings._propCustom] = value;
			}
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x060059DB RID: 23003 RVA: 0x00139D68 File Offset: 0x00137F68
		// (set) Token: 0x060059DC RID: 23004 RVA: 0x00139D7A File Offset: 0x00137F7A
		[ConfigurationProperty("profile", DefaultValue = "")]
		public string Profile
		{
			get
			{
				return (string)base[RuleSettings._propProfile];
			}
			set
			{
				base[RuleSettings._propProfile] = value;
			}
		}

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x060059DD RID: 23005 RVA: 0x00139D88 File Offset: 0x00137F88
		// (set) Token: 0x060059DE RID: 23006 RVA: 0x00139D9A File Offset: 0x00137F9A
		[ConfigurationProperty("provider", DefaultValue = "")]
		public string Provider
		{
			get
			{
				return (string)base[RuleSettings._propProvider];
			}
			set
			{
				base[RuleSettings._propProvider] = value;
			}
		}

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x060059DF RID: 23007 RVA: 0x00139DA8 File Offset: 0x00137FA8
		// (set) Token: 0x060059E0 RID: 23008 RVA: 0x00139DBA File Offset: 0x00137FBA
		[ConfigurationProperty("minInstances", DefaultValue = 1)]
		[IntegerValidator(MinValue = 1)]
		public int MinInstances
		{
			get
			{
				return (int)base[RuleSettings._propMinInstances];
			}
			set
			{
				base[RuleSettings._propMinInstances] = value;
			}
		}

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x060059E1 RID: 23009 RVA: 0x00139DCD File Offset: 0x00137FCD
		// (set) Token: 0x060059E2 RID: 23010 RVA: 0x00139DDF File Offset: 0x00137FDF
		[ConfigurationProperty("maxLimit", DefaultValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0)]
		public int MaxLimit
		{
			get
			{
				return (int)base[RuleSettings._propMaxLimit];
			}
			set
			{
				base[RuleSettings._propMaxLimit] = value;
			}
		}

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x060059E3 RID: 23011 RVA: 0x00139DF2 File Offset: 0x00137FF2
		// (set) Token: 0x060059E4 RID: 23012 RVA: 0x00139E04 File Offset: 0x00138004
		[ConfigurationProperty("minInterval", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan MinInterval
		{
			get
			{
				return (TimeSpan)base[RuleSettings._propMinInterval];
			}
			set
			{
				base[RuleSettings._propMinInterval] = value;
			}
		}

		// Token: 0x04002FB0 RID: 12208
		internal static int DEFAULT_MIN_INSTANCES = 1;

		// Token: 0x04002FB1 RID: 12209
		internal static int DEFAULT_MAX_LIMIT = int.MaxValue;

		// Token: 0x04002FB2 RID: 12210
		internal static TimeSpan DEFAULT_MIN_INTERVAL = TimeSpan.Zero;

		// Token: 0x04002FB3 RID: 12211
		internal static string DEFAULT_CUSTOM_EVAL = null;

		// Token: 0x04002FB4 RID: 12212
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002FB5 RID: 12213
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002FB6 RID: 12214
		private static readonly ConfigurationProperty _propEventName = new ConfigurationProperty("eventName", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002FB7 RID: 12215
		private static readonly ConfigurationProperty _propProvider = new ConfigurationProperty("provider", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002FB8 RID: 12216
		private static readonly ConfigurationProperty _propProfile = new ConfigurationProperty("profile", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002FB9 RID: 12217
		private static readonly ConfigurationProperty _propMinInstances = new ConfigurationProperty("minInstances", typeof(int), RuleSettings.DEFAULT_MIN_INSTANCES, null, StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002FBA RID: 12218
		private static readonly ConfigurationProperty _propMaxLimit = new ConfigurationProperty("maxLimit", typeof(int), RuleSettings.DEFAULT_MAX_LIMIT, new InfiniteIntConverter(), StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002FBB RID: 12219
		private static readonly ConfigurationProperty _propMinInterval = new ConfigurationProperty("minInterval", typeof(TimeSpan), RuleSettings.DEFAULT_MIN_INTERVAL, StdValidatorsAndConverters.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002FBC RID: 12220
		private static readonly ConfigurationProperty _propCustom = new ConfigurationProperty("custom", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
