using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000005 RID: 5
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCryptContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeCryptContextHandle() : base(true)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002059 File Offset: 0x00000259
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeCryptContextHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002069 File Offset: 0x00000269
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.CryptReleaseContext(this, 0U);
				return true;
			}
			return false;
		}
	}
}
