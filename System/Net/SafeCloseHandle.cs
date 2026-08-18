using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x0200050F RID: 1295
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCloseHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600281F RID: 10271 RVA: 0x000A57FA File Offset: 0x000A47FA
		private SafeCloseHandle()
		{
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000A5802 File Offset: 0x000A4802
		internal IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000A580A File Offset: 0x000A480A
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || Interlocked.Increment(ref this._disposed) != 1 || UnsafeNclNativeMethods.SafeNetHandles.CloseHandle(this.handle);
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000A5830 File Offset: 0x000A4830
		internal static int GetSecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle safeHandle)
		{
			int result = -2146893055;
			bool flag = false;
			safeHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				phContext.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					phContext.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					result = UnsafeNclNativeMethods.SafeNetHandles.QuerySecurityContextToken(ref phContext._handle, out safeHandle);
					phContext.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000A58A8 File Offset: 0x000A48A8
		internal static SafeCloseHandle CreateRequestQueueHandle()
		{
			SafeCloseHandle safeCloseHandle = null;
			uint num = UnsafeNclNativeMethods.SafeNetHandles.HttpCreateHttpHandle(out safeCloseHandle, 0U);
			if (safeCloseHandle != null && num != 0U)
			{
				safeCloseHandle.SetHandleAsInvalid();
				throw new HttpListenerException((int)num);
			}
			return safeCloseHandle;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000A58D4 File Offset: 0x000A48D4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Abort()
		{
			this.ReleaseHandle();
			base.SetHandleAsInvalid();
		}

		// Token: 0x0400276A RID: 10090
		private const string SECURITY = "security.dll";

		// Token: 0x0400276B RID: 10091
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x0400276C RID: 10092
		private const string HTTPAPI = "httpapi.dll";

		// Token: 0x0400276D RID: 10093
		private int _disposed;
	}
}
