using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000762 RID: 1890
	public sealed class TraceSection : ConfigurationSection
	{
		// Token: 0x06005B14 RID: 23316 RVA: 0x0013C694 File Offset: 0x0013A894
		static TraceSection()
		{
			TraceSection._properties = new ConfigurationPropertyCollection();
			TraceSection._properties.Add(TraceSection._propEnabled);
			TraceSection._properties.Add(TraceSection._propLocalOnly);
			TraceSection._properties.Add(TraceSection._propMostRecent);
			TraceSection._properties.Add(TraceSection._propPageOutput);
			TraceSection._properties.Add(TraceSection._propRequestLimit);
			TraceSection._properties.Add(TraceSection._propMode);
			TraceSection._properties.Add(TraceSection._writeToDiagnosticTrace);
		}

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x06005B16 RID: 23318 RVA: 0x0013C7FB File Offset: 0x0013A9FB
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TraceSection._properties;
			}
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x06005B17 RID: 23319 RVA: 0x0013C802 File Offset: 0x0013AA02
		// (set) Token: 0x06005B18 RID: 23320 RVA: 0x0013C814 File Offset: 0x0013AA14
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[TraceSection._propEnabled];
			}
			set
			{
				base[TraceSection._propEnabled] = value;
			}
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x06005B19 RID: 23321 RVA: 0x0013C827 File Offset: 0x0013AA27
		// (set) Token: 0x06005B1A RID: 23322 RVA: 0x0013C839 File Offset: 0x0013AA39
		[ConfigurationProperty("mostRecent", DefaultValue = false)]
		public bool MostRecent
		{
			get
			{
				return (bool)base[TraceSection._propMostRecent];
			}
			set
			{
				base[TraceSection._propMostRecent] = value;
			}
		}

		// Token: 0x17001AAF RID: 6831
		// (get) Token: 0x06005B1B RID: 23323 RVA: 0x0013C84C File Offset: 0x0013AA4C
		// (set) Token: 0x06005B1C RID: 23324 RVA: 0x0013C85E File Offset: 0x0013AA5E
		[ConfigurationProperty("localOnly", DefaultValue = true)]
		public bool LocalOnly
		{
			get
			{
				return (bool)base[TraceSection._propLocalOnly];
			}
			set
			{
				base[TraceSection._propLocalOnly] = value;
			}
		}

		// Token: 0x17001AB0 RID: 6832
		// (get) Token: 0x06005B1D RID: 23325 RVA: 0x0013C871 File Offset: 0x0013AA71
		// (set) Token: 0x06005B1E RID: 23326 RVA: 0x0013C883 File Offset: 0x0013AA83
		[ConfigurationProperty("pageOutput", DefaultValue = false)]
		public bool PageOutput
		{
			get
			{
				return (bool)base[TraceSection._propPageOutput];
			}
			set
			{
				base[TraceSection._propPageOutput] = value;
			}
		}

		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x0013C896 File Offset: 0x0013AA96
		// (set) Token: 0x06005B20 RID: 23328 RVA: 0x0013C8A8 File Offset: 0x0013AAA8
		[ConfigurationProperty("requestLimit", DefaultValue = 10)]
		[IntegerValidator(MinValue = 0)]
		public int RequestLimit
		{
			get
			{
				return (int)base[TraceSection._propRequestLimit];
			}
			set
			{
				base[TraceSection._propRequestLimit] = value;
			}
		}

		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x06005B21 RID: 23329 RVA: 0x0013C8BB File Offset: 0x0013AABB
		// (set) Token: 0x06005B22 RID: 23330 RVA: 0x0013C8CD File Offset: 0x0013AACD
		[ConfigurationProperty("traceMode", DefaultValue = TraceDisplayMode.SortByTime)]
		public TraceDisplayMode TraceMode
		{
			get
			{
				return (TraceDisplayMode)base[TraceSection._propMode];
			}
			set
			{
				base[TraceSection._propMode] = value;
			}
		}

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x06005B23 RID: 23331 RVA: 0x0013C8E0 File Offset: 0x0013AAE0
		// (set) Token: 0x06005B24 RID: 23332 RVA: 0x0013C8F2 File Offset: 0x0013AAF2
		[ConfigurationProperty("writeToDiagnosticsTrace", DefaultValue = false)]
		public bool WriteToDiagnosticsTrace
		{
			get
			{
				return (bool)base[TraceSection._writeToDiagnosticTrace];
			}
			set
			{
				base[TraceSection._writeToDiagnosticTrace] = value;
			}
		}

		// Token: 0x0400301E RID: 12318
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x0400301F RID: 12319
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04003020 RID: 12320
		private static readonly ConfigurationProperty _propLocalOnly = new ConfigurationProperty("localOnly", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04003021 RID: 12321
		private static readonly ConfigurationProperty _propMostRecent = new ConfigurationProperty("mostRecent", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04003022 RID: 12322
		private static readonly ConfigurationProperty _propPageOutput = new ConfigurationProperty("pageOutput", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04003023 RID: 12323
		private static readonly ConfigurationProperty _propRequestLimit = new ConfigurationProperty("requestLimit", typeof(int), 10, null, StdValidatorsAndConverters.PositiveIntegerValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04003024 RID: 12324
		private static readonly ConfigurationProperty _propMode = new ConfigurationProperty("traceMode", typeof(TraceDisplayMode), TraceDisplayMode.SortByTime, ConfigurationPropertyOptions.None);

		// Token: 0x04003025 RID: 12325
		private static readonly ConfigurationProperty _writeToDiagnosticTrace = new ConfigurationProperty("writeToDiagnosticsTrace", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
