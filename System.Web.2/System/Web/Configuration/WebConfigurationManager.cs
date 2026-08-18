using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Hosting;

namespace System.Web.Configuration
{
	// Token: 0x02000776 RID: 1910
	public static class WebConfigurationManager
	{
		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x06005BFE RID: 23550 RVA: 0x0013EC8D File Offset: 0x0013CE8D
		public static NameValueCollection AppSettings
		{
			get
			{
				return ConfigurationManager.AppSettings;
			}
		}

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x06005BFF RID: 23551 RVA: 0x0013EC94 File Offset: 0x0013CE94
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06005C00 RID: 23552 RVA: 0x0013EC9B File Offset: 0x0013CE9B
		public static object GetSection(string sectionName)
		{
			if (HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return HttpConfigurationSystem.GetSection(sectionName);
			}
			return ConfigurationManager.GetSection(sectionName);
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x0013ECB1 File Offset: 0x0013CEB1
		public static object GetSection(string sectionName, string path)
		{
			if (HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return HttpConfigurationSystem.GetSection(sectionName, path);
			}
			throw new InvalidOperationException(SR.GetString("Config_GetSectionWithPathArgInvalid"));
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x0013ECD1 File Offset: 0x0013CED1
		public static object GetWebApplicationSection(string sectionName)
		{
			if (HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return HttpConfigurationSystem.GetApplicationSection(sectionName);
			}
			return ConfigurationManager.GetSection(sectionName);
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x0013ECE8 File Offset: 0x0013CEE8
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

		// Token: 0x06005C04 RID: 23556 RVA: 0x0013ED20 File Offset: 0x0013CF20
		public static Configuration OpenMachineConfiguration()
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x0013ED40 File Offset: 0x0013CF40
		public static Configuration OpenMachineConfiguration(string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, locationSubPath, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x0013ED60 File Offset: 0x0013CF60
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, locationSubPath, server, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x0013ED80 File Offset: 0x0013CF80
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server, string userName, string password)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, locationSubPath, server, userName, password, IntPtr.Zero);
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x0013EDA0 File Offset: 0x0013CFA0
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server, IntPtr userToken)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, null, null, null, locationSubPath, server, null, null, userToken);
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x0013EDBC File Offset: 0x0013CFBC
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, fileMap, null, null, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x0013EDDC File Offset: 0x0013CFDC
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap, string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Machine, fileMap, null, null, locationSubPath, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x0013EDFC File Offset: 0x0013CFFC
		public static Configuration OpenWebConfiguration(string path)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, null, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x0013EE1C File Offset: 0x0013D01C
		public static Configuration OpenWebConfiguration(string path, string site)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x0013EE3C File Offset: 0x0013D03C
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, locationSubPath, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x0013EE5C File Offset: 0x0013D05C
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, locationSubPath, server, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x0013EE7C File Offset: 0x0013D07C
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, string userName, string password)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, locationSubPath, server, userName, password, IntPtr.Zero);
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x0013EEA0 File Offset: 0x0013D0A0
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, IntPtr userToken)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, null, path, site, locationSubPath, server, null, null, userToken);
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x0013EEBC File Offset: 0x0013D0BC
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, fileMap, path, null, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x0013EEDC File Offset: 0x0013D0DC
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path, string site)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, fileMap, path, site, null, null, null, null, IntPtr.Zero);
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x0013EEFC File Offset: 0x0013D0FC
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path, string site, string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfigurationImpl(WebLevel.Path, fileMap, path, site, locationSubPath, null, null, null, IntPtr.Zero);
		}
	}
}
