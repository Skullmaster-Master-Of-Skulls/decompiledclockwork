using System;
using System.Configuration;

namespace System.ServiceModel.Activation.Configuration
{
	// Token: 0x020005D0 RID: 1488
	public sealed class DiagnosticSection : ConfigurationSection
	{
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000DF73C File Offset: 0x000DD93C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("performanceCountersEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000DF790 File Offset: 0x000DD990
		internal static DiagnosticSection GetSection()
		{
			DiagnosticSection diagnosticSection = (DiagnosticSection)ConfigurationManager.GetSection(ConfigurationStrings.DiagnosticSectionPath);
			if (diagnosticSection == null)
			{
				diagnosticSection = new DiagnosticSection();
			}
			return diagnosticSection;
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000DF7B7 File Offset: 0x000DD9B7
		// (set) Token: 0x060039D9 RID: 14809 RVA: 0x000DF7C9 File Offset: 0x000DD9C9
		[ConfigurationProperty("performanceCountersEnabled", DefaultValue = true)]
		public bool PerformanceCountersEnabled
		{
			get
			{
				return (bool)base["performanceCountersEnabled"];
			}
			set
			{
				base["performanceCountersEnabled"] = value;
			}
		}

		// Token: 0x04002A31 RID: 10801
		private ConfigurationPropertyCollection properties;
	}
}
