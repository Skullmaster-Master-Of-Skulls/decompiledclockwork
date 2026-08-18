using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000015 RID: 21
	[SecurityCritical]
	internal sealed class SafeCertChainHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000042C2 File Offset: 0x000024C2
		private SafeCertChainHandle() : base(true)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000042D8 File Offset: 0x000024D8
		internal SafeCertChainHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000447C File Offset: 0x0000267C
		internal static SafeCertChainHandle InvalidHandle
		{
			get
			{
				SafeCertChainHandle safeCertChainHandle = new SafeCertChainHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertChainHandle);
				return safeCertChainHandle;
			}
		}

		// Token: 0x06000099 RID: 153
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern void CertFreeCertificateChain(IntPtr handle);

		// Token: 0x0600009A RID: 154 RVA: 0x0000449B File Offset: 0x0000269B
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeCertChainHandle.CertFreeCertificateChain(this.handle);
			return true;
		}
	}
}
