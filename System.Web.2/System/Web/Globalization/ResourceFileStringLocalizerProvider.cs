using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Security.Permissions;
using System.Web.Compilation;

namespace System.Web.Globalization
{
	// Token: 0x02000696 RID: 1686
	public sealed class ResourceFileStringLocalizerProvider : IStringLocalizerProvider
	{
		// Token: 0x06005131 RID: 20785 RVA: 0x00117918 File Offset: 0x00115B18
		public string GetLocalizedString(CultureInfo culture, string name, params object[] arguments)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			string stringSafely = this.GetStringSafely(name, culture);
			if (stringSafely != null)
			{
				return string.Format(stringSafely, arguments);
			}
			return stringSafely;
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x00117954 File Offset: 0x00115B54
		private string GetStringSafely(string name, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.EnsureResourceManager();
			string result = null;
			if (this._resourceManager == null)
			{
				return result;
			}
			string key = string.Format("n={0}&c={1}", name, culture.Name);
			if (this._missingManifestCache.ContainsKey(key))
			{
				return result;
			}
			try
			{
				result = (string)this._resourceManager.GetObject(name, culture);
			}
			catch (Exception)
			{
				this._missingManifestCache.TryAdd(key, null);
			}
			return result;
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x001179DC File Offset: 0x00115BDC
		private ResourceManager EnsureResourceManager()
		{
			if (this._loadedResourceAssembly)
			{
				return this._resourceManager;
			}
			Assembly localResourceAssembly = this.GetLocalResourceAssembly();
			if (localResourceAssembly != null)
			{
				this._resourceManager = new ResourceManager("DataAnnotation.Localization", localResourceAssembly);
				this._resourceManager.IgnoreCase = true;
			}
			this._loadedResourceAssembly = true;
			return this._resourceManager;
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x00117A34 File Offset: 0x00115C34
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private Assembly GetLocalResourceAssembly()
		{
			VirtualPath virtualDir = VirtualPath.Create(HttpRuntime.AppDomainAppVirtualPath);
			string localResourcesAssemblyName = BuildManager.GetLocalResourcesAssemblyName(virtualDir);
			BuildResult buildResultFromCache = BuildManager.GetBuildResultFromCache(localResourcesAssemblyName);
			if (buildResultFromCache != null)
			{
				return ((BuildResultCompiledAssembly)buildResultFromCache).ResultAssembly;
			}
			return null;
		}

		// Token: 0x04002AE7 RID: 10983
		private readonly ConcurrentDictionary<string, object> _missingManifestCache = new ConcurrentDictionary<string, object>();

		// Token: 0x04002AE8 RID: 10984
		private ResourceManager _resourceManager;

		// Token: 0x04002AE9 RID: 10985
		private bool _loadedResourceAssembly;

		// Token: 0x04002AEA RID: 10986
		public const string ResourceFileName = "DataAnnotation.Localization";
	}
}
