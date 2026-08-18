using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000218 RID: 536
	internal class LockedAssemblyCache : IDisposable
	{
		// Token: 0x06002322 RID: 8994 RVA: 0x0007CC9C File Offset: 0x0007AE9C
		internal LockedAssemblyCache(object lockObject, Dictionary<Assembly, ImmutableAssemblyCacheEntry> globalAssemblyCache)
		{
			this._lockObject = lockObject;
			this._globalAssemblyCache = globalAssemblyCache;
			Monitor.Enter(this._lockObject);
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x0007CCBD File Offset: 0x0007AEBD
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Monitor.Exit(this._lockObject);
			this._lockObject = null;
			this._globalAssemblyCache = null;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0007CCE0 File Offset: 0x0007AEE0
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

		// Token: 0x06002325 RID: 8997 RVA: 0x0007CD0A File Offset: 0x0007AF0A
		internal bool TryGetValue(Assembly assembly, out ImmutableAssemblyCacheEntry cacheEntry)
		{
			return this._globalAssemblyCache.TryGetValue(assembly, out cacheEntry);
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x0007CD19 File Offset: 0x0007AF19
		internal void Add(Assembly assembly, ImmutableAssemblyCacheEntry assemblyCacheEntry)
		{
			this._globalAssemblyCache.Add(assembly, assemblyCacheEntry);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0007CD28 File Offset: 0x0007AF28
		internal void Clear()
		{
			this._globalAssemblyCache.Clear();
		}

		// Token: 0x04000FA0 RID: 4000
		private object _lockObject;

		// Token: 0x04000FA1 RID: 4001
		private Dictionary<Assembly, ImmutableAssemblyCacheEntry> _globalAssemblyCache;
	}
}
