using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008BB RID: 2235
	internal class OverlappedContext
	{
		// Token: 0x06005535 RID: 21813 RVA: 0x00138D04 File Offset: 0x00136F04
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public unsafe OverlappedContext()
		{
			if (OverlappedContext.completeCallback == null)
			{
				OverlappedContext.completeCallback = Fx.ThunkCallback(new IOCompletionCallback(OverlappedContext.CompleteCallback));
			}
			if (OverlappedContext.eventCallback == null)
			{
				OverlappedContext.eventCallback = Fx.ThunkCallback(new WaitOrTimerCallback(OverlappedContext.EventCallback));
			}
			if (OverlappedContext.cleanupCallback == null)
			{
				OverlappedContext.cleanupCallback = Fx.ThunkCallback(new WaitOrTimerCallback(OverlappedContext.CleanupCallback));
			}
			this.bufferHolder = new object[]
			{
				OverlappedContext.dummyBuffer
			};
			this.overlapped = new Overlapped();
			this.nativeOverlapped = this.overlapped.UnsafePack(OverlappedContext.completeCallback, this.bufferHolder);
			this.pinnedHandle = GCHandle.FromIntPtr(*(IntPtr*)(this.nativeOverlapped + ((IntPtr.Size == 4) ? -4 : -3) * (IntPtr)sizeof(IntPtr)));
			this.pinnedTarget = this.pinnedHandle.Target;
			this.rootedHolder = new OverlappedContext.RootedHolder();
			this.overlapped.AsyncResult = this.rootedHolder;
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00138E00 File Offset: 0x00137000
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		~OverlappedContext()
		{
			if (this.nativeOverlapped != null && !AppDomain.CurrentDomain.IsFinalizingForUnload() && !Environment.HasShutdownStarted)
			{
				if (this.syncOperationPending)
				{
					ThreadPool.UnsafeRegisterWaitForSingleObject(this.rootedHolder.EventHolder, OverlappedContext.cleanupCallback, this, -1, true);
				}
				else
				{
					Overlapped.Free(this.nativeOverlapped);
				}
			}
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x00138E74 File Offset: 0x00137074
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public unsafe void Free()
		{
			if (this.pendingCallback != null)
			{
				throw Fx.AssertAndThrow("OverlappedContext.Free called while async operation is pending.");
			}
			if (this.syncOperationPending)
			{
				throw Fx.AssertAndThrow("OverlappedContext.Free called while sync operation is pending.");
			}
			if (this.nativeOverlapped == null)
			{
				throw Fx.AssertAndThrow("OverlappedContext.Free called multiple times.");
			}
			this.pinnedTarget = null;
			NativeOverlapped* nativeOverlappedPtr = this.nativeOverlapped;
			this.nativeOverlapped = null;
			Overlapped.Free(nativeOverlappedPtr);
			if (this.completionEvent != null)
			{
				this.completionEvent.Close();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x00138EF1 File Offset: 0x001370F1
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public bool FreeOrDefer()
		{
			if (this.pendingCallback != null || this.syncOperationPending)
			{
				this.deferredFree = true;
				return false;
			}
			this.Free();
			return true;
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x00138F13 File Offset: 0x00137113
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public bool FreeIfDeferred()
		{
			return this.deferredFree && this.FreeOrDefer();
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x00138F28 File Offset: 0x00137128
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public unsafe void StartAsyncOperation(byte[] buffer, OverlappedIOCompleteCallback callback, bool bound)
		{
			if (callback == null)
			{
				throw Fx.AssertAndThrow("StartAsyncOperation called with null callback.");
			}
			if (this.pendingCallback != null)
			{
				throw Fx.AssertAndThrow("StartAsyncOperation called while another is in progress.");
			}
			if (this.syncOperationPending)
			{
				throw Fx.AssertAndThrow("StartAsyncOperation called while a sync operation was already pending.");
			}
			if (this.nativeOverlapped == null)
			{
				throw Fx.AssertAndThrow("StartAsyncOperation called on freed OverlappedContext.");
			}
			this.pendingCallback = callback;
			if (buffer != null)
			{
				this.bufferHolder[0] = buffer;
				this.pinnedHandle.Target = this.pinnedTarget;
				this.bufferPtr = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0));
			}
			if (bound)
			{
				this.overlapped.EventHandleIntPtr = IntPtr.Zero;
				this.rootedHolder.ThisHolder = this;
				return;
			}
			if (this.completionEvent != null)
			{
				this.completionEvent.Reset();
			}
			this.overlapped.EventHandleIntPtr = this.EventHandle;
			this.registration = ThreadPool.UnsafeRegisterWaitForSingleObject(this.completionEvent, OverlappedContext.eventCallback, this, -1, true);
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x00139014 File Offset: 0x00137214
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public void CancelAsyncOperation()
		{
			this.rootedHolder.ThisHolder = null;
			if (this.registration != null)
			{
				this.registration.Unregister(null);
				this.registration = null;
			}
			this.bufferPtr = null;
			this.bufferHolder[0] = OverlappedContext.dummyBuffer;
			this.pendingCallback = null;
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x00139068 File Offset: 0x00137268
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public unsafe void StartSyncOperation(byte[] buffer, ref object holder)
		{
			if (this.syncOperationPending)
			{
				throw Fx.AssertAndThrow("StartSyncOperation called while an operation was already pending.");
			}
			if (this.pendingCallback != null)
			{
				throw Fx.AssertAndThrow("StartSyncOperation called while an async operation was already pending.");
			}
			if (this.nativeOverlapped == null)
			{
				throw Fx.AssertAndThrow("StartSyncOperation called on freed OverlappedContext.");
			}
			this.overlapped.EventHandleIntPtr = this.EventHandle;
			this.rootedHolder.EventHolder = this.completionEvent;
			this.syncOperationPending = true;
			if (buffer != null)
			{
				holder = buffer;
				this.pinnedHandle.Target = this.pinnedTarget;
				this.bufferPtr = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0));
			}
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x00139102 File Offset: 0x00137302
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public bool WaitForSyncOperation(TimeSpan timeout)
		{
			return this.WaitForSyncOperation(timeout, ref this.bufferHolder[0]);
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x00139118 File Offset: 0x00137318
		[SecurityCritical]
		public bool WaitForSyncOperation(TimeSpan timeout, ref object holder)
		{
			if (!this.syncOperationPending)
			{
				throw Fx.AssertAndThrow("WaitForSyncOperation called while no operation was pending.");
			}
			if (!UnsafeNativeMethods.HasOverlappedIoCompleted(this.nativeOverlapped) && !TimeoutHelper.WaitOne(this.completionEvent, timeout))
			{
				GC.SuppressFinalize(this);
				ThreadPool.UnsafeRegisterWaitForSingleObject(this.completionEvent, OverlappedContext.cleanupCallback, this, -1, true);
				return false;
			}
			this.CancelSyncOperation(ref holder);
			return true;
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x00139177 File Offset: 0x00137377
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public void CancelSyncOperation(ref object holder)
		{
			this.bufferPtr = null;
			holder = OverlappedContext.dummyBuffer;
			this.syncOperationPending = false;
			this.rootedHolder.EventHolder = null;
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06005540 RID: 21824 RVA: 0x0013919B File Offset: 0x0013739B
		public object[] Holder
		{
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				return this.bufferHolder;
			}
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06005541 RID: 21825 RVA: 0x001391A4 File Offset: 0x001373A4
		public unsafe byte* BufferPtr
		{
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				byte* ptr = this.bufferPtr;
				if (ptr == null)
				{
					throw Fx.AssertAndThrow("Pointer requested while no operation pending or no buffer provided.");
				}
				return ptr;
			}
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06005542 RID: 21826 RVA: 0x001391CC File Offset: 0x001373CC
		public unsafe NativeOverlapped* NativeOverlapped
		{
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				NativeOverlapped* ptr = this.nativeOverlapped;
				if (ptr == null)
				{
					throw Fx.AssertAndThrow("NativeOverlapped pointer requested after it was freed.");
				}
				return ptr;
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06005543 RID: 21827 RVA: 0x001391F1 File Offset: 0x001373F1
		private IntPtr EventHandle
		{
			get
			{
				if (this.completionEvent == null)
				{
					this.completionEvent = new ManualResetEvent(false);
					this.eventHandle = (IntPtr)(1L | (long)this.completionEvent.SafeWaitHandle.DangerousGetHandle());
				}
				return this.eventHandle;
			}
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x00139230 File Offset: 0x00137430
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private unsafe static void CompleteCallback(uint error, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
			OverlappedContext thisHolder = ((OverlappedContext.RootedHolder)overlapped.AsyncResult).ThisHolder;
			thisHolder.rootedHolder.ThisHolder = null;
			thisHolder.bufferPtr = null;
			thisHolder.bufferHolder[0] = OverlappedContext.dummyBuffer;
			OverlappedIOCompleteCallback overlappedIOCompleteCallback = thisHolder.pendingCallback;
			thisHolder.pendingCallback = null;
			overlappedIOCompleteCallback(true, (int)error, checked((int)numBytes));
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x00139290 File Offset: 0x00137490
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private static void EventCallback(object state, bool timedOut)
		{
			OverlappedContext overlappedContext = state as OverlappedContext;
			if (timedOut)
			{
				if (overlappedContext == null || overlappedContext.rootedHolder == null)
				{
					DiagnosticUtility.FailFast("Can't prevent heap corruption.");
				}
				overlappedContext.rootedHolder.ThisHolder = overlappedContext;
				return;
			}
			overlappedContext.registration = null;
			overlappedContext.bufferPtr = null;
			overlappedContext.bufferHolder[0] = OverlappedContext.dummyBuffer;
			OverlappedIOCompleteCallback overlappedIOCompleteCallback = overlappedContext.pendingCallback;
			overlappedContext.pendingCallback = null;
			overlappedIOCompleteCallback(false, 0, 0);
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x00139300 File Offset: 0x00137500
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private static void CleanupCallback(object state, bool timedOut)
		{
			OverlappedContext overlappedContext = state as OverlappedContext;
			if (timedOut)
			{
				return;
			}
			overlappedContext.pinnedTarget = null;
			overlappedContext.rootedHolder.EventHolder.Close();
			Overlapped.Free(overlappedContext.nativeOverlapped);
		}

		// Token: 0x0400335D RID: 13149
		private const int HandleOffsetFromOverlapped32 = -4;

		// Token: 0x0400335E RID: 13150
		private const int HandleOffsetFromOverlapped64 = -3;

		// Token: 0x0400335F RID: 13151
		private static IOCompletionCallback completeCallback;

		// Token: 0x04003360 RID: 13152
		private static WaitOrTimerCallback eventCallback;

		// Token: 0x04003361 RID: 13153
		private static WaitOrTimerCallback cleanupCallback;

		// Token: 0x04003362 RID: 13154
		private static byte[] dummyBuffer = new byte[0];

		// Token: 0x04003363 RID: 13155
		private object[] bufferHolder;

		// Token: 0x04003364 RID: 13156
		private unsafe byte* bufferPtr;

		// Token: 0x04003365 RID: 13157
		private unsafe NativeOverlapped* nativeOverlapped;

		// Token: 0x04003366 RID: 13158
		private GCHandle pinnedHandle;

		// Token: 0x04003367 RID: 13159
		private object pinnedTarget;

		// Token: 0x04003368 RID: 13160
		private Overlapped overlapped;

		// Token: 0x04003369 RID: 13161
		private OverlappedContext.RootedHolder rootedHolder;

		// Token: 0x0400336A RID: 13162
		private OverlappedIOCompleteCallback pendingCallback;

		// Token: 0x0400336B RID: 13163
		private bool deferredFree;

		// Token: 0x0400336C RID: 13164
		private bool syncOperationPending;

		// Token: 0x0400336D RID: 13165
		private ManualResetEvent completionEvent;

		// Token: 0x0400336E RID: 13166
		private IntPtr eventHandle;

		// Token: 0x0400336F RID: 13167
		private RegisteredWaitHandle registration;

		// Token: 0x02000D7D RID: 3453
		private class RootedHolder : IAsyncResult
		{
			// Token: 0x17001C2A RID: 7210
			// (get) Token: 0x06007E70 RID: 32368 RVA: 0x001D79FE File Offset: 0x001D5BFE
			// (set) Token: 0x06007E71 RID: 32369 RVA: 0x001D7A06 File Offset: 0x001D5C06
			public OverlappedContext ThisHolder
			{
				get
				{
					return this.overlappedBuffer;
				}
				set
				{
					this.overlappedBuffer = value;
				}
			}

			// Token: 0x17001C2B RID: 7211
			// (get) Token: 0x06007E72 RID: 32370 RVA: 0x001D7A0F File Offset: 0x001D5C0F
			// (set) Token: 0x06007E73 RID: 32371 RVA: 0x001D7A17 File Offset: 0x001D5C17
			public ManualResetEvent EventHolder
			{
				get
				{
					return this.eventHolder;
				}
				set
				{
					this.eventHolder = value;
				}
			}

			// Token: 0x17001C2C RID: 7212
			// (get) Token: 0x06007E74 RID: 32372 RVA: 0x001D7A20 File Offset: 0x001D5C20
			object IAsyncResult.AsyncState
			{
				get
				{
					throw Fx.AssertAndThrow("RootedHolder.AsyncState called.");
				}
			}

			// Token: 0x17001C2D RID: 7213
			// (get) Token: 0x06007E75 RID: 32373 RVA: 0x001D7A2C File Offset: 0x001D5C2C
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					throw Fx.AssertAndThrow("RootedHolder.AsyncWaitHandle called.");
				}
			}

			// Token: 0x17001C2E RID: 7214
			// (get) Token: 0x06007E76 RID: 32374 RVA: 0x001D7A38 File Offset: 0x001D5C38
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					throw Fx.AssertAndThrow("RootedHolder.CompletedSynchronously called.");
				}
			}

			// Token: 0x17001C2F RID: 7215
			// (get) Token: 0x06007E77 RID: 32375 RVA: 0x001D7A44 File Offset: 0x001D5C44
			bool IAsyncResult.IsCompleted
			{
				get
				{
					throw Fx.AssertAndThrow("RootedHolder.IsCompleted called.");
				}
			}

			// Token: 0x04004870 RID: 18544
			private OverlappedContext overlappedBuffer;

			// Token: 0x04004871 RID: 18545
			private ManualResetEvent eventHolder;
		}
	}
}
