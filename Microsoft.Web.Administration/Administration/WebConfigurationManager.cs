using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Web;
using System.Web.Hosting;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000078 RID: 120
	public static class WebConfigurationManager
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00008FF0 File Offset: 0x00007FF0
		public static ConfigurationSection GetSection(string sectionPath)
		{
			return WebConfigurationManager.GetSection(HttpContext.Current, sectionPath);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00008FFD File Offset: 0x00007FFD
		public static ConfigurationSection GetSection(HttpContext context, string sectionPath)
		{
			return WebConfigurationManager.GetSection(context, sectionPath, typeof(ConfigurationSection));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009010 File Offset: 0x00008010
		public static ConfigurationSection GetSection(HttpContext context, string sectionPath, Type sectionType)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.Request == null)
			{
				throw new ArgumentNullException("context.Request");
			}
			string siteName = HostingEnvironment.SiteName;
			string path = context.Request.Path;
			return WebConfigurationManager.GetSectionInternal(siteName, path, sectionPath, sectionType);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000905C File Offset: 0x0000805C
		private static ConfigurationSection GetSectionInternal(string siteName, string virtualPath, string sectionPath, Type sectionType)
		{
			if (string.IsNullOrEmpty(sectionPath))
			{
				throw new ArgumentNullException("sectionPath");
			}
			if (sectionType == null)
			{
				throw new ArgumentNullException("sectionType");
			}
			if (!typeof(ConfigurationSection).IsAssignableFrom(sectionType))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Resources.InvalidType, new object[]
				{
					sectionType.ToString()
				}));
			}
			ConstructorInfo constructor = sectionType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
			if (constructor == null)
			{
				throw new InvalidOperationException(Resources.ConstructorNotFound);
			}
			string text = ConfigurationManager.CombineConfigurationPath("MACHINE/WEBROOT/APPHOST", siteName);
			text = ConfigurationManager.CombineConfigurationPath(text, virtualPath);
			if (WebConfigurationManager._defaultConfiguration == null)
			{
				object obj = null;
				int defaultNativeConfigurationSystem = WebConfigurationManager.GetDefaultNativeConfigurationSystem(out obj);
				if (defaultNativeConfigurationSystem != 0)
				{
					Marshal.ThrowExceptionForHR(defaultNativeConfigurationSystem);
				}
				WebConfigurationManager._defaultConfiguration = (obj as IAppHostAdminManager);
			}
			IAppHostElement adminSection = WebConfigurationManager._defaultConfiguration.GetAdminSection(sectionPath, text);
			Configuration.CheckPermissions(adminSection);
			ConfigurationSection configurationSection = (ConfigurationSection)constructor.Invoke(null);
			configurationSection.SetSectionPath(sectionPath);
			configurationSection.Initialize(null, adminSection);
			return configurationSection;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00009155 File Offset: 0x00008155
		public static ConfigurationSection GetSection(string siteName, string virtualPath, string sectionPath)
		{
			return WebConfigurationManager.GetSectionInternal(siteName, virtualPath, sectionPath, typeof(ConfigurationSection));
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009169 File Offset: 0x00008169
		public static ConfigurationSection GetSection(string siteName, string virtualPath, string sectionPath, Type sectionType)
		{
			return WebConfigurationManager.GetSectionInternal(siteName, virtualPath, sectionPath, sectionType);
		}

		// Token: 0x06000379 RID: 889
		[SuppressUnmanagedCodeSecurity]
		[DllImport("nativerd.dll")]
		private static extern int GetDefaultNativeConfigurationSystem([MarshalAs(UnmanagedType.IUnknown)] out object configSystem);

		// Token: 0x04000130 RID: 304
		private const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000131 RID: 305
		private static IAppHostAdminManager _defaultConfiguration;
	}
}
