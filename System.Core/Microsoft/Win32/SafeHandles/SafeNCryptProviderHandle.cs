using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001E RID: 30
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SafeNCryptProviderHandle : SafeNCryptHandle
	{
		// Token: 0x060000F5 RID: 245
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("ncrypt.dll")]
		private static extern int NCryptFreeObject(IntPtr hObject);

		// Token: 0x060000F6 RID: 246 RVA: 0x000038AD File Offset: 0x00001AAD
		internal SafeNCryptProviderHandle Duplicate()
		{
			return base.Duplicate<SafeNCryptProviderHandle>();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000038B5 File Offset: 0x00001AB5
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetHandleValue(IntPtr newHandleValue)
		{
			base.SetHandle(newHandleValue);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000038BE File Offset: 0x00001ABE
		protected override bool ReleaseNativeHandle()
		{
			return SafeNCryptProviderHandle.NCryptFreeObject(this.handle) == 0;
		}
	}
}
