using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000652 RID: 1618
	public sealed class Ipv6Element : ConfigurationElement
	{
		// Token: 0x0600321F RID: 12831 RVA: 0x000D5C2C File Offset: 0x000D4C2C
		public Ipv6Element()
		{
			this.properties.Add(this.enabled);
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000D5C7C File Offset: 0x000D4C7C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x000D5C84 File Offset: 0x000D4C84
		// (set) Token: 0x06003222 RID: 12834 RVA: 0x000D5C97 File Offset: 0x000D4C97
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

		// Token: 0x04002F0E RID: 12046
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F0F RID: 12047
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
