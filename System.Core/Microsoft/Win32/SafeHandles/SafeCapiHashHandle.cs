using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000019 RID: 25
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeCapiHashHandle : SafeCapiHandleBase
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000034A8 File Offset: 0x000016A8
		private SafeCapiHashHandle()
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000034B0 File Offset: 0x000016B0
		public static SafeCapiHashHandle InvalidHandle
		{
			get
			{
				if (SafeCapiHashHandle.s_invalidHandle == null)
				{
					SafeCapiHashHandle safeCapiHashHandle = new SafeCapiHashHandle();
					safeCapiHashHandle.SetHandle(IntPtr.Zero);
					GC.SuppressFinalize(safeCapiHashHandle);
					SafeCapiHashHandle.s_invalidHandle = safeCapiHashHandle;
				}
				return SafeCapiHashHandle.s_invalidHandle;
			}
		}

		// Token: 0x060000DB RID: 219
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptDestroyHash(IntPtr hHash);

		// Token: 0x060000DC RID: 220 RVA: 0x000034EC File Offset: 0x000016EC
		protected override bool ReleaseCapiChildHandle()
		{
			return SafeCapiHashHandle.CryptDestroyHash(this.handle);
		}

		// Token: 0x040000D0 RID: 208
		private static volatile SafeCapiHashHandle s_invalidHandle;
	}
}
