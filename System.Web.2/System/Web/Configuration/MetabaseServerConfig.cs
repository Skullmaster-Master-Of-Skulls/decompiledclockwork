using System;
using System.Collections;
using System.Text;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000718 RID: 1816
	internal class MetabaseServerConfig : IServerConfig, IConfigMapPath, IConfigMapPath2
	{
		// Token: 0x0600576A RID: 22378 RVA: 0x00132644 File Offset: 0x00130844
		internal static IServerConfig GetInstance()
		{
			if (MetabaseServerConfig.s_instance == null)
			{
				object obj = MetabaseServerConfig.s_initLock;
				lock (obj)
				{
					if (MetabaseServerConfig.s_instance == null)
					{
						MetabaseServerConfig.s_instance = new MetabaseServerConfig();
					}
				}
			}
			return MetabaseServerConfig.s_instance;
		}

		// Token: 0x0600576B RID: 22379 RVA: 0x0013269C File Offset: 0x0013089C
		private MetabaseServerConfig()
		{
			HttpRuntime.ForceStaticInit();
			bool flag = this.MBGetSiteNameFromSiteID("1", out this._defaultSiteName);
			this._siteIdForCurrentApplication = HostingEnvironment.SiteID;
			if (this._siteIdForCurrentApplication == null)
			{
				this._siteIdForCurrentApplication = "1";
			}
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x001326E4 File Offset: 0x001308E4
		string IServerConfig.GetSiteNameFromSiteID(string siteID)
		{
			if (StringUtil.EqualsIgnoreCase(siteID, "1"))
			{
				return this._defaultSiteName;
			}
			string result;
			bool flag = this.MBGetSiteNameFromSiteID(siteID, out result);
			return result;
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x00132710 File Offset: 0x00130910
		string IServerConfig.MapPath(IApplicationHost appHost, VirtualPath path)
		{
			string siteID = (appHost == null) ? this._siteIdForCurrentApplication : appHost.GetSiteID();
			return this.MapPathCaching(siteID, path);
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x00132738 File Offset: 0x00130938
		string[] IServerConfig.GetVirtualSubdirs(VirtualPath path, bool inApp)
		{
			string aboPath = this.GetAboPath(this._siteIdForCurrentApplication, path.VirtualPathString);
			return this.MBGetVirtualSubdirs(aboPath, inApp);
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x00132760 File Offset: 0x00130960
		bool IServerConfig.GetUncUser(IApplicationHost appHost, VirtualPath path, out string username, out string password)
		{
			string aboPath = this.GetAboPath(appHost.GetSiteID(), path.VirtualPathString);
			return this.MBGetUncUser(aboPath, out username, out password);
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x0013278A File Offset: 0x0013098A
		long IServerConfig.GetW3WPMemoryLimitInKB()
		{
			return (long)this.MBGetW3WPMemoryLimitInKB();
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x001277FC File Offset: 0x001259FC
		string IConfigMapPath.GetMachineConfigFilename()
		{
			return HttpConfigurationSystem.MachineConfigurationFilePath;
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x00127803 File Offset: 0x00125A03
		string IConfigMapPath.GetRootWebConfigFilename()
		{
			return HttpConfigurationSystem.RootWebConfigurationFilePath;
		}

		// Token: 0x06005773 RID: 22387 RVA: 0x00132793 File Offset: 0x00130993
		private void GetPathConfigFilenameWorker(string siteID, VirtualPath path, out string directory, out string baseName)
		{
			directory = this.MapPathCaching(siteID, path);
			if (directory != null)
			{
				baseName = "web.config";
				return;
			}
			baseName = null;
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x001327B0 File Offset: 0x001309B0
		void IConfigMapPath.GetPathConfigFilename(string siteID, string path, out string directory, out string baseName)
		{
			this.GetPathConfigFilenameWorker(siteID, VirtualPath.Create(path), out directory, out baseName);
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x001327C2 File Offset: 0x001309C2
		void IConfigMapPath2.GetPathConfigFilename(string siteID, VirtualPath path, out string directory, out string baseName)
		{
			this.GetPathConfigFilenameWorker(siteID, path, out directory, out baseName);
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x001327CF File Offset: 0x001309CF
		void IConfigMapPath.GetDefaultSiteNameAndID(out string siteName, out string siteID)
		{
			siteName = this._defaultSiteName;
			siteID = "1";
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x001327E0 File Offset: 0x001309E0
		void IConfigMapPath.ResolveSiteArgument(string siteArgument, out string siteName, out string siteID)
		{
			if (string.IsNullOrEmpty(siteArgument) || StringUtil.EqualsIgnoreCase(siteArgument, "1") || StringUtil.EqualsIgnoreCase(siteArgument, this._defaultSiteName))
			{
				siteName = this._defaultSiteName;
				siteID = "1";
				return;
			}
			siteName = string.Empty;
			siteID = string.Empty;
			bool flag = false;
			if (IISMapPath.IsSiteId(siteArgument))
			{
				flag = this.MBGetSiteNameFromSiteID(siteArgument, out siteName);
			}
			if (flag)
			{
				siteID = siteArgument;
				return;
			}
			flag = this.MBGetSiteIDFromSiteName(siteArgument, out siteID);
			if (flag)
			{
				siteName = siteArgument;
				return;
			}
			siteName = siteArgument;
			siteID = string.Empty;
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x00132862 File Offset: 0x00130A62
		string IConfigMapPath.MapPath(string siteID, string vpath)
		{
			return this.MapPathCaching(siteID, VirtualPath.Create(vpath));
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x00132871 File Offset: 0x00130A71
		string IConfigMapPath2.MapPath(string siteID, VirtualPath vpath)
		{
			return this.MapPathCaching(siteID, vpath);
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x0013287C File Offset: 0x00130A7C
		private VirtualPath GetAppPathForPathWorker(string siteID, VirtualPath vpath)
		{
			string aboPath = this.GetAboPath(siteID, vpath.VirtualPathString);
			string text = this.MBGetAppPath(aboPath);
			if (text == null)
			{
				return VirtualPath.RootVirtualPath;
			}
			string rootAppIDFromSiteID = this.GetRootAppIDFromSiteID(siteID);
			if (StringUtil.EqualsIgnoreCase(rootAppIDFromSiteID, text))
			{
				return VirtualPath.RootVirtualPath;
			}
			string virtualPath = text.Substring(rootAppIDFromSiteID.Length);
			return VirtualPath.CreateAbsolute(virtualPath);
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x001328D4 File Offset: 0x00130AD4
		string IConfigMapPath.GetAppPathForPath(string siteID, string vpath)
		{
			VirtualPath appPathForPathWorker = this.GetAppPathForPathWorker(siteID, VirtualPath.Create(vpath));
			return appPathForPathWorker.VirtualPathString;
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x001328F5 File Offset: 0x00130AF5
		VirtualPath IConfigMapPath2.GetAppPathForPath(string siteID, VirtualPath vpath)
		{
			return this.GetAppPathForPathWorker(siteID, vpath);
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x00132900 File Offset: 0x00130B00
		private string MatchResult(VirtualPath path, string result)
		{
			if (string.IsNullOrEmpty(result))
			{
				return result;
			}
			result = result.Replace('/', '\\');
			if (path.HasTrailingSlash)
			{
				if (!UrlPath.PathEndsWithExtraSlash(result) && !UrlPath.PathIsDriveRoot(result))
				{
					result += "\\";
				}
			}
			else if (UrlPath.PathEndsWithExtraSlash(result) && !UrlPath.PathIsDriveRoot(result))
			{
				result = result.Substring(0, result.Length - 1);
			}
			return result;
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x0013296C File Offset: 0x00130B6C
		private string MapPathCaching(string siteID, VirtualPath path)
		{
			bool doNotCacheUrlMetadata = CachedPathData.DoNotCacheUrlMetadata;
			TimeSpan urlMetadataSlidingExpiration = CachedPathData.UrlMetadataSlidingExpiration;
			MapPathCacheInfo mapPathCacheInfo;
			if (doNotCacheUrlMetadata)
			{
				mapPathCacheInfo = new MapPathCacheInfo();
			}
			else
			{
				string key = "f" + siteID + path.VirtualPathString;
				mapPathCacheInfo = (MapPathCacheInfo)HttpRuntime.Cache.InternalCache.Get(key);
				if (mapPathCacheInfo == null)
				{
					mapPathCacheInfo = new MapPathCacheInfo();
					object obj = HttpRuntime.Cache.InternalCache.Add(key, mapPathCacheInfo, new CacheInsertOptions
					{
						SlidingExpiration = urlMetadataSlidingExpiration
					});
					if (obj != null)
					{
						mapPathCacheInfo = (obj as MapPathCacheInfo);
					}
				}
			}
			if (!mapPathCacheInfo.Evaluated)
			{
				MapPathCacheInfo obj2 = mapPathCacheInfo;
				lock (obj2)
				{
					if (!mapPathCacheInfo.Evaluated && HttpRuntime.IsMapPathRelaxed && path.VirtualPathString.Length > 1)
					{
						VirtualPath virtualPath = path.Parent;
						if (virtualPath != null)
						{
							string virtualPathString = virtualPath.VirtualPathString;
							if (virtualPathString.Length > 1 && StringUtil.StringEndsWith(virtualPathString, '/'))
							{
								virtualPath = VirtualPath.Create(virtualPathString.Substring(0, virtualPathString.Length - 1));
							}
							try
							{
								string text = this.MapPathCaching(siteID, virtualPath);
								if (text == HttpRuntime.GetRelaxedMapPathResult(null))
								{
									mapPathCacheInfo.MapPathResult = text;
									mapPathCacheInfo.Evaluated = true;
								}
							}
							catch
							{
								mapPathCacheInfo.MapPathResult = HttpRuntime.GetRelaxedMapPathResult(null);
								mapPathCacheInfo.Evaluated = true;
							}
						}
					}
					if (!mapPathCacheInfo.Evaluated)
					{
						string text2 = null;
						try
						{
							text2 = this.MapPathActual(siteID, path);
							if (HttpRuntime.IsMapPathRelaxed)
							{
								text2 = HttpRuntime.GetRelaxedMapPathResult(text2);
							}
							if (FileUtil.IsSuspiciousPhysicalPath(text2))
							{
								if (!HttpRuntime.IsMapPathRelaxed)
								{
									throw new HttpException(SR.GetString("Cannot_map_path", new object[]
									{
										path
									}));
								}
								text2 = HttpRuntime.GetRelaxedMapPathResult(null);
							}
						}
						catch (Exception cachedException)
						{
							if (!HttpRuntime.IsMapPathRelaxed)
							{
								mapPathCacheInfo.CachedException = cachedException;
								mapPathCacheInfo.Evaluated = true;
								throw;
							}
							text2 = HttpRuntime.GetRelaxedMapPathResult(null);
						}
						if (text2 != null)
						{
							mapPathCacheInfo.MapPathResult = text2;
							mapPathCacheInfo.Evaluated = true;
						}
					}
				}
			}
			if (mapPathCacheInfo.CachedException != null)
			{
				throw mapPathCacheInfo.CachedException;
			}
			return this.MatchResult(path, mapPathCacheInfo.MapPathResult);
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x00132BC8 File Offset: 0x00130DC8
		private string MapPathActual(string siteID, VirtualPath path)
		{
			string rootAppIDFromSiteID = this.GetRootAppIDFromSiteID(siteID);
			return this.MBMapPath(rootAppIDFromSiteID, path.VirtualPathString);
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x00132BEC File Offset: 0x00130DEC
		private string GetRootAppIDFromSiteID(string siteId)
		{
			return "/LM/W3SVC/" + siteId + "/ROOT";
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x00132C00 File Offset: 0x00130E00
		private string GetAboPath(string siteID, string path)
		{
			string rootAppIDFromSiteID = this.GetRootAppIDFromSiteID(siteID);
			return rootAppIDFromSiteID + this.FixupPathSlash(path);
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x00132C24 File Offset: 0x00130E24
		private string FixupPathSlash(string path)
		{
			if (path == null)
			{
				return null;
			}
			int length = path.Length;
			if (length == 0 || path[length - 1] != '/')
			{
				return path;
			}
			return path.Substring(0, length - 1);
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x00132C5C File Offset: 0x00130E5C
		private bool MBGetSiteNameFromSiteID(string siteID, out string siteName)
		{
			string rootAppIDFromSiteID = this.GetRootAppIDFromSiteID(siteID);
			StringBuilder stringBuilder = new StringBuilder(261);
			int num = UnsafeNativeMethods.IsapiAppHostGetSiteName(rootAppIDFromSiteID, stringBuilder, stringBuilder.Capacity);
			if (num == 1)
			{
				siteName = stringBuilder.ToString();
				return true;
			}
			siteName = string.Empty;
			return false;
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x00132CA0 File Offset: 0x00130EA0
		private bool MBGetSiteIDFromSiteName(string siteName, out string siteID)
		{
			StringBuilder stringBuilder = new StringBuilder(261);
			int num = UnsafeNativeMethods.IsapiAppHostGetSiteId(siteName, stringBuilder, stringBuilder.Capacity);
			if (num == 1)
			{
				siteID = stringBuilder.ToString();
				return true;
			}
			siteID = string.Empty;
			return false;
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x00132CDC File Offset: 0x00130EDC
		private string MBMapPath(string appID, string path)
		{
			int num = 261;
			StringBuilder stringBuilder;
			int num2;
			for (;;)
			{
				stringBuilder = new StringBuilder(num);
				num2 = UnsafeNativeMethods.IsapiAppHostMapPath(appID, path, stringBuilder, stringBuilder.Capacity);
				if (num2 != -2)
				{
					break;
				}
				num *= 2;
			}
			if (num2 == -1)
			{
				throw new HostingEnvironmentException(SR.GetString("Cannot_access_mappath_title"), SR.GetString("Cannot_access_mappath_details"));
			}
			string result;
			if (num2 == 1)
			{
				result = stringBuilder.ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x00132D40 File Offset: 0x00130F40
		private string[] MBGetVirtualSubdirs(string aboPath, bool inApp)
		{
			StringBuilder stringBuilder = new StringBuilder(261);
			int num = 0;
			ArrayList arrayList = new ArrayList();
			for (;;)
			{
				stringBuilder.Length = 0;
				int num2 = UnsafeNativeMethods.IsapiAppHostGetNextVirtualSubdir(aboPath, inApp, ref num, stringBuilder, stringBuilder.Capacity);
				if (num2 == 0)
				{
					break;
				}
				string value = stringBuilder.ToString();
				arrayList.Add(value);
			}
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x00132DA4 File Offset: 0x00130FA4
		private bool MBGetUncUser(string aboPath, out string username, out string password)
		{
			StringBuilder stringBuilder = new StringBuilder(261);
			StringBuilder stringBuilder2 = new StringBuilder(261);
			int num = UnsafeNativeMethods.IsapiAppHostGetUncUser(aboPath, stringBuilder, stringBuilder.Capacity, stringBuilder2, stringBuilder2.Capacity);
			if (num == 1)
			{
				username = stringBuilder.ToString();
				password = stringBuilder2.ToString();
				return true;
			}
			username = null;
			password = null;
			return false;
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x00132DF9 File Offset: 0x00130FF9
		private int MBGetW3WPMemoryLimitInKB()
		{
			return UnsafeNativeMethods.GetW3WPMemoryLimitInKB();
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x00132E00 File Offset: 0x00131000
		private string MBGetAppPath(string aboPath)
		{
			StringBuilder stringBuilder = new StringBuilder(aboPath.Length + 1);
			int num = UnsafeNativeMethods.IsapiAppHostGetAppPath(aboPath, stringBuilder, stringBuilder.Capacity);
			string result;
			if (num == 1)
			{
				result = stringBuilder.ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04002E82 RID: 11906
		private const string DEFAULT_SITEID = "1";

		// Token: 0x04002E83 RID: 11907
		private const string DEFAULT_ROOTAPPID = "/LM/W3SVC/1/ROOT";

		// Token: 0x04002E84 RID: 11908
		private const int MAX_PATH = 260;

		// Token: 0x04002E85 RID: 11909
		private const int BUFSIZE = 261;

		// Token: 0x04002E86 RID: 11910
		private const string LMW3SVC_PREFIX = "/LM/W3SVC/";

		// Token: 0x04002E87 RID: 11911
		private const string ROOT_SUFFIX = "/ROOT";

		// Token: 0x04002E88 RID: 11912
		private static MetabaseServerConfig s_instance;

		// Token: 0x04002E89 RID: 11913
		private static object s_initLock = new object();

		// Token: 0x04002E8A RID: 11914
		private string _defaultSiteName;

		// Token: 0x04002E8B RID: 11915
		private string _siteIdForCurrentApplication;
	}
}
