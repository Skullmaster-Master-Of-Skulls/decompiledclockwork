using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;

namespace System.Web.Optimization
{
	// Token: 0x02000007 RID: 7
	internal class HttpContextCache : IBundleCache
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000022C0 File Offset: 0x000004C0
		public bool IsEnabled(BundleContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return context.HttpContext != null && context.HttpContext.Cache != null && !context.EnableInstrumentation;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022F0 File Offset: 0x000004F0
		public BundleResponse Get(BundleContext context, Bundle bundle)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return context.HttpContext.Cache[bundle.GetCacheKey(context)] as BundleResponse;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000232C File Offset: 0x0000052C
		public void Put(BundleContext context, Bundle bundle, BundleResponse response)
		{
			List<string> list = new List<string>();
			list.AddRange(from f in response.Files
			select f.VirtualFile.VirtualPath);
			list.AddRange(context.CacheDependencyDirectories);
			string cacheKey = bundle.GetCacheKey(context);
			CacheDependency cacheDependency = context.VirtualPathProvider.GetCacheDependency(context.BundleVirtualPath, list, DateTime.UtcNow);
			context.HttpContext.Cache.Insert(cacheKey, response, cacheDependency);
			bundle.CacheKeys.Add(cacheKey);
		}
	}
}
