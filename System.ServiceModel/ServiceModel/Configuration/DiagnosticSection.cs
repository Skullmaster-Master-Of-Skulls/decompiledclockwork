using System;
using System.Configuration;
using System.Security;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000617 RID: 1559
	public sealed class DiagnosticSection : ConfigurationSection
	{
		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x000E5479 File Offset: 0x000E3679
		// (set) Token: 0x06003BF5 RID: 15349 RVA: 0x000E548B File Offset: 0x000E368B
		[ConfigurationProperty("wmiProviderEnabled", DefaultValue = false)]
		public bool WmiProviderEnabled
		{
			get
			{
				return (bool)base["wmiProviderEnabled"];
			}
			set
			{
				base["wmiProviderEnabled"] = value;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000E549E File Offset: 0x000E369E
		[ConfigurationProperty("messageLogging", Options = ConfigurationPropertyOptions.None)]
		public MessageLoggingElement MessageLogging
		{
			get
			{
				return (MessageLoggingElement)base["messageLogging"];
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000E54B0 File Offset: 0x000E36B0
		[ConfigurationProperty("endToEndTracing", Options = ConfigurationPropertyOptions.None)]
		public EndToEndTracingElement EndToEndTracing
		{
			get
			{
				return (EndToEndTracingElement)base["endToEndTracing"];
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x000E54C2 File Offset: 0x000E36C2
		// (set) Token: 0x06003BF9 RID: 15353 RVA: 0x000E54D4 File Offset: 0x000E36D4
		[ConfigurationProperty("performanceCounters", DefaultValue = PerformanceCounterScope.Default)]
		[ServiceModelEnumValidator(typeof(PerformanceCounterScopeHelper))]
		public PerformanceCounterScope PerformanceCounters
		{
			get
			{
				return (PerformanceCounterScope)base["performanceCounters"];
			}
			set
			{
				base["performanceCounters"] = value;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x000E54E7 File Offset: 0x000E36E7
		// (set) Token: 0x06003BFB RID: 15355 RVA: 0x000E54F9 File Offset: 0x000E36F9
		[ConfigurationProperty("etwProviderId", DefaultValue = "{c651f5f6-1c0d-492e-8ae1-b4efd7c9d503}")]
		[StringValidator(MinLength = 32)]
		public string EtwProviderId
		{
			get
			{
				return (string)base["etwProviderId"];
			}
			set
			{
				base["etwProviderId"] = value;
			}
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x000E5507 File Offset: 0x000E3707
		internal static DiagnosticSection GetSection()
		{
			return (DiagnosticSection)ConfigurationHelpers.GetSection(ConfigurationStrings.DiagnosticSectionPath);
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x000E5518 File Offset: 0x000E3718
		[SecurityCritical]
		internal static DiagnosticSection UnsafeGetSection()
		{
			return (DiagnosticSection)ConfigurationHelpers.UnsafeGetSection(ConfigurationStrings.DiagnosticSectionPath);
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000E5529 File Offset: 0x000E3729
		[SecurityCritical]
		internal static DiagnosticSection UnsafeGetSectionNoTrace()
		{
			return (DiagnosticSection)ConfigurationHelpers.UnsafeGetSectionNoTrace(ConfigurationStrings.DiagnosticSectionPath);
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x000E553A File Offset: 0x000E373A
		internal bool IsEtwProviderIdFromConfigFile()
		{
			return base.ElementInformation.Properties["etwProviderId"].ValueOrigin > PropertyValueOrigin.Default;
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x000E555C File Offset: 0x000E375C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("wmiProviderEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("messageLogging", typeof(MessageLoggingElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("endToEndTracing", typeof(EndToEndTracingElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("performanceCounters", typeof(PerformanceCounterScope), PerformanceCounterScope.Default, null, new ServiceModelEnumValidator(typeof(PerformanceCounterScopeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("etwProviderId", typeof(string), "{c651f5f6-1c0d-492e-8ae1-b4efd7c9d503}", null, new StringValidator(32, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C78 RID: 11384
		private ConfigurationPropertyCollection properties;
	}
}
