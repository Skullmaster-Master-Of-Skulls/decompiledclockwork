using System;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200047D RID: 1149
	internal sealed class SafeViewOfFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002DB2 RID: 11698 RVA: 0x00098FA9 File Offset: 0x00097FA9
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeViewOfFileHandle() : base(true)
		{
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00098FB2 File Offset: 0x00097FB2
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeViewOfFileHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x00098FC2 File Offset: 0x00097FC2
		protected override bool ReleaseHandle()
		{
			if (Win32Native.UnmapViewOfFile(this.handle))
			{
				this.handle = IntPtr.Zero;
				return true;
			}
			return false;
		}
	}
}
