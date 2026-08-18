using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NLog.Internal
{
	// Token: 0x02000099 RID: 153
	internal static class NativeMethods
	{
		// Token: 0x060004E7 RID: 1255
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);

		// Token: 0x060004E8 RID: 1256
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x060004E9 RID: 1257
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DuplicateToken(IntPtr existingTokenHandle, int impersonationLevel, out IntPtr duplicateTokenHandle);

		// Token: 0x060004EA RID: 1258
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern void OutputDebugString(string message);

		// Token: 0x060004EB RID: 1259
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryPerformanceCounter(out ulong lpPerformanceCount);

		// Token: 0x060004EC RID: 1260
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryPerformanceFrequency(out ulong lpPerformanceFrequency);

		// Token: 0x060004ED RID: 1261
		[DllImport("kernel32.dll")]
		internal static extern int GetCurrentProcessId();

		// Token: 0x060004EE RID: 1262
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern uint GetModuleFileName([In] IntPtr hModule, [Out] StringBuilder lpFilename, [MarshalAs(UnmanagedType.U4)] [In] int nSize);

		// Token: 0x060004EF RID: 1263
		[DllImport("ole32.dll")]
		internal static extern int CoGetObjectContext(ref Guid iid, out AspHelper.IObjectContext g);
	}
}
