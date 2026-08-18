using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x0200087C RID: 2172
	public sealed class Cache : IEnumerable
	{
		// Token: 0x0600661A RID: 26138 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public Cache()
		{
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x000030B5 File Offset: 0x000012B5
		internal Cache(int dummy)
		{
		}

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x001678D0 File Offset: 0x00165AD0
		public int Count
		{
			get
			{
				return Convert.ToInt32(this.ObjectCache.ItemCount);
			}
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x001678E4 File Offset: 0x00165AE4
		internal CacheStoreProvider GetInternalCache(bool createIfDoesNotExist)
		{
			if (Cache._internalCache == null && createIfDoesNotExist)
			{
				lock (this)
				{
					if (Cache._internalCache == null)
					{
						NameValueCollection cacheStoreProviderSettings = HostingEnvironment.CacheStoreProviderSettings;
						if (cacheStoreProviderSettings != null)
						{
							string name = cacheStoreProviderSettings["name"];
							cacheStoreProviderSettings["isPublic"] = "false";
							Cache._internalCache = (CacheStoreProvider)ProvidersHelper.InstantiateProvider(cacheStoreProviderSettings, typeof(CacheStoreProvider));
							Cache._internalCache.Initialize(name, cacheStoreProviderSettings);
						}
						else
						{
							if (Cache._objectCache is AspNetCache)
							{
								Cache._internalCache = new AspNetCache((AspNetCache)Cache._objectCache, false);
							}
							else
							{
								Cache._internalCache = new AspNetCache(false);
							}
							Cache._internalCache.Initialize(null, new NameValueCollection());
						}
					}
				}
			}
			return Cache._internalCache;
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x001679C4 File Offset: 0x00165BC4
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal CacheStoreProvider GetObjectCache(bool createIfDoesNotExist)
		{
			if (Cache._objectCache == null && createIfDoesNotExist)
			{
				lock (this)
				{
					if (Cache._objectCache == null)
					{
						NameValueCollection cacheStoreProviderSettings = HostingEnvironment.CacheStoreProviderSettings;
						if (cacheStoreProviderSettings != null)
						{
							string name = cacheStoreProviderSettings["name"];
							cacheStoreProviderSettings["isPublic"] = "true";
							Cache._objectCache = (CacheStoreProvider)ProvidersHelper.InstantiateProvider(cacheStoreProviderSettings, typeof(CacheStoreProvider));
							Cache._objectCache.Initialize(name, cacheStoreProviderSettings);
						}
						else
						{
							if (Cache._internalCache is AspNetCache)
							{
								Cache._objectCache = new AspNetCache((AspNetCache)Cache._internalCache, true);
							}
							else
							{
								Cache._objectCache = new AspNetCache(true);
							}
							Cache._objectCache.Initialize(null, new NameValueCollection());
						}
					}
				}
			}
			return Cache._objectCache;
		}

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x0600661F RID: 26143 RVA: 0x00167AA4 File Offset: 0x00165CA4
		internal CacheStoreProvider InternalCache
		{
			get
			{
				return this.GetInternalCache(true);
			}
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x06006620 RID: 26144 RVA: 0x00167AAD File Offset: 0x00165CAD
		internal CacheStoreProvider ObjectCache
		{
			get
			{
				return this.GetObjectCache(true);
			}
		}

		// Token: 0x06006621 RID: 26145 RVA: 0x00167AB6 File Offset: 0x00165CB6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.ObjectCache.GetEnumerator();
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x00167AB6 File Offset: 0x00165CB6
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.ObjectCache.GetEnumerator();
		}

		// Token: 0x17001C9A RID: 7322
		public object this[string key]
		{
			get
			{
				return this.Get(key);
			}
			set
			{
				this.Insert(key, value);
			}
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x00167AD6 File Offset: 0x00165CD6
		public object Get(string key)
		{
			return this.ObjectCache.Get(key);
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x00167AE4 File Offset: 0x00165CE4
		public void Insert(string key, object value)
		{
			this.ObjectCache.Insert(key, value, null);
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x00167AF4 File Offset: 0x00165CF4
		public void Insert(string key, object value, CacheDependency dependencies)
		{
			this.ObjectCache.Insert(key, value, new CacheInsertOptions
			{
				Dependencies = dependencies
			});
		}

		// Token: 0x06006628 RID: 26152 RVA: 0x00167B10 File Offset: 0x00165D10
		public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
		{
			DateTime absoluteExpiration2 = DateTimeUtil.ConvertToUniversalTime(absoluteExpiration);
			this.ObjectCache.Insert(key, value, new CacheInsertOptions
			{
				Dependencies = dependencies,
				AbsoluteExpiration = absoluteExpiration2,
				SlidingExpiration = slidingExpiration
			});
		}

		// Token: 0x06006629 RID: 26153 RVA: 0x00167B50 File Offset: 0x00165D50
		public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
		{
			DateTime absoluteExpiration2 = DateTimeUtil.ConvertToUniversalTime(absoluteExpiration);
			this.ObjectCache.Insert(key, value, new CacheInsertOptions
			{
				Dependencies = dependencies,
				AbsoluteExpiration = absoluteExpiration2,
				SlidingExpiration = slidingExpiration,
				Priority = priority,
				OnRemovedCallback = onRemoveCallback
			});
		}

		// Token: 0x0600662A RID: 26154 RVA: 0x00167BA0 File Offset: 0x00165DA0
		public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
		{
			if (dependencies == null && absoluteExpiration == Cache.NoAbsoluteExpiration && slidingExpiration == Cache.NoSlidingExpiration)
			{
				throw new ArgumentException(SR.GetString("Invalid_Parameters_To_Insert"));
			}
			if (onUpdateCallback == null)
			{
				throw new ArgumentNullException("onUpdateCallback");
			}
			DateTime absoluteExpiration2 = DateTimeUtil.ConvertToUniversalTime(absoluteExpiration);
			this.ObjectCache.Insert(key, value, new CacheInsertOptions
			{
				Priority = CacheItemPriority.NotRemovable
			});
			string[] cachekeys = new string[]
			{
				key
			};
			CacheDependency cacheDependency = new CacheDependency(null, cachekeys);
			if (dependencies == null)
			{
				dependencies = cacheDependency;
			}
			else
			{
				AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
				aggregateCacheDependency.Add(new CacheDependency[]
				{
					dependencies,
					cacheDependency
				});
				dependencies = aggregateCacheDependency;
			}
			HttpRuntime.Cache.InternalCache.Insert("w" + key, new Cache.SentinelEntry(key, cacheDependency, onUpdateCallback), new CacheInsertOptions
			{
				Dependencies = dependencies,
				AbsoluteExpiration = absoluteExpiration2,
				SlidingExpiration = slidingExpiration,
				Priority = CacheItemPriority.NotRemovable,
				OnRemovedCallback = Cache.s_sentinelRemovedCallback
			});
		}

		// Token: 0x0600662B RID: 26155 RVA: 0x00167C94 File Offset: 0x00165E94
		public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
		{
			DateTime absoluteExpiration2 = DateTimeUtil.ConvertToUniversalTime(absoluteExpiration);
			return this.ObjectCache.Add(key, value, new CacheInsertOptions
			{
				Dependencies = dependencies,
				AbsoluteExpiration = absoluteExpiration2,
				SlidingExpiration = slidingExpiration,
				Priority = priority,
				OnRemovedCallback = onRemoveCallback
			});
		}

		// Token: 0x0600662C RID: 26156 RVA: 0x00167CE1 File Offset: 0x00165EE1
		public object Remove(string key)
		{
			return this.ObjectCache.Remove(key, CacheItemRemovedReason.Removed);
		}

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x0600662D RID: 26157 RVA: 0x00167CF0 File Offset: 0x00165EF0
		public long EffectivePrivateBytesLimit
		{
			get
			{
				return AspNetMemoryMonitor.ProcessPrivateBytesLimit;
			}
		}

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x0600662E RID: 26158 RVA: 0x00167CF7 File Offset: 0x00165EF7
		public long EffectivePercentagePhysicalMemoryLimit
		{
			get
			{
				return AspNetMemoryMonitor.PhysicalMemoryPercentageLimit;
			}
		}

		// Token: 0x040034A1 RID: 13473
		public static readonly DateTime NoAbsoluteExpiration = DateTime.MaxValue;

		// Token: 0x040034A2 RID: 13474
		public static readonly TimeSpan NoSlidingExpiration = TimeSpan.Zero;

		// Token: 0x040034A3 RID: 13475
		private static CacheStoreProvider _objectCache = null;

		// Token: 0x040034A4 RID: 13476
		private static CacheStoreProvider _internalCache = null;

		// Token: 0x040034A5 RID: 13477
		private static CacheItemRemovedCallback s_sentinelRemovedCallback = new CacheItemRemovedCallback(Cache.SentinelEntry.OnCacheItemRemovedCallback);

		// Token: 0x02000A73 RID: 2675
		private class SentinelEntry
		{
			// Token: 0x06006F34 RID: 28468 RVA: 0x0018B9CC File Offset: 0x00189BCC
			public SentinelEntry(string key, CacheDependency expensiveObjectDependency, CacheItemUpdateCallback callback)
			{
				this._key = key;
				this._expensiveObjectDependency = expensiveObjectDependency;
				this._cacheItemUpdateCallback = callback;
			}

			// Token: 0x17001E4B RID: 7755
			// (get) Token: 0x06006F35 RID: 28469 RVA: 0x0018B9E9 File Offset: 0x00189BE9
			public string Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17001E4C RID: 7756
			// (get) Token: 0x06006F36 RID: 28470 RVA: 0x0018B9F1 File Offset: 0x00189BF1
			public CacheDependency ExpensiveObjectDependency
			{
				get
				{
					return this._expensiveObjectDependency;
				}
			}

			// Token: 0x17001E4D RID: 7757
			// (get) Token: 0x06006F37 RID: 28471 RVA: 0x0018B9F9 File Offset: 0x00189BF9
			public CacheItemUpdateCallback CacheItemUpdateCallback
			{
				get
				{
					return this._cacheItemUpdateCallback;
				}
			}

			// Token: 0x06006F38 RID: 28472 RVA: 0x0018BA04 File Offset: 0x00189C04
			public static void OnCacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
			{
				Cache.SentinelEntry sentinelEntry = value as Cache.SentinelEntry;
				CacheItemUpdateReason reason2;
				switch (reason)
				{
				case CacheItemRemovedReason.Expired:
					reason2 = CacheItemUpdateReason.Expired;
					break;
				case CacheItemRemovedReason.Underused:
					return;
				case CacheItemRemovedReason.DependencyChanged:
					reason2 = CacheItemUpdateReason.DependencyChanged;
					if (sentinelEntry.ExpensiveObjectDependency.HasChanged)
					{
						return;
					}
					break;
				default:
					return;
				}
				CacheItemUpdateCallback cacheItemUpdateCallback = sentinelEntry.CacheItemUpdateCallback;
				try
				{
					object obj;
					CacheDependency cacheDependency;
					DateTime absoluteExpiration;
					TimeSpan slidingExpiration;
					cacheItemUpdateCallback(sentinelEntry.Key, reason2, out obj, out cacheDependency, out absoluteExpiration, out slidingExpiration);
					if (obj != null && (cacheDependency == null || !cacheDependency.HasChanged))
					{
						HttpRuntime.Cache.Insert(sentinelEntry.Key, obj, cacheDependency, absoluteExpiration, slidingExpiration, sentinelEntry.CacheItemUpdateCallback);
					}
					else
					{
						HttpRuntime.Cache.Remove(sentinelEntry.Key);
					}
				}
				catch (Exception e)
				{
					HttpRuntime.Cache.Remove(sentinelEntry.Key);
					try
					{
						WebBaseEvent.RaiseRuntimeError(e, value);
					}
					catch
					{
					}
				}
			}

			// Token: 0x04003BAE RID: 15278
			private string _key;

			// Token: 0x04003BAF RID: 15279
			private CacheDependency _expensiveObjectDependency;

			// Token: 0x04003BB0 RID: 15280
			private CacheItemUpdateCallback _cacheItemUpdateCallback;
		}
	}
}
