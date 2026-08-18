using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200077E RID: 1918
	public sealed class XhtmlConformanceSection : ConfigurationSection
	{
		// Token: 0x06005C35 RID: 23605 RVA: 0x0013F401 File Offset: 0x0013D601
		static XhtmlConformanceSection()
		{
			XhtmlConformanceSection._properties = new ConfigurationPropertyCollection();
			XhtmlConformanceSection._properties.Add(XhtmlConformanceSection._propMode);
		}

		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x06005C36 RID: 23606 RVA: 0x0013F43C File Offset: 0x0013D63C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return XhtmlConformanceSection._properties;
			}
		}

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x06005C37 RID: 23607 RVA: 0x0013F443 File Offset: 0x0013D643
		// (set) Token: 0x06005C38 RID: 23608 RVA: 0x0013F455 File Offset: 0x0013D655
		[ConfigurationProperty("mode", DefaultValue = XhtmlConformanceMode.Transitional)]
		public XhtmlConformanceMode Mode
		{
			get
			{
				return (XhtmlConformanceMode)base[XhtmlConformanceSection._propMode];
			}
			set
			{
				base[XhtmlConformanceSection._propMode] = value;
			}
		}

		// Token: 0x04003083 RID: 12419
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04003084 RID: 12420
		internal const XhtmlConformanceMode DefaultMode = XhtmlConformanceMode.Transitional;

		// Token: 0x04003085 RID: 12421
		private static readonly ConfigurationProperty _propMode = new ConfigurationProperty("mode", typeof(XhtmlConformanceMode), XhtmlConformanceMode.Transitional, ConfigurationPropertyOptions.None);
	}
}
