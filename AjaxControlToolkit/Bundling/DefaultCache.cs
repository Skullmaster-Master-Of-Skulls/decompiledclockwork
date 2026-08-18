using System;
using System.Web;
using System.Web.Caching;

namespace AjaxControlToolkit.Bundling
{
	// Token: 0x0200005F RID: 95
	public class DefaultCache : ICache
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000A4DF File Offset: 0x000086DF
		public void Set(string key, object value)
		{
			HttpRuntime.Cache[key] = value;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000A4ED File Offset: 0x000086ED
		public T Get<T>(string key) where T : class
		{
			return HttpRuntime.Cache[key] as T;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000A504 File Offset: 0x00008704
		public void Remove(string key)
		{
			HttpRuntime.Cache.Remove(key);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000A512 File Offset: 0x00008712
		public void Set(string key, object value, string fileCacheDependencyName)
		{
			HttpRuntime.Cache.Insert(key, value, new CacheDependency(fileCacheDependencyName));
		}
	}
}
