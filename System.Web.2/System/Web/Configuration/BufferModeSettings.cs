using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Web.Configuration
{
	// Token: 0x020006AD RID: 1709
	public sealed class BufferModeSettings : ConfigurationElement
	{
		// Token: 0x060052D5 RID: 21205 RVA: 0x00123A58 File Offset: 0x00121C58
		static BufferModeSettings()
		{
			BufferModeSettings._properties = new ConfigurationPropertyCollection();
			BufferModeSettings._properties.Add(BufferModeSettings._propName);
			BufferModeSettings._properties.Add(BufferModeSettings._propMaxBufferSize);
			BufferModeSettings._properties.Add(BufferModeSettings._propMaxFlushSize);
			BufferModeSettings._properties.Add(BufferModeSettings._propUrgentFlushThreshold);
			BufferModeSettings._properties.Add(BufferModeSettings._propRegularFlushInterval);
			BufferModeSettings._properties.Add(BufferModeSettings._propUrgentFlushInterval);
			BufferModeSettings._properties.Add(BufferModeSettings._propMaxBufferThreads);
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x00117E9E File Offset: 0x0011609E
		internal BufferModeSettings()
		{
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x00123C33 File Offset: 0x00121E33
		public BufferModeSettings(string name, int maxBufferSize, int maxFlushSize, int urgentFlushThreshold, TimeSpan regularFlushInterval, TimeSpan urgentFlushInterval, int maxBufferThreads) : this()
		{
			this.Name = name;
			this.MaxBufferSize = maxBufferSize;
			this.MaxFlushSize = maxFlushSize;
			this.UrgentFlushThreshold = urgentFlushThreshold;
			this.RegularFlushInterval = regularFlushInterval;
			this.UrgentFlushInterval = urgentFlushInterval;
			this.MaxBufferThreads = maxBufferThreads;
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x060052D8 RID: 21208 RVA: 0x00123C70 File Offset: 0x00121E70
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BufferModeSettings._properties;
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x060052D9 RID: 21209 RVA: 0x00123C77 File Offset: 0x00121E77
		// (set) Token: 0x060052DA RID: 21210 RVA: 0x00123C89 File Offset: 0x00121E89
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[BufferModeSettings._propName];
			}
			set
			{
				base[BufferModeSettings._propName] = value;
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x060052DB RID: 21211 RVA: 0x00123C97 File Offset: 0x00121E97
		// (set) Token: 0x060052DC RID: 21212 RVA: 0x00123CA9 File Offset: 0x00121EA9
		[ConfigurationProperty("maxBufferSize", IsRequired = true, DefaultValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 1)]
		public int MaxBufferSize
		{
			get
			{
				return (int)base[BufferModeSettings._propMaxBufferSize];
			}
			set
			{
				base[BufferModeSettings._propMaxBufferSize] = value;
			}
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x060052DD RID: 21213 RVA: 0x00123CBC File Offset: 0x00121EBC
		// (set) Token: 0x060052DE RID: 21214 RVA: 0x00123CCE File Offset: 0x00121ECE
		[ConfigurationProperty("maxFlushSize", IsRequired = true, DefaultValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 1)]
		public int MaxFlushSize
		{
			get
			{
				return (int)base[BufferModeSettings._propMaxFlushSize];
			}
			set
			{
				base[BufferModeSettings._propMaxFlushSize] = value;
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x060052DF RID: 21215 RVA: 0x00123CE1 File Offset: 0x00121EE1
		// (set) Token: 0x060052E0 RID: 21216 RVA: 0x00123CF3 File Offset: 0x00121EF3
		[ConfigurationProperty("urgentFlushThreshold", IsRequired = true, DefaultValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 1)]
		public int UrgentFlushThreshold
		{
			get
			{
				return (int)base[BufferModeSettings._propUrgentFlushThreshold];
			}
			set
			{
				base[BufferModeSettings._propUrgentFlushThreshold] = value;
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x00123D06 File Offset: 0x00121F06
		// (set) Token: 0x060052E2 RID: 21218 RVA: 0x00123D18 File Offset: 0x00121F18
		[ConfigurationProperty("regularFlushInterval", IsRequired = true, DefaultValue = "00:00:01")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan RegularFlushInterval
		{
			get
			{
				return (TimeSpan)base[BufferModeSettings._propRegularFlushInterval];
			}
			set
			{
				base[BufferModeSettings._propRegularFlushInterval] = value;
			}
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x00123D2B File Offset: 0x00121F2B
		// (set) Token: 0x060052E4 RID: 21220 RVA: 0x00123D3D File Offset: 0x00121F3D
		[ConfigurationProperty("urgentFlushInterval", IsRequired = true, DefaultValue = "00:00:00")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan UrgentFlushInterval
		{
			get
			{
				return (TimeSpan)base[BufferModeSettings._propUrgentFlushInterval];
			}
			set
			{
				base[BufferModeSettings._propUrgentFlushInterval] = value;
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x060052E5 RID: 21221 RVA: 0x00123D50 File Offset: 0x00121F50
		// (set) Token: 0x060052E6 RID: 21222 RVA: 0x00123D62 File Offset: 0x00121F62
		[ConfigurationProperty("maxBufferThreads", DefaultValue = 1)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 1)]
		public int MaxBufferThreads
		{
			get
			{
				return (int)base[BufferModeSettings._propMaxBufferThreads];
			}
			set
			{
				base[BufferModeSettings._propMaxBufferThreads] = value;
			}
		}

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x060052E7 RID: 21223 RVA: 0x00123D75 File Offset: 0x00121F75
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return BufferModeSettings.s_elemProperty;
			}
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x00123D7C File Offset: 0x00121F7C
		private static void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("bufferMode");
			}
			BufferModeSettings bufferModeSettings = (BufferModeSettings)value;
			if (bufferModeSettings.UrgentFlushThreshold > bufferModeSettings.MaxBufferSize)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_attribute1_must_less_than_or_equal_attribute2", new object[]
				{
					bufferModeSettings.UrgentFlushThreshold.ToString(CultureInfo.InvariantCulture),
					"urgentFlushThreshold",
					bufferModeSettings.MaxBufferSize.ToString(CultureInfo.InvariantCulture),
					"maxBufferSize"
				}), bufferModeSettings.ElementInformation.Properties["urgentFlushThreshold"].Source, bufferModeSettings.ElementInformation.Properties["urgentFlushThreshold"].LineNumber);
			}
			if (bufferModeSettings.MaxFlushSize > bufferModeSettings.MaxBufferSize)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_attribute1_must_less_than_or_equal_attribute2", new object[]
				{
					bufferModeSettings.MaxFlushSize.ToString(CultureInfo.InvariantCulture),
					"maxFlushSize",
					bufferModeSettings.MaxBufferSize.ToString(CultureInfo.InvariantCulture),
					"maxBufferSize"
				}), bufferModeSettings.ElementInformation.Properties["maxFlushSize"].Source, bufferModeSettings.ElementInformation.Properties["maxFlushSize"].LineNumber);
			}
			if (!(bufferModeSettings.UrgentFlushInterval < bufferModeSettings.RegularFlushInterval))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_attribute1_must_less_than_attribute2", new object[]
				{
					bufferModeSettings.UrgentFlushInterval.ToString(),
					"urgentFlushInterval",
					bufferModeSettings.RegularFlushInterval.ToString(),
					"regularFlushInterval"
				}), bufferModeSettings.ElementInformation.Properties["urgentFlushInterval"].Source, bufferModeSettings.ElementInformation.Properties["urgentFlushInterval"].LineNumber);
			}
		}

		// Token: 0x04002B70 RID: 11120
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(BufferModeSettings), new ValidatorCallback(BufferModeSettings.Validate)));

		// Token: 0x04002B71 RID: 11121
		private const int DefaultMaxBufferThreads = 1;

		// Token: 0x04002B72 RID: 11122
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002B73 RID: 11123
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002B74 RID: 11124
		private static readonly ConfigurationProperty _propMaxBufferSize = new ConfigurationProperty("maxBufferSize", typeof(int), int.MaxValue, new InfiniteIntConverter(), StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002B75 RID: 11125
		private static readonly ConfigurationProperty _propMaxFlushSize = new ConfigurationProperty("maxFlushSize", typeof(int), int.MaxValue, new InfiniteIntConverter(), StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002B76 RID: 11126
		private static readonly ConfigurationProperty _propUrgentFlushThreshold = new ConfigurationProperty("urgentFlushThreshold", typeof(int), int.MaxValue, new InfiniteIntConverter(), StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002B77 RID: 11127
		private static readonly ConfigurationProperty _propRegularFlushInterval = new ConfigurationProperty("regularFlushInterval", typeof(TimeSpan), TimeSpan.FromSeconds(1.0), StdValidatorsAndConverters.InfiniteTimeSpanConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002B78 RID: 11128
		private static readonly ConfigurationProperty _propUrgentFlushInterval = new ConfigurationProperty("urgentFlushInterval", typeof(TimeSpan), TimeSpan.Zero, StdValidatorsAndConverters.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002B79 RID: 11129
		private static readonly ConfigurationProperty _propMaxBufferThreads = new ConfigurationProperty("maxBufferThreads", typeof(int), 1, new InfiniteIntConverter(), StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.None);
	}
}
