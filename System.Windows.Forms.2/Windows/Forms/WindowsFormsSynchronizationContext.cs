using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000444 RID: 1092
	public sealed class WindowsFormsSynchronizationContext : SynchronizationContext, IDisposable
	{
		// Token: 0x06004BBF RID: 19391 RVA: 0x0013B0D8 File Offset: 0x001392D8
		public WindowsFormsSynchronizationContext()
		{
			this.DestinationThread = Thread.CurrentThread;
			Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
			if (threadContext != null)
			{
				this.controlToSendTo = threadContext.MarshalingControl;
			}
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x0013B10B File Offset: 0x0013930B
		private WindowsFormsSynchronizationContext(Control marshalingControl, Thread destinationThread)
		{
			this.controlToSendTo = marshalingControl;
			this.DestinationThread = destinationThread;
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x0013B121 File Offset: 0x00139321
		// (set) Token: 0x06004BC2 RID: 19394 RVA: 0x0013B14A File Offset: 0x0013934A
		private Thread DestinationThread
		{
			get
			{
				if (this.destinationThreadRef != null && this.destinationThreadRef.IsAlive)
				{
					return this.destinationThreadRef.Target as Thread;
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.destinationThreadRef = new WeakReference(value);
				}
			}
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x0013B15B File Offset: 0x0013935B
		public void Dispose()
		{
			if (this.controlToSendTo != null)
			{
				if (!this.controlToSendTo.IsDisposed)
				{
					this.controlToSendTo.Dispose();
				}
				this.controlToSendTo = null;
			}
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0013B184 File Offset: 0x00139384
		public override void Send(SendOrPostCallback d, object state)
		{
			Thread destinationThread = this.DestinationThread;
			if (destinationThread == null || !destinationThread.IsAlive)
			{
				throw new InvalidAsynchronousStateException(SR.GetString("ThreadNoLongerValid"));
			}
			if (this.controlToSendTo != null)
			{
				this.controlToSendTo.Invoke(d, new object[]
				{
					state
				});
			}
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x0013B1D2 File Offset: 0x001393D2
		public override void Post(SendOrPostCallback d, object state)
		{
			if (this.controlToSendTo != null)
			{
				this.controlToSendTo.BeginInvoke(d, new object[]
				{
					state
				});
			}
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0013B1F3 File Offset: 0x001393F3
		public override SynchronizationContext CreateCopy()
		{
			return new WindowsFormsSynchronizationContext(this.controlToSendTo, this.DestinationThread);
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06004BC7 RID: 19399 RVA: 0x0013B206 File Offset: 0x00139406
		// (set) Token: 0x06004BC8 RID: 19400 RVA: 0x0013B210 File Offset: 0x00139410
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static bool AutoInstall
		{
			get
			{
				return !WindowsFormsSynchronizationContext.dontAutoInstall;
			}
			set
			{
				WindowsFormsSynchronizationContext.dontAutoInstall = !value;
			}
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x0013B21C File Offset: 0x0013941C
		internal static void InstallIfNeeded()
		{
			if (!WindowsFormsSynchronizationContext.AutoInstall || WindowsFormsSynchronizationContext.inSyncContextInstallation)
			{
				return;
			}
			if (SynchronizationContext.Current == null)
			{
				WindowsFormsSynchronizationContext.previousSyncContext = null;
			}
			if (WindowsFormsSynchronizationContext.previousSyncContext != null)
			{
				return;
			}
			WindowsFormsSynchronizationContext.inSyncContextInstallation = true;
			try
			{
				SynchronizationContext synchronizationContext = AsyncOperationManager.SynchronizationContext;
				if (synchronizationContext == null || synchronizationContext.GetType() == typeof(SynchronizationContext))
				{
					WindowsFormsSynchronizationContext.previousSyncContext = synchronizationContext;
					new PermissionSet(PermissionState.Unrestricted).Assert();
					try
					{
						AsyncOperationManager.SynchronizationContext = new WindowsFormsSynchronizationContext();
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			finally
			{
				WindowsFormsSynchronizationContext.inSyncContextInstallation = false;
			}
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x0013B2BC File Offset: 0x001394BC
		public static void Uninstall()
		{
			WindowsFormsSynchronizationContext.Uninstall(true);
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x0013B2C4 File Offset: 0x001394C4
		internal static void Uninstall(bool turnOffAutoInstall)
		{
			if (WindowsFormsSynchronizationContext.AutoInstall)
			{
				WindowsFormsSynchronizationContext windowsFormsSynchronizationContext = AsyncOperationManager.SynchronizationContext as WindowsFormsSynchronizationContext;
				if (windowsFormsSynchronizationContext != null)
				{
					try
					{
						new PermissionSet(PermissionState.Unrestricted).Assert();
						if (WindowsFormsSynchronizationContext.previousSyncContext == null)
						{
							AsyncOperationManager.SynchronizationContext = new SynchronizationContext();
						}
						else
						{
							AsyncOperationManager.SynchronizationContext = WindowsFormsSynchronizationContext.previousSyncContext;
						}
					}
					finally
					{
						WindowsFormsSynchronizationContext.previousSyncContext = null;
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			if (turnOffAutoInstall)
			{
				WindowsFormsSynchronizationContext.AutoInstall = false;
			}
		}

		// Token: 0x04002842 RID: 10306
		private Control controlToSendTo;

		// Token: 0x04002843 RID: 10307
		private WeakReference destinationThreadRef;

		// Token: 0x04002844 RID: 10308
		[ThreadStatic]
		private static bool dontAutoInstall;

		// Token: 0x04002845 RID: 10309
		[ThreadStatic]
		private static bool inSyncContextInstallation;

		// Token: 0x04002846 RID: 10310
		[ThreadStatic]
		private static SynchronizationContext previousSyncContext;
	}
}
