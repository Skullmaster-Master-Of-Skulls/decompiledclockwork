using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020008A7 RID: 2215
	internal sealed class SafeProvHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06005090 RID: 20624 RVA: 0x0011FE2E File Offset: 0x0011EE2E
		private SafeProvHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06005091 RID: 20625 RVA: 0x0011FE3E File Offset: 0x0011EE3E
		internal static SafeProvHandle InvalidHandle
		{
			get
			{
				return new SafeProvHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06005092 RID: 20626
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreeCSP(IntPtr pProvCtx);

		// Token: 0x06005093 RID: 20627 RVA: 0x0011FE4A File Offset: 0x0011EE4A
		protected override bool ReleaseHandle()
		{
			SafeProvHandle._FreeCSP(this.handle);
			return true;
		}
	}
}
