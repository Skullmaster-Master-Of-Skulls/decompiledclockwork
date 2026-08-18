using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.WebPages.Deployment.Resources;
using Microsoft.Internal.Web.Utils;
using Microsoft.Web.Infrastructure;

namespace System.Web.WebPages.Deployment
{
	// Token: 0x0200000C RID: 12
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PreApplicationStartCode
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003504 File Offset: 0x00001704
		public static void Start()
		{
			if (PreApplicationStartCode._startWasCalled)
			{
				return;
			}
			PreApplicationStartCode._startWasCalled = true;
			PreApplicationStartCode.StartCore();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000351C File Offset: 0x0000171C
		internal static bool StartCore()
		{
			BuildManagerWrapper buildManager = new BuildManagerWrapper();
			NameValueCollection appSettings = WebConfigurationManager.AppSettings;
			Action<Version> loadWebPages = new Action<Version>(PreApplicationStartCode.LoadWebPages);
			Action registerForChangeNotification = new Action(PreApplicationStartCode.RegisterForChangeNotifications);
			IEnumerable<AssemblyName> loadedAssemblies = AssemblyUtils.GetLoadedAssemblies();
			return PreApplicationStartCode.StartCore(PreApplicationStartCode._physicalFileSystem, HttpRuntime.AppDomainAppPath, HttpRuntime.BinDirectory, appSettings, loadedAssemblies, buildManager, loadWebPages, registerForChangeNotification, null);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003574 File Offset: 0x00001774
		internal static bool StartCore(IFileSystem fileSystem, string appDomainAppPath, string binDirectory, NameValueCollection appSettings, IEnumerable<AssemblyName> loadedAssemblies, IBuildManager buildManager, Action<Version> loadWebPages, Action registerForChangeNotification, Func<string, AssemblyName> getAssemblyNameThunk = null)
		{
			if (WebPagesDeployment.IsExplicitlyDisabled(appSettings))
			{
				return false;
			}
			Version maxWebPagesVersion = AssemblyUtils.GetMaxWebPagesVersion(loadedAssemblies);
			if (AssemblyUtils.ThisAssemblyName.Version != maxWebPagesVersion)
			{
				return false;
			}
			bool flag = WebPagesDeployment.IsEnabled(fileSystem, appDomainAppPath, appSettings);
			Version versionFromBin = AssemblyUtils.GetVersionFromBin(binDirectory, fileSystem, getAssemblyNameThunk);
			Version versionFromConfig = WebPagesDeployment.GetVersionFromConfig(appSettings);
			Version version;
			if ((version = versionFromConfig) == null)
			{
				version = (versionFromBin ?? AssemblyUtils.WebPagesV1Version);
			}
			Version version2 = version;
			if (versionFromBin != null && versionFromBin != version2)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ConfigurationResources.WebPagesVersionConflict, new object[]
				{
					version2,
					versionFromBin
				}));
			}
			if (versionFromBin != null)
			{
				return false;
			}
			if (!flag)
			{
				registerForChangeNotification();
				return false;
			}
			if (AssemblyUtils.IsVersionAvailable(loadedAssemblies, version2))
			{
				PreApplicationStartCode.InvalidateCompilationResultsIfVersionChanged(buildManager, fileSystem, binDirectory, version2);
				loadWebPages(version2);
				return true;
			}
			if (version2 == AssemblyUtils.WebPagesV1Version && versionFromConfig == null && versionFromBin == null)
			{
				throw new InvalidOperationException(ConfigurationResources.WebPagesImplicitVersionFailure);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ConfigurationResources.WebPagesVersionNotFound, new object[]
			{
				version2,
				AssemblyUtils.ThisAssemblyName.Version
			}));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000036A4 File Offset: 0x000018A4
		private static void InvalidateCompilationResultsIfVersionChanged(IBuildManager buildManager, IFileSystem fileSystem, string binDirectory, Version currentVersion)
		{
			Version previousRuntimeVersion = WebPagesDeployment.GetPreviousRuntimeVersion(buildManager);
			WebPagesDeployment.PersistRuntimeVersion(buildManager, currentVersion);
			if (previousRuntimeVersion == null)
			{
				return;
			}
			if (previousRuntimeVersion != currentVersion)
			{
				WebPagesDeployment.ForceRecompile(fileSystem, binDirectory);
				HttpCompileException ex = new HttpCompileException(ConfigurationResources.WebPagesVersionChanges);
				ex.Data["WebPages.VersionChange"] = true;
				throw ex;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000036FC File Offset: 0x000018FC
		internal static ICollection<MethodInfo> GetPreStartInitMethodsFromAssemblyCollection(IEnumerable<Assembly> assemblies)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (Assembly assembly in assemblies)
			{
				PreApplicationStartMethodAttribute[] array = null;
				try
				{
					array = (PreApplicationStartMethodAttribute[])assembly.GetCustomAttributes(typeof(PreApplicationStartMethodAttribute), true);
				}
				catch
				{
				}
				if (array != null && array.Length != 0)
				{
					PreApplicationStartMethodAttribute preApplicationStartMethodAttribute = array[0];
					MethodInfo methodInfo = null;
					if (preApplicationStartMethodAttribute.Type != null && !string.IsNullOrEmpty(preApplicationStartMethodAttribute.MethodName) && preApplicationStartMethodAttribute.Type.Assembly == assembly)
					{
						methodInfo = PreApplicationStartCode.FindPreStartInitMethod(preApplicationStartMethodAttribute.Type, preApplicationStartMethodAttribute.MethodName);
					}
					if (methodInfo != null)
					{
						list.Add(methodInfo);
					}
				}
			}
			return list;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000037DC File Offset: 0x000019DC
		internal static MethodInfo FindPreStartInitMethod(Type type, string methodName)
		{
			MethodInfo result = null;
			if (type.IsPublic)
			{
				result = type.GetMethod(methodName, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null);
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003808 File Offset: 0x00001A08
		private static void RegisterForChangeNotifications()
		{
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			CacheDependency dependencies = new CacheDependency(appDomainAppPath, DateTime.UtcNow);
			string key = WebPagesDeployment.CacheKeyPrefix + appDomainAppPath;
			HttpRuntime.Cache.Insert(key, appDomainAppPath, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(PreApplicationStartCode.OnChanged));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003857 File Offset: 0x00001A57
		private static void OnChanged(string key, object value, CacheItemRemovedReason reason)
		{
			if (reason != CacheItemRemovedReason.DependencyChanged)
			{
				return;
			}
			if (WebPagesDeployment.AppRootContainsWebPagesFile(PreApplicationStartCode._physicalFileSystem, HttpRuntime.AppDomainAppPath))
			{
				InfrastructureHelper.UnloadAppDomain();
				return;
			}
			PreApplicationStartCode.RegisterForChangeNotifications();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000387C File Offset: 0x00001A7C
		private static void LoadWebPages(Version version)
		{
			IEnumerable<AssemblyName> assembliesForVersion = AssemblyUtils.GetAssembliesForVersion(version);
			IEnumerable<Assembly> enumerable = assembliesForVersion.Select(new Func<AssemblyName, Assembly>(PreApplicationStartCode.LoadAssembly));
			foreach (Assembly assembly in enumerable)
			{
				BuildManager.AddReferencedAssembly(assembly);
			}
			foreach (MethodInfo methodInfo in PreApplicationStartCode.GetPreStartInitMethodsFromAssemblyCollection(enumerable))
			{
				methodInfo.Invoke(null, null);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003928 File Offset: 0x00001B28
		private static Assembly LoadAssembly(AssemblyName name)
		{
			return Assembly.Load(name);
		}

		// Token: 0x04000022 RID: 34
		private const string ToolingIndicatorKey = "WebPages.VersionChange";

		// Token: 0x04000023 RID: 35
		private static readonly IFileSystem _physicalFileSystem = new PhysicalFileSystem();

		// Token: 0x04000024 RID: 36
		private static bool _startWasCalled;
	}
}
