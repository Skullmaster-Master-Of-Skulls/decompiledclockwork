using System;
using System.Runtime.InteropServices;

namespace a.j
{
	// Token: 0x020001C5 RID: 453
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct b
	{
		// Token: 0x06000F12 RID: 3858 RVA: 0x000388A1 File Offset: 0x000378A1
		public b(uint A_0)
		{
			this.a = A_0;
			this.b = 0;
		}

		// Token: 0x04000A94 RID: 2708
		public uint a;

		// Token: 0x04000A95 RID: 2709
		public int b;
	}
}
