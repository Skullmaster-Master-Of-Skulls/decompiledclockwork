using System;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200047A RID: 1146
	internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002DA9 RID: 11689 RVA: 0x00098F31 File Offset: 0x00097F31
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeFindHandle() : base(true)
		{
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x00098F3A File Offset: 0x00097F3A
		protected override bool ReleaseHandle()
		{
			return Win32Native.FindClose(this.handle);
		}
	}
}
