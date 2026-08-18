using System;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x020005BC RID: 1468
	internal sealed class FileStreamAsyncResult : IAsyncResult
	{
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000B48E4 File Offset: 0x000B38E4
		public object AsyncState
		{
			get
			{
				return this._userStateObject;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000B48EC File Offset: 0x000B38EC
		public bool IsCompleted
		{
			get
			{
				return this._isComplete;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000B48F4 File Offset: 0x000B38F4
		public unsafe WaitHandle AsyncWaitHandle
		{
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

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000B4964 File Offset: 0x000B3964
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000B496C File Offset: 0x000B396C
		internal static FileStreamAsyncResult CreateBufferedReadResult(int numBufferedBytes, AsyncCallback userCallback, object userStateObject)
		{
			return new FileStreamAsyncResult
			{
				_userCallback = userCallback,
				_userStateObject = userStateObject,
				_isWrite = false,
				_numBufferedBytes = numBufferedBytes
			};
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000B499C File Offset: 0x000B399C
		private void CallUserCallbackWorker(object callbackState)
		{
			this._isComplete = true;
			if (this._waitHandle != null)
			{
				this._waitHandle.Set();
			}
			this._userCallback(this);
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000B49C5 File Offset: 0x000B39C5
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

		// Token: 0x04001C51 RID: 7249
		internal AsyncCallback _userCallback;

		// Token: 0x04001C52 RID: 7250
		internal object _userStateObject;

		// Token: 0x04001C53 RID: 7251
		internal ManualResetEvent _waitHandle;

		// Token: 0x04001C54 RID: 7252
		internal SafeFileHandle _handle;

		// Token: 0x04001C55 RID: 7253
		internal unsafe NativeOverlapped* _overlapped;

		// Token: 0x04001C56 RID: 7254
		internal int _EndXxxCalled;

		// Token: 0x04001C57 RID: 7255
		internal int _numBytes;

		// Token: 0x04001C58 RID: 7256
		internal int _errorCode;

		// Token: 0x04001C59 RID: 7257
		internal int _numBufferedBytes;

		// Token: 0x04001C5A RID: 7258
		internal bool _isWrite;

		// Token: 0x04001C5B RID: 7259
		internal bool _isComplete;

		// Token: 0x04001C5C RID: 7260
		internal bool _completedSynchronously;
	}
}
