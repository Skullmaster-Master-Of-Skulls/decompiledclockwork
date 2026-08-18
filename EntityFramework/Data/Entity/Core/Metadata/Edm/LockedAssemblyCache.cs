using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000517 RID: 1303
	internal class LockedAssemblyCache : IDisposable
	{
		// Token: 0x06003110 RID: 12560 RVA: 0x000EAB8E File Offset: 0x000E8D8E
		internal LockedAssemblyCache(object lockObject, Dictionary<Assembly, ImmutableAssemblyCacheEntry> globalAssemblyCache)
		{
			this._lockObject = lockObject;
			this._globalAssemblyCache = globalAssemblyCache;
			Monitor.Enter(this._lockObject);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000EABAF File Offset: 0x000E8DAF
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Monitor.Exit(this._lockObject);
			this._lockObject = null;
			this._globalAssemblyCache = null;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000EABD0 File Offset: 0x000E8DD0
		[Conditional("DEBUG")]
		private void AssertLockedByThisThread()
		{
			bool flag = false;
			Monitor.TryEnter(this._lockObject, ref flag);
			if (flag)
			{
				Monitor.Exit(this._lockObject);
			}
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000EABFA File Offset: 0x000E8DFA
		internal bool TryGetValue(Assembly assembly, out ImmutableAssemblyCacheEntry cacheEntry)
		{
			return this._globalAssemblyCache.TryGetValue(assembly, out cacheEntry);
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000EAC09 File Offset: 0x000E8E09
		internal void Add(Assembly assembly, ImmutableAssemblyCacheEntry assemblyCacheEntry)
		{
			this._globalAssemblyCache.Add(assembly, assemblyCacheEntry);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000EAC18 File Offset: 0x000E8E18
		internal void Clear()
		{
			this._globalAssemblyCache.Clear();
		}

		// Token: 0x0400128C RID: 4748
		private object _lockObject;

		// Token: 0x0400128D RID: 4749
		private Dictionary<Assembly, ImmutableAssemblyCacheEntry> _globalAssemblyCache;
	}
}
