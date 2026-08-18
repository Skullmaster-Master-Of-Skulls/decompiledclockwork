using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001A RID: 26
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeCapiKeyHandle : SafeCapiHandleBase
	{
		// Token: 0x060000DD RID: 221 RVA: 0x000034F9 File Offset: 0x000016F9
		private SafeCapiKeyHandle()
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003504 File Offset: 0x00001704
		internal static SafeCapiKeyHandle InvalidHandle
		{
			get
			{
				if (SafeCapiKeyHandle.s_invalidHandle == null)
				{
					SafeCapiKeyHandle safeCapiKeyHandle = new SafeCapiKeyHandle();
					safeCapiKeyHandle.SetHandle(IntPtr.Zero);
					GC.SuppressFinalize(safeCapiKeyHandle);
					SafeCapiKeyHandle.s_invalidHandle = safeCapiKeyHandle;
				}
				return SafeCapiKeyHandle.s_invalidHandle;
			}
		}

		// Token: 0x060000DF RID: 223
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptDestroyKey(IntPtr hKey);

		// Token: 0x060000E0 RID: 224 RVA: 0x00003540 File Offset: 0x00001740
		internal SafeCapiKeyHandle Duplicate()
		{
			SafeCapiKeyHandle safeCapiKeyHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!CapiNative.UnsafeNativeMethods.CryptDuplicateKey(this, IntPtr.Zero, 0, out safeCapiKeyHandle))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				if (safeCapiKeyHandle != null && !safeCapiKeyHandle.IsInvalid && base.ParentCsp != IntPtr.Zero)
				{
					safeCapiKeyHandle.ParentCsp = base.ParentCsp;
				}
			}
			return safeCapiKeyHandle;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000035AC File Offset: 0x000017AC
		protected override bool ReleaseCapiChildHandle()
		{
			return SafeCapiKeyHandle.CryptDestroyKey(this.handle);
		}

		// Token: 0x040000D1 RID: 209
		private static volatile SafeCapiKeyHandle s_invalidHandle;
	}
}
