using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x020001D2 RID: 466
	public sealed class XmlSerializerSection : ConfigurationSection
	{
		// Token: 0x06001F73 RID: 8051 RVA: 0x000AA87C File Offset: 0x000A8A7C
		public XmlSerializerSection()
		{
			this.properties.Add(this.checkDeserializeAdvances);
			this.properties.Add(this.tempFilesLocation);
			this.properties.Add(this.useLegacySerializerGeneration);
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x000AA931 File Offset: 0x000A8B31
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x000AA939 File Offset: 0x000A8B39
		// (set) Token: 0x06001F76 RID: 8054 RVA: 0x000AA94C File Offset: 0x000A8B4C
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

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x000AA960 File Offset: 0x000A8B60
		// (set) Token: 0x06001F78 RID: 8056 RVA: 0x000AA973 File Offset: 0x000A8B73
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

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x000AA982 File Offset: 0x000A8B82
		// (set) Token: 0x06001F7A RID: 8058 RVA: 0x000AA995 File Offset: 0x000A8B95
		[ConfigurationProperty("useLegacySerializerGeneration", DefaultValue = false)]
		public bool UseLegacySerializerGeneration
		{
			get
			{
				return (bool)base[this.useLegacySerializerGeneration];
			}
			set
			{
				base[this.useLegacySerializerGeneration] = value;
			}
		}

		// Token: 0x04000D41 RID: 3393
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000D42 RID: 3394
		private readonly ConfigurationProperty checkDeserializeAdvances = new ConfigurationProperty("checkDeserializeAdvances", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04000D43 RID: 3395
		private readonly ConfigurationProperty tempFilesLocation = new ConfigurationProperty("tempFilesLocation", typeof(string), null, null, new RootedPathValidator(), ConfigurationPropertyOptions.None);

		// Token: 0x04000D44 RID: 3396
		private readonly ConfigurationProperty useLegacySerializerGeneration = new ConfigurationProperty("useLegacySerializerGeneration", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
