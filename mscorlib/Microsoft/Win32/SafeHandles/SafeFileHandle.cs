using System;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000478 RID: 1144
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	public sealed class SafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002DA3 RID: 11683 RVA: 0x00098EE5 File Offset: 0x00097EE5
		private SafeFileHandle() : base(true)
		{
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x00098EEE File Offset: 0x00097EEE
		public SafeFileHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x00098EFE File Offset: 0x00097EFE
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
