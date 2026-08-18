using System;
using System.Runtime.InteropServices;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000222 RID: 546
	internal sealed class ProcessHostServerConfig : IServerConfig
	{
		// Token: 0x06001D48 RID: 7496 RVA: 0x00085250 File Offset: 0x00084250
		internal static IServerConfig GetInstance()
		{
			if (ProcessHostServerConfig.s_instance == null)
			{
				lock (ProcessHostServerConfig.s_initLock)
				{
					if (ProcessHostServerConfig.s_instance == null)
					{
						ProcessHostServerConfig.s_instance = new ProcessHostServerConfig();
					}
				}
			}
			return ProcessHostServerConfig.s_instance;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000852A0 File Offset: 0x000842A0
		static ProcessHostServerConfig()
		{
			HttpRuntime.ForceStaticInit();
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000852B4 File Offset: 0x000842B4
		private ProcessHostServerConfig()
		{
			if (HostingEnvironment.SupportFunctions == null)
			{
				ProcessHostConfigUtils.InitStandaloneConfig();
			}
			else
			{
				IProcessHostSupportFunctions supportFunctions = HostingEnvironment.SupportFunctions;
				if (supportFunctions != null)
				{
					IntPtr nativeConfigurationSystem = supportFunctions.GetNativeConfigurationSystem();
					if (IntPtr.Zero != nativeConfigurationSystem)
					{
						UnsafeIISMethods.MgdSetNativeConfiguration(nativeConfigurationSystem);
					}
				}
			}
			this._siteNameForCurrentApplication = HostingEnvironment.SiteNameNoDemand;
			if (this._siteNameForCurrentApplication == null)
			{
				this._siteNameForCurrentApplication = ProcessHostConfigUtils.GetSiteNameFromId(1U);
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00085318 File Offset: 0x00084318
		string IServerConfig.GetSiteNameFromSiteID(string siteID)
		{
			uint siteId;
			if (!uint.TryParse(siteID, out siteId))
			{
				return string.Empty;
			}
			return ProcessHostConfigUtils.GetSiteNameFromId(siteId);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0008533C File Offset: 0x0008433C
		string IServerConfig.MapPath(IApplicationHost appHost, VirtualPath path)
		{
			string siteName = (appHost == null) ? this._siteNameForCurrentApplication : appHost.GetSiteName();
			string text = ProcessHostConfigUtils.MapPathActual(siteName, path);
			if (FileUtil.IsSuspiciousPhysicalPath(text))
			{
				throw new InvalidOperationException(SR.GetString("Cannot_map_path", new object[]
				{
					path.VirtualPathString
				}));
			}
			return text;
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x00085390 File Offset: 0x00084390
		string[] IServerConfig.GetVirtualSubdirs(VirtualPath path, bool inApp)
		{
			if (!inApp)
			{
				throw new NotSupportedException();
			}
			string virtualPathString = path.VirtualPathString;
			string[] array = null;
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			int num2 = 0;
			try
			{
				int num3 = 0;
				int num4 = UnsafeIISMethods.MgdGetAppCollection(this._siteNameForCurrentApplication, virtualPathString, out zero2, out num2, out zero, out num3);
				if (num4 < 0 || zero2 == IntPtr.Zero)
				{
					throw new InvalidOperationException(SR.GetString("Cant_Enumerate_NativeDirs", new object[]
					{
						num4
					}));
				}
				string text = StringUtil.StringFromWCharPtr(zero2, num2);
				Marshal.FreeBSTR(zero2);
				zero2 = IntPtr.Zero;
				num2 = 0;
				array = new string[num3];
				int num5 = virtualPathString.Length;
				if (virtualPathString[num5 - 1] == '/')
				{
					num5--;
				}
				int length = text.Length;
				string text2 = (num5 > length) ? virtualPathString.Substring(length, num5 - length) : string.Empty;
				uint num6 = 0U;
				while ((ulong)num6 < (ulong)((long)num3))
				{
					num4 = UnsafeIISMethods.MgdGetNextVPath(zero, num6, out zero2, out num2);
					if (num4 < 0 || zero2 == IntPtr.Zero)
					{
						throw new InvalidOperationException(SR.GetString("Cant_Enumerate_NativeDirs", new object[]
						{
							num4
						}));
					}
					string text3 = (num2 > 1) ? StringUtil.StringFromWCharPtr(zero2, num2) : null;
					Marshal.FreeBSTR(zero2);
					zero2 = IntPtr.Zero;
					num2 = 0;
					if (text3 != null && text3.Length > text2.Length)
					{
						if (text2.Length == 0)
						{
							if (text3.IndexOf('/', 1) == -1)
							{
								array[num++] = text3.Substring(1);
							}
						}
						else if (StringUtil.EqualsIgnoreCase(text2, 0, text3, 0, text2.Length))
						{
							int num7 = text3.IndexOf('/', 1 + text2.Length);
							if (num7 > -1)
							{
								array[num++] = text3.Substring(text2.Length + 1, num7 - text2.Length);
							}
							else
							{
								array[num++] = text3.Substring(text2.Length + 1);
							}
						}
					}
					num6 += 1U;
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.Release(zero);
					zero = IntPtr.Zero;
				}
				if (zero2 != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero2);
					zero2 = IntPtr.Zero;
				}
			}
			string[] array2 = null;
			if (num > 0)
			{
				array2 = new string[num];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = array[i];
				}
			}
			return array2;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00085624 File Offset: 0x00084624
		internal bool IsWithinApp(string virtualPath)
		{
			return UnsafeIISMethods.MgdIsWithinApp(this._siteNameForCurrentApplication, HttpRuntime.AppDomainAppVirtualPathString, virtualPath);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00085638 File Offset: 0x00084638
		bool IServerConfig.GetUncUser(IApplicationHost appHost, VirtualPath path, out string username, out string password)
		{
			bool result = false;
			username = null;
			password = null;
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			IntPtr zero2 = IntPtr.Zero;
			int num2 = 0;
			try
			{
				if (UnsafeIISMethods.MgdGetVrPathCreds(appHost.GetSiteName(), path.VirtualPathString, out zero, out num, out zero2, out num2) == 0)
				{
					username = ((num > 0) ? StringUtil.StringFromWCharPtr(zero, num) : null);
					password = ((num2 > 0) ? StringUtil.StringFromWCharPtr(zero2, num2) : null);
					result = (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password));
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero);
				}
				if (zero2 != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero2);
				}
			}
			return result;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000856F4 File Offset: 0x000846F4
		long IServerConfig.GetW3WPMemoryLimitInKB()
		{
			long result = 0L;
			int num = UnsafeIISMethods.MgdGetMemoryLimitKB(out result);
			if (num < 0)
			{
				return 0L;
			}
			return result;
		}

		// Token: 0x0400194C RID: 6476
		private static object s_initLock = new object();

		// Token: 0x0400194D RID: 6477
		private static ProcessHostServerConfig s_instance;

		// Token: 0x0400194E RID: 6478
		private string _siteNameForCurrentApplication;
	}
}
