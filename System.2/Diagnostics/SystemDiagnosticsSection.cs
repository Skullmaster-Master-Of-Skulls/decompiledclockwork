using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004AB RID: 1195
	internal class SystemDiagnosticsSection : ConfigurationSection
	{
		// Token: 0x06002C43 RID: 11331 RVA: 0x000C7A3C File Offset: 0x000C5C3C
		static SystemDiagnosticsSection()
		{
			SystemDiagnosticsSection._properties = new ConfigurationPropertyCollection();
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propAssert);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propPerfCounters);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSources);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSharedListeners);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSwitches);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propTrace);
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x000C7B67 File Offset: 0x000C5D67
		[ConfigurationProperty("assert")]
		public AssertSection Assert
		{
			get
			{
				return (AssertSection)base[SystemDiagnosticsSection._propAssert];
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000C7B79 File Offset: 0x000C5D79
		[ConfigurationProperty("performanceCounters")]
		public PerfCounterSection PerfCounters
		{
			get
			{
				return (PerfCounterSection)base[SystemDiagnosticsSection._propPerfCounters];
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x000C7B8B File Offset: 0x000C5D8B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SystemDiagnosticsSection._properties;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000C7B92 File Offset: 0x000C5D92
		[ConfigurationProperty("sources")]
		public SourceElementsCollection Sources
		{
			get
			{
				return (SourceElementsCollection)base[SystemDiagnosticsSection._propSources];
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002C48 RID: 11336 RVA: 0x000C7BA4 File Offset: 0x000C5DA4
		[ConfigurationProperty("sharedListeners")]
		public ListenerElementsCollection SharedListeners
		{
			get
			{
				return (ListenerElementsCollection)base[SystemDiagnosticsSection._propSharedListeners];
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x000C7BB6 File Offset: 0x000C5DB6
		[ConfigurationProperty("switches")]
		public SwitchElementsCollection Switches
		{
			get
			{
				return (SwitchElementsCollection)base[SystemDiagnosticsSection._propSwitches];
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000C7BC8 File Offset: 0x000C5DC8
		[ConfigurationProperty("trace")]
		public TraceSection Trace
		{
			get
			{
				return (TraceSection)base[SystemDiagnosticsSection._propTrace];
			}
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000C7BDA File Offset: 0x000C5DDA
		protected override void InitializeDefault()
		{
			this.Trace.Listeners.InitializeDefaultInternal();
		}

		// Token: 0x040026C7 RID: 9927
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x040026C8 RID: 9928
		private static readonly ConfigurationProperty _propAssert = new ConfigurationProperty("assert", typeof(AssertSection), new AssertSection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026C9 RID: 9929
		private static readonly ConfigurationProperty _propPerfCounters = new ConfigurationProperty("performanceCounters", typeof(PerfCounterSection), new PerfCounterSection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026CA RID: 9930
		private static readonly ConfigurationProperty _propSources = new ConfigurationProperty("sources", typeof(SourceElementsCollection), new SourceElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026CB RID: 9931
		private static readonly ConfigurationProperty _propSharedListeners = new ConfigurationProperty("sharedListeners", typeof(SharedListenerElementsCollection), new SharedListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026CC RID: 9932
		private static readonly ConfigurationProperty _propSwitches = new ConfigurationProperty("switches", typeof(SwitchElementsCollection), new SwitchElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026CD RID: 9933
		private static readonly ConfigurationProperty _propTrace = new ConfigurationProperty("trace", typeof(TraceSection), new TraceSection(), ConfigurationPropertyOptions.None);
	}
}
