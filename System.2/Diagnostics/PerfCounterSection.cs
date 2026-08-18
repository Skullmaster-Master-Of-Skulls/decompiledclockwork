using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200049F RID: 1183
	internal class PerfCounterSection : ConfigurationElement
	{
		// Token: 0x06002BEC RID: 11244 RVA: 0x000C6C74 File Offset: 0x000C4E74
		static PerfCounterSection()
		{
			PerfCounterSection._properties = new ConfigurationPropertyCollection();
			PerfCounterSection._properties.Add(PerfCounterSection._propFileMappingSize);
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x000C6CB3 File Offset: 0x000C4EB3
		[ConfigurationProperty("filemappingsize", DefaultValue = 524288)]
		public int FileMappingSize
		{
			get
			{
				return (int)base[PerfCounterSection._propFileMappingSize];
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000C6CC5 File Offset: 0x000C4EC5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerfCounterSection._properties;
			}
		}

		// Token: 0x040026A0 RID: 9888
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x040026A1 RID: 9889
		private static readonly ConfigurationProperty _propFileMappingSize = new ConfigurationProperty("filemappingsize", typeof(int), 524288, ConfigurationPropertyOptions.None);
	}
}
