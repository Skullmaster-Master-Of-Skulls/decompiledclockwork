using System;
using System.Runtime.InteropServices;
using System.Security;

namespace a.j
{
	// Token: 0x020001AF RID: 431
	internal struct g
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x000387CF File Offset: 0x000377CF
		[SecuritySafeCritical]
		public static g c(IntPtr A_0)
		{
			return (g)Marshal.PtrToStructure(A_0, typeof(g));
		}

		// Token: 0x040009F2 RID: 2546
		public uint a;

		// Token: 0x040009F3 RID: 2547
		public IntPtr b;
	}
}
