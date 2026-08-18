using System;
using System.Collections;
using System.IO;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007EB RID: 2027
	public abstract class VirtualPathProvider : MarshalByRefObject
	{
		// Token: 0x060060BE RID: 24766 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x0014E05B File Offset: 0x0014C25B
		internal virtual void Initialize(VirtualPathProvider previous)
		{
			this._previous = previous;
			this.Initialize();
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Initialize()
		{
		}

		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x060060C1 RID: 24769 RVA: 0x0014E06A File Offset: 0x0014C26A
		protected internal VirtualPathProvider Previous
		{
			get
			{
				return this._previous;
			}
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x0014E072 File Offset: 0x0014C272
		public virtual string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
		{
			if (this._previous == null)
			{
				return null;
			}
			return this._previous.GetFileHash(virtualPath, virtualPathDependencies);
		}

		// Token: 0x060060C3 RID: 24771 RVA: 0x0014E08B File Offset: 0x0014C28B
		internal string GetFileHash(VirtualPath virtualPath, IEnumerable virtualPathDependencies)
		{
			return this.GetFileHash(virtualPath.VirtualPathString, virtualPathDependencies);
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x0014E09A File Offset: 0x0014C29A
		public virtual CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			if (this._previous == null)
			{
				return null;
			}
			return this._previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x0014E0B4 File Offset: 0x0014C2B4
		internal CacheDependency GetCacheDependency(VirtualPath virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			return this.GetCacheDependency(virtualPath.VirtualPathString, virtualPathDependencies, utcStart);
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x0014E0C4 File Offset: 0x0014C2C4
		public virtual bool FileExists(string virtualPath)
		{
			return this._previous != null && this._previous.FileExists(virtualPath);
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x0014E0DC File Offset: 0x0014C2DC
		internal bool FileExists(VirtualPath virtualPath)
		{
			return this.FileExists(virtualPath.VirtualPathString);
		}

		// Token: 0x060060C8 RID: 24776 RVA: 0x0014E0EA File Offset: 0x0014C2EA
		public virtual bool DirectoryExists(string virtualDir)
		{
			return this._previous != null && this._previous.DirectoryExists(virtualDir);
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x0014E102 File Offset: 0x0014C302
		internal bool DirectoryExists(VirtualPath virtualDir)
		{
			return this.DirectoryExists(virtualDir.VirtualPathString);
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x0014E110 File Offset: 0x0014C310
		public virtual VirtualFile GetFile(string virtualPath)
		{
			if (this._previous == null)
			{
				return null;
			}
			return this._previous.GetFile(virtualPath);
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x0014E128 File Offset: 0x0014C328
		internal VirtualFile GetFile(VirtualPath virtualPath)
		{
			return this.GetFileWithCheck(virtualPath.VirtualPathString);
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x0014E138 File Offset: 0x0014C338
		internal VirtualFile GetFileWithCheck(string virtualPath)
		{
			VirtualFile file = this.GetFile(virtualPath);
			if (file == null)
			{
				return null;
			}
			if (!StringUtil.EqualsIgnoreCase(virtualPath, file.VirtualPath))
			{
				throw new HttpException(SR.GetString("Bad_VirtualPath_in_VirtualFileBase", new object[]
				{
					"VirtualFile",
					file.VirtualPath,
					virtualPath
				}));
			}
			return file;
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x0014E18C File Offset: 0x0014C38C
		public virtual VirtualDirectory GetDirectory(string virtualDir)
		{
			if (this._previous == null)
			{
				return null;
			}
			return this._previous.GetDirectory(virtualDir);
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x0014E1A4 File Offset: 0x0014C3A4
		internal VirtualDirectory GetDirectory(VirtualPath virtualDir)
		{
			return this.GetDirectoryWithCheck(virtualDir.VirtualPathString);
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x0014E1B4 File Offset: 0x0014C3B4
		internal VirtualDirectory GetDirectoryWithCheck(string virtualPath)
		{
			VirtualDirectory directory = this.GetDirectory(virtualPath);
			if (directory == null)
			{
				return null;
			}
			if (!StringUtil.EqualsIgnoreCase(virtualPath, directory.VirtualPath))
			{
				throw new HttpException(SR.GetString("Bad_VirtualPath_in_VirtualFileBase", new object[]
				{
					"VirtualDirectory",
					directory.VirtualPath,
					virtualPath
				}));
			}
			return directory;
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetCacheKey(string virtualPath)
		{
			return null;
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x0014E208 File Offset: 0x0014C408
		internal string GetCacheKey(VirtualPath virtualPath)
		{
			return this.GetCacheKey(virtualPath.VirtualPathString);
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x0014E218 File Offset: 0x0014C418
		public virtual string CombineVirtualPaths(string basePath, string relativePath)
		{
			string basepath = null;
			if (!string.IsNullOrEmpty(basePath))
			{
				basepath = UrlPath.GetDirectory(basePath);
			}
			return UrlPath.Combine(basepath, relativePath);
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x0014E240 File Offset: 0x0014C440
		internal VirtualPath CombineVirtualPaths(VirtualPath basePath, VirtualPath relativePath)
		{
			string virtualPath = this.CombineVirtualPaths(basePath.VirtualPathString, relativePath.VirtualPathString);
			return VirtualPath.Create(virtualPath);
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x0014E268 File Offset: 0x0014C468
		public static Stream OpenFile(string virtualPath)
		{
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			VirtualFile fileWithCheck = virtualPathProvider.GetFileWithCheck(virtualPath);
			return fileWithCheck.Open();
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x0014E289 File Offset: 0x0014C489
		internal static Stream OpenFile(VirtualPath virtualPath)
		{
			return VirtualPathProvider.OpenFile(virtualPath.VirtualPathString);
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x0014E298 File Offset: 0x0014C498
		internal static CacheDependency GetCacheDependency(VirtualPath virtualPath)
		{
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			return virtualPathProvider.GetCacheDependency(virtualPath, new SingleObjectCollection(virtualPath.VirtualPathString), DateTime.MaxValue);
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x0014E2C4 File Offset: 0x0014C4C4
		internal static VirtualPath CombineVirtualPathsInternal(VirtualPath basePath, VirtualPath relativePath)
		{
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			if (virtualPathProvider != null)
			{
				return virtualPathProvider.CombineVirtualPaths(basePath, relativePath);
			}
			return basePath.Parent.Combine(relativePath);
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x0014E2F0 File Offset: 0x0014C4F0
		internal static bool DirectoryExistsNoThrow(string virtualDir)
		{
			bool result;
			try
			{
				result = HostingEnvironment.VirtualPathProvider.DirectoryExists(virtualDir);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x0014E324 File Offset: 0x0014C524
		internal static bool DirectoryExistsNoThrow(VirtualPath virtualDir)
		{
			return VirtualPathProvider.DirectoryExistsNoThrow(virtualDir.VirtualPathString);
		}

		// Token: 0x04003269 RID: 12905
		private VirtualPathProvider _previous;
	}
}
