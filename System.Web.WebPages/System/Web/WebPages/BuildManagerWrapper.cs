using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Util;
using System.Xml.Linq;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages
{
	// Token: 0x02000017 RID: 23
	internal sealed class BuildManagerWrapper : IVirtualPathFactory
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000037E8 File Offset: 0x000019E8
		public BuildManagerWrapper() : this(() => HostingEnvironment.VirtualPathProvider, new VirtualPathUtilityWrapper())
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003824 File Offset: 0x00001A24
		public BuildManagerWrapper(VirtualPathProvider vpp, IVirtualPathUtility virtualPathUtility) : this(() => vpp, virtualPathUtility)
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003858 File Offset: 0x00001A58
		public BuildManagerWrapper(Func<VirtualPathProvider> vppFunc, IVirtualPathUtility virtualPathUtility)
		{
			this._vppFunc = vppFunc;
			this._virtualPathUtility = virtualPathUtility;
			this._isPrecompiled = this.IsNonUpdatablePrecompiledApp();
			if (!this._isPrecompiled)
			{
				this._vppCache = new FileExistenceCache(vppFunc, 1000);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003893 File Offset: 0x00001A93
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000038A4 File Offset: 0x00001AA4
		public IEnumerable<string> SupportedExtensions
		{
			get
			{
				return this._supportedExtensions ?? WebPageHttpHandler.GetRegisteredExtensions();
			}
			set
			{
				this._supportedExtensions = value;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000038AD File Offset: 0x00001AAD
		public bool Exists(string virtualPath)
		{
			if (this._isPrecompiled)
			{
				return this.ExistsInPrecompiledSite(virtualPath);
			}
			return this.ExistsInVpp(virtualPath);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000038C8 File Offset: 0x00001AC8
		internal bool IsNonUpdatablePrecompiledApp()
		{
			VirtualPathProvider virtualPathProvider = this._vppFunc();
			return virtualPathProvider != null && BuildManagerWrapper.IsNonUpdateablePrecompiledApp(virtualPathProvider, this._virtualPathUtility);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000038F4 File Offset: 0x00001AF4
		internal static bool IsNonUpdateablePrecompiledApp(VirtualPathProvider vpp, IVirtualPathUtility virtualPathUtility)
		{
			string virtualPath = virtualPathUtility.ToAbsolute("~/PrecompiledApp.config");
			if (!vpp.FileExists(virtualPath))
			{
				return false;
			}
			XDocument xdocument;
			using (Stream stream = vpp.GetFile(virtualPath).Open())
			{
				try
				{
					xdocument = XDocument.Load(stream);
				}
				catch
				{
					return false;
				}
			}
			if (xdocument.Root == null || !xdocument.Root.Name.LocalName.Equals("precompiledApp", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			XAttribute xattribute = xdocument.Root.Attribute("updatable");
			bool flag;
			return xattribute != null && bool.TryParse(xattribute.Value, out flag) && !flag;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000039B8 File Offset: 0x00001BB8
		private bool ExistsInPrecompiledSite(string virtualPath)
		{
			string keyFromVirtualPath = BuildManagerWrapper.GetKeyFromVirtualPath(virtualPath);
			BuildManagerWrapper.BuildManagerResult buildManagerResult = (BuildManagerWrapper.BuildManagerResult)HttpRuntime.Cache.Get(keyFromVirtualPath);
			if (buildManagerResult == null)
			{
				IWebObjectFactory objectFactory = this.GetObjectFactory(virtualPath);
				buildManagerResult = new BuildManagerWrapper.BuildManagerResult
				{
					ObjectFactory = objectFactory,
					Exists = (objectFactory != null)
				};
				HttpRuntime.Cache.Add(keyFromVirtualPath, buildManagerResult, null, Cache.NoAbsoluteExpiration, BuildManagerWrapper._objectFactoryCacheDuration, CacheItemPriority.Low, null);
			}
			return buildManagerResult.Exists;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003A24 File Offset: 0x00001C24
		private bool ExistsInVpp(string virtualPath)
		{
			return this._vppCache.FileExists(virtualPath);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003A32 File Offset: 0x00001C32
		private IWebObjectFactory GetObjectFactory(string virtualPath)
		{
			if (this.IsPathExtensionSupported(virtualPath))
			{
				return BuildManager.GetObjectFactory(virtualPath, false);
			}
			return null;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003A46 File Offset: 0x00001C46
		public object CreateInstance(string virtualPath)
		{
			return this.CreateInstanceOfType<object>(virtualPath);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003A50 File Offset: 0x00001C50
		public T CreateInstanceOfType<T>(string virtualPath) where T : class
		{
			if (this._isPrecompiled)
			{
				BuildManagerWrapper.BuildManagerResult buildManagerResult = (BuildManagerWrapper.BuildManagerResult)HttpRuntime.Cache.Get(BuildManagerWrapper.GetKeyFromVirtualPath(virtualPath));
				if (buildManagerResult != null)
				{
					return buildManagerResult.ObjectFactory.CreateInstance() as T;
				}
			}
			return (T)((object)BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(T)));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003AAC File Offset: 0x00001CAC
		public bool IsPathExtensionSupported(string virtualPath)
		{
			string extension = PathUtil.GetExtension(virtualPath);
			return !string.IsNullOrEmpty(extension) && this.SupportedExtensions.Contains(extension.Substring(1), StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003AE4 File Offset: 0x00001CE4
		private static string GetKeyFromVirtualPath(string virtualPath)
		{
			return BuildManagerWrapper.KeyGuid.ToString() + "_" + virtualPath;
		}

		// Token: 0x04000033 RID: 51
		internal static readonly Guid KeyGuid = Guid.NewGuid();

		// Token: 0x04000034 RID: 52
		private static readonly TimeSpan _objectFactoryCacheDuration = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000035 RID: 53
		private readonly IVirtualPathUtility _virtualPathUtility;

		// Token: 0x04000036 RID: 54
		private readonly Func<VirtualPathProvider> _vppFunc;

		// Token: 0x04000037 RID: 55
		private readonly bool _isPrecompiled;

		// Token: 0x04000038 RID: 56
		private readonly FileExistenceCache _vppCache;

		// Token: 0x04000039 RID: 57
		private IEnumerable<string> _supportedExtensions;

		// Token: 0x02000018 RID: 24
		private class BuildManagerResult
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000CB RID: 203 RVA: 0x00003B2E File Offset: 0x00001D2E
			// (set) Token: 0x060000CC RID: 204 RVA: 0x00003B36 File Offset: 0x00001D36
			public bool Exists { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000CD RID: 205 RVA: 0x00003B3F File Offset: 0x00001D3F
			// (set) Token: 0x060000CE RID: 206 RVA: 0x00003B47 File Offset: 0x00001D47
			public IWebObjectFactory ObjectFactory { get; set; }
		}
	}
}
