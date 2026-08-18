using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Web;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using Microsoft.Build.Utilities;

// Token: 0x0200000B RID: 11
internal class MTConfigUtil
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600003E RID: 62 RVA: 0x00002C7F File Offset: 0x00000E7F
	private static bool UseMTConfig
	{
		get
		{
			if (MTConfigUtil.s_useMTConfig == null)
			{
				MTConfigUtil.s_useMTConfig = new bool?(BuildManagerHost.InClientBuildManager && (MultiTargetingUtil.IsTargetFramework20 || MultiTargetingUtil.IsTargetFramework35));
			}
			return MTConfigUtil.s_useMTConfig.Value;
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002CBA File Offset: 0x00000EBA
	internal static ProfileSection GetProfileAppConfig()
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetAppConfig().Profile;
		}
		return MTConfigUtil.GetAppConfig<ProfileSection>();
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002CD3 File Offset: 0x00000ED3
	internal static PagesSection GetPagesAppConfig()
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetAppConfig().Pages;
		}
		return MTConfigUtil.GetAppConfig<PagesSection>();
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002CEC File Offset: 0x00000EEC
	internal static PagesSection GetPagesConfig()
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetConfig().Pages;
		}
		return MTConfigUtil.GetConfig<PagesSection>();
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002D05 File Offset: 0x00000F05
	internal static PagesSection GetPagesConfig(string vpath)
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetConfig(vpath).Pages;
		}
		return MTConfigUtil.GetConfig<PagesSection>(vpath);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002D20 File Offset: 0x00000F20
	internal static PagesSection GetPagesConfig(VirtualPath vpath)
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetConfig(vpath).Pages;
		}
		return MTConfigUtil.GetConfig<PagesSection>(vpath);
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002D3B File Offset: 0x00000F3B
	internal static PagesSection GetPagesConfig(HttpContext context)
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetConfig(context).Pages;
		}
		return MTConfigUtil.GetConfig<PagesSection>(context);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00002D56 File Offset: 0x00000F56
	internal static CompilationSection GetCompilationConfig()
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetConfig().Compilation;
		}
		return MTConfigUtil.GetConfig<CompilationSection>();
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00002D6F File Offset: 0x00000F6F
	internal static CompilationSection GetCompilationAppConfig()
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetAppConfig().Compilation;
		}
		return MTConfigUtil.GetAppConfig<CompilationSection>();
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00002D88 File Offset: 0x00000F88
	internal static CompilationSection GetCompilationConfig(string vpath)
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetConfig(vpath).Compilation;
		}
		return MTConfigUtil.GetConfig<CompilationSection>(vpath);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00002DA3 File Offset: 0x00000FA3
	internal static CompilationSection GetCompilationConfig(VirtualPath vpath)
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetConfig(vpath).Compilation;
		}
		return MTConfigUtil.GetConfig<CompilationSection>(vpath);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00002DBE File Offset: 0x00000FBE
	internal static CompilationSection GetCompilationConfig(HttpContext context)
	{
		if (!MTConfigUtil.UseMTConfig)
		{
			return RuntimeConfig.GetConfig(context).Compilation;
		}
		return MTConfigUtil.GetConfig<CompilationSection>(context);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002DDC File Offset: 0x00000FDC
	private static S GetConfig<S>() where S : ConfigurationSection
	{
		HttpContext httpContext = HttpContext.Current;
		if (httpContext != null)
		{
			return MTConfigUtil.GetConfig<S>(httpContext);
		}
		return MTConfigUtil.GetAppConfig<S>();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00002DFE File Offset: 0x00000FFE
	private static S GetAppConfig<S>() where S : ConfigurationSection
	{
		return MTConfigUtil.GetConfig<S>(null);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00002E06 File Offset: 0x00001006
	private static S GetConfig<S>(HttpContext context) where S : ConfigurationSection
	{
		return MTConfigUtil.GetConfig<S>(context.ConfigurationPath);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00002E13 File Offset: 0x00001013
	private static S GetConfig<S>(string vpath) where S : ConfigurationSection
	{
		return MTConfigUtil.GetConfig<S>(VirtualPath.CreateNonRelativeAllowNull(vpath));
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00002E20 File Offset: 0x00001020
	private static S GetConfig<S>(VirtualPath vpath) where S : ConfigurationSection
	{
		Tuple<Type, VirtualPath> key = new Tuple<Type, VirtualPath>(typeof(S), vpath);
		ConfigurationSection configurationSection;
		if (!MTConfigUtil.s_sections.TryGetValue(key, out configurationSection))
		{
			configurationSection = MTConfigUtil.GetConfigHelper<S>(vpath);
			MTConfigUtil.s_sections.TryAdd(key, configurationSection);
		}
		return configurationSection as S;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00002E74 File Offset: 0x00001074
	private static S GetConfigHelper<S>(VirtualPath vpath) where S : ConfigurationSection
	{
		string physicalPath;
		if (vpath == null || !vpath.IsWithinAppRoot)
		{
			vpath = HostingEnvironment.ApplicationVirtualPathObject;
			physicalPath = HostingEnvironment.ApplicationPhysicalPath;
		}
		else
		{
			if (!vpath.DirectoryExists())
			{
				vpath = vpath.Parent;
			}
			physicalPath = HostingEnvironment.MapPath(vpath);
		}
		Configuration configuration = MTConfigUtil.GetConfiguration(vpath, physicalPath);
		if (typeof(S) == typeof(CompilationSection))
		{
			return configuration.GetSection("system.web/compilation") as S;
		}
		if (typeof(S) == typeof(PagesSection))
		{
			return configuration.GetSection("system.web/pages") as S;
		}
		if (typeof(S) == typeof(ProfileSection))
		{
			return configuration.GetSection("system.web/profile") as S;
		}
		throw new InvalidOperationException(SR.GetString("Config_section_not_supported", new object[]
		{
			typeof(S).FullName
		}));
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000050 RID: 80 RVA: 0x00002F7C File Offset: 0x0000117C
	private static string MachineConfigPath
	{
		get
		{
			if (MTConfigUtil.s_machineConfigPath == null)
			{
				MTConfigUtil.s_machineConfigPath = ToolLocationHelper.GetPathToDotNetFrameworkFile("config\\machine.config", TargetDotNetFrameworkVersion.Version20);
				if (string.IsNullOrEmpty(MTConfigUtil.s_machineConfigPath))
				{
					string @string = SR.GetString("Downlevel_requires_35");
					throw new InvalidOperationException(@string);
				}
			}
			return MTConfigUtil.s_machineConfigPath;
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00002FC4 File Offset: 0x000011C4
	private static Configuration GetConfiguration(VirtualPath vpath, string physicalPath)
	{
		Configuration configurationHelper;
		if (!MTConfigUtil.s_configurations.TryGetValue(vpath, out configurationHelper))
		{
			configurationHelper = MTConfigUtil.GetConfigurationHelper(vpath, physicalPath);
			MTConfigUtil.s_configurations.TryAdd(vpath, configurationHelper);
		}
		return configurationHelper;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00002FF8 File Offset: 0x000011F8
	private static Configuration GetConfigurationHelper(VirtualPath vpath, string physicalPath)
	{
		string machineConfigPath = MTConfigUtil.MachineConfigPath;
		WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap(machineConfigPath);
		VirtualPath virtualPath = vpath;
		while (virtualPath != null && virtualPath.IsWithinAppRoot)
		{
			string virtualPathStringNoTrailingSlash = virtualPath.VirtualPathStringNoTrailingSlash;
			if (physicalPath == null)
			{
				physicalPath = HostingEnvironment.MapPath(virtualPath);
			}
			webConfigurationFileMap.VirtualDirectories.Add(virtualPathStringNoTrailingSlash, new VirtualDirectoryMapping(physicalPath, MTConfigUtil.IsAppRoot(virtualPath)));
			virtualPath = virtualPath.Parent;
			physicalPath = null;
		}
		return WebConfigurationManager.OpenMappedWebConfiguration(webConfigurationFileMap, vpath.VirtualPathStringNoTrailingSlash, HostingEnvironment.SiteName);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003070 File Offset: 0x00001270
	private static bool IsAppRoot(VirtualPath path)
	{
		if (MTConfigUtil.s_appVirtualPath == null)
		{
			MTConfigUtil.s_appVirtualPath = VirtualPath.Create(HttpRuntime.AppDomainAppVirtualPathObject.VirtualPathStringNoTrailingSlash);
		}
		VirtualPath obj = VirtualPath.Create(path.VirtualPathStringNoTrailingSlash);
		return MTConfigUtil.s_appVirtualPath.Equals(obj);
	}

	// Token: 0x04000013 RID: 19
	private static readonly ConcurrentDictionary<Tuple<Type, VirtualPath>, ConfigurationSection> s_sections = new ConcurrentDictionary<Tuple<Type, VirtualPath>, ConfigurationSection>();

	// Token: 0x04000014 RID: 20
	private static readonly ConcurrentDictionary<VirtualPath, Configuration> s_configurations = new ConcurrentDictionary<VirtualPath, Configuration>();

	// Token: 0x04000015 RID: 21
	private static string s_machineConfigPath;

	// Token: 0x04000016 RID: 22
	private static VirtualPath s_appVirtualPath;

	// Token: 0x04000017 RID: 23
	private static bool? s_useMTConfig;
}
