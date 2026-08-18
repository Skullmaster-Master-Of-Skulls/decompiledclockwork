using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008BB RID: 2235
	internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06005158 RID: 20824 RVA: 0x00123DE2 File Offset: 0x00122DE2
		private SafeCertContextHandle() : base(true)
		{
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x00123DEB File Offset: 0x00122DEB
		internal SafeCertContextHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600515A RID: 20826 RVA: 0x00123DFB File Offset: 0x00122DFB
		internal static SafeCertContextHandle InvalidHandle
		{
			get
			{
				return new SafeCertContextHandle(IntPtr.Zero);
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600515B RID: 20827 RVA: 0x00123E07 File Offset: 0x00122E07
		internal IntPtr pCertContext
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return Marshal.ReadIntPtr(this.handle);
			}
		}

		// Token: 0x0600515C RID: 20828
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreePCertContext(IntPtr pCert);

		// Token: 0x0600515D RID: 20829 RVA: 0x00123E2C File Offset: 0x00122E2C
		protected override bool ReleaseHandle()
		{
			SafeCertContextHandle._FreePCertContext(this.handle);
			return true;
		}
	}
}
