using System;
using System.Web.Configuration;

namespace System.Web
{
	// Token: 0x020000F2 RID: 242
	public static class SiteMap
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x000296CC File Offset: 0x000278CC
		public static SiteMapNode CurrentNode
		{
			get
			{
				return SiteMap.Provider.CurrentNode;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x000296D8 File Offset: 0x000278D8
		public static bool Enabled
		{
			get
			{
				if (!SiteMap._configEnabledEvaluated)
				{
					SiteMapSection siteMap = RuntimeConfig.GetAppConfig().SiteMap;
					SiteMap._enabled = (siteMap != null && siteMap.Enabled);
					SiteMap._configEnabledEvaluated = true;
				}
				return SiteMap._enabled;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00029713 File Offset: 0x00027913
		public static SiteMapProvider Provider
		{
			get
			{
				SiteMap.Initialize();
				return SiteMap._provider;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0002971F File Offset: 0x0002791F
		public static SiteMapProviderCollection Providers
		{
			get
			{
				SiteMap.Initialize();
				return SiteMap._providers;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0002972C File Offset: 0x0002792C
		public static SiteMapNode RootNode
		{
			get
			{
				SiteMapProvider rootProvider = SiteMap.Provider.RootProvider;
				SiteMapNode rootNode = rootProvider.RootNode;
				if (rootNode == null)
				{
					string name = rootProvider.Name;
					throw new InvalidOperationException(SR.GetString("SiteMapProvider_Invalid_RootNode", new object[]
					{
						name
					}));
				}
				return rootNode;
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000E83 RID: 3715 RVA: 0x00029770 File Offset: 0x00027970
		// (remove) Token: 0x06000E84 RID: 3716 RVA: 0x0002977D File Offset: 0x0002797D
		public static event SiteMapResolveEventHandler SiteMapResolve
		{
			add
			{
				SiteMap.Provider.SiteMapResolve += value;
			}
			remove
			{
				SiteMap.Provider.SiteMapResolve -= value;
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0002978C File Offset: 0x0002798C
		private static void Initialize()
		{
			if (SiteMap._providers != null)
			{
				return;
			}
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
			object lockObject = SiteMap._lockObject;
			lock (lockObject)
			{
				if (SiteMap._providers == null)
				{
					SiteMapSection siteMap = RuntimeConfig.GetAppConfig().SiteMap;
					if (siteMap == null)
					{
						SiteMap._providers = new SiteMapProviderCollection();
					}
					else
					{
						if (!siteMap.Enabled)
						{
							throw new InvalidOperationException(SR.GetString("SiteMap_feature_disabled", new object[]
							{
								"system.web/siteMap"
							}));
						}
						siteMap.ValidateDefaultProvider();
						SiteMap._providers = siteMap.ProvidersInternal;
						SiteMap._provider = SiteMap._providers[siteMap.DefaultProvider];
						SiteMap._providers.SetReadOnly();
					}
				}
			}
		}

		// Token: 0x040005A1 RID: 1441
		internal const string SectionName = "system.web/siteMap";

		// Token: 0x040005A2 RID: 1442
		private static SiteMapProviderCollection _providers;

		// Token: 0x040005A3 RID: 1443
		private static SiteMapProvider _provider;

		// Token: 0x040005A4 RID: 1444
		private static object _lockObject = new object();

		// Token: 0x040005A5 RID: 1445
		private static bool _configEnabledEvaluated;

		// Token: 0x040005A6 RID: 1446
		private static bool _enabled;
	}
}
