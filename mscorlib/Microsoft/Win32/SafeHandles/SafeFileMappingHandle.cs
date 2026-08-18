using System;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000479 RID: 1145
	internal sealed class SafeFileMappingHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002DA6 RID: 11686 RVA: 0x00098F0B File Offset: 0x00097F0B
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeFileMappingHandle() : base(true)
		{
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x00098F14 File Offset: 0x00097F14
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeFileMappingHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x00098F24 File Offset: 0x00097F24
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
