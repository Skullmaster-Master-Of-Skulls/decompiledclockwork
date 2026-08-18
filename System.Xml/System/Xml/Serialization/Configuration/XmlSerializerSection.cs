using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x02000356 RID: 854
	public sealed class XmlSerializerSection : ConfigurationSection
	{
		// Token: 0x06002946 RID: 10566 RVA: 0x000D39BC File Offset: 0x000D29BC
		public XmlSerializerSection()
		{
			this.properties.Add(this.checkDeserializeAdvances);
			this.properties.Add(this.tempFilesLocation);
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000D3A3F File Offset: 0x000D2A3F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x000D3A47 File Offset: 0x000D2A47
		// (set) Token: 0x06002949 RID: 10569 RVA: 0x000D3A5A File Offset: 0x000D2A5A
		[ConfigurationProperty("checkDeserializeAdvances", DefaultValue = false)]
		public bool CheckDeserializeAdvances
		{
			get
			{
				return (bool)base[this.checkDeserializeAdvances];
			}
			set
			{
				base[this.checkDeserializeAdvances] = value;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x000D3A6E File Offset: 0x000D2A6E
		// (set) Token: 0x0600294B RID: 10571 RVA: 0x000D3A81 File Offset: 0x000D2A81
		[ConfigurationProperty("tempFilesLocation", DefaultValue = null)]
		public string TempFilesLocation
		{
			get
			{
				return (string)base[this.tempFilesLocation];
			}
			set
			{
				base[this.tempFilesLocation] = value;
			}
		}

		// Token: 0x040016E7 RID: 5863
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040016E8 RID: 5864
		private readonly ConfigurationProperty checkDeserializeAdvances = new ConfigurationProperty("checkDeserializeAdvances", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x040016E9 RID: 5865
		private readonly ConfigurationProperty tempFilesLocation = new ConfigurationProperty("tempFilesLocation", typeof(string), null, null, new RootedPathValidator(), ConfigurationPropertyOptions.None);
	}
}
