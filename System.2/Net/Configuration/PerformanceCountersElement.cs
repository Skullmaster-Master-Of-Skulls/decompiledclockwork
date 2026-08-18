using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200033B RID: 827
	public sealed class PerformanceCountersElement : ConfigurationElement
	{
		// Token: 0x06001D8B RID: 7563 RVA: 0x0008C134 File Offset: 0x0008A334
		public PerformanceCountersElement()
		{
			this.properties.Add(this.enabled);
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x0008C184 File Offset: 0x0008A384
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x0008C197 File Offset: 0x0008A397
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

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0008C1AB File Offset: 0x0008A3AB
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C54 RID: 7252
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C55 RID: 7253
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
