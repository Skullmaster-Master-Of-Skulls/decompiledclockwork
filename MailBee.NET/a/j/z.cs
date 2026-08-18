using System;
using System.Runtime.InteropServices;
using System.Security;

namespace a.j
{
	// Token: 0x020001BC RID: 444
	internal class z
	{
		// Token: 0x06000EE1 RID: 3809
		[DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int FormatMessage(int A_0, IntPtr A_1, int A_2, int A_3, out IntPtr A_4, int A_5, IntPtr A_6);

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000387E8 File Offset: 0x000377E8
		[SecuritySafeCritical]
		public static string a(int A_0)
		{
			string result = null;
			int a_ = 4864;
			IntPtr intPtr = 0;
			int num = z.FormatMessage(a_, IntPtr.Zero, A_0, 0, out intPtr, 0, IntPtr.Zero);
			if (num > 0)
			{
				result = Marshal.PtrToStringAuto(intPtr, num).TrimEnd(new char[]
				{
					'.',
					'\r',
					'\n'
				});
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}
	}
}
