using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x020007FF RID: 2047
	internal static class BrowserCapabilitiesCompiler
	{
		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x060061BF RID: 25023 RVA: 0x0015548A File Offset: 0x0015368A
		// (set) Token: 0x060061C0 RID: 25024 RVA: 0x00155491 File Offset: 0x00153691
		internal static Assembly AspBrowserCapsFactoryAssembly { get; set; }

		// Token: 0x060061C1 RID: 25025 RVA: 0x0015549C File Offset: 0x0015369C
		static BrowserCapabilitiesCompiler()
		{
			Assembly assembly = null;
			string browserCapAssemblyPublicKeyToken = BrowserCapabilitiesCodeGenerator.BrowserCapAssemblyPublicKeyToken;
			if (browserCapAssemblyPublicKeyToken != null)
			{
				try
				{
					string str;
					if (MultiTargetingUtil.IsTargetFramework40OrAbove)
					{
						str = "4.0.0.0";
					}
					else
					{
						str = "2.0.0.0";
					}
					assembly = Assembly.Load("ASP.BrowserCapsFactory, Version=" + str + ", Culture=neutral, PublicKeyToken=" + browserCapAssemblyPublicKeyToken);
					BrowserCapabilitiesCompiler.AspBrowserCapsFactoryAssembly = assembly;
				}
				catch (FileNotFoundException)
				{
				}
			}
			if (assembly == null || !assembly.GlobalAssemblyCache)
			{
				BrowserCapabilitiesCompiler._browserCapabilitiesFactoryBaseType = typeof(BrowserCapabilitiesFactory);
				return;
			}
			BrowserCapabilitiesCompiler._browserCapabilitiesFactoryBaseType = assembly.GetType("ASP.BrowserCapabilitiesFactory", true);
		}

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x060061C2 RID: 25026 RVA: 0x0015554C File Offset: 0x0015374C
		internal static BrowserCapabilitiesFactoryBase BrowserCapabilitiesFactory
		{
			get
			{
				if (BrowserCapabilitiesCompiler._browserCapabilitiesFactoryBaseInstance != null)
				{
					return BrowserCapabilitiesCompiler._browserCapabilitiesFactoryBaseInstance;
				}
				Type browserCapabilitiesType = BrowserCapabilitiesCompiler.GetBrowserCapabilitiesType();
				object lockObject = BrowserCapabilitiesCompiler._lockObject;
				lock (lockObject)
				{
					if (BrowserCapabilitiesCompiler._browserCapabilitiesFactoryBaseInstance == null && browserCapabilitiesType != null)
					{
						BrowserCapabilitiesCompiler._browserCapabilitiesFactoryBaseInstance = (BrowserCapabilitiesFactoryBase)Activator.CreateInstance(browserCapabilitiesType);
					}
				}
				return BrowserCapabilitiesCompiler._browserCapabilitiesFactoryBaseInstance;
			}
		}

		// Token: 0x060061C3 RID: 25027 RVA: 0x001555C0 File Offset: 0x001537C0
		internal static Type GetBrowserCapabilitiesFactoryBaseType()
		{
			return BrowserCapabilitiesCompiler._browserCapabilitiesFactoryBaseType;
		}

		// Token: 0x060061C4 RID: 25028 RVA: 0x001555C8 File Offset: 0x001537C8
		internal static Type GetBrowserCapabilitiesType()
		{
			InternalSecurityPermissions.Unrestricted.Assert();
			BuildResult buildResult = null;
			try
			{
				buildResult = BuildManager.GetBuildResultFromCache("__browserCapabilitiesCompiler");
				if (buildResult == null)
				{
					DateTime utcNow = DateTime.UtcNow;
					VirtualDirectory directory = BrowserCapabilitiesCompiler.AppBrowsersVirtualDir.GetDirectory();
					string path = HostingEnvironment.MapPathInternal(BrowserCapabilitiesCompiler.AppBrowsersVirtualDir);
					if (directory != null && Directory.Exists(path))
					{
						ArrayList arrayList = new ArrayList();
						ArrayList arrayList2 = new ArrayList();
						bool flag = BrowserCapabilitiesCompiler.AddBrowserFilesToList(directory, arrayList, false);
						if (flag)
						{
							BrowserCapabilitiesCompiler.AddBrowserFilesToList(directory, arrayList2, true);
						}
						else
						{
							arrayList2 = arrayList;
						}
						if (arrayList2.Count > 0)
						{
							ApplicationBrowserCapabilitiesBuildProvider applicationBrowserCapabilitiesBuildProvider = new ApplicationBrowserCapabilitiesBuildProvider();
							foreach (object obj in arrayList)
							{
								string virtualPath = (string)obj;
								applicationBrowserCapabilitiesBuildProvider.AddFile(virtualPath);
							}
							BuildProvidersCompiler buildProvidersCompiler = new BuildProvidersCompiler(null, BuildManager.GenerateRandomAssemblyName("App_Browsers"));
							buildProvidersCompiler.SetBuildProviders(new SingleObjectCollection(applicationBrowserCapabilitiesBuildProvider));
							CompilerResults compilerResults = buildProvidersCompiler.PerformBuild();
							Assembly compiledAssembly = compilerResults.CompiledAssembly;
							Type type = compiledAssembly.GetType("ASP.ApplicationBrowserCapabilitiesFactory");
							buildResult = new BuildResultCompiledType(type);
							buildResult.VirtualPath = BrowserCapabilitiesCompiler.AppBrowsersVirtualDir;
							buildResult.AddVirtualPathDependencies(arrayList2);
							BuildManager.CacheBuildResult("__browserCapabilitiesCompiler", buildResult, utcNow);
						}
					}
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (buildResult == null)
			{
				return BrowserCapabilitiesCompiler._browserCapabilitiesFactoryBaseType;
			}
			return ((BuildResultCompiledType)buildResult).ResultType;
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x0015575C File Offset: 0x0015395C
		private static bool AddBrowserFilesToList(VirtualDirectory directory, IList list, bool doRecurse)
		{
			bool result = false;
			foreach (object obj in directory.Children)
			{
				VirtualFileBase virtualFileBase = (VirtualFileBase)obj;
				if (virtualFileBase.IsDirectory)
				{
					if (doRecurse)
					{
						BrowserCapabilitiesCompiler.AddBrowserFilesToList((VirtualDirectory)virtualFileBase, list, true);
					}
					result = true;
				}
				else
				{
					string extension = Path.GetExtension(virtualFileBase.Name);
					if (StringUtil.EqualsIgnoreCase(extension, ".browser"))
					{
						list.Add(virtualFileBase.VirtualPath);
					}
				}
			}
			return result;
		}

		// Token: 0x040032CD RID: 13005
		internal static readonly VirtualPath AppBrowsersVirtualDir = HttpRuntime.AppDomainAppVirtualPathObject.SimpleCombineWithDir("App_Browsers");

		// Token: 0x040032CE RID: 13006
		private const string browerCapabilitiesTypeName = "BrowserCapabilities";

		// Token: 0x040032CF RID: 13007
		private const string browerCapabilitiesCacheKey = "__browserCapabilitiesCompiler";

		// Token: 0x040032D0 RID: 13008
		private static Type _browserCapabilitiesFactoryBaseType;

		// Token: 0x040032D1 RID: 13009
		private static BrowserCapabilitiesFactoryBase _browserCapabilitiesFactoryBaseInstance;

		// Token: 0x040032D2 RID: 13010
		private static object _lockObject = new object();
	}
}
