using System;
using System.Configuration;
using System.Web.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x02000E7D RID: 3709
	public class ScriptCacheProviderManager
	{
		// Token: 0x06008C9B RID: 35995 RVA: 0x001FE96B File Offset: 0x001FCB6B
		static ScriptCacheProviderManager()
		{
			ScriptCacheProviderManager.Initialize();
		}

		// Token: 0x06008C9C RID: 35996 RVA: 0x001FE974 File Offset: 0x001FCB74
		private static void Initialize()
		{
			ScriptManagerConfigurationSection scriptManagerConfigurationSection = (ScriptManagerConfigurationSection)ConfigurationManager.GetSection("telerik.web.ui/radScriptManager");
			if (scriptManagerConfigurationSection == null)
			{
				return;
			}
			ScriptCacheProviderManager._providers = new WebResourceCacheProviderCollection();
			ProvidersHelper.InstantiateProviders(scriptManagerConfigurationSection.Providers, ScriptCacheProviderManager._providers, typeof(WebResourceCacheProvider));
			ScriptCacheProviderManager._providers.SetReadOnly();
			ScriptCacheProviderManager._defaultProvider = ScriptCacheProviderManager._providers[scriptManagerConfigurationSection.DefaultCacheProvider];
			if (ScriptCacheProviderManager._defaultProvider == null)
			{
				throw new Exception("defaultProvider");
			}
		}

		// Token: 0x17002C69 RID: 11369
		// (get) Token: 0x06008C9D RID: 35997 RVA: 0x001FE9EA File Offset: 0x001FCBEA
		// (set) Token: 0x06008C9E RID: 35998 RVA: 0x001FE9F1 File Offset: 0x001FCBF1
		public static WebResourceCacheProvider Provider
		{
			get
			{
				return ScriptCacheProviderManager._defaultProvider;
			}
			set
			{
				ScriptCacheProviderManager._defaultProvider = value;
				if (!ScriptCacheProviderManager._defaultProvider.IsInitialized)
				{
					ScriptCacheProviderManager._defaultProvider.Initialize();
				}
			}
		}

		// Token: 0x17002C6A RID: 11370
		// (get) Token: 0x06008C9F RID: 35999 RVA: 0x001FEA0F File Offset: 0x001FCC0F
		public static WebResourceCacheProviderCollection Providers
		{
			get
			{
				return ScriptCacheProviderManager._providers;
			}
		}

		// Token: 0x0400277A RID: 10106
		private static WebResourceCacheProvider _defaultProvider;

		// Token: 0x0400277B RID: 10107
		private static WebResourceCacheProviderCollection _providers;
	}
}
