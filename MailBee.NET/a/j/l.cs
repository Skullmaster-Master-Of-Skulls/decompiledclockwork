using System;
using System.Runtime.InteropServices;

namespace a.j
{
	// Token: 0x020001A2 RID: 418
	internal class l
	{
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00038791 File Offset: 0x00037791
		private l()
		{
		}

		// Token: 0x06000ED9 RID: 3801
		[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int LogonUser(string A_0, string A_1, string A_2, int A_3, int A_4, ref IntPtr A_5);

		// Token: 0x06000EDA RID: 3802
		[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int ImpersonateLoggedOnUser(IntPtr A_0);

		// Token: 0x06000EDB RID: 3803
		[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int RevertToSelf();

		// Token: 0x06000EDC RID: 3804
		[DllImport("kernel32", SetLastError = true)]
		public static extern int CloseHandle(IntPtr A_0);
	}
}
