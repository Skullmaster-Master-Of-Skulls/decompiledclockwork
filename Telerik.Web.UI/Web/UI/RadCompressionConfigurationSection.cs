using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x02001831 RID: 6193
	public class RadCompressionConfigurationSection : ConfigurationSection
	{
		// Token: 0x170048C5 RID: 18629
		// (get) Token: 0x0600F0CD RID: 61645 RVA: 0x0036BAA4 File Offset: 0x00369CA4
		[ConfigurationProperty("enablePostbackCompression", DefaultValue = "false", IsRequired = false)]
		public bool EnablePostbackCompression
		{
			get
			{
				return base["enablePostbackCompression"] != null && (bool)base["enablePostbackCompression"];
			}
		}

		// Token: 0x170048C6 RID: 18630
		// (get) Token: 0x0600F0CE RID: 61646 RVA: 0x0036BAC5 File Offset: 0x00369CC5
		[ConfigurationProperty("enableTracing", DefaultValue = "false", IsRequired = false)]
		public bool EnableTracing
		{
			get
			{
				return base["enableTracing"] != null && (bool)base["enableTracing"];
			}
		}

		// Token: 0x170048C7 RID: 18631
		// (get) Token: 0x0600F0CF RID: 61647 RVA: 0x0036BAE6 File Offset: 0x00369CE6
		[ConfigurationProperty("excludeHandlers")]
		public RadCompressionExcludeSettingCollection ExcludeHandlers
		{
			get
			{
				return (RadCompressionExcludeSettingCollection)base["excludeHandlers"];
			}
		}
	}
}
