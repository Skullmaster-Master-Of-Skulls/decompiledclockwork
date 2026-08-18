using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001F RID: 31
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SafeNCryptSecretHandle : SafeNCryptHandle
	{
		// Token: 0x060000FA RID: 250
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("ncrypt.dll")]
		private static extern int NCryptFreeObject(IntPtr hObject);

		// Token: 0x060000FB RID: 251 RVA: 0x000038D6 File Offset: 0x00001AD6
		protected override bool ReleaseNativeHandle()
		{
			return SafeNCryptSecretHandle.NCryptFreeObject(this.handle) == 0;
		}
	}
}
