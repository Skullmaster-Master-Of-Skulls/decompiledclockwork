using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020008A9 RID: 2217
	internal sealed class SafeHashHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06005098 RID: 20632 RVA: 0x0011FE82 File Offset: 0x0011EE82
		private SafeHashHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x0011FE92 File Offset: 0x0011EE92
		internal static SafeHashHandle InvalidHandle
		{
			get
			{
				return new SafeHashHandle(IntPtr.Zero);
			}
		}

		// Token: 0x0600509A RID: 20634
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreeHash(IntPtr pHashCtx);

		// Token: 0x0600509B RID: 20635 RVA: 0x0011FE9E File Offset: 0x0011EE9E
		protected override bool ReleaseHandle()
		{
			SafeHashHandle._FreeHash(this.handle);
			return true;
		}
	}
}
