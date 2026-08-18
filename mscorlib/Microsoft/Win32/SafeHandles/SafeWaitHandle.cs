using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200047E RID: 1150
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	public sealed class SafeWaitHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002DB5 RID: 11701 RVA: 0x00098FDF File Offset: 0x00097FDF
		private SafeWaitHandle() : base(true)
		{
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x00098FE8 File Offset: 0x00097FE8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public SafeWaitHandle(IntPtr existingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x00098FF8 File Offset: 0x00097FF8
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
