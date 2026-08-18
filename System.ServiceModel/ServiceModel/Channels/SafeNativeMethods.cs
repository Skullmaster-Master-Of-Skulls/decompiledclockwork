using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008BD RID: 2237
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		// Token: 0x06005551 RID: 21841
		[DllImport("kernel32.dll")]
		private static extern uint GetSystemTimeAdjustment(out int adjustment, out uint increment, out uint adjustmentDisabled);

		// Token: 0x06005552 RID: 21842
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void GetSystemTimeAsFileTime(out System.Runtime.InteropServices.ComTypes.FILETIME time);

		// Token: 0x06005553 RID: 21843 RVA: 0x0013951C File Offset: 0x0013771C
		public static void GetSystemTimeAsFileTime(out long time)
		{
			System.Runtime.InteropServices.ComTypes.FILETIME filetime;
			SafeNativeMethods.GetSystemTimeAsFileTime(out filetime);
			time = 0L;
			time |= (long)((ulong)filetime.dwHighDateTime);
			time <<= 32;
			time |= (long)((ulong)filetime.dwLowDateTime);
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x00139554 File Offset: 0x00137754
		[SecuritySafeCritical]
		internal static long GetSystemTimeResolution()
		{
			int num;
			uint num2;
			uint num3;
			if (SafeNativeMethods.GetSystemTimeAdjustment(out num, out num2, out num3) != 0U)
			{
				return (long)((ulong)num2);
			}
			return 150000L;
		}

		// Token: 0x04003374 RID: 13172
		public const string KERNEL32 = "kernel32.dll";
	}
}
