using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000018 RID: 24
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal abstract class SafeCapiHandleBase : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000033C5 File Offset: 0x000015C5
		internal SafeCapiHandleBase() : base(true)
		{
		}

		// Token: 0x060000D2 RID: 210
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptContextAddRef(IntPtr hProv, IntPtr pdwReserved, int dwFlags);

		// Token: 0x060000D3 RID: 211
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000033CE File Offset: 0x000015CE
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000033D8 File Offset: 0x000015D8
		protected IntPtr ParentCsp
		{
			get
			{
				return this.m_csp;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			set
			{
				int num = 0;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (SafeCapiHandleBase.CryptContextAddRef(value, IntPtr.Zero, 0))
					{
						this.m_csp = value;
					}
					else
					{
						num = Marshal.GetLastWin32Error();
					}
				}
				if (num != 0)
				{
					throw new CryptographicException(num);
				}
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003428 File Offset: 0x00001628
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void SetParentCsp(SafeCspHandle parentCsp)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				parentCsp.DangerousAddRef(ref flag);
				IntPtr parentCsp2 = parentCsp.DangerousGetHandle();
				this.ParentCsp = parentCsp2;
			}
			finally
			{
				if (flag)
				{
					parentCsp.DangerousRelease();
				}
			}
		}

		// Token: 0x060000D7 RID: 215
		protected abstract bool ReleaseCapiChildHandle();

		// Token: 0x060000D8 RID: 216 RVA: 0x00003470 File Offset: 0x00001670
		protected sealed override bool ReleaseHandle()
		{
			bool flag = this.ReleaseCapiChildHandle();
			bool flag2 = true;
			if (this.m_csp != IntPtr.Zero)
			{
				flag2 = SafeCapiHandleBase.CryptReleaseContext(this.m_csp, 0);
			}
			return flag && flag2;
		}

		// Token: 0x040000CF RID: 207
		private IntPtr m_csp;
	}
}
