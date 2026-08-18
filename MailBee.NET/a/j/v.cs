using System;
using System.Runtime.InteropServices;

namespace a.j
{
	// Token: 0x020001C4 RID: 452
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct v
	{
		// Token: 0x06000F11 RID: 3857 RVA: 0x00038884 File Offset: 0x00037884
		public v(IntPtr A_0)
		{
			this.b = A_0;
			this.a = A_0;
		}

		// Token: 0x04000A92 RID: 2706
		public IntPtr a;

		// Token: 0x04000A93 RID: 2707
		public IntPtr b;
	}
}
