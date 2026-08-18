using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200075A RID: 1882
	public sealed class SystemWebCachingSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x06005AB4 RID: 23220 RVA: 0x0013BB6C File Offset: 0x00139D6C
		[ConfigurationProperty("cache")]
		public CacheSection Cache
		{
			get
			{
				return (CacheSection)base.Sections["cache"];
			}
		}

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x0013BB83 File Offset: 0x00139D83
		[ConfigurationProperty("outputCache")]
		public OutputCacheSection OutputCache
		{
			get
			{
				return (OutputCacheSection)base.Sections["outputCache"];
			}
		}

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x06005AB6 RID: 23222 RVA: 0x0013BB9A File Offset: 0x00139D9A
		[ConfigurationProperty("outputCacheSettings")]
		public OutputCacheSettingsSection OutputCacheSettings
		{
			get
			{
				return (OutputCacheSettingsSection)base.Sections["outputCacheSettings"];
			}
		}

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x06005AB7 RID: 23223 RVA: 0x0013BBB1 File Offset: 0x00139DB1
		[ConfigurationProperty("sqlCacheDependency")]
		public SqlCacheDependencySection SqlCacheDependency
		{
			get
			{
				return (SqlCacheDependencySection)base.Sections["sqlCacheDependency"];
			}
		}
	}
}
