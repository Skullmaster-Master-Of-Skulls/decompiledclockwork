using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Runtime.Caching.Hosting;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007D5 RID: 2005
	internal sealed class ObjectCacheHost : IServiceProvider, IApplicationIdentifier, IFileChangeNotificationSystem, IMemoryCacheManager
	{
		// Token: 0x06006025 RID: 24613 RVA: 0x0014C3FC File Offset: 0x0014A5FC
		object IServiceProvider.GetService(Type service)
		{
			if (service == typeof(IFileChangeNotificationSystem))
			{
				return this;
			}
			if (service == typeof(IMemoryCacheManager))
			{
				return this;
			}
			if (service == typeof(IApplicationIdentifier))
			{
				return this;
			}
			return null;
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x0014C43B File Offset: 0x0014A63B
		string IApplicationIdentifier.GetApplicationId()
		{
			return HttpRuntime.AppDomainAppId;
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x0014C444 File Offset: 0x0014A644
		void IFileChangeNotificationSystem.StartMonitoring(string filePath, OnChangedCallback onChangedCallback, out object state, out DateTimeOffset lastWrite, out long fileSize)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			if (onChangedCallback == null)
			{
				throw new ArgumentNullException("onChangedCallback");
			}
			ObjectCacheHost.FileChangeEventTarget fileChangeEventTarget = new ObjectCacheHost.FileChangeEventTarget(onChangedCallback);
			FileAttributesData nonExistantAttributesData;
			HttpRuntime.FileChangesMonitor.StartMonitoringPath(filePath, fileChangeEventTarget.Handler, out nonExistantAttributesData);
			if (nonExistantAttributesData == null)
			{
				nonExistantAttributesData = FileAttributesData.NonExistantAttributesData;
			}
			state = fileChangeEventTarget;
			lastWrite = nonExistantAttributesData.UtcLastWriteTime;
			fileSize = nonExistantAttributesData.FileSize;
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x0014C4AF File Offset: 0x0014A6AF
		void IFileChangeNotificationSystem.StopMonitoring(string filePath, object state)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			if (state == null)
			{
				throw new ArgumentNullException("state");
			}
			HttpRuntime.FileChangesMonitor.StopMonitoringPath(filePath, state);
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x0014C4DC File Offset: 0x0014A6DC
		void IMemoryCacheManager.ReleaseCache(MemoryCache memoryCache)
		{
			if (memoryCache == null)
			{
				throw new ArgumentNullException("memoryCache");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._cacheInfos != null)
				{
					ObjectCacheHost.MemoryCacheInfo memoryCacheInfo = null;
					if (this._cacheInfos.TryGetValue(memoryCache, out memoryCacheInfo))
					{
						this._cacheInfos.Remove(memoryCache);
					}
				}
			}
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x0014C54C File Offset: 0x0014A74C
		void IMemoryCacheManager.UpdateCacheSize(long size, MemoryCache memoryCache)
		{
			if (memoryCache == null)
			{
				throw new ArgumentNullException("memoryCache");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._cacheInfos == null)
				{
					this._cacheInfos = new Dictionary<MemoryCache, ObjectCacheHost.MemoryCacheInfo>();
				}
				ObjectCacheHost.MemoryCacheInfo memoryCacheInfo = null;
				if (!this._cacheInfos.TryGetValue(memoryCache, out memoryCacheInfo))
				{
					memoryCacheInfo = new ObjectCacheHost.MemoryCacheInfo();
					memoryCacheInfo.Cache = memoryCache;
					this._cacheInfos[memoryCache] = memoryCacheInfo;
				}
				memoryCacheInfo.Size = size;
			}
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x0014C5DC File Offset: 0x0014A7DC
		internal long TrimCache(int percent)
		{
			long num = 0L;
			MemoryCache[] array = null;
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._cacheInfos != null && this._cacheInfos.Count > 0)
				{
					array = new MemoryCache[this._cacheInfos.Keys.Count];
					this._cacheInfos.Keys.CopyTo(array, 0);
				}
			}
			if (array != null)
			{
				foreach (MemoryCache memoryCache in array)
				{
					num += memoryCache.Trim(percent);
				}
			}
			return num;
		}

		// Token: 0x04003247 RID: 12871
		private object _lock = new object();

		// Token: 0x04003248 RID: 12872
		private Dictionary<MemoryCache, ObjectCacheHost.MemoryCacheInfo> _cacheInfos;

		// Token: 0x02000A64 RID: 2660
		internal sealed class FileChangeEventTarget
		{
			// Token: 0x06006EFE RID: 28414 RVA: 0x0018B4EC File Offset: 0x001896EC
			private void OnChanged(object sender, FileChangeEvent e)
			{
				this._onChangedCallback(null);
			}

			// Token: 0x17001E43 RID: 7747
			// (get) Token: 0x06006EFF RID: 28415 RVA: 0x0018B4FA File Offset: 0x001896FA
			internal FileChangeEventHandler Handler
			{
				get
				{
					return this._handler;
				}
			}

			// Token: 0x06006F00 RID: 28416 RVA: 0x0018B502 File Offset: 0x00189702
			internal FileChangeEventTarget(OnChangedCallback onChangedCallback)
			{
				this._onChangedCallback = onChangedCallback;
				this._handler = new FileChangeEventHandler(this.OnChanged);
			}

			// Token: 0x04003B8D RID: 15245
			private OnChangedCallback _onChangedCallback;

			// Token: 0x04003B8E RID: 15246
			private FileChangeEventHandler _handler;
		}

		// Token: 0x02000A65 RID: 2661
		internal sealed class MemoryCacheInfo
		{
			// Token: 0x04003B8F RID: 15247
			internal MemoryCache Cache;

			// Token: 0x04003B90 RID: 15248
			internal long Size;
		}
	}
}
