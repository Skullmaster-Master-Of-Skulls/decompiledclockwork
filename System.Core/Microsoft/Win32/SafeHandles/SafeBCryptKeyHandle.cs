using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000016 RID: 22
	[SecuritySafeCritical]
	internal sealed class SafeBCryptKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000338E File Offset: 0x0000158E
		internal SafeBCryptKeyHandle() : base(true)
		{
		}

		// Token: 0x060000CB RID: 203
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("bcrypt.dll")]
		internal static extern BCryptNative.ErrorCode BCryptDestroyKey(IntPtr hKey);

		// Token: 0x060000CC RID: 204 RVA: 0x00003397 File Offset: 0x00001597
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeBCryptKeyHandle.BCryptDestroyKey(this.handle) == BCryptNative.ErrorCode.Success;
		}
	}
}
