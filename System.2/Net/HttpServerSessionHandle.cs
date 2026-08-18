using System;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001EA RID: 490
	[SuppressUnmanagedCodeSecurity]
	internal sealed class HttpServerSessionHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012F2 RID: 4850 RVA: 0x00064166 File Offset: 0x00062366
		internal HttpServerSessionHandle(ulong id)
		{
			this.serverSessionId = id;
			base.SetHandle(new IntPtr(1));
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00064181 File Offset: 0x00062381
		internal ulong DangerousGetServerSessionId()
		{
			return this.serverSessionId;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00064189 File Offset: 0x00062389
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || Interlocked.Increment(ref this.disposed) != 1 || UnsafeNclNativeMethods.HttpApi.HttpCloseServerSession(this.serverSessionId) == 0U;
		}

		// Token: 0x04001538 RID: 5432
		private int disposed;

		// Token: 0x04001539 RID: 5433
		private ulong serverSessionId;
	}
}
