using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000459 RID: 1113
	internal sealed class SafeCryptProvHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002978 RID: 10616 RVA: 0x000BC9A6 File Offset: 0x000BABA6
		private SafeCryptProvHandle() : base(true)
		{
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000BC9AF File Offset: 0x000BABAF
		internal SafeCryptProvHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000BC9C0 File Offset: 0x000BABC0
		internal static SafeCryptProvHandle InvalidHandle
		{
			get
			{
				SafeCryptProvHandle safeCryptProvHandle = new SafeCryptProvHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCryptProvHandle);
				return safeCryptProvHandle;
			}
		}

		// Token: 0x0600297B RID: 10619
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool CryptReleaseContext(IntPtr hCryptProv, uint dwFlags);

		// Token: 0x0600297C RID: 10620 RVA: 0x000BC9DF File Offset: 0x000BABDF
		protected override bool ReleaseHandle()
		{
			return SafeCryptProvHandle.CryptReleaseContext(this.handle, 0U);
		}
	}
}
