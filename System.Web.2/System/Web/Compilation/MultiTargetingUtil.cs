using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Versioning;
using System.Security.Permissions;
using System.Web.Configuration;
using Microsoft.Build.Utilities;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace System.Web.Compilation
{
	// Token: 0x0200084C RID: 2124
	internal class MultiTargetingUtil
	{
		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x060064C3 RID: 25795 RVA: 0x00160E5F File Offset: 0x0015F05F
		// (set) Token: 0x060064C4 RID: 25796 RVA: 0x00160E6B File Offset: 0x0015F06B
		internal static FrameworkName TargetFrameworkName
		{
			get
			{
				MultiTargetingUtil.EnsureFrameworkNamesInitialized();
				return MultiTargetingUtil.s_targetFrameworkName;
			}
			set
			{
				MultiTargetingUtil.s_targetFrameworkName = value;
			}
		}

		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x060064C5 RID: 25797 RVA: 0x00160E73 File Offset: 0x0015F073
		internal static FrameworkName LatestFrameworkName
		{
			get
			{
				MultiTargetingUtil.EnsureFrameworkNamesInitialized();
				return MultiTargetingUtil.s_latestFrameworkName;
			}
		}

		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x060064C6 RID: 25798 RVA: 0x00160E7F File Offset: 0x0015F07F
		internal static List<FrameworkName> KnownFrameworkNames
		{
			get
			{
				MultiTargetingUtil.EnsureFrameworkNamesInitialized();
				return MultiTargetingUtil.s_knownFrameworkNames;
			}
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x00160E8C File Offset: 0x0015F08C
		internal static void EnsureFrameworkNamesInitialized()
		{
			if (MultiTargetingUtil.s_targetFrameworkName == null)
			{
				object obj = MultiTargetingUtil.s_targetFrameworkNameLock;
				lock (obj)
				{
					if (MultiTargetingUtil.s_targetFrameworkName == null)
					{
						MultiTargetingUtil.InitializeKnownAndLatestFrameworkNames();
						MultiTargetingUtil.InitializeTargetFrameworkName();
					}
				}
			}
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x00160EEC File Offset: 0x0015F0EC
		private static void InitializeKnownAndLatestFrameworkNames()
		{
			IList<string> supportedTargetFrameworks = ToolLocationHelper.GetSupportedTargetFrameworks();
			Version v = null;
			MultiTargetingUtil.s_knownFrameworkNames = new List<FrameworkName>();
			foreach (string frameworkName in supportedTargetFrameworks)
			{
				FrameworkName frameworkName2 = new FrameworkName(frameworkName);
				MultiTargetingUtil.s_knownFrameworkNames.Add(frameworkName2);
				Version frameworkNameVersion = MultiTargetingUtil.GetFrameworkNameVersion(frameworkName2);
				if (MultiTargetingUtil.s_latestFrameworkName == null || v < frameworkNameVersion)
				{
					MultiTargetingUtil.s_latestFrameworkName = frameworkName2;
					v = frameworkNameVersion;
				}
			}
		}

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x060064C9 RID: 25801 RVA: 0x00160F80 File Offset: 0x0015F180
		internal static string ConfigTargetFrameworkMoniker
		{
			get
			{
				if (!MultiTargetingUtil.s_initializedConfigTargetFrameworkMoniker)
				{
					object obj = MultiTargetingUtil.s_configTargetFrameworkMonikerLock;
					lock (obj)
					{
						if (!MultiTargetingUtil.s_initializedConfigTargetFrameworkMoniker)
						{
							RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
							CompilationSection compilation = appConfig.Compilation;
							string text = compilation.TargetFramework;
							if (text != null)
							{
								text = text.Trim();
							}
							MultiTargetingUtil.s_configTargetFrameworkMoniker = text;
							MultiTargetingUtil.s_initializedConfigTargetFrameworkMoniker = true;
						}
					}
				}
				return MultiTargetingUtil.s_configTargetFrameworkMoniker;
			}
		}

		// Token: 0x060064CA RID: 25802 RVA: 0x00160FFC File Offset: 0x0015F1FC
		private static void InitializeTargetFrameworkName()
		{
			string configTargetFrameworkMoniker = MultiTargetingUtil.ConfigTargetFrameworkMoniker;
			if (!MultiTargetingUtil.WebConfigExists)
			{
				MultiTargetingUtil.s_targetFrameworkName = MultiTargetingUtil.FrameworkNameV40;
				MultiTargetingUtil.ValidateCompilerVersionFor40AndAbove();
				return;
			}
			if (configTargetFrameworkMoniker != null)
			{
				MultiTargetingUtil.InitializeTargetFrameworkNameFor40AndAbove(configTargetFrameworkMoniker);
				return;
			}
			if (BuildManagerHost.SupportsMultiTargeting)
			{
				MultiTargetingUtil.InitializeTargetFrameworkNameFor20Or35();
				return;
			}
			MultiTargetingUtil.s_targetFrameworkName = MultiTargetingUtil.FrameworkNameV40;
		}

		// Token: 0x060064CB RID: 25803 RVA: 0x00161048 File Offset: 0x0015F248
		private static void ValidateTargetFrameworkMoniker(string targetFrameworkMoniker)
		{
			CompilationSection compilation = RuntimeConfig.GetAppConfig().Compilation;
			int lineNumber = compilation.ElementInformation.LineNumber;
			string source = compilation.ElementInformation.Source;
			try
			{
				string text = targetFrameworkMoniker;
				Version version = MultiTargetingUtil.GetVersion(targetFrameworkMoniker);
				if (version != null)
				{
					text = ".NETFramework,Version=v" + text;
				}
				MultiTargetingUtil.s_targetFrameworkName = MultiTargetingUtil.CreateFrameworkName(text);
			}
			catch (ArgumentException ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_target_framework_version", new object[]
				{
					MultiTargetingUtil.s_configTargetFrameworkAttributeName,
					targetFrameworkMoniker,
					ex.Message
				}), source, lineNumber);
			}
			Version frameworkNameVersion = MultiTargetingUtil.GetFrameworkNameVersion(MultiTargetingUtil.s_targetFrameworkName);
			if (frameworkNameVersion < MultiTargetingUtil.Version40)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_lower_target_version", new object[]
				{
					MultiTargetingUtil.s_configTargetFrameworkAttributeName
				}), source, lineNumber);
			}
			Version frameworkNameVersion2 = MultiTargetingUtil.GetFrameworkNameVersion(MultiTargetingUtil.LatestFrameworkName);
			if (frameworkNameVersion2 != null && frameworkNameVersion2 >= frameworkNameVersion)
			{
				return;
			}
			int major = frameworkNameVersion.Major;
			Version installedTargetVersion = MultiTargetingUtil.GetInstalledTargetVersion(major);
			if (installedTargetVersion != null && installedTargetVersion >= frameworkNameVersion)
			{
				return;
			}
			if (MultiTargetingUtil.IsSupportedVersion(MultiTargetingUtil.s_targetFrameworkName))
			{
				return;
			}
			throw new ConfigurationErrorsException(SR.GetString("Invalid_higher_target_version", new object[]
			{
				MultiTargetingUtil.s_configTargetFrameworkAttributeName
			}), source, lineNumber);
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x00161194 File Offset: 0x0015F394
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static Version GetInstalledTargetVersion(int majorVersion)
		{
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v" + majorVersion.ToString() + "\\Full";
			try
			{
				object value = Registry.GetValue(keyName, "TargetVersion", null);
				string text = value as string;
				if (!string.IsNullOrEmpty(text))
				{
					return new Version(text);
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060064CD RID: 25805 RVA: 0x001611FC File Offset: 0x0015F3FC
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool IsSupportedVersion(FrameworkName frameworkName)
		{
			try
			{
				FrameworkName frameworkName2 = new FrameworkName(frameworkName.Identifier, frameworkName.Version);
				Version version = Environment.Version;
				string str = string.Concat(new string[]
				{
					version.Major.ToString(),
					".",
					version.Minor.ToString(),
					".",
					version.Build.ToString()
				});
				string name = "SOFTWARE\\Microsoft\\.NETFramework\\v" + str + "\\SKUs";
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name);
				foreach (string name2 in registryKey.GetSubKeyNames())
				{
					try
					{
						FrameworkName frameworkName3 = MultiTargetingUtil.CreateFrameworkName(name2);
						FrameworkName frameworkName4 = new FrameworkName(frameworkName3.Identifier, frameworkName3.Version);
						if (string.Equals(frameworkName2.FullName, frameworkName4.FullName, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x060064CE RID: 25806 RVA: 0x00161318 File Offset: 0x0015F518
		private static bool WebConfigExists
		{
			get
			{
				VirtualPath appDomainAppVirtualPathObject = HttpRuntime.AppDomainAppVirtualPathObject;
				if (appDomainAppVirtualPathObject != null)
				{
					string path = appDomainAppVirtualPathObject.SimpleCombine("web.config").MapPath();
					return File.Exists(path);
				}
				return false;
			}
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x00161350 File Offset: 0x0015F550
		private static string GetCompilerVersionFor20Or35()
		{
			string text = MultiTargetingUtil.GetCSharpCompilerVersion();
			string text2 = MultiTargetingUtil.GetVisualBasicCompilerVersion();
			text = MultiTargetingUtil.ReplaceCompilerVersionFor20Or35(text);
			text2 = MultiTargetingUtil.ReplaceCompilerVersionFor20Or35(text2);
			Version versionFromVString = CompilationUtil.GetVersionFromVString(text);
			Version versionFromVString2 = CompilationUtil.GetVersionFromVString(text2);
			if (versionFromVString > versionFromVString2)
			{
				return text;
			}
			return text2;
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x00161394 File Offset: 0x0015F594
		private static void InitializeTargetFrameworkNameFor20Or35()
		{
			string compilerVersionFor20Or = MultiTargetingUtil.GetCompilerVersionFor20Or35();
			if (CompilationUtil.IsCompilerVersion35(compilerVersionFor20Or))
			{
				MultiTargetingUtil.s_targetFrameworkName = MultiTargetingUtil.FrameworkNameV35;
				return;
			}
			if (compilerVersionFor20Or == "v2.0" || compilerVersionFor20Or == null)
			{
				MultiTargetingUtil.s_targetFrameworkName = MultiTargetingUtil.FrameworkNameV30;
				return;
			}
			throw new ConfigurationErrorsException(SR.GetString("Compiler_version_20_35_required", new object[]
			{
				MultiTargetingUtil.s_configTargetFrameworkAttributeName
			}));
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x001613F2 File Offset: 0x0015F5F2
		private static string ReplaceCompilerVersionFor20Or35(string compilerVersion)
		{
			if (CompilationUtil.IsCompilerVersion35(compilerVersion))
			{
				return compilerVersion;
			}
			return "v2.0";
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x00161403 File Offset: 0x0015F603
		private static string GetCSharpCompilerVersion()
		{
			return CompilationUtil.GetCompilerVersion(typeof(CSharpCodeProvider));
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x00161414 File Offset: 0x0015F614
		private static string GetVisualBasicCompilerVersion()
		{
			return CompilationUtil.GetCompilerVersion(typeof(VBCodeProvider));
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x00161425 File Offset: 0x0015F625
		private static void ReportInvalidCompilerVersion(string compilerVersion)
		{
			throw new ConfigurationErrorsException(SR.GetString("Invalid_attribute_value", new object[]
			{
				compilerVersion,
				"system.codedom/compilers/compiler/ProviderOption/CompilerVersion"
			}));
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x00161448 File Offset: 0x0015F648
		private static void InitializeTargetFrameworkNameFor40AndAbove(string targetFrameworkMoniker)
		{
			MultiTargetingUtil.ValidateTargetFrameworkMoniker(targetFrameworkMoniker);
			MultiTargetingUtil.ValidateCompilerVersionFor40AndAbove();
		}

		// Token: 0x060064D6 RID: 25814 RVA: 0x00161455 File Offset: 0x0015F655
		private static void ValidateCompilerVersionFor40AndAbove()
		{
			MultiTargetingUtil.ValidateCompilerVersionFor40AndAbove(MultiTargetingUtil.GetCSharpCompilerVersion());
			MultiTargetingUtil.ValidateCompilerVersionFor40AndAbove(MultiTargetingUtil.GetVisualBasicCompilerVersion());
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x0016146C File Offset: 0x0015F66C
		private static void ValidateCompilerVersionFor40AndAbove(string compilerVersion)
		{
			if (compilerVersion != null)
			{
				Exception ex = null;
				if (compilerVersion.Length < 4 || compilerVersion[0] != 'v')
				{
					MultiTargetingUtil.ReportInvalidCompilerVersion(compilerVersion);
				}
				try
				{
					Version versionFromVString = CompilationUtil.GetVersionFromVString(compilerVersion);
					if (versionFromVString < MultiTargetingUtil.Version40)
					{
						throw new ConfigurationErrorsException(SR.GetString("Compiler_version_40_required", new object[]
						{
							MultiTargetingUtil.s_configTargetFrameworkAttributeName
						}));
					}
				}
				catch (ArgumentNullException ex2)
				{
					ex = ex2;
				}
				catch (ArgumentOutOfRangeException ex3)
				{
					ex = ex3;
				}
				catch (ArgumentException ex4)
				{
					ex = ex4;
				}
				catch (FormatException ex5)
				{
					ex = ex5;
				}
				catch (OverflowException ex6)
				{
					ex = ex6;
				}
				if (ex != null)
				{
					MultiTargetingUtil.ReportInvalidCompilerVersion(compilerVersion);
				}
			}
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x060064D8 RID: 25816 RVA: 0x00161538 File Offset: 0x0015F738
		internal static bool IsTargetFramework35
		{
			get
			{
				return object.Equals(MultiTargetingUtil.TargetFrameworkName, MultiTargetingUtil.FrameworkNameV35);
			}
		}

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x060064D9 RID: 25817 RVA: 0x00161549 File Offset: 0x0015F749
		internal static bool IsTargetFramework20
		{
			get
			{
				return object.Equals(MultiTargetingUtil.TargetFrameworkName, MultiTargetingUtil.FrameworkNameV20) || object.Equals(MultiTargetingUtil.TargetFrameworkName, MultiTargetingUtil.FrameworkNameV30);
			}
		}

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x060064DA RID: 25818 RVA: 0x0016156D File Offset: 0x0015F76D
		internal static Version TargetFrameworkVersion
		{
			get
			{
				return MultiTargetingUtil.GetFrameworkNameVersion(MultiTargetingUtil.TargetFrameworkName);
			}
		}

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x00161579 File Offset: 0x0015F779
		internal static bool IsTargetFramework40OrAbove
		{
			get
			{
				return MultiTargetingUtil.TargetFrameworkVersion.Major >= 4;
			}
		}

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x060064DC RID: 25820 RVA: 0x0016158B File Offset: 0x0015F78B
		internal static bool IsTargetFramework45OrAbove
		{
			get
			{
				return MultiTargetingUtil.IsTargetFramework40OrAbove && MultiTargetingUtil.TargetFrameworkVersion.Minor >= 5;
			}
		}

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x060064DD RID: 25821 RVA: 0x001443BC File Offset: 0x001425BC
		internal static bool EnableReferenceAssemblyResolution
		{
			get
			{
				return BuildManagerHost.InClientBuildManager;
			}
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x001615A6 File Offset: 0x0015F7A6
		internal static FrameworkName CreateFrameworkName(string name)
		{
			return new FrameworkName(name);
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x001615AE File Offset: 0x0015F7AE
		private static Version GetFrameworkNameVersion(FrameworkName name)
		{
			if (name == null)
			{
				return null;
			}
			return name.Version;
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x001615C4 File Offset: 0x0015F7C4
		private static Version GetVersion(string version)
		{
			if (string.IsNullOrEmpty(version) || !char.IsDigit(version[0]))
			{
				return null;
			}
			try
			{
				return new Version(version);
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x040033FA RID: 13306
		internal static readonly FrameworkName FrameworkNameV20 = MultiTargetingUtil.CreateFrameworkName(".NETFramework,Version=v2.0");

		// Token: 0x040033FB RID: 13307
		internal static readonly FrameworkName FrameworkNameV30 = MultiTargetingUtil.CreateFrameworkName(".NETFramework,Version=v3.0");

		// Token: 0x040033FC RID: 13308
		internal static readonly FrameworkName FrameworkNameV35 = MultiTargetingUtil.CreateFrameworkName(".NETFramework,Version=v3.5");

		// Token: 0x040033FD RID: 13309
		internal static readonly FrameworkName FrameworkNameV40 = MultiTargetingUtil.CreateFrameworkName(".NETFramework,Version=v4.0");

		// Token: 0x040033FE RID: 13310
		internal static readonly FrameworkName FrameworkNameV45 = MultiTargetingUtil.CreateFrameworkName(".NETFramework,Version=v4.5");

		// Token: 0x040033FF RID: 13311
		internal static Version Version40 = new Version(4, 0);

		// Token: 0x04003400 RID: 13312
		internal static Version Version35 = new Version(3, 5);

		// Token: 0x04003401 RID: 13313
		private static FrameworkName s_targetFrameworkName = null;

		// Token: 0x04003402 RID: 13314
		private static string s_configTargetFrameworkMoniker = null;

		// Token: 0x04003403 RID: 13315
		private static object s_configTargetFrameworkMonikerLock = new object();

		// Token: 0x04003404 RID: 13316
		private static bool s_initializedConfigTargetFrameworkMoniker = false;

		// Token: 0x04003405 RID: 13317
		private static object s_targetFrameworkNameLock = new object();

		// Token: 0x04003406 RID: 13318
		private static string s_configTargetFrameworkAttributeName = "targetFramework";

		// Token: 0x04003407 RID: 13319
		private static FrameworkName s_latestFrameworkName = null;

		// Token: 0x04003408 RID: 13320
		private static List<FrameworkName> s_knownFrameworkNames = null;
	}
}
