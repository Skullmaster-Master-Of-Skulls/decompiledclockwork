using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x0200034E RID: 846
	public sealed class DateTimeSerializationSection : ConfigurationSection
	{
		// Token: 0x0600291A RID: 10522 RVA: 0x000D329C File Offset: 0x000D229C
		public DateTimeSerializationSection()
		{
			this.properties.Add(this.mode);
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x000D32FC File Offset: 0x000D22FC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x000D3304 File Offset: 0x000D2304
		// (set) Token: 0x0600291D RID: 10525 RVA: 0x000D3317 File Offset: 0x000D2317
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

		// Token: 0x040016DA RID: 5850
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040016DB RID: 5851
		private readonly ConfigurationProperty mode = new ConfigurationProperty("mode", typeof(DateTimeSerializationSection.DateTimeSerializationMode), DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip, new EnumConverter(typeof(DateTimeSerializationSection.DateTimeSerializationMode)), null, ConfigurationPropertyOptions.None);

		// Token: 0x0200034F RID: 847
		public enum DateTimeSerializationMode
		{
			// Token: 0x040016DD RID: 5853
			Default,
			// Token: 0x040016DE RID: 5854
			Roundtrip,
			// Token: 0x040016DF RID: 5855
			Local
		}
	}
}
