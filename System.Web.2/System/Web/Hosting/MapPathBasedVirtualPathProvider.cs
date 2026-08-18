using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007CF RID: 1999
	internal class MapPathBasedVirtualPathProvider : VirtualPathProvider
	{
		// Token: 0x06006007 RID: 24583 RVA: 0x0014BDA4 File Offset: 0x00149FA4
		public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			foreach (object obj in virtualPathDependencies)
			{
				string virtualPath2 = (string)obj;
				string fileName = HostingEnvironment.MapPathInternal(virtualPath2);
				hashCodeCombiner.AddFile(fileName);
			}
			return hashCodeCombiner.CombinedHashString;
		}

		// Token: 0x06006008 RID: 24584 RVA: 0x0014BE10 File Offset: 0x0014A010
		public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			if (virtualPathDependencies == null)
			{
				return null;
			}
			StringCollection stringCollection = null;
			foreach (object obj in virtualPathDependencies)
			{
				string virtualPath2 = (string)obj;
				string value = HostingEnvironment.MapPathInternal(virtualPath2);
				if (stringCollection == null)
				{
					stringCollection = new StringCollection();
				}
				stringCollection.Add(value);
			}
			if (stringCollection == null)
			{
				return null;
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return new CacheDependency(0, array, utcStart);
		}

		// Token: 0x06006009 RID: 24585 RVA: 0x0014BEA4 File Offset: 0x0014A0A4
		private string CreateCacheKey(bool isFile, string physicalPath)
		{
			if (isFile)
			{
				return "Bf" + physicalPath;
			}
			return "Bd" + physicalPath;
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x0014BEC0 File Offset: 0x0014A0C0
		private bool CacheLookupOrInsert(string virtualPath, bool isFile)
		{
			string text = HostingEnvironment.MapPathInternal(virtualPath);
			bool doNotCacheUrlMetadata = CachedPathData.DoNotCacheUrlMetadata;
			string key = null;
			if (!doNotCacheUrlMetadata)
			{
				key = this.CreateCacheKey(isFile, text);
				bool? flag = HttpRuntime.Cache.InternalCache.Get(key) as bool?;
				if (flag != null)
				{
					return flag.Value;
				}
			}
			bool flag2 = isFile ? File.Exists(text) : Directory.Exists(text);
			if (doNotCacheUrlMetadata)
			{
				return flag2;
			}
			string text2 = flag2 ? text : FileUtil.GetFirstExistingDirectory(MapPathBasedVirtualPathProvider.AppRoot, text);
			if (text2 != null)
			{
				CacheDependency dependencies = new CacheDependency(text2);
				TimeSpan urlMetadataSlidingExpiration = CachedPathData.UrlMetadataSlidingExpiration;
				HttpRuntime.Cache.InternalCache.Insert(key, flag2, new CacheInsertOptions
				{
					Dependencies = dependencies,
					SlidingExpiration = urlMetadataSlidingExpiration
				});
			}
			return flag2;
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x0600600B RID: 24587 RVA: 0x0014BF84 File Offset: 0x0014A184
		private static string AppRoot
		{
			get
			{
				string text = MapPathBasedVirtualPathProvider._AppRoot;
				if (text == null)
				{
					InternalSecurityPermissions.AppPathDiscovery.Assert();
					text = Path.GetFullPath(HttpRuntime.AppDomainAppPathInternal);
					text = FileUtil.RemoveTrailingDirectoryBackSlash(text);
					MapPathBasedVirtualPathProvider._AppRoot = text;
				}
				return text;
			}
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x0014BFBD File Offset: 0x0014A1BD
		public override bool FileExists(string virtualPath)
		{
			return this.CacheLookupOrInsert(virtualPath, true);
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x0014BFC7 File Offset: 0x0014A1C7
		public override bool DirectoryExists(string virtualDir)
		{
			return this.CacheLookupOrInsert(virtualDir, false);
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x0014BFD1 File Offset: 0x0014A1D1
		public override VirtualFile GetFile(string virtualPath)
		{
			return new MapPathBasedVirtualFile(virtualPath);
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x0014BFD9 File Offset: 0x0014A1D9
		public override VirtualDirectory GetDirectory(string virtualDir)
		{
			return new MapPathBasedVirtualDirectory(virtualDir);
		}

		// Token: 0x04003236 RID: 12854
		private static string _AppRoot;
	}
}
