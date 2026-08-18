using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000014 RID: 20
	[SecurityCritical]
	internal sealed class SafeCryptMsgHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000042C2 File Offset: 0x000024C2
		private SafeCryptMsgHandle() : base(true)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000042D8 File Offset: 0x000024D8
		internal SafeCryptMsgHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004450 File Offset: 0x00002650
		internal static SafeCryptMsgHandle InvalidHandle
		{
			get
			{
				SafeCryptMsgHandle safeCryptMsgHandle = new SafeCryptMsgHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCryptMsgHandle);
				return safeCryptMsgHandle;
			}
		}

		// Token: 0x06000094 RID: 148
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CryptMsgClose(IntPtr handle);

		// Token: 0x06000095 RID: 149 RVA: 0x0000446F File Offset: 0x0000266F
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCryptMsgHandle.CryptMsgClose(this.handle);
		}
	}
}
