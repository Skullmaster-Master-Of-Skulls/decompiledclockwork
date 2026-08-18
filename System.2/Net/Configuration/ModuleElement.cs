using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000339 RID: 825
	public sealed class ModuleElement : ConfigurationElement
	{
		// Token: 0x06001D7E RID: 7550 RVA: 0x0008C000 File Offset: 0x0008A200
		public ModuleElement()
		{
			this.properties.Add(this.type);
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0008C040 File Offset: 0x0008A240
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0008C048 File Offset: 0x0008A248
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x0008C05B File Offset: 0x0008A25B
		[ConfigurationProperty("type")]
		public string Type
		{
			get
			{
				return (string)base[this.type];
			}
			set
			{
				base[this.type] = value;
			}
		}

		// Token: 0x04001C52 RID: 7250
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C53 RID: 7251
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}
