using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000097 RID: 151
	[SuppressUnmanagedCodeSecurity]
	internal class OpsInit
	{
		// Token: 0x0600076F RID: 1903
		[DllImport("kernel32.dll")]
		public static extern int SetDllDirectory(string pathName);

		// Token: 0x06000770 RID: 1904
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CheckVersionCompatibility(string version);

		// Token: 0x06000771 RID: 1905
		[DllImport("kernel32")]
		public static extern IntPtr LoadLibrary(string fileName);

		// Token: 0x06000772 RID: 1906
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetFileAttributes(string fileName);
	}
}
