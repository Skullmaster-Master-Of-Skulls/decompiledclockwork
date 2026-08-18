using System;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001E9 RID: 489
	[SuppressUnmanagedCodeSecurity]
	internal sealed class HttpRequestQueueV2Handle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012EF RID: 4847 RVA: 0x0006412E File Offset: 0x0006232E
		private HttpRequestQueueV2Handle()
		{
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00064136 File Offset: 0x00062336
		internal IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0006413E File Offset: 0x0006233E
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || Interlocked.Increment(ref this.disposed) != 1 || UnsafeNclNativeMethods.SafeNetHandles.HttpCloseRequestQueue(this.handle) == 0U;
		}

		// Token: 0x04001537 RID: 5431
		private int disposed;
	}
}
