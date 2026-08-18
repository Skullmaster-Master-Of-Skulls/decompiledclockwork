using System;
using System.Runtime.InteropServices;

namespace a.j
{
	// Token: 0x020001C7 RID: 455
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct av
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x00038924 File Offset: 0x00037924
		public av(string A_0, string A_1, string A_2, string A_3)
		{
			this.a = 512U;
			this.i = 10U;
			this.b = 0U;
			this.c = A_0;
			if (A_0 != null)
			{
				this.d = (uint)A_0.Length;
			}
			else
			{
				this.d = 0U;
			}
			this.e = A_2;
			if (A_2 != null)
			{
				this.f = (uint)A_2.Length;
			}
			else
			{
				this.f = 0U;
			}
			this.g = A_1;
			if (A_1 != null)
			{
				this.h = (uint)A_1.Length;
			}
			else
			{
				this.h = 0U;
			}
			this.j = A_3;
			if (A_3 != null)
			{
				this.k = (uint)A_3.Length;
				return;
			}
			this.k = 0U;
		}

		// Token: 0x04000A9D RID: 2717
		public uint a;

		// Token: 0x04000A9E RID: 2718
		public uint b;

		// Token: 0x04000A9F RID: 2719
		public string c;

		// Token: 0x04000AA0 RID: 2720
		public uint d;

		// Token: 0x04000AA1 RID: 2721
		public string e;

		// Token: 0x04000AA2 RID: 2722
		public uint f;

		// Token: 0x04000AA3 RID: 2723
		public string g;

		// Token: 0x04000AA4 RID: 2724
		public uint h;

		// Token: 0x04000AA5 RID: 2725
		public uint i;

		// Token: 0x04000AA6 RID: 2726
		public string j;

		// Token: 0x04000AA7 RID: 2727
		public uint k;
	}
}
