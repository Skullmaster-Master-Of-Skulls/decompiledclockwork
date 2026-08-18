using System;
using System.Configuration;
using System.Configuration.Internal;
using System.IO;
using System.Threading;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000046 RID: 70
	internal class CachedPathData
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x000068C0 File Offset: 0x00004AC0
		internal CachedPathData(string configPath, VirtualPath virtualPath, string physicalPath, bool exists)
		{
			this._runtimeConfig = RuntimeConfig.GetErrorRuntimeConfig();
			this._configPath = configPath;
			this._virtualPath = virtualPath;
			this._physicalPath = physicalPath;
			this._flags[4] = exists;
			string schemeDelimiter = Uri.SchemeDelimiter;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00006908 File Offset: 0x00004B08
		internal static void InitializeUrlMetadataSlidingExpiration(HostingEnvironmentSection section)
		{
			TimeSpan urlMetadataSlidingExpiration = section.UrlMetadataSlidingExpiration;
			if (urlMetadataSlidingExpiration == TimeSpan.Zero)
			{
				CachedPathData.s_doNotCacheUrlMetadata = true;
				return;
			}
			if (urlMetadataSlidingExpiration == TimeSpan.MaxValue)
			{
				CachedPathData.s_urlMetadataSlidingExpiration = Cache.NoSlidingExpiration;
				CachedPathData.s_doNotCacheUrlMetadata = false;
				return;
			}
			CachedPathData.s_urlMetadataSlidingExpiration = urlMetadataSlidingExpiration;
			CachedPathData.s_doNotCacheUrlMetadata = false;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000695A File Offset: 0x00004B5A
		internal static CachedPathData GetMachinePathData()
		{
			return CachedPathData.GetConfigPathData("machine");
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00006966 File Offset: 0x00004B66
		internal static CachedPathData GetRootWebPathData()
		{
			return CachedPathData.GetConfigPathData("machine/webroot");
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00006972 File Offset: 0x00004B72
		internal static CachedPathData GetApplicationPathData()
		{
			if (!HostingEnvironment.IsHosted)
			{
				return CachedPathData.GetRootWebPathData();
			}
			return CachedPathData.GetConfigPathData(HostingEnvironment.AppConfigPath);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000698C File Offset: 0x00004B8C
		internal static CachedPathData GetVirtualPathData(VirtualPath virtualPath, bool permitPathsOutsideApp)
		{
			if (!HostingEnvironment.IsHosted)
			{
				return CachedPathData.GetRootWebPathData();
			}
			if (virtualPath != null)
			{
				virtualPath.FailIfRelativePath();
			}
			if (!(virtualPath == null) && virtualPath.IsWithinAppRoot)
			{
				string configPathFromSiteIDAndVPath = WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(HostingEnvironment.SiteID, virtualPath);
				return CachedPathData.GetConfigPathData(configPathFromSiteIDAndVPath);
			}
			if (permitPathsOutsideApp)
			{
				return CachedPathData.GetApplicationPathData();
			}
			throw new ArgumentException(SR.GetString("Cross_app_not_allowed", new object[]
			{
				(virtualPath != null) ? virtualPath.VirtualPathString : "null"
			}));
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00006A10 File Offset: 0x00004C10
		private static bool IsCachedPathDataRemovable(string configPath)
		{
			if (CachedPathData.s_appConfigPathLength == 0)
			{
				CachedPathData.s_appConfigPathLength = (HostingEnvironment.IsHosted ? HostingEnvironment.AppConfigPath.Length : "machine/webroot".Length);
			}
			return configPath.Length > CachedPathData.s_appConfigPathLength;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00006A48 File Offset: 0x00004C48
		private static CachedPathData GetConfigPathData(string configPath)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = CachedPathData.IsCachedPathDataRemovable(configPath);
			if (flag3 && CachedPathData.DoNotCacheUrlMetadata)
			{
				string text = null;
				VirtualPath virtualPath = null;
				WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out text, out virtualPath);
				string physicalPath = CachedPathData.GetPhysicalPath(virtualPath);
				string parent = ConfigPathUtility.GetParent(configPath);
				CachedPathData configPathData = CachedPathData.GetConfigPathData(parent);
				if (!string.IsNullOrEmpty(physicalPath))
				{
					FileUtil.PhysicalPathStatus(physicalPath, false, false, out flag, out flag2);
				}
				CachedPathData cachedPathData = new CachedPathData(configPath, virtualPath, physicalPath, flag);
				cachedPathData.Init(configPathData);
				return cachedPathData;
			}
			string key = CachedPathData.CreateKey(configPath);
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			CachedPathData cachedPathData2 = (CachedPathData)internalCache.Get(key);
			if (cachedPathData2 != null)
			{
				cachedPathData2.WaitForInit();
				return cachedPathData2;
			}
			bool flag4 = false;
			string text2 = null;
			VirtualPath virtualPath2 = null;
			CachedPathData parentData = null;
			CacheDependency cacheDependency = null;
			string text3 = null;
			string[] filenames = null;
			string[] cachekeys = null;
			if (WebConfigurationHost.IsMachineConfigPath(configPath))
			{
				flag4 = true;
			}
			else
			{
				string parent2 = ConfigPathUtility.GetParent(configPath);
				parentData = CachedPathData.GetConfigPathData(parent2);
				string text4 = CachedPathData.CreateKey(parent2);
				cachekeys = new string[]
				{
					text4
				};
				if (!WebConfigurationHost.IsVirtualPathConfigPath(configPath))
				{
					flag4 = true;
				}
				else
				{
					flag4 = !flag3;
					WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out text2, out virtualPath2);
					text3 = CachedPathData.GetPhysicalPath(virtualPath2);
					if (!string.IsNullOrEmpty(text3))
					{
						FileUtil.PhysicalPathStatus(text3, false, false, out flag, out flag2);
						if (flag && !flag2)
						{
							filenames = new string[]
							{
								text3
							};
						}
					}
				}
				try
				{
					cacheDependency = new CacheDependency(0, filenames, cachekeys);
				}
				catch
				{
				}
			}
			CachedPathData cachedPathData3 = null;
			bool flag5 = false;
			bool flag6 = false;
			CacheItemPriority priority = flag4 ? CacheItemPriority.NotRemovable : CacheItemPriority.Normal;
			TimeSpan slidingExpiration = flag4 ? Cache.NoSlidingExpiration : CachedPathData.UrlMetadataSlidingExpiration;
			try
			{
				using (cacheDependency)
				{
					cachedPathData3 = new CachedPathData(configPath, virtualPath2, text3, flag);
					try
					{
					}
					finally
					{
						cachedPathData2 = (CachedPathData)internalCache.Add(key, cachedPathData3, new CacheInsertOptions
						{
							Dependencies = cacheDependency,
							SlidingExpiration = slidingExpiration,
							Priority = priority,
							OnRemovedCallback = CachedPathData.s_callback
						});
						if (cachedPathData2 == null)
						{
							flag5 = true;
						}
					}
				}
				if (!flag5)
				{
					cachedPathData2.WaitForInit();
					return cachedPathData2;
				}
				CachedPathData obj = cachedPathData3;
				lock (obj)
				{
					try
					{
						cachedPathData3.Init(parentData);
						flag6 = true;
					}
					finally
					{
						cachedPathData3._flags[1] = true;
						Monitor.PulseAll(cachedPathData3);
						if (cachedPathData3._flags[64])
						{
							cachedPathData3.Close();
						}
					}
				}
			}
			finally
			{
				if (flag5)
				{
					if (!cachedPathData3._flags[1])
					{
						CachedPathData obj2 = cachedPathData3;
						lock (obj2)
						{
							cachedPathData3._flags[1] = true;
							Monitor.PulseAll(cachedPathData3);
							if (cachedPathData3._flags[64])
							{
								cachedPathData3.Close();
							}
						}
					}
					if (!flag6 || (cachedPathData3.ConfigRecord != null && cachedPathData3.ConfigRecord.HasInitErrors))
					{
						if (cacheDependency != null)
						{
							if (!flag6)
							{
								cacheDependency = new CacheDependency(0, null, cachekeys);
							}
							else
							{
								cacheDependency = new CacheDependency(0, filenames, cachekeys);
							}
						}
						using (cacheDependency)
						{
							internalCache.Insert(key, cachedPathData3, new CacheInsertOptions
							{
								Dependencies = cacheDependency,
								AbsoluteExpiration = DateTime.UtcNow.AddSeconds(5.0),
								OnRemovedCallback = CachedPathData.s_callback
							});
						}
					}
				}
			}
			return cachedPathData3;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00006DF4 File Offset: 0x00004FF4
		private static string GetPhysicalPath(VirtualPath virtualPath)
		{
			string text = null;
			try
			{
				text = virtualPath.MapPathInternal(true);
			}
			catch (HttpException ex)
			{
				if (ex.GetHttpCode() == 500)
				{
					throw new HttpException(404, string.Empty);
				}
				throw;
			}
			FileUtil.CheckSuspiciousPhysicalPath(text);
			return text;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00006E44 File Offset: 0x00005044
		internal static void RemoveBadPathData(CachedPathData pathData)
		{
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string text = pathData._configPath;
			string key = CachedPathData.CreateKey(text);
			while (pathData != null && !pathData.CompletedFirstRequest && !pathData.Exists)
			{
				internalCache.Remove(key);
				text = ConfigPathUtility.GetParent(text);
				if (text == null)
				{
					break;
				}
				key = CachedPathData.CreateKey(text);
				pathData = (CachedPathData)internalCache.Get(key);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00006EA8 File Offset: 0x000050A8
		internal static void MarkCompleted(CachedPathData pathData)
		{
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string text = pathData._configPath;
			do
			{
				pathData.CompletedFirstRequest = true;
				text = ConfigPathUtility.GetParent(text);
				if (text == null)
				{
					break;
				}
				string key = CachedPathData.CreateKey(text);
				pathData = (CachedPathData)internalCache.Get(key);
			}
			while (pathData != null && !pathData.CompletedFirstRequest);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00006EF8 File Offset: 0x000050F8
		private void Close()
		{
			if (this._flags[1] && this._flags.ChangeValue(32, true) && this._flags[16])
			{
				this.ConfigRecord.Remove();
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00006F34 File Offset: 0x00005134
		private static void OnCacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
		{
			CachedPathData cachedPathData = (CachedPathData)value;
			cachedPathData._flags[64] = true;
			cachedPathData.Close();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00006F5C File Offset: 0x0000515C
		private static string CreateKey(string configPath)
		{
			return "d" + configPath;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00006F6C File Offset: 0x0000516C
		private void Init(CachedPathData parentData)
		{
			if (!HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				this._runtimeConfig = null;
				return;
			}
			IInternalConfigRecord uniqueConfigRecord = HttpConfigurationSystem.GetUniqueConfigRecord(this._configPath);
			if (uniqueConfigRecord.ConfigPath.Length == this._configPath.Length)
			{
				this._flags[16] = true;
				this._runtimeConfig = new RuntimeConfig(uniqueConfigRecord);
				return;
			}
			this._runtimeConfig = parentData._runtimeConfig;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00006FD4 File Offset: 0x000051D4
		private void WaitForInit()
		{
			if (!this._flags[1])
			{
				lock (this)
				{
					if (!this._flags[1])
					{
						Monitor.Wait(this);
					}
				}
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000702C File Offset: 0x0000522C
		internal void ValidatePath(string physicalPath)
		{
			if (string.IsNullOrEmpty(this._physicalPath) && string.IsNullOrEmpty(physicalPath))
			{
				return;
			}
			if (!string.IsNullOrEmpty(this._physicalPath) && !string.IsNullOrEmpty(physicalPath))
			{
				if (this._physicalPath.Length == physicalPath.Length)
				{
					if (string.Compare(this._physicalPath, 0, physicalPath, 0, physicalPath.Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return;
					}
				}
				else if (this._physicalPath.Length - physicalPath.Length == 1)
				{
					if (this._physicalPath[this._physicalPath.Length - 1] == System.IO.Path.DirectorySeparatorChar && string.Compare(this._physicalPath, 0, physicalPath, 0, physicalPath.Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return;
					}
				}
				else if (physicalPath.Length - this._physicalPath.Length == 1 && physicalPath[physicalPath.Length - 1] == System.IO.Path.DirectorySeparatorChar && string.Compare(this._physicalPath, 0, physicalPath, 0, this._physicalPath.Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return;
				}
			}
			FileUtil.CheckSuspiciousPhysicalPath(physicalPath);
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00007131 File Offset: 0x00005331
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0000713F File Offset: 0x0000533F
		internal bool CompletedFirstRequest
		{
			get
			{
				return this._flags[2];
			}
			set
			{
				this._flags[2] = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000714E File Offset: 0x0000534E
		internal VirtualPath Path
		{
			get
			{
				return this._virtualPath;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00007156 File Offset: 0x00005356
		internal string PhysicalPath
		{
			get
			{
				return this._physicalPath;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000715E File Offset: 0x0000535E
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x00007170 File Offset: 0x00005370
		internal bool AnonymousAccessChecked
		{
			get
			{
				return this._flags[256];
			}
			set
			{
				this._flags[256] = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00007183 File Offset: 0x00005383
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00007195 File Offset: 0x00005395
		internal bool AnonymousAccessAllowed
		{
			get
			{
				return this._flags[512];
			}
			set
			{
				this._flags[512] = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x000071A8 File Offset: 0x000053A8
		internal bool Exists
		{
			get
			{
				return this._flags[4];
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000071B6 File Offset: 0x000053B6
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x000071BE File Offset: 0x000053BE
		internal HandlerMappingMemo CachedHandler
		{
			get
			{
				return this._handlerMemo;
			}
			set
			{
				this._handlerMemo = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000071C7 File Offset: 0x000053C7
		internal IInternalConfigRecord ConfigRecord
		{
			get
			{
				if (this._runtimeConfig == null)
				{
					return null;
				}
				return this._runtimeConfig.ConfigRecord;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x000071DE File Offset: 0x000053DE
		internal RuntimeConfig RuntimeConfig
		{
			get
			{
				return this._runtimeConfig;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000071E6 File Offset: 0x000053E6
		internal static TimeSpan UrlMetadataSlidingExpiration
		{
			get
			{
				return CachedPathData.s_urlMetadataSlidingExpiration;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x000071ED File Offset: 0x000053ED
		internal static bool DoNotCacheUrlMetadata
		{
			get
			{
				return CachedPathData.s_doNotCacheUrlMetadata;
			}
		}

		// Token: 0x0400012B RID: 299
		internal const int FInited = 1;

		// Token: 0x0400012C RID: 300
		internal const int FCompletedFirstRequest = 2;

		// Token: 0x0400012D RID: 301
		internal const int FExists = 4;

		// Token: 0x0400012E RID: 302
		internal const int FOwnsConfigRecord = 16;

		// Token: 0x0400012F RID: 303
		internal const int FClosed = 32;

		// Token: 0x04000130 RID: 304
		internal const int FCloseNeeded = 64;

		// Token: 0x04000131 RID: 305
		internal const int FAnonymousAccessChecked = 256;

		// Token: 0x04000132 RID: 306
		internal const int FAnonymousAccessAllowed = 512;

		// Token: 0x04000133 RID: 307
		private static CacheItemRemovedCallback s_callback = new CacheItemRemovedCallback(CachedPathData.OnCacheItemRemoved);

		// Token: 0x04000134 RID: 308
		private static TimeSpan s_urlMetadataSlidingExpiration = HostingEnvironmentSection.DefaultUrlMetadataSlidingExpiration;

		// Token: 0x04000135 RID: 309
		private static bool s_doNotCacheUrlMetadata = false;

		// Token: 0x04000136 RID: 310
		private static int s_appConfigPathLength = 0;

		// Token: 0x04000137 RID: 311
		private SafeBitVector32 _flags;

		// Token: 0x04000138 RID: 312
		private string _configPath;

		// Token: 0x04000139 RID: 313
		private VirtualPath _virtualPath;

		// Token: 0x0400013A RID: 314
		private string _physicalPath;

		// Token: 0x0400013B RID: 315
		private RuntimeConfig _runtimeConfig;

		// Token: 0x0400013C RID: 316
		private HandlerMappingMemo _handlerMemo;
	}
}
