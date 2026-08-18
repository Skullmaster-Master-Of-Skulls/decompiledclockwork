using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000023 RID: 35
	[SecurityCritical(SecurityCriticalScope.Everything)]
	public sealed class SafeMemoryMappedViewHandle : SafeBuffer
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000397E File Offset: 0x00001B7E
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeMemoryMappedViewHandle() : base(true)
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003987 File Offset: 0x00001B87
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeMemoryMappedViewHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003997 File Offset: 0x00001B97
		protected override bool ReleaseHandle()
		{
			if (UnsafeNativeMethods.UnmapViewOfFile(this.handle))
			{
				this.handle = IntPtr.Zero;
				return true;
			}
			return false;
		}
	}
}
