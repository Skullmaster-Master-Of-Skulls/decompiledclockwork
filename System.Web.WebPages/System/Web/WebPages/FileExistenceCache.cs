using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Web.Hosting;

namespace System.Web.WebPages
{
	// Token: 0x02000037 RID: 55
	internal class FileExistenceCache
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000541C File Offset: 0x0000361C
		public FileExistenceCache(VirtualPathProvider virtualPathProvider, int milliSecondsBeforeReset = 1000) : this(() => virtualPathProvider, milliSecondsBeforeReset)
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005464 File Offset: 0x00003664
		public FileExistenceCache(Func<VirtualPathProvider> virtualPathProviderFunc, int milliSecondsBeforeReset = 1000)
		{
			this._virtualPathProviderFunc = virtualPathProviderFunc;
			this._virtualPathFileExists = ((string path) => this._virtualPathProviderFunc().FileExists(path));
			this._ticksBeforeReset = milliSecondsBeforeReset * 10000;
			this.Reset();
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000054AA File Offset: 0x000036AA
		public VirtualPathProvider VirtualPathProvider
		{
			get
			{
				return this._virtualPathProviderFunc();
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000054B7 File Offset: 0x000036B7
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000054C5 File Offset: 0x000036C5
		public int MilliSecondsBeforeReset
		{
			get
			{
				return this._ticksBeforeReset / 10000;
			}
			internal set
			{
				this._ticksBeforeReset = value * 10000;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000054D4 File Offset: 0x000036D4
		internal IDictionary<string, bool> CacheInternal
		{
			get
			{
				return this._cache;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000054DC File Offset: 0x000036DC
		public bool TimeExceeded
		{
			get
			{
				return DateTime.UtcNow.Ticks - Interlocked.Read(ref this._creationTick) > (long)this._ticksBeforeReset;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000550C File Offset: 0x0000370C
		public void Reset()
		{
			this._cache = new ConcurrentDictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			long ticks = DateTime.UtcNow.Ticks;
			Interlocked.Exchange(ref this._creationTick, ticks);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005544 File Offset: 0x00003744
		public bool FileExists(string virtualPath)
		{
			if (this.TimeExceeded)
			{
				this.Reset();
			}
			return this._cache.GetOrAdd(virtualPath, this._virtualPathFileExists);
		}

		// Token: 0x0400007A RID: 122
		private const int TicksPerMillisecond = 10000;

		// Token: 0x0400007B RID: 123
		private readonly Func<VirtualPathProvider> _virtualPathProviderFunc;

		// Token: 0x0400007C RID: 124
		private readonly Func<string, bool> _virtualPathFileExists;

		// Token: 0x0400007D RID: 125
		private ConcurrentDictionary<string, bool> _cache;

		// Token: 0x0400007E RID: 126
		private long _creationTick;

		// Token: 0x0400007F RID: 127
		private int _ticksBeforeReset;
	}
}
