using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Hosting;

namespace System.Web.Configuration
{
	// Token: 0x0200026A RID: 618
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public static class WebConfigurationManager
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x0008DD93 File Offset: 0x0008CD93
		public static NameValueCollection AppSettings
		{
			get
			{
				return ConfigurationManager.AppSettings;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x0008DD9A File Offset: 0x0008CD9A
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0008DDA1 File Offset: 0x0008CDA1
		public static object GetSection(string sectionName)
		{
			if (HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return HttpConfigurationSystem.GetSection(sectionName);
			}
			return ConfigurationManager.GetSection(sectionName);
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0008DDB7 File Offset: 0x0008CDB7
		public static object GetSection(string sectionName, string path)
		{
			if (HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return HttpConfigurationSystem.GetSection(sectionName, path);
			}
			throw new InvalidOperationException(SR.GetString("Config_GetSectionWithPathArgInvalid"));
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0008DDD7 File Offset: 0x0008CDD7
		public static object GetWebApplicationSection(string sectionName)
		{
			if (HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return HttpConfigurationSystem.GetApplicationSection(sectionName);
			}
			return ConfigurationManager.GetSection(sectionName);
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0008DDF0 File Offset: 0x0008CDF0
		private static Configuration OpenWebConfigurationImpl(WebLevel webLevel, ConfigurationFileMap fileMap, string path, string site, string locationSubPath, string server, string userName, string password, IntPtr userToken)
		{
			VirtualPath path2;
			if (HostingEnvironment.IsHosted)
			{
				path2 = VirtualPath.CreateNonRelativeAllowNull(path);
			}
			else
			{
				path2 = VirtualPath.CreateAbsoluteAllowNull(path);
			}
			return WebConfigurationHost.OpenConfiguration(webLevel, fileMap, path2, site, locationSubPath, server, userName, password, userToken);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x0008DE28 File Offset: 0x0008CE28
		public static Configuration OpenMachineConfiguration()
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0008DE48 File Offset: 0x0008CE48
		public static Configuration OpenMachineConfiguration(string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, locationSubPath, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x0008DE68 File Offset: 0x0008CE68
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, locationSubPath, server, null, null, IntPtr.Zero);
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0008DE88 File Offset: 0x0008CE88
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server, string userName, string password)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, locationSubPath, server, userName, password, IntPtr.Zero);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0008DEA8 File Offset: 0x0008CEA8
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server, IntPtr userToken)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, locationSubPath, server, null, null, userToken);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0008DEC4 File Offset: 0x0008CEC4
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, fileMap, null, null, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0008DEE4 File Offset: 0x0008CEE4
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap, string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, fileMap, null, null, locationSubPath, null, null, null, IntPtr.Zero);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x0008DF04 File Offset: 0x0008CF04
		public static Configuration OpenWebConfiguration(string path)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, null, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0008DF24 File Offset: 0x0008CF24
		public static Configuration OpenWebConfiguration(string path, string site)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x0008DF44 File Offset: 0x0008CF44
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, locationSubPath, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0008DF64 File Offset: 0x0008CF64
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, locationSubPath, server, null, null, IntPtr.Zero);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x0008DF84 File Offset: 0x0008CF84
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, string userName, string password)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, locationSubPath, server, userName, password, IntPtr.Zero);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x0008DFA8 File Offset: 0x0008CFA8
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, IntPtr userToken)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, locationSubPath, server, null, null, userToken);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0008DFC4 File Offset: 0x0008CFC4
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, fileMap, path, null, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x0008DFE4 File Offset: 0x0008CFE4
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path, string site)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, fileMap, path, site, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x0008E004 File Offset: 0x0008D004
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path, string site, string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, fileMap, path, site, locationSubPath, null, null, null, IntPtr.Zero);
		}
	}
}
