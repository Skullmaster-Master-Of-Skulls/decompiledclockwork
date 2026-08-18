using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000072 RID: 114
	internal sealed class DirMonCompletion : IDisposable
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0000AB59 File Offset: 0x00008D59
		internal static int ActiveDirMonCompletions
		{
			get
			{
				return DirMonCompletion._activeDirMonCompletions;
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000AB60 File Offset: 0x00008D60
		internal DirMonCompletion(DirectoryMonitor dirMon, string dir, bool watchSubtree, uint notifyFilter)
		{
			this._dirMon = dirMon;
			NativeFileChangeNotification nativeFileChangeNotification = new NativeFileChangeNotification(this.OnFileChange);
			this._ndirMonCompletionHandleLock = new object();
			try
			{
			}
			finally
			{
				object ndirMonCompletionHandleLock = this._ndirMonCompletionHandleLock;
				lock (ndirMonCompletionHandleLock)
				{
					this._rootCallback = GCHandle.Alloc(nativeFileChangeNotification);
					int num = UnsafeNativeMethods.DirMonOpen(dir, HttpRuntime.AppDomainAppId, watchSubtree, notifyFilter, dirMon.FcnMode, nativeFileChangeNotification, out this._ndirMonCompletionPtr);
					if (num != 0)
					{
						this._rootCallback.Free();
						throw FileChangesMonitor.CreateFileMonitoringException(num, dir);
					}
					this._ndirMonCompletionHandle = new HandleRef(this, this._ndirMonCompletionPtr);
					Interlocked.Increment(ref DirMonCompletion._activeDirMonCompletions);
				}
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000AC2C File Offset: 0x00008E2C
		~DirMonCompletion()
		{
			this.Dispose(false);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000AC5C File Offset: 0x00008E5C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000AC6C File Offset: 0x00008E6C
		private void Dispose(bool disposing)
		{
			if (Interlocked.Exchange(ref this._disposed, 1) == 0)
			{
				object ndirMonCompletionHandleLock = this._ndirMonCompletionHandleLock;
				lock (ndirMonCompletionHandleLock)
				{
					bool fNeedToDispose = !AppDomain.CurrentDomain.IsFinalizingForUnload();
					HandleRef ndirMonCompletionHandle = this._ndirMonCompletionHandle;
					if (ndirMonCompletionHandle.Handle != IntPtr.Zero)
					{
						this._ndirMonCompletionHandle = new HandleRef(this, IntPtr.Zero);
						UnsafeNativeMethods.DirMonClose(ndirMonCompletionHandle, fNeedToDispose);
					}
				}
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		private void OnFileChange(FileAction action, string fileName, long ticks)
		{
			DateTime utcCompletion;
			if (ticks == 0L)
			{
				utcCompletion = DateTime.MinValue;
			}
			else
			{
				utcCompletion = DateTimeUtil.FromFileTimeToUtc(ticks);
			}
			if (action == FileAction.Dispose)
			{
				if (this._rootCallback.IsAllocated)
				{
					this._rootCallback.Free();
				}
				Interlocked.Decrement(ref DirMonCompletion._activeDirMonCompletions);
				return;
			}
			using (new ApplicationImpersonationContext())
			{
				this._dirMon.OnFileChange(action, fileName, utcCompletion);
			}
		}

		// Token: 0x0400020C RID: 524
		private static int _activeDirMonCompletions;

		// Token: 0x0400020D RID: 525
		private DirectoryMonitor _dirMon;

		// Token: 0x0400020E RID: 526
		private IntPtr _ndirMonCompletionPtr;

		// Token: 0x0400020F RID: 527
		private HandleRef _ndirMonCompletionHandle;

		// Token: 0x04000210 RID: 528
		private GCHandle _rootCallback;

		// Token: 0x04000211 RID: 529
		private int _disposed;

		// Token: 0x04000212 RID: 530
		private object _ndirMonCompletionHandleLock;
	}
}
