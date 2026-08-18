using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200045C RID: 1116
	internal sealed class SafeCryptMsgHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002987 RID: 10631 RVA: 0x000BCA7D File Offset: 0x000BAC7D
		private SafeCryptMsgHandle() : base(true)
		{
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000BCA86 File Offset: 0x000BAC86
		internal SafeCryptMsgHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000BCA98 File Offset: 0x000BAC98
		internal static SafeCryptMsgHandle InvalidHandle
		{
			get
			{
				SafeCryptMsgHandle safeCryptMsgHandle = new SafeCryptMsgHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCryptMsgHandle);
				return safeCryptMsgHandle;
			}
		}

		// Token: 0x0600298A RID: 10634
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CryptMsgClose(IntPtr handle);

		// Token: 0x0600298B RID: 10635 RVA: 0x000BCAB7 File Offset: 0x000BACB7
		protected override bool ReleaseHandle()
		{
			return SafeCryptMsgHandle.CryptMsgClose(this.handle);
		}
	}
}
