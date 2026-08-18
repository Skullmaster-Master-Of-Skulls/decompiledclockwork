using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Routing;
using System.Web.WebPages.ApplicationParts;
using System.Web.WebPages.Resources;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages
{
	// Token: 0x02000008 RID: 8
	public class ApplicationPart
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000026A8 File Offset: 0x000008A8
		public ApplicationPart(Assembly assembly, string rootVirtualPath) : this(new ResourceAssembly(assembly), rootVirtualPath)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000272C File Offset: 0x0000092C
		internal ApplicationPart(IResourceAssembly assembly, string rootVirtualPath)
		{
			if (string.IsNullOrEmpty(rootVirtualPath))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "rootVirtualPath");
			}
			if (!rootVirtualPath.EndsWith("/", StringComparison.Ordinal))
			{
				rootVirtualPath += "/";
			}
			this.Assembly = assembly;
			this.RootVirtualPath = rootVirtualPath;
			this._applicationPartResources = new Lazy<IDictionary<string, string>>(() => this.Assembly.GetManifestResourceNames().ToDictionary((string key) => key, (string key) => key, StringComparer.OrdinalIgnoreCase));
			this._applicationPartName = new Lazy<string>(() => this.Assembly.Name);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000027BC File Offset: 0x000009BC
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000027C4 File Offset: 0x000009C4
		internal IResourceAssembly Assembly { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000027CD File Offset: 0x000009CD
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000027D5 File Offset: 0x000009D5
		internal string RootVirtualPath { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000027DE File Offset: 0x000009DE
		internal string Name
		{
			get
			{
				return this._applicationPartName.Value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000027EB File Offset: 0x000009EB
		internal IDictionary<string, string> ApplicationPartResources
		{
			get
			{
				return this._applicationPartResources.Value;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027F8 File Offset: 0x000009F8
		public static void Register(ApplicationPart applicationPart)
		{
			ApplicationPart._initApplicationPart.EnsurePerformed();
			ApplicationPart._partRegistry.Register(applicationPart);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002810 File Offset: 0x00000A10
		public static string ProcessVirtualPath(Assembly assembly, string baseVirtualPath, string virtualPath)
		{
			if (ApplicationPart._partRegistry == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.ApplicationPart_ModuleNotRegistered, new object[]
				{
					assembly
				}));
			}
			ApplicationPart applicationPart = ApplicationPart._partRegistry[new ResourceAssembly(assembly)];
			if (applicationPart == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.ApplicationPart_ModuleNotRegistered, new object[]
				{
					assembly
				}));
			}
			return applicationPart.ProcessVirtualPath(baseVirtualPath, virtualPath);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002882 File Offset: 0x00000A82
		internal static IEnumerable<ApplicationPart> GetRegisteredParts()
		{
			ApplicationPart._initApplicationPart.EnsurePerformed();
			return ApplicationPart._partRegistry.RegisteredParts;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000289C File Offset: 0x00000A9C
		private string ProcessVirtualPath(string baseVirtualPath, string virtualPath)
		{
			virtualPath = ApplicationPart.ResolveVirtualPath(this.RootVirtualPath, baseVirtualPath, virtualPath);
			if (!virtualPath.StartsWith(this.RootVirtualPath, StringComparison.OrdinalIgnoreCase))
			{
				return virtualPath;
			}
			string virtualPath2 = "~/" + virtualPath.Substring(this.RootVirtualPath.Length);
			string resourceNameFromVirtualPath = this.GetResourceNameFromVirtualPath(virtualPath2);
			if (!this.ApplicationPartResources.ContainsKey(resourceNameFromVirtualPath))
			{
				return virtualPath;
			}
			return this.GetResourceVirtualPath(virtualPath);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002904 File Offset: 0x00000B04
		internal static string ResolveVirtualPath(string applicationRoot, string baseVirtualPath, string virtualPath)
		{
			if (virtualPath.StartsWith("@/", StringComparison.OrdinalIgnoreCase))
			{
				return applicationRoot + virtualPath.Substring("@/".Length);
			}
			return VirtualPathUtility.Combine(baseVirtualPath, virtualPath);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002934 File Offset: 0x00000B34
		internal Stream GetResourceStream(string virtualPath)
		{
			string resourceNameFromVirtualPath = this.GetResourceNameFromVirtualPath(virtualPath);
			string name;
			if (this.ApplicationPartResources.TryGetValue(resourceNameFromVirtualPath, out name))
			{
				return this.Assembly.GetManifestResourceStream(name);
			}
			return null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002967 File Offset: 0x00000B67
		private string GetResourceNameFromVirtualPath(string virtualPath)
		{
			return ApplicationPart.GetResourceNameFromVirtualPath(this.Name, virtualPath);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002978 File Offset: 0x00000B78
		internal static string GetResourceNameFromVirtualPath(string moduleName, string virtualPath)
		{
			if (!virtualPath.StartsWith("~/", StringComparison.Ordinal))
			{
				virtualPath = "~/" + virtualPath;
			}
			string text = VirtualPathUtility.GetDirectory(virtualPath);
			if (text.Length >= 2)
			{
				text = text.Substring(2);
			}
			text = text.Replace('/', '.');
			text = text.Replace(' ', '_');
			string fileName = Path.GetFileName(virtualPath);
			return moduleName + "." + text + fileName;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029E2 File Offset: 0x00000BE2
		private string GetResourceVirtualPath(string virtualPath)
		{
			return ApplicationPart.GetResourceVirtualPath(this.Name, this.RootVirtualPath, virtualPath);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029F8 File Offset: 0x00000BF8
		internal static string GetResourceVirtualPath(string moduleName, string moduleRoot, string virtualPath)
		{
			virtualPath = virtualPath.Substring(moduleRoot.Length).TrimStart(new char[]
			{
				'/'
			});
			return "~/r.ashx/" + HttpUtility.UrlPathEncode(moduleName) + "/" + virtualPath;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A3C File Offset: 0x00000C3C
		private static void InitApplicationParts()
		{
			DictionaryBasedVirtualPathFactory dictionaryBasedVirtualPathFactory = new DictionaryBasedVirtualPathFactory();
			VirtualPathFactoryManager.RegisterVirtualPathFactory(dictionaryBasedVirtualPathFactory);
			ApplicationPart._partRegistry = new ApplicationPartRegistry(dictionaryBasedVirtualPathFactory);
			RouteTable.Routes.Add(new Route("r.ashx/{module}/{*path}", new ResourceRouteHandler(ApplicationPart._partRegistry)));
		}

		// Token: 0x04000007 RID: 7
		private const string ModuleRootSyntax = "@/";

		// Token: 0x04000008 RID: 8
		private const string ResourceVirtualPathRoot = "~/r.ashx/";

		// Token: 0x04000009 RID: 9
		private const string ResourceRoute = "r.ashx/{module}/{*path}";

		// Token: 0x0400000A RID: 10
		private static readonly LazyAction _initApplicationPart = new LazyAction(new Action(ApplicationPart.InitApplicationParts));

		// Token: 0x0400000B RID: 11
		private static ApplicationPartRegistry _partRegistry;

		// Token: 0x0400000C RID: 12
		private readonly Lazy<IDictionary<string, string>> _applicationPartResources;

		// Token: 0x0400000D RID: 13
		private readonly Lazy<string> _applicationPartName;
	}
}
