using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000729 RID: 1833
	internal static class ProcessHostConfigUtils
	{
		// Token: 0x06005854 RID: 22612 RVA: 0x00134FE6 File Offset: 0x001331E6
		static ProcessHostConfigUtils()
		{
			HttpRuntime.ForceStaticInit();
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x00134FF8 File Offset: 0x001331F8
		internal static void InitStandaloneConfig()
		{
			if (!HostingEnvironment.IsUnderIISProcess && !ServerConfig.UseMetabase && ProcessHostConfigUtils.s_InitedExternalConfig == 0)
			{
				object obj = ProcessHostConfigUtils.s_InitedExternalConfigLock;
				lock (obj)
				{
					if (ProcessHostConfigUtils.s_InitedExternalConfig == 0)
					{
						try
						{
							ProcessHostConfigUtils._configWrapper = new ProcessHostConfigUtils.NativeConfigWrapper();
						}
						finally
						{
							ProcessHostConfigUtils.s_InitedExternalConfig = 1;
						}
					}
				}
			}
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x0013506C File Offset: 0x0013326C
		internal static string MapPathActual(string siteName, VirtualPath path)
		{
			string result = null;
			IntPtr zero = IntPtr.Zero;
			int length = 0;
			try
			{
				int num = UnsafeIISMethods.MgdMapPathDirect(IntPtr.Zero, siteName, path.VirtualPathString, out zero, out length);
				if (num < 0)
				{
					throw new InvalidOperationException(SR.GetString("Cannot_map_path", new object[]
					{
						path.VirtualPathString
					}));
				}
				result = ((zero != IntPtr.Zero) ? StringUtil.StringFromWCharPtr(zero, length) : null);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero);
				}
			}
			return result;
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x001350FC File Offset: 0x001332FC
		internal static string GetSiteNameFromId(uint siteId)
		{
			if (siteId == 1U && ProcessHostConfigUtils.s_defaultSiteName != null)
			{
				return ProcessHostConfigUtils.s_defaultSiteName;
			}
			IntPtr zero = IntPtr.Zero;
			int length = 0;
			string result = null;
			try
			{
				result = ((UnsafeIISMethods.MgdGetSiteNameFromId(IntPtr.Zero, siteId, out zero, out length) == 0 && zero != IntPtr.Zero) ? StringUtil.StringFromWCharPtr(zero, length) : string.Empty);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero);
				}
			}
			if (siteId == 1U)
			{
				ProcessHostConfigUtils.s_defaultSiteName = result;
			}
			return result;
		}

		// Token: 0x04002EEE RID: 12014
		internal const uint DEFAULT_SITE_ID_UINT = 1U;

		// Token: 0x04002EEF RID: 12015
		internal const string DEFAULT_SITE_ID_STRING = "1";

		// Token: 0x04002EF0 RID: 12016
		private static string s_defaultSiteName;

		// Token: 0x04002EF1 RID: 12017
		private static int s_InitedExternalConfig;

		// Token: 0x04002EF2 RID: 12018
		private static object s_InitedExternalConfigLock = new object();

		// Token: 0x04002EF3 RID: 12019
		private static ProcessHostConfigUtils.NativeConfigWrapper _configWrapper;

		// Token: 0x02000A47 RID: 2631
		private class NativeConfigWrapper : CriticalFinalizerObject
		{
			// Token: 0x06006EA3 RID: 28323 RVA: 0x0018A198 File Offset: 0x00188398
			internal NativeConfigWrapper()
			{
				int num = UnsafeIISMethods.MgdInitNativeConfig();
				if (num < 0)
				{
					ProcessHostConfigUtils.s_InitedExternalConfig = 0;
					throw new InvalidOperationException(SR.GetString("Cant_Init_Native_Config", new object[]
					{
						num.ToString("X8", CultureInfo.InvariantCulture)
					}));
				}
			}

			// Token: 0x06006EA4 RID: 28324 RVA: 0x0018A1E8 File Offset: 0x001883E8
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			~NativeConfigWrapper()
			{
				UnsafeIISMethods.MgdTerminateNativeConfig();
			}
		}
	}
}
