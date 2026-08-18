using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.WebPages.Deployment.Resources;
using Microsoft.Internal.Web.Utils;
using Microsoft.Win32;

namespace System.Web.WebPages.Deployment
{
	// Token: 0x0200000B RID: 11
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WebPagesDeployment
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002ECE File Offset: 0x000010CE
		public static Version GetVersionWithoutEnabledCheck(string path)
		{
			return WebPagesDeployment.GetVersionWithoutEnabledCheckInternal(path, AssemblyUtils.WebPagesV1Version);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002EDB File Offset: 0x000010DB
		public static Version GetExplicitWebPagesVersion(string path)
		{
			return WebPagesDeployment.GetVersionWithoutEnabledCheckInternal(path, null);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002EE4 File Offset: 0x000010E4
		[Obsolete("This method is obsolete and is meant for legacy code. Use GetVersionWithoutEnabled instead.")]
		public static Version GetVersion(string path)
		{
			return WebPagesDeployment.GetObsoleteVersionInternal(path, WebPagesDeployment.GetAppSettings(path), new PhysicalFileSystem());
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002EF8 File Offset: 0x000010F8
		internal static Version GetObsoleteVersionInternal(string path, NameValueCollection configuration, IFileSystem fileSystem)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "path");
			}
			string binDirectory = WebPagesDeployment.GetBinDirectory(path);
			Version versionFromBin = AssemblyUtils.GetVersionFromBin(binDirectory, WebPagesDeployment._fileSystem, null);
			Version versionInternal = WebPagesDeployment.GetVersionInternal(configuration, versionFromBin, null);
			if (versionInternal != null)
			{
				return versionInternal;
			}
			if (WebPagesDeployment.AppRootContainsWebPagesFile(fileSystem, path))
			{
				return AssemblyUtils.WebPagesV1Version;
			}
			return null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002F55 File Offset: 0x00001155
		public static Version GetMaxVersion()
		{
			return AssemblyUtils.GetMaxWebPagesVersion();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F5C File Offset: 0x0000115C
		public static bool IsEnabled(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "path");
			}
			return WebPagesDeployment.IsEnabled(WebPagesDeployment._fileSystem, path, WebPagesDeployment.GetAppSettings(path));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F87 File Offset: 0x00001187
		public static bool IsExplicitlyDisabled(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "path");
			}
			return WebPagesDeployment.IsExplicitlyDisabled(WebPagesDeployment.GetAppSettings(path));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002FAC File Offset: 0x000011AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IDictionary<string, Version> GetIncompatibleDependencies(string appPath)
		{
			if (string.IsNullOrEmpty(appPath))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "appPath");
			}
			string configPath = Path.Combine(appPath, "web.config");
			IDictionary<string, IEnumerable<string>> binAssemblyReferences = AppDomainHelper.GetBinAssemblyReferences(appPath, configPath);
			return AssemblyUtils.GetAssembliesMatchingOtherVersions(binAssemblyReferences);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FEC File Offset: 0x000011EC
		internal static bool IsExplicitlyDisabled(NameValueCollection appSettings)
		{
			bool? enabled = WebPagesDeployment.GetEnabled(appSettings);
			return enabled != null && !enabled.Value;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003018 File Offset: 0x00001218
		internal static bool IsEnabled(IFileSystem fileSystem, string path, NameValueCollection appSettings)
		{
			bool? enabled = WebPagesDeployment.GetEnabled(appSettings);
			if (enabled == null)
			{
				return WebPagesDeployment.AppRootContainsWebPagesFile(fileSystem, path);
			}
			return enabled.Value;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003044 File Offset: 0x00001244
		private static bool? GetEnabled(NameValueCollection appSettings)
		{
			string value = appSettings.Get("webpages:Enabled");
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			return new bool?(bool.Parse(value));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000307A File Offset: 0x0000127A
		internal static Version GetVersionInternal(NameValueCollection appSettings, Version binVersion, Version defaultVersion)
		{
			return WebPagesDeployment.GetVersionFromConfig(appSettings) ?? (binVersion ?? defaultVersion);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000308C File Offset: 0x0000128C
		private static Version GetVersionWithoutEnabledCheckInternal(string path, Version defaultVersion)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "path");
			}
			string binDirectory = WebPagesDeployment.GetBinDirectory(path);
			Version versionFromBin = AssemblyUtils.GetVersionFromBin(binDirectory, WebPagesDeployment._fileSystem, null);
			return WebPagesDeployment.GetVersionInternal(WebPagesDeployment.GetAppSettings(path), versionFromBin, defaultVersion);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000030D4 File Offset: 0x000012D4
		public static string GetAssemblyPath(Version version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			string text = string.Format(CultureInfo.InvariantCulture, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ASP.NET Web Pages\\v{0}.{1}", new object[]
			{
				version.Major,
				version.Minor
			});
			object value = Registry.GetValue(text, "InstallPath", WebPagesDeployment._installPathNotFound);
			if (value == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ConfigurationResources.WebPagesRegistryKeyDoesNotExist, new object[]
				{
					text
				}));
			}
			if (value == WebPagesDeployment._installPathNotFound)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ConfigurationResources.InstallPathNotFound, new object[]
				{
					text
				}));
			}
			return Path.Combine((string)value, "Assemblies");
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000319A File Offset: 0x0000139A
		public static IEnumerable<AssemblyName> GetWebPagesAssemblies()
		{
			return AssemblyUtils.GetAssembliesForVersion(AssemblyUtils.ThisAssemblyName.Version);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000031AC File Offset: 0x000013AC
		private static NameValueCollection GetAppSettings(string path)
		{
			if (path.StartsWith("~/", StringComparison.Ordinal))
			{
				return (NameValueCollection)WebConfigurationManager.GetSection("appSettings", path);
			}
			Configuration configuration = WebConfigurationManager.OpenMappedWebConfiguration(new WebConfigurationFileMap
			{
				VirtualDirectories = 
				{
					{
						"/",
						new VirtualDirectoryMapping(path, true)
					}
				}
			}, "/");
			AppSettingsSection appSettings = configuration.AppSettings;
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (object obj in appSettings.Settings)
			{
				KeyValueConfigurationElement keyValueConfigurationElement = (KeyValueConfigurationElement)obj;
				nameValueCollection.Add(keyValueConfigurationElement.Key, keyValueConfigurationElement.Value);
			}
			return nameValueCollection;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003270 File Offset: 0x00001470
		internal static Version GetVersionFromConfig(NameValueCollection appSettings)
		{
			string text = appSettings.Get("webpages:Version");
			if (!string.IsNullOrEmpty(text))
			{
				Version version = new Version(text);
				if (version.Build == -1 || version.Revision == -1)
				{
					version = new Version(version.Major, version.Minor, (version.Build == -1) ? 0 : version.Build, (version.Revision == -1) ? 0 : version.Revision);
				}
				return version;
			}
			return null;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000032E4 File Offset: 0x000014E4
		internal static bool AppRootContainsWebPagesFile(IFileSystem fileSystem, string path)
		{
			IEnumerable<string> source = fileSystem.EnumerateFiles(path);
			return source.Any(new Func<string, bool>(WebPagesDeployment.IsWebPagesFile));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000330C File Offset: 0x0000150C
		private static bool IsWebPagesFile(string file)
		{
			string extension = Path.GetExtension(file);
			return WebPagesDeployment._webPagesExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003330 File Offset: 0x00001530
		private static string GetBinDirectory(string path)
		{
			if (HostingEnvironment.IsHosted)
			{
				return HttpRuntime.BinDirectory;
			}
			return Path.Combine(path, "bin");
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000334C File Offset: 0x0000154C
		internal static Version GetPreviousRuntimeVersion(IBuildManager buildManagerFileSystem)
		{
			string cachedFileName = WebPagesDeployment.GetCachedFileName();
			try
			{
				Stream stream = buildManagerFileSystem.ReadCachedFile(cachedFileName);
				if (stream == null)
				{
					return null;
				}
				using (StreamReader streamReader = new StreamReader(stream))
				{
					string input = streamReader.ReadLine();
					Version result;
					if (Version.TryParse(input, out result))
					{
						return result;
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000033C0 File Offset: 0x000015C0
		internal static void PersistRuntimeVersion(IBuildManager buildManager, Version version)
		{
			string cachedFileName = WebPagesDeployment.GetCachedFileName();
			try
			{
				Stream stream = buildManager.CreateCachedFile(cachedFileName);
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					streamWriter.WriteLine(version.ToString());
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000341C File Offset: 0x0000161C
		internal static void ForceRecompile(IFileSystem fileSystem, string binDirectory)
		{
			string path = Path.Combine(binDirectory, "WebPagesRecompilation.deleteme");
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(fileSystem.OpenFile(path)))
				{
					streamWriter.WriteLine();
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003478 File Offset: 0x00001678
		private static string GetCachedFileName()
		{
			return typeof(WebPagesDeployment).Namespace;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000348C File Offset: 0x0000168C
		private static string RemoveTrailingSlash(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				path = path.TrimEnd(new char[]
				{
					Path.DirectorySeparatorChar
				});
			}
			return path;
		}

		// Token: 0x0400001A RID: 26
		private const string AppSettingsVersionKey = "webpages:Version";

		// Token: 0x0400001B RID: 27
		private const string AppSettingsEnabledKey = "webpages:Enabled";

		// Token: 0x0400001C RID: 28
		private const string ForceRecompilationFile = "WebPagesRecompilation.deleteme";

		// Token: 0x0400001D RID: 29
		private const string WebPagesRegistryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ASP.NET Web Pages\\v{0}.{1}";

		// Token: 0x0400001E RID: 30
		internal static readonly string CacheKeyPrefix = "__System.Web.WebPages.Deployment__";

		// Token: 0x0400001F RID: 31
		private static readonly string[] _webPagesExtensions = new string[]
		{
			".cshtml",
			".vbhtml"
		};

		// Token: 0x04000020 RID: 32
		private static readonly object _installPathNotFound = new object();

		// Token: 0x04000021 RID: 33
		private static readonly IFileSystem _fileSystem = new PhysicalFileSystem();
	}
}
