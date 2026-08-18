using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200000F RID: 15
	[SecurityCritical]
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000042C2 File Offset: 0x000024C2
		private SafeLibraryHandle() : base(true)
		{
		}

		// Token: 0x06000077 RID: 119
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FreeLibrary([In] IntPtr hModule);

		// Token: 0x06000078 RID: 120 RVA: 0x000042CB File Offset: 0x000024CB
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeLibraryHandle.FreeLibrary(this.handle);
		}
	}
}
