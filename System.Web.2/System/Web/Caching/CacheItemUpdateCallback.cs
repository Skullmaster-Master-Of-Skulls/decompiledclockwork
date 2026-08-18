using System;

namespace System.Web.Caching
{
	// Token: 0x02000878 RID: 2168
	// (Invoke) Token: 0x06006617 RID: 26135
	public delegate void CacheItemUpdateCallback(string key, CacheItemUpdateReason reason, out object expensiveObject, out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration);
}
