using System;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x020000B4 RID: 180
	internal sealed class PipeAsyncResult : IAsyncResult
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000F7AD File Offset: 0x0000D9AD
		public object AsyncState
		{
			get
			{
				return this._userStateObject;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000F7B5 File Offset: 0x0000D9B5
		public bool IsCompleted
		{
			get
			{
				return this._isComplete;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		public unsafe WaitHandle AsyncWaitHandle
		{
			[SecurityCritical]
			get
			{
				if (this._waitHandle == null)
				{
					ManualResetEvent manualResetEvent = new ManualResetEvent(false);
					if (this._overlapped != null && this._overlapped->EventHandle != IntPtr.Zero)
					{
						manualResetEvent.SafeWaitHandle = new SafeWaitHandle(this._overlapped->EventHandle, true);
					}
					if (this._isComplete)
					{
						manualResetEvent.Set();
					}
					this._waitHandle = manualResetEvent;
				}
				return this._waitHandle;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000F830 File Offset: 0x0000DA30
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000F838 File Offset: 0x0000DA38
		private void CallUserCallbackWorker(object callbackState)
		{
			this._isComplete = true;
			if (this._waitHandle != null)
			{
				this._waitHandle.Set();
			}
			this._userCallback(this);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000F861 File Offset: 0x0000DA61
		internal void CallUserCallback()
		{
			if (this._userCallback != null)
			{
				this._completedSynchronously = false;
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.CallUserCallbackWorker));
				return;
			}
			this._isComplete = true;
			if (this._waitHandle != null)
			{
				this._waitHandle.Set();
			}
		}

		// Token: 0x0400055A RID: 1370
		internal AsyncCallback _userCallback;

		// Token: 0x0400055B RID: 1371
		internal object _userStateObject;

		// Token: 0x0400055C RID: 1372
		internal ManualResetEvent _waitHandle;

		// Token: 0x0400055D RID: 1373
		[SecurityCritical]
		internal SafePipeHandle _handle;

		// Token: 0x0400055E RID: 1374
		[SecurityCritical]
		internal unsafe NativeOverlapped* _overlapped;

		// Token: 0x0400055F RID: 1375
		internal int _EndXxxCalled;

		// Token: 0x04000560 RID: 1376
		internal int _errorCode;

		// Token: 0x04000561 RID: 1377
		internal bool _isComplete;

		// Token: 0x04000562 RID: 1378
		internal bool _completedSynchronously;
	}
}
