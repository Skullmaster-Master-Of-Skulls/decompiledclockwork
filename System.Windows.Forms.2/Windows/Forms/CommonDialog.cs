using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000164 RID: 356
	[ToolboxItemFilter("System.Windows.Forms")]
	public abstract class CommonDialog : Component
	{
		// Token: 0x06000EBA RID: 3770 RVA: 0x0002C27A File Offset: 0x0002A47A
		public CommonDialog()
		{
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0002C282 File Offset: 0x0002A482
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x0002C28A File Offset: 0x0002A48A
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06000EBD RID: 3773 RVA: 0x0002C293 File Offset: 0x0002A493
		// (remove) Token: 0x06000EBE RID: 3774 RVA: 0x0002C2A6 File Offset: 0x0002A4A6
		[SRDescription("CommonDialogHelpRequested")]
		public event EventHandler HelpRequest
		{
			add
			{
				base.Events.AddHandler(CommonDialog.EventHelpRequest, value);
			}
			remove
			{
				base.Events.RemoveHandler(CommonDialog.EventHelpRequest, value);
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0002C2BC File Offset: 0x0002A4BC
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (msg == 272)
			{
				CommonDialog.MoveToScreenCenter(hWnd);
				this.defaultControlHwnd = wparam;
				UnsafeNativeMethods.SetFocus(new HandleRef(null, wparam));
			}
			else if (msg == 7)
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(null, hWnd), 1105, 0, 0);
			}
			else if (msg == 1105)
			{
				UnsafeNativeMethods.SetFocus(new HandleRef(this, this.defaultControlHwnd));
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0002C328 File Offset: 0x0002A528
		internal static void MoveToScreenCenter(IntPtr hWnd)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(null, hWnd), ref rect);
			Rectangle workingArea = Screen.GetWorkingArea(Control.MousePosition);
			int x = workingArea.X + (workingArea.Width - rect.right + rect.left) / 2;
			int y = workingArea.Y + (workingArea.Height - rect.bottom + rect.top) / 3;
			SafeNativeMethods.SetWindowPos(new HandleRef(null, hWnd), NativeMethods.NullHandleRef, x, y, 0, 0, 21);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002C3B0 File Offset: 0x0002A5B0
		protected virtual void OnHelpRequest(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CommonDialog.EventHelpRequest];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002C3E0 File Offset: 0x0002A5E0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual IntPtr OwnerWndProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (msg == CommonDialog.helpMsg)
			{
				if (NativeWindow.WndProcShouldBeDebuggable)
				{
					this.OnHelpRequest(EventArgs.Empty);
				}
				else
				{
					try
					{
						this.OnHelpRequest(EventArgs.Empty);
					}
					catch (Exception t)
					{
						Application.OnThreadException(t);
					}
				}
				return IntPtr.Zero;
			}
			return UnsafeNativeMethods.CallWindowProc(this.defOwnerWndProc, hWnd, msg, wparam, lparam);
		}

		// Token: 0x06000EC3 RID: 3779
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public abstract void Reset();

		// Token: 0x06000EC4 RID: 3780
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected abstract bool RunDialog(IntPtr hwndOwner);

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0002C448 File Offset: 0x0002A648
		public DialogResult ShowDialog()
		{
			return this.ShowDialog(null);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0002C454 File Offset: 0x0002A654
		public DialogResult ShowDialog(IWin32Window owner)
		{
			IntSecurity.SafeSubWindows.Demand();
			if (!SystemInformation.UserInteractive)
			{
				throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
			}
			NativeWindow nativeWindow = null;
			IntPtr intPtr = IntPtr.Zero;
			DialogResult result = DialogResult.Cancel;
			try
			{
				if (owner != null)
				{
					intPtr = Control.GetSafeHandle(owner);
				}
				if (intPtr == IntPtr.Zero)
				{
					intPtr = UnsafeNativeMethods.GetActiveWindow();
				}
				if (intPtr == IntPtr.Zero)
				{
					nativeWindow = new NativeWindow();
					nativeWindow.CreateHandle(new CreateParams());
					intPtr = nativeWindow.Handle;
				}
				if (CommonDialog.helpMsg == 0)
				{
					CommonDialog.helpMsg = SafeNativeMethods.RegisterWindowMessage("commdlg_help");
				}
				NativeMethods.WndProc wndProc = new NativeMethods.WndProc(this.OwnerWndProc);
				this.hookedWndProc = Marshal.GetFunctionPointerForDelegate(wndProc);
				IntPtr userCookie = IntPtr.Zero;
				try
				{
					this.defOwnerWndProc = UnsafeNativeMethods.SetWindowLong(new HandleRef(this, intPtr), -4, wndProc);
					if (Application.UseVisualStyles)
					{
						userCookie = UnsafeNativeMethods.ThemingScope.Activate();
					}
					Application.BeginModalMessageLoop();
					try
					{
						result = (this.RunDialog(intPtr) ? DialogResult.OK : DialogResult.Cancel);
					}
					finally
					{
						Application.EndModalMessageLoop();
					}
				}
				finally
				{
					IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, intPtr), -4);
					if (IntPtr.Zero != this.defOwnerWndProc || windowLong != this.hookedWndProc)
					{
						UnsafeNativeMethods.SetWindowLong(new HandleRef(this, intPtr), -4, new HandleRef(this, this.defOwnerWndProc));
					}
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
					this.defOwnerWndProc = IntPtr.Zero;
					this.hookedWndProc = IntPtr.Zero;
					GC.KeepAlive(wndProc);
				}
			}
			finally
			{
				if (nativeWindow != null)
				{
					nativeWindow.DestroyHandle();
				}
			}
			return result;
		}

		// Token: 0x040007FA RID: 2042
		private static readonly object EventHelpRequest = new object();

		// Token: 0x040007FB RID: 2043
		private const int CDM_SETDEFAULTFOCUS = 1105;

		// Token: 0x040007FC RID: 2044
		private static int helpMsg;

		// Token: 0x040007FD RID: 2045
		private IntPtr defOwnerWndProc;

		// Token: 0x040007FE RID: 2046
		private IntPtr hookedWndProc;

		// Token: 0x040007FF RID: 2047
		private IntPtr defaultControlHwnd;

		// Token: 0x04000800 RID: 2048
		private object userData;
	}
}
