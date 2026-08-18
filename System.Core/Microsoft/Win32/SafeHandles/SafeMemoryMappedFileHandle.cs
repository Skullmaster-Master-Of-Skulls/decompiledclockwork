using System;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000022 RID: 34
	[SecurityCritical(SecurityCriticalScope.Everything)]
	public sealed class SafeMemoryMappedFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00003958 File Offset: 0x00001B58
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeMemoryMappedFileHandle() : base(true)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003961 File Offset: 0x00001B61
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeMemoryMappedFileHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003971 File Offset: 0x00001B71
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.CloseHandle(this.handle);
		}
	}
}
