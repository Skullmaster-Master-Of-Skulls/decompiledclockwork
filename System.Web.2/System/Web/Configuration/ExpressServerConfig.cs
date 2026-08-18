using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006D7 RID: 1751
	internal sealed class ExpressServerConfig : IServerConfig, IServerConfig2, IConfigMapPath, IConfigMapPath2, IDisposable
	{
		// Token: 0x06005437 RID: 21559 RVA: 0x001272EC File Offset: 0x001254EC
		internal static IServerConfig GetInstance(string version)
		{
			if (ExpressServerConfig.s_instance == null)
			{
				object obj = ExpressServerConfig.s_initLock;
				lock (obj)
				{
					if (ExpressServerConfig.s_instance == null)
					{
						if (Thread.GetDomain().IsDefaultAppDomain())
						{
							throw new InvalidOperationException();
						}
						ExpressServerConfig.s_instance = new ExpressServerConfig(version);
					}
				}
			}
			return ExpressServerConfig.s_instance;
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x00127358 File Offset: 0x00125558
		static ExpressServerConfig()
		{
			HttpRuntime.ForceStaticInit();
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x000030B5 File Offset: 0x000012B5
		private ExpressServerConfig()
		{
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x00127369 File Offset: 0x00125569
		internal ExpressServerConfig(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._nativeConfig = new NativeConfig(version);
		}

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x0600543B RID: 21563 RVA: 0x0012738C File Offset: 0x0012558C
		private string CurrentAppSiteName
		{
			get
			{
				string text = this._currentAppSiteName;
				if (text == null)
				{
					text = HostingEnvironment.SiteNameNoDemand;
					if (text == null)
					{
						text = this._nativeConfig.GetSiteNameFromId(1U);
					}
					this._currentAppSiteName = text;
				}
				return text;
			}
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x001273C4 File Offset: 0x001255C4
		void IDisposable.Dispose()
		{
			NativeConfig nativeConfig = this._nativeConfig;
			this._nativeConfig = null;
			if (nativeConfig != null)
			{
				nativeConfig.Dispose();
			}
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x001273E8 File Offset: 0x001255E8
		string IServerConfig.GetSiteNameFromSiteID(string siteID)
		{
			uint siteID2;
			if (!uint.TryParse(siteID, out siteID2))
			{
				return string.Empty;
			}
			return this._nativeConfig.GetSiteNameFromId(siteID2);
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x00127414 File Offset: 0x00125614
		string IServerConfig.MapPath(IApplicationHost appHost, VirtualPath path)
		{
			string siteName = (appHost == null) ? this.CurrentAppSiteName : appHost.GetSiteName();
			string text = this._nativeConfig.MapPathDirect(siteName, path);
			if (FileUtil.IsSuspiciousPhysicalPath(text))
			{
				throw new InvalidOperationException(SR.GetString("Cannot_map_path", new object[]
				{
					path.VirtualPathString
				}));
			}
			return text;
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0012746C File Offset: 0x0012566C
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
				int num4 = this._nativeConfig.MgdGetAppCollection(this.CurrentAppSiteName, virtualPathString, out zero2, out num2, out zero, out num3);
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

		// Token: 0x06005440 RID: 21568 RVA: 0x001276FC File Offset: 0x001258FC
		bool IServerConfig2.IsWithinApp(string virtualPath)
		{
			return this._nativeConfig.MgdIsWithinApp(this.CurrentAppSiteName, HttpRuntime.AppDomainAppVirtualPathString, virtualPath);
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x00127718 File Offset: 0x00125918
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
				if (this._nativeConfig.MgdGetVrPathCreds(appHost.GetSiteName(), path.VirtualPathString, out zero, out num, out zero2, out num2) == 0)
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

		// Token: 0x06005442 RID: 21570 RVA: 0x001277DC File Offset: 0x001259DC
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

		// Token: 0x06005443 RID: 21571 RVA: 0x001277FC File Offset: 0x001259FC
		string IConfigMapPath.GetMachineConfigFilename()
		{
			return HttpConfigurationSystem.MachineConfigurationFilePath;
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x00127803 File Offset: 0x00125A03
		string IConfigMapPath.GetRootWebConfigFilename()
		{
			return HttpConfigurationSystem.RootWebConfigurationFilePath;
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x0012780A File Offset: 0x00125A0A
		private void GetPathConfigFilenameWorker(string siteID, VirtualPath path, out string directory, out string baseName)
		{
			directory = this.MapPathCaching(siteID, path);
			if (directory != null)
			{
				baseName = "web.config";
				return;
			}
			baseName = null;
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x00127827 File Offset: 0x00125A27
		void IConfigMapPath.GetPathConfigFilename(string siteID, string path, out string directory, out string baseName)
		{
			this.GetPathConfigFilenameWorker(siteID, VirtualPath.Create(path), out directory, out baseName);
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x00127839 File Offset: 0x00125A39
		void IConfigMapPath2.GetPathConfigFilename(string siteID, VirtualPath path, out string directory, out string baseName)
		{
			this.GetPathConfigFilenameWorker(siteID, path, out directory, out baseName);
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x00127846 File Offset: 0x00125A46
		void IConfigMapPath.GetDefaultSiteNameAndID(out string siteName, out string siteID)
		{
			siteID = "1";
			siteName = this._nativeConfig.GetSiteNameFromId(1U);
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x00127860 File Offset: 0x00125A60
		void IConfigMapPath.ResolveSiteArgument(string siteArgument, out string siteName, out string siteID)
		{
			if (string.IsNullOrEmpty(siteArgument) || StringUtil.EqualsIgnoreCase(siteArgument, "1") || StringUtil.EqualsIgnoreCase(siteArgument, this._nativeConfig.GetSiteNameFromId(1U)))
			{
				siteName = this._nativeConfig.GetSiteNameFromId(1U);
				siteID = "1";
				return;
			}
			siteName = string.Empty;
			siteID = string.Empty;
			string text = null;
			if (IISMapPath.IsSiteId(siteArgument))
			{
				uint siteID2;
				if (uint.TryParse(siteArgument, out siteID2))
				{
					text = this._nativeConfig.GetSiteNameFromId(siteID2);
				}
			}
			else
			{
				uint num = this._nativeConfig.MgdResolveSiteName(siteArgument);
				if (num != 0U)
				{
					siteID = num.ToString(CultureInfo.InvariantCulture);
					siteName = siteArgument;
					return;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				siteName = text;
				siteID = siteArgument;
				return;
			}
			siteName = siteArgument;
			siteID = string.Empty;
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x00127918 File Offset: 0x00125B18
		private string MapPathWorker(string siteID, VirtualPath path)
		{
			return this.MapPathCaching(siteID, path);
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x00127922 File Offset: 0x00125B22
		string IConfigMapPath2.MapPath(string siteID, VirtualPath path)
		{
			return this.MapPathWorker(siteID, path);
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0012792C File Offset: 0x00125B2C
		string IConfigMapPath.MapPath(string siteID, string path)
		{
			return this.MapPathWorker(siteID, VirtualPath.Create(path));
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0012793C File Offset: 0x00125B3C
		string IConfigMapPath.GetAppPathForPath(string siteID, string path)
		{
			VirtualPath appPathForPathWorker = this.GetAppPathForPathWorker(siteID, VirtualPath.Create(path));
			return appPathForPathWorker.VirtualPathString;
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0012795D File Offset: 0x00125B5D
		VirtualPath IConfigMapPath2.GetAppPathForPath(string siteID, VirtualPath path)
		{
			return this.GetAppPathForPathWorker(siteID, path);
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x00127968 File Offset: 0x00125B68
		private VirtualPath GetAppPathForPathWorker(string siteID, VirtualPath path)
		{
			uint siteId = 0U;
			if (!uint.TryParse(siteID, out siteId))
			{
				return VirtualPath.RootVirtualPath;
			}
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			string text;
			try
			{
				text = ((this._nativeConfig.MgdGetAppPathForPath(siteId, path.VirtualPathString, out zero, out num) == 0 && num > 0) ? StringUtil.StringFromWCharPtr(zero, num) : null);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero);
				}
			}
			if (text == null)
			{
				return VirtualPath.RootVirtualPath;
			}
			return VirtualPath.Create(text);
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x001279F0 File Offset: 0x00125BF0
		private string MapPathCaching(string siteID, VirtualPath path)
		{
			string text = this._nativeConfig.MapPathDirect(((IServerConfig)this).GetSiteNameFromSiteID(siteID), path);
			if (text != null && text.Length == 2 && text[1] == ':')
			{
				text += "\\";
			}
			if (HttpRuntime.IsMapPathRelaxed)
			{
				text = HttpRuntime.GetRelaxedMapPathResult(text);
			}
			if (FileUtil.IsSuspiciousPhysicalPath(text))
			{
				if (!HttpRuntime.IsMapPathRelaxed)
				{
					throw new HttpException(SR.GetString("Cannot_map_path", new object[]
					{
						path
					}));
				}
				text = HttpRuntime.GetRelaxedMapPathResult(null);
			}
			return text;
		}

		// Token: 0x04002C45 RID: 11333
		private static object s_initLock = new object();

		// Token: 0x04002C46 RID: 11334
		private static ExpressServerConfig s_instance;

		// Token: 0x04002C47 RID: 11335
		private NativeConfig _nativeConfig;

		// Token: 0x04002C48 RID: 11336
		private string _currentAppSiteName;
	}
}
