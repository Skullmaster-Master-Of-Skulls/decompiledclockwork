using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BB RID: 1211
	internal class OcAssemblyCache
	{
		// Token: 0x06002C9E RID: 11422 RVA: 0x000D9A5C File Offset: 0x000D7C5C
		internal OcAssemblyCache()
		{
			this._conventionalOcCache = new Dictionary<Assembly, ImmutableAssemblyCacheEntry>();
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000D9A6F File Offset: 0x000D7C6F
		internal bool TryGetConventionalOcCacheFromAssemblyCache(Assembly assemblyToLookup, out ImmutableAssemblyCacheEntry cacheEntry)
		{
			cacheEntry = null;
			return this._conventionalOcCache.TryGetValue(assemblyToLookup, out cacheEntry);
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000D9A81 File Offset: 0x000D7C81
		internal void AddAssemblyToOcCacheFromAssemblyCache(Assembly assembly, ImmutableAssemblyCacheEntry cacheEntry)
		{
			if (this._conventionalOcCache.ContainsKey(assembly))
			{
				return;
			}
			this._conventionalOcCache.Add(assembly, cacheEntry);
		}

		// Token: 0x04001073 RID: 4211
		private readonly Dictionary<Assembly, ImmutableAssemblyCacheEntry> _conventionalOcCache;
	}
}
