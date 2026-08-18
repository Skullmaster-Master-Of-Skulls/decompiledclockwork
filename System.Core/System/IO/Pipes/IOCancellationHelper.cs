using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace System.IO.Pipes
{
	// Token: 0x020000AE RID: 174
	internal class IOCancellationHelper
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		public IOCancellationHelper(CancellationToken cancellationToken)
		{
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		[SecurityCritical]
		public unsafe void AllowCancellation(SafeHandle handle, NativeOverlapped* overlapped)
		{
			if (!this._cancellationToken.CanBeCanceled)
			{
				return;
			}
			this._handle = handle;
			this._overlapped = overlapped;
			if (this._cancellationToken.IsCancellationRequested)
			{
				this.Cancel();
				return;
			}
			this._cancellationRegistration = this._cancellationToken.Register(new Action(this.Cancel));
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000E136 File Offset: 0x0000C336
		[SecurityCritical]
		public void SetOperationCompleted()
		{
			if (this._overlapped != null)
			{
				this._cancellationRegistration.Dispose();
				this._handle = null;
				this._overlapped = null;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000E15C File Offset: 0x0000C35C
		public void ThrowIOOperationAborted()
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			__Error.OperationAborted();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000E170 File Offset: 0x0000C370
		[SecurityCritical]
		private unsafe void Cancel()
		{
			SafeHandle handle = this._handle;
			NativeOverlapped* overlapped = this._overlapped;
			if (handle != null && !handle.IsInvalid && overlapped != null)
			{
				if (!UnsafeNativeMethods.CancelIoEx(handle, overlapped))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
				}
				this.SetOperationCompleted();
			}
		}

		// Token: 0x0400054A RID: 1354
		private CancellationToken _cancellationToken;

		// Token: 0x0400054B RID: 1355
		private CancellationTokenRegistration _cancellationRegistration;

		// Token: 0x0400054C RID: 1356
		[SecurityCritical]
		private SafeHandle _handle;

		// Token: 0x0400054D RID: 1357
		[SecurityCritical]
		private unsafe NativeOverlapped* _overlapped;
	}
}
