using System;
using System.Runtime.InteropServices;
using System.Security;

namespace a.j
{
	// Token: 0x020001A8 RID: 424
	internal struct n
	{
		// Token: 0x06000EDE RID: 3806 RVA: 0x000387A1 File Offset: 0x000377A1
		[SecuritySafeCritical]
		public static n f(IntPtr A_0)
		{
			return (n)Marshal.PtrToStructure(A_0, typeof(n));
		}

		// Token: 0x040009D6 RID: 2518
		public uint a;

		// Token: 0x040009D7 RID: 2519
		public IntPtr b;

		// Token: 0x040009D8 RID: 2520
		public uint c;

		// Token: 0x040009D9 RID: 2521
		public IntPtr d;

		// Token: 0x040009DA RID: 2522
		public IntPtr e;
	}
}
