using System;
using System.Runtime.InteropServices;

namespace a.j
{
	// Token: 0x020001C6 RID: 454
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct ap
	{
		// Token: 0x06000F13 RID: 3859 RVA: 0x000388B4 File Offset: 0x000378B4
		public ap(string A_0, string A_1, string A_2)
		{
			this.g = 2U;
			this.a = A_0;
			if (A_0 != null)
			{
				this.b = (uint)A_0.Length;
			}
			else
			{
				this.b = 0U;
			}
			this.c = A_2;
			if (A_2 != null)
			{
				this.d = (uint)A_2.Length;
			}
			else
			{
				this.d = 0U;
			}
			this.e = A_1;
			if (A_1 != null)
			{
				this.f = (uint)A_1.Length;
				return;
			}
			this.f = 0U;
		}

		// Token: 0x04000A96 RID: 2710
		public string a;

		// Token: 0x04000A97 RID: 2711
		public uint b;

		// Token: 0x04000A98 RID: 2712
		public string c;

		// Token: 0x04000A99 RID: 2713
		public uint d;

		// Token: 0x04000A9A RID: 2714
		public string e;

		// Token: 0x04000A9B RID: 2715
		public uint f;

		// Token: 0x04000A9C RID: 2716
		public uint g;
	}
}
