using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000458 RID: 1112
	internal sealed class SafeLocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002973 RID: 10611 RVA: 0x000BC955 File Offset: 0x000BAB55
		private SafeLocalAllocHandle() : base(true)
		{
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000BC95E File Offset: 0x000BAB5E
		internal SafeLocalAllocHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x000BC970 File Offset: 0x000BAB70
		internal static SafeLocalAllocHandle InvalidHandle
		{
			get
			{
				SafeLocalAllocHandle safeLocalAllocHandle = new SafeLocalAllocHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeLocalAllocHandle);
				return safeLocalAllocHandle;
			}
		}

		// Token: 0x06002976 RID: 10614
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x06002977 RID: 10615 RVA: 0x000BC98F File Offset: 0x000BAB8F
		protected override bool ReleaseHandle()
		{
			return SafeLocalAllocHandle.LocalFree(this.handle) == IntPtr.Zero;
		}
	}
}
