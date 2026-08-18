using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200047C RID: 1148
	internal sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002DAE RID: 11694 RVA: 0x00098F71 File Offset: 0x00097F71
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeRegistryHandle() : base(true)
		{
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x00098F7A File Offset: 0x00097F7A
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeRegistryHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x06002DB0 RID: 11696
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll")]
		private static extern int RegCloseKey(IntPtr hKey);

		// Token: 0x06002DB1 RID: 11697 RVA: 0x00098F8C File Offset: 0x00097F8C
		protected override bool ReleaseHandle()
		{
			int num = SafeRegistryHandle.RegCloseKey(this.handle);
			return num == 0;
		}
	}
}
