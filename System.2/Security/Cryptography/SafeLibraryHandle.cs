using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000457 RID: 1111
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002970 RID: 10608 RVA: 0x000BC93F File Offset: 0x000BAB3F
		private SafeLibraryHandle() : base(true)
		{
		}

		// Token: 0x06002971 RID: 10609
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FreeLibrary([In] IntPtr hModule);

		// Token: 0x06002972 RID: 10610 RVA: 0x000BC948 File Offset: 0x000BAB48
		protected override bool ReleaseHandle()
		{
			return SafeLibraryHandle.FreeLibrary(this.handle);
		}
	}
}
