using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200017D RID: 381
	[SecuritySafeCritical]
	[Target("ImpersonatingWrapper", IsWrapper = true)]
	public class ImpersonatingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x00022D48 File Offset: 0x00020F48
		public ImpersonatingTargetWrapper() : this(null)
		{
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00022D51 File Offset: 0x00020F51
		public ImpersonatingTargetWrapper(string name, Target wrappedTarget) : this(wrappedTarget)
		{
			base.Name = name;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00022D61 File Offset: 0x00020F61
		public ImpersonatingTargetWrapper(Target wrappedTarget)
		{
			this.Domain = ".";
			this.LogOnType = SecurityLogOnType.Interactive;
			this.LogOnProvider = LogOnProviderType.Default;
			this.ImpersonationLevel = SecurityImpersonationLevel.Impersonation;
			base.WrappedTarget = wrappedTarget;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00022D9B File Offset: 0x00020F9B
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x00022DA3 File Offset: 0x00020FA3
		public string UserName { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00022DAC File Offset: 0x00020FAC
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x00022DB4 File Offset: 0x00020FB4
		public string Password { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00022DBD File Offset: 0x00020FBD
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x00022DC5 File Offset: 0x00020FC5
		[DefaultValue(".")]
		public string Domain { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00022DCE File Offset: 0x00020FCE
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x00022DD6 File Offset: 0x00020FD6
		public SecurityLogOnType LogOnType { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00022DDF File Offset: 0x00020FDF
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x00022DE7 File Offset: 0x00020FE7
		public LogOnProviderType LogOnProvider { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00022DF0 File Offset: 0x00020FF0
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x00022DF8 File Offset: 0x00020FF8
		public SecurityImpersonationLevel ImpersonationLevel { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00022E01 File Offset: 0x00021001
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x00022E09 File Offset: 0x00021009
		[DefaultValue(false)]
		public bool RevertToSelf { get; set; }

		// Token: 0x06000E5A RID: 3674 RVA: 0x00022E14 File Offset: 0x00021014
		protected override void InitializeTarget()
		{
			if (!this.RevertToSelf)
			{
				this.newIdentity = this.CreateWindowsIdentity(out this.duplicateTokenHandle);
			}
			using (this.DoImpersonate())
			{
				base.InitializeTarget();
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00022E64 File Offset: 0x00021064
		protected override void CloseTarget()
		{
			using (this.DoImpersonate())
			{
				base.CloseTarget();
			}
			if (this.duplicateTokenHandle != IntPtr.Zero)
			{
				NativeMethods.CloseHandle(this.duplicateTokenHandle);
				this.duplicateTokenHandle = IntPtr.Zero;
			}
			if (this.newIdentity != null)
			{
				this.newIdentity.Dispose();
				this.newIdentity = null;
			}
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00022EE0 File Offset: 0x000210E0
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			using (this.DoImpersonate())
			{
				base.WrappedTarget.WriteAsyncLogEvent(logEvent);
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00022F1C File Offset: 0x0002111C
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			using (this.DoImpersonate())
			{
				base.WrappedTarget.WriteAsyncLogEvents(logEvents);
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00022F58 File Offset: 0x00021158
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			using (this.DoImpersonate())
			{
				base.WrappedTarget.Flush(asyncContinuation);
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00022F94 File Offset: 0x00021194
		private IDisposable DoImpersonate()
		{
			if (this.RevertToSelf)
			{
				return new ImpersonatingTargetWrapper.ContextReverter(WindowsIdentity.Impersonate(IntPtr.Zero));
			}
			return new ImpersonatingTargetWrapper.ContextReverter(this.newIdentity.Impersonate());
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00022FC0 File Offset: 0x000211C0
		private WindowsIdentity CreateWindowsIdentity(out IntPtr handle)
		{
			IntPtr intPtr;
			if (!NativeMethods.LogonUser(this.UserName, this.Domain, this.Password, (int)this.LogOnType, (int)this.LogOnProvider, out intPtr))
			{
				throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			if (!NativeMethods.DuplicateToken(intPtr, (int)this.ImpersonationLevel, out handle))
			{
				NativeMethods.CloseHandle(intPtr);
				throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			NativeMethods.CloseHandle(intPtr);
			return new WindowsIdentity(handle);
		}

		// Token: 0x04000408 RID: 1032
		private WindowsIdentity newIdentity;

		// Token: 0x04000409 RID: 1033
		private IntPtr duplicateTokenHandle = IntPtr.Zero;

		// Token: 0x0200017E RID: 382
		internal class ContextReverter : IDisposable
		{
			// Token: 0x06000E61 RID: 3681 RVA: 0x00023032 File Offset: 0x00021232
			public ContextReverter(WindowsImpersonationContext windowsImpersonationContext)
			{
				this.wic = windowsImpersonationContext;
			}

			// Token: 0x06000E62 RID: 3682 RVA: 0x00023041 File Offset: 0x00021241
			public void Dispose()
			{
				this.wic.Undo();
			}

			// Token: 0x04000411 RID: 1041
			private WindowsImpersonationContext wic;
		}
	}
}
