using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000737 RID: 1847
	public sealed class ProfileSettings : ConfigurationElement
	{
		// Token: 0x0600591B RID: 22811 RVA: 0x00137188 File Offset: 0x00135388
		static ProfileSettings()
		{
			ProfileSettings._properties = new ConfigurationPropertyCollection();
			ProfileSettings._properties.Add(ProfileSettings._propName);
			ProfileSettings._properties.Add(ProfileSettings._propMinInstances);
			ProfileSettings._properties.Add(ProfileSettings._propMaxLimit);
			ProfileSettings._properties.Add(ProfileSettings._propMinInterval);
			ProfileSettings._properties.Add(ProfileSettings._propCustom);
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x00117E9E File Offset: 0x0011609E
		internal ProfileSettings()
		{
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x001372AC File Offset: 0x001354AC
		public ProfileSettings(string name) : this()
		{
			this.Name = name;
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x001372BB File Offset: 0x001354BB
		public ProfileSettings(string name, int minInstances, int maxLimit, TimeSpan minInterval) : this(name)
		{
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = minInterval;
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x001372DA File Offset: 0x001354DA
		public ProfileSettings(string name, int minInstances, int maxLimit, TimeSpan minInterval, string custom) : this(name)
		{
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = minInterval;
			this.Custom = custom;
		}

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x06005920 RID: 22816 RVA: 0x00137301 File Offset: 0x00135501
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileSettings._properties;
			}
		}

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x06005921 RID: 22817 RVA: 0x00137308 File Offset: 0x00135508
		// (set) Token: 0x06005922 RID: 22818 RVA: 0x0013731A File Offset: 0x0013551A
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[ProfileSettings._propName];
			}
			set
			{
				base[ProfileSettings._propName] = value;
			}
		}

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x06005923 RID: 22819 RVA: 0x00137328 File Offset: 0x00135528
		// (set) Token: 0x06005924 RID: 22820 RVA: 0x0013733A File Offset: 0x0013553A
		[ConfigurationProperty("minInstances", DefaultValue = 1)]
		[IntegerValidator(MinValue = 1)]
		public int MinInstances
		{
			get
			{
				return (int)base[ProfileSettings._propMinInstances];
			}
			set
			{
				base[ProfileSettings._propMinInstances] = value;
			}
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06005925 RID: 22821 RVA: 0x0013734D File Offset: 0x0013554D
		// (set) Token: 0x06005926 RID: 22822 RVA: 0x0013735F File Offset: 0x0013555F
		[ConfigurationProperty("maxLimit", DefaultValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0)]
		public int MaxLimit
		{
			get
			{
				return (int)base[ProfileSettings._propMaxLimit];
			}
			set
			{
				base[ProfileSettings._propMaxLimit] = value;
			}
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06005927 RID: 22823 RVA: 0x00137372 File Offset: 0x00135572
		// (set) Token: 0x06005928 RID: 22824 RVA: 0x00137384 File Offset: 0x00135584
		[ConfigurationProperty("minInterval", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan MinInterval
		{
			get
			{
				return (TimeSpan)base[ProfileSettings._propMinInterval];
			}
			set
			{
				base[ProfileSettings._propMinInterval] = value;
			}
		}

		// Token: 0x170019D8 RID: 6616
		// (get) Token: 0x06005929 RID: 22825 RVA: 0x00137397 File Offset: 0x00135597
		// (set) Token: 0x0600592A RID: 22826 RVA: 0x001373A9 File Offset: 0x001355A9
		[ConfigurationProperty("custom", DefaultValue = "")]
		public string Custom
		{
			get
			{
				return (string)base[ProfileSettings._propCustom];
			}
			set
			{
				base[ProfileSettings._propCustom] = value;
			}
		}

		// Token: 0x04002F4A RID: 12106
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002F4B RID: 12107
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002F4C RID: 12108
		private static readonly ConfigurationProperty _propMinInstances = new ConfigurationProperty("minInstances", typeof(int), RuleSettings.DEFAULT_MIN_INSTANCES, null, StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F4D RID: 12109
		private static readonly ConfigurationProperty _propMaxLimit = new ConfigurationProperty("maxLimit", typeof(int), RuleSettings.DEFAULT_MAX_LIMIT, new InfiniteIntConverter(), StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F4E RID: 12110
		private static readonly ConfigurationProperty _propMinInterval = new ConfigurationProperty("minInterval", typeof(TimeSpan), RuleSettings.DEFAULT_MIN_INTERVAL, StdValidatorsAndConverters.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F4F RID: 12111
		private static readonly ConfigurationProperty _propCustom = new ConfigurationProperty("custom", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
