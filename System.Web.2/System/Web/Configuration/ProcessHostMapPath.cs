using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200072A RID: 1834
	internal sealed class ProcessHostMapPath : IConfigMapPath, IConfigMapPath2
	{
		// Token: 0x06005858 RID: 22616 RVA: 0x00135184 File Offset: 0x00133384
		static ProcessHostMapPath()
		{
			HttpRuntime.ForceStaticInit();
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x0013518C File Offset: 0x0013338C
		internal ProcessHostMapPath(IProcessHostSupportFunctions functions)
		{
			if (functions == null)
			{
				ProcessHostConfigUtils.InitStandaloneConfig();
			}
			if (functions != null)
			{
				this._functions = Misc.CreateLocalSupportFunctions(functions);
			}
			if (this._functions != null)
			{
				IntPtr nativeConfigurationSystem = this._functions.GetNativeConfigurationSystem();
				if (IntPtr.Zero != nativeConfigurationSystem)
				{
					UnsafeIISMethods.MgdSetNativeConfiguration(nativeConfigurationSystem);
				}
			}
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x001277FC File Offset: 0x001259FC
		string IConfigMapPath.GetMachineConfigFilename()
		{
			return HttpConfigurationSystem.MachineConfigurationFilePath;
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x001351E0 File Offset: 0x001333E0
		string IConfigMapPath.GetRootWebConfigFilename()
		{
			string text = null;
			if (this._functions != null)
			{
				text = this._functions.GetRootWebConfigFilename();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = HttpConfigurationSystem.RootWebConfigurationFilePath;
			}
			return text;
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x00135212 File Offset: 0x00133412
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

		// Token: 0x0600585D RID: 22621 RVA: 0x0013522F File Offset: 0x0013342F
		void IConfigMapPath.GetPathConfigFilename(string siteID, string path, out string directory, out string baseName)
		{
			this.GetPathConfigFilenameWorker(siteID, VirtualPath.Create(path), out directory, out baseName);
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x00135241 File Offset: 0x00133441
		void IConfigMapPath2.GetPathConfigFilename(string siteID, VirtualPath path, out string directory, out string baseName)
		{
			this.GetPathConfigFilenameWorker(siteID, path, out directory, out baseName);
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x0013524E File Offset: 0x0013344E
		void IConfigMapPath.GetDefaultSiteNameAndID(out string siteName, out string siteID)
		{
			siteID = "1";
			siteName = ProcessHostConfigUtils.GetSiteNameFromId(1U);
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x00135260 File Offset: 0x00133460
		void IConfigMapPath.ResolveSiteArgument(string siteArgument, out string siteName, out string siteID)
		{
			if (string.IsNullOrEmpty(siteArgument) || StringUtil.EqualsIgnoreCase(siteArgument, "1") || StringUtil.EqualsIgnoreCase(siteArgument, ProcessHostConfigUtils.GetSiteNameFromId(1U)))
			{
				siteName = ProcessHostConfigUtils.GetSiteNameFromId(1U);
				siteID = "1";
				return;
			}
			siteName = string.Empty;
			siteID = string.Empty;
			string text = null;
			if (IISMapPath.IsSiteId(siteArgument))
			{
				uint siteId;
				if (uint.TryParse(siteArgument, out siteId))
				{
					text = ProcessHostConfigUtils.GetSiteNameFromId(siteId);
				}
			}
			else
			{
				uint num = UnsafeIISMethods.MgdResolveSiteName(IntPtr.Zero, siteArgument);
				if (num != 0U)
				{
					siteID = num.ToString(CultureInfo.InvariantCulture);
					siteName = siteArgument;
					return;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				siteName = text;
				siteID = siteArgument;
				return;
			}
			siteName = siteArgument;
			siteID = string.Empty;
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x00135305 File Offset: 0x00133505
		private string MapPathWorker(string siteID, VirtualPath path)
		{
			return this.MapPathCaching(siteID, path);
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x0013530F File Offset: 0x0013350F
		string IConfigMapPath2.MapPath(string siteID, VirtualPath path)
		{
			return this.MapPathWorker(siteID, path);
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x00135319 File Offset: 0x00133519
		string IConfigMapPath.MapPath(string siteID, string path)
		{
			return this.MapPathWorker(siteID, VirtualPath.Create(path));
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x00135328 File Offset: 0x00133528
		string IConfigMapPath.GetAppPathForPath(string siteID, string path)
		{
			VirtualPath appPathForPathWorker = this.GetAppPathForPathWorker(siteID, VirtualPath.Create(path));
			return appPathForPathWorker.VirtualPathString;
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x00135349 File Offset: 0x00133549
		VirtualPath IConfigMapPath2.GetAppPathForPath(string siteID, VirtualPath path)
		{
			return this.GetAppPathForPathWorker(siteID, path);
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x00135354 File Offset: 0x00133554
		private VirtualPath GetAppPathForPathWorker(string siteID, VirtualPath path)
		{
			uint siteId = 0U;
			if (!uint.TryParse(siteID, out siteId))
			{
				return VirtualPath.RootVirtualPath;
			}
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			string text;
			try
			{
				text = ((UnsafeIISMethods.MgdGetAppPathForPath(IntPtr.Zero, siteId, path.VirtualPathString, out zero, out num) == 0 && num > 0) ? StringUtil.StringFromWCharPtr(zero, num) : null);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero);
				}
			}
			if (text == null)
			{
				return VirtualPath.RootVirtualPath;
			}
			return VirtualPath.Create(text);
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x001353DC File Offset: 0x001335DC
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
						try
						{
							string text2 = null;
							uint siteId;
							if (uint.TryParse(siteID, out siteId))
							{
								string siteNameFromId = ProcessHostConfigUtils.GetSiteNameFromId(siteId);
								text2 = ProcessHostConfigUtils.MapPathActual(siteNameFromId, path);
							}
							if (text2 != null && text2.Length == 2 && text2[1] == ':')
							{
								text2 += "\\";
							}
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
							mapPathCacheInfo.MapPathResult = text2;
						}
						catch (Exception cachedException)
						{
							if (!HttpRuntime.IsMapPathRelaxed)
							{
								mapPathCacheInfo.CachedException = cachedException;
								mapPathCacheInfo.Evaluated = true;
								throw;
							}
							mapPathCacheInfo.MapPathResult = HttpRuntime.GetRelaxedMapPathResult(null);
						}
						mapPathCacheInfo.Evaluated = true;
					}
				}
			}
			if (mapPathCacheInfo.CachedException != null)
			{
				throw mapPathCacheInfo.CachedException;
			}
			return this.MatchResult(path, mapPathCacheInfo.MapPathResult);
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x00135674 File Offset: 0x00133874
		private string MatchResult(VirtualPath path, string result)
		{
			if (string.IsNullOrEmpty(result))
			{
				return result;
			}
			result = result.Replace('/', '\\');
			if (path.HasTrailingSlash)
			{
				if (!UrlPath.PathEndsWithExtraSlash(result))
				{
					result += "\\";
				}
			}
			else if (UrlPath.PathEndsWithExtraSlash(result))
			{
				result = result.Substring(0, result.Length - 1);
			}
			return result;
		}

		// Token: 0x04002EF4 RID: 12020
		private IProcessHostSupportFunctions _functions;

		// Token: 0x04002EF5 RID: 12021
		internal static string _DefaultPhysicalPathOnMapPathFailure;
	}
}
