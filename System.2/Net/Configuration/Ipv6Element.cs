using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000336 RID: 822
	public sealed class Ipv6Element : ConfigurationElement
	{
		// Token: 0x06001D75 RID: 7541 RVA: 0x0008BF40 File Offset: 0x0008A140
		public Ipv6Element()
		{
			this.properties.Add(this.enabled);
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0008BF90 File Offset: 0x0008A190
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x0008BF98 File Offset: 0x0008A198
		// (set) Token: 0x06001D78 RID: 7544 RVA: 0x0008BFAB File Offset: 0x0008A1AB
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

		// Token: 0x04001C4F RID: 7247
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C50 RID: 7248
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
