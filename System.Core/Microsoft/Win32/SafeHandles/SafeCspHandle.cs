using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001B RID: 27
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeCspHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x000035B9 File Offset: 0x000017B9
		private SafeCspHandle() : base(true)
		{
		}

		// Token: 0x060000E3 RID: 227
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptContextAddRef(SafeCspHandle hProv, IntPtr pdwReserved, int dwFlags);

		// Token: 0x060000E4 RID: 228
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);

		// Token: 0x060000E5 RID: 229 RVA: 0x000035C4 File Offset: 0x000017C4
		public SafeCspHandle Duplicate()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			SafeCspHandle result;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr handle = base.DangerousGetHandle();
				int num = 0;
				SafeCspHandle safeCspHandle = new SafeCspHandle();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (!SafeCspHandle.CryptContextAddRef(this, IntPtr.Zero, 0))
					{
						num = Marshal.GetLastWin32Error();
					}
					else
					{
						safeCspHandle.SetHandle(handle);
					}
				}
				if (num != 0)
				{
					safeCspHandle.Dispose();
					throw new CryptographicException(num);
				}
				result = safeCspHandle;
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003654 File Offset: 0x00001854
		protected override bool ReleaseHandle()
		{
			return SafeCspHandle.CryptReleaseContext(this.handle, 0);
		}
	}
}
