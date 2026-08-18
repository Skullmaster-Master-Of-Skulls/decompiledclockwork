using System;
using System.Collections.Specialized;
using System.Configuration;

namespace System.Data.Entity.Util
{
	// Token: 0x02000128 RID: 296
	internal static class AppSettings
	{
		// Token: 0x06001566 RID: 5478 RVA: 0x000488D4 File Offset: 0x00046AD4
		private static void EnsureSettingsLoaded()
		{
			if (!AppSettings._settingsInitialized)
			{
				object appSettingsLock = AppSettings._appSettingsLock;
				lock (appSettingsLock)
				{
					if (!AppSettings._settingsInitialized)
					{
						NameValueCollection nameValueCollection = null;
						try
						{
							nameValueCollection = ConfigurationManager.AppSettings;
						}
						finally
						{
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["EntityFramework_SimplifyLimitOperations"], out AppSettings._SimplifyLimitOperations))
							{
								AppSettings._SimplifyLimitOperations = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["EntityFramework_SimplifyUserSpecifiedViews"], out AppSettings._SimplifyUserSpecifiedViews))
							{
								AppSettings._SimplifyUserSpecifiedViews = true;
							}
							if (nameValueCollection == null || !int.TryParse(nameValueCollection["EntityFramework_QueryCacheSize"], out AppSettings._QueryCacheSize) || AppSettings._QueryCacheSize < 1)
							{
								AppSettings._QueryCacheSize = 1000;
							}
							AppSettings._settingsInitialized = true;
						}
					}
				}
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x000489B4 File Offset: 0x00046BB4
		internal static bool SimplifyLimitOperations
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._SimplifyLimitOperations;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x000489C0 File Offset: 0x00046BC0
		internal static bool SimplifyUserSpecifiedViews
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._SimplifyUserSpecifiedViews;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x000489CC File Offset: 0x00046BCC
		internal static int QueryCacheSize
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._QueryCacheSize;
			}
		}

		// Token: 0x04000A33 RID: 2611
		private static volatile bool _settingsInitialized = false;

		// Token: 0x04000A34 RID: 2612
		private static object _appSettingsLock = new object();

		// Token: 0x04000A35 RID: 2613
		private static bool _SimplifyLimitOperations = false;

		// Token: 0x04000A36 RID: 2614
		private static bool _SimplifyUserSpecifiedViews = true;

		// Token: 0x04000A37 RID: 2615
		private static int _QueryCacheSize;

		// Token: 0x04000A38 RID: 2616
		private const int DefaultQueryCacheSize = 1000;
	}
}
