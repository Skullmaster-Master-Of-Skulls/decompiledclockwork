using System;
using System.Configuration;

namespace System.Drawing.Configuration
{
	// Token: 0x02000084 RID: 132
	public sealed class SystemDrawingSection : ConfigurationSection
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x00021FC4 File Offset: 0x000201C4
		static SystemDrawingSection()
		{
			SystemDrawingSection.properties.Add(SystemDrawingSection.bitmapSuffix);
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00021FFA File Offset: 0x000201FA
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x0002200C File Offset: 0x0002020C
		[ConfigurationProperty("bitmapSuffix")]
		public string BitmapSuffix
		{
			get
			{
				return (string)base[SystemDrawingSection.bitmapSuffix];
			}
			set
			{
				base[SystemDrawingSection.bitmapSuffix] = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0002201A File Offset: 0x0002021A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SystemDrawingSection.properties;
			}
		}

		// Token: 0x0400071D RID: 1821
		private const string BitmapSuffixSectionName = "bitmapSuffix";

		// Token: 0x0400071E RID: 1822
		private static readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400071F RID: 1823
		private static readonly ConfigurationProperty bitmapSuffix = new ConfigurationProperty("bitmapSuffix", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}
