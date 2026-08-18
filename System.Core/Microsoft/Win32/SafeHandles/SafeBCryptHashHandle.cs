using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000015 RID: 21
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeBCryptHashHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00003338 File Offset: 0x00001538
		private SafeBCryptHashHandle() : base(true)
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003341 File Offset: 0x00001541
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003349 File Offset: 0x00001549
		internal IntPtr HashObject
		{
			get
			{
				return this.m_hashObject;
			}
			set
			{
				this.m_hashObject = value;
			}
		}

		// Token: 0x060000C8 RID: 200
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("bcrypt")]
		private static extern BCryptNative.ErrorCode BCryptDestroyHash(IntPtr hHash);

		// Token: 0x060000C9 RID: 201 RVA: 0x00003354 File Offset: 0x00001554
		protected override bool ReleaseHandle()
		{
			bool result = SafeBCryptHashHandle.BCryptDestroyHash(this.handle) == BCryptNative.ErrorCode.Success;
			if (this.m_hashObject != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.m_hashObject);
			}
			return result;
		}

		// Token: 0x040000CE RID: 206
		private IntPtr m_hashObject;
	}
}
