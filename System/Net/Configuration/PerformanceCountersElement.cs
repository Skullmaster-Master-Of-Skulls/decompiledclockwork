using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000657 RID: 1623
	public sealed class PerformanceCountersElement : ConfigurationElement
	{
		// Token: 0x06003235 RID: 12853 RVA: 0x000D5E20 File Offset: 0x000D4E20
		public PerformanceCountersElement()
		{
			this.properties.Add(this.enabled);
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000D5E70 File Offset: 0x000D4E70
		// (set) Token: 0x06003237 RID: 12855 RVA: 0x000D5E83 File Offset: 0x000D4E83
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[this.enabled];
			}
			set
			{
				base[this.enabled] = value;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x000D5E97 File Offset: 0x000D4E97
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002F13 RID: 12051
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F14 RID: 12052
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
