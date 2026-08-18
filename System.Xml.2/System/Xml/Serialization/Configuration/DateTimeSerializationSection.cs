using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x020001CD RID: 461
	public sealed class DateTimeSerializationSection : ConfigurationSection
	{
		// Token: 0x06001F4F RID: 8015 RVA: 0x000AA234 File Offset: 0x000A8434
		public DateTimeSerializationSection()
		{
			this.properties.Add(this.mode);
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x000AA294 File Offset: 0x000A8494
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x000AA29C File Offset: 0x000A849C
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x000AA2AF File Offset: 0x000A84AF
		[ConfigurationProperty("mode", DefaultValue = DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip)]
		public DateTimeSerializationSection.DateTimeSerializationMode Mode
		{
			get
			{
				return (DateTimeSerializationSection.DateTimeSerializationMode)base[this.mode];
			}
			set
			{
				base[this.mode] = value;
			}
		}

		// Token: 0x04000D3A RID: 3386
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000D3B RID: 3387
		private readonly ConfigurationProperty mode = new ConfigurationProperty("mode", typeof(DateTimeSerializationSection.DateTimeSerializationMode), DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip, new EnumConverter(typeof(DateTimeSerializationSection.DateTimeSerializationMode)), null, ConfigurationPropertyOptions.None);

		// Token: 0x02000488 RID: 1160
		public enum DateTimeSerializationMode
		{
			// Token: 0x04001E08 RID: 7688
			Default,
			// Token: 0x04001E09 RID: 7689
			Roundtrip,
			// Token: 0x04001E0A RID: 7690
			Local
		}
	}
}
