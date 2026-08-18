using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001D RID: 29
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SafeNCryptKeyHandle : SafeNCryptHandle
	{
		// Token: 0x060000F0 RID: 240
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("ncrypt.dll")]
		private static extern int NCryptFreeObject(IntPtr hObject);

		// Token: 0x060000F1 RID: 241 RVA: 0x00003883 File Offset: 0x00001A83
		public SafeNCryptKeyHandle()
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000388B File Offset: 0x00001A8B
		public SafeNCryptKeyHandle(IntPtr handle, SafeHandle parentHandle) : base(handle, parentHandle)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003895 File Offset: 0x00001A95
		internal SafeNCryptKeyHandle Duplicate()
		{
			return base.Duplicate<SafeNCryptKeyHandle>();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000389D File Offset: 0x00001A9D
		protected override bool ReleaseNativeHandle()
		{
			return SafeNCryptKeyHandle.NCryptFreeObject(this.handle) == 0;
		}
	}
}
