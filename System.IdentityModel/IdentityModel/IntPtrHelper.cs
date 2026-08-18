using System;

namespace System.IdentityModel
{
	// Token: 0x02000091 RID: 145
	internal static class IntPtrHelper
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x00011F4C File Offset: 0x0001014C
		internal static IntPtr Add(IntPtr a, int b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x04000449 RID: 1097
		private const string KERNEL32 = "kernel32.dll";
	}
}
