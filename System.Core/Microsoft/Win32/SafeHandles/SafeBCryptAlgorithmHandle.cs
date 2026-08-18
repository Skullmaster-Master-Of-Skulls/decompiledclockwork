using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000014 RID: 20
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeBCryptAlgorithmHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x0000331E File Offset: 0x0000151E
		private SafeBCryptAlgorithmHandle() : base(true)
		{
		}

		// Token: 0x060000C3 RID: 195
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("bcrypt")]
		private static extern BCryptNative.ErrorCode BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, int flags);

		// Token: 0x060000C4 RID: 196 RVA: 0x00003327 File Offset: 0x00001527
		protected override bool ReleaseHandle()
		{
			return SafeBCryptAlgorithmHandle.BCryptCloseAlgorithmProvider(this.handle, 0) == BCryptNative.ErrorCode.Success;
		}
	}
}
