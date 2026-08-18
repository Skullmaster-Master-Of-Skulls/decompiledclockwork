using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001BA RID: 442
	internal class OcAssemblyCache
	{
		// Token: 0x06001EFD RID: 7933 RVA: 0x0006D5ED File Offset: 0x0006B7ED
		internal OcAssemblyCache()
		{
			this._conventionalOcCache = new Dictionary<Assembly, ImmutableAssemblyCacheEntry>();
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0006D600 File Offset: 0x0006B800
		internal bool TryGetConventionalOcCacheFromAssemblyCache(Assembly assemblyToLookup, out ImmutableAssemblyCacheEntry cacheEntry)
		{
			cacheEntry = null;
			return this._conventionalOcCache.TryGetValue(assemblyToLookup, out cacheEntry);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0006D612 File Offset: 0x0006B812
		internal void AddAssemblyToOcCacheFromAssemblyCache(Assembly assembly, ImmutableAssemblyCacheEntry cacheEntry)
		{
			if (this._conventionalOcCache.ContainsKey(assembly))
			{
				return;
			}
			this._conventionalOcCache.Add(assembly, cacheEntry);
		}

		// Token: 0x04000D04 RID: 3332
		private Dictionary<Assembly, ImmutableAssemblyCacheEntry> _conventionalOcCache;
	}
}
