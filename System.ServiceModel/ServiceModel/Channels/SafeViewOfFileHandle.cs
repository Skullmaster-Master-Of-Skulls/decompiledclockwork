using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C3 RID: 2243
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeViewOfFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060055B3 RID: 21939 RVA: 0x00139942 File Offset: 0x00137B42
		internal SafeViewOfFileHandle() : base(true)
		{
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x0013994B File Offset: 0x00137B4B
		protected override bool ReleaseHandle()
		{
			if (UnsafeNativeMethods.UnmapViewOfFile(this.handle) != 0)
			{
				this.handle = IntPtr.Zero;
				return true;
			}
			return false;
		}
	}
}
