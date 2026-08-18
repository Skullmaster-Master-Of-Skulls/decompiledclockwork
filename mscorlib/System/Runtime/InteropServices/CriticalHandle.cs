using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000480 RID: 1152
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x06002DBA RID: 11706 RVA: 0x00099027 File Offset: 0x00098027
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandle(IntPtr invalidHandleValue)
		{
			this.handle = invalidHandleValue;
			this._isClosed = false;
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x00099040 File Offset: 0x00098040
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x00099070 File Offset: 0x00098070
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Cleanup()
		{
			if (this.IsClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this.IsInvalid)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!this.ReleaseHandle())
			{
				this.FireCustomerDebugProbe();
			}
			Marshal.SetLastWin32Error(lastWin32Error);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002DBD RID: 11709
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FireCustomerDebugProbe();

		// Token: 0x06002DBE RID: 11710 RVA: 0x000990B6 File Offset: 0x000980B6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x000990BF File Offset: 0x000980BF
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002DC0 RID: 11712
		public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get; }

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000990C7 File Offset: 0x000980C7
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000990D0 File Offset: 0x000980D0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000990D9 File Offset: 0x000980D9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Dispose(bool disposing)
		{
			this.Cleanup();
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000990E1 File Offset: 0x000980E1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void SetHandleAsInvalid()
		{
			this._isClosed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002DC5 RID: 11717
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		// Token: 0x04001799 RID: 6041
		protected IntPtr handle;

		// Token: 0x0400179A RID: 6042
		private bool _isClosed;
	}
}
