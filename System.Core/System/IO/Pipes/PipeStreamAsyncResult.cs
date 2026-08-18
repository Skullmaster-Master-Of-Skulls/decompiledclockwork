using System;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x020000B7 RID: 183
	internal sealed class PipeStreamAsyncResult : IAsyncResult
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001095E File Offset: 0x0000EB5E
		public object AsyncState
		{
			get
			{
				return this._userStateObject;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00010966 File Offset: 0x0000EB66
		public bool IsCompleted
		{
			get
			{
				return this._isComplete;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00010970 File Offset: 0x0000EB70
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

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000109E0 File Offset: 0x0000EBE0
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000109E8 File Offset: 0x0000EBE8
		private void CallUserCallbackWorker(object callbackState)
		{
			this._isComplete = true;
			if (this._waitHandle != null)
			{
				this._waitHandle.Set();
			}
			this._userCallback(this);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00010A11 File Offset: 0x0000EC11
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

		// Token: 0x04000577 RID: 1399
		internal AsyncCallback _userCallback;

		// Token: 0x04000578 RID: 1400
		internal object _userStateObject;

		// Token: 0x04000579 RID: 1401
		internal ManualResetEvent _waitHandle;

		// Token: 0x0400057A RID: 1402
		[SecurityCritical]
		internal SafePipeHandle _handle;

		// Token: 0x0400057B RID: 1403
		[SecurityCritical]
		internal unsafe NativeOverlapped* _overlapped;

		// Token: 0x0400057C RID: 1404
		internal int _EndXxxCalled;

		// Token: 0x0400057D RID: 1405
		internal int _numBytes;

		// Token: 0x0400057E RID: 1406
		internal int _errorCode;

		// Token: 0x0400057F RID: 1407
		internal bool _isMessageComplete;

		// Token: 0x04000580 RID: 1408
		internal bool _isWrite;

		// Token: 0x04000581 RID: 1409
		internal bool _isComplete;

		// Token: 0x04000582 RID: 1410
		internal bool _completedSynchronously;
	}
}
