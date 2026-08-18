using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200071D RID: 1821
	internal sealed class NativeConfig : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x060057A5 RID: 22437 RVA: 0x00133212 File Offset: 0x00131412
		private NativeConfig()
		{
		}

		// Token: 0x060057A6 RID: 22438 RVA: 0x0013321C File Offset: 0x0013141C
		internal NativeConfig(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			int hresult = 0;
			using (new IISVersionHelper(version))
			{
				hresult = UnsafeIISMethods.MgdCreateNativeConfigSystem(out this._nativeConfig);
			}
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x060057A7 RID: 22439 RVA: 0x00133274 File Offset: 0x00131474
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~NativeConfig()
		{
			this.Dispose(false);
		}

		// Token: 0x060057A8 RID: 22440 RVA: 0x001332A4 File Offset: 0x001314A4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060057A9 RID: 22441 RVA: 0x001332B4 File Offset: 0x001314B4
		private void Dispose(bool disposing)
		{
			if (this._nativeConfig != IntPtr.Zero)
			{
				IntPtr intPtr = Interlocked.Exchange(ref this._nativeConfig, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					int hresult = UnsafeIISMethods.MgdReleaseNativeConfigSystem(intPtr);
					Misc.ThrowIfFailedHr(hresult);
				}
			}
		}

		// Token: 0x060057AA RID: 22442 RVA: 0x00133300 File Offset: 0x00131500
		internal string GetSiteNameFromId(uint siteID)
		{
			IntPtr zero = IntPtr.Zero;
			int length = 0;
			string result = null;
			try
			{
				result = ((UnsafeIISMethods.MgdGetSiteNameFromId(this._nativeConfig, siteID, out zero, out length) == 0 && zero != IntPtr.Zero) ? StringUtil.StringFromWCharPtr(zero, length) : string.Empty);
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

		// Token: 0x060057AB RID: 22443 RVA: 0x00133370 File Offset: 0x00131570
		internal string MapPathDirect(string siteName, VirtualPath path)
		{
			string result = null;
			IntPtr zero = IntPtr.Zero;
			int length = 0;
			try
			{
				int num = UnsafeIISMethods.MgdMapPathDirect(this._nativeConfig, siteName, path.VirtualPathString, out zero, out length);
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

		// Token: 0x060057AC RID: 22444 RVA: 0x00133400 File Offset: 0x00131600
		internal int MgdGetAppCollection(string siteName, string virtualPath, out IntPtr pBstr, out int cBstr, out IntPtr pAppCollection, out int count)
		{
			return UnsafeIISMethods.MgdGetAppCollection(this._nativeConfig, siteName, virtualPath, out pBstr, out cBstr, out pAppCollection, out count);
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x00133416 File Offset: 0x00131616
		internal bool MgdIsWithinApp(string siteName, string appPath, string virtualPath)
		{
			return UnsafeIISMethods.MgdIsWithinApp(this._nativeConfig, siteName, appPath, virtualPath);
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x00133426 File Offset: 0x00131626
		internal int MgdGetVrPathCreds(string siteName, string virtualPath, out IntPtr bstrUserName, out int cchUserName, out IntPtr bstrPassword, out int cchPassword)
		{
			return UnsafeIISMethods.MgdGetVrPathCreds(this._nativeConfig, siteName, virtualPath, out bstrUserName, out cchUserName, out bstrPassword, out cchPassword);
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x0013343C File Offset: 0x0013163C
		internal uint MgdResolveSiteName(string siteName)
		{
			return UnsafeIISMethods.MgdResolveSiteName(this._nativeConfig, siteName);
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x0013344A File Offset: 0x0013164A
		internal int MgdGetAppPathForPath(uint siteId, string virtualPath, out IntPtr bstrPath, out int cchPath)
		{
			return UnsafeIISMethods.MgdGetAppPathForPath(this._nativeConfig, siteId, virtualPath, out bstrPath, out cchPath);
		}

		// Token: 0x04002E98 RID: 11928
		private IntPtr _nativeConfig;
	}
}
