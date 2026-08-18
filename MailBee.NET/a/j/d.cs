using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace a.j
{
	// Token: 0x020001A9 RID: 425
	internal struct d
	{
		// Token: 0x06000EDF RID: 3807 RVA: 0x000387B8 File Offset: 0x000377B8
		[SecuritySafeCritical]
		public static d m(IntPtr A_0)
		{
			return (d)Marshal.PtrToStructure(A_0, typeof(d));
		}

		// Token: 0x040009DB RID: 2523
		public uint a;

		// Token: 0x040009DC RID: 2524
		public g b;

		// Token: 0x040009DD RID: 2525
		public u c;

		// Token: 0x040009DE RID: 2526
		public c d;

		// Token: 0x040009DF RID: 2527
		public System.Runtime.InteropServices.ComTypes.FILETIME e;

		// Token: 0x040009E0 RID: 2528
		public System.Runtime.InteropServices.ComTypes.FILETIME f;

		// Token: 0x040009E1 RID: 2529
		public c g;

		// Token: 0x040009E2 RID: 2530
		public ad h;

		// Token: 0x040009E3 RID: 2531
		public k i;

		// Token: 0x040009E4 RID: 2532
		public k j;

		// Token: 0x040009E5 RID: 2533
		public uint k;

		// Token: 0x040009E6 RID: 2534
		public IntPtr l;
	}
}
