using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020008A8 RID: 2216
	internal sealed class SafeKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06005094 RID: 20628 RVA: 0x0011FE58 File Offset: 0x0011EE58
		private SafeKeyHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06005095 RID: 20629 RVA: 0x0011FE68 File Offset: 0x0011EE68
		internal static SafeKeyHandle InvalidHandle
		{
			get
			{
				return new SafeKeyHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06005096 RID: 20630
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreeHKey(IntPtr pKeyCtx);

		// Token: 0x06005097 RID: 20631 RVA: 0x0011FE74 File Offset: 0x0011EE74
		protected override bool ReleaseHandle()
		{
			SafeKeyHandle._FreeHKey(this.handle);
			return true;
		}
	}
}
