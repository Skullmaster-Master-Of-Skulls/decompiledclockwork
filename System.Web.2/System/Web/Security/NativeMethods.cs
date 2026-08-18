using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Web.Security
{
	// Token: 0x020005CB RID: 1483
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x06004B3F RID: 19263
		[DllImport("Netapi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DsGetDcNameW")]
		internal static extern int DsGetDcName([In] string computerName, [In] string domainName, [In] IntPtr domainGuid, [In] string siteName, [In] uint flags, out IntPtr domainControllerInfo);

		// Token: 0x06004B40 RID: 19264
		[DllImport("Netapi32.dll")]
		internal static extern int NetApiBufferFree([In] IntPtr buffer);

		// Token: 0x06004B41 RID: 19265
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern int FormatMessageW([In] int dwFlags, [In] int lpSource, [In] int dwMessageId, [In] int dwLanguageId, [Out] StringBuilder lpBuffer, [In] int nSize, [In] int arguments);

		// Token: 0x04002885 RID: 10373
		internal const int ERROR_NO_SUCH_DOMAIN = 1355;

		// Token: 0x04002886 RID: 10374
		internal const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x04002887 RID: 10375
		internal const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x04002888 RID: 10376
		internal const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
	}
}
